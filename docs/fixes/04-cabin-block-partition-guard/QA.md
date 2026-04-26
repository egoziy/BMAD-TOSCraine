# 04 — Cabin block partition guard · QA brief

## What broke before this fix
A GOLD1 crane operator picking a container that the system thought was in BOND2 saw nothing on the cabin yard map, the move was not blocked, and TB_Location was never updated. Symptom: container "vanished" until someone noticed and corrected by hand.

## Setup
- Listener configured per the existing 1:1 mapping (GOLD1↔BOND1, GOLD2↔BOND2, GOLD3↔BOND3)
- Cabin app booted against `bootstrap_sample.json`
- A test fixture or replay tool that can send an A1 with arbitrary `block_code`

## Test scenarios
1. **Correct block** — GOLD1 sends A1 with BOND1. Expected: accepted, cabin map updates, MSSQL UPDATE touches one row.
2. **Foreign block** — GOLD1 sends A1 with BOND2. Expected: listener refuses, structured error logged, metric `foreign_block` increments by 1, no PG write.
3. **Unknown block** — GOLD1 sends A1 with BOND9. Expected: refused; metric increments.

## Edge cases worth probing
- Block name with whitespace or wrong case (`bond1`, ` BOND1`)
- Crane id mismatch on the same TCP session
- Burst of foreign-block frames (metric must not under-count)

## Sign-off
- [ ] No silent zero-row UPDATEs in a 30-minute mixed-traffic soak
- [ ] Foreign-block metric matches injected count exactly
- [ ] Cabin yard map reflects only events from its configured block
