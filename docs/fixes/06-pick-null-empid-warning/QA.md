# 06 — Pick null op-id warning · QA brief

## What broke before this fix
When the wire data carried a null `operator_emp_id` on a pick, the move appeared to complete normally on the cabin side, but the legacy `CO_Containers` forklift dates were never stamped. Billing reports later showed undercounts that nobody could trace.

## Setup
- Listener with TerminalData_AI clone
- A way to inject an a2_pick with `operator_emp_id = null` (test harness or modified replay)
- Access to `RG_Container`, `RG_Shifting`, `CO_Containers` for verification

## Test scenarios
1. **Null op-id with policy = warn** — Inject pick with null op-id, then matching place. Expected: warning event in log; `CO_Containers` forklift trio stamped (sentinel or fallback); end-to-end count correct.
2. **Null op-id with policy = fail** — Same input, with FAIL_PERMANENT policy. Expected: pick row in `FAILED_PERMANENT`; metric increments; place either also fails or proceeds without state.
3. **Valid op-id baseline** — Same flow with a real op-id. Expected: stamping works as before; no warning emitted.

## Edge cases worth probing
- Stale `RG_Container` row from a previous run (must not satisfy place lookup)
- Burst of null-op-id picks (warning rate must not flood logs)
- Op-id present but unknown to `dbo.Employees` (separate failure mode, do not collapse into this one)

## Sign-off
- [ ] No silent skip of `RG_Container` insert
- [ ] Warning or FAILED_PERMANENT visible per policy on every null-op-id event
- [ ] Billing counts reconcile end-to-end on a 100-event sample
