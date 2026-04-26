---
stepsCompleted: [1, 2, 3, 4]
inputDocuments:
  - _bmad-output/planning-artifacts/architecture.md
  - docs/brownfield/00_SUMMARY.md
  - docs/brownfield/01_inventory.md
  - docs/brownfield/02_components.md
  - docs/brownfield/03_data_flows.md
  - docs/brownfield/04_database.md
  - docs/brownfield/05_protocol.md
  - docs/brownfield/06_operator_ui.md
  - docs/brownfield/07_edge_cases.md
  - docs/brownfield/08_binaries.md
  - docs/brownfield/09_open_questions.md
  - KoneCranes/RTG/Network/RTG-TD-Konecranes TOS interface_V40.pdf
  - CurrentSystem/RTG-Template/design_handoff_login/README.md
  - CurrentSystem/RTG-Template/design_handoff_rtg_crane_operator/README.md
  - CurrentSystem/RTG-Template/ (Modern variant — JSX prototypes + HTML previews + goldbond-logo.png)
prdStatus: 'waived — architecture.md serves as PRD-equivalent (Path A, Yaniv 2026-04-26)'
project_name: 'Craines-TOS'
user_name: 'Yaniv'
date: '2026-04-26'
lastStep: 4
status: 'complete'
completedAt: '2026-04-26'
storyCount: 125
epicCount: 9
---

# Craines-TOS — Epic & Story Breakdown

## Overview

מסמך זה מחלק את הדרישות מהארכיטקטורה ומבנדל ה-UX (Modern variant) ל-epics ו-user stories ניתנים-ליישום. הדרישות נגזרות מ-`architecture.md` (PRD-equivalent — Path A) + `RTG-Template/` (UX design final).

**מבנה המסמך:**
- §Requirements Inventory — מלאי דרישות מלא (FRs + NFRs + Additional + UX-DRs)
- §FR Coverage Map — איזה epic/story מכסה כל FR
- §Epic List — 13 epics בסדר תלויות
- §Per-Epic Detail — עבור כל epic: stories עם acceptance criteria

**שפה:** narrative בעברית; identifiers טכניים (file paths, class names, FR/NFR IDs) באנגלית — תואם ל-`architecture.md`.

---

## Requirements Inventory

### Functional Requirements

נגזרות מ-`architecture.md §1.1` (8 תחומי-יכולת FR-1..FR-8) ו-RTG-Template (Modern variant). מנותחות לרזולוציה של story-level.

#### FR1 — Crane Wire-Protocol Listener

- **FR1.1**: קבלת חיבורי TCP מ-PLC על ports per-crane (30701/2/3); single connection per port; bind על OT subnet
- **FR1.2**: פירוק הודעות YARDIT v4.0 — A1 (position), A2 03 (PICK done), A2 04 (PLACE done), A3 (cancel ack)
- **FR1.3**: שליחת broadcast jobs (B3 PICK, B4 PLACE) מ-job queue ב-`rtg.jobs` ל-PLC
- **FR1.4**: שליחת ACK responses (`04B2 + counter + checksum`) פר V40 spec
- **FR1.5**: Idempotency על retransmits — dedup לפי `(crane_id, counter, msg_type)`
- **FR1.6**: Length-byte framing פר V40 spec (lookup byte 1 → length); אין הסתמכות על `stream.Read(256)` כפי שב-legacy
- **FR1.7**: Validation של checksum נכנס; reject + log + DLQ אם לא תקף
- **FR1.8**: DLQ ל-unknown frames — לוגינג structured + alert ב-DLQ growth
- **FR1.9**: Per-crane `BackgroundService` ב-`Rtg.Listener` — אחד לכל GOLD1/2/3
- **FR1.10**: System.IO.Pipelines-based TCP server — אין `goto`, אין `Substring(offset)` קשיח

#### FR2 — Cabin Operator UI (Flutter Windows Desktop, Modern variant)

- **FR2.1**: Login flow — username dropdown (AD-synced operators) + 4-digit PIN keypad + crane selector (GOLD1/2/3); enabled only כש-`userId && pin.length===4 && craneAvailable`
- **FR2.2**: Main yard screen (M1) — 2D grid (rows A–F × cols 94-106) עם stack-height heatmap; live crane position indicator; source/target tile highlighting
- **FR2.3**: Submit PICK/PLACE jobs דרך map interaction — בחירת FROM cell, הזנת TO destination, אישור
- **FR2.4**: Cancel pending jobs עם hold-to-confirm gesture
- **FR2.5**: Manual location editor (יציאה מ-FR2.2 / כניסה ל-context menu) — override לתוכן cell בחצר
- **FR2.6**: Truck mode (M6) — unloading queue (אמבר header) + loading queue (teal header); driver/plate/wait time per bay
- **FR2.7**: Work-orders list (M3) — filterable table עם block tabs (BOND1/BOND2/BOND3); columns container/size/type/weight/locations/task/time
- **FR2.8**: Reports — empty containers (M9 left), no-location (M9 right), expected containers timeline (M8), movements log (M10)
- **FR2.9**: Suggested locations screen (M7) — 3×2 grid with 0-100 score bars; "מומלץ" highlight; per-card row/col/stack chips + reason
- **FR2.10**: Container detail screen (M5) — deal-containers list left, full detail panel right; hero treatment עבור container ID
- **FR2.11**: RTG settings/diagnostics (M11) — Connections card (PLC/GPS/satellites/accuracy/HMI/TOS), Current position (lat/long/altitude/heading/speed), action list (recalibrate / test / sync / maintenance / reboot)
- **FR2.12**: GPS HUD on every screen — top bar עם fix type (RTK/DGPS/2D/NO FIX), satellite count, accuracy in meters
- **FR2.13**: RTL Hebrew throughout; 1920×1080 native; touchscreen-first
- **FR2.14**: WebSocket push subscription via SignalR — replaces 500ms polling של legacy
- **FR2.15**: Numeric keypad (M4) — 3×4 grid, color-coded keys (digits white, ← אמבר, CLR אדום, confirm ירוק 68px); תומך גם input ל-PIN וגם ל-block code search
- **FR2.16**: Numpad + container picker dual-pane (M4) — keypad left, filtered container list right
- **FR2.17**: Time-aware greeting (`בוקר טוב` / `צהריים טובים` / `ערב טוב`) on login — cosmetic
- **FR2.18**: Self-update via `GET /api/v1/clients/cabin/version` — startup + hourly check; force-update if `current < minRequired`

#### FR3 — Forklift Driver UI (Flutter Windows Desktop)

- **FR3.1**: Login flow — username + 7-char password + forklift number input + terminal indicator (Ashdod ILCXQ; ILGBH disabled)
- **FR3.2**: Main information screen (`frmInformation` equivalent) — current container details (ID, weight, contents, customer, line, location/expected, status)
- **FR3.3**: F-key shortcuts — F2 damage, F3 query, F4 works, F5 info, F6 sister-containers, F7 reserved, F10 exit, F12 continue
- **FR3.4**: Activity reporting — PICK/PLACE actions ל-API
- **FR3.5**: Info menu (frmInfoMenu)
- **FR3.6**: Works screen (frmWorks)
- **FR3.7**: Recommended location screen (sp_ForkLiftRecommendedLocation equivalent)
- **FR3.8**: Empty containers list (frmEmptyContainers)
- **FR3.9**: In-out diary (frmInOutDiory)
- **FR3.10**: Comment input (frmComment) — הוספת הערה למכולה
- **FR3.11**: Select container (frmSelectContainer)
- **FR3.12**: Special location (frmSpecialLocation)
- **FR3.13**: Same deal containers (frmSameDealNumber)
- **FR3.14**: Messages screen (frmMessage)
- **FR3.15**: Change forklift number (frmChangeForkliftNumber)
- **FR3.16**: Single-terminal (Ashdod ILCXQ) — ILGBH מחוץ להיקף
- **FR3.17**: Offline write-queue — Hive cache + queued writes; sync on reconnect
- **FR3.18**: Self-update via `GET /api/v1/clients/forklift/version`

#### FR4 — REST + WebSocket API (Rtg.Api)

- **FR4.1**: REST endpoints — `/api/v1/auth/*` (login, refresh, logout)
- **FR4.2**: REST endpoints — `/api/v1/cranes/{id}` (state, position queries)
- **FR4.3**: REST endpoints — `/api/v1/jobs` (POST submit, GET list, DELETE cancel)
- **FR4.4**: REST endpoints — `/api/v1/blocks/{id}/cells` (yard map), `PATCH /api/v1/locations/{id}` (manual edit)
- **FR4.5**: REST endpoints — `/api/v1/shifting` (history queries), `/api/v1/work-orders`, `/api/v1/trucks`, `/api/v1/containers/{id}` (composite query joining CO_Containers + CP_Deal + TB_Drivers)
- **FR4.6**: REST endpoints — `/api/v1/reports/{empty,no-location,expected,diary}`
- **FR4.7**: REST endpoints — `/api/v1/forklift/*` — מימוש ~80 sp_ForkLift* equivalents
- **FR4.8**: SignalR hub `/hub/rtg` — topic-based subscriptions (`SubscribeToCrane`, `SubscribeToBlock`)
- **FR4.9**: PG `LISTEN` בתוך `PgListenerHostedService` → SignalR push לסגנון `OnPositionUpdate`, `OnJobStateChange`, `OnLocationUpdated`
- **FR4.10**: Self-update endpoint `GET /api/v1/clients/{cabin|forklift}/version` → `{latest, minRequired, downloadUrl}`
- **FR4.11**: OpenAPI 3.x spec auto-generated via `Microsoft.AspNetCore.OpenApi`; Swagger UI at `/swagger` בסביבת dev
- **FR4.12**: Rate limiting — 100 req/min/user reads, 30 writes; via `Microsoft.AspNetCore.RateLimiting`
- **FR4.13**: RFC 7807 Problem Details ל-error responses
- **FR4.14**: Correlation IDs — `X-Request-Id` header; logged ב-Serilog context

#### FR5 — Persistence Layer (PostgreSQL 18.3)

