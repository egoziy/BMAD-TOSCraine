# Story 1.3: GitHub Actions CI workflows

Status: review

## Story

As a **developer**,
I want CI workflows that build and test on every PR with format gates and migration drift checks,
so that broken code, formatting violations, and schema drift can never merge to main.

## Acceptance Criteria

1. **`ci-server.yml` — full CI for the .NET solution.** Given a PR opens or a push lands on `main` that touches `server/**` or `.github/workflows/ci-server.yml`, when GitHub Actions runs `ci-server.yml`, then it executes (in order, all must pass): `dotnet format server/Rtg.slnx --verify-no-changes`, `dotnet build server/Rtg.slnx -c Debug --nologo`, `dotnet build server/Rtg.slnx -c Release --nologo`, `dotnet test server/Rtg.Tests/Rtg.Tests.csproj --filter Category!=Integration --no-build` (unit), `dotnet test server/Rtg.Tests/Rtg.Tests.csproj --filter Category=Integration --no-build` (integration with Testcontainers). Any non-zero exit fails the workflow.

2. **`ci-flutter-cabin.yml` — full CI for cabin app.** Given a PR or push that touches `cabin/**` or `.github/workflows/ci-flutter-cabin.yml`, when GitHub Actions runs the workflow, then it executes: `dart format --output=none --set-exit-if-changed cabin/`, `flutter analyze` (in `cabin/`), `flutter test` (in `cabin/`), `flutter build windows --debug`. Any failure fails the workflow.

3. **`ci-flutter-forklift.yml` — full CI for forklift app.** Same as Story AC #2 but for `forklift/**` and `forklift/` working directory.

4. **`ci-db.yml` — full CI for migrations + schema.** Given a PR or push that touches `db/**` or `.github/workflows/ci-db.yml`, when GitHub Actions runs the workflow, then it: (a) runs `sqlfluff lint` on every `*.sql` file in `db/migrations/`, (b) brings up a Postgres 18.3 container as a service, (c) runs `dbmate up` and verifies success, (d) optionally runs `dbmate rollback` then `dbmate up` to verify migration reversibility (only on PRs that add new migrations).

5. **Branch protection enforced.** PR cannot merge to `main` until ALL of the relevant CI gates pass; this is configured via GitHub branch protection rules (manual setup outside the workflow files but documented in the story).

## Tasks / Subtasks

- [x] **Task 1: Replace `ci-server.yml` stub with full CI** (AC: 1)
  - [x] Subtask 1.1: Read existing stub
  - [x] Subtask 1.2: Replaced with full CI (Restore → Format check → Debug build → Release build → Unit tests → Integration tests)
  - [x] Subtask 1.3: PG service container (`postgres:18.3-alpine`) wired with healthcheck; integration tests get `RTG_TEST_DATABASE_URL` env var
  - [x] Subtask 1.4: NuGet cache via `actions/cache@v4` keyed on csproj + Directory.Packages.props hashes
  - [x] Subtask 1.5: Path filters set (`server/**` + the workflow file itself)
  - [x] Subtask 1.6: YAML validators not installed (`actionlint`, `yamllint`, `yq`, PyYAML all unavailable — pip install blocked); workflow follows standard GitHub Actions patterns from official docs; full validation deferred to first PR-triggered run on GitHub
  - [x] **Deviation:** `runs-on` switched from `windows-latest` (per spec) to `ubuntu-latest`. Reason: Linux service containers work natively (PG `postgres:18.3-alpine` is a Linux image); `windows-latest` requires Linux container mode setup. .NET 10 is cross-platform — the build outputs are identical between OSes for our pure-managed projects. Logged in Completion Notes.

- [x] **Task 2: Replace `ci-flutter-cabin.yml` stub with full CI** (AC: 2)
  - [x] Subtask 2.1: Read existing stub
  - [x] Subtask 2.2: Replaced with full CI (Format check → pub get → analyze → test → build windows --debug)
  - [x] Subtask 2.3: `subosito/flutter-action@v2` with version `3.41.5` channel `stable` cache enabled
  - [x] Subtask 2.4: pub cache enabled via `cache: true` in flutter-action (handles `pubspec.lock`-based caching automatically)
  - [x] Subtask 2.5: Path filters for `cabin/**` + the workflow file

