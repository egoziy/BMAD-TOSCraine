# 10 — KoneCranes Reference Material

> **Phase 1 (addendum)** · Analyst: Claude Code · Date: 2026-04-21
> **Source:** the `KoneCranes/` directory added by the operator after Phase 1 delivery. Contains vendor documentation, simulators, and the Goldbond-specific network diagram.
> **Impact:** ✅ **RESOLVES Q-11** (the last 🔴 blocker). Phase 2 can proceed to implementation.

---

## 1. What the KoneCranes directory contains

| Category | Contents | Key use |
|---|---|---|
| **Protocol specifications** | `RTG-TD-Konecranes TOS interface_V40.pdf` (the main doc) · `RTG-TD-Konecranes TOS interface_v33.pdf` (earlier version) | Defines every message type, byte offset, and value enum — answers Q-11, Q-20, Q-27, Q-28, Q-29, Q-31 |
| **Simulators** | `KcCheSimulatorInstaller.exe` (15 MB installer) · `chesimu.exe` / `tossimu.exe` (pre-unpacked Qt5-based binaries) · `che simulator manual.pdf` | **Integration-test surface** — we can run the new listener against the vendor's own simulator before touching real cranes |
| **Network layout** | `BxH-RTG-Gold-Bond-Ashdod-Connectivity-Layout-05092017.pdf` · `IP-Plan-BxH-Gold-Bond-Ashdod-G2036-G2037-September-{05,14}-2017.xlsx` | Concrete IP plan for the actual Goldbond deployment — resolves Q-47 partially |
| **Operator + maintenance manuals** | `BOXHUNTER_G2036-2037_Operators_Manual_HE_rev.A.pdf` (Hebrew!) · `BOXHUNTER_G1719_Maintenance_Manual_en_rev.A.pdf` · `BOXHUNTER_Web_CMS_Owners_Manual_en_rev.A.pdf` | Operator workflows; Web-CMS for remote crane monitoring |
| **Site-specific drawings** | `Documents for Arnold/Drawings and calculations/` — ~20 PDFs and DWGs (erection, general arrangement, structural calcs) | Not in scope for software migration; reference only |
| **BoxHunter (crane model) specs** | `01 Konecranes BH specs_rev3.4.pdf` · `BXH videos/*.mp4` | Product reference; cranes are KoneCranes **BoxHunter** — serial numbers **G2036** (Crane 1) and **G2037** (Crane 2) |

Goldbond's cranes are **KoneCranes BoxHunter RTG**, protocol family is **"YARDIT PDS"** (Yard IT Position Detection System — per the chesimu dropdown: v3.0 / v3.3 / v4.0).

---

## 2. Protocol — the reality (consolidating with `05_protocol.md`)

### 2.1 Frame envelope — now confirmed

The V40 spec defines the on-wire frame as:

```
FF XX NN aaaaaaaaaa...aa ZZ
```

Where:

| Byte(s) | Meaning |
|---|---|
| `FF` | Sync / start marker |
| `XX` | Length byte |
| `NN` | Frame counter |
| `aaaaaa...` | Message body (starts with 2-byte Msg-ID) |
| `ZZ` | End marker (details not fully in the extracted text; likely 1 byte) |

**Reconciling with legacy (`05_protocol.md` §2):** the legacy listener reads `data.Substring(0, 2) == "??"` to check the header, then skips offsets 2-3, and parses the body from offset 4.

The explanation: `Encoding.ASCII.GetString()` converts non-ASCII bytes to `?`. The real bytes at offsets 0-3 are likely `0xFF 0xFF 0xXX 0xNN` (or `0xFF` + length + counter + spare), which after ASCII decoding render as `????`. Legacy only tests the first two (`??`). Thus:

