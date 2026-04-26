# 13 — NormalizeBlock strict · QA brief

## What broke before this fix
A heartbeat from a crane reporting an unknown block name (typo, future block code, mis-config) was silently rewritten as BOND1. Cabin maps placed the crane in the wrong block, with no log line to explain it.

## Setup
- Listener with PG persistence
- A way to inject an A1 with arbitrary `block_code` (test harness or replay)
- Log tail and metrics endpoint visible

## Test scenarios
1. **Known block** — Inject A1 with BOND1. Expected: persisted normally; no warning.
2. **Unknown block** — Inject A1 with BOND4. Expected: not persisted; structured warning logged; metric increments by 1.
3. **Typo** — Inject A1 with BOND01. Expected: same as unknown.

## Edge cases worth probing
- Whitespace and case variants (`bond1`, ` BOND1`)
- Burst of unknown-block heartbeats — metric must not under-count
- Recovery once the crane returns to a known block

## Sign-off
- [ ] No silent BOND1 fallbacks
- [ ] Metric matches injected unknown-block count
- [ ] Cabin map never displays a crane in the wrong block due to this path
