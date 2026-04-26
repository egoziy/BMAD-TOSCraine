# 02 — Outbox ordering under concurrency

**Severity:** CRITICAL

## Problem
`FOR UPDATE SKIP LOCKED LIMIT 100` lets two workers (GBDEV + 192.6.8.3 bridge) interleave a2_place and a2_pick for the same container. MSSQL ends up with the wrong terminal state because mirror writes can land out of source order.

## Files to change
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:111-117` — `SELECT … FOR UPDATE SKIP LOCKED LIMIT 100`

## Failing test to write first
`OutboxWorker_TwoWorkers_PreservePerAggregateOrder`. Seed three events for the same container in order (pick → place → pick). Run two workers concurrently against the same outbox. Assert MSSQL `RG_Shifting` history rows for that container appear in source order. Today the second worker can grab the place before the first worker finishes the pick.

## Implementation hint
Either (a) hash `aggregate_id` to N stripes and run one worker per stripe, with `WHERE hashtext(aggregate_id) % N = :stripe` in the SELECT; or (b) take a PG advisory lock at startup (`pg_try_advisory_lock`) so only one worker drains at a time across the cluster.

## Acceptance criteria
- [ ] Concurrent drain preserves source order per `aggregate_id`
- [ ] Two listener instances can run simultaneously without losing throughput
- [ ] Test exercises ≥2 workers and ≥3 events per aggregate
