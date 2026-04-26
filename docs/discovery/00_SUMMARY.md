# 00 — Phase 1 Discovery Report · Consolidated Summary

> **Project:** Goldbond RTG crane-location system modernization
> **Author:** Claude Code (analyst)
> **Date:** 2026-04-21
> **Status:** ✅ Phase 1 (Deep Analysis & Discovery) COMPLETE. Awaiting operator review and approval to begin Phase 2.

---

## 1. Executive summary (1 page)

Goldbond operates three Rubber-Tired Gantry cranes (Crane 1, Crane 2, Crane 3) that stream real-time position data to a .NET Framework 4.8 listener running on Windows, which writes every packet to a shared Microsoft SQL Server 2019 database (`TerminalData` on server `SQL01` / `192.6.8.52`). Operators in each crane cabin run a Windows Forms UI (`RTGApp.exe`) that reads from the same DB every 500 ms, queues work to the crane via DB table `RG_B3`, and records operator activity.

Phase 1 discovery reveals a functioning but heavily legacy stack with four strategic concerns:

1. **The three cranes are not three instances of one system; they are two parallel codebases.** Cranes 1 and 2 share a single compiled Operator UI (`RTGApp1-2.csproj`). Crane 3 runs a separate fork (`RTGApp3.csproj`) that has drifted meaningfully (different identity logic, different auth model, per-crane stored procedures and DB views prefixed `*RTG3*`, `*Bond3*`). The single biggest unification opportunity in Phase 2.
2. **Listener and persistence are fused** in one executable per crane (`TOSConsole{1,2,3}.exe`), with SQL statements built by string-concatenation, no transactions, no framing on the wire, no inbound checksum validation, and hard-coded IP/credentials. These three binaries are 95% identical; they differ only in TCP port, `CHE` literal (`'GOLD{1,2,3}'`), and one flag column in `TB_Parameters`. Phase 2 consolidation to one listener process is both feasible and urgent.
3. **The database is the nervous system.** DB triggers and a single-row 48-column god-table `TB_Parameters` act as an event bus: when the listener writes to `TB_Location`, a trigger sets `RefreshMapRTG{1..4}` or `RefreshMapRTG3N` flags that the operator UIs poll 2x per second. In parallel, stored procedures invoke `xp_cmdshell` from the DB server to start/kill the listener executables. The control plane and the data plane are tangled.
4. **Material security findings in deployed artifacts.** The compiled RTG1-2 UI binary contains `User ID=malgezot;Password=12345678` as its fallback connection string (overridden at runtime by `exe.config`, but still live in the binary). The RTG3 Settings.settings committed in source still contains an earlier plaintext password `z3334606*` (no longer used by the binary but compromised by its presence). Authentication is 4-digit PIN only; the PIN check ignores the selected LoginName, so the audit trail is unreliable. The listener TCP path is trivially SQL-injectable from a spoofed crane. All three issues need Phase-2 attention; two need immediate credential rotation regardless of migration timeline.

Phase 1 produced 9 sub-documents (01–09) and this summary. It identified **46 open questions** of which **6 are hard blockers** that require operator input before Phase 2 design can meaningfully begin — most prominently **the vendor / crane-PLC protocol specification** (Q-11), without which the new listener cannot be written byte-compatible.

The full picture is sufficient to design Phase 2 once the blockers are answered. Migration strategy should be module-by-module with Crane 3 as pilot (smallest, already forked, already partly isolated), preserving dual-write to MSSQL throughout.

---

## 2. How the system works today (narrative, 3-5 pages)

### 2.1 The physical setup

Goldbond's container terminal at ILCXQ operates three RTG cranes moving containers between yard slots (blocks `BOND1`, `BOND2`, `BOND3`), trucks (`T`), and ground staging (`G`). Each crane has a PLC / transponder that speaks a vendor-proprietary ASCII-over-TCP protocol to a central listener. Cranes 1 and 2 work the northern blocks (`BOND1/2`), Crane 3 works the southern block (`BOND3`).

