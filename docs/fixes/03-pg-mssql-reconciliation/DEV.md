# 03 — PG ↔ MSSQL reconciliation

**Severity:** CRITICAL

## Problem
`MssqlLocationImporter` runs every 60s, MSSQL→PG, and overwrites blindly. The legacy `ContainerLocation` form still writes to MSSQL, so it clobbers the writes our outbox just mirrored from PG. No checksum/diff job exists, so divergence is invisible until an operator notices a wrong slot.

## Files to change
- `src/Rtg.Api/Services/MssqlLocationImporter.cs` — every-60s overwrite path

## Failing test to write first
`MssqlLocationImporter_DoesNotClobberNewerOutboxWrite`. PG has `rtg.location` written by outbox at T+10s. MSSQL `TB_Location` was last written at T+0s by legacy form. Run importer at T+15s. Assert PG row is unchanged. Today PG gets overwritten with the older MSSQL value.

## Implementation hint
Add `last_writer` (`'outbox' | 'legacy' | 'importer'`) and `last_writer_ts` columns on both sides. Importer only overwrites PG when MSSQL `last_writer_ts > pg.last_writer_ts`. Add a daily job that checksums `rtg.location` against `dbo.TB_Location` and emits a divergence count metric.

## Acceptance criteria
- [ ] Importer respects `last_writer_ts` and skips when PG is newer
- [ ] `last_writer` column populated on every write path (outbox mirror, legacy, importer)
- [ ] Daily reconciliation job logs divergence count and exposes it as a metric
