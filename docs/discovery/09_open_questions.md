# 09 — Open Questions Register

> **Phase 1 — Step 1.9** · Analyst: Claude Code · Date: 2026-04-21
> **Last updated:** 2026-04-21 — operator answered the 6 blockers (see §0 below)
> **Purpose:** a living, numbered list of everything I could not determine from code alone. Each item states what I need to know, why it matters for the migration, and how to find out. **These are the blocking inputs for Phase 2.**
>
> Questions are grouped by category and by blocking severity. Severity:
> - **🔴 Blocker** — Phase 2 cannot begin meaningful design work without an answer.
> - **🟠 High** — Answer shapes a major architectural decision.
> - **🟡 Medium** — Answer refines a design; work can start without it.
> - **🟢 Low** — Nice-to-have; can be resolved during implementation.

Total questions as of this revision: **46**.

---

## 0. Operator answers to the 6 blockers (2026-04-21)

| # | Question | Operator answer | Status |
|---|---|---|---|
| Q-11 | Crane vendor / PLC spec | **KoneCranes** | ✅ **FULLY RESOLVED 2026-04-21.** Operator delivered the vendor documentation (`KoneCranes/RTG/Network/RTG-TD-Konecranes TOS interface_V40.pdf` + v33 + simulator + network layout). Cranes are **KoneCranes BoxHunter G2036/G2037**, protocol **YARDIT PDS v4.0**. All 3 previously-unknown protocol questions (bytes 2-3, status codes, message ID table) are now documented. See [`10_konecranes_reference.md`](10_konecranes_reference.md) for full summary. |
| Q-39 | Is the TFS source current? | "The assumption is yes" | ⚠ **Conflicting evidence.** I have concrete drift evidence (§7 of `08_binaries.md` — `version 2024-10-29` in binary vs `2023-09-06` in source; TOSService.exe has a working `static void Main` that the TFS source doesn't; two log-paths in TOSConsole2 binary etc.). The *assumption* is not consistent with the *observed*. Phase 2 approach: proceed trusting the TFS snapshot as the 95% correct source; plan a **binary-vs-source diff via ILSpy** as a Phase-2 pre-cutover verification step to catch the last 5%. |
| Q-02 | `malgezot / 12345678` still valid? | "Not that I know of" | ⚠ Unconfirmed. Treat as potentially-live exposed credential. Phase 2 security work-item: explicitly test the login on SQL01 and disable it if valid. |
| Q-38 | Is `z3334606*` still valid? | "Not that I know of" | ⚠ Same as Q-02. Rotate regardless. |
| Q-12 | Which restart path is live (E: or C:)? | "Not sure" | 🟠 Unresolved. **Not blocking Phase 2 DESIGN**, but blocking Phase 2 DEPLOYMENT. Will add as a discovery task inside Phase 2's rollout plan — first pre-cutover audit of the two listener/DB-server hosts. |
| Q-04 | Is `TOSService.exe` running? | **YES** | ✅ Confirmed. **New architectural finding — see below.** |

### 0.1 Implication of Q-04 = YES

Combined with the 2026-04-21 operator update ("each crane has its own network, 192.6.1.xxx for crane 1"), the confirmed topology is:

- **Each crane runs on a private network / VLAN:** Crane 1 = `192.6.1.x`, Crane 2 = `192.6.2.x`, Crane 3 = `192.6.3.x` *(all confirmed by operator 2026-04-21)*.
- **The listener host is multi-homed** — it has an interface on every crane subnet. Its Crane-1-side IP is `192.6.1.8` (which is the literal in `TOSConsole1/Program.cs:119`, although the code accidentally binds to all interfaces).
- **The DB server** (`SQL01`) has IP `192.6.8.52` on a separate subnet `192.6.8.x`.
- **TOSService.exe runs on the DB server** (or on another host with IP 192.6.8.52). Its `IPAddress.Parse("192.6.8.52")` hard-coded bind address fixes it to that one subnet. Any crane that can route to `192.6.8.x` can reach it.
- **TOSConsole{1,2,3}.exe run on the listener host.** They bind to any interface and listen on ports 30701/2/3. Each crane reaches its dedicated TOSConsole via the subnet they share with the listener host.

So the system has **two distinct listener hosts**:

| Host | Bound IPs | Binaries | Restart path |
|---|---|---|---|
| **Listener host** | Multi-homed: `192.6.1.8` (Crane 1), `192.6.2.x` (Crane 2), `192.6.3.x` (Crane 3) | `TOSConsole1/2/3.exe` + `ConsolesReRun.exe` | `E:\RTG\Console{n}\` |
| **DB server (SQL01)** | `192.6.8.52` only | `TOSService.exe` + `SQL Server` + `xp_cmdshell` SPs | `C:\RTG\RTG{n}\` |

This cleanly resolves Q-12: both restart paths are real, on their respective hosts. Not a mistake.

**Architectural consequence for Phase 2:** the target-architecture deployment plan must decide whether to:
- (a) Continue the dual-host model — one "crane-facing" host per subnet + the DB host. Natural fit if cranes' PLCs only know one target IP each.
- (b) Consolidate — pick either the listener host or the DB server as the single TCP terminator, require cranes to re-route / re-address if needed. Requires KoneCranes PLC config changes.

Which is right depends on: (i) crane PLC configuration flexibility, (ii) the reason TOSService was created in the first place — was it intended as a fallback for Crane 2 specifically, or did the architect just want crane traffic visible on the DB server too?

This becomes high-priority new questions:

### Q-47 🟡 Partially resolved (new) — Which IP does each crane's PLC target?
From the KoneCranes Gold-Bond connectivity PDF (`KoneCranes/RTG/Network/BxH-RTG-Gold-Bond-Ashdod-Connectivity-Layout-05092017.pdf`): cranes are BoxHunter **G2036** (Crane 1) and **G2037** (Crane 2), each has an mGuard 4004 NAT firewall, and the customer network is 192.6.0.0/16 with Terminal Router at `192.6.8.254/16`. The GPS-PC (CCS) inside each crane is at `192.6.{1,2}.8`. The cranes therefore connect **outbound** through their mGuard to the TOS host on the customer network. **Exact TOS target IP still needs confirmation** — likely `192.6.8.52` (DB server, where TOSService.exe runs) or another 192.6.8.x edge host. See `10_konecranes_reference.md` §3.2.
The PLC firmware on each crane has one (or more — if retry targets exist) hard-coded destination IP+port. For each crane:
- What IP does Crane 1 target? (Expected: `192.6.1.8:30701` on the listener host.)
- What IP does Crane 2 target? (Options: listener host's `192.6.2.x:30702`, OR DB server's `192.6.8.52:30702` via TOSService, OR both with failover?)
- What IP does Crane 3 target?
*How to find out:* KoneCranes PLC configuration panel on each crane, OR `netstat -an | findstr 3070` + `Get-NetTCPConnection -RemoteAddress` on both listener hosts to see who is actually connected.

### Q-48 ✅ RESOLVED — Subnet assignments
Operator confirmed (2026-04-21): Crane 1 = `192.6.1.x`, Crane 2 = `192.6.2.x`, Crane 3 = `192.6.3.x`, DB server subnet = `192.6.8.x`. Closed.

### Q-49 🟡 Medium (new) — Why does TOSService exist at all?
If the dual-host hypothesis is correct and TOSConsole2 already handles Crane 2 on the listener host, what role does TOSService play? Candidates:
- Primary listener (TOSConsole2 is dormant) — unlikely given Oct 2024 rebuild of TOSConsole2.
- Fallback that Crane 2 falls back to if the listener host is unreachable.
- A debugging / packet-tap listener for the developers (PDB path shows "WorkspaceMichael"; Michael may have stood it up for his own visibility).
- Historical experiment that was never retired.
*How to find out:* operator knowledge, ideally from the original developer Michael.

### 0.2 Updated blocker count

With operator answers:

- **Resolved (usable for Phase 2 design):** Q-11 (vendor = KoneCranes), Q-04 (TOSService is live)
- **Resolved as "proceed with caveat":** Q-39 (assume source current, add decompile verification), Q-02+Q-38 (rotate credentials, don't depend on their status)
- **Deferred to Phase 2 deploy/rollout plan:** Q-12

**Net:** Phase 2 DESIGN can begin. Phase 2 CODING of the listener must wait on KoneCranes protocol spec (Q-11 follow-up), OR on a live packet capture if the spec isn't obtainable.

---

## Section A — Operational reality / deployment topology (🔴 Blockers first)

### Q-01 🟡 Medium — Project root layout
The migration prompt described a root layout with `current/`, `From TFS/`, `RTG/`, `RTG1-2/`, `RTG3/`, `rtg-discovery/` all at the project root. The actual structure has all of these under a single `Current/` folder and the prompt files at the outer level. Is this intentional (to be matched by the target PG tree), or should I normalise paths? **Proposed:** ignore for analysis, will be normalised in Phase 2.
*How to find out:* Confirm with you in the first review.

### Q-04 🔴 Blocker — Is `TOSService.exe` running in production?
`From TFS/RTG/TOSService/` has a broken `Main()` in source, but a working deployed binary dated 2022-06-09. Only the `Gold2` path is live. Three possibilities: (a) it is not running; (b) it is running as a redundant listener for Crane 2; (c) it is the primary listener for Crane 2 and TOSConsole2.exe is dormant. Each possibility implies a different Phase-2 retirement plan.
*How to find out:* RDP or Task Manager on the listener host(s). Report whether `TOSService.exe` is a live process. Also check Task Scheduler / Services for scheduled launches.

### Q-12 🔴 Blocker — Two restart mechanisms, two different paths
`ConsolesReRun.exe` restarts listeners from `E:\RTG\Console{n}\TOSConsole{n}.exe`. Stored procedure `RunEnconsoleRTG{n}` restarts them from `C:\RTG\RTG{n}\TOSConsole{n}.exe` (via `xp_cmdshell` on the DB server). These are different drive letters and different folder names.
Either the DB server and the listener host are the same machine with a drive alias, OR one path is stale, OR two different hosts each have the binaries in the path their local disk uses.
*Why it matters:* impacts where Phase 2 deploys the new listener and which supervision mechanism survives.
*How to find out:* on the DB server (`SQL01` / `192.6.8.52`), check whether `C:\RTG\RTG{1,2,3}\TOSConsole{n}.exe` exist. On the listener host (inferred to be `192.6.1.8`), check whether `E:\RTG\Console{1,2,3}\TOSConsole{n}.exe` exist. Compare timestamps.

### Q-33 🟠 High — Where is the "Enconsole" button's SQL invocation?
The FrmMap01 enables `EnconsoleButtom` after 45 seconds of stale `RG_A1.Time`. The click handler was not read in detail during Phase 1. Does it call `RunEnconsoleRTG{n}` via `xp_cmdshell`? Or does it simply start a local process? Answer affects whether Phase 2 UI can trigger listener restart from the cabin.
*How to find out:* deep-read Form1.cs around `EnconsoleButtom_Click`, OR decompile the RTG1-2/RTGApp.exe binary.

### Q-34 🟠 High — What DB server specs?
TimerCHE fires every 500 ms from up to 3 cabin UIs ≈ 36 queries/second just for the map polling, on a 2.59 M-row `CO_Containers` table. Phase 2 target must sustain this load. Is the current `SQL01` a bottleneck? CPU / RAM / disk IOPS?
*How to find out:* ask ops for server specs (cores, RAM, storage), and for SQL Server Activity Monitor / DMV metrics (`sys.dm_exec_query_stats`).

### Q-40 🟡 Medium — Why are production binaries Debug builds?
Every `.NET Framework` binary in `RTG/` is compiled in Debug configuration with `DefineConstants=DEBUG;TRACE`. Is this intentional (for log verbosity?), accidental (informal build process), or historical?
*Why it matters:* Phase 2 CI/CD must produce Release builds — but if Debug is required for some diagnostic reason, we need to preserve that.
*How to find out:* ask the original developers / check for any `#if DEBUG` blocks that matter.

---

## Section B — Vendor & protocol questions (🔴)

### Q-11 🔴 Blocker — Crane vendor / PLC model / protocol spec document
The wire protocol (A1/A2/A3 + ?? header + FFFF replies) is not a recognised industry standard. It is vendor-proprietary. Without the vendor spec, we cannot confidently implement the new listener because:
- We don't know the purpose of bytes 2–3 of the inbound frame
- We don't know whether inbound frames carry a checksum
- We don't know whether the crane de-duplicates re-sent jobs
- We don't know the full set of Status codes (`CraneStatus`, `GPSStatus`)
*How to find out:* Who is the RTG vendor (Kalmar? Konecranes? ZPMC? MHI? Hans Kunz?)? Is there a protocol datasheet? Who is the vendor's technical contact at Goldbond? If no docs exist, we need a live packet capture from at least one crane.

### Q-20 ✅ RESOLVED — Status codes (from KoneCranes V40 spec)
- `Status` (auto-steering): `"0000"` = OK; any other 4-digit value = error code.
- `CraneStatus`: `"01"` off · `"02"` on/idle · `"03"` running · `"04"` error/fatal.
- `GPSStatus`: `"01"` OK · `"02"` GPS internal fault · `"03"` no satellites · `"04"` no comm to base-station.
- `PLC` comm: `"01"` OK · `"02"` failure.
- `TwistLock`: `"O"` open / `"C"` close.
- `HBHeight`: logical value 1..7.
See `10_konecranes_reference.md` §2.3 for the full field table.

### Q-27 ✅ RESOLVED — Bytes 2-3 are length + counter
Frame envelope is `FFXXNNaaaaaaaaaa...aaZZ` where `XX` is length byte and `NN` is frame counter. Legacy ignores them (works by luck because it doesn't need framing at its throughput). New `rtg-listener` should parse `XX` for proper length-prefixed framing. See `10_konecranes_reference.md` §2.1.

### Q-28 ✅ RESOLVED — `HBHeight` is a logical stacking level 1..7
4-char ASCII encoding a logical height (how many containers deep in a stack, 1..7). In the `Row` field `G`/`T` are orthogonal — they mean "ground" / "truck" (not stacked). The legacy UI's `A→0, B→1, ..., G→6, T→7` mapping is a UI-side compression for grid column rendering only; the wire value is `1..7`. See `10_konecranes_reference.md` §2.3.

### Q-29 ⚠ Partially resolved — `03B10FEFA` decodes as a B1 Twist-Lock-Block message, not a NAK
Decode using the KoneCranes spec:
- `03` = length (3 body bytes)
- `B1` = Msg-ID `0x4231` = **Twist-Lock-Block status** (TOS → CHE)
- `0F EFA` = B1 body (1-byte TWL_block value + padding) + suffix
Legacy is sending a twist-lock-block status every time an unknown frame arrives — this is definitely a bug, accidentally working because the crane probably ignores it. The KoneCranes spec defines **no explicit NAK** — the CHE just retransmits if no ACK. The new `rtg-listener` should NOT emit this byte sequence; instead, log the unknown frame and rely on the CHE retransmit mechanism. See `10_konecranes_reference.md` §2.5.

### Q-30 🟠 High — How is `RG_B3.PreMessage` populated?
The UI INSERT into `RG_B3` does NOT list `PreMessage` in its columns, but reads it back one SELECT later. Hypotheses: (a) DB trigger on INSERT of RG_B3 computes PreMessage from other columns, (b) DEFAULT constraint, (c) NULL and `calcChecksum(null)` is tolerated.
*Why it matters:* Phase 2 must recreate this behaviour. If it's a trigger we haven't seen, it must be found.
*How to find out:* `SELECT * FROM sys.triggers WHERE parent_id = OBJECT_ID('RG_B3')` on the live DB. Alternatively, `SELECT DEFINITION FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID('RG_B3')`.

### Q-31 ✅ RESOLVED — CHE does NOT autonomously deduplicate; it retransmits unacked messages
From the chesimu manual (`KoneCranes/RTG/Network/che simulator manual.pdf` §2.1):
> "If Ack Required check box is checked then job message is confirmed with acknowledge message. Also pick/place message expects acknowledgement. Max Resents spin box used to set up how many times pick/place message is resent if not acknowledged."

So CHE expects **B2 ACKs** from TOS for A1/A2, retransmits up to N times if no ACK. Symmetric: TOS expects **A3 ACKs** for B3/B4. The listener must deduplicate A2 events by `(crane_id, counter)` — a retransmitted A2 with the same counter is the *same event*, not a duplicate pick/place. Implementation: `rtg.outbox.idempotency_key = crane_id || ':' || counter || ':' || move_type` naturally deduplicates. See `10_konecranes_reference.md` §2.8.

### Q-41 🟠 High — What replaces `V_MapRTGBond3`?
The RTG3 UI binary queries `SELECT * FROM dbo.V_MapRTGBond3` but this view no longer exists. What happens at runtime? Silent exception? Empty result? Is there a try/catch in FrmMap01's RTG3 variant?
*How to find out:* either a live test on the RTG3 cabin PC, OR ILSpy decompile of RTG3/RTGApp.exe.

---

## Section C — Data semantics & identity (🟠 mostly)

### Q-02 🔴 Blocker — Is `malgezot / 12345678` still a valid SQL login?
The RTG1-2 UI binary's compile-time fallback connection string uses `User ID=malgezot;Password=12345678`. At runtime, `exe.config` overrides this with Integrated Security. But we need to know:
- Does the `malgezot` login still exist on SQL01?
- What permissions does it have?
- Has the password `12345678` been changed since 2023?
- How many other apps still use this account?
*Why it matters:* If still valid, **this is an exposed secret** (visible in the binary to anyone with the `.exe` file). Phase 2 migration plan must rotate or revoke.
*How to find out:* `SELECT * FROM sys.sql_logins WHERE name = 'malgezot'` on SQL01.

### Q-03 🟡 Medium — `ForkliftApp_TemporaryKey.pfx`
The PFX file exists in the ForkliftApp sources (`From TFS/RTG/New Folder/ForkliftApp/ForkliftApp/`). The filename suggests VS-generated dev signing key. Is it actually a production certificate, or the default dev-signing key from the Visual Studio "Sign the ClickOnce manifests" wizard?
*How to find out:* open with Windows cert manager; check "Issued By" and "Valid From/To".

### Q-06 🟠 High — Does RTG3 UI use a distinct DB login at runtime?
RTG3 binary defaults to Integrated Security SSPI. But the cabin PC is a Windows box — what Windows user does it run as? The cabin PC's machine account, a domain service account, or a local user?
*How to find out:* RDP to the RTG3 cabin PC, check the auto-logon user and service accounts.

### Q-07 🟠 High — Why did Crane 3 branch?
Confirmed the fork is real (`02_components.md` §5). The code hypothesis is "Crane 3 procured later, developer branched instead of parameterising". Is that the actual story? Or is Crane 3's PLC different, requiring different SQL?
*Why it matters:* if PLC differs, Phase 2 listener still needs per-crane logic. If only procurement timing differs, Phase 2 can unify.

### Q-08 🟡 Medium — Are Console1, Console2 protocol-identical with Console3 despite 2024 rebuild?
Consoles 1 & 2 were rebuilt October 2024; Console 3 has not been touched since September 2023. Did the 2024 rebuild introduce any wire-protocol-visible behaviour change that would be incompatible with the Crane 3 PLC expectations?
*How to find out:* diff the binary string pools. Already done (§3 of `08_binaries.md`): only log-version literal and a second log-path are different — no protocol changes. **Tentatively resolved: no protocol divergence.** Can confirm with ILSpy.

### Q-09 🟠 High — `CHE` identity namespace: `GOLD` vs `BOND`
Code writes to `RG_A1.CHE` with `GOLD1/2/3` (from Listener), but to `RG_Log.CHE` with `BOND1/2` (from RTG1-2 UI via `CHEName.txt`) or `GOLD3` (from RTG3 UI). Plus `TB_Location.BlocCode` uses `BOND1/2/3`. Is this by design?
*How to find out:* inspect live data — `SELECT DISTINCT CHE FROM RG_Log; SELECT DISTINCT CHE FROM RG_A1; SELECT DISTINCT BlocCode FROM TB_Location; SELECT DISTINCT CHE FROM TB_Location`. See what values are actually present.

### Q-10 🟠 High — Should the Flutter UI keep old screens?
`obj/x86/Debug/` in the TFS source tree contains pre-compiled resource sets for `FrmMap01`, `FrmContainersByBloc`, `FrmLogin`, `FrmRecommendedLocation`, `FrmContainerNoLocation`, `ContainerLocation`, `frmInformation`, `FrmWorks`, `FrmExpectedContainers`, `FrmEmptyContainers`, `FrmMenu`, `FrmInOutDiory`, AND historical artefacts for removed `FrmMap01` (old variant), `frmInformation` (old variant), and `FrmWorks` (formerly with different structure). Are any of the removed screens still needed?
*How to find out:* ask you — which screens does the operator use daily? Which are never touched?

### Q-13 🟢 Low — Resolved during deep-read of Form1.cs
*(I inferred the UI does INSERT into RG_Container; confirmed at `Form1.cs:1457-1459`.)* Closed.

### Q-14 🟡 Medium — `RG_ColDG` semantics
Inferred during Step 1.6 analysis: RG_ColDG is a bay-column-to-UI-grid-index map used by FrmMap01's "color the empty cells green" overlay. **Tentatively resolved.** To fully close, confirm the meaning of `ColS1`, `ColS2`, `IndexCol`.

### Q-15 🟡 Medium — TimerCHE tick interval & edge
Resolved during Step 1.6: 500 ms, from Designer file. Closed.

### Q-15-B 🟠 High — How much of the 1.6 M `RG_ErrorLog` is from framing failures vs other causes?
*Why it matters:* prioritising edge-case fixes in Phase 2.
*How to find out:* sample from `RG_ErrorLog`: `SELECT TOP 200 Msg FROM RG_ErrorLog ORDER BY CTime DESC` and categorise.

### Q-18 🟢 Low — Will be resolved when Q-33 is answered.

### Q-19 🟠 High — Size of `RG_A1_LOG`
The trigger `rg_up_triger` inserts one row per RG_A1 UPDATE. Potentially ≥ 1 million rows/year per crane. Not in `01_target_objects.txt`.
*How to find out:* `SELECT COUNT(*) FROM RG_A1_LOG; SELECT MIN(when_inserted), MAX(when_inserted) FROM RG_A1_LOG; sp_spaceused RG_A1_LOG`.

### Q-21 🟡 Medium — RG_A1 trigger recursion
`rg_a1_updatetime` AFTER UPDATE does another UPDATE → does this fire `rg_up_triger` again, creating 2 rows in `RG_A1_LOG` per packet?
*How to find out:* `SELECT is_recursive_triggers_on FROM sys.databases WHERE name='TerminalData'` and test.

### Q-22 🟢 Low — What uses `RG_B3.FinishDate`?
Column defined but not observed in code. Hypothesis: set by UI on confirm-complete (`Form1.cs:2013` — `UPDATE RG_B3 set FinishDate=GetDate()`). **Tentatively resolved.**

### Q-23 🟡 Medium — Magic OperatorID `444444444`
`V_RG_CurrentOperator` excludes `OperatorID <> '444444444'`. Who is this? System user? Admin?
*How to find out:* `SELECT EmpName FROM HR_Emp WHERE EmpID='444444444'`.

### Q-24 🟢 Low — `TB_Location.LocationCodeNew` / `BlocCodeNew`
Defined but not visible in use. Probably a migration artefact. Closed — ignore in Phase 2 unless a trigger references it.

### Q-25 🟡 Medium — `TB_Parameters.RefreshMapRTG3N` and `ContainerPick3`
Defined in schema and referenced by the `TB_Location_Container_up` trigger (for BOND3+GOLD3). Does the RTG3 UI actually consume these, or does it skip the "refresh map" flow?
*How to find out:* ILSpy on the RTG3 RTGApp.exe binary, specifically FrmMap01.TimerCHE_Tick_1 handler.

### Q-26 🟢 Low — `V_MapRTGBond1Pre` / `V_MapRTGBond2Pre`
These views are JOINed by `V_MapRTGBond1` / `V_MapRTGBond2` but not in `01_target_objects.txt`. So they exist but weren't flagged as targets. Probably inputs to the "pivot" layer.
*How to find out:* `SELECT OBJECT_DEFINITION(OBJECT_ID('V_MapRTGBond1Pre'))` on live DB.

### Q-32 🟡 Medium — The "Empty" bit column behaviour
RTG3 UI has a bulk UPDATE `TB_Location SET empty=1 WHERE TB_Location.CHE = 'gold3' AND (HandlingTypeCode='EM' or EmptyContainer=1) AND exitdate IS NULL`. The RTG1-2 UI does not. Should Phase 2 provide this function to all cranes?

### Q-36 🟢 Low — `FrmRecommendedLocation.Seven_Click` writes to wrong field
Appends to `txtRecommendedLocation.Text` instead of `txtLocation.Text`. Copy-paste bug. Closed — fix in Phase 2.

### Q-37 🟠 High — UI binary contains typed-DataSet SQL; was it ever used?
The RTG1-2 UI binary has parameterised INSERT/UPDATE/DELETE for `RG_B3` generated by Visual Studio's Typed DataSet wizard. Source code uses `Con1.ReturnDT(raw_sql)` everywhere. Is the typed-DataSet path actually invoked anywhere at runtime?
*How to find out:* ILSpy decompile to check caller references.

### Q-35 🟢 Low — Group-code `22` = Crane Operator role
Hardcoded in multiple places. Fine for now; Phase 2 should parameterise.

### Q-38 🔴 Blocker — Are `malgezot / 12345678` and `z3334606*` compromised?
See Q-02 for `malgezot / 12345678` (confirmed exposed in binary).
`z3334606*` is committed in `From TFS/RTG/RTG3/RTGApp/RTGApp/Properties/Settings.settings:11` (even if the binary no longer uses it).
Treat both as compromised until rotated.
*How to find out:* rotate immediately regardless of usage.

### Q-39 🔴 Blocker — Is the TFS source current?
§7 of `08_binaries.md` documents 5 drift points between the TFS snapshot and the deployed binaries. Without a fresh source we risk redesigning from stale code.
*How to find out:* do a fresh TFS checkout of the current `main` / `dev` branch of `RTG`. Compare timestamps and sha256 of key files.

### Q-42 🟡 Medium — Who are the developers?
`ContermTFS` workspace and `WorkspaceMichael` workspace. Contact info / names / current roles. Useful for onboarding and Phase 2 interviews.

### Q-43 🟡 Medium — PIN-rotation enforcement
Schema has `HR_Emp.DateOfPinCodeUpdate` + `TB_Parameters.PinCodeUpdateDaysInterval`. Neither RTG UI enforces. Do other apps enforce? What is the org policy?
*How to find out:* check other apps' code (MIS, ForkliftApp) for the same columns.

### Q-44 🟡 Medium — Number of active operators / expected concurrent sessions
For load model of Phase 2. Schema says 1345 SC_Users, 876 HR_Emp, UserGroupCode=22 subset unknown.
*How to find out:* `SELECT COUNT(*) FROM SC_Users WHERE UserGroupCode=22`.

### Q-45 🟠 High — Multi-terminal scope
RTG views hard-code `Terminal='ILCXQ'`. Is Goldbond planning to expand to other terminals in the next N years? Phase 2 parameterisation effort depends.

### Q-46 🟡 Medium — ForkliftApp integration contract
What does `sp_ForkLift*` expect from RTG's writes to `CO_Containers` and `TB_Location`? Is there a documented data contract?
*How to find out:* interview ForkliftApp owner.

---

## 2. Fast-track: the blockers (🔴) that Phase 2 depends on

| # | Title | Why blocker |
|---|---|---|
| **Q-02** | `malgezot / 12345678` SQL login still valid? | Exposed credential; security response required |
| **Q-04** | Is TOSService.exe running today? | Determines listener topology for Phase 2 |
| **Q-11** | Crane vendor / protocol spec document | Cannot write byte-compatible listener without it |
| **Q-12** | Restart-path disagreement (E:\ vs C:\, Console1 vs RTG1) | Determines deployment layout |
| **Q-38** | Rotate `malgezot` and `z3334606*` | Security-response prerequisite |
| **Q-39** | Is the TFS source current? | We may be analysing stale code |

**Before I begin Phase 2, I need answers to at least these 6.**

---

## 3. Fast-track: the high-impact (🟠)

| # | Title | Decision it blocks |
|---|---|---|
| Q-30 | How is `RG_B3.PreMessage` populated? | Whether Phase 2 listener or UI composes the wire message |
| Q-31 | Does crane de-dupe re-broadcast jobs? | De-duplication requirement in new listener |
| Q-33 | What does "Enconsole" button do? | Cabin-triggered listener restart path |
| Q-34 | SQL Server specs & current load | Sizing Phase 2 |
| Q-41 | What replaces `V_MapRTGBond3`? | RTG3 UI testability |
| Q-06 | RTG3 UI's Windows service-account | Phase-2 auth transition plan |
| Q-07 | Why did Crane 3 fork? | Unification scope |
| Q-15-B | Categorize `RG_ErrorLog` top causes | Prioritise bug-fixes |
| Q-19 | Size of `RG_A1_LOG` | Retention / partitioning design |
| Q-37 | Is typed-DataSet SQL path invoked? | Attack-surface footprint |
| Q-45 | Multi-terminal scope | Parameterisation effort |

---

## 4. Closed / tentatively resolved during Phase 1

| # | Resolved by |
|---|---|
| Q-13 | `Form1.cs:1457-1459` shows RG_Container INSERT by UI on G/T source. |
| Q-14 | `MappingSquare()` in `Form1.cs:820-848` reveals `RG_ColDG` → UI cell highlighting. Partial. |
| Q-15 | `Form1.Designer.cs:321` shows `TimerCHE.Interval = 500`. |
| Q-22 | `Form1.cs:2013` shows `UPDATE RG_B3 set FinishDate=GetDate()`. |
| Q-24 | Heuristic "migration artefact". |
| Q-26 | Exists but not central. |
| Q-35 | Hardcoded "22" is the RTG operator group — confirmed. |
| Q-36 | Copy-paste bug in `FrmRecommendedLocation.Seven_Click`. |

---

## 5. Check-in summary

- **46 questions catalogued.** 6 🔴 blockers, 11 🟠 high-impact, ~25 🟡 medium, several 🟢 low; 8 already tentatively closed.
- The six blockers are a **small, answerable list** — all require you (operator access, vendor contact, IT / DBA queries, HR/dev rosters). Most can be resolved in a single 30-minute walkthrough.
- The top unknown is **vendor / protocol spec** (Q-11) — without it we cannot confidently code the new listener. If you can get any vendor documentation, even partial, that unblocks the single biggest Phase-2 work item.
- **Next:** Step 1.10 — write the consolidated discovery summary `docs/discovery/00_SUMMARY.md`, which will include: executive summary (1 page), "how the system works today" narrative (3-5 pages), current-state architecture diagram, top 10 risks, and a link to the top 10 open questions from this register.
