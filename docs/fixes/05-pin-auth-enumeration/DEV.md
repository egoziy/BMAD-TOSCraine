# 05 — PIN auth enumeration

**Severity:** CRITICAL

## Problem
`/api/auth/login` returns identical 401 for unknown user vs wrong PIN, but `/api/auth/operators` is `AllowAnonymous` and lists every active `login_name`. Argon2 verify only runs on a known user, so an attacker iterates the dropdown and brute-forces PINs. The existing `LockedUntil` is per-account, not per-IP.

## Files to change
- `src/Rtg.Api/Auth/AuthEndpoints.cs:29` — `/api/auth/operators` AllowAnonymous list
- `src/Rtg.Api/Auth/AuthEndpoints.cs:73-81` — login 401 path

## Failing test to write first
`AuthEndpoints_OperatorsRequiresAuth_OrRateLimited`. Hit `/api/auth/operators` 100 times anonymously from one IP. Assert either the endpoint requires auth or returns 429 after threshold. Today it returns the full operator list every time.

## Implementation hint
Either move `/api/auth/operators` behind authentication (cabin can fetch its own operator after login) or apply a per-IP rate limit. Add a per-IP+per-login_name attempt counter alongside the existing per-account `LockedUntil`. Add CSRF/replay protection on `/api/auth/login`.

## Acceptance criteria
- [ ] Anonymous enumeration of operators is blocked or rate-limited
- [ ] Per-IP+per-login_name counter locks repeat offenders independent of `LockedUntil`
- [ ] CSRF/replay token required on login POST