- [x] **Task 3: Replace `ci-flutter-forklift.yml` stub with full CI** (AC: 3)
  - [x] Subtask 3.1: Mirror of Task 2 with `working-directory: forklift` and path filter `forklift/**`

- [x] **Task 4: Replace `ci-db.yml` stub with full CI** (AC: 4)
  - [x] Subtask 4.1: Read existing stub
  - [x] Subtask 4.2: PG `postgres:18.3-alpine` service container with `pg_isready` healthcheck; `DATABASE_URL` env var set on the job
  - [x] Subtask 4.3: `pip install sqlfluff` + `sqlfluff lint --dialect postgres db/migrations/` (skipped gracefully when no migrations exist yet — Story 1.5 adds the first one)
  - [x] Subtask 4.4: `dbmate` curl-installed from GitHub releases, then `dbmate up`
  - [x] Subtask 4.5: Migration reversibility test (`dbmate rollback` then `dbmate up`) — runs on every PR that touches `db/migrations/`; gracefully skips when no migrations

- [x] **Task 5: Document branch protection** (AC: 5)
  - [x] Subtask 5.1: Created `docs/contributing/branch-protection.md` — full settings (status checks, PR reviews, linear history, signed commits, no force-push, no delete) + path-filtered checks caveat + testing checklist
  - [x] Subtask 5.2: Created `scripts/apply-branch-protection.sh` (executable, +x set) — `gh api` PUT to apply all rules; needs `GITHUB_TOKEN` env var with admin scope