Each crane's cabin has a Windows PC (tablet or desktop) running the `RTGApp.exe` Operator UI. The operator logs in with a 4-digit PIN, sees the live yard map (container density per bay-row-height), and dispatches pickup/place jobs to the crane. The crane executes the move physically, reports back pick and place events over the TCP link, and the system records every move in the `RG_Shifting` history table.

Behind the scenes, the DB server (`SQL01` at `192.6.8.52`) holds all state. The listener host (inferred IP `192.6.1.8`, possibly co-located with the DB server) runs three console binaries, one per crane. The DB server itself also runs `xp_cmdshell` to launch / kill these listeners on demand via stored procedures `RunEnconsoleRTG{n}` and `KillToss{n}` — an old-school control plane that ties OS processes to T-SQL.

### 2.2 Component map

```
┌────────────────────┐          TCP 30701-30703         ┌──────────────────────────┐
│ Crane PLC          │◄────── ASCII, "??" framed, ─────►│ Listener host (192.6.1.8)│
│ (3 cranes)         │        fixed byte offsets        │                          │
└────────────────────┘                                  │  TOSConsole1.exe (GOLD1) │
                                                        │  TOSConsole2.exe (GOLD2) │
                                                        │  TOSConsole3.exe (GOLD3) │
                                                        │  ConsolesReRun.exe       │
┌────────────────────┐                                  │  (watchdog)              │
│ Cabin PC (x3)      │                                  │  TOSService.exe (stale?) │
│                    │                                  └────────────┬─────────────┘
│ RTGApp.exe         │                                               │
│ WinForms + Hebrew  │                                               │
│ RTL                │                                               │ SSPI
└──────┬─────────────┘                                               │
       │ SQL (SSPI + malgezot/12345678 fallback)                     │
       │                                                             │
       ▼                                                             ▼
┌──────────────────────────────────────────────────────────────────────────┐
│ SQL01 · MSSQL 2019 · TerminalData · 192.6.8.52                           │
│                                                                          │
│ RG_A1 (3)  RG_B3 (475)  RG_Log (31k)  RG_Shifting (503k)                 │
│ RG_Container (0)  RG_ColDG (84)  RG_ErrorLog (1.6M)  RG_A1_LOG (?)       │
│ TB_Location (35k)  TB_Parameters (1, 48 cols)  TB_RecommendedLocation    │
│ CO_Containers (2.6M, SHARED)  CO_ContainerProfile (1.7M, SHARED)         │
│ CP_Deal / CP_Order (SHARED)  HR_Emp / SC_Users (SHARED)                  │
│ TC_Client (SHARED)                                                       │
│                                                                          │
│ + triggers: TB_Location_Container_up → writes RefreshMapRTG{n} flags     │
│             rg_up_triger → inserts to RG_A1_LOG every UPDATE             │
│             update_location_in_container → double-updates CO_Containers  │
│                                                                          │
│ + xp_cmdshell SPs: RunEnconsoleRTG{n}, KillToss{n}                       │
│ + ~80 sp_ForkLift* (ForkliftApp integration surface — SHARED)            │
└──────────────────────────────────────────────────────────────────────────┘
```

### 2.3 The daily happy path

An operator arrives at Crane 1 and logs in. The login form reads `C:\RTG\CHEName.txt` — a single line `BOND1` — and pre-fills the CHE field. The operator selects their name (`LoginName`) from a combobox populated by `SELECT LoginName FROM HR_Emp JOIN SC_Users WHERE UserGroupCode = 22`. They type their 4-digit PIN on an on-screen numeric keypad. The UI runs `SELECT COUNT(*) ... WHERE UserPinCode = <PIN> AND UserGroupCode = 22`; if any operator in group 22 has that PIN, login succeeds (the LoginName is not part of the check — this is a known security flaw). An `INSERT INTO RG_Log (OperatorID, LoginDate, CHE, BlockName)` records the session. The main map form `FrmMap01` opens.

