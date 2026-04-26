# 05 — Network & Protocol

> **Phase 1 — Step 1.5** · Analyst: Claude Code · Date: 2026-04-21
> **Scope:** the wire protocol spoken between the RTG crane PLC / transponder and `TOSConsole{1,2,3}.exe`. Evidence is taken from the listener code (`From TFS/RTG/TOSConsole{1,2,3}/TOSConsole/Program.cs`), `Crc32.cs`, and the UI message-builder in `Form1.cs`. Every byte-offset below is *confirmed* from code; every interpretation is marked **CONFIRMED** / **INFERRED** / **UNKNOWN**.

---

## 1. Transport

| Property | Value | Evidence |
|---|---|---|
| Transport | **TCP** (stream, reliable, ordered) | `TOSConsole1/Program.cs:138` `new TcpListener(port)` |
| Direction | Listener **server-side**; crane acts as TCP **client** | `Program.cs:155` `server.AcceptTcpClient()` |
| Port (Crane 1) | `30701` | `Program.cs:118` |
| Port (Crane 2) | `30702` | `TOSConsole2/Program.cs` |
| Port (Crane 3) | `30703` | `TOSConsole3/Program.cs` |
| Bind address (in source) | `IPAddress.Parse("192.6.1.8")` — **this is the listener host's interface on Crane 1's private network** (per operator update 2026-04-21: each crane runs on its own subnet; Crane 1 = `192.6.1.x`). The listener host is **multi-homed**, with one interface on each crane's subnet. | `Program.cs:119` |
| Actual bind address (runtime) | **Any** — the `TcpListener` is instantiated with only the port: `new TcpListener(port)` (`Program.cs:138`). The `localAddr` is never passed. The listener therefore binds on **all** interfaces (including the DB subnet), not just the crane-1 interface. Possibly an accidental wide-open binding. | `Program.cs:119,138` |
| Single persistent connection | Confirmed — the listener does **one** `AcceptTcpClient()` per `START:` iteration and never returns to accept another until it crashes. | `Program.cs:155` |
| TLS / TCPS | None | No `SslStream`, no TLS references anywhere |
| Encoding | ASCII, uppercased on receipt | `Program.cs:211,214` `Encoding.ASCII.GetString(...).ToUpper()` |
| Framing | **No explicit framing.** Messages identified by magic header `??` + fixed byte offsets. One `stream.Read(bytes, 0, 256)` call per message iteration. | `Program.cs:206` |
| Buffer size | 256 bytes | `Program.cs:142-144` |

### 1.1 Consequence — TCP vs. message boundaries

The listener treats each `stream.Read` return value as "one message". TCP does not guarantee that. A slow or fragmented network will either:

- **Split one logical message** across two reads — the first read returns bytes 0..N of a >N-byte message; `data.Substring(0, 2)` still sees `??`, field extraction throws `ArgumentOutOfRangeException`, listener catches → `goto Outer` → discards the half-message → reads again; now the second fragment (without `??` header) is consumed and fails the header check → fallback NAK is sent → **message lost silently**.
- **Coalesce two logical messages** into one read — `data` contains `??A1...??A2...`. Field extraction processes the first portion as one message, the trailing `??A2...` is completely ignored.

This class of bugs is exactly what a **missing framing layer** produces. A significant portion of the 1.6 M rows in `RG_ErrorLog` likely traces to this (Q-15-B).

---

## 2. Inbound frame format (crane → listener)

**CONFIRMED** offsets (derived from the exact `data.Substring(offset, length)` calls in `TOSConsole1/Program.cs`):

### 2.1 Shared header (all inbound messages)

| Offset | Len | Field | Value / meaning | Confidence |
|---:|---:|---|---|---|
| 0 | 2 | Magic | Literal `??` | CONFIRMED (Program.cs:219) |
| 2 | 2 | **Unknown** | Not read by listener code | UNKNOWN (Q-27) |
| 4 | 2 | Message type | `A1`, `A2`, `A3` | CONFIRMED (Program.cs:222, 246, 332) |

