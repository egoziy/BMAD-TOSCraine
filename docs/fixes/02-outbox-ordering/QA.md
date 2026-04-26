# 02 — Outbox ordering · QA brief

## What broke before this fix
With two listener instances draining the same PG outbox, MSSQL occasionally showed a container's place row before its pick row. Yard map briefly displayed the container at its old slot then jumped — or worse, jumped and stayed wrong.

## Setup
- Two listener processes pointed at the same PG database
- Test harness that can inject ordered outbox rows for a single `aggregate_id`
- MSSQL `RG_Shifting` query handy: `SELECT * FROM RG_Shifting WHERE ContainerNo = … ORDER BY EventTime`

## Test scenarios
1. **Single-aggregate burst** — Inject pick, place, pick (same container) in quick succession. Run two listeners. Expected: `RG_Shifting` rows appear in pick/place/pick order.
2. **Mixed aggregates** — Inject 100 events across 20 containers, each container's events ordered. Run two listeners. Expected: every container's history is in source order; total throughput ≥ single-listener baseline.
3. **Worker crash mid-drain** — Kill one of the two listeners while draining. Expected: surviving listener continues without ordering violations.

## Edge cases worth probing
- Same `aggregate_id` arriving rapidly within < 50ms
- Hash collision between two aggregates landing on the same stripe (still ordered, still parallel across stripes)
- Advisory-lock loser starves indefinitely if winner never releases — should restart cleanly

## Sign-off
- [ ] No out-of-order `RG_Shifting` rows in 1000-event soak
- [ ] Two-listener throughput ≥ single-listener baseline
- [ ] Restart of either listener does not corrupt order
