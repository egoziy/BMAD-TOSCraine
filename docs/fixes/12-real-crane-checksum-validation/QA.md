# 12 — Real-crane checksum validation · QA brief

## What broke before this fix
With `ValidateInboundChecksum: false`, mis-framed packets from a real crane were silently dropped. The listener log showed "OK" while the cabin map fell behind. We had no visibility into protocol errors during the chesimu-to-real-crane transition.

## Setup
- Listener built with the new per-crane setting
- A captured real-crane stream (or live GOLD3 with permission to test)
- Prometheus scrape against the listener

## Test scenarios
1. **Strict on real crane** — Configure GOLD3 with `ValidateInboundChecksum: true`. Replay a known-good capture. Expected: frames parsed, no `unknown_frame` increments.
2. **Strict catches bad checksum** — Inject a flipped byte into one frame. Expected: that frame is rejected; metric increments by 1; remaining stream continues.
3. **Permissive on chesimu** — Leave the simulator's setting false. Expected: existing dev workflow unaffected.

## Edge cases worth probing
- A2 frame at exactly the new minimum length boundary
- A4 frame variant from real crane (currently chesimu-untested)
- Burst of mis-framed packets — metric must reflect actual count

## Sign-off
- [ ] Real-crane capture parses with strict validation
- [ ] `unknown_frame` metric is reliable on injected corruption
- [ ] Chesimu workflow still works with permissive setting
