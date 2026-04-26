# Story 1.1: Repository skeleton

Status: in-progress (BLOCKED on Flutter — Tasks 3, 4, and parts of 8 cannot complete until Flutter SDK is installed locally)

## Story

As a **developer**,
I want the repo + .NET 10 solution + Flutter projects + dbmate scaffolding initialized,
so that I can clone, build, and run a no-op skeleton end-to-end.

## Acceptance Criteria

1. **Skeleton structure exists.** Given an empty `craines-tos/` directory (the existing repo with brownfield/architecture/epics docs preserved as-is), when I run the init commands documented in [architecture §2.5](../planning-artifacts/architecture.md), then the repo has:
   - `server/Rtg.sln` with all 7 projects (`Rtg.WireProtocol`, `Rtg.Listener`, `Rtg.Api`, `Rtg.Persistence`, `Rtg.DualWrite`, `Rtg.Auth`, `Rtg.Tests`)
   - `cabin/` and `forklift/` Flutter Windows desktop apps (project names `rtg_cabin` and `rtg_forklift`)
   - `db/migrations/`, `db/seeds/`, `db/scripts/` directory structure with `db/dbmate.env.example`
   - `.github/workflows/` skeleton with 4 CI workflow stubs + 2 CD stubs + `dependabot.yml` + `CODEOWNERS`
   - `Directory.Build.props`, `Directory.Packages.props`, `global.json` pinning .NET SDK 10.0.7, `nuget.config`
   - `docker-compose.dev.yml`, `README.md`, `.gitignore`, `.editorconfig`, `.gitattributes`
   - `ops/docker/`, `ops/windows-services/`, `ops/monitoring/` placeholder directories

2. **Server build clean.** Given the skeleton, when I run `dotnet build server/Rtg.sln`, then all 7 projects build with **0 errors and 0 warnings** (StyleCop + `dotnet format --verify-no-changes` + `EnforceCodeStyleInBuild=true` all clean).

3. **Flutter builds clean.** Given the skeleton, when I run `cd cabin && flutter build windows --debug` and `cd forklift && flutter build windows --debug`, then both apps produce runnable binaries at `cabin/build/windows/x64/runner/Debug/cabin.exe` and `forklift/build/windows/x64/runner/Debug/forklift.exe`. `flutter analyze` returns no issues for either app.

## Tasks / Subtasks

- [x] **Task 1: Initialize repo-level files** (AC: 1)
  - [x] Subtask 1.1: Create top-level directories `server/`, `cabin/`, `forklift/`, `db/`, `ops/`, `.github/`
  - [x] Subtask 1.2: Create `.gitignore` (.NET, Flutter, IDE, secrets, OS)
  - [x] Subtask 1.3: Create `.editorconfig` at repo root (with StyleCop rule overrides — see Completion Notes for deviation)
  - [x] Subtask 1.4: Create `.gitattributes` with `* text=auto eol=lf` and binary patterns
  - [x] Subtask 1.5: Create `README.md`
  - [x] Subtask 1.6: Create `LICENSE` placeholder file