*Note:* after step 4, the listener branches on message type. The next positions differ per type.

### 2.2 Message type `A1` — position / status update

Every row in `RG_A1` is written from one of these. Field offsets below are bytes from position 0 of `data`.

| Offset | Len | Field → DB column | Format | Example (inferred) |
|---:|---:|---|---|---|
| 12 | 6 | `Time` → `RG_A1.Time` | `HHmmss` ASCII, digits only | `"143022"` = 14:30:22 |
| 18 | 4 | `Status` → `RG_A1.Status` | 4-char code | Unknown codes; Q-20 |
| 22 | 8 | `HBBlockName` → `RG_A1.HBBlockName` | ASCII, space-padded | `"BOND1   "`, `"BOND2   "`, `"GROUND  "`, `"TRUCK   "`, ... |
| 30 | 3 | `HBBayNumber` → `RG_A1.HBBayNumber` | ASCII digits | `"103"`, `"185"`. Each bay is typically one of the values seen in `V_MapRTGBond1Pre` (100-132 for BOND1, 133-185 for BOND2). |
| 33 | 3 | `HBRowNumber` → `RG_A1.HBRowNumber` | Typically `A..F` + space, sometimes `G` (ground) or `T` (truck) | Inferred from `.Replace("A","0").Replace("B","1")...` in `Form1.cs:858` |
| 36 | 4 | `HBHeight` → `RG_A1.HBHeight` | 4-char code | e.g. `"1   "` or `"A   "` (multiple encodings — Q-28) |
| 40 | 2 | `CraneStatus` → `RG_A1.CraneStatus` | 2-char code | UNKNOWN (Q-20) |
| 42 | 2 | `GPSStatus` → `RG_A1.GPSStatus` | 2-char code | UNKNOWN (Q-20) |
| 44 | 5 | (CHE — but only read by `TOSService`, not by `TOSConsole`) | `"GOLD1"` / `"GOLD2"` / `"GOLD3"` | CONFIRMED — `TOSService/Program.cs:134` reads this offset, but **TOSConsole hard-codes `CHE='GOLD{1,2,3}'` at compile time** and does not read this position. Inconsistency. |
| 55 | 2 | `PLC` → `RG_A1.PLC` | 2-char code | UNKNOWN |
| 75 | 2 | `Len` → `RG_A1.Len` | Container length (`"20"` / `"40"` / `"45"`) | INFERRED |
| 77 | 1 | `TwistLock` → `RG_A1.TwistLock` | Single char, e.g. `"1"` (locked) / `"0"` (unlocked) | INFERRED |

**Minimum inbound A1 packet length: 78 bytes** (offset 77 + 1). No end-of-frame marker observed.

**Fields NOT read by listener** at offsets 2–4, 6–12, 18, 44–55, 57–75, 78+ — any of these could hold a checksum, packet length, secondary CHE identifier, sequence number, etc. **The listener does not validate.** This is one of the single most impactful facts for Phase 2's protocol adapter: we must negotiate the *real* packet schema with the crane vendor (Q-11) because the code cannot tell us everything the packet contains.

### 2.3 Message type `A2` — work-cycle event (crane-initiated)

Sub-type in bytes 14-15:
- `03` = **PICK** — crane reports it has picked up a container.
- `04` = **PLACE** — crane reports it has placed a container.

| Offset | Len | Field | Meaning |
|---:|---:|---|---|
| 12 | 2 | Counter echo | Hex-encoded sequence number that matches a `RG_B3.Counter` row |
| 14 | 2 | Sub-type | `03` or `04` |
| 40 | 5 | Weight (on PLACE only) → `CO_Containers.RtgWeight` | ASCII digits — the crane's scale reading in some unit |
| 78 | 5 | **BlocCode** (Lift / Place) → matches `TB_Location.BlocCode` | `"BOND1"`, `"BOND2"`, `"BOND3"`, `"G    "`, `"T    "` (ground / truck) |
| 86 | 3 | Bay number | `"103"` etc. |
| 89 | 1 | Row letter | `A`..`F` (yard row) or `G`/`T` (ground/truck destination) |
| 92 | 1 | Height | `1`..`5` or similar |
| 114 | 2 | (Len — in commented-out code only) | Listener has dead code referring to `data.Substring(114, 2)` — probably the length field |