`FrmMap01` starts a WinForms timer that fires every 500 ms. Each tick: (a) `SELECT Time, Status, HBBlockName, ... FROM RG_A1 WHERE CHE='GOLD1'` updates the live status grid, (b) checks `TB_Parameters.ContainerPick1` — if TRUE, show a "you just picked" widget, (c) checks `TB_Parameters.RefreshMapRTG` (or the right flag for (crane, block) pair) — if TRUE, re-render the yard-map grid by querying `V_MapRTGBond1` and then set the flag back to FALSE.

Meanwhile, on the listener side, `TOSConsole1.exe` has a single TCP connection open to Crane 1's PLC on port 30701. The listener is in a `while(true)` loop with `goto` control flow. At the top of each iteration it does `SELECT Message FROM RG_B3 WHERE CHE='GOLD1' AND A3Date IS NULL AND PickDate IS NULL` — any pending job messages that operators enqueued. For each row, it converts the hex-encoded `Message` field back to bytes and writes those bytes to the TCP stream — the crane physically receives the job.

The crane acknowledges by sending a packet starting with `??` followed by a message-type code (`A1`, `A2`, or `A3`). `A1` is the routine position update — every 1-2 seconds. The listener parses the packet by **fixed byte offsets**: `Time` is at offset 12..18, `HBBlockName` at 22..30, `HBBayNumber` at 30..33, and so on. It builds a string like `"UPDATE RG_A1 SET Time='143022', Status='OK01', HBBlockName='BOND1   ', ... WHERE CHE='GOLD1'"` by **concatenation** (no parameters), and executes it. The `rg_up_triger` trigger then inserts this row into `RG_A1_LOG` (tens of thousands of rows per day).

When the operator picks a yard cell in the UI map and presses Submit, the UI runs a chain: `SELECT MAX(CounterID)` → build next counter → `INSERT RG_B3 (...)` with the lift and place coordinates → `SELECT PreMessage FROM RG_B3 WHERE CounterID=@new` (the `PreMessage` is populated by **an unknown mechanism** — trigger or default; see Q-30) → build `Message = "FFFF" + <hex>(PreMessage + checksum)` → `UPDATE RG_B3 SET Message=...` → `Thread.Sleep(2000)` (yes, really) → `SELECT COUNT(*)` to see if the listener already picked up the row. Then the next listener iteration sees the row, sends to the crane, and the crane starts moving.

When the crane picks physically, it sends `??..A2 <counter> 03 <coords>`. The listener extracts the counter, updates `RG_B3.PickDate = GetDate()`, looks up the container at that (LocationCode, BlocCode) in `TB_Location`, and ACKs with a `FFFF+04B2+counter+checksum` wire reply. If the pickup was from ground or truck (`G` / `T` row), it sets `TB_Parameters.ContainerPick1 = TRUE` — the UI timer will catch this on the next tick and pop a "you have a container" widget.

When the crane places, it sends `??..A2 <counter> 04 <coords>`. The listener marks `RG_B3.PlaceDate`, deletes from `RG_Container` if present, updates `CO_Containers.LocationCode` and `EntranceForkliftDate`, INSERTs into `RG_Shifting` (which then triggers `update_location_in_container` — another `UPDATE CO_Containers`), and UPDATEs `TB_Location` twice (clearing old slot, setting new slot). Each `TB_Location` UPDATE fires `TB_Location_Container_up` which sets the `RefreshMapRTG{n}` flag in `TB_Parameters`. The operator's next UI tick (within 500 ms) sees the flag, re-renders the map, clears the flag. Close loop.

