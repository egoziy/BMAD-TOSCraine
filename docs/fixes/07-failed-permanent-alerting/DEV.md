# 07 — FAILED_PERMANENT alerting

**Severity:** SHOULD FIX

## Problem
After 10 attempts, an outbox row flips to `FAILED_PERMANENT` and sits in PG forever. MSSQL diverges silently. Nothing alerts; nothing fails the readiness probe.

## Files to change
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:168` — `FAILED_PERMANENT` UPDATE
- `src/Rtg.Listener/Program.cs:96` — `/health/ready` endpoint

## Failing test to write first
`HealthReady_FailsWhenFailedPermanentExists`. Seed one outbox row with `status='FAILED_PERMANENT'`. Hit `/health/ready`. Assert 503 or unhealthy. Today the endpoint returns 200.

## Implementation hint
Expose a Prometheus gauge `rtg_outbox_failed_permanent_total` driven by `SELECT COUNT(*) FROM outbox WHERE status='FAILED_PERMANENT'`. Wire `/health/ready` to fail when count > 0 (or above a configurable threshold).

## Acceptance criteria
- [ ] `rtg_outbox_failed_permanent_total` metric is exposed and accurate
- [ ] `/health/ready` returns unhealthy when count exceeds threshold
- [ ] Alerting threshold is configurable via `appsettings.json`