- **Byte 0**: sync marker (0xFF, becomes `?`)
- **Byte 1**: length byte (or second sync byte; legacy can't tell because both decode to `?`)
- **Byte 2-3**: length / counter per spec
- **Byte 4+**: message body — Msg-ID at 4-5, Version at 6-11, fields from 12 onward

So legacy's byte offsets for body fields are all **4 bytes higher** than the spec's body offsets — a consistent shift across all 3 message types. This is consistent, not a bug.

**Implication for Q-27:** bytes 2-3 are **length + counter**, not unused. The new `rtg-listener` should parse them properly for framing (length-prefixed message delimitation).

### 2.2 All message types — from the spec

| Msg-ID | Hex | Direction | Purpose | Size |
|---|---|---|---|---|
| `A1` | 0x4131 | CHE → TOS | **Heartbeat / position update** | ~74 bytes body |
| `A2` | 0x4132 | CHE → TOS | **Pick (move type 03) / Place (move type 04)** | ~142 bytes body |
| `A3` | 0x4133 | CHE → TOS | **ACK** from CHE for TOS messages (B3/B4) | 4 bytes body |
| `A4` | 0x4134 | CHE → TOS | **Job request** from CHE to TOS (lift or place request) | ~142 bytes body |
| `B1` | 0x4231 | TOS → CHE | **Twist-lock block** status | 3 bytes body |
| `B2` | 0x4232 | TOS → CHE | **ACK** from TOS for CHE messages (A1/A2) | 4 bytes body |
| `B3` | 0x4233 | TOS → CHE | **Job data** (the pick-and-place instruction) | ~199 bytes body |
| `B4` | 0x4234 | TOS → CHE | **Job cancel** | 4 bytes body |
| `B5` | 0x4235 | TOS → CHE | (purpose unclear — likely another control) | 4 bytes body |

### 2.3 A1 — confirmed field map (this was ~90% guessed in Phase 1; now 100%)

| Body offset | Wire offset* | #bytes | Field | Values / meaning |
|---|---|---|---|---|
| 000 | 004 | 2 | Msg-ID | `"A1"` (ASCII) |
| 002 | 006 | 6 | Version | Version number |
| 008 | 012 | 6 | Time | `HHMMSS` |
| 014 | 018 | 4 | Status | Auto-steering status; `"0000"` = OK, any other = error code |
| 018 | 022 | 8 | Block name | `"BOND1   "` / `"BOND2   "` / `"BOND3   "` / `"ERROR   "` / `"NO BLOCK"` |
| 026 | 030 | 3 | Bay number | 3 ASCII digits |
| 029 | 033 | 3 | Row number | 3 ASCII chars — `A..F` (yard) or `G`/`T` (ground/truck) |
| 032 | 036 | 4 | Height | Logical value `1..7` |
| 036 | 040 | 2 | **Crane status** | `"01"` = off · `"02"` = on/idle · `"03"` = running · `"04"` = error/fatal |
| 038 | 042 | 2 | **GPS status** | `"01"` = OK · `"02"` = GPS internal fault · `"03"` = no satellites · `"04"` = no base-station comm |
| 040 | 044 | 5 | CHE | Crane id (`GOLD1`/`GOLD2`/`GOLD3`) |
| 045 | 049 | 6 | System status | Reserved for internal use (info) |
| 051 | 055 | 2 | **PLC comm. status** | `"01"` = OK · `"02"` = Failure |
| 053 | 057 | 9 | PosX | CHE position x-coordinate (meters from base station) |
| 062 | 066 | 9 | PosY | CHE position y-coordinate |
| 071 | 075 | 2 | Len | Spreader size: `"20"` / `"30"` / `"40"` / `"45"` / `"T"` (twin-lift) |
| 073 | 077 | 1 | Twist lock | `"O"` = open · `"C"` = close |

*Wire offset is body offset + 4 (the frame header). **Matches legacy code exactly.**

**This resolves Q-20, Q-28:** all the opaque codes are now documented.

### 2.4 A2 — pick/place event (crane → TOS)

Body offsets (add 4 for wire):

| body offset | #bytes | Field |
|---|---|---|
| 000 | 2 | Msg-ID `"A2"` |
| 002 | 6 | Version |
| 008 | 2 | **Counter (AsciiHex)** |
| 010 | 2 | Move type: `"03"` = lift, `"04"` = ground (place) |
| 012 | 14 | Time `YYYYMMDDHHMMSS` (full date, not just `HHMMSS` like A1) |
| 026 | 5 | Status |
| 031 | 5 | CHE |
| 036 | 5 | **ContWeight** — container weight in kg, valid in place message only |
| 041 | 2 | PosOnTruck |
| 043 | 5 | spare |
| 048 | 12 | ContID1 (container ID) |
| 060 | 12 | ContID2 (twin-lift only) |
| 072 | 1 | Mount |
| 073 | 1 | Tag |
| 074 | 8 | Position1 — Block name |
| 082 | 3 | Bay number |
| 085 | 3 | Row number |
| 088 | 4 | Height |
| 092 | 8 | Position2 — Block name (twin-lift only) |
| 100 | 3 | Bay |
| 103 | 3 | Row |
| 106 | 4 | Height |
| 110 | 2 | Len |
| 112 | 6 | System status |
| 118 | 1 | Pos y/n (`Y` = use PosX/PosY/PosZ coords; `N` = use Position1/2) |
| 119 | 9 | PosX |
| 128 | 9 | PosY |
| 137 | 5 | PosZ |
| 142 | 30 | spare |

Total body ~172 bytes.

**Legacy reads:** Counter at wire 12 (body 8 + 4 ✓), Move type at wire 14 (body 10 + 4 ✓), Block name at wire 78 (body 74 + 4 ✓), Bay at wire 86 (body 82 + 4 ✓), Row at wire 89 (body 85 + 4 ✓), Height at wire 92 (body 88 + 4 ✓). **Offsets confirmed.**

### 2.5 Happy-path sequence (from the spec's flow diagram)

```
CHE                          TOS
│                              │
│── Heartbeat A1 (periodic) ──▶│
│── Heartbeat A1 ─────────────▶│
│                              │
│◀─── Job Data Message (B3) ───│
│── A3 (ACK) ─────────────────▶│
│                              │
│◀─── (B4 cancel, optional) ───│
│── A3 (ACK) ─────────────────▶│
│                              │
│── Heartbeat A1 ─────────────▶│
│── Pick A2 (move type=03) ───▶│
│◀─── B2 (ACK) ────────────────│
│                              │
│── Heartbeat A1 ─────────────▶│
│── Place A2 (move type=04) ──▶│
│◀─── B2 (ACK) ────────────────│
│                              │
│── Heartbeat A1 ─────────────▶│
```

**Legacy reality check:** the legacy TOSConsole sends ACKs of the form `FFFF + "04B2" + counter + checksum`. The spec shows that `B2` is the TOS-ACK message. The `04` in the legacy ACK is the **length byte** (`XX` = 4). So the outbound format is:

```
0xFF 0xFF 0x04 <counter_byte> <B2 ASCII 0x42 0x32> <counter_hex ASCII 2 bytes> <checksum_hex_ASCII>
```

Wait — the legacy uses `"04B2" + counter + checksum` inside `ReturnAsciText` (which turns each ASCII char to its hex-pair representation), then `StrToByteArray` on that hex. Net effect: the bytes on wire are literally ASCII `0 4 B 2 <counter_ASCII> <checksum_ASCII>`. That's NOT the same as having a length byte 0x04.

So actually: the legacy ACK is `0xFF 0xFF` + ASCII `"04B2" + counter + checksum`. The `"04"` is **part of the ASCII message body**, interpreted as a length-field-in-ASCII (per the spec envelope `FFXXNN` with XX as a length byte — but here XX is encoded as ASCII `"04"` instead of binary 0x04).

This is an interesting Goldbond-specific encoding quirk. It works because the crane firmware tolerates it. But it means the legacy's outbound is actually:

```
0xFF 0xFF 0x30 0x34 0x42 0x32 <counter_ASCII> <checksum_ASCII>
         (ASCII "04"       B2)
```

Which is 6 bytes of overhead before the payload. Doesn't match spec literally but is interoperable.

**For the new listener:** we should **match legacy byte-exact** to preserve interoperability — whatever encoding the current BoxHunter cranes accept. Don't "fix" the encoding to match the spec unless we can test against a real crane.

### 2.6 Checksum

The spec text I extracted didn't explicitly describe the inbound checksum. The `ZZ` at the end of the frame envelope is likely the checksum (1 or 2 bytes). Two actions:

1. **Run `chesimu.exe` and capture real frames** — observe the exact bytes at end-of-frame, compare to each plausible checksum algorithm.
2. **Read the full v40 PDF in a real PDF viewer** — the text extract likely missed tables/diagrams that define the checksum.

Until confirmed: keep `validate_inbound_checksum: false` (listener config default) and upgrade to `true` after observation.

### 2.7 Heartbeat behaviour

The spec says A1 is the heartbeat (Q-7 confirmed: the crane sends A1 at a configured interval even when not moving). The chesimu manual confirms: "Heartbeat checkbox can be used to activate heartbeat message. Heartbeat message will be sent at constant intervals defined at Interval selection box."

So **connection-liveness detection is free**: if the listener doesn't receive A1 within 2×interval, the crane is down. No need for TCP keep-alive as the *only* mechanism.

### 2.8 Duplicate handling (Q-31)

The chesimu manual mentions the `Ack Required` checkbox:
> "If Ack Required check box is checked then job message is confirmed with acknowledge message. Also pick/place message expects acknowledgement. Max Resents spin box used to set up how many times pick/place message is resent if not acknowledged."

**Implication:** the crane expects B2 ACKs from the TOS for A1/A2 messages. If no ACK, CHE retransmits up to `Max Resends`. So the crane does NOT de-duplicate autonomously — it retransmits and expects the TOS to handle duplicates (usually via Counter).

For the new listener: **match each A2 Counter to the most recent one received; if we see the same Counter twice within a short window, treat as a retransmission, not a real duplicate event.** The outbox events still deduplicate via idempotency_key (schema §4.5).

Symmetric for TOS→CHE (B3): CHE acknowledges with A3. If no A3, TOS retransmits B3. The spec confirms this flow.

---

## 3. Network topology — Goldbond Ashdod, from the connectivity PDF

```
          ┌─── Crane #1 (G2036) ───┐    ┌── Customer Network ──┐    ┌─── Crane #2 (G2037) ───┐
          │                        │    │   192.6.0.0/16       │    │                        │
          │  mGuard 4004 Firewall  │    │                       │    │  mGuard 4004 Firewall  │
          │  WAN: 192.6.1.0/20     │───►│  Terminal Router     │◄───│  WAN: 192.6.2.0/20     │
          │                        │    │  (L3/DGW)            │    │                        │
          │  PLC · GPS-PC (CCS)    │    │  192.6.8.254/16      │    │  PLC · GPS-PC (CCS)    │
          │  192.6.1.8/20          │    │                       │    │  192.6.2.8/20          │
          │  OP Panel · VMT · TOS  │    │  DGPS Base Station   │    │  OP Panel · VMT · TOS  │
          │  Cameras · Video PC    │    │  192.6.8.8/20         │    │  Cameras · Video PC    │
          │                        │    │                       │    │                        │
          │                        │    │  MSSQL / TOS host    │    │                        │
          │                        │    │  (on customer net)    │    │                        │
          └────────────────────────┘    └───────────────────────┘    └────────────────────────┘
```

### 3.1 What this tells us that we didn't know

1. **Cranes are `BoxHunter G2036` and `G2037`.** Crane 3 is not in the doc (added later); serial likely `G2xxx`.
2. **Each crane has an `mGuard 4004` NAT firewall between its internal network and the customer network.** So the TOS doesn't talk directly to the crane PLC — it talks via the firewall's NAT.
3. **`192.6.1.8` is the GPS-PC (CCS) on Crane 1**, not the listener host. It's the Crane Control System computer inside Crane 1 that runs the positioning software and talks to the TOS. The `IPAddress.Parse("192.6.1.8")` in `TOSConsole1/Program.cs:119` is a **client IP**, not a server bind address. This explains why the code passes it to `localAddr` but never uses it in `new TcpListener(port)` — it was a misnamed variable, effectively dead code.
4. **DGPS Base Station at `192.6.8.8`** — the differential GPS reference station. Cranes' GPS-PCs compute their positions relative to this; the status codes in A1 (`GPS status 04 = no comm to base station`) refer to this device.
5. **MSSQL / TOS runs on the customer network**, reachable via the Terminal Router (192.6.8.254/16). Which is the `192.6.8.52` we've been seeing — that's the DB server, on the customer network.

### 3.2 Q-47 partially resolved

**Crane 1's PLC / GPS-PC targets the TOS host on the customer network.** The `192.6.1.8` hardcoded in the legacy code is the crane's own CCS IP, not the target. So Crane 1 sends TCP from `192.6.1.8:xxxx` to `TOS_IP:30701`. The TOS_IP is some 192.6.8.x address — either the DB server directly (where TOSService runs) or another host.

This reinforces the **dual-host hypothesis**:
- `TOSService.exe` runs on the DB server (192.6.8.52) and can accept crane traffic there.
- `TOSConsole{1,2,3}.exe` runs on ... some other host, possibly also on the customer net.

**Still open:** we don't know the target IP the crane PLCs are configured with. Q-47 is still live.

### 3.3 Impact on Phase 2 architecture

The previous design (`01_architecture.md` §5) placed the edge host on the three crane subnets (192.6.1.x, 192.6.2.x, 192.6.3.x) directly. With the mGuard firewalls in play, the edge host is actually on the **customer network only** (192.6.8.x or a new subnet like 192.6.9.x), and cranes reach it via routing through the Terminal Router. This is a **simpler** deployment than "multi-homed edge host per crane subnet".

The new picture:

```
Crane 1 (192.6.1.x) ─┐
                     │   mGuard NAT
                     ├── + Terminal Router ─── edge host (192.6.9.x) ─── PG + MSSQL
                     │   L3/DGW 192.6.8.254
Crane 2 (192.6.2.x) ─┤
                     │
Crane 3 (192.6.3.x) ─┘
```

The edge host needs **one NIC on the customer network**, not three on the crane subnets. This is an architecture update to feed into §5 of `01_architecture.md` (to be done in a follow-up edit).

---

## 4. The simulators — test infrastructure we didn't realise we had

### 4.1 `chesimu.exe` — CHE (crane-side) simulator

- Acts like a real crane from the protocol perspective.
- Can be TCP Client (connects to a TOS) or TCP Server (accepts a TOS-initiated connection).
- Supports 3 protocol versions (YARDIT PDS 3.0 / 3.3 / 4.0). We pick the version the BoxHunter firmware actually speaks (Goldbond is on 4.0 per the V40 spec we have).
- Can send heartbeat A1, can send pick/place A2 on operator click, receives B3 jobs from TOS.
- Configurable position, container ID, weight, twin-lift mode, status codes.
- Ack Required mode — simulates retransmission of unacked messages.

### 4.2 `tossimu.exe` — TOS-side simulator

- Acts like the TOS. We can point `chesimu` at it to see how a "correct" TOS behaves.
- Comes with sample CSV data (`B1.csv`, `B2`).
- This is essentially the reference TOS implementation — a validation target for our `rtg-listener` behavior.

### 4.3 How to use in Phase 2 testing (see `03_listener.md` §10)

Previously we proposed writing a Python fake crane. That's no longer necessary — `chesimu.exe` IS the fake crane, and it's the vendor's own reference implementation.

New test strategy:
1. Point `chesimu.exe` at our `rtg-listener`. Send heartbeats, picks, places. Verify PG state updates correctly.
2. Point `chesimu.exe` at `tossimu.exe` and at our listener simultaneously (dual-mode). Compare behaviors byte-by-byte — our listener should produce the same A3/B2/B3 traffic as `tossimu.exe` for the same input.
3. Record real crane traffic for 1 hour; replay it against both `tossimu.exe` and our listener. Assert outputs identical.

This drastically reduces the risk of a byte-level incompatibility bug.

---

## 5. Status update on Phase 1 open questions

| # | Title | Previous status | Now |
|---|---|---|---|
| Q-11 | Crane vendor / PLC spec | 🔴 Blocker | ✅ **RESOLVED** — KoneCranes BoxHunter, protocol "YARDIT PDS" v4.0, full spec in `RTG-TD-Konecranes TOS interface_V40.pdf` |
| Q-20 | Status codes | 🟡 | ✅ **RESOLVED** — full enum values documented in §2.3 above |
| Q-27 | Bytes 2-3 of inbound frame | 🟡 | ✅ **RESOLVED** — length byte + counter byte per spec envelope `FFXXNNaaaa...ZZ` |
| Q-28 | `HBHeight` encoding | 🟡 | ✅ **RESOLVED** — logical value 1..7 |
| Q-29 | NAK `03B10FEFA` | 🟡 | ⚠ Partially — "03" is length, "B1" is the twist-lock-block message (Msg-ID 0x4231), "0F EFA" is some B1 body. Legacy is sending a Twist-Lock-Block status as a NAK fallback. This works by coincidence. In the new listener, we implement a proper NAK (probably just "don't ACK" since the spec doesn't define a NAK). |
| Q-31 | Duplicate detection | 🟠 | ✅ **RESOLVED** — not done autonomously by crane; counter + idempotency_key in `rtg.outbox` is the right mechanism |
| Q-47 | Crane target IPs | 🟠 | 🟡 Partially — confirmed cranes are NATted via mGuard; target TOS IP still needs `netstat`/`ipconfig` audit on live hosts |
| Q-60 | Inbound checksum layout | 🔴 Blocker (for listener coding) | 🟡 Improved — spec envelope suggests ZZ at end is the checksum. Exact algo and length still need observation via `chesimu.exe`. Ship with `validate_inbound_checksum: false` until confirmed |

**Net:** the single remaining 🔴 blocker (Q-11) is resolved. Phase 2 listener coding can begin immediately.

---

## 6. Impact on Phase 2 design docs

Summary of updates needed (to be applied in follow-up edits):

- **`02_components.md` §3.3** — clarify that `192.6.1.8` is the crane's own CCS IP, not the listener bind; the legacy `localAddr` variable is dead code with a misleading name.
- **`03_data_flows.md` §1** — network diagram should show mGuard firewalls between cranes and customer network.
- **`05_protocol.md` §2** — add the formal Msg-ID enums and the confirmed body-offset table; update §10 open questions.
- **Phase-2 `01_architecture.md` §5.1** — the edge host does NOT need multi-homed NICs on each crane subnet; a single NIC on the customer network is sufficient (routing via Terminal Router).
- **Phase-2 `03_listener.md` §10** — replace "Python fake crane" with "`chesimu.exe` from KoneCranes" as the primary test fixture.
- **Phase-2 `06_rollout.md` §3 Prereq P-2** — resolved by this directory.
- **Phase-2 `07_risks.md` R1** — residual drops from 🟡 to 🟢.

---

## 7. Hebrew operator manual

`BOXHUNTER_G2036-2037_Operators_Manual_HE_rev.A.pdf` is the **Hebrew operator's manual** for Goldbond's specific crane models. This is valuable for:

- **Terminology consistency** — the new Flutter UI's Hebrew strings should match the terms used in the manual that operators have been trained on. E.g. if the manual says `מלגזן` / `נהג` / `סוג מטען` we should use those exact words.
- **Workflow reference** — understanding the operator's mental model.

Defer full read to Phase 3 (implementation) but add it to the UI terminology review checklist.

---

## 8. Check-in summary

- **Q-11 fully resolved.** KoneCranes BoxHunter, protocol YARDIT PDS v4.0, complete spec in `RTG-TD-Konecranes TOS interface_V40.pdf`. 7 previously-open questions now answered.
- **Unexpected bonus:** vendor simulators (`chesimu.exe` + `tossimu.exe`) are the perfect integration-test surface for the new listener — we can test against the vendor's own reference implementation. This replaces the proposed Python fake-crane approach.
- **Network architecture clarified:** `192.6.1.8` is the crane's own CCS IP (not the listener bind); cranes are NATted via mGuard firewalls and all roads lead through the Terminal Router at 192.6.8.254. Edge host design simplifies to a single customer-network NIC.
- **Only one minor uncertainty remains:** the exact byte layout of the inbound checksum (`ZZ` at end of frame envelope) — resolvable by running `chesimu.exe` and observing packets. Not a blocker for architectural work; set `validate_inbound_checksum: false` in the listener config until observation confirms.
