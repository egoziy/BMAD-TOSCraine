# 06 — Pick null op-id warning

**Severity:** SHOULD FIX

## Problem
`MirrorA2PickAsync` silently skips the `RG_Container` insert when `operator_emp_id` is null. `MirrorA2PlaceAsync` then reads `RG_Container` to drive `EntranceForkliftDate` stamping, sees no in-flight container, and the legacy `CO_Containers` forklift trio is never stamped. Billing undercounts.

## Files to change
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:471-480` — `MirrorA2PickAsync` null-op-id branch
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:542-563` — `MirrorA2PlaceAsync` reads `RG_Container`

## Failing test to write first
`MirrorA2Pick_NullOpId_DoesNotSilentlySkip`. Inject an a2_pick with `operator_emp_id = null`, then a matching a2_place. Assert either (a) a structured warning event is emitted and `CO_Containers` forklift trio is stamped via fallback, or (b) the pick is moved to `FAILED_PERMANENT`. Today both events succeed but stamping is skipped.

## Implementation hint
Decide policy: tolerate null op-id (emit warning + use a sentinel emp id for `RG_Container`) or treat it as bad data and route the pick to FAILED_PERMANENT. Either way, do not silently drop the row.

## Acceptance criteria
- [ ] Null `operator_emp_id` triggers a structured warning event with the outbox row id
- [ ] `RG_Container` insert either happens with a sentinel value or the pick goes to FAILED_PERMANENT
- [ ] `MirrorA2PlaceAsync` no longer silently no-ops because of an upstream null