**Minimum inbound A2 packet length: 93 bytes** (offset 92 + 1). If `data.Length < 93` on an A2 frame, `IndexOutOfRangeException` is thrown and caught (`Program.cs:346-351`).

**Key idiom — composite location code:** `data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1)` is the 5-char concatenation that matches `TB_Location.LocationCode`. So the location string is **3 chars bay + 1 char row + 1 char height = 5 chars total**, and `TB_Location.LocationCode` is defined as `nvarchar(11)` — meaning the remaining 6 chars are padding or additional coordinate qualifiers in other contexts.

### 2.4 Message type `A3` — cancel acknowledgment (crane-initiated)

| Offset | Len | Field | Meaning |
|---:|---:|---|---|
| 6 | 2 | Counter | Echoes `RG_B3.Counter` of the job being cancelled |

**Effect in listener:**
```sql
UPDATE RG_B3 SET CancelDate  = GetDate() WHERE  A3Date IS NOT NULL AND  Counter='<counter>' AND CHE = 'GOLD1';
UPDATE RG_B3 SET A3Date      = GetDate() WHERE  A3Date IS NULL     AND  Counter='<counter>' AND CHE = 'GOLD1';
```
(`Program.cs:334-335`)

Note the two statements run **in order**: the first sets `CancelDate` only if `A3Date` was already set; the second sets `A3Date` only if it wasn't. The combined effect is idempotent-ish — first time you see A3, it sets `A3Date`; second time, it sets `CancelDate`.

**Minimum inbound A3 length: 8 bytes** (offset 6 + 2).

### 2.5 Fallback — unknown message type

If the frame starts with `??` but the message type is not `A1`/`A2`/`A3` (or if `A2` is neither `03` nor `04`), the listener falls through to `Program.cs:338-339` and sends this byte stream back to the crane:

```
0xFF 0xFF 0x30 0x33 0x42 0x31 0x30 0x46 0x45 0x46 0x41
```

Which is `0xFF 0xFF` + ASCII `"03B10FEFA"`. **INFERRED:** this is a NAK / "unknown-type" response. The format suggests: `03` = length? (probably not), `B1` = response type, `0F` = error code, `EFA` = checksum. Exact meaning is an open question (Q-29).

### 2.6 Header-check failure

If the first two bytes are not `??`, the listener does NOT go through the normal decode; instead, after the full `if (data.Substring(0,2) == "??")` block is skipped, execution reaches the unknown-type fallback above (same NAK bytes). So any non-`??` input also receives the same NAK. No distinction logged.

---

## 3. Outbound frame format (listener → crane)

### 3.1 ACK of `A2/03` and `A2/04`

On successful pick/place processing, `TOSConsole1/Program.cs:276,326`:

```csharp
byte[] msg1 = StrToByteArray("FFFF"
    + ReturnAsciText("04B2" + data.Substring(12, 2)         // echoed counter
                     + calcChecksum("04B2" + data.Substring(12, 2))));
stream1.Write(msg1, 0, msg1.Length);
```

Decoded:

| Field | Source | Bytes (on wire) |
|---|---|---|
| Start-of-frame | Literal `FFFF` → bytes `0xFF 0xFF` | 2 |
| Body | Literal `04B2` + Counter(2 chars) + Checksum(variable) — each character encoded as its 2-hex-digit ASCII representation via `ReturnAsciText`, then parsed back to 1 byte per hex-pair via `StrToByteArray` | N |
| End-of-frame | **None** | 0 |

**The `ReturnAsciText → StrToByteArray` double-transformation is a no-op.** Concretely:

- `ReturnAsciText("04B2")` produces `"30343432"` (each of the 4 ASCII chars `0`, `4`, `B`, `2` becomes its hex byte representation as uppercase hex chars: `0`→`30`, `4`→`34`, `B`→`42`, `2`→`32`).
- `StrToByteArray("30343432")` converts those 8 hex characters back to 4 bytes: `0x30 0x34 0x42 0x32`.
- Result: exactly the same 4 bytes as `"04B2"` in ASCII.

