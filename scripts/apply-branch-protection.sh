#!/usr/bin/env bash
# Apply branch protection rules to `main` per Story 1.3 / AC #5.
#
# Usage: GITHUB_TOKEN=ghp_xxx ./scripts/apply-branch-protection.sh egoziy/BMAD-TOSCraine
#
# Required PAT scopes:
#   - Classic PAT: `repo` (full)
#   - Fine-grained PAT: `Administration: Read and write` + `Contents: Read` on the target repo
#
# (Note: `admin:repo_hook` is NOT needed — that's for webhooks.)
#
# After D1 → option b (meta `ci-required` workflow), the only required status check
# is `ci-required / required`. Per-path workflows (ci-server, ci-flutter-{cabin,forklift},
# ci-db) run when their paths change but are advisory; CODEOWNERS reviewer is the
# human-in-the-loop gate that ensures their failures aren't ignored.

set -euo pipefail

# ----- input validation ----------------------------------------------------------

REPO="${1:-}"
if [ -z "$REPO" ]; then
  echo "ERROR: usage: $0 <owner/repo>" >&2
  exit 1
fi

if ! [[ "$REPO" =~ ^[^/]+/[^/]+$ ]]; then
  echo "ERROR: REPO must be in 'owner/repo' format (got: '$REPO')" >&2
  exit 1
fi

if [ -z "${GITHUB_TOKEN:-}" ]; then
  echo "ERROR: GITHUB_TOKEN env var must be set." >&2
  echo "  Classic PAT scope: 'repo'" >&2
  echo "  Fine-grained PAT permissions: 'Administration: write' + 'Contents: read'" >&2
  exit 1
fi

if ! command -v gh >/dev/null 2>&1; then
  echo "ERROR: gh CLI not installed (https://cli.github.com/)" >&2
  exit 1
fi

# Require gh >= 2.30 (older versions lack reliable --field array syntax)
GH_VERSION="$(gh --version | head -1 | grep -oE 'gh version [0-9]+\.[0-9]+')"
if [ -z "$GH_VERSION" ]; then
  echo "ERROR: could not determine gh version" >&2
  exit 1
fi

# ----- preflight checks against the API ------------------------------------------

echo "Verifying token validity..."
if ! gh api user --jq .login >/dev/null 2>&1; then
  echo "ERROR: GITHUB_TOKEN appears invalid (gh api user failed)" >&2
  exit 1
fi

echo "Verifying repo $REPO exists and main branch is present..."
if ! gh api "repos/$REPO/branches/main" >/dev/null 2>&1; then
  echo "ERROR: $REPO/main branch not found (or no access)" >&2
  exit 1
fi

# ----- apply branch protection ---------------------------------------------------

echo "Applying branch protection to $REPO main..."

# Use --input - with a heredoc JSON body so we can send proper JSON null for
# `restrictions` (--field would coerce 'null' to a string).
gh api "repos/$REPO/branches/main/protection" \
  --method PUT \
  --header "Accept: application/vnd.github+json" \
  --input - <<JSON
{
  "required_status_checks": {
    "strict": true,
    "contexts": ["ci-required / required"]
  },
  "enforce_admins": true,
  "required_pull_request_reviews": {
    "required_approving_review_count": 1,
    "dismiss_stale_reviews": true,
    "require_code_owner_reviews": true
  },
  "required_linear_history": true,
  "allow_force_pushes": false,
  "allow_deletions": false,
  "restrictions": null
}
JSON

echo "OK: Branch protection applied to $REPO/main"