On connection drop, the listener catches an exception, does `server.Stop()` and loops back to `AcceptTcpClient()`. The crane retries TCP and reconnects — at which point the listener re-sends all still-pending `RG_B3` rows. Whether the crane de-duplicates is **unknown** (Q-31) — this is one of the open protocol questions. If the listener process crashes entirely, either `ConsolesReRun.exe` (a .NET 6 watchdog that runs from Task Scheduler) or a direct operator action via `RunEnconsoleRTG{n}` stored procedure relaunches it. These two mechanisms target **different folder paths on disk** (`E:\RTG\Console{n}\` vs `C:\RTG\RTG{n}\`) — one of them is stale or they run on different hosts; this must be resolved (Q-12).

### 2.4 Per-crane differences and the Crane 3 fork

Cranes 1 and 2 share one UI binary built from `RTG1-2/RTGApp/RTGApp/RTGApp1-2.csproj`; they distinguish themselves by `C:\RTG\CHEName.txt` containing `BOND1` or `BOND2`. Crane 3 has its own UI binary from `RTG3/RTGApp/RTGApp/RTGApp3.csproj` with hard-coded `CHE='GOLD3'` and `LiftBlockName='Bond3'`. The RTG3 UI uses Integrated Security; the RTG1-2 UI also uses it, but has a dormant `malgezot/12345678` fallback in the binary. RTG3 has a bulk-empty-location update that RTG1-2 lacks. RTG3 calls a view `V_MapRTGBond3` that does not exist in the current DB (and the runtime behaviour is unknown — Q-41). RTG3 logs with a quote-escaping helper; RTG1-2 does not. RTG3's source is attributed to developer "Michael" via the PDB path; RTG1-2's source comes from a different workspace.

At the DB level, the split is also real: stored procedures `RunEnconsoleRTG1/2/3` and `KillToss1/2/3` exist as three identical copies; views `V_ContainerUnloadRG` (Bond 1/2) vs `V_ContainerUnloadRTG3` filter differently; `V_LocationCount` vs `V_LocationCountRTG3` and `V_RTGLoad` vs `V_RTG3Load` are near-duplicates with Bond scope swapped; `TB_WorkType.Gold3` is a per-work-type boolean just for Crane 3. Five `RefreshMapRTG*` flags exist in `TB_Parameters` — one per (crane, block) pair — because the map-render trigger needed to branch per pair.

This fork is the single most important input to Phase 2 design. It can be **unified** back into one code path with per-crane configuration if (Q-07) confirms that Crane 3's PLC and protocol are identical to Cranes 1 and 2. It **must remain split** if the PLCs differ meaningfully. Phase 2 should pick Crane 3 as pilot either way — it's the smallest, most isolated scope.

### 2.5 The data model in one paragraph

`RG_A1` is the 3-row "live crane status" table (one row per CHE). `RG_B3` is the 475-row job queue written by the UI and consumed by the listener — each row represents one pick-and-place order with lifecycle dates (`A3Date`, `PickDate`, `PlaceDate`, `FinishDate`, `CancelDate`) instead of an explicit state machine. `RG_Container` is a transient per-crane "I'm carrying X" scratchpad (0-3 rows). `RG_Log` and `RG_Shifting` are append-only history (31K and 503K rows). `RG_ErrorLog` is an application log in DB (1.6M rows — write-heavy). `RG_A1_LOG` is a position-history log written by trigger (size unknown, likely millions). `TB_Location` is the 35K-row yard map. `TB_Parameters` is a 48-column single-row god-table where the RTG-relevant 8 columns implement a pub/sub signal bus via triggers and polling. All *other* tables are shared with the broader ERP (MIS, ForkliftApp) — any RTG write to them crosses an integration boundary and must dual-write during the transition.

### 2.6 The wire protocol

Cranes speak a vendor-proprietary ASCII-over-TCP protocol. All messages start with `??`. Inbound types are `A1` (position), `A2 03` (pick), `A2 04` (place), `A3` (cancel ack). Every field is at a fixed byte offset — `Time` at 12, `HBBlockName` at 22, and so on. Outbound replies start with `0xFF 0xFF` and carry `04B2<counter><checksum>` for ACK or `03B10FEFA` for NAK. Checksum is a modular-additive two's-complement 16-bit sum — not CRC-16/CCITT despite the misleading class name `Crc16Ccitt`. There is **no framing layer**: the listener trusts that one `stream.Read(256)` returns exactly one message. In reality TCP may split or coalesce, which causes silent message loss. The listener also does not validate inbound checksums. A full specification is in `05_protocol.md`.

---

## 3. Current-state architecture diagram

```mermaid
flowchart TB
  classDef cfg fill:#fff4c2,stroke:#c5a300;
  classDef secret fill:#ffd1d1,stroke:#b30000;
  classDef stale fill:#d9d9d9,stroke:#666;

  subgraph Cranes["RTG Cranes (3)"]
    PLC1[Crane 1 PLC<br/>vendor-proprietary<br/>TCP ASCII]
    PLC2[Crane 2 PLC]
    PLC3[Crane 3 PLC]
  end

  subgraph Cabs["Cabin PCs"]
    UI12[RTGApp.exe RTG1-2<br/>.NET 4.8 WinForms<br/>Hebrew RTL<br/>CHE from C:\RTG\CHEName.txt]
    UI3[RTGApp.exe RTG3<br/>.NET 4.8 WinForms<br/>CHE='GOLD3' hardcoded<br/>queries V_MapRTGBond3 ❓]
  end

  subgraph LH["Listener host (192.6.1.8?)"]
    L1[TOSConsole1.exe<br/>port 30701, GOLD1<br/>v2024-10-29]
    L2[TOSConsole2.exe<br/>port 30702, GOLD2<br/>v2024-10-29]
    L3[TOSConsole3.exe<br/>port 30703, GOLD3<br/>v2023-09-06]
    WD[ConsolesReRun.exe<br/>.NET 6 watchdog<br/>from E:\RTG\Console{n}\]:::stale
    TS[TOSService.exe<br/>only Gold2 path<br/>broken in source ❓]:::stale
  end

  subgraph DB["TerminalData (SQL01 · 192.6.8.52)"]
    direction LR
    RG_A1[(RG_A1 · 3 rows<br/>live status)]
    RG_B3[(RG_B3 · 475<br/>job queue)]
    RG_Shifting[(RG_Shifting · 503k<br/>movement history)]
    RG_Log[(RG_Log · 31k<br/>login sessions)]
    RG_ErrorLog[(RG_ErrorLog · 1.6M ⚠)]
    RG_A1_LOG[(RG_A1_LOG · ?)]:::stale
    TB_Loc[(TB_Location · 35k)]
    TB_Par[(TB_Parameters · 1×48<br/>god-table)]
    CO[(CO_Containers · 2.6M<br/>SHARED with MIS, ForkliftApp)]
    TRIG{{triggers:<br/>TB_Location_Container_up<br/>→ sets RefreshMapRTGn flags<br/>rg_up_triger → RG_A1_LOG<br/>update_location_in_container}}
    XPS{{xp_cmdshell:<br/>RunEnconsoleRTGn<br/>KillTossn<br/>from C:\RTG\RTGn\}}
  end

  PLC1 <-->|"TCP ??-A1/A2/A3<br/>no framing<br/>no checksum val."| L1
  PLC2 <--> L2
  PLC3 <--> L3

  L1 & L2 & L3 -->|"SQL SSPI<br/>string-concat<br/>no txn"| RG_A1 & RG_B3 & RG_Shifting & TB_Loc & TB_Par & CO
  TS --> RG_A1 & RG_B3 & TB_Loc

  UI12 -->|"SSPI<br/>or malgezot/12345678"| RG_B3 & RG_Log & CO & TB_Loc & TB_Par:::secret
  UI3 -->|SSPI| RG_B3 & RG_Log & CO & TB_Loc & TB_Par

  UI12 -.-> RG_ErrorLog
  UI3 -.-> RG_ErrorLog

  RG_A1 -.-> TRIG -.->|1 insert/packet| RG_A1_LOG
  TB_Loc -.-> TRIG -.->|sets bit| TB_Par
  RG_Shifting -.-> TRIG -.->|double-updates| CO

  WD -->|"Process.Kill<br/>+ Start"| L1 & L2 & L3
  XPS -.->|"shell out"| L1 & L2 & L3
```

Legend: solid = synchronous data flow; dashed = trigger-driven side-effect or out-of-band control; ❓ = behaviour uncertain; ⚠ = concern.

---

## 4. Top 10 risks for the migration

| # | Risk | Evidence | Severity | Mitigation direction for Phase 2 |
|---|---|---|---|---|
| R1 | **Vendor protocol spec not available.** We inferred the wire format from the listener source; critical unknowns remain (bytes 2-3, inbound checksum, duplicate de-dup policy). | `05_protocol.md` §2, Q-11, Q-27, Q-31 | 🔴 Critical | Pre-block: obtain vendor doc or live capture before Phase 2 listener coding |
| R2 | **Compile-time secrets in deployed binaries** — `User ID=malgezot;Password=12345678` in RTG1-2 UI binary; `z3334606*` in RTG3 TFS source | `08_binaries.md` §4.1, Q-02, Q-38 | 🔴 Critical | Rotate credentials immediately (independently of migration timeline); never bake secrets at build time |
| R3 | **Source-to-binary drift** — TFS snapshot is behind deployed binaries in at least 5 concrete ways | `08_binaries.md` §7, Q-39 | 🔴 Critical | Get fresh TFS checkout OR decompile binaries before Phase 2 design rewrites behaviour |
| R4 | **Write amplification & trigger chains** — one packet triggers 10-15 DB writes; `RG_A1_LOG` grows unbounded; `RG_ErrorLog` has 1.6M rows. | `04_database.md` §4.3, Q-19, #30 in `07_edge_cases.md` | 🟠 High | Replace trigger chains with application-layer events; introduce retention + partitioning on log tables |
| R5 | **Crane 3 fork vs Crane 1+2 mainline** — real codebase divergence in both UI and DB schema | `02_components.md` §5, `04_database.md` §5, Q-07 | 🟠 High | Pilot Phase 2 on Crane 3 (smallest, isolated); unify in a parameterised code path |
| R6 | **Control plane entangled with DB** — `xp_cmdshell` SPs run on SQL01 to launch/kill `.exe` files; two restart paths disagree on disk paths | `04_database.md` §5.2, Q-04, Q-12 | 🟠 High | Move to a real process-supervisor (systemd, Windows Service recovery, Kubernetes liveness). Disable `xp_cmdshell` |
| R7 | **No TCP framing + no inbound checksum validation** — silent message loss and SQL injection from the wire | `05_protocol.md` §1.1, §8 | 🟠 High | Real framing layer in the new listener; parameterise every SQL call; validate inbound checksum and log violations |
| R8 | **No transactions in multi-statement flows** — PLACE handler runs 7 statements without atomicity | `03_data_flows.md` §7, `07_edge_cases.md` #16 | 🟠 High | Wrap PLACE, PICK, and cancel flows in PG transactions; use outbox pattern for dual-write |
| R9 | **Authentication flaws: PIN-group matching, no lockout, misleading audit trail** | `06_operator_ui.md` §4, `07_edge_cases.md` #13-14 | 🟠 High | Per-operator PIN match, account lockout, proper audit log that records who was actually authenticated |
| R10 | **DB-as-message-bus** — `TB_Parameters` bit columns drive the UI map refresh via polling every 500 ms × 3 cabs | `03_data_flows.md` §10, `04_database.md` §4.1 | 🟡 Medium | Replace with server push (PG LISTEN/NOTIFY + WebSocket) from day 1 of Phase 2 |

---

## 5. Top 10 open questions blocking the design phase

In descending priority:

1. **Q-11** Vendor / crane-PLC protocol specification.
2. **Q-39** Is the TFS source current? (If not, we must get fresh source or decompile.)
3. **Q-02** / **Q-38** Are `malgezot/12345678` and `z3334606*` still valid credentials? Rotate regardless.
4. **Q-12** Where do `ConsolesReRun.exe` and `RunEnconsoleRTG{n}` SP actually find the listener binaries? (E:\ vs C:\, Console{n} vs RTG{n}.)
5. **Q-04** Is `TOSService.exe` a live process today or a dormant legacy?
6. **Q-34** SQL Server specs (cores, RAM, IOPS) and current load — for Phase-2 sizing.
7. **Q-07** Why did Crane 3 fork — procurement timing or genuine hardware divergence?
8. **Q-30** How is `RG_B3.PreMessage` populated? (Trigger, default constraint, or code path we haven't seen?)
9. **Q-31** Does the crane firmware de-duplicate re-broadcast jobs on reconnect?
10. **Q-06** What Windows identity does the RTG3 cabin PC run as, and what SQL permissions does that identity have?

Full register of **46 questions** is in `09_open_questions.md`.

---

## 6. Document map

| Phase-1 doc | What it contains |
|---|---|
| `00_SUMMARY.md` | This document. |
| `01_inventory.md` | Repository inventory (file-type, age, entry points, dead code). |
| `02_components.md` | Listener/UI mapping per logical component, per-crane diffs, secrets found. |
| `03_data_flows.md` | 7 end-to-end scenarios with Mermaid sequence diagrams; component diagram. |
| `04_database.md` | 21 tables + 13 views + 80 procs + 2 functions mapped; write/read matrix; ER diagram. |
| `05_protocol.md` | Wire protocol byte-offset tables, checksum algorithm, security findings. |
| `06_operator_ui.md` | 13 forms inventoried; auth analysis; Hebrew RTL notes; 3 wireframe sketches. |
| `07_edge_cases.md` | 40 edge cases with file:line evidence and severity. |
| `08_binaries.md` | PDB paths, string-pool analysis, source-to-binary drift register. |
| `09_open_questions.md` | 46 questions, originally 6 blockers; reduced to 0 after 2026-04-21. |
| `10_konecranes_reference.md` | **Addendum 2026-04-21.** Consolidates the KoneCranes vendor documentation, simulators, and Goldbond network diagram. Resolves Q-11 and 6 other protocol questions. |

---

## 7. Operator answers to blockers (2026-04-21)

The 6 blocker questions were answered by the operator on the day of Phase 1 delivery. Full detail is in `09_open_questions.md` §0.

| # | Answer | Net effect on Phase 2 |
|---|---|---|
| Q-11 | **KoneCranes** (Finnish crane manufacturer) → later that day, the operator delivered the **full vendor documentation directory** (`KoneCranes/RTG/`) containing the V40 protocol spec, the che/tos simulators, and the Goldbond-specific network layout. | ✅ Fully resolved. Listener can be coded byte-perfect. See `10_konecranes_reference.md`. |
| Q-39 | "assumed current" | Will trust TFS as the baseline for design; add a binary-vs-source diff (ILSpy) pass as a Phase-2 pre-cutover verification. |
| Q-02 / Q-38 | "not that I know of" | Treat both credentials as potentially live; rotate regardless. |
| Q-12 | "not sure" | ✅ Resolved by the KoneCranes network diagram — the legacy `E:\RTG\Console{n}\` and `C:\RTG\RTG{n}\` paths correspond to the two distinct hosts (listener host + DB server / TOSService). |
| Q-04 | **Yes, TOSService is running** | Dual-host hypothesis confirmed. The listener host (customer network) runs TOSConsole1/2/3; the DB server runs TOSService (bound to 192.6.8.52). |
| — | **Network topology (2026-04-21):** each crane on its own private subnet behind an mGuard 4004 NAT — Crane 1 = `192.6.1.x`, Crane 2 = `192.6.2.x`, Crane 3 = `192.6.3.x`. Customer network `192.6.0.0/16`, DB server `192.6.8.52`, Terminal Router `192.6.8.254`. | Cranes egress via NAT to the customer network; the edge host needs **only one NIC on the customer network** (not multi-homed). Architecture doc §5.1 updated. |

**Net:** Phase 2 DESIGN can begin. Phase 2 CODING of the new listener waits on the KoneCranes protocol document or a packet capture.

---

## 8. Explicit STOP — awaiting Phase-2 approval

> **Phase 1 complete.** Blockers answered.
>
> Before I begin Phase 2 I need **your explicit "go"** — both because the prompt's Phase Gate rule requires it, and because the Q-04 answer changed the architectural picture (two-host listener topology) and I want to make sure you're aligned with the consequences before I start designing around it.
>
> If you want me to begin Phase 2 now, say so (e.g. "go" / "proceed" / "אשר להתחיל Phase 2"). If you want to discuss any of the 10 risks or adjust priorities first, let's do that.

---

*— End of Phase 1 Discovery Report —*
