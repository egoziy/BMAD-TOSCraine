# 05 — PIN auth enumeration · QA brief

## What broke before this fix
Anyone on the network could pull the full operator list from `/api/auth/operators` without credentials, then brute-force 4-digit PINs against `/api/auth/login`. Per-account lockout slowed individual targets but did not stop iteration across all logins.

## Setup
- Rtg.Api running locally
- A second machine on the same subnet for the attacker viewpoint
- Several seeded operator accounts with known PINs

## Test scenarios
1. **Anonymous enumeration** — Curl `/api/auth/operators` with no token. Expected: 401, or 429 after the rate threshold.
2. **Distributed brute force** — Try 200 wrong PINs across 10 different `login_name`s from one IP. Expected: per-IP counter locks the IP after threshold; existing per-account lockout still applies.
3. **CSRF replay** — Capture a successful login token; replay the same request after expiry. Expected: rejected.

## Edge cases worth probing
- IP address spoofing via `X-Forwarded-For` (must use trusted source only)
- Cabin login from a known good operator IP during attack on adjacent accounts (legitimate user must not be locked out)
- Wall clock drift between server and client around CSRF expiry

## Sign-off
- [ ] `/api/auth/operators` does not return data anonymously
- [ ] Brute-force from one IP is locked within configured threshold
- [ ] Replay of an old login POST is refused
