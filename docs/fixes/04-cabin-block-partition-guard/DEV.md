# 04 — Cabin block partition guard

**Severity:** CRITICAL

## Problem
`appsettings.json` allows GOLD1/GOLD2 on BOND1+BOND2 but cabin `bootstrap_sample.json` pins each cabin to one block. When GOLD1 picks in BOND2, the GOLD1 cabin yard map never sees it, and `MirrorA2PlaceAsync` UPDATE matches zero rows because TB_Location is under the other block. Operator sees no error.

## Files to change
- `src/Rtg.Listener/appsettings.json:9,17` — per-crane allowed-blocks list
- `src/rtg_cab/assets/bootstrap_sample.json` — cabin-to-block pin
- `src/Rtg.Listener/Persistence/OutboxWorker.cs:625-633` — `MirrorA2PlaceAsync` zero-row UPDATE

## Failing test to write first
`A1ForeignBlock_IsRejectedAtIngest`. Send an A1 from GOLD1 declaring BOND2. Assert the listener rejects the frame with a structured error and does not write to PG. Today the frame is accepted and silently mismirrored.

## Implementation hint
Hard-restrict each crane to one block on the wire side: refuse A1 with foreign `block_code` and increment a `Metrics.ProtocolErrors.foreign_block` counter. Per-crane block partition matches the existing 1:1 GOLD↔BOND mapping.

## Acceptance criteria
- [ ] Listener refuses A1 with a `block_code` outside the crane's configured block
- [ ] Refusal emits a structured log event and a metric increment
- [ ] `MirrorA2PlaceAsync` UPDATE always touches exactly one row, or the event goes to FAILED with a clear reason
