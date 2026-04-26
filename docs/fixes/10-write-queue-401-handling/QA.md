# 10 — Cabin write queue 401 handling · QA brief

## What broke before this fix
When the cabin JWT expired mid-shift, the operator's queued location edits silently flipped to `failed_permanent`. The user saw no error, but the changes never reached the server. Reload the screen and your work was gone.

## Setup
- Cabin app built, logged in, with a queued write outstanding
- Server configured with a short JWT lifetime (minutes) for testing
- A way to invalidate or rotate the active token

## Test scenarios
1. **JWT expiry** — Log in. Wait for token to expire. Make a location edit. Expected: write succeeds after a transparent token refresh; queue is empty.
2. **Server returns 403** — Force a 403 from the API. Expected: reauth attempt or clear UX state; not silent permanent failure.
3. **Real permanent 4xx** — Send a malformed payload that yields 400. Expected: row marked `failed_permanent` as before; user sees an error.

## Edge cases worth probing
- Refresh token also expired — must surface a logout / relogin flow
- Multiple queued writes when token expires (only one refresh, all retried)
- Network drop during the refresh roundtrip

## Sign-off
- [ ] No queued writes silently lost across a JWT expiry
- [ ] Operator never has to manually re-enter data after a token refresh
- [ ] 400/404/422 still reach `failed_permanent` and surface an error
