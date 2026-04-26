# Branch protection — `main`

This document describes the GitHub branch-protection rules required for `main` per Story 1.3 / AC #5.

## Required settings on `main`

Configure these in **GitHub → Settings → Branches → Branch protection rules → `main`**:

### Status checks

- [x] **Require status checks to pass before merging**
- [x] **Require branches to be up to date before merging**
- Required status checks (path-filtered, only enforce when relevant files change):
  - `ci-server / build-and-test` — required when `server/**` changes
  - `ci-flutter-cabin / build-and-test` — required when `cabin/**` changes
  - `ci-flutter-forklift / build-and-test` — required when `forklift/**` changes
  - `ci-db / lint-and-replay` — required when `db/**` changes

### Pull request reviews

- [x] **Require a pull request before merging**
- [x] **Require approvals** — at least 1 approving review
- [x] **Dismiss stale pull request approvals when new commits are pushed**
- [x] **Require review from Code Owners** — uses `.github/CODEOWNERS`

### Other

- [x] **Require linear history** (no merge commits — squash or rebase only)
- [x] **Require signed commits** (optional but recommended once team has GPG/SSH signing set up)
- [x] **Do not allow bypassing the above settings** — applies to admins too
- [x] **Restrict who can push to matching branches** — only allow PRs (no direct pushes)
- [x] **Allow force pushes** — **NO** (prevents history rewrites)
- [x] **Allow deletions** — **NO** (`main` cannot be deleted)

## Apply via `gh` CLI (optional)

The script `scripts/apply-branch-protection.sh` automates this. Requires a GitHub admin token (PAT or GitHub App) with `repo` and `admin:repo_hook` scopes.

```bash
GITHUB_TOKEN=ghp_xxx ./scripts/apply-branch-protection.sh goldbond/craines-tos
```

## Path-filtered status checks — caveat

GitHub branch-protection's "required status checks" require the named contexts to either pass OR not run. With path-filtered workflows (each CI workflow only triggers on specific paths), some PRs won't run all CI workflows — which is correct, but branch-protection may show those as "expected" rather than "passed".

The cleanest solution is the newer **"required workflows"** feature (organization-level setting that respects path filters) instead of repository-level "required status checks". Use that if your GitHub Enterprise plan supports it; otherwise the rules above are good enough — admins can merge after manually verifying the relevant workflows ran green.

## Testing the rules

1. Open a draft PR that touches only `server/**` — confirm only `ci-server` runs and is required to merge
2. Open a PR that touches `cabin/**` and `forklift/**` — confirm both Flutter workflows run
3. Open a PR that touches `db/migrations/` — confirm `ci-db` runs and the migration replay test executes
4. Try to push directly to `main` — should be rejected
5. Try to merge a PR with a failing required check — should be blocked