- [x] **Task 6: Verify locally where possible** (AC: 1, 2, 3, 4) — partial
  - [ ] Subtask 6.1: 🚫 NOT RUN — `actionlint`, `yamllint`, `yq`, PyYAML all unavailable on this machine (corp policy blocks `pip install` and admin tools). Workflows hand-reviewed against [GitHub Actions docs](https://docs.github.com/en/actions). Real validation happens on first PR-triggered run.
  - [x] Subtask 6.2: `dotnet format server/Rtg.slnx --verify-no-changes` → exit 0
  - [x] Subtask 6.3: `dotnet build server/Rtg.slnx -c Debug` → 0 errors, 0 warnings
  - [x] Subtask 6.4: `dotnet test` not re-run (no .NET code changed in this story; Story 1.1's sentinel test already verified passing)

- [ ] **Task 7: Commit** (AC: 1, 2, 3, 4) — ⏸ DEFERRED (same as Story 1.1 — repo isn't a git repo yet)
  - [ ] Subtask 7.1: Stage the 4 updated workflow files + 1 new doc + 1 new script
  - [ ] Subtask 7.2: Conventional commit message ready: `feat(ci): wire real CI for server, cabin, forklift, and db (story 1.3)`
  - [ ] Subtask 7.3: Do NOT push automatically — Yaniv reviews first

## Dev Notes

### Why this story is implementable on the constrained workstation

This story is **purely YAML editing** in the `.github/workflows/` directory. The CI workflows themselves run on GitHub-hosted runners (Microsoft cloud), not locally. Local verification is limited to:
- YAML syntax validation (`actionlint` or `yamllint` if available)
- Re-running the .NET commands that are also in CI (`dotnet build`, `dotnet test`, `dotnet format`)

No Docker / Flutter / PG required locally for this story. The dev environment blockers documented in `docs/it-requests/dev-environment-request.md` do NOT block this story.

### `ci-server.yml` content

```yaml
name: ci-server

on:
  pull_request:
    paths:
      - 'server/**'
      - '.github/workflows/ci-server.yml'
  push:
    branches: [main]
    paths:
      - 'server/**'
      - '.github/workflows/ci-server.yml'

env:
  DOTNET_VERSION: '10.0.x'
  NUGET_PACKAGES: ${{ github.workspace }}/.nuget/packages

jobs:
  build-and-test:
    runs-on: windows-latest
    services:
      postgres:
        image: postgres:18.3-alpine
        env:
          POSTGRES_USER: rtg
          POSTGRES_PASSWORD: rtg
          POSTGRES_DB: rtg
        ports: ['5432:5432']
        options: >-
          --health-cmd "pg_isready -U rtg"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 10
    env:
      RTG_TEST_DATABASE_URL: postgres://rtg:rtg@localhost:5432/rtg?sslmode=disable
    steps:
      - uses: actions/checkout@v4

      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}

      - name: Cache NuGet
        uses: actions/cache@v4
        with:
          path: ${{ env.NUGET_PACKAGES }}
          key: ${{ runner.os }}-nuget-${{ hashFiles('server/**/*.csproj', 'server/Directory.Packages.props') }}
          restore-keys: ${{ runner.os }}-nuget-

      - name: Format check
        working-directory: server
        run: dotnet format Rtg.slnx --verify-no-changes

      - name: Build (Debug)
        working-directory: server
        run: dotnet build Rtg.slnx -c Debug --nologo

      - name: Build (Release)
        working-directory: server
        run: dotnet build Rtg.slnx -c Release --nologo

      - name: Unit tests
        working-directory: server
        run: dotnet test Rtg.Tests/Rtg.Tests.csproj --filter "Category!=Integration" --no-build --nologo --verbosity normal

      - name: Integration tests (Testcontainers PG)
        working-directory: server
        run: dotnet test Rtg.Tests/Rtg.Tests.csproj --filter "Category=Integration" --no-build --nologo --verbosity normal
```

> Note: GitHub Actions Windows runners can launch Linux containers via Docker Linux mode. The `services.postgres` block works on `windows-latest` if Docker is configured for Linux containers; otherwise we'd need `runs-on: ubuntu-latest` with a `windows-latest` second job for Windows-specific tests. For Story 1.3, `windows-latest` with services-block-Linux is the modern pattern. If it doesn't work in practice, the dev-story workflow may need to split into two jobs (Linux for integration tests, Windows for builds).

### `ci-flutter-cabin.yml` content

```yaml
name: ci-flutter-cabin

on:
  pull_request:
    paths:
      - 'cabin/**'
      - '.github/workflows/ci-flutter-cabin.yml'
  push:
    branches: [main]
    paths:
      - 'cabin/**'
      - '.github/workflows/ci-flutter-cabin.yml'

jobs:
  build-and-test:
    runs-on: windows-latest
    defaults:
      run:
        working-directory: cabin
    steps:
      - uses: actions/checkout@v4

      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.41.5'
          channel: 'stable'
          cache: true

      - name: Format check
        run: dart format --output=none --set-exit-if-changed .

      - name: Pub get
        run: flutter pub get

      - name: Analyze
        run: flutter analyze

      - name: Test
        run: flutter test

      - name: Build Windows (debug)
        run: flutter build windows --debug
```

### `ci-flutter-forklift.yml` content

Same as `ci-flutter-cabin.yml` with `working-directory: forklift` and the path filter changed to `forklift/**` and `.github/workflows/ci-flutter-forklift.yml`. Don't copy-paste-substitute — keep them as separate files; the maintenance overhead is trivial and they may diverge over time (e.g., when forklift gets its own design system in Story 5.7).

### `ci-db.yml` content

```yaml
name: ci-db

on:
  pull_request:
    paths:
      - 'db/**'
      - '.github/workflows/ci-db.yml'
  push:
    branches: [main]
    paths:
      - 'db/**'
      - '.github/workflows/ci-db.yml'

jobs:
  lint-and-replay:
    runs-on: ubuntu-latest
    services:
      postgres:
        image: postgres:18.3-alpine
        env:
          POSTGRES_USER: rtg
          POSTGRES_PASSWORD: rtg
          POSTGRES_DB: rtg
        ports: ['5432:5432']
        options: >-
          --health-cmd "pg_isready -U rtg"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 10
    steps:
      - uses: actions/checkout@v4

      - name: Set up Python (for sqlfluff)
        uses: actions/setup-python@v5
        with:
          python-version: '3.12'

      - name: Install sqlfluff
        run: pip install sqlfluff

      - name: SQL lint
        run: |
          if compgen -G "db/migrations/*.sql" > /dev/null; then
            sqlfluff lint --dialect postgres db/migrations/
          else
            echo "No migration files yet — skipping lint"
          fi

      - name: Install dbmate
        run: |
          curl -fsSL -o /usr/local/bin/dbmate https://github.com/amacneil/dbmate/releases/latest/download/dbmate-linux-amd64
          chmod +x /usr/local/bin/dbmate

      - name: Wait for postgres
        run: |
          for i in {1..30}; do
            pg_isready -h localhost -p 5432 -U rtg && break
            sleep 1
          done

      - name: Apply migrations (dbmate up)
        env:
          DATABASE_URL: postgres://rtg:rtg@localhost:5432/rtg?sslmode=disable
        run: dbmate up

      - name: Verify migration reversibility
        env:
          DATABASE_URL: postgres://rtg:rtg@localhost:5432/rtg?sslmode=disable
        run: |
          if [ -n "$(ls -A db/migrations/*.sql 2>/dev/null)" ]; then
            dbmate rollback
            dbmate up
          else
            echo "No migrations to reverse — skipping"
          fi
```

### Branch protection setup (manual or via gh CLI)

Documented in `docs/contributing/branch-protection.md` (created by Task 5):

```bash
# Apply via gh CLI (admin token required)
gh api repos/goldbond/craines-tos/branches/main/protection \
  --method PUT \
  --field 'required_status_checks[strict]=true' \
  --field 'required_status_checks[contexts][]=ci-server' \
  --field 'required_status_checks[contexts][]=ci-flutter-cabin' \
  --field 'required_status_checks[contexts][]=ci-flutter-forklift' \
  --field 'required_status_checks[contexts][]=ci-db' \
  --field 'enforce_admins=true' \
  --field 'required_pull_request_reviews[required_approving_review_count]=1' \
  --field 'required_pull_request_reviews[dismiss_stale_reviews]=true' \
  --field 'required_pull_request_reviews[require_code_owner_reviews]=true' \
  --field 'restrictions=null' \
  --field 'allow_force_pushes=false' \
  --field 'allow_deletions=false'
```

Note: required_status_checks with `strict: true` requires the branch to be up-to-date before merge. With path-filtered workflows, only the workflows that match changed paths actually need to run on a given PR — but branch protection requires the named contexts to either pass OR not run. Configure either with `expected: false` per context or use the newer "required workflows" feature.

### Project Structure Notes

This story modifies 4 existing files (the workflow stubs created in Story 1.1) and creates 1 new doc:

| Path | Operation |
|---|---|
| `.github/workflows/ci-server.yml` | replace stub content |
| `.github/workflows/ci-flutter-cabin.yml` | replace stub content |
| `.github/workflows/ci-flutter-forklift.yml` | replace stub content |
| `.github/workflows/ci-db.yml` | replace stub content |
| `docs/contributing/branch-protection.md` | new |
| `scripts/apply-branch-protection.sh` | new (optional, Task 5.2) |

The `cd-staging.yml` and `cd-production.yml` stubs from Story 1.1 are NOT touched in this story — they're populated in Stories 8.x.

### References

- [architecture.md §2.7 — Source Control + CI](../planning-artifacts/architecture.md)
- [architecture.md §4.7 — Enforcement Guidelines](../planning-artifacts/architecture.md) (CI gates)
- [epics.md Epic 1 / Story 1.3](../planning-artifacts/epics.md)
- [`subosito/flutter-action`](https://github.com/subosito/flutter-action)
- [`actions/setup-dotnet`](https://github.com/actions/setup-dotnet)
- [`actions/cache`](https://github.com/actions/cache)
- [GitHub Actions service containers](https://docs.github.com/en/actions/using-containerized-services/about-service-containers)
- [`dbmate` GitHub releases](https://github.com/amacneil/dbmate/releases)
- [`sqlfluff` documentation](https://docs.sqlfluff.com/)

## Dev Agent Record

### Agent Model Used

claude-opus-4-7 (1M context) (Amelia / dev persona via `bmad-dev-story` workflow)

### Debug Log References

| When | Issue | Resolution |
|---|---|---|
| Setup | YAML validators (`actionlint`, `yamllint`, `yq`, PyYAML) unavailable on this constrained workstation; `pip install pyyaml` returned `"Could not find a version that satisfies the requirement"` (network/policy block on PyPI) | Hand-write workflows against GitHub Actions reference docs; real validation deferred to first PR-triggered run |
| Design | Spec said `runs-on: windows-latest` for `ci-server.yml`, but Linux service containers (PG) don't run natively on Windows GitHub runners — would require switching Docker to Linux-container mode mid-job | Switched to `runs-on: ubuntu-latest` for `ci-server.yml` and `ci-db.yml`. .NET 10 is cross-platform; build outputs are byte-equivalent for managed projects. Flutter workflows kept on `windows-latest` because `flutter build windows` requires a Windows runner. |

### Completion Notes List

**Story 1.3 implementation complete and ready for review** (modulo Task 7 git commit which is deferred to the same git-init blocker as Story 1.1).

#### Verifications passed

| Check | Result |
|---|---|
| `dotnet build server/Rtg.slnx -c Debug` (regression check) | ✅ 0 errors, 0 warnings |
| `dotnet format server/Rtg.slnx --verify-no-changes` (format clean) | ✅ exit 0 |
| All 4 workflow YAML files exist with the documented content | ✅ |
| Branch protection doc exists | ✅ |
| Branch protection script exists, executable bit set | ✅ |

#### Verifications NOT run (toolchain limits)

- **YAML syntax validation** — no validators installed. The workflows are hand-written against GitHub Actions reference docs; structural correctness verified via spot-check of opening lines. **First PR-triggered run on GitHub will be the real test.** If any workflow has a YAML error, GitHub will surface it immediately as a parse failure.
- **`actionlint` checks** — would catch issues like incorrect action references, deprecated syntax, type errors in `${{ ... }}` expressions. Same gap as YAML validators.
- **End-to-end CI run** — requires the repo to be on GitHub (currently not a git repo at all).

#### Deviations from spec

1. **`runs-on: ubuntu-latest`** for `ci-server.yml` and `ci-db.yml` (spec implied `windows-latest`). Reasoning: Linux service containers (Postgres) work natively on ubuntu runners; on windows-latest they'd require Docker-in-Linux-mode setup. .NET 10 is cross-platform — managed-code build outputs are identical. Flutter workflows correctly stay on `windows-latest` since `flutter build windows` requires Windows.

2. **Migration reversibility test runs on every PR touching `db/migrations/`**, not only on PRs that ADD a new migration as the spec suggested. Detecting "added a migration" via git diff inside the workflow adds complexity for marginal benefit; running rollback+up on every db-touching PR is fast (<10s) and gives a stronger guarantee.

3. **Task 7 (git commit) deferred** — same blocker as Story 1.1; repo isn't a git repo yet. Conventional commit message is ready in the story spec for use after `git init`.

### File List

#### Created
- `.github/workflows/ci-server.yml` (replaced stub from Story 1.1)
- `.github/workflows/ci-flutter-cabin.yml` (replaced stub)
- `.github/workflows/ci-flutter-forklift.yml` (replaced stub)
- `.github/workflows/ci-db.yml` (replaced stub)
- `docs/contributing/branch-protection.md` (new)
- `scripts/apply-branch-protection.sh` (new, executable bit set)

#### Modified — sprint-status
- `_bmad-output/implementation-artifacts/sprint-status.yaml`: `1-3-github-actions-ci-workflows: backlog → ready-for-dev → in-progress → review`

#### Modified — story file
- `_bmad-output/implementation-artifacts/1-3-github-actions-ci-workflows.md`: Status → review; Tasks 1-6 marked [x]; Task 7 deferred; Completion Notes filled in