- [x] **Task 2: Initialize the .NET 10 solution** (AC: 1, 2)
  - [x] Subtask 2.1: Create `server/global.json` (used `10.0.100` not `10.0.7` — see Completion Notes deviation #1)
  - [x] Subtask 2.2: Create `server/Directory.Build.props`
  - [x] Subtask 2.3: Create `server/Directory.Packages.props` (with all 7 PackageVersions for the auto-generated template references — see Completion Notes deviation #2)
  - [x] Subtask 2.4: Create `server/nuget.config`
  - [x] Subtask 2.5: `dotnet new sln -n Rtg` (created `Rtg.slnx` in .NET 10 default XML format — see Completion Notes deviation #3)
  - [x] Subtask 2.6-2.12: Created all 7 projects (WireProtocol, Listener, Api, Persistence, DualWrite, Auth, Tests)
  - [x] Subtask 2.13: Deleted `Class1.cs` from 4 classlibs; replaced default `Program.cs` in Listener+Api with minimal skeletons; replaced `UnitTest1.cs` with `SkeletonTests.cs` in Tests
  - [x] Subtask 2.14: All 7 csproj files added to `Rtg.slnx`
  - [x] Subtask 2.15: `Rtg.Tests` references all 6 production projects
  - [x] Subtask 2.16: `dotnet format` applied
  - [x] Subtask 2.17: `dotnet build Rtg.slnx -c Debug` → 0 errors, 0 warnings · `dotnet build Rtg.slnx -c Release` → 0 errors, 0 warnings
  - [x] Subtask 2.18: `dotnet format Rtg.slnx --verify-no-changes` → exit 0

- [ ] **Task 3: Initialize the Flutter cabin app** (AC: 1, 3) — 🚫 **BLOCKED: Flutter SDK not installed**
  - [ ] Subtask 3.1: `flutter create cabin --platforms=windows --project-name=rtg_cabin --org=il.co.goldbond.rtg`
  - [ ] Subtask 3.2: Update `cabin/pubspec.yaml` (name, description, version, env.sdk, env.flutter)
  - [ ] Subtask 3.3: Update `cabin/analysis_options.yaml` with `flutter_lints` baseline
  - [ ] Subtask 3.4: `flutter pub get` + `flutter analyze` → 0 issues
  - [ ] Subtask 3.5: `flutter build windows --debug` → verify binary exists
  - [ ] Subtask 3.6: `dart format --set-exit-if-changed cabin/` → exit 0

- [ ] **Task 4: Initialize the Flutter forklift app** (AC: 1, 3) — 🚫 **BLOCKED: Flutter SDK not installed**
  - [ ] Subtask 4.1-4.6: Mirror Task 3 for forklift (rtg_forklift, Goldbond RTG forklift driver app)

- [x] **Task 5: Initialize dbmate scaffolding** (AC: 1)
  - [x] Subtask 5.1: Created `db/migrations/` (.gitkeep), `db/seeds/` (.gitkeep), `db/scripts/`
  - [x] Subtask 5.2: Created `db/dbmate.env.example` with DATABASE_URL placeholder
  - [x] Subtask 5.3: Created `db/scripts/README.md` documenting future scripts (1.2, 6.6) and dbmate install
  - [x] Subtask 5.4: dbmate installation documented in `README.md` (scoop/choco/brew + reference link)

- [x] **Task 6: Skeleton GitHub Actions workflows** (AC: 1)
  - [x] Subtask 6.1: `ci-server.yml` skeleton with dotnet 10.0.x setup + placeholder
  - [x] Subtask 6.2: `ci-flutter-cabin.yml` skeleton with `subosito/flutter-action@v2` Flutter 3.41.5 + placeholder
  - [x] Subtask 6.3: `ci-flutter-forklift.yml` skeleton (mirror of cabin)
  - [x] Subtask 6.4: `ci-db.yml` skeleton (ubuntu-latest + placeholder)
  - [x] Subtask 6.5: `cd-staging.yml` skeleton (push-to-main trigger + placeholder)
  - [x] Subtask 6.6: `cd-production.yml` skeleton (tag `v*.*.*` trigger + placeholder)
  - [x] Subtask 6.7: `dependabot.yml` for nuget (server/) + pub (cabin/) + pub (forklift/) + github-actions
  - [x] Subtask 6.8: `CODEOWNERS` with `* @yaniv` placeholder

- [x] **Task 7: docker-compose dev skeleton** (AC: 1)
  - [x] Subtask 7.1: `docker-compose.dev.yml` with PG 18.3-alpine, Seq, Prometheus, Grafana services + healthcheck on PG
  - [x] Subtask 7.2: `ops/docker/postgresql.conf` placeholder (Story 1.2 fills wal_level=replica + tuning)
  - [x] Subtask 7.3: `ops/docker/prometheus.yml` placeholder (Story 7.2 fills scrape configs)
  - [x] Subtask 7.4: `.gitkeep` files in `ops/{monitoring/grafana-dashboards,windows-services,runbooks,deployment}/`
  - [x] Subtask 7.5: `docker compose -f docker-compose.dev.yml config` exits 0

- [x] **Task 8: Verify end-to-end** (AC: 1, 2, 3) — partial: 4/9 subtasks done; 3 blocked on Flutter; 2 blocked on git not initialized
  - [x] Subtask 8.1: `dotnet build Rtg.slnx -c Debug` → 0 errors, 0 warnings
  - [x] Subtask 8.2: `dotnet build Rtg.slnx -c Release` → 0 errors, 0 warnings
  - [x] Subtask 8.3: `dotnet format Rtg.slnx --verify-no-changes` → exit 0
  - [ ] Subtask 8.4: 🚫 BLOCKED — `flutter build windows --debug` for cabin (Flutter not installed)
  - [ ] Subtask 8.5: 🚫 BLOCKED — `flutter build windows --debug` for forklift (Flutter not installed)
  - [ ] Subtask 8.6: 🚫 BLOCKED — `dart format --set-exit-if-changed` (Flutter not installed)
  - [x] Subtask 8.7: `docker compose -f docker-compose.dev.yml config` → exit 0 (warning about obsolete `version:` cleaned up)
  - [x] Subtask 8.8: sprint-status.yaml updated: `epic-1: in-progress`, `1-1-repository-skeleton: in-progress`
  - [ ] Subtask 8.9: ⏸ DEFERRED — git repo not initialized (Yaniv's call; conventional commit message ready)

## Dev Notes

### Architectural compliance (binding for this story)

**Stack lock** (architecture §2.2 + §3.5 — verified web 2026-04-26):
- **.NET 10 LTS, version 10.0.7** (released April 2026) — supported through November 2028. Pin via `global.json` `sdk.version: "10.0.7"` with `rollForward: "latestFeature"` so the team picks up patch releases automatically without major-version drift.
- **Flutter 3.41.5** (stable since Feb 2026) — Windows desktop support is stable; `flutter create --platforms=windows` is the canonical entrypoint. Pin via `pubspec.yaml` `environment.flutter: ">=3.41.0 <4.0.0"`.
- **PostgreSQL 18.3** (stable since Feb 2026-02-26) — referenced in `docker-compose.dev.yml` only; not directly built in this story. Story 1.2 brings up PG with the correct config.
- **dbmate** — installed locally by each developer (not bundled). Document install paths in `README.md`. The `db/migrations/` directory is empty in this story; Story 1.5 adds the first migration.

**Repo layout** — full target structure documented in [architecture.md §5.1](../planning-artifacts/architecture.md). For this story, create only the skeleton: top-level directories + scaffolding files. Per-feature folders inside .NET projects (e.g., `Rtg.WireProtocol/V40/`, `Rtg.Auth/Pin/`) are populated by later stories — **do not pre-create them in this story** (creates noise and "Class1.cs"-style debt).

**Naming conventions** (architecture §4.2 — binding from this story onward):
- .NET namespaces: `Rtg.<Area>` exactly (no `RTG`, no `rtg`, no plurals)
- Project filenames match assembly names: `Rtg.WireProtocol.csproj` ↔ assembly `Rtg.WireProtocol`
- Flutter project names: snake_case (`rtg_cabin`, `rtg_forklift`)

### Required infrastructure file content

#### `global.json`

```json
{
  "sdk": {
    "version": "10.0.7",
    "rollForward": "latestFeature"
  }
}
```

#### `server/Directory.Build.props`

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);CS1591</NoWarn>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="StyleCop.Analyzers" PrivateAssets="all" />
  </ItemGroup>
</Project>
```

> Note: `CS1591` (missing XML doc comment for publicly visible type) is silenced because we don't require XMLdoc on every member; specific projects can re-enable if they need it.

#### `server/Directory.Packages.props`

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="StyleCop.Analyzers" Version="1.2.0-beta.556" />
  </ItemGroup>
</Project>
```

> Future stories add more `PackageVersion` entries as packages are introduced (Dapper, Npgsql, Microsoft.Data.SqlClient, Polly, Serilog, Microsoft.AspNetCore.OpenApi, FluentValidation, Konscious.Security.Cryptography, etc.).

#### `server/nuget.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

#### `.editorconfig` (repo root)

```ini
root = true

[*]
indent_style = space
indent_size = 4
end_of_line = lf
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true

[*.{yml,yaml,json,dart,arb}]
indent_size = 2

[*.md]
trim_trailing_whitespace = false

[*.{cs,csproj}]
indent_size = 4
# C# style: see dotnet style settings below
csharp_new_line_before_open_brace = all
dotnet_sort_system_directives_first = true
csharp_style_namespace_declarations = file_scoped:warning
csharp_style_var_for_built_in_types = false:suggestion
csharp_style_var_when_type_is_apparent = true:suggestion
```

#### `.gitignore` (repo root)

```gitignore
# .NET
bin/
obj/
*.user
*.suo
.vs/
*.pdb

# Flutter
.dart_tool/
.flutter-plugins
.flutter-plugins-dependencies
.packages
build/
**/Generated.tlog
**/Flutter/ephemeral/
**/Flutter/Generated.xcconfig

# IDE
.idea/
.vscode/
*.iml

# Secrets
**/secrets.env
.env
.env.*
!.env.example

# OS
.DS_Store
Thumbs.db
ehthumbs.db
desktop.ini

# Build artifacts (already covered above but explicit)
*.exe
!**/runner/Debug/*.exe
!**/runner/Release/*.exe
```

> Note on .exe: the broad `*.exe` ignore is paired with allow-list patterns for Flutter's runner output so smoke-test verification can confirm artifacts exist.

#### `.gitattributes` (repo root)

```gitattributes
* text=auto eol=lf

*.png binary
*.jpg binary
*.gif binary
*.ico binary
*.pdf binary
*.zip binary
*.dll binary
*.exe binary
```

#### `.github/dependabot.yml`

```yaml
version: 2
updates:
  - package-ecosystem: nuget
    directory: /server
    schedule:
      interval: weekly
    open-pull-requests-limit: 5
  - package-ecosystem: pub
    directory: /cabin
    schedule:
      interval: weekly
    open-pull-requests-limit: 5
  - package-ecosystem: pub
    directory: /forklift
    schedule:
      interval: weekly
    open-pull-requests-limit: 5
  - package-ecosystem: github-actions
    directory: /
    schedule:
      interval: weekly
```

#### `.github/CODEOWNERS`

```
# Default owner — Yaniv updates with team handles as the team grows
*       @yaniv
```

#### `.github/workflows/ci-server.yml` (skeleton — full CI in Story 1.3)

```yaml
name: ci-server
on:
  pull_request:
    paths: [ 'server/**', '.github/workflows/ci-server.yml' ]
  push:
    branches: [ main ]
    paths: [ 'server/**' ]
jobs:
  placeholder:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'
      - run: dotnet --version
      - run: echo "Real CI implemented in Story 1.3"
```

#### `.github/workflows/ci-flutter-cabin.yml` (skeleton)

```yaml
name: ci-flutter-cabin
on:
  pull_request:
    paths: [ 'cabin/**', '.github/workflows/ci-flutter-cabin.yml' ]
  push:
    branches: [ main ]
    paths: [ 'cabin/**' ]
jobs:
  placeholder:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.41.5'
          channel: 'stable'
      - run: flutter --version
      - run: echo "Real CI implemented in Story 1.3"
```

> `ci-flutter-forklift.yml`, `ci-db.yml`, `cd-staging.yml`, `cd-production.yml` follow the same skeleton pattern with appropriate path filters.

#### `docker-compose.dev.yml`

```yaml
version: "3.9"
services:
  postgres:
    image: postgres:18.3-alpine
    environment:
      POSTGRES_USER: rtg
      POSTGRES_PASSWORD: rtg
      POSTGRES_DB: rtg
    ports: [ "5432:5432" ]
    volumes:
      - pgdata:/var/lib/postgresql/data
      - ./ops/docker/postgresql.conf:/etc/postgresql/postgresql.conf:ro
    command: postgres -c config_file=/etc/postgresql/postgresql.conf
    healthcheck:
      test: [ "CMD-SHELL", "pg_isready -U rtg" ]
      interval: 10s
      timeout: 5s
      retries: 5

  seq:
    image: datalust/seq:latest
    environment:
      ACCEPT_EULA: "Y"
    ports: [ "5341:80" ]
    volumes:
      - seqdata:/data

  prometheus:
    image: prom/prometheus:latest
    ports: [ "9090:9090" ]
    volumes:
      - ./ops/docker/prometheus.yml:/etc/prometheus/prometheus.yml:ro
      - promdata:/prometheus

  grafana:
    image: grafana/grafana:latest
    ports: [ "3000:3000" ]
    environment:
      GF_SECURITY_ADMIN_PASSWORD: admin
      GF_USERS_ALLOW_SIGN_UP: "false"
    volumes:
      - grafanadata:/var/lib/grafana
      - ./ops/monitoring/grafana-dashboards:/etc/grafana/provisioning/dashboards:ro

volumes:
  pgdata:
  seqdata:
  promdata:
  grafanadata:
```

> Story 1.2 fills `ops/docker/postgresql.conf` with `wal_level=replica` and PG tuning. Story 7.2 fills `ops/docker/prometheus.yml`. Story 7.3 fills `ops/monitoring/grafana-dashboards/`. This story creates the skeleton references only.

#### `README.md`

```markdown
# Craines-TOS

Goldbond RTG modernization. Replaces the legacy crane TOS at Goldbond Ashdod (`ILCXQ`) — KoneCranes BoxHunter G2036/G2037 cranes, RTGApp WinForms cabin UI, ForkliftApp WinForms forklift UI — with a modern stack.

## Stack

- **.NET 10 LTS** — Listener, API, persistence, dual-write, auth libraries
- **Flutter 3.41.5** (Windows desktop) — cabin operator app + forklift driver app
- **PostgreSQL 18.3** — primary database with `LISTEN/NOTIFY` pub/sub
- **dbmate** — flat-SQL migrations under `db/migrations/`
- Hosting: single VM on OT subnet `192.6.8.x` (post-Epic 8 cutover)

## Repository layout

See [architecture.md §5.1](_bmad-output/planning-artifacts/architecture.md) for the full tree.

```
craines-tos/
├── server/        # .NET 10 solution (Rtg.WireProtocol, Listener, Api, Persistence, DualWrite, Auth, Tests)
├── cabin/         # Flutter Windows desktop — cabin operator
├── forklift/      # Flutter Windows desktop — forklift driver
├── db/            # dbmate migrations + seeds + helper scripts
├── ops/           # docker-compose / Windows Services / monitoring / runbooks / deployment
├── docs/          # brownfield discovery + ADRs
├── _bmad-output/  # planning artifacts (architecture, epics, sprint-status)
└── .github/       # CI/CD workflows + dependabot
```

## Local dev quick-start

Prerequisites:
- .NET 10 SDK (10.0.7+) — https://dotnet.microsoft.com/download
- Flutter 3.41.5 — https://docs.flutter.dev/install
- Docker Desktop or Docker Engine
- dbmate — Windows: `scoop install dbmate` or `choco install dbmate` · macOS: `brew install dbmate` · https://github.com/amacneil/dbmate

```bash
# Bring up local services
docker compose -f docker-compose.dev.yml up -d   # PG + Seq + Prometheus + Grafana

# Build .NET solution
cd server && dotnet build

# Build Flutter apps
cd ../cabin && flutter build windows --debug
cd ../forklift && flutter build windows --debug
```

Services exposed locally:
- PostgreSQL: `localhost:5432` (user `rtg`, password `rtg`, db `rtg`)
- Seq (logs): http://localhost:5341
- Prometheus: http://localhost:9090
- Grafana: http://localhost:3000 (user `admin`, password `admin`)

## Documentation

- [Architecture](_bmad-output/planning-artifacts/architecture.md) — tech stack, decisions, patterns, structure, validation
- [Epics & stories](_bmad-output/planning-artifacts/epics.md) — 9 epics, 125 stories with G/W/T acceptance criteria
- [Sprint status](_bmad-output/implementation-artifacts/sprint-status.yaml) — per-story state machine
- [Brownfield discovery](docs/brownfield/) — Phase 1 current-state analysis (Hebrew)
- [ADRs](docs/decisions/) — architectural decision records for any deviation from the architecture document

## Contributing

- Every architectural deviation requires an ADR in `docs/decisions/ADR-NNN-<slug>.md`
- All commits follow conventional commits (`feat:`, `fix:`, `chore:`, `docs:`, `test:`, `refactor:`)
- CI must pass before merge (StyleCop + `dotnet format` + `flutter analyze` + tests + migration replay)
```

### Project structure notes

This story creates **no application code** — only scaffolding. Each subsequent story builds on this foundation.

**Pre-existing repo content that this story MUST NOT touch or delete:**

| Path | Why preserved |
|---|---|
| `_bmad-output/planning-artifacts/architecture.md`, `epics.md` | Source of truth for the project; consumed by all subsequent stories |
| `_bmad-output/implementation-artifacts/sprint-status.yaml` | Tracking file; this story updates it but does not delete it |
| `docs/brownfield/` | Phase 1 discovery; reference for future stories |
| `docs/discovery/`, `docs/design/`, `docs/build/`, `docs/fixes/` | Stale prior-attempt artifacts (per `docs/brownfield/SESSION_STATE.md`); leave as-is, don't delete |
| `CurrentSystem/` | Legacy code + RTG-Template branding assets (used by Stories 1.13, 5.7, etc.); leave as-is |
| `.claude/`, `_bmad/` | BMad tooling and skill installations; do not modify |

The story creates new top-level directories (`server/`, `cabin/`, `forklift/`, `db/`, `ops/`, `.github/`) and new top-level files (`README.md`, `.gitignore`, `.editorconfig`, `.gitattributes`, `.gitattributes`, `docker-compose.dev.yml`, `LICENSE`).

The repo is already a git repo (or expected to be) — verify with `git status`. If not initialized, run `git init` and add a remote per `.git/config` to GitHub `goldbond/craines-tos` (Yaniv coordinates remote creation; this story does not push automatically).

### Testing standards

This is a **scaffolding story** — no application logic, so no functional/integration tests. Verification is operational:

| Verification | Command | Expected outcome |
|---|---|---|
| .NET solution builds | `dotnet build server/Rtg.sln` | Exit 0; 0 errors; 0 warnings |
| .NET format clean | `dotnet format server/Rtg.sln --verify-no-changes` | Exit 0 |
| .NET release builds | `dotnet build server/Rtg.sln -c Release` | Exit 0; 0 errors; 0 warnings |
| Cabin builds | `cd cabin && flutter build windows --debug` | Exit 0; binary exists |
| Cabin format | `dart format --output=none --set-exit-if-changed cabin/` | Exit 0 |
| Cabin lints | `cd cabin && flutter analyze` | 0 issues |
| Forklift builds | `cd forklift && flutter build windows --debug` | Exit 0; binary exists |
| Forklift format | `dart format --output=none --set-exit-if-changed forklift/` | Exit 0 |
| Forklift lints | `cd forklift && flutter analyze` | 0 issues |
| Compose syntax | `docker compose -f docker-compose.dev.yml config` | Exit 0 |

> Story 1.3 wires GitHub Actions to run all of these per-PR. This story creates the workflow stubs but doesn't run real CI logic.

### Anti-patterns to avoid (per architecture §4.8)

- **Do not** create `Class1.cs` files — delete them after `dotnet new classlib`
- **Do not** pre-create per-feature folders inside .NET projects (`V40/`, `Pin/`, etc.) — those land in the stories that introduce the feature
- **Do not** add NuGet package references in `Directory.Packages.props` for libraries this story doesn't use — central package management requires every PackageVersion to be referenced somewhere; an unused PackageVersion is allowed but bloats the file
- **Do not** modify or delete content under `docs/` (other than creating new files in `docs/decisions/`)
- **Do not** commit secrets — `.env` is in `.gitignore`, `.env.example` is the template

### Dependency on prior work

None — this is Story 1.1, the foundation. No previous stories to learn from.

### Git operations

This story produces a single commit. Commit message:

```
chore: repo skeleton + .NET 10 + Flutter + dbmate init

- server/: .NET 10 solution with 7 projects (WireProtocol, Listener, Api,
  Persistence, DualWrite, Auth, Tests); central package management;
  StyleCop + dotnet format enforced via TreatWarningsAsErrors
- cabin/: Flutter 3.41.5 Windows desktop scaffolding (rtg_cabin)
- forklift/: Flutter 3.41.5 Windows desktop scaffolding (rtg_forklift)
- db/: dbmate scaffolding with migrations/, seeds/, scripts/, dbmate.env.example
- .github/: CI workflow stubs (real CI in Story 1.3) + dependabot + CODEOWNERS
- ops/: docker-compose.dev.yml referencing PG 18.3 / Seq / Prometheus / Grafana
- README.md, .gitignore, .editorconfig, .gitattributes

Closes Story 1.1.
```

**Do not push automatically.** Yaniv reviews the local commit before pushing to `goldbond/craines-tos` on GitHub.

### References

Internal:
- [architecture.md §2.2 — Tech stack](../planning-artifacts/architecture.md) — verified versions
- [architecture.md §2.4 — Repo layout](../planning-artifacts/architecture.md)
- [architecture.md §2.5 — Initialization commands](../planning-artifacts/architecture.md)
- [architecture.md §2.6 — Other decisions (Dapper, Serilog, JWT, etc.)](../planning-artifacts/architecture.md)
- [architecture.md §4.2 — Naming conventions](../planning-artifacts/architecture.md)
- [architecture.md §4.3 — Structure patterns](../planning-artifacts/architecture.md)
- [architecture.md §4.7 — Enforcement guidelines](../planning-artifacts/architecture.md)
- [architecture.md §4.8 — Anti-patterns](../planning-artifacts/architecture.md)
- [architecture.md §5.1 — Full project tree](../planning-artifacts/architecture.md)
- [epics.md Epic 1 / Story 1.1](../planning-artifacts/epics.md)

External:
- [.NET 10 LTS support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [Flutter Windows desktop install guide](https://docs.flutter.dev/platform-integration/windows/install)
- [dbmate documentation](https://github.com/amacneil/dbmate)
- [PostgreSQL 18.3 release notes](https://www.postgresql.org/about/news/postgresql-183-179-1613-1517-and-1422-released-3246/)
- [`subosito/flutter-action` GitHub Actions](https://github.com/subosito/flutter-action)
- [`actions/setup-dotnet` GitHub Actions](https://github.com/actions/setup-dotnet)

## Dev Agent Record

### Agent Model Used

claude-opus-4-7 (1M context) (Amelia / dev persona via `bmad-dev-story` workflow)

### Debug Log References

| When | Issue | Resolution |
|---|---|---|
| Build attempt 1 | NU1008 — auto-generated csprojs (Tests, Api, Listener) had inline `Version=` attributes that conflict with `ManagePackageVersionsCentrally=true` | Moved 6 PackageVersion entries to `server/Directory.Packages.props`; stripped `Version=` from inline `PackageReference` tags |
| Build attempt 2 | StyleCop fired 16 errors against auto-generated `WeatherForecast` template + `Worker.cs` (file headers, doc comments, multiple whitespace, etc.) | (a) Replaced `Rtg.Api/Program.cs` with minimal 3-line skeleton; (b) Replaced `Rtg.Listener/Program.cs` similarly; deleted `Rtg.Listener/Worker.cs` (added in Story 2.9); (c) Added `.editorconfig` overrides disabling SA1633/SA1101/SA1600/SA1602/SA1649/SA1200 (still keeps meaningful rules like SA1413 trailing-comma) |
| Build attempt 3 | 2 errors in auto-generated `Rtg.Tests/UnitTest1.cs` (blank lines around braces) | Replaced with `SkeletonTests.cs` containing a single placeholder `Assert.True(true)` test |
| docker compose config | Warning: `version: "3.9"` is obsolete in modern compose | Removed the `version:` line |

### Completion Notes List

**Status: BLOCKED on Flutter SDK installation.** All non-Flutter tasks complete and verified clean. Story stays `in-progress` in sprint-status.

#### Verifications passed

| Check | Result |
|---|---|
| `dotnet build Rtg.slnx -c Debug` | 0 errors, 0 warnings ✅ |
| `dotnet build Rtg.slnx -c Release` | 0 errors, 0 warnings ✅ |
| `dotnet format Rtg.slnx --verify-no-changes` | exit 0 ✅ |
| `dotnet test Rtg.Tests/Rtg.Tests.csproj` | 1 passed, 0 failed ✅ |
| `docker compose -f docker-compose.dev.yml config` | exit 0 ✅ |

#### Deviations from story spec (worth flagging for review)

1. **`global.json` SDK version: `10.0.100` instead of `10.0.7`.** The story spec said `10.0.7` but that's a runtime patch version, not a valid SDK version. Real .NET 10 SDK versions follow `10.0.<feature_band><patch>` (e.g., 10.0.100, 10.0.201). Used `10.0.100` with `rollForward: latestFeature` so it picks the latest installed feature band (currently 10.0.201 on this machine).

2. **`Directory.Packages.props` has 7 PackageVersions instead of empty.** The story spec said the file should be empty initially with versions added by future stories. But the auto-generated csprojs from `dotnet new webapi`, `dotnet new worker`, and `dotnet new xunit` reference packages with versions, and Central Package Management requires those to be defined in `Directory.Packages.props`. Added: StyleCop.Analyzers, coverlet.collector, Microsoft.NET.Test.Sdk, xunit, xunit.runner.visualstudio, Microsoft.AspNetCore.OpenApi, Microsoft.Extensions.Hosting.

3. **Solution file is `Rtg.slnx` (XML format), not `Rtg.sln` (legacy format).** .NET 10's `dotnet new sln` defaults to the new XML-based `.slnx` format. Both work identically with `dotnet build`/`format`/`test`/`sln` commands; the difference is purely tool ergonomics (better diff/merge story).

4. **`.editorconfig` includes StyleCop rule overrides.** Disabled SA1633 (file headers), SA1101 (`this.` prefix), SA1600/SA1602 (XML docs on internal types), SA1649 (file name match), SA1200 (using inside namespace). Reasoning: these are pedantic defaults that conflict with modern .NET conventions and minimal-API patterns. Kept meaningful rules like SA1413 (trailing comma) and SA1505/SA1508 (brace blank-line hygiene).

5. **Auto-generated content replaced with minimal skeletons:**
   - `Rtg.Api/Program.cs`: dropped the `WeatherForecast` example; minimal 3-line `WebApplication.CreateBuilder(args).Build().Run()` placeholder
   - `Rtg.Listener/Program.cs`: dropped the `AddHostedService<Worker>()` line; minimal `Host.CreateApplicationBuilder(args).Build().Run()` placeholder
   - `Rtg.Listener/Worker.cs`: deleted (will be re-introduced in Story 2.9 as `CraneListenerWorker`)
   - `Rtg.Tests/UnitTest1.cs` → `Rtg.Tests/SkeletonTests.cs`: replaced empty `Test1()` with `SolutionBuilds()` sentinel test

6. **Tasks 3+4 (Flutter cabin/forklift) BLOCKED — Group Policy.**
   - Initial check: Flutter not installed on this machine.
   - Attempted install via `git clone -b stable --depth 1 https://github.com/flutter/flutter.git E:\AI\flutter\` — clone succeeded.
   - First bootstrap run (`flutter --version`) was **blocked by Windows Group Policy**: `"This program is blocked by group policy. For more information, contact your system administrator. Error: Unable to determine engine version..."`. The block is at the OS level, applied when `dart.exe` (bundled engine binary) tries to execute from a user-writable path (`E:\AI\flutter\`).
   - This is a corporate security policy (AppLocker / Software Restriction Policy) preventing execution from user dirs.
   - **Resolution paths for Yaniv:**
     - Ask IT to whitelist `E:\AI\flutter\` (or the parent dev-tools directory)
     - Have IT install Flutter to `C:\Program Files\Flutter\` (admin-only path, policy-allowed)
     - Try `winget install Google.Flutter` (winget packages are MS-signed; may bypass policy)
     - Run from an admin-elevated shell if user account has admin rights
   - SDK source files (~600MB) are parked at `E:\AI\flutter\` ready for use once execution is permitted; can be deleted if Yaniv prefers a clean slate.

7. **Task 8.9 (git commit) DEFERRED.** Repo isn't a git repo yet (`git rev-parse --is-inside-work-tree` returned fatal error). The story spec said the dev should `git init` if not initialized, but doing so without coordination has remote-creation implications (Yaniv chooses default branch name, GPG signing config, remote URL, etc.). Deferring to Yaniv. Conventional commit message ready for use:
   ```
   chore: repo skeleton + .NET 10 + Flutter + dbmate init

   - server/: .NET 10 solution with 7 projects (...)
   - cabin/, forklift/: Flutter scaffolding (deferred — SDK install pending)
   - db/: dbmate scaffolding
   - .github/: CI/CD workflow stubs + dependabot + CODEOWNERS
   - ops/: docker-compose.dev.yml + placeholders for Story 1.2/7.x

   Closes Story 1.1 (partial — Flutter parts blocked).
   ```

#### Resume instructions for next session

To complete Story 1.1:
1. Install Flutter 3.41.5 (https://docs.flutter.dev/install/windows) and add to PATH
2. Run `/bmad-dev-story` again — it'll detect `1-1-repository-skeleton` is `in-progress` and resume; the remaining Flutter tasks will execute
3. After Flutter tasks complete, the dev-story workflow flips status to `review` and Yaniv runs `/bmad-code-review`

### File List

#### Created — repo root
- `.gitignore`
- `.editorconfig`
- `.gitattributes`
- `LICENSE`
- `README.md`
- `docker-compose.dev.yml`

#### Created — server/ (.NET 10 solution)
- `server/Rtg.slnx`
- `server/global.json`
- `server/Directory.Build.props`
- `server/Directory.Packages.props`
- `server/nuget.config`
- `server/Rtg.WireProtocol/Rtg.WireProtocol.csproj`
- `server/Rtg.Listener/Rtg.Listener.csproj`
- `server/Rtg.Listener/Program.cs` (minimal skeleton)
- `server/Rtg.Listener/Properties/launchSettings.json` (auto-generated by template)
- `server/Rtg.Listener/appsettings.json` (auto-generated by template)
- `server/Rtg.Listener/appsettings.Development.json` (auto-generated by template)
- `server/Rtg.Api/Rtg.Api.csproj`
- `server/Rtg.Api/Program.cs` (minimal skeleton, WeatherForecast removed)
- `server/Rtg.Api/Properties/launchSettings.json` (auto-generated by template)
- `server/Rtg.Api/appsettings.json` (auto-generated by template)
- `server/Rtg.Api/appsettings.Development.json` (auto-generated by template)
- `server/Rtg.Api/Rtg.Api.http` (auto-generated by template)
- `server/Rtg.Persistence/Rtg.Persistence.csproj`
- `server/Rtg.DualWrite/Rtg.DualWrite.csproj`
- `server/Rtg.Auth/Rtg.Auth.csproj`
- `server/Rtg.Tests/Rtg.Tests.csproj`
- `server/Rtg.Tests/SkeletonTests.cs` (replaces auto-generated `UnitTest1.cs`)

#### Created — db/ (dbmate scaffolding)
- `db/dbmate.env.example`
- `db/migrations/.gitkeep`
- `db/seeds/.gitkeep`
- `db/scripts/README.md`

#### Created — .github/ (CI + dependabot + CODEOWNERS)
- `.github/workflows/ci-server.yml`
- `.github/workflows/ci-flutter-cabin.yml`
- `.github/workflows/ci-flutter-forklift.yml`
- `.github/workflows/ci-db.yml`
- `.github/workflows/cd-staging.yml`
- `.github/workflows/cd-production.yml`
- `.github/dependabot.yml`
- `.github/CODEOWNERS`

#### Created — ops/ (docker-compose support + placeholders)
- `ops/docker/postgresql.conf` (placeholder)
- `ops/docker/prometheus.yml` (placeholder)
- `ops/monitoring/grafana-dashboards/.gitkeep`
- `ops/windows-services/.gitkeep`
- `ops/runbooks/.gitkeep`
- `ops/deployment/.gitkeep`

#### Auto-deleted
- `server/Rtg.WireProtocol/Class1.cs`
- `server/Rtg.Persistence/Class1.cs`
- `server/Rtg.DualWrite/Class1.cs`
- `server/Rtg.Auth/Class1.cs`
- `server/Rtg.Listener/Worker.cs` (will return in Story 2.9 as `CraneListenerWorker`)
- `server/Rtg.Tests/UnitTest1.cs` (renamed to `SkeletonTests.cs` with new content)

#### Modified — sprint-status
- `_bmad-output/implementation-artifacts/sprint-status.yaml`: `epic-1: backlog → in-progress`, `1-1-repository-skeleton: backlog → in-progress`

#### Pending (Flutter blocked)
- `cabin/` (Flutter project init via `flutter create`)
- `forklift/` (Flutter project init via `flutter create`)