So the ACK on wire is simply: `0xFF 0xFF` + `<"04B2" ASCII>` + `<echoed counter, 2 ASCII chars>` + `<checksum hex ASCII, variable length>`.

### 3.2 The checksum algorithm (`calcChecksum`)

From `TOSConsole1/Program.cs:30-42` (identical in `Crc32.cs:18-30` and `Form1.cs:742-754`):

```csharp
static string calcChecksum(string instr)
{
    int Checksum = 0;
    for (int i = 0; i < instr.Length; i++)
        Checksum += (int)(instr[i]);
    ushort twosComp = (ushort)(~Checksum + 1);
    return string.Format("{0:X}", twosComp);   // uppercase hex, NO PADDING
}
```

Properties:

- **Sum of all input character codes**, treated as `int`.
- Two's-complement negation, truncated to 16 bits (`ushort`).
- Formatted with `{0:X}` — **no leading zeros**. So `0x3` is `"3"`, `0x00FF` is `"FF"`, `0x1234` is `"1234"`. Length on wire varies from **1 to 4 hex digits**.
- **This is NOT CRC-16/CCITT** — despite the class name `Crc16Ccitt`. It is a modular additive checksum over 16 bits. The class name is misleading.
- Because the checksum is variable-length, the receiver cannot distinguish checksum boundary from next field. **For outbound** the crane sees only one trailing field, so this works by accident; **for inbound**, the listener does NOT compute or validate any checksum.
- Sensitive to the *encoded* representation: the checksum is over the ASCII body `"04B2<counter>"`, NOT over the bytes that go on the wire (which are the same — see §3.1).

### 3.3 Broadcasts (sending pending jobs)

At connection-open and on every `goto Outer`, the listener reads:

```sql
SELECT Message       FROM RG_B3 WHERE CHE='GOLD1' AND A3Date   IS NULL AND PickDate IS NULL
UNION ALL
SELECT MessageCancel FROM RG_B3 WHERE CHE='GOLD1' AND MessageCancel IS NOT NULL AND CancelDate IS NULL
```
(`Program.cs:181`)

For each returned row, it does `byte[] msg1 = ToByteArray(Message); stream.Write(msg1, ...)` (`Program.cs:197-198`). So:

- **`Message` is already the hex-encoded wire format** — stored as a `varchar(1500)` string of hex characters in `RG_B3`.
- `ToByteArray` converts pairs of hex chars to bytes (same as `StrToByteArray` but takes different parameter names).
- The UI is responsible for populating `Message`. From `Form1.cs:1437`:
  ```csharp
  MessageStr = "FFFF" + ReturnAsciText(dt5.Rows[0][0].ToString()           // PreMessage
                                       + calcChecksum(dt5.Rows[0][0].ToString()));
  UPDATE RG_B3 set Message='<MessageStr>' WHERE ...;
  ```
- Where does `PreMessage` come from? **UNKNOWN** — the INSERT at `Form1.cs:1420-1430` does not list `PreMessage` in its column list, yet `PreMessage` is SELECTed back immediately afterwards at line 1436. **Hypotheses:** (a) `PreMessage` has a DEFAULT constraint (not shown in the columns dump), (b) there is a DB trigger on INSERT that computes `PreMessage` from the lift/place/container columns, (c) it is `NULL` and the subsequent `calcChecksum(null.ToString())` returns `"0"`. All three hypotheses are testable; Q-30.

### 3.4 Outbound frame summary

