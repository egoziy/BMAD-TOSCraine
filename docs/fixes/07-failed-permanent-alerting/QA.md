# 07 — FAILED_PERMANENT alerting · QA brief

## What broke before this fix
Outbox rows that hit max retries flipped to `FAILED_PERMANENT` and stayed there. No dashboard, no page, no readiness failure. Operations only noticed when MSSQL state visibly disagreed with PG days later.

## Setup
- Rtg.Listener with PG outbox
- Prometheus scrape (or curl `/metrics`)
- Ability to insert an outbox row directly with `status='FAILED_PERMANENT'`

## Test scenarios
1. **Clean baseline** — Empty outbox. Hit `/health/ready` and `/metrics`. Expected: ready 200, metric value 0.
2. **One failure** — Insert one row with status `FAILED_PERMANENT`. Expected: ready returns 503; metric value 1.
3. **Recovery** — Manually clear or reprocess the failed row. Expected: ready returns 200; metric drops to 0.

## Edge cases worth probing
- Threshold configured > 0 (small number of permanent failures tolerated)
- Metric refresh lag during burst of failures
- `/health/ready` called during a database hiccup (must distinguish DB-down from failed-row-present)

## Sign-off
- [ ] Metric matches actual count on a 10-row sample
- [ ] Readiness probe flips correctly across the threshold
- [ ] Alert fires within configured scrape interval
