# 03 — PG ↔ MSSQL reconciliation · QA brief

## What broke before this fix
Cabin operators occasionally saw containers snap back to a previous slot 60 seconds after a real-crane move. The cause was the importer overwriting PG with stale MSSQL data because the legacy form had touched the same row.

## Setup
- Rtg.Api running with `MssqlLocationImporter` enabled
- A row in `rtg.location` recently written by outbox mirror
- A row in `dbo.TB_Location` for the same slot last written by the legacy form a few seconds earlier
- Daily reconciliation job scheduled and ready to invoke manually

## Test scenarios
1. **Newer PG wins** — Touch `rtg.location` at T+10s via outbox. MSSQL last write at T+0s. Wait for next importer cycle. Expected: PG unchanged.
2. **Newer MSSQL wins** — Touch `dbo.TB_Location` via legacy form at T+10s. PG last write at T+0s. Importer cycle. Expected: PG updated.
3. **Reconciliation job** — Force a divergence by editing one row directly on each side. Run the daily job. Expected: divergence count = 1 in metric/log.

## Edge cases worth probing
- Clocks skewed between PG and MSSQL servers
- Row written on one side with no `last_writer_ts` (pre-migration)
- Reconciliation job during heavy outbox drain

## Sign-off
- [ ] No snap-back observed during a one-hour cabin demo with concurrent legacy writes
- [ ] Daily job runs and reports zero divergence on a clean system
- [ ] Manually induced divergence is detected and reported within 24h
