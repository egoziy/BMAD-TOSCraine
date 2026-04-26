#!/usr/bin/env bash
# Apply branch protection rules to `main` per Story 1.3 / AC #5
# Usage: GITHUB_TOKEN=ghp_xxx ./scripts/apply-branch-protection.sh goldbond/craines-tos

set -euo pipefail

REPO="${1:?usage: $0 <owner/repo>}"

if [ -z "${GITHUB_TOKEN:-}" ]; then
  echo "ERROR: GITHUB_TOKEN env var must be set (PAT with repo + admin:repo_hook scopes)" >&2
  exit 1
fi

if ! command -v gh >/dev/null 2>&1; then
  echo "ERROR: gh CLI not installed (https://cli.github.com/)" >&2
  exit 1
fi

echo "Applying branch protection to $REPO main branch..."

gh api "repos/$REPO/branches/main/protection" \
  --method PUT \
  --header "Accept: application/vnd.github+json" \
  --field "required_status_checks[strict]=true" \
  --field "required_status_checks[contexts][]=ci-server / build-and-test" \
  --field "required_status_checks[contexts][]=ci-flutter-cabin / build-and-test" \
  --field "required_status_checks[contexts][]=ci-flutter-forklift / build-and-test" \
  --field "required_status_checks[contexts][]=ci-db / lint-and-replay" \
  --field "enforce_admins=true" \
  --field "required_pull_request_reviews[required_approving_review_count]=1" \
  --field "required_pull_request_reviews[dismiss_stale_reviews]=true" \
  --field "required_pull_request_reviews[require_code_owner_reviews]=true" \
  --field "required_linear_history=true" \
  --field "allow_force_pushes=false" \
  --field "allow_deletions=false" \
  --field "restrictions=null"

echo "✅ Branch protection applied to $REPO/main"
