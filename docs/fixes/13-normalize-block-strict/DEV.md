# 13 — NormalizeBlock strict

**Severity:** SHOULD FIX

## Problem
`NormalizeBlock` silently maps unknown block names to BOND1. A real crane sending an unknown block (BOND4 future expansion, typo, anything off-list) writes A1 heartbeats as BOND1. The cabin map shows the crane in the wrong block.

## Files to change
- `src/Rtg.Listener/Persistence/PgCranePersistence.cs:557-569` — `NormalizeBlock`

## Failing test to write first
`NormalizeBlock_UnknownName_ThrowsAndSkipsPersistence`. Call `NormalizeBlock("BOND4")`. Assert it throws (or returns null) and that `PgCranePersistence` skips persistence and logs. Today the function returns `"BOND1"` and the bad heartbeat is written.

## Implementation hint
Replace the silent default with a throw or explicit null. The caller logs a structured event and skips persistence. Add a metric for unknown-block events so they are visible.

## Acceptance criteria
- [ ] Unknown block name no longer silently maps to BOND1
- [ ] Caller logs a structured event and skips the heartbeat
- [ ] Metric increments per unknown-block event