| Type | First 2 bytes | Body | Example (schematic) |
|---|---|---|---|
| ACK (A2/03, A2/04) | `0xFF 0xFF` | ASCII `"04B2"` + counter + checksum | `FF FF '0' '4' 'B' '2' '1' '7' 'F' 'E' 'A'` |
| NAK (unknown type) | `0xFF 0xFF` | ASCII `"03B10FEFA"` | `FF FF '0' '3' 'B' '1' '0' 'F' 'E' 'F' 'A'` |
| Broadcast / job message | `0xFF 0xFF` | ASCII content of `RG_B3.Message` + its checksum (already baked in) | variable, up to ~1500 chars |
| Cancel message | `0xFF 0xFF` | ASCII content of `RG_B3.MessageCancel` + its checksum | variable, up to ~1500 chars |

---

## 4. Per-crane differences in protocol

| Property | Crane 1 | Crane 2 | Crane 3 |
|---|---|---|---|
| TCP port | 30701 | 30702 | 30703 |
| `CHE` identity (in DB writes) | `'GOLD1'` | `'GOLD2'` | `'GOLD3'` |
| Listener source file | `TOSConsole1/Program.cs` | `TOSConsole2/Program.cs` | `TOSConsole3/Program.cs` |
| Protocol frame format | Same | Same | Same |
| A1/A2/A3 handling | Same | Same | Same |
| Checksum algorithm | Same | Same | Same |
| `TB_Parameters.ContainerPickN` flag used | `ContainerPick1` | `ContainerPick2` | `ContainerPick3` (inferred) |

**Conclusion:** per-crane differences in the protocol layer are **purely configuration** (port + CHE literal). A Phase 2 redesign can absolutely use a single listener process configured by crane-id. The per-crane csproj clone is pure technical debt, not a real protocol divergence.

---

## 5. Re-connection, heartbeat, timeout behaviour

### 5.1 What the listener does

**Nothing about heartbeat or timeout is explicit in source.** The observed behaviour is:

- `server.AcceptTcpClient()` blocks until a connection arrives (`Program.cs:155`).
- Once connected, `stream.Read(bytes, 0, bytes.Length)` blocks until data arrives or the socket closes (`Program.cs:206`).
- If the socket dies silently (crane reboots, cable unplugged), the read blocks forever until TCP layer decides the connection is dead. Windows default `KeepAliveTime` ≈ 2 hours. So under default OS TCP settings, a silently-dropped crane can leave the listener hung for up to 2 hours before it notices.
- No `SO_KEEPALIVE` is set in code. No `ReceiveTimeout`/`SendTimeout` is set.

### 5.2 What the UI sees

- UI reads `RG_A1.Time` on every timer tick. If `Time` doesn't change for 90 consecutive ticks, the UI infers "connection dead" and enables the **Enconsole** button (`Form1.cs:1624-1635`).
- The operator has to press Enconsole to re-establish. The button's click handler was not read in detail — needs Q-18.

### 5.3 What the crane sees

Unknown. Whatever the vendor's PLC firmware implements. Probably a client-side retry loop with some back-off.

### 5.4 Operational reality (inferred)

1. Crane drops TCP.
2. Listener's `stream.Read` returns 0 or throws → `goto Outer` → next iteration throws again → falls to outer catch → `goto START` → `server.Stop(); new TcpListener; AcceptTcpClient()` blocks.
3. Crane's TCP client stack retries; when it successfully reconnects, the listener accepts and immediately broadcasts all un-picked `RG_B3` rows.
4. This can send **duplicate commands** to the crane for any job that was sent but not yet picked. The crane's firmware is the de-duplication authority. Since we don't have vendor docs, we don't know if it de-duplicates (Q-11).

---

## 6. Is this a known / standard protocol?

### 6.1 What it is NOT

- **Not NMEA 0183.** NMEA frames use `$GP…*hh` delimiters, comma-separated fields, and a two-hex-digit CRC8 after `*`. This protocol uses `??` header, fixed offsets, no delimiters, and a variable-length modular checksum.
- **Not DGPS / RTCM.** Those are binary. This is ASCII.
- **Not Modbus / OPC-UA / MQTT.** Those have their own framings.
- **Not a common crane-SCADA protocol** (I'd recognize NovaTech's DNP3, OMRON FINS, Rockwell EtherNet/IP). This is not that.

### 6.2 What it probably is

Empirically this looks like a **vendor-proprietary serial-over-TCP protocol**:

