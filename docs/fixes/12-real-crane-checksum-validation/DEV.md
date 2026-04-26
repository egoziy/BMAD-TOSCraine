# 12 — Real-crane checksum validation

**Severity:** SHOULD FIX

## Problem
Protocol constants are pinned to chesimu (the simulator). `A2_MIN_LENGTH = 180` is chesimu-derived, A4 framing is flagged as "follows once we run chesimu", and `ValidateInboundChecksum: false` masks framing bugs by silently dropping mis-framed packets. Real-crane cutover will hit issues we currently cannot see.

## Files to change
- `src/Rtg.Listener/Protocol/Framer.cs:42` — `A2_MIN_LENGTH = 180`
- `src/Rtg.Listener/Protocol/FrameParser.cs:145` — A4 follow-up comment
- `src/Rtg.Listener/appsettings.json:31` — `ValidateInboundChecksum: false`

## Failing test to write first
`Framer_RealCraneFrame_RejectedWithUnknownFrameMetric`. Replay a captured GOLD3 frame with checksum validation on. Assert either it parses or it increments `Metrics.ProtocolErrors.unknown_frame`. Today validation is off and silent drops are invisible to tests.

## Implementation hint
Flip `ValidateInboundChecksum` to true in dev against the GOLD3 stream before cutover. Make the setting per-crane so chesimu can stay permissive while real cranes are strict. Wire `Metrics.ProtocolErrors.unknown_frame` and verify in tests.

## Acceptance criteria
- [ ] `ValidateInboundChecksum` is per-crane and defaults to true for real cranes
- [ ] `unknown_frame` metric increments on every dropped frame
- [ ] Captured real-crane stream parses cleanly with strict validation
