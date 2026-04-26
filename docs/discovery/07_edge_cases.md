# 07 — Edge Cases & Failure Modes

> **Phase 1 — Step 1.7** · Analyst: Claude Code · Date: 2026-04-21
> **Scope:** every failure / edge / degraded-mode scenario that is either **observable in code** or **strongly implied** by the structure described in `03_data_flows.md`, `04_database.md`, `05_protocol.md`, `06_operator_ui.md`. The prompt asks for ≥25 rows; this register has **40**. Each row identifies the scenario, how the existing code handles it (good, bad, or silent), the source evidence, and the **risk if unhandled in the new system**.

**Legend for "Current handling":**
- ✅ handled (code takes a deliberate action)
- ⚠ partial (handled superficially; known-broken corner cases remain)
- 💀 unhandled (code either crashes, loses data, or silently produces wrong state)
- ⏻ opportunistic (the OS or SqlClient connection pool masks the problem in practice)

---

| # | Scenario | Current handling | Source (file:line) | Risk if unhandled in new system |
|---|---|---|---|---|
| 1 | **TCP stream split mid-message** — `stream.Read` returns only part of one `??`-framed message. | 💀 Listener calls `data.Substring(0, 2)` on the truncated buffer; if len ≥ 2 sees `??`, then `data.Substring(4, 2)` or later throws `ArgumentOutOfRangeException`; `goto Outer` discards the partial data. Remaining half is read next, fails `??` check, triggers NAK. Message is lost silently. | `TOSConsole1/Program.cs:206, 219, 222, 346-351` | High — position updates dropped without operator awareness. Crane state diverges from DB. Potential yard collisions. |
| 2 | **TCP stream coalescing** — two A1 frames arrive in one `stream.Read`. | 💀 Only the first is parsed; bytes beyond offset `77` are discarded. The second frame is silently dropped. | `TOSConsole1/Program.cs:206-236` (no loop to consume remaining bytes) | High — some position updates never reach the DB. |
| 3 | **Crane drops TCP silently** (cable unplugged, PLC crash). | ⏻ Listener blocks on `stream.Read()` until OS TCP keep-alive fails, default ≈ 2 hours. Meanwhile UI detects staleness via `CounterConnect > 90` ≈ 45 seconds. | No `SO_KEEPALIVE` is set; `Program.cs:155,206`. UI logic at `Form1.cs:1624-1635` | High — listener is "blind" for up to 2 hours. Jobs queued during this window don't flow. |
| 4 | **Malformed packet** — not starting with `??`. | ✅ Falls through to fallback NAK bytes `0xFF 0xFF 0x30 0x33 0x42 0x31 0x30 0x46 0x45 0x46 0x41`. | `TOSConsole1/Program.cs:338-339` | Low — already handled. Re-implement same behaviour. |
| 5 | **A1 packet shorter than 78 bytes** — sensor partial-transmit. | 💀 `data.Substring(77, 1)` throws; caught at `Program.cs:346-351`; `goto Outer`. `RG_A1` UPDATE not run. No log entry other than the exception trace. | `Program.cs:235-236, 346-351` | Low — RG_A1 retains previous row; UI's staleness detector will eventually flag it. |
| 6 | **A2/03 or A2/04 packet shorter than 93 bytes.** | 💀 Same `ArgumentOutOfRangeException`; `goto Outer`. Job stays in RG_B3 with no date set. | `Program.cs:268,275,285,346-351` | Medium — the crane physically picked/placed the container but the DB thinks the job is still open; yard state diverges. |
| 7 | **SQL injection through the wire** — crane (or attacker spoofing the crane) sends a packet where a field contains `'` or `--`. | 💀 Data is concatenated into SQL with `'…'` wrapping. A packet with `…'; DROP TABLE RG_A1;--…` at the right offset executes under SSPI. Partial mitigation: `ToUpper()` converts the SQL keywords but does not defuse them. | `Program.cs:211 (ToUpper)`, `Program.cs:225-236, 267-268, 287, 322-323` | **Critical** — any TCP-reachable attacker can run arbitrary SQL as the listener's Windows identity. |
| 8 | **SQL injection through the UI** — operator types `'` into Container / Password / Location. | 💀 Every text field value is concatenated into SQL with minimal or no escaping. `FrmLogin.cs:74` passes `Password.Text` unescaped (and PIN is numeric, so practically low-risk); `Form1.cs` inserts `Container.Text` raw into INSERTs. Operators are trusted and rarely type `'` intentionally, so this has probably never fired. | `FrmLogin.cs:74,87`, many in `Form1.cs` | Medium — accidental rather than malicious. |
| 9 | **Duplicate packet** — crane resends the same A1 / A2 twice (retry logic or network). | 💀 A1 is idempotent (same UPDATE twice). A2/03 PICK idempotent iff only `UPDATE RG_B3 SET PickDate = GetDate()` — but the `UPDATE dbo.TB_Parameters SET ContainerPick1='TRUE'` already sees the flag as TRUE (no-op). A2/04 PLACE is **NOT idempotent** — the INSERT into `RG_Shifting` runs twice; `UPDATE CO_Containers` with `DATEDIFF > 1` guard tolerates it but `DELETE RG_Container` becomes a no-op on the second call. Net: two `RG_Shifting` rows per real place. | `Program.cs:283-325` | High — `RG_Shifting` row duplication skews reports; `CO_Containers.LocationCode` gets re-written based on first packet. |
| 10 | **Wrong `CHE` value** — packet from Crane 1 arrives on port 30701, but with packet CHE field containing `GOLD2`. | ⚠ TOSConsole1 ignores the packet CHE entirely and hard-codes `CHE='GOLD1'` in the UPDATE WHERE clause. So data is written as if it were Crane 1 regardless. TOSService (stale) does read the packet CHE at offset 44. Inconsistency. | `TOSConsole1/Program.cs:236` (hardcoded), `TOSService/Program.cs:134` (reads position 44) | Low in practice (physically a crane can't send on another crane's port). Medium if a misconfigured network lets traffic leak across. |
| 11 | **Container ID typo** — operator types wrong 11-char container code. | 💀 No checksum validation on the container number (industry container IDs have a check digit — `ABCU 123456 7`). The INSERT into `RG_B3` accepts any 11-char string. Subsequent queries `SELECT Container FROM TB_Location WHERE …` may return 0 rows, at which point `Program.cs:269` skips the whole processing silently. | `Form1.cs:1420-1430`, `Program.cs:268-269` | Medium — operator workflow appears to succeed but `RG_Shifting` / `CO_Containers` are not updated. Yard state diverges. |
| 12 | **Two operators log in on the same crane** (e.g. shift change race, or two tablets running). | 💀 `V_RG_CurrentOperator` picks `MAX(LoginDate)` per `CHE` — only one is "current". Both UIs keep polling their own map and issuing their own writes. `RG_B3.OperatorID` can alternate. No session lock. | `05_module_definitions.sql:50-57`, `FrmLogin.cs:85-90` | Medium — audit trail reflects the *second* login; the first tablet's pending jobs get attributed incorrectly. |
| 13 | **PIN matches another operator's** — wrong person picks LoginName but their PIN happens to match. | ⚠ Login accepts; `RG_Log.OperatorID` = selected LoginName's `EmpID` (not the PIN owner's). Audit trail lies. | `FrmLogin.cs:69-90` | High — for audit / safety / HR, knowing *who actually did a movement* is essential. |
| 14 | **Failed login attempts** (brute force). | 💀 No lockout, no rate limit, no log of failures. 10 000 attempts in 10 seconds are allowed. | `FrmLogin.cs:69-98` | Medium — PIN space is only 10⁴ = 10000 values per operator group. A keypad-attacker can brute-force in seconds. |
| 15 | **DB deadlock** (SqlClient) during listener writes. | 💀 `Crc16Ccitt.ReturnDT` catches, logs, returns `null`. Caller `dt.Rows[...]` throws NullReferenceException → caught → `goto Outer`. Writes before the deadlock are committed; writes after are not. | `Crc32.cs:74-78` | High — yard state partially updated; no rollback. |
| 16 | **DB connection loss mid-packet** (DB server restart during A2/04 PLACE processing). | 💀 Same as #15 but worse — the PLACE handler runs **7 statements without a transaction**. Up to 6 may have committed before the crash. | `TOSConsole1/Program.cs:283-327` | High — `RG_B3.PlaceDate` set but `TB_Location` not updated, or vice versa. Yard state inconsistent. |
| 17 | **Listener exits mid-transaction** (process killed by `KillToss`, OS shutdown, OOM). | 💀 `Process.Kill()` from `KillToss*.exe` or SP `KillToss1` — immediate termination. Any open SqlClient connection is ABORTed by the server. Partial state as in #16. | `From TFS/RTG/KillToss1/Program.cs:17`, `05_module_definitions.sql:350-354` | High. |
| 18 | **Service-restart broadcasts duplicates** — after reconnect, listener re-sends all `RG_B3 WHERE A3Date IS NULL AND PickDate IS NULL` rows. | 💀 Crane firmware is expected to de-duplicate based on `Counter` — **we don't know if it does** (Q-11). | `TOSConsole1/Program.cs:181` | High — crane could execute the same job twice (e.g. pick a container twice — physical contradiction or error). |
| 19 | **CounterID wrap at 255** — UI `btnOK_Click` detects and handles: DELETE all RG_B3 rows, reset. | ⚠ Destroys history of in-flight jobs. If crane had a job with Counter=255 "in transit" and hadn't acked yet, that job vanishes. | `Form1.cs:1409-1413` | Medium — rare (255 jobs between housekeeping events), but catastrophic when it happens. |
| 20 | **Checksum ambiguity on outbound ACK** — `calcChecksum` returns variable-length hex (1-4 chars). | ⚠ The crane doesn't know where the checksum ends; the ACK has no length prefix. The firmware either uses fixed position or ignores the checksum entirely. | `Crc32.cs:18-30`, `Program.cs:36-42` | Low — works in production today but is fragile if a future ACK has a smaller checksum. |
| 21 | **Clock drift / time-zone drift** — cabin PC clock vs crane PLC clock. | 💀 UI's `TimerCHE_Tick_1` computes `SecondsByTimer` by parsing `RG_A1.Time` (HHmmss) via `TimeSpan.Parse(...).TotalSeconds` and comparing to `DateTime.Now.ToString("HHmmss")`. If PC and PLC disagree on `HH`, the 90-tick "stale" counter misfires. Also **fails at midnight rollover** (00:00:05 vs 23:59:58 has a 24-hour apparent delta). | `Form1.cs:1600-1622` | High — false "reconnect needed" alerts; real disconnects missed. |
| 22 | **GPS jitter / bad fix** — crane `GPSStatus` reports unreliable position. | 💀 Listener stores the value in `RG_A1.GPSStatus` but **never checks it**. UI displays the value in `DGH` but does not color-code or alert. | `Program.cs:232`, `Form1.cs:1580` | Medium — operator sees a map position that may be wrong; no visual cue of confidence. |
| 23 | **Stale cached `RG_A1` data after restart** — `RG_A1` has 3 rows that are PERSISTENT (`CHE` is PK). If listener restarts but crane doesn't reconnect, UI sees the old position. | ✅ The `Time` field will not change, so `CounterConnect > 90` eventually enables reconnect button. | `Form1.cs:1624-1635` | Low — handled, but the 45-second delay before the operator is notified is long. |
| 24 | **`CHEName.txt` missing or malformed** on cabin PC. | 💀 `System.IO.File.ReadAllLines(@"C:\RTG\CHEName.txt")` throws `FileNotFoundException`, uncaught, Windows Forms crash dialog. | `FrmLogin.cs:246-252` | Medium — cabin PC cannot log in; requires IT intervention. |
| 25 | **`CHEName.txt` contains wrong value** — someone types `BOND3` into a Crane-1 cabin by mistake. | 💀 UI logs in as if it were Crane 3, writes `RG_Log.CHE='BOND3'`, reads `V_MapRTGBond2` (wrong). Operator sees the wrong block's map. | `FrmLogin.cs:246-252` → `StrCHE = CHE.Text` → `RG_Log` | Medium — operator may notice "wrong yard" but confused audit trail in `RG_Log`. |
| 26 | **`Thread.Sleep(2000)` during submit blocks the UI**. | 💀 If DB is slow, listener race may still not have picked up the row after 2 seconds. UI shows misleading "green" state. | `Form1.cs:1440`, `FrmRecommendedLocation.cs:217` | Medium — operator gets conflicting signals. |
| 27 | **DataView filter with apostrophes in text** — Hebrew client names sometimes contain `'`. | 💀 `new DataView(dt, "לקוח = '" + this.cmbClientCode.Text + "'", …)` throws DataView filter parse exception. Uncaught. | `FrmRecommendedLocation.cs:226` | Low-Medium — real clients' names may contain `'` (e.g. `ל. מ'רקוס`). |
| 28 | **`TimerCHE` fires while previous tick's query is still running** (DB slow). | 💀 Each tick creates a new `ConTerminalData`, new SqlConnection, new adapter. SqlClient connection pool absorbs this up to its limit (default 100 connections per process). Beyond that, `InvalidOperationException` thrown on `Open()`, silently swallowed → UI freezes. | `Form1.cs:1579, 1659-1697` | Medium — during DB stress the UI becomes unresponsive. |
| 29 | **`RG_ErrorLog` write path recursion** — a log-write fails → triggers another log-write → ... | ✅ Both `Utils.WriteLog` (RTG1-2) and `ContainerLocation.WriteLog` (RTG3) wrap the INSERT in `try { } catch { }` with an empty catch — suppressing further failures. | `Utils.cs:18-31`, `ContainerLocation.cs:297-305` | Low — handled, at cost of silent log drops on DB outages. |
| 30 | **`RG_ErrorLog` fills the database** (currently 1.6 M rows, growth ≈ 500k/year). | 💀 No archival, no pruning. | `04_row_counts.txt:16` | High — disk pressure; log-table index maintenance during backup operations stalls workload. |
| 31 | **`RG_A1_LOG` fills the database** — the `rg_up_triger` trigger on `RG_A1` UPDATE inserts every packet. | ❓ Size unknown — table not in target list. Estimated 500k-1M rows per crane per year. | `06_triggers.sql:503-513` | High. |
| 32 | **Multi-row UPDATE on `TB_Location`** — trigger `TB_Location_Container_up` uses `count(*)=1` gate; multi-row updates don't fire the `RefreshMapRTG*` flags. | 💀 If a batch reassigns many slots (e.g. forklift bulk move), UI map won't refresh. | `06_triggers.sql:608-700` | Low — current code only does single-row updates, but fragile. |
| 33 | **`xp_cmdshell`-triggered restart misfires** — SP `RunEnconsoleRTG1` runs `'c:\RTG\RTG1\TOSConsole1.exe'` but the DB server and listener host are different. | ❓ Unknown — requires operational inspection. | `05_module_definitions.sql:387-391` | High — operator presses "Enconsole" button, no listener starts, crane stays silent. |
| 34 | **`ConsolesReRun` and `RunEnconsoleRTG*` disagree on paths** (`E:\RTG\Console{n}\` vs `C:\RTG\RTG{n}\`). | 💀 Either both paths exist on one machine (copies stay in sync somehow) or one of them is dead code. | `ConsolesReRun/Program.cs:11,21,31` vs `05_module_definitions.sql:390` | High — two restart mechanisms with different source-of-truth binaries. |
| 35 | **Operator enters `G` or `T` row but wrong BlocCode** — e.g. valid crane slot letter `G` typed with BOND1. | ⚠ Listener treats `G`/`T` specially (ground/truck) regardless of BlocCode. `TB_Location SELECT` may still succeed if BlocCode was valid. Logic branches in `Program.cs:255-263` interpret `G`/`T` as ground/truck. | `Program.cs:255, 313` | Medium — possible to mis-interpret the operation. |
| 36 | **Short packet causes `data.Substring(x, y)` ArgumentOutOfRangeException** — any sub-77-byte input to the wire. | 💀 Handled by outer catch → `goto Outer`. But the socket remains open and the same malformed client can keep sending garbage, stalling the listener. | `Program.cs:346-351` | Medium — malicious / misconfigured client can slow-DoS. |
| 37 | **No length validation on `Message` / `MessageCancel`** — `RG_B3` fields are `varchar(1500)`. | 💀 If UI composes a message longer than 1500 chars (hex), SQL throws truncation error. | `Form1.cs:1437-1438` | Low — current messages are < 200 hex chars. |
| 38 | **UI receives `NullReferenceException` from `ConTerminalData.ReturnDT(null return)`**. | 💀 Listener's `Crc16Ccitt.ReturnDT` returns `null` on error, but UI's `ConTerminalData.ReturnDT` doesn't have the same null-return path — exceptions bubble up. However, `FrmMap01.TimerCHE_Tick_1` does not wrap in try/catch — uncaught exception on a timer handler logs to `RG_ErrorLog` and continues. | `Class1.cs`, `Form1.cs:1573-1700` | Medium. |
| 39 | **Concurrent writes to `RG_B3` from two UIs on the same crane** — two cabs (unlikely but possible during training / parallel ops) both `SELECT MAX(CounterID) + 1`. | 💀 No UNIQUE constraint (confirmed: `PK_RG_B3` is (CHE, CounterID) but the next-value is read by SELECT MAX + 1 — race). Two INSERTs can collide on PK, one throws. | `Form1.cs:1417, 1420-1430` | Low — in practice only one cabin per crane, but PR code would hit this. |
| 40 | **Accidental typing of Hebrew text into container-ID field** — keypad is numeric, but operator might paste. | 💀 No char-set validation. Field stored as-is. Listener's SELECT later returns no rows. | `Form1.cs` (numeric button handlers do `+=`) | Low. |

---

## 2. Cross-references

| Area | Edge-case IDs |
|---|---|
| Protocol / wire | 1, 2, 3, 4, 5, 6, 7, 10, 18, 20, 36 |
| SQL injection | 7, 8 |
| Authentication | 13, 14 |
| Database integrity | 9, 15, 16, 17, 19, 26, 28, 38, 39 |
| Operational / deploy | 24, 25, 33, 34 |
| Data retention / growth | 29, 30, 31 |
| Time / clock | 3, 21, 23 |
| Multi-user / multi-UI | 12, 39 |
| UI quirks | 21, 26, 27, 32, 37, 40 |
| Incomplete schema validation | 11, 22, 35 |

---

## 3. Summary and priorities for Phase 2

### 3.1 Must-fix in Phase 2 (severity-driven)

1. **#7 SQL injection from the wire** — critical.
2. **#1, #2 missing TCP framing** — data-integrity blocker.
3. **#16, #17 partial-transaction yard state divergence** — must wrap PLACE logic in a transaction (PG) or deferred outbox (dual-write).
4. **#13 PIN-LoginName decoupled in auth** — identity / safety / compliance.
5. **#3 no TCP keep-alive** — needs `SO_KEEPALIVE` with short probe.
6. **#14 unlimited PIN attempts** — lockout after N failures.
7. **#30, #31 log-table growth** — partitioning + retention.
8. **#33, #34 restart path duplication** — single source-of-truth for process supervision.
9. **#29 UI silently drops log writes** — centralize client-side logging.
10. **#21 midnight rollover + clock drift** — proper `timestamp with time zone` semantics.

### 3.2 Nice-to-fix

- #4, #23: already handled but noisy.
- #27: Hebrew apostrophe handling.
- #11: container checksum digit.

### 3.3 Deliberate preserve-as-is

- #18 job broadcast after reconnect: we must keep re-sending until the crane protocol allows explicit delivery receipt; **but** add a watermark to avoid loops.
- #20 variable-length checksum on wire: we must maintain byte-compat with the crane firmware.

---

## 4. Check-in summary

- **40 edge cases catalogued** with file:line evidence. Top concerns fall into four buckets: (a) wire-level integrity (#1, #2, #7, #18); (b) DB transactional integrity (#9, #15, #16, #17); (c) identity / auth (#13, #14); (d) growth / retention (#30, #31).
- **Most-severe new finding during this step:** the **combination** of (no framing) + (no inbound checksum validation) + (string-concatenation SQL) means a misbehaving or spoofed crane can both poison data silently AND execute arbitrary SQL. This is the single biggest security reason to prioritize the listener redesign.
- **Most-impactful ops finding:** restart mechanism disagreement between `ConsolesReRun.exe` (looks at `E:\RTG\Console{n}\`) and SP `RunEnconsoleRTG{n}` (looks at `C:\RTG\RTG{n}\`). Must be resolved before Phase 2 deploys.
- **Next:** Step 1.8 — reverse-engineer the compiled binaries in `RTG/` (exes, dlls) to confirm framework/version matches source, detect dependencies, and verify that deployed binaries match the TFS source (any "hot-patched" unchecked-in versions?).