- ASCII-text framing with `??` magic → common in embedded controllers' ad-hoc protocols from the 1990s–2000s.
- Fixed byte offsets → typical of legacy spec that was once serial-rs-232 and got wrapped in TCP.
- `A1 / A2 / A3 / B1 / B2 / B3` message family naming → looks like a protocol-spec table of opcodes.
- Simple modular-sum checksum → embedded-systems typical (CRC-16 too expensive for small MCUs).
- The `04B2` in ACKs matches the pattern of "incoming message = `A2`, outgoing echo = `B2`" — i.e. the first digit is message family and the letter A/B denotes direction. A/B/C family naming is common in Japanese automation gear (e.g. Mitsubishi).

**Plausible origin vendors:** Kalmar, Konecranes, ZPMC, Mitsubishi Heavy Industries, Hans Kunz — any RTG vendor that had a proprietary TOS link module in the 2000s. Goldbond is a terminal operator; the cranes were likely bought as a package with a vendor PLC that speaks this protocol.

This needs to be confirmed with (a) the vendor datasheet, (b) a packet capture of live traffic, (c) the hardware nameplate. See Q-11.

---

## 7. Packet flow on the wire — example trace

A sample lifecycle for one pick-and-place job, showing bytes on wire. All ASCII unless noted.

```
--- Connect open ---
listener pulls pending jobs from RG_B3
< pushes "FFFF" + <Message of RG_B3 row> + <its checksum>         (UI had pre-built this as hex)

--- Crane starts moving ---
> receives   "??..A1 <6 chars Time>..."
listener: UPDATE RG_A1 SET ... WHERE CHE='GOLD1'
(no reply)
> receives   "??..A1 <another tick>"
(repeats, many times per minute)

--- Crane picks the container ---
> receives   "??<2>A2<Counter>03..<position>.."
listener: mark PickDate, TB_Parameters.ContainerPick1, look up container
listener < sends  "FFFF"+"04B2"+<Counter>+<checksum>

--- Crane moves to destination ---
(stream of A1 packets)

--- Crane places ---
> receives   "??<2>A2<Counter>04..<position>.."
listener: mark PlaceDate, delete RG_Container, update CO_Containers, insert RG_Shifting, update TB_Location
listener < sends  "FFFF"+"04B2"+<Counter>+<checksum>
```

For an A3-acknowledged cancel:

```
(UI writes MessageCancel to RG_B3)
listener < sends  "FFFF"+<MessageCancel hex>
> receives   "??..A3<Counter>.."
listener: first run sets RG_B3.A3Date; second run sets CancelDate
(no reply)
```

---

## 8. Security posture on the wire

| Issue | Observation |
|---|---|
| Encryption | None. TCP cleartext. |
| Authentication of client (crane) | None. Anyone who can reach port 30701/2/3 from a routable network is accepted. |
| Authentication of server (listener) | None. Crane cannot verify it is talking to the legit listener. |
| Integrity | `calcChecksum` on outbound only; listener does **not** verify inbound checksums. |
| Replay protection | Zero. Listener happily re-processes identical packets. |
| DoS resistance | Zero. Single-threaded, blocking socket, single-client model — one rogue client holding the connection open stalls the listener indefinitely. |
| Rate limiting | Zero. |
| Logging of suspicious events | Every exception is written to `\\Broadcast\Logs\RTG\…` but unusual-but-non-exceptional traffic is not logged. |
| SQL-injectability via the wire | **Yes.** Packet content is ASCII and goes directly into SQL strings via `Program.cs:225-236` / `:267,268,272,287` and others. An attacker who can speak the `??` protocol can quote-escape and inject arbitrary SQL against `TerminalData` under the listener's SSPI identity. |

This section is referenced by `07_edge_cases.md` (entries for SQL injection, replay, and DoS).

---

## 9. Implications for the Phase 2 listener redesign

*Design details go in `docs/design/03_listener.md`; this section just lists protocol-driven requirements.*

