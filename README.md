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
- .NET 10 SDK (10.0.100 or later) — https://dotnet.microsoft.com/download
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
