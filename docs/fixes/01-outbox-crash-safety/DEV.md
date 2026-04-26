# 01 — Outbox crash safety

**Severity:** CRITICAL

## Problem
PG transaction and MSSQL `SqlConnection` are independent. If MSSQL commits but the process crashes before PG marks the row `SENT`, the next drain replays the same event. `MirrorA2PlaceAsync` is non-idempotent: replays double-write `RG_Shifting` history and DELETE `RG_Container`, losing in-flight pickup state.

## Files to change
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:140-187` — drain loop and PG `status='SENT'` UPDATE
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:200-203` — `MirrorToMssqlAsync` opens MSSQL connection
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:504` — `MirrorA2PlaceAsync` non-idempotent path

## Failing test to write first
`OutboxWorker_Replay_DoesNotDoubleWriteRgShifting`. Drain a single a2_place row, kill the worker between MSSQL commit and PG UPDATE, restart, drain again. Assert `RG_Shifting` has exactly one row for that idempotency_key. Today the test sees two.

## Implementation hint
Add `dbo.outbox_processed (idempotency_key PK, processed_at)` on MSSQL. Inside the same `SqlTransaction` as each mirror SQL, `INSERT … WHERE NOT EXISTS` on the key, and gate every mirror write with the same `NOT EXISTS` check.

## Acceptance criteria
- [ ] Replay of any outbox row is a no-op on MSSQL side
- [ ] `dbo.outbox_processed` row is written in the same SqlTransaction as the mirror SQL
- [ ] Crash-between-commits test passes; `RG_Shifting` rowcount stays at 1
