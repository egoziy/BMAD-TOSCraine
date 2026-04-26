# 10 — Cabin write queue 401 handling

**Severity:** SHOULD FIX

## Problem
`WriteQueueWorker._attempt` treats every 4xx as permanent. When the JWT expires mid-shift, every queued cabin write flips to `failed_permanent` and the operator silently loses pending location edits.

## Files to change
- `src/rtg_cab/lib/services/write_queue_worker.dart:64` — 4xx handling in `_attempt`

## Failing test to write first
`WriteQueueWorker_401_TriggersReauthAndRetry`. Queue a write, return 401 once, then 200 on retry. Assert the worker reauthenticates and the write succeeds. Today the row is moved to `failed_permanent` after the first 401.

## Implementation hint
Distinguish 401/403 from other 4xx: on 401/403, attempt token refresh / reauth and retry the original request once; on any other 4xx, keep the existing permanent-failure behaviour.

## Acceptance criteria
- [ ] 401 response triggers token refresh and a single retry
- [ ] 403 response triggers reauth or surfaces a clear UX state, not silent loss
- [ ] 400/404/422 still treated as permanent
