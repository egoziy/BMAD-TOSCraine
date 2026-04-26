# Deferred Work

This document tracks review findings that have been deferred — real issues but not actionable in the current sprint. Each entry lists the source review, the issue, and a deferral reason.

## Deferred from: code review of 1-3-github-actions-ci-workflows (2026-04-26)

- **W1: cabin/forklift have no pubspec.yaml yet** [`cabin/`, `forklift/`] — Workflows defined in Story 1.3 will fail when triggered (`flutter pub get` requires `pubspec.yaml`). Will land naturally when Stories 1.13 (cabin design system) and 1.18 (forklift design system) execute. Path filters mean the workflows don't trigger until those dirs have content, so this is non-blocking until then. Root cause: Story 1.1's Flutter scaffolding (Tasks 3+4) is blocked by Group Policy preventing `dart.exe` execution — see Story 1.1 deviation #6 + `docs/it-requests/dev-environment-request.md`.

- **W2: Two Flutter workflows are byte-identical** [`.github/workflows/ci-flutter-{cabin,forklift}.yml`] — Drift hazard: future version bumps will eventually be applied to one and not the other. Standard fix: refactor to a reusable `workflow_call` workflow with a `dir` input. Deferred because it's premature optimization for two files; revisit when team grows or when cabin and forklift diverge functionally.

- **W3: `dart format` runs before `pub get`** [`.github/workflows/ci-flutter-*.yml`] — Generated files (`*.g.dart`, freezed-output, l10n-generated) won't exist before `pub get` runs `build_runner`. For a project without code generators (current state), this ordering is fine. Deferred until first codegen-using story lands; revisit when adding `freezed`, `build_runner`, or `flutter gen-l10n`.

- **W4: `gh api` PUT for branch protection is non-atomic** [`scripts/apply-branch-protection.sh`] — A partial-failure mid-PUT could leave the repo with stale rules (e.g., status checks set, PR reviews not). Real but low-probability issue for a one-time setup script. Deferred; mitigation if needed: read existing rules first via GET, diff, then PUT only on drift, with idempotency.

- **W5: No `.sqlfluff` config** [`db/`] — `sqlfluff lint --dialect postgres` uses default rule set, which may reject valid SQL on first migration PR (Story 1.5). Deferred to Story 1.5 — that story's dev should add a `db/.sqlfluff` config calibrated to team style + the migration patterns the architecture specifies (CREATE TABLE conventions, naming, etc.).
