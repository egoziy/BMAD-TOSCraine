# 01 — Outbox crash safety · QA brief

## What broke before this fix
After a listener crash mid-drain, MSSQL `RG_Shifting` got duplicate history rows for the same move and `RG_Container` rows for in-flight pickups disappeared. Operators saw containers vanish from cabin map between a pick and its place.

## Setup
- Rtg.Listener running against PG + TerminalData_AI clone
- Outbox empty; `dbo.outbox_processed` table exists on MSSQL
- Ability to SIGKILL the listener (use task manager or `Stop-Process -Force`)

## Test scenarios
1. **Clean replay** — Inject one a2_place via test harness. Let it drain. Verify PG row `SENT`, `dbo.outbox_processed` has one row, `RG_Shifting` has one row. Expected: all three counts = 1.
2. **Crash between commits** — Inject one a2_place. Pause MSSQL commit (debug breakpoint or sleep injection between MSSQL commit and PG UPDATE). SIGKILL the listener after MSSQL commits. Restart. Expected: `RG_Shifting` still has one row; PG row eventually flips to `SENT`; no duplicate `outbox_processed` insert.
3. **Replay of pick** — Same as #2 but for a2_pick. Expected: `RG_Container` DELETE only happens once; in-flight pickup state preserved across replay.

## Edge cases worth probing
- Two events with the same idempotency_key (defensive dedupe at source)
- MSSQL connection drop mid-transaction — should rollback both `outbox_processed` insert and mirror SQL
- Long-running drain crossing a PG transaction timeout

## Sign-off
- [ ] No `RG_Shifting` duplicates after 100 induced crashes
- [ ] `dbo.outbox_processed` row count matches PG `status='SENT'` count
- [ ] In-flight pickup never lost across a forced restart