1. **Preserve the protocol exactly.** The cranes' firmware cannot be changed without vendor involvement. The new listener must speak the same `??`/`A1`/`A2`/`A3` + `FFFF`/`04B2`/`03B1` wire format — byte-compatible with the existing cranes. Any vendor docs obtained during Phase 1 finalization should be attached here.
2. **Add length-prefixed framing ** *in front of* the wire format — at application layer — before parsing. Buffer bytes until a full `??`-framed message is recognized (by minimum-length heuristic for each type), then parse. Do not assume `stream.Read` returns exactly one message.
3. **Validate inbound checksums** using the same modular-sum algorithm — and log violations. Today zero validation is done. Starting to validate is a behaviour change that may reveal silent data corruption.
4. **Parameterize the SQL** from packet fields — stop all string concatenation into SQL.
5. **One listener binary, crane-id as config.** Same code serves all 3 cranes; differences are `{port, CHE, BlocCode}` tuples in a YAML/JSON/DB-config.
6. **Heartbeat / keep-alive.** Set `SO_KEEPALIVE` with a short probe interval; fail-fast on dead sockets (seconds, not 2 hours).
7. **Proper state-machine** for each job (PENDING → SENT → PICKED → PLACED → DONE / CANCELLED) instead of nullable-date columns.
8. **De-duplication of re-broadcast jobs** after reconnect — either by expecting the crane to tolerate duplicates (likely today's behaviour) or by maintaining a "last-sent-to-crane" watermark on reconnect.
9. **TLS is probably not achievable** because the crane PLC firmware doesn't speak TLS — but the listener-to-DB link and listener-to-cab links definitely will (Phase 2 design).
10. **Replace the `Crc16Ccitt` class name with an accurate one** and keep the legacy algorithm in a `LegacyModularChecksum` helper. Do not migrate to real CRC-16/CCITT since the cranes expect the legacy algorithm.

---

## 10. Open questions for protocol (compiled into `09_open_questions.md`)

- **Q-11:** Who is the crane vendor / PLC model? Is there a vendor protocol spec document? What does byte 2-3 (between `??` and message type) mean? Is there a checksum field on inbound? (Requires operator knowledge or vendor contact.)
- **Q-20:** What are the valid values of `Status`, `CraneStatus`, `GPSStatus`? Is there an enum?
- **Q-27:** Is bytes 2-3 of the A1/A2/A3 frame a length, a counter, or something else?
- **Q-28:** What encoding is `HBHeight` (4-char field)? The UI treats it as a numeric; the A1 position-update stores it verbatim.
- **Q-29:** What does `03B10FEFA` mean as a reply? Is this from the vendor's NAK format or a Goldbond-specific response?
- **Q-30:** How is `RG_B3.PreMessage` populated on INSERT? The UI's INSERT doesn't name the column; is there a trigger or default constraint?
- **Q-31:** Does the crane firmware de-duplicate jobs that are re-broadcast on reconnect, or will it execute a job twice?

---

## 11. Check-in summary

- **Protocol fully reverse-engineered from code**: TCP ASCII, `??` magic header, fixed byte offsets for 3 message types (A1 position, A2 pick/place, A3 cancel), outbound `FFFF` + `04B2` / `03B1` replies. Simple additive-modular checksum on outbound only; no inbound validation.
- **Big protocol-level risks for migration:** no framing (message-boundary bugs), no inbound checksum validation (silent data corruption), SQL injection through the wire, default Windows `KeepAliveTime=2h` means dead cranes look alive for hours, job duplication on reconnect. All of these are preserved-bug candidates in `07_edge_cases.md`.
- **Vendor identification is unresolved.** The protocol shape matches a Japanese- or Korean-vendor-proprietary serial-over-TCP format (A1/B2/etc. opcode naming), but cannot be confirmed without the vendor datasheet or a live packet capture. **This is a blocker for Phase 2 to begin coding the listener** — Q-11 must be answered before design.
- **Next:** Step 1.6 — Operator UI analysis. Map every form, every button, Hebrew strings, auth model, offline behaviour. Deep read of FrmMap01 (116 KB) required.