- **FR5.1**: PG schema `rtg.*` — tables `users`, `refresh_tokens`, `cranes`, `crane_positions`, `jobs`, `job_state_history`, `locations`, `shifting`, `audit_log`, `dual_write_outbox`, `containers` (read-mirror)
- **FR5.2**: Partitioned tables (monthly): `rtg.crane_positions`, `rtg.shifting`, `rtg.audit_log`
- **FR5.3**: PG `LISTEN/NOTIFY` triggers — channels `rtg.<crane>.<topic>` (`rtg.gold3.position`, `rtg.gold3.job`, `rtg.bond3.location_updated`); minimal payload `{id, ts, type}`
- **FR5.4**: dbmate migrations ב-`db/migrations/` — `up/down` per migration; CI replay test
- **FR5.5**: `mssql_mirror.*` schema with foreign tables via `postgres_fdw` — read-only access ל-MSSQL טבלאות (CO_Containers, CP_Deal, TB_Drivers, וכו') במהלך transition
- **FR5.6**: 90-day hot retention; archive cold לאחסון זול (script ב-`db/scripts/`)
- **FR5.7**: Reconciliation cron @ hourly — diff בין `rtg.shifting` ל-MSSQL `RG_Shifting`; alert על drift
- **FR5.8**: Repository pattern (`Rtg.Persistence/Repositories/`) — אחד לכל aggregate; Dapper-based
- **FR5.9**: PG `wal_level = replica` מהיום הראשון (פותח path ל-streaming replication עתידית)
- **FR5.10**: Connection pooling via Npgsql; `MaxPoolSize` configurable per service

#### FR6 — Auth + Active Directory Identity

- **FR6.1**: AD-2 model — LDAP sync cron @ 15 min ל-`rtg.users` mirror; AD authoritative ל-`(active, group_membership, email)`
- **FR6.2**: PIN/password hashed עם **Argon2id** (64MB / 3 iter / 4 par) ב-PG `rtg.users.pin_hash`
- **FR6.3**: JWT issuance — RS256 (asymmetric), private key on server only
- **FR6.4**: Access token TTL 15 min; refresh token TTL 8 hours (matches a shift)
- **FR6.5**: Refresh tokens stored in `rtg.refresh_tokens` עם revocation list
- **FR6.6**: Server-side login validation — PIN hash verify + AD active check + group membership
- **FR6.7**: Lockout — 5 attempts in 15 min → 30 min lockout per `(user, source_ip)`
- **FR6.8**: Audit trail — לכל login/logout/PIN attempt + לכל PICK/PLACE/Cancel/manual edit/role change; tied ל-AD principal (לא רק `rtg.users.id`)
- **FR6.9**: Logout endpoint + refresh token revocation
- **FR6.10**: Session expiry behavior — 401 על access expiration → client refreshes; 401 על refresh expiration → redirect to login

#### FR7 — Process Supervision

- **FR7.1**: Windows Service registration למודולים: `Rtg.Listener`, `Rtg.Api`, `Rtg.DualWrite`
- **FR7.2**: Auto-restart on crash via Windows Service recovery options (3 attempts before alarm)
- **FR7.3**: Graceful shutdown — drain mode עם 30s timeout; מפסיק לקבל TCP חדשים; מסיים processing של frames נקראים; מנקה outbox; אם לא הסתיים תוך 30s → hard abort
- **FR7.4**: Health check endpoint per service: `GET /health` returns `200 OK` + JSON `{status, db, signalrConnections, queueDepth}`
- **FR7.5**: Removal of legacy supervision — `xp_cmdshell`, `ConsolesReRun.exe`, `TosReRun.exe`, `KillToss{n}.exe` all replaced by Windows Service infrastructure

#### FR8 — Migration & Cutover

- **FR8.1**: GOLD3 pilot first — Saturday cutover window, < 30s rollback path
- **FR8.2**: Dual-write sync to MSSQL — PG first → MSSQL inline; אם MSSQL נכשל → `rtg.dual_write_outbox`
- **FR8.3**: Outbox processor — retry 1m → 5m → 15m → 1h → 6h → 24h; dead-letter at 10 attempts
- **FR8.4**: Reconciliation drift alerts — compare PG ↔ MSSQL hourly; alert on drift > threshold
- **FR8.5**: Rollback path — legacy `TOSService.exe` stays in standby on `192.6.8.52`; flip listener target IP at PLC firewall (one rule change) → < 30s
- **FR8.6**: GOLD1 + GOLD2 rollout in subsequent Saturdays after pilot stability (1-4 weeks reconciliation)
- **FR8.7**: MSSQL retirement plan — when downstream consumers (MIS, AuditWeb, WMS, legacy ForkliftApp) finish their own migrations
- **FR8.8**: Pre-cutover dry-run — chesimu-driven full-stack smoke test on the new VM

---

### NonFunctional Requirements

נגזרות מ-`architecture.md §1.1` (NFR-1..NFR-10), עם פירוט פר sub-aspect.

#### NFR1 — Security-by-Design

- **NFR1.1**: 0 secrets בקוד או ב-config files — כולם ב-env vars, GitHub Secrets, או `C:\Rtg\secrets\<service>.env` (mode 0600)
- **NFR1.2**: 100% queries via parameterized statements (Dapper תומך native; אין `string.Format` ב-SQL)
- **NFR1.3**: Server-side auth verification — אין client-side trust
- **NFR1.4**: Rate limiting + lockout (per FR4.12 + FR6.7)
- **NFR1.5**: Firewall whitelist על TCP ports 30701-30703 — PLC IPs בלבד
- **NFR1.6**: אין `xp_cmdshell` בכלל
- **NFR1.7**: TLS terminator ב-IIS reverse proxy מול Kestrel; HTTP→HTTPS redirect; HSTS
- **NFR1.8**: PII (PIN, password, JWT, refresh token) — אסור ב-logs בכלל
- **NFR1.9**: Cert source — self-signed ל-pilot עם cert-rotation runbook; transition ל-internal CA כשאפשר

#### NFR2 — Real-Time UX

- **NFR2.1**: p95 cabin push latency ≤ 200ms (state change → SignalR client receive)
- **NFR2.2**: p99 PLC ACK round-trip ≤ 1s (under normal load)
- **NFR2.3**: WebSocket reconnect ≤ 5s (after disconnect detection within 30s SignalR heartbeat)

#### NFR3 — Data Integrity

- **NFR3.1**: PLACE handler עוטף את כל ה-DB writes ב-single PG transaction; rollback on any failure → log + NAK
- **NFR3.2**: 100% retransmits מתבצע dedup על idempotency key
- **NFR3.3**: 100% inbound frames עוברים checksum validation
- **NFR3.4**: 0 lost messages on controlled crash (validated by integration test)
- **NFR3.5**: 0 partial-PLACE writes ב-load test

#### NFR4 — Reliability

- **NFR4.1**: MTTR < 60s on service crash
- **NFR4.2**: Graceful shutdown — drain mode (per FR7.3)
- **NFR4.3**: Forklift offline write-queue (per FR3.17)
- **NFR4.4**: PG `wal_level = replica` from day 1 — מאפשר streaming replication עתידי בלי restart
- **NFR4.5**: VM backup — snapshots + nightly `pg_dump` + weekly offsite copy

#### NFR5 — Observability

- **NFR5.1**: Serilog structured JSON output → Seq (Docker container על ה-VM)
- **NFR5.2**: Prometheus + Grafana — 4 SLI dashboards (listener throughput, API latency, dual-write lag, auth failures)
- **NFR5.3**: Alert rules ב-Grafana Alertmanager — listener disconnect, DB write failure, DLQ growth, dual-write lag > threshold
- **NFR5.4**: Correlation IDs throughout (FR4.14)

#### NFR6 — Migration Safety

- **NFR6.1**: Dual-write עם eventual consistency tolerance (per FR8.2)
- **NFR6.2**: Idempotency keys ל-replay (per FR1.5 + FR8.4)
- **NFR6.3**: Read-back verification לפני flip — pre-cutover smoke test (per FR8.8)
- **NFR6.4**: < 30s rollback validated ב-DR drill (per FR8.5)

#### NFR7 — Performance Baseline

- **NFR7.1**: Sustain 6 msg/sec inbound (3 cranes × 1-2 A1/sec)
- **NFR7.2**: Support ~10 jobs/min/crane peak
- **NFR7.3**: 3 cabin clients + 5-15 forklift clients concurrent
- **NFR7.4**: 10× baseline ב-load test (60 msg/sec) without degradation
- **NFR7.5**: API p95 latency < 200ms על read endpoints; < 500ms על write endpoints

#### NFR8 — Compliance / Audit

- **NFR8.1**: Audit trail trustworthy — כל פעולה תיוחס ל-AD principal מאומת (לא לקבוצה כמו ב-legacy)
- **NFR8.2**: AuditWeb-equivalent — `rtg.audit_log` mirrored ל-MSSQL `AuditWeb` via DualWrite
- **NFR8.3**: Audit log retention 90 days hot + archived

#### NFR9 — Internationalization (Hebrew RTL)

- **NFR9.1**: 100% operator-facing strings in Hebrew
- **NFR9.2**: Strings catalog בלבד — `cabin/lib/core/l10n/he.arb`, `forklift/lib/core/l10n/he.arb`, `server/Rtg.Api/resources/he.json`
- **NFR9.3**: 0 hardcoded Hebrew strings ב-code (CI gate)
- **NFR9.4**: LTR-in-RTL — container IDs, GPS coordinates, timestamps, mono numbers
- **NFR9.5**: `Directionality.rtl` ב-root של שני ה-Flutter apps

#### NFR10 — Hardware / Network

- **NFR10.1**: Cabin — 21" 1920×1080 industrial touchscreen, Windows desktop fleet קיים
- **NFR10.2**: Forklift — rugged Windows tablets קיימים, F-key keyboard support
- **NFR10.3**: Touch targets — minimum 44px; primary actions ≥ 60px
- **NFR10.4**: OT subnet topology — `192.6.x.x` עם mGuard NAT לכל crane
- **NFR10.5**: VM sizing — 4-8 vCPU, 16-32 GB RAM, ~500 GB SSD
- **NFR10.6**: NTP sync ל-Goldbond NTP server pre-cutover

---

### Additional Requirements

נגזרות מ-`architecture.md §2-§6` — דרישות תשתית, פיתוח, ו-deployment שמשפיעות על epic/story creation.

- **AR-1**: Stack lock — .NET 10 LTS + Flutter 3.41.5 + PG 18.3 + dbmate + Heebo & IBM Plex Mono fonts
- **AR-2**: Monorepo on GitHub (`goldbond/craines-tos`), workflows ב-`.github/workflows/`
- **AR-3**: Single VM on OT subnet `192.6.8.x` (separate from `192.6.8.52`)
- **AR-4**: PG runs ב-Docker container `postgres:18.3-alpine`; .NET stack כ-Windows Services bare-metal
- **AR-5**: Solution layout `server/Rtg.{WireProtocol,Persistence,Auth,DualWrite,Listener,Api,Tests}` (architecture §5.1)
- **AR-6**: Two Flutter apps `cabin/` + `forklift/` עם feature-first organization (architecture §5.1)
- **AR-7**: Dapper for PG queries + SqlClient for MSSQL dual-write (אין EF Core)
- **AR-8**: xUnit + Bogus + Testcontainers (PG-in-Docker) ל-tests
- **AR-9**: dbmate flat SQL migrations ב-`db/migrations/`
- **AR-10**: GitHub Actions self-hosted runner on the VM ל-deploy
- **AR-11**: Logging: Serilog → Seq (Docker container on VM)
- **AR-12**: Metrics: Prometheus + Grafana (Docker on VM)
- **AR-13**: Backup: VM snapshots + nightly `pg_dump` + weekly offsite
- **AR-14**: Secrets at runtime: `C:\Rtg\secrets\<service>.env` mode 0600, service account only
- **AR-15**: NTP sync ל-Goldbond NTP server pre-cutover
- **AR-16**: PG `postgres_fdw` extension ל-`mssql_mirror.*` foreign-data wrapper views
- **AR-17**: chesimu (KoneCranes simulator) used לבדיקות פיתוח + pre-cutover smoke test
- **AR-18**: ADRs required לכל סטייה ארכיטקטונית — `docs/decisions/ADR-NNN-<slug>.md`
- **AR-19**: Pre-commit hooks — `dotnet format` + `dart format` + linters (StyleCop ב-.NET, `flutter_lints` ב-Flutter)
- **AR-20**: CI gates — format check, OpenAPI annotations, migration drift, test coverage targets:
  - `Rtg.WireProtocol` 90% line coverage
  - `Rtg.Persistence`, `Rtg.Auth`, `Rtg.DualWrite` 80%
  - `Rtg.Listener`, `Rtg.Api` 70%
  - Flutter cabin/forklift 60%
- **AR-21**: Story #1 = repo skeleton (init commands per architecture §2.5)
- **AR-22**: Modern variant of UI selected — Conservative + Bold are reference-only
- **AR-23**: Forklift design system — extrapolate from cabin Modern tokens; **forklift UX bundle is currently a gap** (no design_handoff for forklift in `RTG-Template/`); requires UX pass before forklift epic stories are fully detailed
- **AR-24**: GPS receiver integration for cabin (fix type, satellites, accuracy in meters) — wire ל-RTK receiver via platform channel; ≥1Hz update; debounce status chip
- **AR-25**: AD bind credentials — IT provisions service account (`svc-rtg-ldap`) with read-only scope on user/group OUs
- **AR-26**: Firewall whitelist owner — IT, contact handoff in `ops/runbooks/cutover-gold3.md`

---

### UX Design Requirements

נגזרות מ-`CurrentSystem/RTG-Template/` (Modern variant — selected by Goldbond). דרישות מאפיין-ספציפיות לכל משימת מימוש.

- **UX-DR1 — Design tokens (cabin)**: יישום `core/theme/CraneTheme` בפלטר עם כל ה-tokens מ-`RTG-Template/design_handoff_rtg_crane_operator/README.md` "Global Design Tokens (Modern)":
  - Colors: teal `#0f766e`, tealDeep `#115e59`, tealSoft `#ccfbf1`, amber `#d97706`, green `#16a34a`, red `#dc2626`, violet `#7c3aed`, surface `#f6f8fa`, card `#ffffff`, ink `#0f172a`, muted `#64748b`, line `#e2e8f0`
  - Stack-height palette: 7 levels (empty + 1/6 through 6/6)
  - Typography: Heebo (400-900) + IBM Plex Mono (400-700) — bundled via `pubspec.yaml`
  - Sizes: 11/12/13/14/15/16/18/20/22/24/28/30/36/44/72px (no new sizes)
  - Radii: 8/10/12/14/16/20/999px
  - Shadows: card / stats / dropdown / active / login / icon-tile / status-pill — exact specs in README

- **UX-DR2 — Reusable widgets (cabin core)**:
  - `TouchButton` — gloves-friendly, ≥44px / ≥60px primary, transitions
  - `StatusPill` — system pills with colored dot + halo + label + value
  - `MonoNumeric` — LTR-in-RTL wrapper for IDs, coords, timestamps
  - `GlassPanel` — frosted-glass card with `backdrop-filter: blur(8-10px)`
  - `CraneIndicator` — live position marker on yard grid
  - `YardCell` — tile with stack-height heatmap colors
  - `HebrewText` — RTL helper widget
  - `SourceTargetTile` — source (amber gradient + 2.5px solid border + "מקור") / target (teal gradient + 2.5px **dashed** border + "→h+1")

- **UX-DR3 — Login screen layout** (per `design_handoff_login/README.md`):
  - Top bar 112px — logo (108px) + divider + title + build line + 4 system pills (PLC/GPS/TOS/רשת) + clock
  - Main grid `1.15fr 1fr; gap: 44px; padding: 14px 64px 32px`
  - Right column — badge "מערכת חדשה · דור 4" + hero gradient title "בוקר טוב. מוכנים להתחיל?" + lede + 4-stat strip
  - Left column — login card עם username dropdown + 4-cell PIN row + 3-button crane picker + actions row + security footer
  - Floating numeric keypad — bottom-left, 380px wide, 3×4 grid
  - Ambient background — radial wash + grid + container-silhouette band

- **UX-DR4 — M1 Main yard screen**: top bar עם GPS HUD; 2D grid (rows A-F × cols 94-106); stack-height heatmap; source/target pulse + dashed states; bottom panels (source / arrow / target) + action bar (אישור / ביטול / משאית / עבודות / תפריט)

- **UX-DR5 — M2 Main menu**: 8 action tiles ב-4×2 grid; כל tile עם accent stripe בצבע קטגוריה (Empty / No-location / Log / Containers-by-block / Update location / Suggested / Expected / Settings)

- **UX-DR6 — M3 Jobs list**: filterable table; block tabs (BOND1/BOND2/BOND3); columns container/size/type/weight/from/to/task/time/action; search input

- **UX-DR7 — M4 Numpad + container picker**: two-column — keypad left (3×4 grid: digits white / ← amber / CLR red / confirm green 68px), filtered container list right

- **UX-DR8 — M5 Container detail + deal containers**: deal-list left, full detail panel right; hero treatment for container ID (teal gradient); fields (size, type, handling code, location, weight, capacity, customer, risk, job, release info); "בחר מכולה זו" green action

- **UX-DR9 — M6 Truck load/unload**: top — truck bays with driver/plate/wait time; middle — unload table (amber header, ↓ arrow); bottom — load table (teal header, ↑ arrow)

- **UX-DR10 — M7 Suggested locations**: 3×2 grid of ranked cards; "מומלץ" highlight on top recommendation (teal gradient); per-card location code (hero mono) + row/col/stack chips + reason text + 0-100 score bar + "select" button

- **UX-DR11 — M8 Expected containers**: timeline list; per-row ETA badge (highlighted at-gate) + container ID + line + customer + size + type + truck bay + status pill (בדרך / הגיע לשער / צפוי)

- **UX-DR12 — M9 Empty + No-location**: split screen; left — empty containers with days-in-yard age coding (amber >7, red >14); right — no-location list with reason text (e.g., "GPS לא עודכן"); per-row "Update location" action

- **UX-DR13 — M10 Movements log**: chronological list; per-row time → container ID → from-chip → arrow → to-chip → operator → status pill; filter tabs (today / yesterday / this week / custom range)

- **UX-DR14 — M11 RTG settings + GPS panel**: Connections card (PLC, GPS fix, satellites, accuracy, HMI, TOS server — colored metric tiles); Current position card (lat/long/altitude/heading/speed/last update); action list (recalibrate GPS, test PLC, sync with TOS, maintenance mode, reboot); version/build footer

- **UX-DR15 — GPS HUD** (every cabin screen, top bar): fix type (RTK/DGPS/2D/NO FIX) + satellite count + accuracy in meters; debounce status chip color (no flash on single-frame dropout); ≥1Hz update rate; subtle amber pulse when RTK lost

- **UX-DR16 — Stack-height heatmap palette**: 7 levels with progressive color/border treatment per `design_handoff_rtg_crane_operator/README.md` "Stack-height palette"

- **UX-DR17 — Source/target tile visual states**: source = amber 135° gradient + 2.5px solid amber border + "מקור" label; target = teal 135° gradient + 2.5px **dashed** teal border + "→h+1" label

- **UX-DR18 — PIN entry UI**: 4 dot cells (50×50, radius 10); empty state (white bg + 2px `#cbd5e1` border); filled state (teal bg + teal border + ● glyph 28px white); current caret (teal border + halo `0 0 0 4px rgba(15,118,110,.15)`); transition `.12s`

- **UX-DR19 — Numeric keypad component**: 3×4 grid; digits (white bg + ink text + 1.5px `#e2e8f0` border + 26px/700); CLR (red bg `#fef2f2` + red text `#dc2626` + 15px/700); ← (amber bg `#fffbeb` + amber text `#d97706` + 26px/700); height 64px; behavior — every key forces PIN focus; CLR clears; ← pops last; 0-9 append while pin.length < 4

- **UX-DR20 — Crane picker (3-button grid)**: states default (white + ink) / active (teal + white + shadow) / disabled (`#f8fafc` + `#94a3b8` + opacity 0.65); per-button — top row crane id (20px/800) + status dot (8×8 colored), sub-line "BOND# · status"

- **UX-DR21 — Forklift design system** (gap closure): extrapolate from cabin Modern tokens; reuse `core/theme/`; commission dedicated UX pass for 14 forklift screens (currently no design_handoff in `RTG-Template/`); placeholder design uses cabin tokens but with denser layout for the smaller tablet form factor

- **UX-DR22 — System pill component** (top bar reuse on every screen): pill (radius 999, padding 6×12, white-translucent bg + backdrop blur + 1px `#e2e8f0` border); colored dot (8×8 with halo `0 0 0 3px <color>33`); 11px label + 12px IBM Plex Mono value

- **UX-DR23 — Hebrew time-aware greeting**: time-based greeting on cabin login screen ("בוקר טוב" / "צהריים טובים" / "ערב טוב") — replaces mock "בוקר טוב"

- **UX-DR24 — RTG-Template badge feature flag**: "מערכת חדשה · דור 4" badge — feature-flagged; default-on for first 3 months, then auto-hide

- **UX-DR25 — High-contrast mode toggle (deferred)**: cabin runtime toggle that re-skins Modern with heavier ink/borders for sunlight readability — Bold variant CSS kept around as starting point; not in pilot

---

### FR Coverage Map

> Approved 2026-04-26 — 9 epics organized by user value (re-structuring of architecture §6.7's 13-step technical sequence; details in §Sequencing Rationale below).

| FR | Epic | Notes |
|---|---|---|
| **FR1.1-1.10** Wire protocol | Epic 2 | V40 codec + listener foundation |
| **FR2.1** Cabin login | Epic 1 | Login slice |
| **FR2.2** Yard map | Epic 2 | M1 + crane indicator |
| **FR2.3-2.5** Job submit/cancel/manual edit | Epic 3 | Container ops |
| **FR2.6-2.11** Daily ops screens | Epic 4 | M2-M11 |
| **FR2.12** GPS HUD | Epic 2 | Cross-cutting top-bar element |
| **FR2.13** RTL Hebrew | Epic 1 (foundation) | Cross-cutting via UX-DR1 |
| **FR2.14** WebSocket push | Epic 2 | Foundation, reused |
| **FR2.15-2.16** Numpad + picker | Epic 4 | M4 |
| **FR2.17** Time greeting | Epic 1 | Login flourish |
| **FR2.18** Cabin self-update | Epic 4 + Epic 7 | Endpoint in 4.1, monitoring in 7.7 |
| **FR3.1, FR3.16** Forklift login + single-terminal | Epic 1 | Login slice |
| **FR3.2-3.15, FR3.17-3.18** Forklift screens + offline + self-update | Epic 5 | Forklift slice |
| **FR4.1** Auth endpoints | Epic 1 | Foundation |
| **FR4.2** Cranes endpoints | Epic 2 | |
| **FR4.3** Jobs endpoints | Epic 3 | |
| **FR4.4** Locations endpoints | Epic 2 + Epic 3 | Read in 2, write in 3 |
| **FR4.5** Composite endpoints | Epic 4 | |
| **FR4.6** Reports | Epic 4 | |
| **FR4.7** Forklift API | Epic 5 | |
| **FR4.8-4.9** SignalR + LISTEN | Epic 2 | Foundation |
| **FR4.10** Self-update endpoint | Epic 4 + Epic 5 + Epic 7 | Cabin endpoint in 4.1, forklift endpoint in 5.6, monitoring in 7.7 |
| **FR4.11-4.14** OpenAPI/RFC 7807/rate limiting/correlation | Epic 1 | Foundation infra |
| **FR5.1** Schema | Distributed per-epic ownership | users/refresh_tokens/audit_log=1, cranes/positions=2, jobs/locations/shifting=3, dual_write_outbox + mssql_mirror.*=6, audit_log retention=7 |
| **FR5.2** Partitioning | Epic 2 + Epic 3 + Epic 7 | crane_positions/shifting/audit_log |
| **FR5.3** LISTEN/NOTIFY | Epic 2 | Foundation |
| **FR5.4** dbmate | Epic 1 | Foundation |
| **FR5.5** FDW mirror | Epic 6 | |
| **FR5.6** Retention | Epic 7 | |
| **FR5.7** Reconciliation | Epic 6 | |
| **FR5.8-5.10** Repo pattern, wal_level, pooling | Epic 1 | Foundation |
| **FR6.1-6.10** Auth + AD | Epic 1 | Full slice |
| **FR7.1-7.3** Windows Services + drain | Epic 8 | Pilot deploy |
| **FR7.4** Health checks | Epic 7 | |
| **FR7.5** Legacy supervision torn down | Epic 9 | After full rollout |
| **FR8.1, FR8.5, FR8.8** GOLD3 + rollback + dry-run | Epic 8 | |
| **FR8.2-8.4** Dual-write + outbox + drift | Epic 6 | |
| **FR8.6-8.7** Rollout + retirement | Epic 9 | |

**NFR distribution:** NFR1 (security) → spread across 1, 7, 8; NFR2 (real-time UX) → 2; NFR3 (data integrity) → 2, 3; NFR4 (reliability) → 1 (wal_level), 5 (offline), 8 (recovery); NFR5 (observability) → 7; NFR6 (migration safety) → 6, 8; NFR7 (performance) → 2, 3, 8; NFR8 (audit) → 1, 6; NFR9 (i18n RTL) → 1 (foundation), all UI epics; NFR10 (hardware) → 1, 8.

**UX-DR distribution:** UX-DR1-2 (tokens + widgets) → 1; UX-DR3, UX-DR18-20, UX-DR22-23 (login + components) → 1; UX-DR4, UX-DR15-17, UX-DR22 (M1 + GPS) → 2; UX-DR4, UX-DR17 (yard tile states) → 3; UX-DR5-14 (M2-M11) → 4; UX-DR21 (forklift design system) → 5; UX-DR24-25 (badge feature flag, deferred high-contrast) → 4 + future.

**AR distribution:** AR-1 through AR-22 → Epic 1 foundation; AR-3 (VM), AR-4 (Docker), AR-10 (deploy), AR-13-16 (ops), AR-25-26 (IT handoff) → Epic 8; AR-17 (chesimu) → Epic 2 + Epic 8; AR-23 (forklift design gap) → Epic 5; AR-24 (GPS receiver) → Epic 2.

---

## Epic List

> 9 epics, organized by user value per BMad principles. Architecture §6.7's 13-item technical sequence is re-mapped as story ordering within these epics. Approved by Yaniv 2026-04-26.

### Epic 1: Operator Identity & Sign-in

**User outcome:** Crane operators and forklift drivers authenticate via AD-mapped accounts with PIN/password; off-boarding works automatically; audit trail starts; the codebase has a working foundation.

**Includes:** Repo skeleton + CI + dev DB + Flutter scaffolding (foundation stories), PG schema for `users`+`refresh_tokens`, `Rtg.Auth` library (Argon2id + RS256 JWT + lockout), AD LDAP sync cron, auth API endpoints, cabin login screen (Modern UX), forklift login screen (extrapolated tokens), OpenAPI/RFC 7807/rate limiting/correlation IDs as foundation infra.

**FRs covered:** AR-1..AR-22 (foundation); FR5.1 partial (users, refresh_tokens), FR5.4 (dbmate), FR5.8-5.10; FR6.1-6.10 (full auth); FR4.1, FR4.11-4.14; FR2.1 (cabin login); FR3.1, FR3.16 (forklift login); FR2.17 (time greeting)
**NFRs:** NFR1.1-1.4, NFR1.6, NFR1.8-1.9; NFR8.1; NFR9.1-9.5
**UX-DRs:** UX-DR1, UX-DR2, UX-DR3, UX-DR18-DR20, UX-DR22-DR23

### Epic 2: Live Crane State (Wire Protocol + Real-time Map)

**User outcome:** Cabin operators see their crane's live position and status on the yard map, replacing the legacy 500ms-polling experience with sub-200ms WebSocket push. The Listener processes A1 telemetry from chesimu (and later real PLC) into PG, with proper framing/checksum/idempotency.

**Includes:** `Rtg.WireProtocol` V40 codec + tests, `Rtg.Listener` Worker Service with `System.IO.Pipelines`-based TCP server, A1 handler, PG schema for `cranes`+`crane_positions` (partitioned), LISTEN/NOTIFY triggers, SignalR `RtgHub` + topic subscriptions, `PgListenerHostedService` for PG→WebSocket fanout, cabin yard map screen (M1) with stack-height heatmap + GPS HUD + crane indicator, GPS receiver wiring, chesimu integration tests.

**FRs covered:** FR1.1-1.10 (full listener); FR5.1 partial (cranes, crane_positions), FR5.2 (partitioning), FR5.3 (LISTEN/NOTIFY); FR4.2 (cranes endpoints), FR4.8-4.9 (SignalR + LISTEN bridge); FR2.2 (yard map), FR2.12 (GPS HUD), FR2.13 (RTL), FR2.14 (WebSocket push)
**NFRs:** NFR2.1-2.3 (real-time UX); NFR3.2-3.4 (idempotency, checksum, DLQ); NFR4.4 (wal_level); NFR7.1
**UX-DRs:** UX-DR4 (M1 yard screen), UX-DR15 (GPS HUD), UX-DR16 (heatmap palette), UX-DR22 (system pills)

### Epic 3: Container Operations (PICK / PLACE / Cancel)

**User outcome:** Cabin operators dispatch container moves to the crane via the yard map (FROM cell + TO destination), cancel pending jobs with hold-to-confirm, and override yard locations manually when GPS auto-locate fails. Jobs flow listener → PLC → back; PG transactions guarantee no partial PLACE state.

**Includes:** PG schema for `jobs`+`locations`+`shifting` (partitioned), A2 PICK/PLACE handlers (single PG transaction), A3 cancel handler, jobs API endpoints, locations API + manual-edit endpoint, cabin job submit + cancel UI, source/target tile UX states, manual location editor.

**FRs covered:** FR1.3 (broadcast jobs), FR1.4 (ACK); FR5.1 partial (jobs, locations, shifting); FR4.3 (jobs API), FR4.4 (locations API); FR2.3-2.5 (submit, cancel, manual edit)
**NFRs:** NFR3.1 (PLACE TX), NFR3.5 (no partial-PLACE), NFR7.2 (10 jobs/min/crane)
**UX-DRs:** UX-DR4 (yard map source/target), UX-DR17 (visual states)

### Epic 4: Cabin Daily Operations (Reports + Diagnostics)

**User outcome:** Cabin operators have full daily-ops surface — work-orders, truck mode, container detail, suggested locations, expected containers, empty containers, no-location list, movements log, and RTG settings/diagnostics screen.

**Includes:** M2-M11 cabin screens, supporting API endpoints (work-orders, trucks, containers composite, reports, suggested-location scoring), numeric keypad component, RTG diagnostics integration with health checks.

**FRs covered:** FR2.6-2.11 (truck, work-orders, reports, suggested, container detail, RTG settings); FR2.15-2.16 (numpad + container picker); FR4.4-4.6 (locations, shifting, work-orders, trucks, containers, reports endpoints); FR2.18 (cabin self-update)
**UX-DRs:** UX-DR5-DR14 (M2-M11 screens), UX-DR24 (badge feature flag), UX-DR25 (deferred high-contrast)

### Epic 5: Forklift Operations

**User outcome:** Forklift drivers (logged in via Epic 1) view current container info, report PICK/PLACE activity, and use all 14 forklift screens. Offline-tolerant — if network drops momentarily, work queues locally and syncs on reconnect.

**Includes:** Forklift Flutter app full feature set (frmInformation through frmChangeForkliftNumber), ~80 sp_ForkLift* equivalents as REST endpoints, F-key keyboard shortcuts (F2-F12), Hive offline cache + write queue, forklift-specific design tokens extrapolated from cabin Modern + dedicated UX pass for layout (AR-23 closure).

**FRs covered:** FR3.2-3.15 (full forklift screens); FR3.17 (offline); FR3.18 (self-update); FR4.7 (forklift API ~80 endpoints)
**NFRs:** NFR4.3 (offline write-queue)
**UX-DRs:** UX-DR21 (forklift design system commission)

### Epic 6: Migration Bridge (Dual-Write to MSSQL)

**User outcome:** Downstream consumers (MIS, AuditWeb, WMS, legacy ForkliftApp) keep getting their data without disruption during the transition; pilot deploys without breaking those systems; drift detected within an hour.

**Includes:** `Rtg.DualWrite` Windows Service, MSSQL writer with idempotency, outbox processor with exponential backoff retry, `rtg.dual_write_outbox` schema, hourly reconciliation cron, `mssql_mirror.*` FDW views to legacy MSSQL, drift alerts, AuditWeb mirror.

**FRs covered:** FR5.5 (FDW), FR5.7 (reconciliation), FR8.2 (dual-write), FR8.3 (outbox retry), FR8.4 (drift alerts)
**NFRs:** NFR6.1-6.4, NFR8.2 (AuditWeb mirror)

### Epic 7: Observability & Operational Readiness

**User outcome:** Ops team has visibility into the new system — structured logs, 4 SLI metrics dashboards, alert rules. Incidents are diagnosable; SLAs trackable.

**Includes:** Serilog → Seq pipeline (Docker), Prometheus + Grafana (Docker), 4 SLI dashboards (listener throughput, API latency, dual-write lag, auth failures), Alertmanager rules, health check endpoints per service, 90-day retention + archive script.

**FRs covered:** FR4.10 (self-update endpoint plumbing); FR5.6 (retention); FR7.4 (health checks)
**NFRs:** NFR5.1-5.4 (full observability)

### Epic 8: Pilot Deployment (GOLD3 Cutover)

**User outcome:** GOLD3 crane operator switches from legacy `TOSService.exe` to the new system on a Saturday window. <30s rollback path validated. 1-4 weeks of dual-running and drift monitoring before scaling out.

**Includes:** VM provisioning (IT-side coordination), GitHub Actions self-hosted runner, Windows Service registration + recovery options + drain shutdown, `C:\Rtg\secrets\` lifecycle, NTP sync, firewall whitelist, runbooks (cutover, rollback, on-call, secret-rotation, AD sync troubleshooting), pre-cutover dry-run with chesimu, audit log review.

**FRs covered:** FR7.1-7.3 (Windows Services + drain); FR8.1 (GOLD3 pilot), FR8.5 (rollback), FR8.8 (dry-run); AR-3, AR-4, AR-10, AR-13-AR-16, AR-25, AR-26
**NFRs:** NFR1.5 (firewall whitelist); NFR4.1-4.5 (reliability validated); NFR6.3-6.4 (rollback validated); NFR7.4 (10× load test); NFR10.1-10.6

### Epic 9: Production Rollout (GOLD1 + GOLD2)

**User outcome:** All three cranes operate on the new system; the legacy listener stack on `192.6.8.52` is decommissioned in stages; MSSQL retirement plan begins as downstream consumers finish their own migrations.

**Includes:** GOLD1 cutover (post-pilot Saturday), GOLD2 cutover (subsequent Saturday), lessons-learned ledger, MSSQL retirement plan + downstream migration tracker, legacy decommissioning runbook (TOSService, TOSConsole1/2/3, KillToss1/2/3, ConsolesReRun, TosReRun).

**FRs covered:** FR7.5 (legacy supervision torn down); FR8.6 (rollout); FR8.7 (MSSQL retirement)

---

## Sequencing Rationale

**Epic 1 → 2 → 3 → 4** is the cabin-operator vertical: login → see crane → operate crane → daily ops surface. Each epic is shippable as a horizontal slice (chesimu in dev, real PLC in pilot).

**Epic 5** (Forklift) is parallelizable with Epic 4 (different user, different app), but requires Epic 1 (auth) + Epic 6 (dual-write to MSSQL for sp_ForkLift* read patterns).

**Epic 6** (Dual-Write) can start in parallel with Epic 2 once the schema is established — needs the rtg.* schema to exist but can grow independently.

**Epic 7** (Observability) can be developed alongside Epics 2-5 — instrumentation hooks land in those epics; dashboards/alerts come together in Epic 7.

**Epic 8** (Pilot Deployment) requires Epics 1-3 minimum (login + crane state + ops). Epics 4-7 ideally complete before pilot, but Epics 5 (forklift) and 9 (rollout) come after.

**Epic 9** (Production Rollout) is purely deployment + decom — depends on stable pilot.

---

## Epic 1: Operator Identity & Sign-in

Crane operators and forklift drivers authenticate via AD-mapped accounts with PIN/password; off-boarding works automatically; audit trail starts; the codebase has a working foundation.

### Story 1.1: Repository skeleton

As a **developer**,
I want the repo + .NET 10 solution + Flutter projects + dbmate scaffolding initialized,
So that I can clone, build, and run a no-op skeleton end-to-end.

**Acceptance Criteria:**

**Given** an empty `craines-tos/` directory
**When** I run the init commands documented in architecture §2.5
**Then** I have `server/Rtg.sln` with all 7 projects, `cabin/` and `forklift/` Flutter Windows apps, `db/migrations/` with dbmate config, `.github/workflows/` skeleton, `docker-compose.dev.yml`, `Directory.Build.props`, `Directory.Packages.props`, `global.json` pinning .NET SDK 10.x, and `README.md`.

**Given** the skeleton
**When** I run `dotnet build server/Rtg.sln`
**Then** all 7 projects build with 0 errors and 0 warnings (StyleCop+`dotnet format` clean).

**Given** the skeleton
**When** I run `cd cabin && flutter build windows --debug` and `cd forklift && flutter build windows --debug`
**Then** both apps produce runnable binaries.

### Story 1.2: Dev environment via docker-compose

As a **developer**,
I want PG 18.3 + Seq + Prometheus + Grafana running in Docker locally with `wal_level = replica` configured,
So that I can develop against real services without external dependencies and the PG config matches production.

**Acceptance Criteria:**

**Given** Docker installed
**When** I run `docker compose -f docker-compose.dev.yml up -d`
**Then** PG, Seq, Prometheus, and Grafana start and pass health checks within 30 seconds.

**Given** PG is up
**When** I check `SHOW wal_level`
**Then** it returns `replica` (configured in `ops/docker/postgresql.conf`).

**And** Seq is reachable at `http://localhost:5341`, Grafana at `http://localhost:3000`, PG at `localhost:5432`, and Prometheus at `http://localhost:9090`.

### Story 1.3: GitHub Actions CI workflows

As a **developer**,
I want CI workflows that build and test on every PR with format gates and migration drift checks,
So that broken code, formatting violations, and schema drift can never merge to main.

**Acceptance Criteria:**

**Given** a PR opens
**When** GitHub Actions runs `ci-server.yml`
**Then** it executes `dotnet format --verify-no-changes`, `dotnet build`, `dotnet test --filter Category!=Integration`, and integration tests with Testcontainers PG; all must pass.

**Given** a PR opens
**When** GitHub Actions runs `ci-flutter-cabin.yml` and `ci-flutter-forklift.yml`
**Then** each executes `dart format --output none --set-exit-if-changed .`, `flutter analyze`, `flutter test`, and `flutter build windows`; all must pass.

**Given** a PR opens
**When** `ci-db.yml` runs
**Then** it executes SQL lint via `sqlfluff` and a migration replay test (apply all migrations to clean PG); all must pass.

**And** PR cannot merge until all gates pass (branch protection enforced).

### Story 1.4: API foundation infrastructure

As a **developer**,
I want the `Rtg.Api` project preconfigured with OpenAPI auto-gen, RFC 7807 error responses, rate limiting, correlation IDs, Serilog structured logging, and Npgsql connection pooling,
So that all subsequent API endpoints inherit these capabilities consistently and require no per-endpoint setup.

**Acceptance Criteria:**

**Given** an API endpoint throws an unhandled exception
**When** the response returns to the client
**Then** the body is `application/problem+json` per RFC 7807 with `type`, `title`, `status`, `detail`, and `traceId` (=`X-Request-Id`) fields.

**Given** a request comes in without an `X-Request-Id` header
**When** middleware runs
**Then** a UUID v7 is generated, attached to the response header, and pushed into Serilog's log context for the request.

**Given** more than 100 read requests per minute from the same authenticated user
**When** the next request arrives
**Then** API responds with 429 Too Many Requests + RFC 7807 + `Retry-After` header.

**And** OpenAPI spec is available at `/swagger/v1/swagger.json` and Swagger UI at `/swagger` in dev environment only.

### Story 1.5: PG schema for users, refresh tokens, and audit log

As a **developer**,
I want PG migrations creating `rtg.users`, `rtg.refresh_tokens`, and `rtg.audit_log` (partitioned monthly),
So that authentication state and audit events can be persisted with proper indexing and retention.

**Acceptance Criteria:**

**Given** a fresh PG instance
**When** `dbmate up` runs
**Then** `rtg.users` exists with columns `id UUID PK DEFAULT uuidv7()`, `login_name VARCHAR(50) NOT NULL`, `ad_principal VARCHAR(100) NOT NULL`, `pin_hash VARCHAR(200) NOT NULL`, `is_active BOOLEAN NOT NULL DEFAULT true`, `created_at TIMESTAMPTZ NOT NULL DEFAULT now()`, `updated_at TIMESTAMPTZ NOT NULL DEFAULT now()`, with `uq_users_login_name` unique constraint and `idx_users_ad_principal` index.

**Given** the migration runs
**Then** `rtg.refresh_tokens` has `id`, `user_id` FK to `rtg.users(id)`, `token_hash`, `issued_at`, `expires_at`, `revoked_at NULLABLE`, with index on `(user_id, expires_at) WHERE revoked_at IS NULL`.

**Given** the migration runs
**Then** `rtg.audit_log` is range-partitioned by month on `created_at`, with the current month's partition pre-created; columns `id BIGSERIAL`, `actor_principal`, `action VARCHAR(64)`, `ip INET`, `metadata JSONB`, `created_at TIMESTAMPTZ`.

**And** every migration file has both `-- migrate:up` and `-- migrate:down` sections that work correctly (`dbmate down` then `up` returns to identical state).

### Story 1.6: Argon2id PIN hasher

As a **system**,
I want `IPinHasher` implemented with Argon2id (memory cost 64MB, 3 iterations, 4 parallelism),
So that PINs are stored hashed with industry-standard parameters.

**Acceptance Criteria:**

**Given** a 4-character or 7-character PIN/password string
**When** `Argon2idPinHasher.Hash(pin)` is called
**Then** it returns a string in standard Argon2id format starting with `$argon2id$v=19$m=65536,t=3,p=4$...`.

**Given** a hash and the original PIN
**When** `Verify(pin, hash)` is called
**Then** it returns `true` in constant time (no early-out for non-matching prefixes).

**Given** a hash and an incorrect PIN
**When** `Verify` is called
**Then** it returns `false`.

**And** `Rtg.Tests/Auth/Argon2idPinHasherTests.cs` achieves ≥90% line coverage of `Rtg.Auth/Pin/`.

### Story 1.7: RS256 JWT issuer and validator

As a **system**,
I want `IJwtIssuer` and `IJwtValidator` using RS256 (asymmetric), private key on server only, public key distributed,
So that tokens cannot be forged from a compromised cabin or forklift client.

**Acceptance Criteria:**

**Given** a verified user identity (`adPrincipal`, `roles`)
**When** `RsaJwtIssuer.IssueAccess(identity)` is called
**Then** it returns a JWT signed with RS256 with 15-minute expiry containing claims `sub`, `aud`, `iat`, `exp`, `principal`, `roles`, `jti`.

**Given** a JWT signed with the wrong key
**When** `RsaJwtValidator.Validate(jwt)` is called
**Then** it returns `Invalid(reason=InvalidSignature)`.

**Given** a JWT past its `exp`
**When** `Validate` is called
**Then** it returns `Invalid(reason=Expired)`.

**And** an integration test wires both into the API and verifies a valid JWT lets a request reach an endpoint while an invalid one returns 401.

### Story 1.8: Refresh token service with rotation and revocation

As a **system**,
I want `RefreshTokenService` that issues 8-hour refresh tokens, persists them hashed in `rtg.refresh_tokens`, rotates on use, and supports revocation,
So that long-lived sessions are revocable and stolen tokens have a bounded blast radius.

**Acceptance Criteria:**

**Given** a successful login
**When** `RefreshTokenService.Issue(userId)` is called
**Then** a row is inserted into `rtg.refresh_tokens` with hashed token + 8-hour expiry; the unhashed token is returned exactly once to the caller.

**Given** a refresh request with a valid refresh token
**When** `Refresh(token)` is called
**Then** the existing token is marked revoked, a new token is issued, and a new access token is returned (token rotation).

**Given** a logout request with the refresh token
**When** `Revoke(token)` is called
**Then** `revoked_at` is set; subsequent refresh attempts return 401.

**And** a CI test verifies that token hashes (not raw tokens) are stored — the raw token never appears in the DB.

### Story 1.9: Lockout tracker

As a **system**,
I want `LockoutTracker` that tracks failed login attempts per `(user, source_ip)` with a 5-attempts-in-15-minutes threshold and 30-minute lockout,
So that brute-force attacks are mitigated without locking out a fat-fingered legitimate operator.

**Acceptance Criteria:**

**Given** 5 failed login attempts within 15 minutes from `(david.cohen, 192.6.1.50)`
**When** the 6th attempt is made
**Then** API returns 429 Too Many Requests + RFC 7807 + `Retry-After: 1800`.

**Given** a lockout is active
**When** 30 minutes pass
**Then** the next attempt is allowed.

**Given** a successful login
**When** it succeeds
**Then** the failure counter for that `(user, ip)` resets to 0.

**And** unit tests cover concurrency (multiple attempts arriving simultaneously do not bypass the threshold).

### Story 1.10: AD LDAP sync cron service

As a **system**,
I want `AdUserSyncService` running every 15 minutes that queries AD via LDAP and upserts the `rtg.users` mirror,
So that AD remains the directory of truth and off-boarding propagates within 15 minutes (per AD-2 model).

**Acceptance Criteria:**

**Given** AD has user `david.cohen` in group `RTG-Operators`
**When** `AdUserSyncService` runs
**Then** `rtg.users` has a row with `login_name='david.cohen'`, `ad_principal='david.cohen@goldbond.local'`, `is_active=true`.

**Given** AD disables `david.cohen`
**When** the next sync runs
**Then** `rtg.users` row is updated to `is_active=false` (soft-disable, preserving audit references).

**Given** AD is unreachable
**When** sync runs
**Then** an error event is logged and an alert fires; the service does not crash; previous sync state in `rtg.users` is preserved.

**And** AD bind credentials are loaded from env vars only (`AD_BIND_USER`, `AD_BIND_PASSWORD` from `C:\Rtg\secrets\auth.env`); never from a config file checked into git.

### Story 1.11: Auth audit publisher

As a **system**,
I want `AuthAuditPublisher` that writes to `rtg.audit_log` for every login/logout/PIN attempt/lockout/refresh event, tied to the AD principal,
So that the audit trail is trustworthy (NFR8.1) and forgery-resistant.

**Acceptance Criteria:**

**Given** a successful login by `david.cohen`
**When** the API responds 200
**Then** an entry exists in `rtg.audit_log` with `actor_principal='david.cohen@goldbond.local'`, `action='login.success'`, `ip='192.6.1.50'`, `created_at=now()`, `metadata={"clientType":"cabin"}`.

**Given** a failed login attempt
**When** the API responds 401
**Then** an entry exists with `action='login.failure'`, reason in `metadata` (e.g., `{"reason":"invalid_pin"}`); the PIN value never appears.

**Given** any audit event
**When** I scan all audit_log rows
**Then** no row contains a PIN, password, JWT token, or refresh token (CI test verifies via regex against `metadata` and all string columns).

### Story 1.12: Auth API endpoints

As an **API client (cabin or forklift)**,
I want `POST /api/v1/auth/login`, `POST /api/v1/auth/refresh`, `POST /api/v1/auth/logout`,
So that I can authenticate, maintain my session, and sign out cleanly.

**Acceptance Criteria:**

**Given** a request `POST /api/v1/auth/login` with body `{loginName, pin, clientType}`
**When** credentials are valid and the user is AD-active
**Then** API returns 200 with `{accessToken, refreshToken, expiresInSeconds, user: {principal, displayName, roles}}`.

**Given** invalid credentials (wrong PIN, unknown user, locked-out user, AD-inactive)
**When** the request comes in
**Then** API returns 401 with RFC 7807 problem details with no information leakage about which factor failed (`title="Authentication failed"`).

**Given** a valid refresh token in body of `POST /api/v1/auth/refresh`
**When** the request comes in
**Then** API returns 200 with new `accessToken` + rotated `refreshToken`; old refresh token is now revoked.

**Given** `POST /api/v1/auth/logout` with valid auth header + refresh token in body
**When** the request comes in
**Then** API returns 204; the refresh token is revoked.

**And** all endpoints have OpenAPI annotations with examples; CI fails if any auth endpoint lacks annotations.

### Story 1.13: Cabin design system foundation

As a **cabin developer**,
I want `core/theme/CraneTheme` implementing all design tokens (Heebo + IBM Plex Mono fonts, full color palette, radii, shadows, spacing) per UX-DR1, plus reusable widgets `TouchButton`, `StatusPill`, `MonoNumeric`, `GlassPanel`, `HebrewText` per UX-DR2,
So that all subsequent cabin screens render consistently with the Modern variant without per-screen restyling.

**Acceptance Criteria:**

**Given** `pubspec.yaml` bundles Heebo (400-900) and IBM Plex Mono (400-700)
**When** the cabin app builds
**Then** Hebrew body text renders in Heebo and numeric IDs/timestamps in IBM Plex Mono.

**Given** `CraneTheme.light` is applied at the `MaterialApp` root
**When** any screen renders
**Then** all colors, radii, shadows match the Modern variant tokens documented in `RTG-Template/design_handoff_rtg_crane_operator/README.md` "Global Design Tokens (Modern)" — verified by golden-image tests.

**Given** `TouchButton` is rendered with `variant: primary`
**When** measured
**Then** height ≥ 60px; for `variant: secondary`, height ≥ 44px.

**And** widget tests exist for each of the 5 reusable widgets covering rendering, theming, and basic interaction.

### Story 1.14: Cabin Hebrew localization (he.arb + RTL)

As a **cabin developer**,
I want `flutter_localizations` + `intl` + `lib/core/l10n/he.arb` containing all login screen strings, plus `Directionality.rtl` at the app root,
So that the app is fully Hebrew RTL with zero hardcoded strings (NFR9).

**Acceptance Criteria:**

**Given** `he.arb` contains keys for every login screen string
**When** the app runs
**Then** all UI strings come from `S.of(context).<key>`, never inline; CI lint detects any inline Hebrew Unicode in `.dart` files and fails the build.

**Given** `Directionality(textDirection: TextDirection.rtl)` wraps `MaterialApp`
**When** any screen renders
**Then** layout flows right-to-left (text alignment, list arrows, navigation gestures).

**And** numeric IDs and timestamps render LTR-in-RTL via `MonoNumeric` widget.

### Story 1.15: Cabin login screen UI (Modern layout)

As a **crane operator**,
I want a login screen following the Modern UX bundle (top bar with logo + system pills + clock; right hero column with badge, gradient title, lede, 4-stat strip; left login card with username dropdown, PIN row, crane picker, action buttons, security footer; floating numeric keypad bottom-left; ambient background),
So that the visual experience matches the customer-approved design.

**Acceptance Criteria:**

**Given** the cabin app launches at 1920×1080
**When** the login screen renders
**Then** layout matches `design_handoff_login/README.md` "Screen anatomy" — top bar 112px, main grid 1.15fr/1fr with 44px gap, padding 14/64/32, floating keypad at bottom-left 380px wide.

**Given** the screen renders
**When** I inspect each component
**Then** the Goldbond logo (108px), divider, title "מערכת מנופאי RTG", build line, 4 system pills (PLC/GPS/TOS/רשת), clock, badge "מערכת חדשה · דור 4", hero gradient title, lede, 4-stat strip, login card, and ambient background all match Modern tokens (colors, radii, shadows, fonts).

**And** the screen renders without scrollbars at the target resolution and degrades gracefully at smaller resolutions (≥1280×720).

### Story 1.16: Cabin login screen behavior

As a **crane operator**,
I want the login screen to interactively handle username selection, PIN entry, crane selection, and login submission,
So that I can complete the login workflow with touch-only input.

**Acceptance Criteria:**

**Given** I tap the username dropdown
**When** the dropdown opens
**Then** it shows a scrollable list of operators (sourced from `GET /api/v1/users/operators` — populated by AD sync); selecting one closes the dropdown, sets `userId`, moves focus to PIN row, and the avatar tile shows the operator's initial.

**Given** the PIN row has focus
**When** I tap a digit on the floating numeric keypad
**Then** a dot fills in the PIN cell; CLR clears all dots; ← removes the last; max 4 digits; current caret cell shows teal halo.

**Given** I tap an available crane button (GOLD1/2/3)
**When** the crane is `available=true`
**Then** the button activates with teal background and shadow; cranes with `available=false` (in-maintenance) cannot be selected.

**Given** `userId && pin.length===4 && selectedCrane?.available`
**When** I tap "כניסה למערכת"
**Then** the cabin calls `POST /api/v1/auth/login`; on 200 navigate to yard map; on 401 show RTL error message and clear PIN.

**And** the time-aware greeting renders "בוקר טוב" / "צהריים טובים" / "ערב טוב" based on local time; the badge "מערכת חדשה · דור 4" is feature-flagged (default-on, hideable via config).

### Story 1.17: Cabin auth wiring (Dio interceptors + secure storage)

As a **cabin developer**,
I want `core/auth/` integrated with a Dio interceptor that attaches `Authorization: Bearer <accessToken>`, auto-refreshes on 401 once, and clears tokens on refresh failure, with `flutter_secure_storage` for token persistence,
So that all subsequent API calls in the cabin app are authenticated transparently.

**Acceptance Criteria:**

**Given** a successful login
**When** tokens are stored
**Then** `flutter_secure_storage` (Windows DPAPI-backed) holds `accessToken` + `refreshToken` under namespaced keys (`cabin.access`, `cabin.refresh`).

**Given** any subsequent API request
**When** Dio sends it
**Then** the `Authorization: Bearer <accessToken>` header is automatically attached.

**Given** an API response 401
**When** the auth interceptor runs
**Then** it calls `POST /api/v1/auth/refresh` once; on 200 retry the original request with the new token; on failure clear `flutter_secure_storage` and navigate to login.

**And** widget tests verify the interceptor flow with mocked Dio + mocked storage.

### Story 1.18: Forklift design system foundation

As a **forklift developer**,
I want `core/theme/ForkliftTheme` extrapolating from cabin Modern tokens (same color palette, typography, radii) but with denser layout for the smaller tablet form factor, plus the same reusable widgets,
So that the forklift app inherits a consistent visual language without a dedicated UX bundle (closes UX-DR21 / AR-23 for pilot).

**Acceptance Criteria:**

**Given** `ForkliftTheme.light` is applied at the forklift app root
**When** any screen renders
**Then** colors and typography match cabin Modern tokens.

**Given** the theme has reduced spacing scale for the smaller form factor
**When** widgets render
**Then** primary touch targets remain ≥44px; the layout fits in the forklift tablet's working resolution without horizontal scroll.

**And** ADR `docs/decisions/ADR-001-forklift-design-extrapolation.md` documents the decision, the trade-off accepted, and the trigger for commissioning a dedicated forklift UX pass post-pilot.

### Story 1.19: Forklift login screen UI and behavior

As a **forklift driver**,
I want a login screen with a username dropdown, 7-character masked password field, forklift number entry, terminal indicator (Ashdod, locked), and F-key shortcuts (F12 submit, F10 exit),
So that I can authenticate at shift start using the same physical-keyboard-friendly interaction model the legacy app trained drivers on.

**Acceptance Criteria:**

**Given** the forklift app launches
**When** the login screen renders
**Then** `txtPassword` is a `MaskedTextBox` (input obscured), forklift number input accepts only digits, terminal selector shows "אשדוד" disabled (Haifa out of scope per K6).

**Given** `loginName && password.length===7 && forkliftNumber`
**When** I press F12 or tap "אישור"
**Then** the app calls `POST /api/v1/auth/login` with `{loginName, password, clientType: "forklift", forkliftNumber}`.

**Given** local file `ForkLiftNumber.xml` exists in the app's data directory
**When** the login screen renders
**Then** the forklift number field auto-fills from the file; otherwise it's blank and required.

**And** F10 closes the app cleanly; F-key handlers are scoped to the login screen (no global shortcuts that could fire on background).

### Story 1.20: Forklift auth wiring

As a **forklift developer**,
I want `core/auth/` for forklift mirroring the cabin's Dio interceptor + secure storage + refresh logic, with namespaced storage keys to avoid interference if both apps run on the same machine,
So that authenticated API calls work the same way in both apps and tokens don't collide.

**Acceptance Criteria:**

**Given** a successful forklift login
**When** tokens are stored
**Then** `flutter_secure_storage` holds them under `forklift.access` + `forklift.refresh` keys (separate from cabin's `cabin.access` / `cabin.refresh`).

**Given** a 401 response on any forklift API call
**When** the auth interceptor runs
**Then** it calls `POST /api/v1/auth/refresh` once; on success retry; on failure clear forklift-namespaced storage and navigate to forklift login.

**And** widget tests verify the dual-app token namespacing: storing a token in one app's namespace does not leak to the other.

---

## Epic 2: Live Crane State (Wire Protocol + Real-time Map)

Cabin operators see their crane's live position and status on the yard map; the Listener processes A1 telemetry from chesimu (and later real PLC) into PG with proper framing/checksum/idempotency; UI updates via WebSocket push (replaces legacy 500ms polling).

### Story 2.1: V40 wire protocol decoder

As a **listener developer**,
I want `Rtg.WireProtocol/V40/Decoder.cs` parsing inbound frames per the KoneCranes V40 spec into typed records (`A1Position`, `A2PickDone`, `A2PlaceDone`, `A3CancelAck`),
So that all downstream handlers receive validated, strongly-typed data instead of raw byte slices.

**Acceptance Criteria:**

**Given** a valid A1 frame from chesimu
**When** `Decoder.Decode(bytes)` is called
**Then** it returns `Ok(A1Position)` with all fields populated per V40 offsets (Time/Status/HBBlockName/HBBayNumber/HBRowNumber/HBHeight/CraneStatus/GPSStatus/PLC/Len/TwistLock).

**Given** a frame missing the `??` prefix or with msg type ≠ A1/A2/A3
**When** `Decode` is called
**Then** it returns `Err(MalformedFrameException)` with reason in the message; no exception escapes.

**Given** a truncated frame (offsets out of range)
**When** `Decode` is called
**Then** it returns `Err(MalformedFrameException("offset out of range"))`; no exception escapes.

**And** `Rtg.Tests/WireProtocol/DecoderTests.cs` covers ≥90% of the file using captured chesimu frame fixtures stored in `Rtg.Tests/WireProtocol/Fixtures/`.

### Story 2.2: V40 wire protocol encoder

As a **listener developer**,
I want `Rtg.WireProtocol/V40/Encoder.cs` building outbound ACK and broadcast (B3/B4) frames per V40 spec,
So that the listener can respond to PLC events and dispatch jobs with byte-perfect frames.

**Acceptance Criteria:**

**Given** a counter value
**When** `Encoder.BuildA2Ack(counter)` is called
**Then** it returns bytes equal to `[0xFF, 0xFF, ...ASCII("04B2")...counter...checksum]` matching the V40 spec.

**Given** a job (`OutboundJob` record with crane, container, from/to coordinates)
**When** `Encoder.BuildB3PickBroadcast(job)` is called
**Then** it returns bytes per V40 layout for a B3 frame.

**And** golden tests verify byte-perfect output against fixtures captured from a known-good legacy listener interaction.

### Story 2.3: V40 checksum and length-prefix framing helpers

As a **listener developer**,
I want `Rtg.WireProtocol/V40/Checksum.cs` (additive-sum compatible with legacy + CRC16-CCITT for the new system) and `V40/Framing.cs` (length-prefix reader for `System.IO.Pipelines`),
So that frames are correctly delimited and verified end-to-end.

**Acceptance Criteria:**

**Given** an ASCII string
**When** `Checksum.AdditiveSum(s)` is called
**Then** it returns the legacy two's-complement-of-sum value, byte-equivalent to the legacy `calcChecksum` for any input.

**Given** a `ReadOnlySequence<byte>` with one or more length-prefixed frames
**When** `Framing.TryReadFrame(sequence, out frame)` is called
**Then** it reads the length byte at offset 1 and returns one complete frame; partial/incomplete data leaves the sequence positioned for the next read attempt.

**Given** two TCP-coalesced frames in one read buffer
**When** `Framing.TryReadFrame` is called twice
**Then** both frames are extracted in order; no data is lost.

### Story 2.4: PG schema for cranes, crane_positions, and LISTEN/NOTIFY

As a **developer**,
I want migrations creating `rtg.cranes` (3 pre-seeded rows: GOLD1/2/3 with their block codes) and `rtg.crane_positions` (partitioned monthly), plus a trigger publishing to channel `rtg.<crane_code>.position` on every INSERT,
So that crane state has both current and historical persistence with pub/sub-ready notifications.

**Acceptance Criteria:**

**Given** `dbmate up` runs on a fresh PG
**When** schema is checked
**Then** `rtg.cranes` exists with rows for `GOLD1` (BOND1), `GOLD2` (BOND2), `GOLD3` (BOND3) including `id`, `code`, `block_code`, `tcp_port`, `is_active`, `current_position` JSONB.

**Given** the migration runs
**Then** `rtg.crane_positions` is range-partitioned by month on `recorded_at` with the current month's partition pre-created; columns `id BIGSERIAL`, `crane_id`, `counter SMALLINT`, `bay`, `row`, `height`, `crane_status`, `gps_status`, `recorded_at TIMESTAMPTZ`.

**Given** a row is inserted into `rtg.crane_positions` for GOLD3
**When** a PG client `LISTEN rtg.gold3.position`
**Then** it receives a NOTIFY payload `{"id":<bigint>,"ts":"<iso>","type":"position"}`.

**And** the unique constraint `uq_crane_positions_crane_counter_idempotency` prevents duplicate inserts on `(crane_id, counter, recorded_at_truncated_to_second)` (idempotency at the DB layer).

### Story 2.5: Crane and crane-position repositories

As a **developer**,
I want `ICraneRepository` and `ICranePositionRepository` (Dapper-based) with current-state upsert and history insert operations,
So that the listener and API have a clean, parameterized data-access layer.

**Acceptance Criteria:**

**Given** a Dapper repository test with Testcontainers PG
**When** `CraneRepository.UpsertCurrentPosition(craneId, position)` is called
**Then** `rtg.cranes.current_position` is updated; if called twice with the same `(crane_id, counter)`, the second call is a no-op (idempotent).

**Given** the repository
**When** `CranePositionRepository.AppendHistory(craneId, counter, position)` is called
**Then** a row is inserted into `rtg.crane_positions`; duplicate `(crane_id, counter)` returns the existing row's id (idempotent via UNIQUE constraint).

**Given** the API handler
**When** `CraneRepository.GetCurrentState(craneId)` is called
**Then** it returns `Crane` with the latest `current_position` and `last_updated_at`.

### Story 2.6: System.IO.Pipelines-based TCP server

As a **listener developer**,
I want `Rtg.Listener/TcpServer/PipelinesTcpServer.cs` accepting TCP connections and delivering complete length-framed frames to a handler pipeline using `System.IO.Pipelines`,
So that the legacy `stream.Read(256)` no-framing model is replaced with a backpressure-aware, allocation-friendly receiver.

**Acceptance Criteria:**

**Given** the server bound to a port
**When** a TCP client sends a length-prefixed frame
**Then** the server reads using `PipeReader`, delegates to `Framing.TryReadFrame`, and invokes the registered handler with the parsed bytes.

**Given** the server is asked to stop via `IHostApplicationLifetime.StopAsync` (drain mode)
**When** there are in-flight reads
**Then** the server stops accepting new connections, finishes processing already-read frames, and exits within 30 seconds; if drain doesn't complete in 30s, the server force-closes (per FR7.3 + Gap #3).

**Given** the client TCP connection drops mid-frame
**When** `PipeReader` returns end-of-stream
**Then** the server logs the disconnect, releases the connection, and re-accepts (one connection per crane port).

### Story 2.7: A1 position handler

As a **listener**,
I want `Rtg.Listener/Handlers/A1PositionHandler.cs` that takes a decoded `A1Position`, upserts `rtg.cranes.current_position`, appends to `rtg.crane_positions` (idempotent on counter), and notifies via PG,
So that crane telemetry flows from the wire to PG to subscribers.

**Acceptance Criteria:**

**Given** an A1Position arrives for GOLD3
**When** `A1PositionHandler.Handle(message)` runs
**Then** `rtg.cranes` is updated and a row is inserted in `rtg.crane_positions`; PG fires NOTIFY on `rtg.gold3.position`.

**Given** the same A1 (same counter) arrives twice within 1 second
**When** the handler runs the second time
**Then** the unique constraint kicks in and the duplicate is silently absorbed; no second NOTIFY fires; the listener logs `duplicate_suppressed`.

**Given** the DB write fails (deadlock, connection lost)
**When** the handler retries via Polly (max 3 attempts with backoff)
**Then** it eventually succeeds or logs `handler_failed` + sends to DLQ.

### Story 2.8: Dead-letter queue for unknown frames

As a **listener**,
I want `Rtg.Listener/Handlers/DeadLetterHandler.cs` that captures malformed/unknown frames to `rtg.listener_dlq` table with raw bytes + decode error + crane + timestamp,
So that wire anomalies are debuggable post-hoc instead of silently dropped (FR1.8).

**Acceptance Criteria:**

**Given** a frame fails decoder validation
**When** the listener pipeline routes it to the DLQ handler
**Then** a row is inserted in `rtg.listener_dlq` with `crane_id`, `raw_bytes BYTEA`, `decode_error`, `received_at TIMESTAMPTZ`.

**Given** the DLQ is growing fast (>10 frames/minute)
**When** the alerting pipeline checks
**Then** a Grafana alert fires (Epic 7 wires this; Epic 2 only emits the metric).

**And** raw bytes in `rtg.listener_dlq` are masked in any log output (the DLQ is the audit trail; logs never print full PII-adjacent payloads).

### Story 2.9: Crane Listener worker service

As a **listener developer**,
I want `Rtg.Listener/Workers/CraneListenerWorker.cs` BackgroundService — one instance per crane (GOLD1/2/3) — that wires PipelinesTcpServer + Decoder + A1PositionHandler + DeadLetterHandler,
So that each crane has its own fault-isolated listener pipeline.

**Acceptance Criteria:**

**Given** the listener service starts
**When** `appsettings.Production.json` configures three cranes (GOLD1:30701, GOLD2:30702, GOLD3:30703)
**Then** three `CraneListenerWorker` BackgroundServices register and bind their respective ports.

**Given** GOLD2's PLC connection drops while GOLD1 and GOLD3 are healthy
**When** GOLD2's worker handles the disconnect
**Then** only GOLD2's worker re-accepts; GOLD1 and GOLD3 are unaffected.

**Given** the listener service receives `Stop` from Windows Service Control Manager
**When** drain shutdown is initiated
**Then** all 3 workers cooperatively drain (per Story 2.6); the service exits within 30s.

### Story 2.10: Cranes REST endpoints

As an **API client (cabin)**,
I want `GET /api/v1/cranes` (list state) and `GET /api/v1/cranes/{id}` (single crane),
So that I can fetch initial crane state on cabin startup before subscribing to live updates.

**Acceptance Criteria:**

**Given** an authenticated GET to `/api/v1/cranes`
**When** API responds
**Then** body is `{"items":[{"id","code":"GOLD1","blockCode":"BOND1","currentPosition":{...},"lastUpdatedAt":"..."},...],"totalCount":3}` with all 3 cranes.

**Given** GET `/api/v1/cranes/{nonexistent-id}`
**When** API responds
**Then** 404 with RFC 7807 problem details.

**And** OpenAPI annotations are present for both endpoints with example responses; CI verifies.

### Story 2.11: SignalR RtgHub with topic subscriptions

As an **API client (cabin)**,
I want SignalR hub `/hub/rtg` with methods `SubscribeToCrane(craneId)` and `UnsubscribeFromCrane(craneId)`, JWT-authenticated on connect,
So that cabin clients receive only the events they're interested in (one cabin = one crane).

**Acceptance Criteria:**

**Given** a cabin client connects to `/hub/rtg` with a valid JWT in the `Authorization` header
**When** `SubscribeToCrane("gold3")` is called
**Then** the client is added to SignalR group `crane:gold3`; the server tracks the subscription per connection.

**Given** the client disconnects (network drop or explicit close)
**When** the connection is lost
**Then** the server cleans up all of the client's group memberships.

**Given** an unauthenticated WebSocket attempt
**When** the connection is initiated
**Then** the hub rejects it with 401.

### Story 2.12: PG LISTEN → SignalR fanout (PgListenerHostedService)

As an **API service**,
I want `Rtg.Api/Hosted/PgListenerHostedService.cs` (BackgroundService) that LISTENs on per-crane PG channels and pushes typed events to SignalR groups,
So that any DB state change reaches subscribed cabin clients in <200ms (NFR2.1).

**Acceptance Criteria:**

**Given** the service starts
**When** it issues `LISTEN rtg.gold1.position`, `LISTEN rtg.gold2.position`, `LISTEN rtg.gold3.position` on a long-lived Npgsql connection
**Then** it stays connected; on PG disconnect it reconnects with exponential backoff.

**Given** a NOTIFY arrives on `rtg.gold3.position` with payload `{"id":12345,"ts":"...","type":"position"}`
**When** the service handles it
**Then** it loads the position row, builds a `PositionUpdateEvent` DTO, and calls `_hub.Clients.Group("crane:gold3").OnPositionUpdate(event)`.

**And** end-to-end test: insert a row in `rtg.crane_positions` → SignalR test client subscribed to `gold3` receives the `OnPositionUpdate` within 200ms p95 over 100 trials.

### Story 2.13: Cabin yard map screen layout (M1)

As a **crane operator**,
I want the M1 yard map screen rendering the top bar (with system pills, GPS HUD slot, clock), 2D yard grid (rows A-F × cols 94-106), bottom action area placeholder, and ambient background per UX-DR4,
So that I have the visual frame for live crane monitoring (interactivity comes in Epic 3).

**Acceptance Criteria:**

**Given** I'm logged in and the cabin app navigates to `/yard-map`
**When** the screen renders at 1920×1080
**Then** the layout matches `RTG-Template/design_handoff_rtg_crane_operator/README.md` M1 specification — top bar with crane ID + block + system pills + clock, 2D grid centered, bottom panels scaffolded as placeholders.

**Given** the grid renders
**When** I look at each cell
**Then** stack-height heatmap colors match UX-DR16 palette (empty `#fff`/`#cbd5e1` border through 6/6 `#fef2f2`/`#b91c1c`); cell radius 8px; spacing matches Modern tokens.

**And** the screen wraps a `Scaffold` with `Directionality.rtl` and the `CraneTheme` is applied; widget golden tests verify the layout.

### Story 2.14: Cabin live crane position indicator

As a **crane operator**,
I want a live position marker (`CraneIndicator` widget) overlaid on the yard grid that follows the latest A1 position from the listener,
So that I can see at a glance where the crane currently is and trust the system reflects reality.

**Acceptance Criteria:**

**Given** the yard map is open
**When** an `OnPositionUpdate` event arrives via SignalR
**Then** the indicator moves to the new (bay, row, height) cell with a smooth 200ms animation; the indicator shows the crane code (GOLD3) inline.

**Given** the position hasn't updated for >5s
**When** the indicator renders
**Then** it shows a "stale" visual hint (subtle amber pulse) per design language (telemetry trustworthiness signal).

**And** the indicator handles position updates at up to 5Hz without animation jank (≥60fps on the target hardware).

### Story 2.15: Cabin GPS HUD widget

As a **crane operator**,
I want a `GpsHud` widget on the top bar of every cabin screen showing fix type (RTK/DGPS/2D/NO FIX), satellite count, and accuracy in meters per UX-DR15,
So that I can trust (or distrust) the crane's reported position at a glance.

**Acceptance Criteria:**

**Given** the cabin receives GPS state in the `OnPositionUpdate` event (or via separate `OnGpsStatusUpdate`)
**When** the HUD renders
**Then** it shows the fix type label (e.g., `RTK · 14 לוויינים · ±2cm`) using Heebo for label + IBM Plex Mono for numerics.

**Given** GPS goes from `RTK` to `2D` for a single update
**When** the HUD updates
**Then** the status chip does NOT flash amber (debounced); it stays green for 2 consecutive degraded updates before chipping amber.

**Given** RTK is lost for 10 seconds
**When** the HUD renders
**Then** the chip shows amber pulse + "GPS: 2D · ±5m" ; reverts to green when RTK restored.

**And** widget tests cover the debounce logic at 1Hz, 5Hz, and burst rates.

### Story 2.16: Cabin yard map data wiring (Riverpod + SignalR)

As a **cabin developer**,
I want Riverpod providers `craneStateProvider(craneId)` and `yardMapProvider(blockCode)` that fetch initial state via REST and subscribe to SignalR for live updates, with auto-reconnect (exponential backoff up to 30s),
So that the yard map screen is reactive without per-component subscription boilerplate.

**Acceptance Criteria:**

**Given** the yard map screen mounts for `GOLD3` / `BOND3`
**When** the providers initialize
**Then** they call `GET /api/v1/cranes/gold3` + `GET /api/v1/blocks/BOND3/cells` (read in Epic 3) for initial state, then connect to SignalR `/hub/rtg` and call `SubscribeToCrane("gold3")`.

**Given** the SignalR connection drops
**When** the client reconnects
**Then** the client re-issues `SubscribeToCrane("gold3")` automatically (server-side subscription state is per-connection); a re-fetch of REST initial state is performed to close the gap.

**Given** the yard map screen unmounts
**When** Riverpod disposes the providers
**Then** `UnsubscribeFromCrane("gold3")` is called and the SignalR connection is released.

**And** integration test: simulate a server-pushed position update via test SignalR fixture → assert `craneStateProvider` rebuilds with the new value.

### Story 2.17: chesimu integration test + dev runbook

As a **listener developer**,
I want an integration test in `Rtg.Tests/Listener/ChesimuIntegrationTests.cs` that runs the full listener stack against captured chesimu frames (or live chesimu binary) and asserts end-to-end flow, plus a dev runbook `docs/runbooks/chesimu-dev.md` documenting how to install and configure chesimu locally,
So that wire-protocol changes can be validated against the vendor simulator without requiring access to a real PLC.

**Acceptance Criteria:**

**Given** the test bootstraps the listener pointing at a Testcontainers PG and uses the captured chesimu A1 fixture set
**When** frames are replayed against the listener via TCP
**Then** `rtg.cranes.current_position` updates, `rtg.crane_positions` accumulates rows, and `rtg.gold3.position` NOTIFYs fire.

**Given** the test asserts on the SignalR-side
**When** a position is replayed
**Then** a SignalR test client subscribed to GOLD3 receives the `OnPositionUpdate` event within 500ms.

**Given** a developer follows `docs/runbooks/chesimu-dev.md`
**When** they install chesimu, point it at `localhost:30703`, and start it
**Then** they can interactively send A1/A2/A3 frames and observe them flow into PG and out to a connected cabin client; the runbook documents each step explicitly.

**And** the chesimu fixture set is committed under `Rtg.Tests/WireProtocol/Fixtures/` for offline replay (CI can run without chesimu binary).

---

## Epic 3: Container Operations (PICK / PLACE / Cancel)

Cabin operators dispatch container moves to the crane via the yard map (FROM cell + TO destination), cancel pending jobs with hold-to-confirm, and override yard locations manually. Jobs flow listener → PLC → back; PG transactions guarantee no partial PLACE state.

### Story 3.1: PG schema for jobs, locations, shifting + LISTEN/NOTIFY triggers

As a **developer**,
I want migrations creating `rtg.jobs`, `rtg.locations`, `rtg.shifting` (partitioned monthly) with NOTIFY triggers on state-change events,
So that the persistence layer for container operations exists with proper indexing, idempotency constraints, and pub/sub plumbing.

**Acceptance Criteria:**

**Given** a fresh PG and `dbmate up` runs
**When** schema is checked
**Then** `rtg.jobs` exists with columns `id UUID PK DEFAULT uuidv7()`, `crane_id`, `container_id`, `from_block`, `from_location`, `to_block`, `to_location`, `truck_type NULLABLE`, `counter SMALLINT NULLABLE`, `status VARCHAR(20) NOT NULL DEFAULT 'PENDING'`, `message BYTEA NULLABLE`, `message_cancel BYTEA NULLABLE`, `a3_date`, `pick_date`, `place_date`, `cancel_date`, `created_by_principal`, `created_at`; with `idx_jobs_crane_status` and unique constraint `uq_jobs_crane_counter (crane_id, counter) WHERE counter IS NOT NULL`.

**Given** the migration runs
**Then** `rtg.locations` has `(block_code, location_code)` composite PK, `container_id NULLABLE`, `last_che NULLABLE`, `last_updated_at`; with index on `container_id WHERE container_id IS NOT NULL`.

**Given** the migration runs
**Then** `rtg.shifting` is partitioned by month on `shift_date`, with the current month's partition pre-created; columns `id BIGSERIAL`, `crane_id`, `container_id`, `from_block`, `from_location`, `to_block`, `to_location`, `operator_principal`, `shift_date`.

**And** triggers `tg_jobs_after_state_change_notify` (on jobs UPDATE → NOTIFY `rtg.<crane>.job`) and `tg_locations_after_update_notify` (on locations UPDATE → NOTIFY `rtg.<block>.location_updated`) are created with minimal payloads `{id, ts, type}`.

### Story 3.2: Job, location, and shifting repositories

As a **developer**,
I want `IJobRepository`, `ILocationRepository`, `IShiftingRepository` (Dapper-based) with the operations needed by the API and listener handlers,
So that all DB access for container operations is parameterized and tested.

**Acceptance Criteria:**

**Given** an integration test with Testcontainers PG
**When** `JobRepository.Submit(job)` is called
**Then** a row is inserted into `rtg.jobs` with `status='PENDING'`; the returned `Job` has the new `id` (UUID v7).

**Given** the repository
**When** `JobRepository.AssignCounter(jobId, counter)` is called from the dispatcher (Story 3.8)
**Then** `rtg.jobs.counter` and `status='SENT'` are updated atomically; calling again with the same counter is idempotent.

**Given** an A2 ack arriving from the listener
**When** `JobRepository.MarkPickDone(craneId, counter)` is called
**Then** `rtg.jobs.pick_date=now()`, `status='PICKED'`; if no matching row found returns `JobNotFoundException` (logged + DLQ-routed by the handler).

**And** `LocationRepository.GetCells(blockCode)` returns all locations for a block; `PatchLocation(blockCode, locationCode, container_id, principal)` writes the change with audit trail (calls `IAuditLogRepository.LogManualEdit`).

### Story 3.3: Jobs REST endpoints

As an **API client (cabin)**,
I want `POST /api/v1/jobs`, `GET /api/v1/jobs?craneId=...&status=...`, `DELETE /api/v1/jobs/{id}`,
So that cabin can submit PICK/PLACE jobs, list pending/in-flight ones, and request cancels.

**Acceptance Criteria:**

**Given** an authenticated `POST /api/v1/jobs` with body `{craneId, containerId, fromLocation: {block, code}, toLocation: {block, code}, truckType: nullable}`
**When** validation passes (FluentValidation: required fields, valid block codes, etc.)
**Then** API returns 201 with `Location: /api/v1/jobs/{id}` and body `{id, status: "PENDING", counter: null, ...}`.

**Given** `GET /api/v1/jobs?craneId=gold3&status=PENDING,SENT`
**When** the request is authenticated
**Then** API returns 200 with `{items:[...], totalCount:N}` filtered by query params.

**Given** `DELETE /api/v1/jobs/{id}` for a PENDING or SENT job
**When** the request is authenticated
**Then** API returns 202 (cancel requested); the job's `status` is updated to `CANCEL_REQUESTED` and a NOTIFY fires; eventual A3 from PLC closes the cycle.

**Given** `DELETE /api/v1/jobs/{id}` for an already-completed job
**When** the request runs
**Then** API returns 409 Conflict (RFC 7807 with `"job is in terminal state"`).

### Story 3.4: Locations REST endpoints

As an **API client (cabin)**,
I want `GET /api/v1/blocks/{code}/cells` (yard map data) and `PATCH /api/v1/locations/{block}/{code}` (manual edit),
So that cabin can render the live yard grid and operators can override cell content when GPS auto-locate fails.

**Acceptance Criteria:**

**Given** authenticated `GET /api/v1/blocks/BOND3/cells`
**When** the request runs
**Then** API returns 200 with `{items:[{blockCode, locationCode, containerId, lastChe, lastUpdatedAt, stackHeight}], totalCount:N}` covering all cells in the block.

**Given** authenticated `PATCH /api/v1/locations/BOND3/094A1` with body `{containerId: "INKU6436694"}`
**When** validation passes (container exists in mssql_mirror or is null)
**Then** API returns 200; `rtg.locations` is updated; an audit entry is written with `actor_principal` and `metadata={"reason":"manual_edit","oldValue":"...","newValue":"..."}`.

**And** PATCH requires the cabin operator to have `manual_edit_locations` permission (AD group `RTG-SeniorOperators`); regular operators get 403.

### Story 3.5: Listener — A2 PICK handler

As a **listener**,
I want `Rtg.Listener/Handlers/A2PickHandler.cs` that processes A2 03 frames by marking the matching job as picked and acking the PLC,
So that the cabin sees the PICK confirmation and the audit trail records who and when.

**Acceptance Criteria:**

**Given** an `A2PickDone` arrives for GOLD3 with counter=42
**When** `A2PickHandler.Handle(message)` runs
**Then** `rtg.jobs.pick_date=now()`, `status='PICKED'` for `(crane=GOLD3, counter=42)`; an audit entry is written with `action='job.picked'` tied to the job's `created_by_principal`.

**Given** the A2 03 references a non-existent job (counter mismatch — likely a retransmit after we already moved on, or a wire glitch)
**When** the handler runs
**Then** the frame is sent to DLQ with reason `unmatched_counter`; PLC receives an ACK regardless (so PLC doesn't keep retransmitting).

**Given** an A2 03 with the same counter arrives twice within 1 second
**When** the handler runs the second time
**Then** the second is suppressed via idempotency on `(crane_id, counter, msg_type)`; PLC still gets a fresh ACK.

### Story 3.6: Listener — A2 PLACE handler (single PG transaction)

As a **listener**,
I want `Rtg.Listener/Handlers/A2PlaceHandler.cs` that wraps all DB writes (jobs.place_date, shifting INSERT, locations clear-from + set-to, dual-write outbox emit via `IPostCommitDualWriter` interface) in a single PG transaction,
So that the legacy 5-table inconsistency window (brownfield edge case #12) is closed permanently per NFR3.1.

**Acceptance Criteria:**

**Given** an `A2PlaceDone` for `(GOLD3, counter=42, container=INKU6436694, fromLoc=BOND3:094A1, toLoc=BOND3:095B2)`
**When** `A2PlaceHandler.Handle(message)` runs successfully
**Then** within ONE PG transaction: `rtg.jobs.place_date=now()` & `status='PLACED'`; INSERT into `rtg.shifting` for the move; UPDATE `rtg.locations` clearing the FROM cell; UPDATE `rtg.locations` setting the TO cell with the container; `IPostCommitDualWriter.Enqueue(events)` is called (no-op stub in Epic 3, real in Epic 6); ACK sent to PLC.

**Given** the second `UPDATE rtg.locations` (set-to) fails with a deadlock
**When** the handler runs
**Then** the entire transaction rolls back (jobs.place_date stays null, shifting row not inserted, locations FROM-clear is reverted); the message is logged as `place_handler_failed` and routed to DLQ; PLC does NOT receive an ACK (so it retransmits).

**Given** the same A2 04 with the same counter arrives twice (after a retransmit)
**When** the handler runs the second time
**Then** the idempotency check finds `pick_date IS NOT NULL AND place_date IS NOT NULL` for the existing job and short-circuits; PLC still receives an ACK.

**And** `IPostCommitDualWriter` is registered in DI as `NullPostCommitDualWriter` for Epic 3 (no-op stub); a comment in `Rtg.Listener/Program.cs` notes that Epic 6 swaps in the real outbox-backed implementation.

### Story 3.7: Listener — A3 cancel handler

As a **listener**,
I want `Rtg.Listener/Handlers/A3CancelHandler.cs` that processes A3 frames by updating job state to `CANCELLED` (after first marking `a3_date` if not yet set),
So that operator-initiated cancels close cleanly and the cabin gets the confirmation.

**Acceptance Criteria:**

**Given** an `A3CancelAck` arrives for `(GOLD3, counter=42)` for a job in `CANCEL_REQUESTED` state
**When** the handler runs
**Then** in one PG transaction: `rtg.jobs.cancel_date=now()`, `status='CANCELLED'`; `a3_date` is also set if it was null (matches legacy two-stage update); a NOTIFY fires.

**Given** an A3 arrives but the job is already `PLACED` (i.e., the operator clicked cancel after the crane completed the move)
**When** the handler runs
**Then** the cancel is logged as `cancel_too_late` and the job state is unchanged; an audit entry records the attempt.

### Story 3.8: Listener — Outbound job dispatcher

As a **listener**,
I want the `CraneListenerWorker` extended with an outbound dispatcher path that LISTENs on `rtg.<crane>.job`, fetches PENDING and CANCEL_REQUESTED jobs, encodes B3/B4/cancel frames via `Rtg.WireProtocol.Encoder`, and writes them to the crane's TCP connection,
So that cabin job submissions reach the PLC.

**Acceptance Criteria:**

**Given** the cabin POSTs a new PICK job for GOLD3
**When** the API inserts into `rtg.jobs` and PG fires NOTIFY on `rtg.gold3.job`
**Then** the GOLD3 worker receives the notify within 100ms, fetches the job, assigns the next sequential counter via `JobRepository.AssignCounter`, encodes a B3 frame, writes to the TCP connection.

**Given** a cabin DELETE on a SENT job
**When** the API updates `status=CANCEL_REQUESTED` and notifies
**Then** the worker fetches the job, encodes a cancel frame from `message_cancel`, writes to TCP.

**Given** the PLC's TCP connection is dropped while the worker has a job to dispatch
**When** the dispatcher attempts the write
**Then** the write fails; the job stays in `PENDING` (counter not yet committed); on next reconnect the worker re-attempts dispatch (no work lost).

**And** sequential counter assignment uses an advisory lock per crane to prevent two workers from assigning the same counter (defensive — should never happen with one BackgroundService per crane, but cheap insurance).

### Story 3.9: Cabin yard map — cell selection + source/target panels

As a **crane operator**,
I want to tap a yard cell to select it as my FROM (source), then tap another to select it as my TO (target), with both selections shown in the bottom panels of M1,
So that I can compose a PICK/PLACE job visually before submitting.

**Acceptance Criteria:**

**Given** the yard map is open with no selection
**When** I tap a cell containing a container
**Then** the cell highlights as source (per UX-DR17 amber-gradient + solid border + "מקור" label) and the bottom-left source panel populates with the container info (ID, weight, customer, etc. from `GET /api/v1/containers/{id}` — composite query using mssql_mirror).

**Given** a source is selected
**When** I tap an empty cell
**Then** the cell highlights as target (UX-DR17 teal-gradient + dashed border + "→h+1" label); the bottom-right target panel populates with the cell info (block, coordinates, recommended height).

**Given** I tap a cell that's already selected as source
**When** the tap fires
**Then** the source clears; target also clears (TO requires FROM); panels reset.

**And** if I tap a cell that's not a valid target (e.g., locked, occupied when source is also non-empty meaning PICK-into-stack), the tap is rejected with a brief Hebrew toast hint ("יעד לא תקף") via the `he.arb` catalog.

### Story 3.10: Cabin yard map — source/target tile visual states

As a **crane operator**,
I want the source and target tiles on M1 to render with the precise visual states defined in UX-DR17 (source: amber 135° gradient + 2.5px solid amber border + "מקור" label; target: teal 135° gradient + 2.5px **dashed** teal border + "→h+1" label),
So that the active job is unambiguous at a glance even in bright sunlight.

**Acceptance Criteria:**

**Given** a source cell is set
**When** the cell renders
**Then** its visual matches UX-DR17 — amber gradient, 2.5px solid amber border, "מקור" label centered; gold standard verified by widget golden-image test.

**Given** a target cell is set
**When** the cell renders
**Then** its visual matches UX-DR17 — teal gradient, 2.5px dashed teal border, "→h+1" label centered; gold standard verified.

**Given** both source and target are cleared
**When** cells re-render
**Then** they revert to their stack-height heatmap colors per UX-DR16.

### Story 3.11: Cabin — submit job ("אישור" action)

As a **crane operator**,
I want the "אישור" button on the M1 action bar to be enabled only when both source and target are set, and on tap to POST `/api/v1/jobs` with the selected pair, then update the UI optimistically,
So that I can dispatch the move with one tap and see immediate feedback.

**Acceptance Criteria:**

**Given** source and target are both set and valid
**When** the action bar renders
**Then** "אישור" is enabled (teal gradient + shadow); when source or target is unset, "אישור" is disabled (gray + opacity 0.65).

**Given** I tap "אישור"
**When** the cabin calls `POST /api/v1/jobs`
**Then** on 201 the source/target tiles transition to "in-flight" visual hint (subtle pulse), the bottom panels show `status: "PENDING"`, and the action bar pivots to show "ביטול" enabled.

**Given** the API returns 4xx
**When** the cabin handles the error
**Then** an RTL error toast appears with the Hebrew message from `he.arb`; the source/target stay selected so the operator can correct and retry.

**And** the optimistic update reverts if SignalR doesn't deliver a job-state event within 5 seconds (timeout protection).

### Story 3.12: Cabin — cancel job (hold-to-confirm "ביטול")

As a **crane operator**,
I want the "ביטול" button to require a 2-second hold-to-confirm gesture (per legacy parity for accidental-tap protection) and on confirm DELETE `/api/v1/jobs/{id}`,
So that I don't accidentally cancel an in-flight job by brushing the screen.

**Acceptance Criteria:**

**Given** an in-flight job and the operator presses "ביטול"
**When** they hold the button
**Then** a circular progress arc fills around the button over 2 seconds (visual hold-feedback per UX); release before 2s aborts.

**Given** the operator holds for the full 2 seconds
**When** the gesture completes
**Then** the cabin calls `DELETE /api/v1/jobs/{id}` and the UI shows `status: "CANCEL_REQUESTED"`; on PLC A3 ack arriving via SignalR, status flips to `CANCELLED` and source/target tiles clear.

**Given** the API returns 409 (job already terminal)
**When** the response handles
**Then** an RTL toast says the job already completed; UI fetches latest state.

### Story 3.13: Cabin — manual location editor

As a **senior crane operator**,
I want a "עדכון איתור" flow accessible from a cell context menu (long-press on a yard cell) where I can manually set or clear the container at that location,
So that I can correct GPS auto-locate failures without losing operational context.

**Acceptance Criteria:**

**Given** I long-press a cell
**When** the context menu appears
**Then** it offers "עדכון איתור" + "הצג פרטי מכולה" (the latter goes to M5 in Epic 4); only senior operators see "עדכון איתור" (others get "הצג פרטי מכולה" only).

**Given** I tap "עדכון איתור"
**When** the editor opens
**Then** it shows a small modal with the cell coordinates locked + a container ID input (with autocomplete from `mssql_mirror.containers`) + Save/Cancel actions.

**Given** I save with a valid container ID
**When** the cabin calls `PATCH /api/v1/locations/{block}/{code}`
**Then** API responds 200, the cell on M1 updates immediately via SignalR `OnLocationUpdated`, and an audit entry is written with `metadata={"reason":"manual_edit","oldValue":"...","newValue":"..."}`.

**And** clearing a cell (saving with empty container ID) calls the same PATCH with `containerId: null`.

### Story 3.14: End-to-end PICK/PLACE/Cancel chesimu integration test

As a **listener developer**,
I want an end-to-end integration test in `Rtg.Tests/Listener/E2EPickPlaceTests.cs` that submits a job via the API, observes the listener dispatching to chesimu, replays a PICK ack and a PLACE ack from chesimu, and asserts state at every step,
So that the full happy path is validated before pilot deploy.

**Acceptance Criteria:**

**Given** the test harness with Testcontainers PG, the listener service, the API, and a SignalR test client subscribed to GOLD3
**When** `POST /api/v1/jobs` submits a PICK
**Then** within 200ms the chesimu test endpoint receives the B3 frame; the job in `rtg.jobs` has `status='SENT'` with a counter assigned.

**Given** the test replays the chesimu A2 03 ack
**When** the listener processes it
**Then** `rtg.jobs.pick_date IS NOT NULL`, `status='PICKED'`, and the SignalR test client receives `OnJobStateChange{state:"PICKED"}` within 200ms.

**Given** the test replays the chesimu A2 04 ack
**When** the listener processes it
**Then** in ONE PG transaction: `place_date IS NOT NULL`, `status='PLACED'`, a row in `rtg.shifting`, and `rtg.locations` has the container at the new cell with the old cell cleared.

**Given** a separate test submits a job, gets it dispatched, then DELETEs it
**When** the test replays the chesimu A3 ack
**Then** `rtg.jobs.cancel_date IS NOT NULL`, `status='CANCELLED'`.

**And** the test asserts NFR7.2 (sustain 10 jobs/min/crane for 5 minutes) without errors or back-pressure failures — runs on CI nightly, not per-PR.

---

## Epic 4: Cabin Daily Operations (M2-M11 + Supporting APIs)

Cabin operators have full daily-ops surface — main menu, work-orders, truck mode, container detail, suggested locations, expected containers, empty/no-location, movements log, RTG settings/diagnostics. Most screens are read-side queries against PG `rtg.*` and `mssql_mirror.*` (FDW views).

### Story 4.1: Cabin self-update endpoint

As a **cabin developer**,
I want `GET /api/v1/clients/cabin/version` returning `{latest, minRequired, downloadUrl}` and a CI build that publishes new cabin builds to the API's static asset store,
So that cabin apps auto-update at startup + hourly per FR2.18 / FR4.10.

**Acceptance Criteria:**

**Given** GET `/api/v1/clients/cabin/version`
**When** unauthenticated
**Then** API returns 200 with `{latest: "1.2.3", minRequired: "1.0.0", downloadUrl: "https://api.../cabin-1.2.3.zip", releaseNotesUrl: "..."}`.

**Given** the cabin app starts with `currentVersion < minRequired`
**When** the version check runs
**Then** the cabin shows a forced-update modal blocking all interaction; on tap, downloads the new build.

**Given** `currentVersion < latest && currentVersion >= minRequired`
**When** the version check runs
**Then** an unobtrusive update toast appears with "עדכן עכשיו" / "מאוחר יותר" actions; defer is allowed but reminded hourly.

**And** GitHub Actions `cd-staging.yml` publishes new cabin builds to a versioned URL on the API host; the API's version response reads from a manifest file.

### Story 4.2: Composite container detail endpoint

As an **API client (cabin)**,
I want `GET /api/v1/containers/{id}` returning full container detail joining `mssql_mirror.CO_Containers` + `CO_ContainerProfile` + `CP_Deal` + `TC_Client` + `TB_Drivers` (legacy MSSQL data via FDW),
So that the M5 container detail screen has all fields in one round-trip.

**Acceptance Criteria:**

**Given** authenticated GET `/api/v1/containers/INKU6436694`
**When** the container exists in `mssql_mirror.CO_Containers`
**Then** API returns 200 with `{id, size, type, handlingCode, currentLocation, weight, capacity, customer, line, dealNumber, riskCode, releaseInfo, ...}` — all the fields M5 specifies.

**Given** the container ID isn't in MSSQL
**When** the request runs
**Then** API returns 404 with RFC 7807.

**And** the response p95 latency is < 500ms (joins 4-5 large tables; verified via load test).

### Story 4.3: Truck queues endpoint

As an **API client (cabin)**,
I want `GET /api/v1/cranes/{id}/trucks` returning truck bays + unload queue + load queue from `mssql_mirror.V_ContainerUnloadRG` and `V_RTGLoad`,
So that the M6 truck load/unload screen has its data in one call.

**Acceptance Criteria:**

**Given** authenticated GET `/api/v1/cranes/gold3/trucks`
**When** the request runs
**Then** API returns 200 with `{bays: [...], unloadQueue: [...], loadQueue: [...]}` filtered for the crane's responsibility area.

**Given** there are no active trucks
**When** the request runs
**Then** API returns 200 with empty arrays (not 204) — the cabin always renders the screen layout.

### Story 4.4: Work orders endpoint

As an **API client (cabin)**,
I want `GET /api/v1/work-orders?blockCode=BOND3&status=open` returning open work orders from `mssql_mirror.CP_Order` joined with container/customer info,
So that the M3 jobs list and the global "עבודות" view both have the data they need.

**Acceptance Criteria:**

**Given** authenticated GET `/api/v1/work-orders?blockCode=BOND3`
**When** the request runs
**Then** API returns 200 with `{items: [{containerId, size, type, weight, currentLocation, destination, taskDescription, dueAt, status}, ...], totalCount}`.

**Given** the query includes `?status=open,in-progress`
**When** the request runs
**Then** API filters to those statuses (default: `open`).

**And** results are paginated (default pageSize=50, max 200) with `?page=N&pageSize=M`.

### Story 4.5: Reports endpoints (empty + no-location + expected)

As an **API client (cabin)**,
I want `GET /api/v1/reports/empty-containers`, `GET /api/v1/reports/no-location`, `GET /api/v1/reports/expected-containers`,
So that the M9 + M8 screens have their data sources.

**Acceptance Criteria:**

**Given** GET `/api/v1/reports/empty-containers?blockCode=BOND3`
**When** the request runs
**Then** API returns 200 with `{items: [{containerId, location, daysInYard, line, customer}, ...]}` — sourced from `mssql_mirror.CO_Containers WHERE EmptyContainer=1 OR HandlingTypeCode='EM'`.

**Given** GET `/api/v1/reports/no-location?blockCode=BOND3`
**When** the request runs
**Then** API returns 200 with containers where `LocationCode IS NULL` plus a `reason` field per row (e.g., `"GPS לא עודכן"`, `"AUTO-ID נכשל"`).

**Given** GET `/api/v1/reports/expected-containers?craneId=gold3&windowMinutes=60`
**When** the request runs
**Then** API returns 200 with `{items: [{containerId, eta, line, customer, size, type, truckBay, status}, ...]}` ordered by ETA ascending.

### Story 4.6: Movements log endpoint

As an **API client (cabin)**,
I want `GET /api/v1/shifting?craneId=gold3&from=2026-04-26T00:00:00Z&to=2026-04-26T23:59:59Z`,
So that M10 (movements log) can render today/yesterday/this-week/custom-range filters.

**Acceptance Criteria:**

**Given** authenticated GET `/api/v1/shifting?craneId=gold3&from=...&to=...`
**When** the request runs
**Then** API returns 200 with `{items: [{id, time, containerId, fromBlock, fromLocation, toBlock, toLocation, operator, status}, ...], totalCount}` ordered by `shiftDate DESC`, paginated.

**Given** range > 7 days
**When** the request runs
**Then** API caps the response at the most recent 7 days' worth and includes a `truncated: true` field with a hint to narrow the range.

### Story 4.7: Suggested locations scoring + endpoint

As an **API service**,
I want `GET /api/v1/locations/suggested?containerId=...&craneId=...` that runs a scoring algorithm (proximity to current crane position, stack-height fit, customer-grouping, deal-grouping, dangerous-goods compatibility) and returns the top 6 candidates with 0-100 scores,
So that the M7 suggested locations screen can show ranked options.

**Acceptance Criteria:**

**Given** authenticated GET `/api/v1/locations/suggested?containerId=INKU6436694&craneId=gold3`
**When** the request runs
**Then** API returns 200 with `{items: [{locationCode, blockCode, row, col, stackLevel, score: 0-100, reason: "..."}, ...]}` (max 6 items, sorted by score DESC).

**Given** no valid locations exist (block full / crane out of reach)
**When** the request runs
**Then** API returns 200 with empty `items` and a `noSuggestionsReason` field.

**And** the scoring algorithm is documented as a separate `Rtg.Persistence/SuggestedLocations/ScoringAlgorithm.cs` class with explicit weights (proximity 30%, stack-fit 25%, customer-group 20%, deal-group 15%, DG-compat 10%); weights are configurable via `appsettings`.

### Story 4.8: RTG diagnostic endpoints

As an **API client (cabin)**,
I want endpoints for the M11 settings/diagnostics screen — health checks, GPS recalibrate, PLC test, TOS sync, maintenance mode toggle, reboot,
So that operators can self-diagnose and take recovery actions.

**Acceptance Criteria:**

**Given** authenticated GET `/api/v1/cranes/{id}/diagnostics`
**When** the request runs
**Then** API returns 200 with `{plc: {connected, lastFrameAt}, gps: {fixType, satellites, accuracyMeters, lat, lng, alt, heading, speed, lastUpdateAt}, hmi: {connected, version}, tosServer: {reachable, latencyMs}}`.

**Given** authenticated POST `/api/v1/cranes/{id}/diagnostics/recalibrate-gps` (senior operator only)
**When** the request runs
**Then** API forwards a recalibration command to the listener, which writes a calibrate frame to the PLC; returns 202 Accepted with a polling URL for the result.

**Given** POST `/api/v1/cranes/{id}/diagnostics/maintenance-mode` (senior operator only)
**When** the request runs
**Then** the crane state is flipped to `maintenance`; new jobs are rejected; in-flight jobs are paused; cabin shows the maintenance banner.

**And** all diagnostic-action endpoints require AD group `RTG-SeniorOperators`; regular operators see read-only diagnostics.

### Story 4.9: Reusable NumericKeypad widget

As a **cabin developer**,
I want `cabin/lib/widgets/numeric_keypad.dart` implementing the M4 keypad (3×4 grid: 1-9 / CLR-0-← / digits white + CLR red bg + ← amber bg + 26px/700 typography) per UX-DR19,
So that the keypad is reusable across login (Epic 1), M4, and any future numeric-input screens.

**Acceptance Criteria:**

**Given** the widget is rendered with `onDigit`, `onClear`, `onBackspace`, `currentValue` callbacks
**When** I tap "5"
**Then** `onDigit(5)` fires.

**Given** `CLR` is tapped
**When** the press completes
**Then** `onClear()` fires.

**Given** `←` is tapped
**When** the press completes
**Then** `onBackspace()` fires.

**And** widget tests verify each key's color, size, and behavior; the keypad's visual matches UX-DR19 via golden-image test.

### Story 4.10: M2 Main menu screen

As a **crane operator**,
I want a main menu screen (M2) with 8 action tiles in a 4×2 grid, each with category accent stripe, navigating me to the corresponding sub-screen,
So that I have a hub to access all daily-ops surfaces from M1's "תפריט" button.

**Acceptance Criteria:**

**Given** I tap "תפריט" on M1
**When** I navigate
**Then** M2 renders with 8 tiles per UX-DR5: Empty containers / No-location / Log / Containers-by-block / Update location / Suggested / Expected / RTG settings; each tile has its accent stripe color and a one-line Hebrew description.

**Given** I tap a tile
**When** the navigation fires
**Then** I'm routed via `go_router` to the corresponding screen (M9 left for Empty, M9 right for No-location, M10 for Log, etc.).

**And** an "X" close button returns to M1; the "Update location" tile only appears for senior operators.

### Story 4.11: M3 Jobs list screen

As a **crane operator**,
I want the M3 jobs list screen — filterable table with block tabs (BOND1/BOND2/BOND3), columns for container/size/type/weight/from/to/task/time/action, and a search input,
So that I can see all open work and pick what to do next.

**Acceptance Criteria:**

**Given** I navigate to M3
**When** the screen mounts
**Then** it calls `GET /api/v1/work-orders?blockCode=<currentCraneBlock>` and renders the table; block tabs let me filter by BOND1/BOND2/BOND3.

**Given** I type in the search input
**When** the value changes (debounced 300ms)
**Then** the table filters client-side by container ID / customer / line.

**Given** I tap an "action" button on a row
**When** the action fires
**Then** the row's container becomes the source on M1 (deep-linked back) — the operator can then pick a target and submit.

### Story 4.12: M4 Numpad + container picker screen

As a **crane operator**,
I want the M4 dual-pane screen — numeric keypad on the left, filtered container list on the right (filtered by the block code typed on the keypad),
So that I can find a specific container quickly when I know the block code.

**Acceptance Criteria:**

**Given** I navigate to M4
**When** the screen mounts
**Then** the left pane shows the reusable `NumericKeypad` and the right pane is empty with a placeholder ("הקש קוד גוש לסינון").

**Given** I type "094"
**When** the input changes (debounced 300ms)
**Then** the right pane fetches `GET /api/v1/blocks/{currentBlock}/cells?locationPrefix=094` (or similar) and renders matching containers; tapping a row navigates to M5 container detail.

**Given** I tap CLR
**When** the field clears
**Then** the right pane resets to its placeholder.

### Story 4.13: M5 Container detail + deal containers screen

As a **crane operator**,
I want the M5 detail screen — left column showing all containers in the same deal (sister containers), right column showing the full detail panel with hero treatment for the container ID,
So that I can verify the right container and see related shipping context.

**Acceptance Criteria:**

**Given** I navigate to M5 with a container ID
**When** the screen mounts
**Then** it calls `GET /api/v1/containers/{id}` for the detail panel and `GET /api/v1/containers/{id}/sisters` (returns all containers sharing the same `dealNumber`) for the left column.

**Given** the detail panel renders
**When** I look at it
**Then** the container ID is in hero treatment (teal gradient, mono font, large) and all fields render per UX-DR8 (size, type, handling code, location, weight, capacity, customer, risk, job, release info).

**Given** I tap "בחר מכולה זו" (green action button)
**When** the action fires
**Then** the container becomes the source on M1 (deep-link back to yard map with the container preselected).

### Story 4.14: M6 Truck load/unload screen

As a **crane operator**,
I want the M6 truck mode screen — top section showing truck bays (driver/plate/wait time), middle table showing the unload queue (amber header, ↓), bottom table showing the load queue (teal header, ↑),
So that I can see what truck work is in front of me and prioritize.

**Acceptance Criteria:**

**Given** I navigate to M6
**When** the screen mounts
**Then** it calls `GET /api/v1/cranes/{id}/trucks` and renders the 3-section layout per UX-DR9.

**Given** new truck data arrives via SignalR (Epic 7-territory? or pulled?)
**When** the cabin handles the event
**Then** the screen updates without losing scroll position; for now the screen polls every 30s if SignalR push isn't yet available.

**And** tapping a row in the unload table makes that container the source on M1 (deep-link); tapping a row in the load table sets up a PLACE-into-truck job (pre-fills the target with the truck bay code).

### Story 4.15: M7 Suggested locations screen

As a **crane operator**,
I want the M7 suggested locations screen — 3×2 grid of ranked cards with "מומלץ" highlight on the top recommendation, each card showing location code (hero mono), row/col/stack chips, a 0-100 score bar, reason text, and a "select" button,
So that I can pick a target location quickly when I'm not sure where to place a container.

**Acceptance Criteria:**

**Given** I'm on M1 with a source container selected and I tap "→ הצעות"
**When** I navigate to M7
**Then** the cabin calls `GET /api/v1/locations/suggested?containerId=...&craneId=...` and renders up to 6 cards per UX-DR10 sorted by score DESC.

**Given** I look at the top card
**When** it renders
**Then** it has a teal gradient background and a "מומלץ" badge; subsequent cards use the standard card style.

**Given** I tap "select" on a card
**When** the action fires
**Then** the card's location becomes the target on M1 (deep-link back); the source/target tiles light up per UX-DR17.

### Story 4.16: M8 Expected containers screen

As a **crane operator**,
I want the M8 expected containers screen — timeline list of containers arriving in the next hour, each row showing ETA badge (highlighted at-gate), container ID, line, customer, size, type, truck bay, and status pill,
So that I can prepare for incoming work without surprises.

**Acceptance Criteria:**

**Given** I navigate to M8
**When** the screen mounts
**Then** the cabin calls `GET /api/v1/reports/expected-containers?craneId=...&windowMinutes=60` and renders a vertical timeline per UX-DR11.

**Given** a container's status is `הגיע לשער` (at-gate)
**When** the row renders
**Then** the ETA badge has a highlight (amber pulse) and the row is positioned at the top of the timeline regardless of ETA sort order.

**Given** the screen is open for >60s
**When** the auto-refresh ticks (every 60s)
**Then** the data refreshes; existing scroll position is preserved.

### Story 4.17: M9 Empty + No-location split screen

As a **crane operator**,
I want the M9 split screen — left column listing empty containers with days-in-yard age coding (amber >7d, red >14d), right column listing no-location containers with a reason field, each row with an "Update location" action,
So that I can clean up the yard's accumulating debt.

**Acceptance Criteria:**

**Given** I navigate to M9
**When** the screen mounts
**Then** the left column calls `GET /api/v1/reports/empty-containers?blockCode=...` and renders rows with age badges (white <7d, amber 7-14d, red >14d).

**Given** the right column renders
**When** the data arrives from `GET /api/v1/reports/no-location?blockCode=...`
**Then** each row shows the container ID, last known coordinates (if any), and the reason ("GPS לא עודכן" / "AUTO-ID נכשל" / "אישור לא תקף").

**Given** I tap "Update location" on a no-location row
**When** the modal opens
**Then** it's the same manual location editor from Story 3.13, pre-filled with the container ID.

### Story 4.18: M10 Movements log screen

As a **crane operator**,
I want the M10 movements log — chronological list of every container move today, each row showing time, container ID, from-chip, arrow, to-chip, operator, status pill, with filter tabs for today/yesterday/this-week/custom-range,
So that I can audit recent operations and catch any anomalies.

**Acceptance Criteria:**

**Given** I navigate to M10
**When** the screen mounts
**Then** the "today" tab is active by default; the cabin calls `GET /api/v1/shifting?craneId=...&from=<today_00:00>&to=<today_23:59>` and renders the list per UX-DR13.

**Given** I tap "השבוע"
**When** the tab changes
**Then** the request re-fires with the week range; rows are grouped by date with collapsible date headers.

**Given** I tap "טווח מותאם"
**When** the date-range picker opens
**Then** it lets me pick a `from`/`to` (max 7-day range per the API cap from Story 4.6); on confirm the list refreshes.

### Story 4.19: M11 RTG settings + GPS panel screen

As a **crane operator**,
I want the M11 settings/diagnostics screen — left two cards (Connections: PLC/GPS/satellites/accuracy/HMI/TOS as colored metric tiles; Current position: lat/long/altitude/heading/speed/last-update), right column with action list (recalibrate GPS / test PLC / sync TOS / maintenance / reboot), bottom version footer,
So that I can self-diagnose and take recovery actions when needed.

**Acceptance Criteria:**

**Given** I navigate to M11
**When** the screen mounts
**Then** the cabin calls `GET /api/v1/cranes/{id}/diagnostics` every 5 seconds (live diagnostics) and renders metrics per UX-DR14.

**Given** I tap "Recalibrate GPS"
**When** the action fires
**Then** the cabin calls `POST /api/v1/cranes/{id}/diagnostics/recalibrate-gps`; on 202 a progress indicator shows; on completion (polled), a success/failure toast appears.

**Given** I tap "Maintenance mode" (senior operator only)
**When** the action fires
**Then** a confirmation modal appears ("המנוף יעבור לתחזוקה? עבודות פעילות יושהו"); on confirm the cabin calls `POST /diagnostics/maintenance-mode`; on success the cabin shows a global maintenance banner.

**And** non-senior operators see the diagnostics page in read-only mode (no action buttons rendered).

### Story 4.20: Cabin badge feature flag + high-contrast mode placeholder

As a **cabin developer**,
I want a feature flag for the "מערכת חדשה · דור 4" badge (default-on, hideable via config; auto-hide after 90 days) per UX-DR24, plus a high-contrast mode placeholder per UX-DR25 (kept as deferred but the toggle exists in M11 with a "בקרוב" indicator),
So that the rollout polish items don't get lost and Bold-variant CSS stays referenceable for a future high-contrast pivot.

**Acceptance Criteria:**

**Given** the cabin app's `coreConfig` has `features.welcomeBadge = true` (default)
**When** the login screen renders within 90 days of first deploy
**Then** the badge "מערכת חדשה · דור 4" shows; after 90 days OR if config flips to `false`, it hides.

**Given** I navigate to M11 settings
**When** the screen renders
**Then** there's a "מצב ניגוד גבוה" toggle in the diagnostics action list with a "בקרוב" sub-label (disabled, not interactive); ADR `docs/decisions/ADR-002-high-contrast-deferred.md` documents the deferral.

---

## Epic 5: Forklift Operations

Forklift drivers view current container info, report PICK/PLACE activity, and use all 14 forklift screens. Offline-tolerant — brief network drops queue locally and sync on reconnect. Forklift-specific UX commissioned (closes UX-DR21 / AR-23).

### Story 5.1: Forklift API — container query endpoints

As an **API client (forklift)**,
I want REST endpoints replacing legacy container query SPs — `sp_ForkLiftContainersIn`, `Out`, `EM`, `EMOut`, `ActQuery` (and their `_App` variants),
So that the forklift app can fetch container lists for each work mode (incoming, outgoing, empty, active).

**Acceptance Criteria:**

**Given** authenticated `GET /api/v1/forklift/containers?mode={in|out|empty|empty-out|active}&forkliftNumber=...`
**When** the request runs
**Then** API returns 200 with `{items: [{containerId, size, type, weight, location, customer, line, status, ...}, ...], totalCount}` filtered per mode.

**Given** mode=`active` with a forklift number
**When** the request runs
**Then** results are scoped to containers currently associated with that forklift (sp_ForkLiftContainersActQuery semantics).

**And** all five mode variants return a consistent schema with mode-specific fields nullable; OpenAPI documents this.

### Story 5.2: Forklift API — container update endpoints

As an **API client (forklift)**,
I want endpoints replacing `sp_ForkLiftContainersInUpDate`, `sp_ForkLiftContainersOutUpDate`, `sp_ForkLiftContainersLoctionUpDate`, `sp_ForkliftContainersCommentUpDate`,
So that forklift drivers can record PICK/PLACE/location-change/comment activity.

**Acceptance Criteria:**

**Given** authenticated `POST /api/v1/forklift/containers/{id}/activity` with body `{type: 'in|out|location|comment', forkliftNumber, ...}`
**When** the request runs (validated)
**Then** API writes to `rtg.shifting` (for in/out/location) or to a comments table (for comment), via dual-write to MSSQL `CO_Containers` updates the legacy fields; returns 200.

**Given** the activity is offline-queued by the app and synced on reconnect (Story 5.7 for client side)
**When** the synced request arrives with idempotency key
**Then** API checks for duplicate via `(forklift_number, idempotency_key, type, timestamp)` and rejects duplicates with 200 + `{deduplicated: true}`.

**And** every activity records the AD principal of the operator who logged in on the forklift app.

### Story 5.3: Forklift API — lookup combos + recommended location

As an **API client (forklift)**,
I want endpoints for dropdown population (customers, locations, shipping lines, special locations, works, container types) and recommended-location lookup,
So that the forklift app's combo boxes have data and the recommended-location screen has its source.

**Acceptance Criteria:**

**Given** authenticated `GET /api/v1/forklift/lookups/{customers|locations|lines|special-locations|works|container-types}?q=<search>`
**When** the request runs
**Then** API returns 200 with `{items: [{value, label, ...}]}` matching the search prefix; results capped at 100.

**Given** authenticated `GET /api/v1/forklift/recommended-location?containerId=...&forkliftNumber=...`
**When** the request runs
**Then** API returns 200 with `{recommendedLocation: {block, code, score, reason}}` from the same scoring algorithm as Epic 4 Story 4.7 (shared module).

**Given** authenticated `POST /api/v1/forklift/recommended-location/update` with body `{containerId, location, dealNumber: nullable}`
**When** the request runs
**Then** the recommendation is recorded in `rtg.recommended_location` (mssql_mirror sync via dual-write).

### Story 5.4: Forklift API — damage, hazmat, UN endpoints

As an **API client (forklift)**,
I want endpoints replacing `sp_ForkliftDamage`, `sp_ForkliftHazardousSubstances`, `sp_ForkliftUn`,
So that forklift drivers can report damage and look up hazmat/UN codes for risk-coded containers.

**Acceptance Criteria:**

**Given** authenticated `POST /api/v1/forklift/containers/{id}/damage` with body `{description, severity, photoBlobIds: nullable}`
**When** the request runs
**Then** a damage record is written to `rtg.damage_reports` and dual-written to MSSQL; returns 200.

**Given** authenticated `GET /api/v1/forklift/hazardous-substances?unCode=...`
**When** the request runs
**Then** API returns 200 with `{unCode, classification, properName, packingGroup, properShippingName, ...}` from `mssql_mirror.TC_HazardousSubstances`.

**And** UN code lookup is read-only; damage reports are append-only with audit trail.

### Story 5.5: Forklift API — works endpoints

As an **API client (forklift)**,
I want endpoints replacing `sp_ForkLiftActWorks`, `sp_ForkLiftWorks`, `sp_ForkLiftWorksByDate`, `sp_ForkLiftWorkDone`,
So that the forklift app's works screens have their data.

**Acceptance Criteria:**

**Given** authenticated `GET /api/v1/forklift/works?forkliftNumber=...&date=<iso>` (default: today)
**When** the request runs
**Then** API returns 200 with `{items: [{id, containerId, fromLocation, toLocation, dueAt, status, customer, line}, ...]}` for that forklift's pending/in-progress work.

**Given** authenticated `POST /api/v1/forklift/works/{workId}/done` with body `{forkliftNumber, completedAt}`
**When** the request runs
**Then** the work record is marked done; dual-write updates MSSQL `CP_Order`; audit trail logged.

### Story 5.6: Forklift self-update endpoint

As a **forklift developer**,
I want `GET /api/v1/clients/forklift/version` mirroring the cabin self-update endpoint (Story 4.1) but with separate version tracking,
So that forklift apps update independently from cabin apps.

**Acceptance Criteria:**

**Given** GET `/api/v1/clients/forklift/version`
**When** unauthenticated
**Then** API returns `{latest, minRequired, downloadUrl, releaseNotesUrl}` from a separate forklift manifest.

**Given** the forklift app starts with `currentVersion < minRequired`
**When** the version check runs
**Then** the app shows a forced-update modal blocking interaction.

**And** GitHub Actions publishes forklift builds to a versioned URL distinct from cabin's.

### Story 5.7: Forklift design system commission (UX-DR21 closure)

As a **forklift driver and a designer**,
I want a forklift-specific design pass producing tokens, screen layouts, and interaction patterns that extrapolate from cabin Modern but optimize for the smaller tablet form factor (rugged 10-12" tablet, mounted in-cab) with extensive F-key keyboard usage,
So that the forklift UX is purposeful instead of a thin shrink-wrap of cabin UX.

**Acceptance Criteria:**

**Given** the design pass is initiated
**When** the deliverables land in `CurrentSystem/RTG-Template/design_handoff_forklift/`
**Then** they include a README, design tokens overlay (denser spacing, possibly larger typography for distance reading), at least one wireframe per forklift screen (12 screens), and a description of F-key behavior conventions.

**Given** the design pass is approved
**When** the forklift Flutter codebase is updated
**Then** `forklift/lib/core/theme/ForkliftTheme` reflects the new tokens; an ADR `ADR-003-forklift-design-handoff.md` documents the decisions and links to the design files.

**And** if the design pass is delayed beyond pilot (acceptable risk per AR-23), the placeholder ForkliftTheme from Epic 1 Story 1.18 stays active and ADR-001 is referenced.

### Story 5.8: Forklift offline cache + write queue (Hive)

As a **forklift driver**,
I want my recent container views, lookups, and pending writes to persist locally in Hive so brief network drops don't lose work,
So that I can keep operating through transient OT-network gaps and sync when connectivity returns.

**Acceptance Criteria:**

**Given** the forklift app has loaded a container detail view while online
**When** the network drops mid-shift
**Then** the cached view remains available; an offline indicator appears in the top bar.

**Given** I record an activity (PICK/PLACE/comment) while offline
**When** the activity is submitted
**Then** it's serialized to Hive's `pending_writes` box with a generated UUID v7 idempotency key + timestamp; the UI reflects the activity locally.

**Given** the network returns
**When** the sync worker runs (every 30s + on connectivity change)
**Then** it drains `pending_writes` to the API in order; deduplication is handled server-side; on success the local entry is removed; on failure (4xx) the entry is moved to a `failed_writes` box for manual review.

**And** the sync queue has a max depth of 500 entries; beyond that the app refuses new offline writes and surfaces an explicit error.

### Story 5.9: Forklift F-key shortcut framework

As a **forklift driver**,
I want F2-F12 keyboard shortcuts working consistently across all forklift screens (F2 damage, F3 query, F4 works, F5 info, F6 sister-containers, F7 reserved, F10 exit, F12 continue) per FR3.3,
So that I can navigate without touching the screen mid-task.

**Acceptance Criteria:**

**Given** any forklift screen is active
**When** I press F10
**Then** the app exits cleanly (or returns to the previous screen, depending on stack); F10 is consistent across screens.

**Given** the forklift screen has registered F-key handlers
**When** I press F2 on a container detail view
**Then** the damage-comment dialog opens; pressing F12 confirms; pressing F10 cancels.

**Given** an F-key isn't applicable to the current screen
**When** I press it
**Then** nothing happens (no error toast — silent ignore for unbinded keys).

**And** the F-key bindings are implemented via Flutter `Shortcuts` + `Actions` widgets and are testable via widget tests.

### Story 5.10: frmInformation main info screen

As a **forklift driver**,
I want the main information screen showing the current container's full details (ID, weight, contents, customer, line, location/expected, status) plus the F-key action bar (F2-F12),
So that I have one screen showing everything I need to act on a container.

**Acceptance Criteria:**

**Given** I'm logged in and a container is selected
**When** I navigate to frmInformation
**Then** the screen calls `GET /api/v1/containers/{id}` and renders all fields with hero treatment for the container ID; F-key action bar renders at the bottom.

**Given** no container is selected
**When** the screen renders
**Then** an empty-state with "סרק מכולה" CTA appears, leading to frmSelectContainer (Story 5.17).

**Given** I press F12 ("המשך")
**When** the action fires
**Then** I'm routed to frmActivity (Story 5.11) with the current container preselected.

### Story 5.11: frmInfoMenu

As a **forklift driver**,
I want a menu screen with shortcuts to all forklift sub-screens (works, recommended location, empty containers, in-out diary, special location, etc.),
So that I have a hub when I'm not in the middle of a specific task.

**Acceptance Criteria:**

**Given** I tap "תפריט מידע" from frmInformation
**When** the screen mounts
**Then** it renders a list of sub-screen entries; each is keyboard-navigable (arrow keys + Enter) and tap-friendly.

**Given** I navigate via arrow keys + Enter
**When** I select an entry
**Then** the corresponding screen opens.

### Story 5.12: frmActivity (PICK/PLACE reporting)

As a **forklift driver**,
I want a screen to record my PICK or PLACE activity for the current container — with location/destination input, F12 to confirm, F10 to cancel,
So that activity flows into the system promptly (online or queued offline).

**Acceptance Criteria:**

**Given** I'm on frmActivity with a container preselected
**When** I enter a location and press F12
**Then** the app calls `POST /api/v1/forklift/containers/{id}/activity` with `type=in/out/location`; on 200, the screen confirms and returns to frmInformation.

**Given** I'm offline
**When** I press F12
**Then** the activity is queued via Story 5.8; the UI confirms with an "ממתין לסנכרון" indicator; sync drains it when network returns.

**Given** the location is invalid
**When** F12 fires
**Then** validation kicks in (server-side validates against `rtg.locations`); error renders in Hebrew via `he.arb`.

### Story 5.13: frmWorks

As a **forklift driver**,
I want a screen listing my pending and active works for the day (sourced from `GET /api/v1/forklift/works?forkliftNumber=...`),
So that I know what I should do next.

**Acceptance Criteria:**

**Given** I navigate to frmWorks (F4 from anywhere or via menu)
**When** the screen mounts
**Then** the works list renders sorted by `dueAt` ascending; each row has container ID, from-loc, to-loc, customer, line, status pill.

**Given** I tap a row or press Enter on a focused row
**When** the action fires
**Then** the work's container becomes the current container and I navigate to frmInformation.

**Given** F9 is pressed ("הכל")
**When** the toggle fires
**Then** the filter expands to show all forklifts' works (read-only mode for non-supervisor drivers).

### Story 5.14: frmRecommendedLocation

As a **forklift driver**,
I want a screen showing the recommended location for the current container plus an option to confirm or override,
So that I can place containers efficiently using yard-management heuristics.

**Acceptance Criteria:**

**Given** I navigate to frmRecommendedLocation with a container preselected
**When** the screen mounts
**Then** it calls `GET /api/v1/forklift/recommended-location?containerId=...` and renders the recommendation with score + reason; an "אשר" action confirms (POSTs to update endpoint), an "החלף" lets me type an override.

**Given** I confirm the recommendation
**When** the action fires
**Then** `POST /api/v1/forklift/recommended-location/update` is called and I'm routed to frmActivity with the location pre-filled.

### Story 5.15: frmEmptyContainers

As a **forklift driver**,
I want a screen listing empty containers in my work area (sourced from `GET /api/v1/forklift/containers?mode=empty`),
So that I can prioritize empty-container moves which often back up.

**Acceptance Criteria:**

**Given** I navigate to frmEmptyContainers
**When** the screen mounts
**Then** the list renders with container IDs, current locations, days-in-yard age coding (amber >7d, red >14d), and customer/line.

**Given** I tap a row
**When** the action fires
**Then** the container is selected and I navigate to frmInformation.

### Story 5.16: frmInOutDiory

As a **forklift driver**,
I want a screen showing the in/out diary for the day (containers I and other forklifts have moved),
So that I can see today's activity at a glance.

**Acceptance Criteria:**

**Given** I navigate to frmInOutDiory
**When** the screen mounts
**Then** it calls a forklift-scoped variant of the shifting endpoint (`GET /api/v1/forklift/diary?forkliftNumber=...&date=<today>`) and renders chronologically with from→to chips.

**Given** I scroll
**When** older entries become visible
**Then** they paginate in (50 per page).

### Story 5.17: frmComment (with damage mode)

As a **forklift driver**,
I want a screen to add a comment to the current container, with a special "damage" mode (F2 entry) that captures damage type + severity,
So that container condition issues are recorded with context.

**Acceptance Criteria:**

**Given** I'm on frmInformation and press F2 (damage)
**When** frmComment opens in damage mode
**Then** the form shows damage-specific fields (severity dropdown, type, description); F12 submits via `POST /api/v1/forklift/containers/{id}/damage`.

**Given** I enter frmComment via the menu (non-damage)
**When** the form opens in normal mode
**Then** it shows a free-text comment field; F12 submits via `POST /api/v1/forklift/containers/{id}/activity` with `type=comment`.

**Given** I press F10
**When** the action fires
**Then** the form closes without saving; any text in the field is discarded after a confirmation dialog.

### Story 5.18: frmSelectContainer

As a **forklift driver**,
I want a screen to select a container by ID (typed or scanned) or by browsing recently-viewed containers,
So that I can pick a container to work on without navigating through multiple lookups.

**Acceptance Criteria:**

**Given** I navigate to frmSelectContainer
**When** the screen mounts
**Then** a search input (numeric+alpha) and a list of recently-viewed containers (from Hive cache) render.

**Given** I type a partial container ID
**When** the input changes (debounced 300ms)
**Then** results from `GET /api/v1/containers?prefix=...` populate the list.

**Given** I tap (or press Enter on) a result
**When** the action fires
**Then** the container becomes selected and I navigate to frmInformation.

### Story 5.19: frmSpecialLocation

As a **forklift driver**,
I want a screen showing special locations (out-of-yard, cold storage, customs hold, etc.) and letting me direct a container to one,
So that exceptional placement is captured as a first-class operation.

**Acceptance Criteria:**

**Given** I navigate to frmSpecialLocation with a container preselected
**When** the screen mounts
**Then** it calls `GET /api/v1/forklift/lookups/special-locations` and renders the options as a tap-friendly list.

**Given** I select a special location and press F12
**When** the action fires
**Then** the activity is recorded via `POST /api/v1/forklift/containers/{id}/activity` with `type=location` and the special location code.

### Story 5.20: frmSameDealNumber

As a **forklift driver**,
I want a screen listing all containers in the same deal as the current container (sister containers), accessible via F6 from frmInformation,
So that I can locate related containers when working on a deal-level task.

**Acceptance Criteria:**

**Given** I'm on frmInformation and press F6
**When** frmSameDealNumber opens
**Then** it calls `GET /api/v1/containers/{id}/sisters` and renders the list with each sister's location and status.

**Given** I tap a sister
**When** the action fires
**Then** I navigate to frmInformation for that sister.

### Story 5.21: frmMessage + frmChangeForkliftNumber (combined)

As a **forklift driver**,
I want two small dialog screens — frmMessage (system messages from supervisor / dispatcher) and frmChangeForkliftNumber (re-keying the forklift identity if I switch trucks mid-shift),
So that I can receive operational messages and switch trucks cleanly.

**Acceptance Criteria:**

**Given** the forklift app receives a message via SignalR push (from dispatcher or system)
**When** frmMessage opens automatically
**Then** the message text + sender renders; F12 acknowledges and dismisses.

**Given** I tap "שנה מספר מלגזה" or open the screen via menu
**When** frmChangeForkliftNumber opens
**Then** the input is pre-filled with the current forklift number from `ForkLiftNumber.xml`; on save, the file is updated and the in-memory `forkliftNumber` value is reset; subsequent activities use the new number.

**Given** the forklift number changes during an active session
**When** the change is committed
**Then** any in-flight pending writes (Story 5.8) are flushed before the change to avoid mis-attribution.

---

## Epic 6: Migration Bridge (Dual-Write to MSSQL)

Downstream consumers (MIS, AuditWeb-Web, WMS, legacy ForkliftApp) keep getting their data without disruption during the transition. Drift detected within an hour. Activates the `IPostCommitDualWriter` seam from Epic 3 Story 3.6.

### Story 6.1: PG schema for dual_write_outbox

As a **developer**,
I want a migration creating `rtg.dual_write_outbox` with the columns + indexes the outbox processor needs,
So that all dual-write events have a durable, queryable home.

**Acceptance Criteria:**

**Given** `dbmate up` runs
**When** schema is checked
**Then** `rtg.dual_write_outbox` exists with `id BIGSERIAL PK`, `aggregate_type VARCHAR(64)` (e.g., `job`, `shifting`, `location`, `audit_log`), `aggregate_id`, `operation VARCHAR(16)` (`insert`/`update`/`delete`), `payload JSONB` (the MSSQL-shaped record), `idempotency_key UUID NOT NULL`, `attempts INT NOT NULL DEFAULT 0`, `next_attempt_at TIMESTAMPTZ`, `last_error TEXT NULLABLE`, `processed_at TIMESTAMPTZ NULLABLE`, `dead_lettered_at TIMESTAMPTZ NULLABLE`, `created_at`.

**Given** the migration runs
**Then** indexes exist: `idx_outbox_pending (next_attempt_at) WHERE processed_at IS NULL AND dead_lettered_at IS NULL`, and `uq_outbox_idempotency (idempotency_key)` to prevent duplicate enqueues.

### Story 6.2: IPostCommitDualWriter real implementation (replaces Epic 3 stub)

As a **listener developer**,
I want the real `OutboxPostCommitDualWriter` registered in DI (replacing the `NullPostCommitDualWriter` stub from Epic 3 Story 3.6),
So that PLACE/PICK/Cancel/manual-edit events from the listener and API now produce outbox rows for downstream MSSQL replication.

**Acceptance Criteria:**

**Given** the PLACE handler completes a transaction successfully (Story 3.6)
**When** `IPostCommitDualWriter.Enqueue(events)` is called
**Then** outbox rows are inserted in the SAME PG transaction (so an outbox row never exists for a rolled-back PLACE).

**Given** any cabin/forklift state-change endpoint (job submit, manual location edit, activity report, damage report)
**When** the endpoint commits
**Then** matching outbox rows are enqueued covering the equivalent legacy MSSQL writes (RG_B3 update, RG_Shifting insert, TB_Location update, CO_Containers update, AuditWeb insert as appropriate per FR8.2).

**And** ADR `ADR-004-dual-write-event-mapping.md` documents the rtg→MSSQL field-by-field mapping for each aggregate type.

### Story 6.3: MssqlWriter with idempotency

As a **DualWrite developer**,
I want `Rtg.DualWrite/MssqlWriter/MssqlWriter.cs` that takes an outbox row and writes it to MSSQL `TerminalData`, idempotent on `idempotency_key` so retries don't double-write,
So that dual-write is replay-safe across processor crashes and retries.

**Acceptance Criteria:**

**Given** an outbox row with `aggregate_type=shifting, operation=insert, payload={...}, idempotency_key=<uuid>`
**When** `MssqlWriter.Apply(row)` is called
**Then** the corresponding INSERT into `RG_Shifting` is executed AND the `idempotency_key` is recorded in MSSQL `rtg_idempotency_log` table (pre-created via a setup migration outside dbmate).

**Given** the same outbox row is processed twice (after a retry)
**When** `MssqlWriter.Apply` runs the second time
**Then** the idempotency check finds the prior insert and short-circuits without re-writing; returns `Skipped`.

**And** all writes use parameterized SqlCommand (no SQL injection); MSSQL connection string comes from env vars only.

### Story 6.4: Outbox processor BackgroundService with retry policy

As a **DualWrite developer**,
I want `Rtg.DualWrite/Outbox/OutboxRetryWorker.cs` (BackgroundService) that polls `rtg.dual_write_outbox WHERE processed_at IS NULL AND dead_lettered_at IS NULL AND next_attempt_at <= now()`, calls `MssqlWriter.Apply`, and updates the row with success or schedules retry,
So that dual-writes propagate to MSSQL with bounded retry semantics per FR8.3.

**Acceptance Criteria:**

**Given** the worker is running
**When** it polls every 1 second
**Then** it picks up to 50 ready rows in a single batch and processes them sequentially.

**Given** an apply succeeds
**When** the worker handles the row
**Then** `processed_at=now()` is set; the row is no longer picked up.

**Given** an apply fails (MSSQL down, connection timeout, etc.)
**When** the worker handles the failure
**Then** `attempts` is incremented; `next_attempt_at` is set per backoff schedule (1m → 5m → 15m → 1h → 6h → 24h, capped); `last_error` is recorded; if `attempts >= 10` the row is dead-lettered (`dead_lettered_at=now()`).

**And** dead-letter rows fire a Grafana alert (Epic 7 wires); they are queryable via a diagnostic endpoint for ops review.

### Story 6.5: postgres_fdw setup + mssql_mirror.* views

As a **developer**,
I want a migration that creates the `mssql_mirror` schema with foreign tables via `postgres_fdw` (or `tds_fdw` for MSSQL access) covering `CO_Containers`, `CO_ContainerProfile`, `CP_Deal`, `CP_Order`, `TC_Client`, `TB_Drivers`, `TB_RecommendedLocation`, `TC_HazardousSubstances`, `HR_Emp`, `SC_Users`, `SC_AppGroup`, `V_ContainerUnloadRG`, `V_RTGLoad`, `V_OrderForkLift`,
So that the API can read legacy MSSQL data transparently from PG without separate connection management.

**Acceptance Criteria:**

**Given** the migration runs
**When** PG is queried
**Then** `mssql_mirror.CO_Containers` is queryable as if it were a local table; the FDW resolves to the MSSQL connection (creds from env vars, not in migration).

**Given** the foreign tables are in place
**When** the API performs `SELECT ... FROM rtg.cranes JOIN mssql_mirror.CO_Containers ...`
**Then** the join executes with predicates pushed down to MSSQL where possible (verified via `EXPLAIN`).

**And** the FDW user mapping is read-only at the MSSQL side (DBA-provisioned `rtg_reader` login with SELECT-only grants on the listed tables).

### Story 6.6: Reconciliation service (hourly drift check)

As a **DualWrite developer**,
I want `Rtg.DualWrite/Reconciliation/ReconciliationCronWorker.cs` that runs hourly, diffs `rtg.shifting` against MSSQL `RG_Shifting` (and similar for other dual-written tables) for the past 24 hours, and reports any mismatches,
So that silent dual-write drift is caught within an hour per FR5.7 / NFR6.4.

**Acceptance Criteria:**

**Given** the cron fires at the top of every hour
**When** it queries the last 24 hours of `rtg.shifting` and the same window in `mssql_mirror.RG_Shifting`
**Then** it computes the symmetric difference (rows in PG not in MSSQL, and vice versa) keyed on the dual-write idempotency key.

**Given** a drift is detected (>0 missing rows on either side)
**When** the worker handles the result
**Then** it logs `drift_detected` with the count and sample IDs, increments a Prometheus counter `dual_write_drift_total`, and (if the count > threshold) fires an alert.

**And** the reconciliation runs against the same 4 aggregate types covered by the outbox (shifting, jobs, locations, audit_log).

### Story 6.7: Drift and DLQ alert publisher

As an **ops engineer**,
I want metrics + alert rules covering outbox dead-letter growth rate, reconciliation drift count, and outbox-lag (oldest unprocessed row age),
So that I'm paged before MSSQL consumers notice gaps.

**Acceptance Criteria:**

**Given** Prometheus scrapes `Rtg.DualWrite`
**When** metrics are exposed
**Then** they include `outbox_pending_total`, `outbox_dead_lettered_total`, `outbox_oldest_pending_age_seconds`, `dual_write_drift_total`, `dual_write_apply_duration_seconds_histogram`.

**Given** `outbox_oldest_pending_age_seconds > 300` for 2 minutes (the outbox is falling behind)
**When** Alertmanager evaluates
**Then** an alert fires to the configured channel (email per Yaniv's choice in arch §3.6 D5.2).

**And** alert rules are checked into `ops/monitoring/alert-rules.yml`.

### Story 6.8: AuditWeb mirror writer

As a **DualWrite developer**,
I want a specialized writer that takes `rtg.audit_log` outbox events and writes them in the legacy `AuditWeb` text-statement format (per legacy Web triggers' INSERT-as-text pattern),
So that the existing Web consumer keeps receiving the audit stream it depends on (NFR8.2).

**Acceptance Criteria:**

**Given** an outbox row with `aggregate_type=audit_log`
**When** `AuditWebWriter.Apply(row)` is called
**Then** an INSERT into MSSQL `AuditWeb (ActionType, UserName, ActionStatus, Action)` is executed with `Action` formatted as the equivalent text-INSERT statement matching the legacy trigger output.

**Given** the writer runs in pilot
**When** the Web consumer queries `AuditWeb` for the last hour
**Then** it sees entries from the new system (with `UserName` = AD principal, distinguishable from legacy MSSQL trigger entries by a marker field).

### Story 6.9: End-to-end dual-write integration test

As a **DualWrite developer**,
I want `Rtg.Tests/DualWrite/DualWriteE2ETests.cs` that submits a job via API, watches it flow through PG → outbox → MSSQL, asserts both sides match, and validates retry-on-MSSQL-down,
So that the migration bridge is provably correct before pilot.

**Acceptance Criteria:**

**Given** the test harness with Testcontainers PG + Testcontainers MSSQL + listener + API + DualWrite all wired
**When** a PICK/PLACE flow completes (Story 3.14 base + dual-write enabled)
**Then** within 5 seconds, both `rtg.shifting` and `mssql_mirror.RG_Shifting` have matching rows for the move; reconciliation reports 0 drift.

**Given** the test stops the MSSQL container mid-run
**When** the outbox worker tries to apply
**Then** rows accumulate in `dual_write_outbox` with incrementing `attempts`; restarting MSSQL drains them with no data loss.

**Given** an outbox row exhausts 10 retries
**When** the worker handles it
**Then** the row is dead-lettered; the `outbox_dead_lettered_total` metric increments; the row's `last_error` is preserved for diagnosis.

---

## Epic 7: Observability & Operational Readiness

Ops team has visibility into the system — structured logs, 4 SLI metrics dashboards, alert rules, health checks. Incidents are diagnosable; SLAs trackable.

### Story 7.1: Serilog + Seq integration across all .NET services

As an **ops engineer**,
I want every `.NET` service (`Rtg.Listener`, `Rtg.Api`, `Rtg.DualWrite`) configured with Serilog → Seq (Docker container on the VM) with structured JSON enrichers (RequestId, MessageId, CraneId, AdPrincipal),
So that all logs land in one place, are queryable, and are correlatable across services.

**Acceptance Criteria:**

**Given** any service starts up
**When** it logs an event
**Then** the event lands in Seq within 2 seconds with structured fields (no string-formatted messages — placeholders only per pattern §4.5.3).

**Given** an HTTP request flows through the API and triggers a listener handler
**When** I query Seq for the request's `RequestId`
**Then** I see all related log entries from API + Listener + DualWrite correlated.

**And** PII (PIN, password, JWT, refresh token) never appears in any log line — verified by a CI test that scans Seq fixture output against a regex blocklist.

### Story 7.2: Prometheus metrics export

As an **ops engineer**,
I want each .NET service exposing `/metrics` endpoint with Prometheus-format metrics covering throughput, latency, queue depth, error rate,
So that Prometheus scrapes them and Grafana can render the dashboards.

**Acceptance Criteria:**

**Given** Prometheus scrapes `http://api-vm:5000/metrics` every 15 seconds
**When** the API has handled requests
**Then** standard `aspnet_core_*` metrics + custom `rtg_*` counters/histograms are exposed.

**Given** the listener has processed frames
**When** Prometheus scrapes
**Then** custom metrics include `listener_frames_received_total{crane,msg_type}`, `listener_handler_duration_seconds_histogram`, `listener_dlq_total`, `listener_active_connections{crane}`.

**Given** DualWrite is running
**When** Prometheus scrapes
**Then** outbox metrics from Story 6.7 are exposed plus `dual_write_apply_duration_seconds_histogram`.

### Story 7.3: Grafana SLI dashboards (4 dashboards)

As an **ops engineer**,
I want 4 Grafana dashboards as JSON files in `ops/monitoring/grafana-dashboards/` — `listener-throughput.json`, `api-latency.json`, `dual-write-lag.json`, `auth-failures.json`,
So that I can see system health at a glance.

**Acceptance Criteria:**

**Given** the dashboards are imported into Grafana
**When** I open `listener-throughput`
**Then** it shows panels: frames/sec per crane, ACK round-trip p95/p99, DLQ count rate, active TCP connections, with 1m/5m/1h/24h time ranges.

**Given** I open `api-latency`
**When** the panel renders
**Then** it shows p50/p95/p99 latency per endpoint group (`/auth/*`, `/jobs/*`, `/cranes/*`, `/forklift/*`, `/reports/*`), error rate per group, request rate.

**Given** I open `dual-write-lag` and `auth-failures`
**When** they render
**Then** they cover outbox queue depth + dead-letter rate + drift count (per Story 6.7) and login failure rate + lockout rate respectively.

### Story 7.4: Alertmanager rules

As an **ops engineer**,
I want alert rules in `ops/monitoring/alert-rules.yml` covering listener disconnect, DLQ growth rate, dual-write outbox lag, auth failure spike, API error rate, and PG connection saturation,
So that operational anomalies page someone before users notice.

**Acceptance Criteria:**

**Given** alert rules are loaded into Alertmanager
**When** `listener_active_connections{crane="gold3"} == 0` for 30 seconds during an active shift window
**Then** an alert `ListenerDisconnect` fires to the email channel.

**Given** the rules
**When** outbox lag, DLQ growth, drift, auth failure spike, or API 5xx rate breach thresholds (per the rule definitions)
**Then** corresponding alerts fire with Hebrew + English summaries.

**And** all alerts include runbook links pointing to `ops/runbooks/`.

### Story 7.5: Health check endpoints per service

As an **ops engineer**,
I want `GET /health` on each .NET service returning `{status, db, signalrConnections (api only), queueDepth (dualwrite only), uptime}`,
So that Windows Service health probes and external monitors can verify liveness/readiness.

**Acceptance Criteria:**

**Given** GET `/health` on the API
**When** the request runs
**Then** API returns 200 with `{status: "healthy", db: "connected", signalrConnections: <n>, uptimeSeconds: <n>}`; if any subsystem is unhealthy, status is `degraded` (still 200) or `unhealthy` (503).

**Given** GET `/health` on the listener
**When** the request runs (listener exposes a small HTTP probe port)
**Then** it returns `{status, activeCranes: 3, db: "connected", outboxLag: 12}`.

**And** Windows Service Recovery Options are configured to restart on health check failures (verified in Epic 8).

### Story 7.6: 90-day hot retention + archive script

As a **DBA**,
I want a scheduled job (cron via `pg_cron` extension or external scheduler) that drops partitions older than 90 days from `rtg.crane_positions`, `rtg.shifting`, `rtg.audit_log`, after archiving them to Parquet files in cold storage,
So that hot tables stay performant and historical data remains retrievable per FR5.6.

**Acceptance Criteria:**

**Given** `pg_cron` (or external cron) is configured
**When** the monthly archive job runs at 02:00 on the 1st
**Then** partitions older than 90 days are exported to `<archive-dir>/{year}/{month}/<table>.parquet` (a separate volume backed up offsite weekly), then dropped.

**Given** the archive directory has a partition file
**When** I need to query historical data
**Then** an ad-hoc DuckDB query against the Parquet file returns the historical rows; runbook documents this.

**And** ADR `ADR-005-archive-strategy.md` documents the choice of Parquet + DuckDB and the trigger to revisit (e.g., if compliance requires longer hot retention).

### Story 7.7: Self-update version-rollout monitoring

As an **ops engineer**,
I want metrics + alerts on cabin/forklift self-update success rate (download success vs. failure, version distribution across the fleet),
So that I can detect a bad rollout (e.g., download URL returning 5xx) and roll forward/back accordingly.

**Acceptance Criteria:**

**Given** cabin/forklift apps report version + update status to `POST /api/v1/clients/telemetry`
**When** the API receives the report
**Then** it increments `client_version_count{app="cabin",version="1.2.3"}` and `client_update_outcome_total{app, outcome}`.

**Given** the rolling 1-hour update success rate drops below 90%
**When** Alertmanager evaluates
**Then** a `ClientUpdateRolloutFailing` alert fires.

---

## Epic 8: Pilot Deployment (GOLD3 Cutover)

GOLD3 crane operator switches from legacy `TOSService.exe` to the new system on a Saturday window. <30s rollback path validated. 1-4 weeks of dual-running and drift monitoring before scaling out.

### Story 8.1: VM provisioning + IT handoff runbook

As an **ops engineer**,
I want `ops/runbooks/vm-provisioning.md` documenting hardware spec (4-8 vCPU, 16-32 GB RAM, ~500 GB SSD, Windows Server 2022, OT subnet `192.6.8.x`), required software (Docker Desktop / Docker Engine, .NET 10 Runtime, PowerShell 7), AD service account requests, firewall ports,
So that IT can provision the VM independently and the cutover doesn't block on infra negotiation.

**Acceptance Criteria:**

**Given** the runbook exists in `ops/runbooks/`
**When** IT follows it
**Then** they can provision the VM end-to-end without architect intervention; the runbook covers VM creation, OS hardening (CIS-1.5 baseline), network config, AD-join, firewall rules.

**Given** the AD service account `svc-rtg-ldap` is requested
**When** IT provisions it
**Then** it has read-only access to user/group OUs and is documented in `ops/runbooks/ad-service-accounts.md`.

### Story 8.2: Self-hosted GitHub Actions runner

As an **ops engineer**,
I want a GitHub Actions self-hosted runner installed on the VM (Windows service, registered to the `goldbond/craines-tos` repo),
So that `cd-staging.yml` and `cd-production.yml` workflows can build artifacts on the VM and deploy in-place.

**Acceptance Criteria:**

**Given** the runner is installed
**When** I trigger the staging workflow
**Then** the runner picks up the job, builds .NET artifacts + Flutter artifacts, runs `dbmate up`, restarts Windows Services, and posts back success.

**Given** the runner crashes
**When** Windows Service Recovery kicks in
**Then** it auto-restarts within 30 seconds.

### Story 8.3: Windows Service registration

As an **ops engineer**,
I want `Rtg.Listener`, `Rtg.Api`, `Rtg.DualWrite` registered as Windows Services with proper recovery options (auto-restart on crash, 3 attempts before alarm), service account, and dependency declarations (start after Docker / PG),
So that the services run unattended and recover from transient failures.

**Acceptance Criteria:**

**Given** the registration scripts in `ops/windows-services/` are run on the VM
**When** I check `Get-Service Rtg.*`
**Then** all three services exist with `StartupType: Automatic`, recovery configured, and run under the dedicated service account.

**Given** I `Stop-Service Rtg.Listener` then crash a process
**When** the Windows Service Control Manager handles it
**Then** the service auto-restarts; an event is logged in Windows Event Log.

### Story 8.4: Drain shutdown end-to-end validation

As an **ops engineer**,
I want a test that issues `Stop-Service Rtg.Listener` while a chesimu replay is mid-flight and asserts no message loss + bounded shutdown time per FR7.3,
So that the drain mode introduced in Story 2.6 is provably correct end-to-end.

**Acceptance Criteria:**

**Given** chesimu is sending A1 frames at 5 Hz to the listener
**When** I `Stop-Service Rtg.Listener`
**Then** the service stops accepting new TCP connections immediately, processes all read frames, exits within 30 seconds, and `rtg.crane_positions` shows all the frames that arrived before the stop.

**Given** the drain takes > 30 seconds (synthesized via test fixture)
**When** the timeout fires
**Then** the service hard-exits; the test asserts that the count of in-flight messages lost is bounded and logged.

### Story 8.5: Secrets-on-VM lifecycle setup

As an **ops engineer**,
I want `C:\Rtg\secrets\<service>.env` files on the VM (mode 0600, owned by service account) populated with JWT signing keys, AD bind password, MSSQL connection string, plus a runbook `ops/runbooks/secret-rotation.md`,
So that runtime secrets are accessible to services but not committed to git or readable by other accounts.

**Acceptance Criteria:**

**Given** the VM is provisioned
**When** IT runs the secrets setup script (in `ops/deployment/install-secrets.ps1`)
**Then** the env files exist with correct ACLs; PowerShell `(Get-ACL ...)` confirms `BUILTIN\Administrators + svc-rtg` only.

**Given** a service starts up
**When** it loads config
**Then** secrets come from the env file (not appsettings.json); CI test verifies no secret keys are present in any `appsettings*.json`.

**And** the rotation runbook documents annual JWT key rotation + AD password rotation per AD policy.

### Story 8.6: NTP sync configuration

As an **ops engineer**,
I want the VM configured to sync to Goldbond's NTP server (`ntp.goldbond.local` or equivalent) with monitoring + alerting on drift > 1 second,
So that timestamps across the new system + MSSQL + cabins + forklifts stay aligned per AR-15 / NFR10.6.

**Acceptance Criteria:**

**Given** the VM's `w32time` service is configured
**When** I run `w32tm /query /status`
**Then** the source is the Goldbond NTP server and current offset is < 100ms.

**Given** clock drift exceeds 1 second
**When** monitoring detects it
**Then** an alert fires; the runbook documents the recovery (manual `w32tm /resync` then investigate the network).

### Story 8.7: Firewall whitelist coordination + runbook

As an **ops engineer**,
I want `ops/runbooks/firewall-whitelist.md` documenting which PLC IPs need to reach which listener ports (GOLD1 PLC → 30701, GOLD2 → 30702, GOLD3 → 30703), plus an IT handoff for the actual rule provisioning,
So that the listener ports are accessible only from PLCs and not from arbitrary OT-network hosts (NFR1.5).

**Acceptance Criteria:**

**Given** the runbook
**When** IT provisions firewall rules
**Then** only the documented PLC IPs can reach ports 30701-30703 on the VM; verified by `Test-NetConnection` from a non-whitelisted host returning failure.

**Given** the firewall is in place
**When** the listener starts up
**Then** it logs which IPs successfully connected; unauthorized connection attempts are logged + counted via metric `listener_unauthorized_connection_attempts_total`.

### Story 8.8: TLS terminator (IIS reverse proxy) + cert deployment

As an **ops engineer**,
I want IIS configured as a reverse proxy in front of Kestrel (API), terminating TLS with a self-signed cert (pilot) + HTTP→HTTPS redirect + HSTS,
So that cabin/forklift clients connect over HTTPS and the foundation for cert rotation is in place per Gap #1 / D2.1.

**Acceptance Criteria:**

**Given** IIS is configured per the runbook
**When** I `curl -k https://api-vm/health`
**Then** TLS is negotiated and the response is the API's health JSON; HTTP requests are 301-redirected to HTTPS.

**Given** the self-signed cert is deployed
**When** cabin/forklift apps trust the cert (via local cert store)
**Then** SignalR + REST work over HTTPS without warnings.

**And** ADR `ADR-006-tls-self-signed-pilot.md` documents the deferred transition to an internal CA-signed cert.

### Story 8.9: Pre-cutover dry-run (chesimu E2E + load test)

As an **ops engineer**,
I want a dry-run runbook `ops/runbooks/pre-cutover-dry-run.md` that drives chesimu through a full operational scenario (login → 50 jobs → cancel some → all reports) and a load test sustaining 10× baseline (60 msg/sec) for 30 minutes,
So that confidence in pilot readiness is grounded in evidence, not hope (FR8.8 / NFR7.4).

**Acceptance Criteria:**

**Given** the dry-run is executed
**When** all scenarios complete
**Then** SLIs match expectations: ACK round-trip p99 < 1s, cabin push p95 < 200ms, no DLQ growth, no drift detected, 0 lost messages.

**Given** the load test runs for 30 minutes at 60 msg/sec
**When** results are collected
**Then** PG CPU < 60%, listener memory stable, no GC pauses > 100ms; results saved to `ops/load-test-results/<date>.md`.

### Story 8.10: Cutover Saturday + rollback runbook (combined)

As an **ops engineer**,
I want `ops/runbooks/cutover-gold3.md` + `ops/runbooks/rollback.md` documenting every step of the GOLD3 cutover and the <30-second rollback path,
So that the Saturday window has zero ambiguity and rollback is exercised, not improvised.

**Acceptance Criteria:**

**Given** the cutover runbook
**When** the team executes it
**Then** it covers: pre-cutover checks, IT firewall flip (legacy listener IP off / new listener IP on for GOLD3), service start order, smoke test checklist, sign-off criteria.

**Given** the rollback runbook
**When** the team rehearses it (DR drill)
**Then** they can flip GOLD3 back to legacy `TOSService.exe` on `192.6.8.52` in < 30 seconds (one firewall rule change), verified by stopwatch in the DR drill.

**And** both runbooks are reviewed and approved by IT, the on-call team, and the product owner (Yaniv).

### Story 8.11: On-call playbook

As an **on-call engineer**,
I want `ops/runbooks/on-call.md` documenting alert response procedures for each Alertmanager rule (Story 7.4) — symptom, root-cause hypothesis, immediate mitigation, escalation path,
So that incident response is consistent regardless of who's on-call.

**Acceptance Criteria:**

**Given** an alert fires (e.g., `ListenerDisconnect`)
**When** I open the playbook
**Then** I find a section keyed to that alert with the runbook steps; first step is always "verify with second tool" (avoid acting on a single-source alert).

**And** the playbook covers each of the Story 7.4 alerts plus a generic "unknown alert" section pointing to Seq + Grafana entry points.

### Story 8.12: Audit log review post-cutover

As a **product owner (Yaniv)**,
I want a post-cutover audit log review session (T+24h, T+1week) where the team queries `rtg.audit_log` for anomalies (failed logins, lockouts, manual edits, errors),
So that the new audit trail is verified as trustworthy before scaling to GOLD1+GOLD2.

**Acceptance Criteria:**

**Given** the cutover happened on Saturday
**When** the T+24h review meeting runs
**Then** the team queries audit_log via Grafana (or directly) and confirms: every operator's actions are recorded, AD principal mapping is correct, no PII appears in audit metadata, drift = 0.

**Given** the review at T+1week
**When** the team examines a 7-day audit log
**Then** they confirm activity volume matches expectations and no suspicious patterns (e.g., one user logged in from multiple IPs simultaneously) are present.

---

## Epic 9: Production Rollout (GOLD1 + GOLD2)

All three cranes operate on the new system; the legacy listener stack on `192.6.8.52` is decommissioned in stages; MSSQL retirement plan begins as downstream consumers finish their own migrations.

### Story 9.1: Lessons-learned ledger from GOLD3 pilot

As a **product owner**,
I want `docs/post-pilot/lessons-learned.md` capturing what went well, what didn't, what changed mid-cutover, and explicit go/no-go criteria for the GOLD1 + GOLD2 cutovers,
So that the rollout decisions are evidence-based.

**Acceptance Criteria:**

**Given** the pilot has run for 1-4 weeks
**When** the lessons-learned session is held
**Then** the ledger documents: every alert fired, every manual intervention, every drift detected, performance metrics vs. targets, operator feedback (cabin + forklift survey), and a list of fixes/refinements applied.

**Given** the go/no-go criteria
**When** the team evaluates against them (e.g., "0 P1 incidents in last 7 days", "drift = 0", "operator NPS > X")
**Then** a clear go/no-go decision is recorded with a date for the next cutover.

### Story 9.2: GOLD1 cutover Saturday

As an **ops engineer**,
I want to execute the cutover for GOLD1 following the same runbook as GOLD3 (Story 8.10) with any GOLD3-derived refinements,
So that GOLD1 operators move to the new system with the same < 30s rollback path.

**Acceptance Criteria:**

**Given** the GOLD3 lessons-learned ledger has informed runbook updates
**When** the GOLD1 cutover Saturday executes
**Then** it follows the updated runbook; any deviation is logged in real-time.

**Given** the cutover completes
**When** the smoke test runs
**Then** GOLD1 operates on the new system end-to-end; the team enters a 1-week reconciliation period before GOLD2.

### Story 9.3: GOLD2 cutover Saturday

As an **ops engineer**,
I want to execute the cutover for GOLD2 mirroring GOLD1's flow, completing the cabin-side rollout,
So that all three cranes are on the new system.

**Acceptance Criteria:**

**Given** GOLD1 has been stable for 1+ week
**When** the GOLD2 cutover Saturday executes
**Then** all three cranes report to the new listener; the legacy `TOSService.exe` and `TOSConsole{1,2,3}.exe` on `192.6.8.52` have no active TCP connections.

### Story 9.4: Legacy decommissioning

As an **ops engineer**,
I want `ops/runbooks/legacy-decommission.md` documenting the safe removal of `TOSService.exe`, `TOSConsole1/2/3.exe`, `KillToss1/2/3.exe`, `ConsolesReRun.exe`, `TosReRun.exe`, plus the disabling of `xp_cmdshell` SPs (`RunEnconsoleRTG{n}`, `KillToss{n}`),
So that the legacy attack surface and operational complexity is removed (FR7.5).

**Acceptance Criteria:**

**Given** all three cranes have been on the new system for 4+ weeks with 0 rollbacks
**When** the decommissioning runbook is executed
**Then** legacy services are stopped, binaries archived (not deleted — kept on a separate volume for 6 months), `xp_cmdshell` is disabled at the MSSQL server level, legacy SPs are dropped.

**Given** the decommissioning is complete
**When** any system tries to invoke the legacy listener
**Then** it gets a clean error (not a silent failure); MSSQL `xp_cmdshell` returns "disabled" if invoked.

### Story 9.5: MSSQL retirement plan + downstream consumer migration tracker

As a **product owner**,
I want `docs/post-pilot/mssql-retirement-plan.md` listing every downstream MSSQL consumer (MIS, AuditWeb-Web, WMS, legacy ForkliftApp variants, any other apps), their migration status, the trigger conditions for retiring MSSQL (or specific tables) entirely, and the dual-write off-ramp,
So that the modernization has a clear endgame and dual-write doesn't run forever.

**Acceptance Criteria:**

**Given** the plan is drafted
**When** stakeholders review it
**Then** every MSSQL consumer has an owner, a migration plan, and a target date.

**Given** a consumer migrates off MSSQL
**When** the plan is updated
**Then** the corresponding outbox aggregate type can be marked for retirement (dual-write disabled for that aggregate); the runbook documents the safe-disable procedure.

**And** when all consumers have migrated, MSSQL `TerminalData` is retired in a final cutover; the dual-write infrastructure is removed; the FDW views are dropped.

---
