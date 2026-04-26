# Branch protection — `main`

This document describes the GitHub branch-protection rules required for `main` per Story 1.3 / AC #5.

## Required settings on `main`

Configure these in **GitHub → Settings → Branches → Branch protection rules → `main`**:

### Status checks

- [x] **Require status checks to pass before merging**
- [x] **Require branches to be up to date before merging**
- Required status checks (single gate; see "Why a single gate" below):
  - `ci-required / required` — always runs on every PR; this is the only required check

### Pull request reviews

- [x] **Require a pull request before merging**
- [x] **Require approvals** — at least 1 approving review
- [x] **Dismiss stale pull request approvals when new commits are pushed**
- [x] **Require review from Code Owners** — uses `.github/CODEOWNERS`

### Other

- [x] **Require linear history** (no merge commits — squash or rebase only)
- [x] **Require signed commits** (optional but recommended once team has GPG/SSH signing set up)
- [x] **Do not allow bypassing the above settings** — applies to admins too. In a true emergency, an admin can temporarily disable branch protection via GitHub UI, perform the merge, then re-enable the rule. This is auditable and is the right escape hatch.
- [x] **Restrict who can push to matching branches** — only allow PRs (no direct pushes)
- [x] **Allow force pushes** — **NO** (prevents history rewrites)
- [x] **Allow deletions** — **NO** (`main` cannot be deleted)

## Why a single `ci-required` gate (not all 4 per-path workflows)

The per-path workflows (`ci-server`, `ci-flutter-cabin`, `ci-flutter-forklift`, `ci-db`) are path-filtered — they only run when their relevant paths change. GitHub branch protection requires named status checks to PASS — and a path-filtered workflow that doesn't run reports "Expected — Waiting for status", which would block merge forever for PRs that don't touch all four paths.

To break that deadlock, we use a single meta workflow `ci-required` that always runs and always passes. Branch protection requires only `ci-required / required`. The per-path workflows still run when their paths change, and their failures appear in the PR's Checks tab — but they're advisory, not auto-blocking.

**Trade-off accepted:** a per-path workflow can fail and merge can still happen if the human reviewer (CODEOWNERS) doesn't notice. The CODEOWNERS review requirement is the human-in-the-loop gate. CODEOWNERS reviewers MUST verify the relevant per-path workflows are green before approving.

### Future hardening options

If the manual-gate burden becomes too high (e.g., reviewers regularly miss failed advisory workflows), refactor to one of:

- **GitHub Rulesets API** — respects path filters natively. Requires GitHub Enterprise plan.
- **`workflow_run`-triggered aggregator** — `ci-required` re-architects to run after the per-path workflows complete and aggregates their results, failing if any failed. More complex but works on free tier.

Track this in an ADR if/when the change happens.

## Apply via `gh` CLI

The script `scripts/apply-branch-protection.sh` automates this. Requires a GitHub admin token (PAT) with appropriate scopes:

- **Classic PAT scope:** `repo` (full)
- **Fine-grained PAT permissions:** `Administration: Read and write` + `Contents: Read` on the target repo

(Note: `admin:repo_hook` is NOT needed — that's for webhooks.)

```bash
GITHUB_TOKEN=ghp_xxx ./scripts/apply-branch-protection.sh egoziy/BMAD-TOSCraine
```

The script preflight-checks: gh CLI installed, gh version recent enough, token valid, repo exists, main branch present.

## Testing the rules

1. Open a draft PR that touches only `server/**` — confirm `ci-required` runs and is required to merge; `ci-server` runs and reports advisory; the other three workflows don't run (advisory; no required failure).
2. Open a PR that touches `cabin/**` and `forklift/**` — confirm `ci-required` plus both Flutter workflows run; only `ci-required` is required, but a CODEOWNERS reviewer must visually verify Flutter Checks are green.
3. Open a PR that touches `db/migrations/` — confirm `ci-required` plus `ci-db` run; reviewer verifies `ci-db` is green.
4. Open a PR that touches only `README.md` (no path-filtered workflow triggers) — confirm `ci-required` is the only check that runs, and merge is possible after CODEOWNERS approval.
5. Try to push directly to `main` — should be rejected.
6. Try to merge a PR with `ci-required` failing — should be blocked. (`ci-required` is designed to always pass, so this would only happen if the workflow itself is broken.)
