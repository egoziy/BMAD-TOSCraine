---
stepsCompleted: [1, 2, 3, 4, 5, 6, 7, 8]
inputDocuments:
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
  - docs/brownfield/SESSION_STATE.md
  - KoneCranes/RTG/Network/RTG-TD-Konecranes TOS interface_V40.pdf
  - CurrentSystem/Current/From TFS/RTG/TOSService/TOSService/Program.cs
  - kickoff-conversation-2026-04-26 (locked-in decisions)
workflowType: 'architecture'
project_name: 'Craines-TOS'
user_name: 'Yaniv'
date: '2026-04-26'
prdStatus: 'waived — brownfield set + V40 spec + kickoff decisions serve as PRD equivalent (Path A, Yaniv 2026-04-26)'
lastStep: 8
status: 'complete'
completedAt: '2026-04-26'
---

# מסמך החלטות אדריכליות — Goldbond RTG Modernization

**פרויקט:** Craines-TOS · מודרניזציה של מערכת איתור מנופי RTG ב-Goldbond Ashdod
**אדריכל:** Winston (BMad)
**משתמש:** Yaniv
**תאריך התחלה:** 26 באפריל 2026
**שפת תיעוד:** עברית (לפי `_bmad/bmm/config.yaml:document_output_language`)

_מסמך זה נבנה צעד-אחר-צעד דרך גילוי משותף. סעיפים נוספים בכל איטרציה של ה-workflow._

---

## 0. רישום קלטים (Input Registry)

**הערת PRD:** PRD פורמלי לא הופק. בהסכמת Yaniv (2026-04-26, Path A במהלך step-01) — מסד הקלטים הבא משמש כתחליף ל-PRD לכל הליך התכנון. אם downstream skill (sprint planning, validation) ידרוש PRD מפורש — ייוצר wrapper דק בנקודת-הצורך, לא מראש.

### 0.1 קלטי תיעוד (Discovery + Spec)

| # | מסמך | תפקיד |
|---:|---|---|
| 1 | `docs/brownfield/00_SUMMARY.md` | תקציר ברונפילד · תחליף-PRD ראשי |
| 2 | `docs/brownfield/01_inventory.md` | אינוונטר מאגר הקוד (550 קבצים) |
| 3 | `docs/brownfield/02_components.md` | מיפוי 20 רכיבים, fork analysis, secrets register |
| 4 | `docs/brownfield/03_data_flows.md` | 7 זרימות end-to-end עם Mermaid |
| 5 | `docs/brownfield/04_database.md` | DB structure, ~125 אובייקטים, dual-write boundary |
| 6 | `docs/brownfield/05_protocol.md` | פרוטוקול ASCII-over-TCP — offsets, framing, checksum |
| 7 | `docs/brownfield/06_operator_ui.md` | RTGApp + ForkliftApp screen-by-screen |
| 8 | `docs/brownfield/07_edge_cases.md` | 31 edge cases (11 P1, 15 P2, 5 P3) |
| 9 | `docs/brownfield/08_binaries.md` | 15 בינארים, build dates, dependencies |
| 10 | `docs/brownfield/09_open_questions.md` | מרשם שאלות פתוחות (gate הוסר 2026-04-26) |
| 11 | `docs/brownfield/SESSION_STATE.md` | קובץ-המשכיות מ-Phase 1 |
| 12 | `KoneCranes/RTG/Network/RTG-TD-Konecranes TOS interface_V40.pdf` | ספק הפרוטוקול הקאנוני (vendor-authoritative) |
| 13 | `CurrentSystem/Current/From TFS/RTG/TOSService/TOSService/Program.cs` | מקור הליסטנר בייצור (TFS, drift קל מהבינארי הפרוס) |

### 0.2 החלטות שננעלו ב-kickoff (2026-04-26)

| # | החלטה | מקור |
|---:|---|---|
| K1 | **פיילוט = GOLD3** (קראן 3, BOND3 — הקטן והמבודד; כבר בעל code-branch נפרד) | Yaniv 2026-04-26 |
| K2 | **PG hosting = host חדש ייעודי על OT subnet `192.6.8.x`** (נפרד מ-`192.6.8.52`) — ממליץ Winston, Yaniv אישר ("whatever you recommend") | Winston rec, Yaniv approved 2026-04-26 |
| K3 | **ForkliftApp בהיקף** — החלפת `ForkliftApp.exe` (.NET 4.0) ב-Flutter forklift UI על אותה Rtg.Api tier | Yaniv 2026-04-26 |
| K4 | **NFR ראשי — security-by-design** — אין `sa`, אין credentials מובנים בקוד/config, אין הצגת סיסמאות בטקסט גלוי | Yaniv 2026-04-26 |
| K5 | **`xp_cmdshell` יוסר לחלוטין** — המערכת החדשה לא תעתיק את מודל ההרשאות הישן | Yaniv 2026-04-26 |
| K6 | **Haifa (`ILGBH`) מחוץ להיקף** — בחיפה אין מנופים; multi-terminal complexity לא חל | Yaniv 2026-04-26, סוגר Q-UI-07 |
| K7 | **DB מראה לבדיקות** = `10.10.200.51:49993` / `TerminalData_AI` / `GBDEV` | מאומת Phase 1 |

### 0.3 מתחים אדריכליים שזוהו ב-kickoff (לעיבוד בשלבים הבאים)

1. **Co-location vs. split** — נסגר ב-K2 (split, PG על host חדש על OT subnet)
2. **Pub/sub model** — מועמד: PG `LISTEN/NOTIFY` + WebSocket. דורש validation על payload size limit (8KB) ועל reconnect storm scenarios
3. **Dual-write pattern** — מועמד: app-level dual-write עם idempotency keys בפיילוט. שיקול-מחדש לפני scaling לקראנים 1+2
4. **Cabin auth UX** — פתוח: איך לשמר חוויית-PIN שמתאימה לכפפות תוך תיקון פגמי-הקיים (PIN לא מאומת מול LoginName, SQL injection, no lockout)
5. **Listener resilience** — פתוח: framing לפי KoneCranes V40, idempotency keys לרטראנסמיטים, transactional DB writes, DLQ ל-unknown frames

---

## 1. ניתוח קונטקסט הפרויקט (Project Context Analysis)

### 1.1 סקירת דרישות (Requirements Overview)

**הערה על מקור הדרישות:** PRD פורמלי לא קיים (Path A). הדרישות הפונקציונליות נגזרות מהברונפילד (מה שהמערכת הקיימת עושה היום = החוזה שהחדשה חייבת לשמר), בתוספת החלטות ה-kickoff (שמוסיפות את האילוצים של המודרניזציה).

#### דרישות פונקציונליות (FRs) — 8 תחומי-יכולת

| # | תחום | מקור |
|---:|---|---|
| FR-1 | **Crane wire-protocol listener** — קבלת TCP מ-PLC על port ייעודי (30701/2/3); פירוק הודעות YARDIT v4.0 (A1/A2 03/A2 04/A3); שליחת broadcast jobs מ-job queue (B3/B4); שליחת ACK (`04B2`+counter+checksum); idempotency על retransmits; framing לפי length byte של V40; validation של checksum נכנס; DLQ ל-unknown frames | brownfield/05 + V40 spec |
| FR-2 | **Cabin operator UI (Flutter)** — login עם operator + PIN בן 4 ספרות; מפת חצר חיה (BOND1/2/3 cells); אינדיקטור מיקום-קראן; submit PICK/PLACE; cancel עם hold-to-confirm; עדכון איתור ידני; truck mode (פריקה+טעינה); work-orders; info reports (empty/no-location/expected/diary); RTL עברית; touchscreen-first; multi-crane (CHE נבחר ב-login); WebSocket push | brownfield/06 + K3 |
| FR-3 | **Forklift driver UI (Flutter)** — login (username + 7-char + forklift#); מסך מידע מכולה; קיצורי F-keys (F2-F12); activity reporting (PICK/PLACE); 14+ מסכי-משנה; אותה Rtg.Api tier; single-terminal (Ashdod בלבד) | brownfield/06 + K3 + K6 |
| FR-4 | **REST + WebSocket API (Rtg.Api)** — endpoints לכל פעולת UI; ~80 sp_ForkLift\* equivalents כ-endpoints; yard map queries; container detail composites; truck queues; work-orders; reports | brownfield/04 + 06 |
| FR-5 | **Persistence layer** — PG primary (host חדש על OT subnet); schema ל-crane_state/job/location/shifting/error_log/session_log/outbox/parameters; pub/sub דרך LISTEN/NOTIFY + WebSocket fanout; **dual-write ל-MSSQL TerminalData** במהלך מעבר (MIS + AuditWeb + WMS עדיין צורכים מ-MSSQL) | brownfield/04 + K2 |
| FR-6 | **Auth & identity** — user store; PIN/password מאומתים server-side (אין trust ב-client); audit trail (RG_Log equivalent) קשור ל-actor מאומת; lockout אחרי N נסיונות כושלים; logout / session expiry | brownfield/06 §6 + K4 |
| FR-7 | **Process supervision** — החלפת `ConsolesReRun` + `TosReRun` + `KillToss` בפלטפורמה (Windows Service / systemd / container runtime); אין יותר `xp_cmdshell` SPs | brownfield/02 §4.3 + K5 |
| FR-8 | **Migration / cutover** — rollout crane-by-crane החל מ-GOLD3; חלון Saturday cutover; rollback path < 30s; idempotency keys לטיפול ב-replay במהלך cutover | K1 + brownfield/00 §6 |

#### דרישות לא-פונקציונליות (NFRs) — 10 קריטיות

| # | NFR | מקור | מדד-הצלחה |
|---:|---|---|---|
| NFR-1 | **Security-by-design** — אין `sa`, אין credentials מובנים, אין plaintext passwords; parameterized queries (אין SQL injection); rate limiting + lockout; whitelist על listener ports (PLC IPs בלבד); אין `xp_cmdshell` | K4 + K5 + brownfield/00 §4 | 0 secrets בקוד; 100% queries מ-prepared statements; lockout מוטמע |
| NFR-2 | **Real-time UX** — עדכוני map ב-cabin תוך ≤200ms מ-state change (לעומת polling 500ms היום); PLC ACK round-trip ≤1s; WebSocket reconnect ≤5s | brownfield/03 §1 + 06 | p95 cabin push ≤200ms; p99 ACK ≤1s |
| NFR-3 | **Data integrity** — PLACE flow טרנזקציוני (היום 5 טבלאות בסיכון inconsistent state); idempotency keys על retransmits; inbound checksum validation; DLQ + alerting ל-unknown frames | brownfield/07 #12-13 + V40 §2.8 | 0 partial-PLACE writes ב-load test; 100% retransmits dedup |
| NFR-4 | **Reliability** — auto-restart על crash; graceful shutdown (drain in-flight); PG primary HA path מוגדר (אינו חובה ב-pilot, מתועד); offline write-queue ב-forklift | brownfield/02 §4.3 + 06 §7 | MTTR < 60s; 0 lost messages ב-controlled crash |
| NFR-5 | **Observability** — structured logs (לא UNC paths); metrics ל-throughput/latency; alerts על disconnect/write-fail/DLQ-growth | brownfield/03 §6 + 07 #24 | dashboard עם 4 SLI core; alerts מוגדרים |
| NFR-6 | **Migration safety** — dual-write עם eventual consistency tolerance; idempotency keys לטיפול ב-replay; read-back verification לפני flip | brownfield/04 §7 + K2 | rollback ≤30s validated ב-DR drill |
| NFR-7 | **Performance / scale baseline** — 3 cranes × 1-2 A1/sec ≈ 6 msg/sec inbound; ~10 jobs/min/crane peak; 3 cabin clients + 5-15 forklift clients concurrent; CO_Containers 2.6M rows | brownfield/04 §3 + 03 §1 | sustained 10x baseline ב-load test |
| NFR-8 | **Compliance / audit** — audit trail trustworthy (PIN bound to LoginName, לא ל-group); Web audit pattern (AuditWeb) משומר או מוחלף | brownfield/04 §8 + 07 #6 | 0 forgeable audit entries; AuditWeb-equivalent מתועד |
| NFR-9 | **i18n** — RTL Hebrew בכל UI מפעיל; אין multi-language switcher (Hebrew בלבד) | brownfield/06 §2.5 + §3.4 | 100% operator strings ב-Hebrew |
| NFR-10 | **Hardware / network constraints** — Cabin PCs קיימים (Windows touchscreens); forklift tablets קיימים (rugged + F-keys); PG host TBD (תקציב פתוח); OT subnet 192.6.x.x עם mGuard NAT לכל crane | brownfield + KoneCranes connectivity PDF | תאימות מלאה ל-fleet קיים |

### 1.2 היקף וסיבוכיות (Scale & Complexity)

- **רמת סיבוכיות: גבוהה** (לא enterprise, אך הרבה מעל בינוני). מכפילים: real-time wire protocol vendor-spec'd × brownfield dual-write × שני operator personas × hard security NFRs.
- **תחום טכני ראשי:** full-stack עם real-time edge (TCP listener) ו-brownfield migration twist
- **רכיבים אדריכליים מוערכים: 8-10**:
  1. **Rtg.Listener** — TCP server לכל crane (מאוחד או לכל crane בנפרד — החלטה ב-step 03+)
  2. **Rtg.WireProtocol** — codec library משותפת ל-V40 (encode/decode/checksum/framing)
  3. **Rtg.Api** — HTTP REST + WebSocket SignalR
  4. **Rtg.MssqlDualWrite** — sync writer עם idempotency
  5. **Rtg.Auth** — identity + PIN check + lockout + audit
  6. **Cabin Flutter app**
  7. **Forklift Flutter app**
  8. **PG schema + LISTEN/NOTIFY layer**
  9. **Supervision/orchestration** (Windows Service או systemd או container runtime)
  10. **(אופציונלי)** Observability stack (logs/metrics/alerts) — נפרד או built-in

### 1.3 אילוצים טכניים ותלויות (Technical Constraints & Dependencies)

| # | אילוץ | משמעות אדריכלית |
|---:|---|---|
| C1 | **PLC = KoneCranes BoxHunter G2036/G2037** עם YARDIT PDS v4.0 hard-coded | אין שינוי ב-PLC; ה-listener חייב להתאים ל-spec הוונדור 100% |
| C2 | **MSSQL `TerminalData` שותף עם MIS/ForkliftApp(legacy)/AuditWeb-Web/WMS** | dual-write הכרחי; אין לוקסוס לקצוץ את MSSQL בפיילוט |
| C3 | **Cabin PCs ו-forklift tablets קיימים — Windows-based** | UI חדש חייב לרוץ על Windows desktop (Flutter Desktop = יתרון) |
| C4 | **OT network 192.6.x.x עם mGuard NAT לכל crane** | network design של PG host ו-listener חייב לתאם את ה-firewall topology הקיים |
| C5 | **Cutover window = שבת בלבד** | rollout iterative; rollback path מוטמע |
| C6 | **Hebrew RTL** | Flutter package + locale handling |
| C7 | **TFS legacy source = source of truth ל-listener behavior** (drift קל מהבינארי) | התנהגות ה-listener החדש תתבסס על הקריאה של ה-TFS source + V40 spec; verification ב-cutover |

### 1.4 דאגות חוצות (Cross-Cutting Concerns)

| דאגה | היכן זה מופיע | החלטה אדריכלית נדרשת |
|---|---|---|
| **Idempotency** | listener retransmits, WebSocket reconnect, dual-write replay | מודל מפתח-idempotency (counter+CHE+msg_type?); נפח retention; reconciliation |
| **Audit identity** | Cabin login, Forklift login, all PICK/PLACE/Cancel | unified user store? AD integration? PIN+username binding mechanism |
| **Migration consistency** | כל פעולת write ב-period המעבר | dual-write semantics (sync vs. async, ack-strategy, replay-on-fail) |
| **Observability** | listener, API, dual-writer, UI | structured logging stack; metrics; alerting policy |
| **Security/transport** | TCP listener, WebSocket, REST | TLS strategy ב-OT network (PLC לא תומך TLS); WebSocket auth tokens; REST auth |
| **RTL & touch UX** | cabin app, forklift app | Flutter package selection; common widgets; gesture handling |
| **Localization of error messages** | כל UI surface | Hebrew strings catalog; runtime-lookup vs. compiled |

---

## 2. הערכת Starter Templates ו-Stack טכנולוגי (Tech Stack)

### 2.1 תחום טכנולוגי ראשי

Full-stack עם real-time edge: TCP listener (server-side), REST + WebSocket API, Flutter desktop apps × 2, PG primary, dual-write ל-MSSQL.

### 2.2 ה-Stack שנבחר

| רכיב | טכנולוגיה | גרסה (אומתה web 2026-04-26) | רציונל |
|---|---|---|---|
| Listener + API + libs | **.NET 10 LTS** (multi-project solution) | 10.0.7 — תמיכה עד Nov 2028 | התאמת-team, הליבה הנוכחית .NET, `System.IO.Pipelines` מצטיין בפרוטוקולים בינאריים, LTS ארוך |
| Cabin desktop UI | **Flutter 3.41.5** (Windows desktop) | stable מ-Feb 2026 | tooltip/dialog windows experimental, RTL-ready, touchscreen-first |
| Forklift desktop UI | **Flutter 3.41.5** (Windows desktop) | זהה | אותו toolchain כמו cabin → ייעול פיתוח |
| PostgreSQL primary | **PostgreSQL 18.3** | latest stable מ-2026-02-26 | LISTEN/NOTIFY, partitioning, JSONB; well-understood |
| DB migrations | **dbmate** (flat SQL) | — | flat SQL = transparent ובר-review לאורך migration מ-MSSQL |

### 2.3 חלופות שנשקלו ונפסלו

| חלופה | סיבת דחייה |
|---|---|
| Go ל-listener | אין familiarity ב-team; דרג חוסר-יעילות במהלך מודרניזציה רגישה |
| Node.js ל-API | חלש יותר על binary protocols; mixed-language repo = overhead כלי-פיתוח |
| Rust | learning curve תלול מדי במהלך מודרניזציה in-flight |
| EF Core migrations (במקום dbmate) | generator יכול להסתיר surprises; flat SQL נקי ובר-audit |

### 2.4 מבנה ה-Repo (Monorepo)

```
craines-tos/
├── server/                  # .NET 10 solution
│   ├── Rtg.sln
│   ├── Rtg.WireProtocol/    # V40 codec
│   ├── Rtg.Listener/        # Worker Service — TCP per crane
│   ├── Rtg.Api/             # Minimal API + SignalR
│   ├── Rtg.Persistence/     # Npgsql + SqlClient
│   ├── Rtg.DualWrite/       # MSSQL sync writer
│   ├── Rtg.Auth/            # PIN/auth/lockout/audit
│   └── Rtg.Tests/           # xUnit
├── cabin/                   # Flutter Windows desktop
├── forklift/                # Flutter Windows desktop
├── db/migrations/           # dbmate — Vnnnn__name.sql
├── docs/
└── ops/                     # supervision + deployment
```

### 2.5 פקודות אתחול (Initialization Commands)

```bash
# Server (.NET 10 solution)
mkdir server && cd server
dotnet new sln -n Rtg
dotnet new classlib -n Rtg.WireProtocol -f net10.0
dotnet new worker    -n Rtg.Listener     -f net10.0
dotnet new webapi    -n Rtg.Api          -f net10.0 --use-minimal-apis
dotnet new classlib  -n Rtg.Persistence  -f net10.0
dotnet new classlib  -n Rtg.DualWrite    -f net10.0
dotnet new classlib  -n Rtg.Auth         -f net10.0
dotnet new xunit     -n Rtg.Tests        -f net10.0
dotnet sln add **/*.csproj
cd ..

# Flutter cabin + forklift
flutter create cabin    --platforms=windows --project-name=rtg_cabin
flutter create forklift --platforms=windows --project-name=rtg_forklift

# DB migrations
mkdir -p db/migrations db/seeds
# dbmate installed separately; run: dbmate new init_schema
```

**הערה:** סיפור היישום הראשון (story #1) של ה-implementation = ביצוע ה-init commands האלה ויצירת ה-skeleton structure.

### 2.6 החלטות נלוות (Confirmed 2026-04-26)

| מאפיין | בחירה | רציונל |
|---|---|---|
| API style | REST (Minimal API) + SignalR למצב push | מודרני, low-ceremony, מתאים ל-real-time UX |
| ORM/DAL | **Dapper** ל-PG + SqlClient ל-MSSQL | אין EF magic; שליטה בשאילתות; ביצועים טובים |
| Testing | xUnit + Bogus + **Testcontainers** (PG-in-Docker ל-integration tests) | בסיס סטנדרטי .NET |
| Auth tokens | JWT short-lived + refresh בצד server | סטנדרט; pairs עם WebSocket auth |
| Logging | **Serilog** + structured JSON output | תחליף ל-UNC text logs של legacy |
| Container strategy (pilot) | **PG ב-Docker; .NET stack כ-Windows Services bare-on-VM** | אישור Yaniv 2026-04-26 — תואם למודל ה-ops של IT |

### 2.7 תשתית ו-Identity (Confirmed 2026-04-26)

#### Source Control + CI
- **GitHub** (אישור Yaniv 2026-04-26)
- CI דרך **GitHub Actions** ב-`.github/workflows/`:
  - Server job: `dotnet build` + `dotnet test`
  - Flutter jobs (×2): `flutter analyze` + `flutter test`
  - DB job: SQL lint על `db/migrations/`
- Secrets ב-**GitHub Secrets + environment-scoped** (dev/staging/prod) — אין credentials מובנים בקוד (תואם NFR-1)

#### Hosting
- **VM יחידה על OT subnet `192.6.8.x`** (אישור Yaniv 2026-04-26 — "המערכת תשב על VM כמו היום")
- VM אחת מארחת:
  - PostgreSQL 18.3 (Docker container)
  - `Rtg.Listener` (Windows Service)
  - `Rtg.Api` (Windows Service)
  - `Rtg.DualWrite` (Windows Service)
- Sizing מוערך: 4-8 vCPU, 16-32 GB RAM, ~500 GB SSD
- Backup: VM snapshots + scheduled `pg_dump` ל-volume נפרד — תואם למודל ה-backup של MSSQL היום
- **HA path (מתועד, לא ב-pilot)**: PG streaming replication ל-warm standby על VM שנייה; failover ע"י DNS/VIP. שיקול מחדש לפני scale לקראנים 1+2

#### Identity / Auth — Active Directory Integration
- **AD פעיל ב-Goldbond** (אישור Yaniv 2026-04-26)
- **המודל הנבחר: AD-2** — AD = directory of truth, PIN ב-PG hashed:

| היבט | מימוש |
|---|---|
| Source of "מי זה עובד?" | AD (LDAP query / sync) — שם, תפקיד, group membership, employment status |
| Source of "מה ה-PIN/password?" | PG `rtg.users` table — PIN hashed (Argon2id או bcrypt) |
| Login flow | UI: pick username (from AD-synced list) + enter PIN/password → server validates: (a) PIN hash matches, (b) AD says user still active in correct group |
| Off-boarding | אוטומטי — AD disable מגיע לאחר sync, חשבון ה-user ב-rtg.users נחסם או נמחק |
| Audit | כל פעולה (PICK/PLACE/Cancel) קשורה ל-AD principal (user@goldbond.local), לא רק ל-rtg.users.id |
| Lockout | server-side, 5 נסיונות תוך 15 דק' → חסימה ל-30 דק' |
| Cabin UX | זהה ל-legacy: PIN בן 4 ספרות; אין AD password ב-touchscreen |
| Forklift UX | זהה: 7-char password (תוארך ל-8+ ב-rollout שני, אם ירצו) + forklift number |

חלופות שנדחו:
- **AD-1 (full OIDC)** — דורש typing של AD password ב-cabin; לא מעשי עם כפפות
- **AD-3 (ignore AD)** — מבזבז את ה-AD; off-boarding ידני; נכשל NFR-8 (audit trustworthy)

---

## 3. החלטות אדריכליות מרכזיות (Core Architectural Decisions)

> כל ההחלטות הצרות בסעיף זה אושרו ע"י Yaniv ב-2026-04-26 ("All as your recommendations").

### 3.1 ניתוח עדיפות החלטות

**החלטות קריטיות (חוסמות יישום) — כולן נסגרו:**
- Stack: .NET 10 LTS + Flutter 3.41.5 + PG 18.3 + dbmate (steps 2-3)
- Hosting: VM יחיד על OT subnet (step 3)
- Identity: AD-2 (step 3)
- כל ההחלטות בסעיפים 3.2-3.6 שלהלן

**החלטות חשובות (מעצבות אדריכלות) — כולן נסגרו:**
- Pub/sub channel design (3.2)
- Idempotency model (3.2)
- Dual-write semantics (3.2)
- JWT signing + lifetime (3.3)
- WebSocket hub design (3.4)
- Logging + metrics stack (3.6)

**החלטות נדחות (Post-MVP):**
- HA / streaming replication ל-PG (לאחר scale-out מעבר ל-pilot)
- Blue/green deploy (כשנגיע למימוש מתקדם)
- Hashicorp Vault / Azure Key Vault (במקום env vars + file-perm) — שיקול-מחדש כשנעבור scale
- Secondary monitoring (APM כמו AppDynamics/Dynatrace) — overkill ל-pilot

---

### 3.2 ארכיטקטורת נתונים (Data Architecture)

| # | החלטה | בחירה | הערות |
|---:|---|---|---|
| D1.1 | History retention | **90 ימים hot ב-PG, archive ל-cold storage לאחר מכן** | מתאים ל-RG_A1_LOG-equivalent (~470M שורות/שנה ב-legacy = lose 90% מהעומס מיד) |
| D1.2 | Dual-write failure mode | **Ack to user; outbox מטפל ב-MSSQL בנפרד** | PG הוא primary; MSSQL הוא downstream consumer |
| D-DA-1 | PG schema namespace | `rtg.*` לכל הטבלאות ה-RTG; `mssql_mirror.*` ל-read-mirrors של MSSQL views | גבול נקי; הרשאות per-schema |
| D-DA-2 | מודל טבלאות — state vs. event | **Event-sourced ל-crane telemetry** (`rtg.crane_position` append-only עם current-state view); **state-based ל-jobs/locations** (mutable rows + audit trail ב-`rtg.audit_log`) | telemetry טבעית append-only; jobs צריכים queryable current state |
| D-DA-3 | Partitioning | חודשי ל-`rtg.crane_position`, `rtg.shifting`, `rtg.audit_log` | retention 90-יום triviai עם partitioning |
| D-DA-4 | Pub/sub channels | אחד לכל `(crane, topic)` — `rtg.gold3.position`, `rtg.gold3.job`, `rtg.bond3.map` | payload < 8KB ע"י שליחת IDs בלבד; cabin client מאזין ל-channel הספציפי שלו |
| D-DA-5 | Idempotency key | inbound listener: `(crane_id, counter, msg_type)` תואם V40; API requests: UUID v7 (sortable) | listener key תואם spec; UUID v7 ידידותי ל-DB indexes |
| D-DA-6 | Dual-write pattern | **App-level synchronous + outbox fallback**: PG first → MSSQL inline; אם MSSQL נכשל → `rtg.dual_write_outbox` ל-retry | sync = MSSQL consumers fresh; outbox = failure non-blocking |
| D-DA-7 | Reconciliation | hourly cron — diff בין `rtg.shifting` ל-`RG_Shifting`; alert על drift | מזהה silent drift לפני שמתפשט |
| D-DA-8 | Caching | מינימלי — רק AD lookup TTL 5min ו-JWT signing keys in-memory | premature caching = stale-data bugs |

---

### 3.3 הזדהות ואבטחה (Authentication & Security)

| # | החלטה | בחירה | הערות |
|---:|---|---|---|
| D2.1 | Internal CA | **self-signed ל-pilot**; cert-rotation plan מתועד; transition ל-CA פנימי או internal CA חדש כשנעבור scale | מתאים לפיילוט; לא חוסם |
| D2.2 | Firewall whitelist owner | **IT — Phase-2 ops handoff**, נדרש איש קשר ב-IT לזיהוי + תחזוקה | runbook אופרטיבי לקראת step-3 (deployment) |
| D-AS-1 | AD integration | **LDAP sync via cron** (every 15 min) ל-`rtg.users` mirror; AD authoritative ל-`(active, group_membership, email)` | login path אינו תלוי ב-AD בזמן ריצה; off-boarding window 15-min מקובל |
| D-AS-2 | PIN hashing | **Argon2id** (64MB / 3 iter / 4 par) | OWASP-recommended; bcrypt fallback אם .NET libs awkward |
| D-AS-3 | JWT signing | **RS256** asymmetric — private key רק על השרת; cabin/forklift verify עם public key | מונע forgery אם compromise על cabin |
| D-AS-4 | JWT lifetime | access 15 min · refresh 8 hours · refresh tokens ב-`rtg.refresh_tokens` עם revocation | 8 hours = משמרת אחת |
| D-AS-5 | Lockout | 5 attempts ב-15 דק' → lockout 30 דק', per `(user, source_ip)` | מונע brute force; לא חוסם fat-fingered operator |
| D-AS-6 | Listener transport (PLC↔server) | **ללא TLS** (PLC לא תומך); compensating: firewall whitelist + OT subnet + audit logging | OT reality; defense-in-depth via network |
| D-AS-7 | API/WebSocket transport | **TLS terminated ב-reverse proxy** (IIS או nginx) מול Kestrel; HTTP→HTTPS redirect; HSTS | סטנדרט; cert מ-CA פנימי / self-signed |
| D-AS-8 | Secrets at runtime | **env vars מ-`/etc/rtg/secrets.env` (Linux) או service-account env block (Windows Service)** — file mode 0600, owner = service account | אין vault ב-pilot; שיקול-מחדש ב-scale |
| D-AS-9 | Audit trail | `rtg.audit_log` — login/logout, PIN attempts, PICK/PLACE/Cancel, manual location edit, role change. Tied ל-AD principal. Mirror ל-MSSQL `AuditWeb` ל-consumer compatibility | טריוויאלי trustworthy (NFR-8); משמר Web consumer pattern |

---

### 3.4 API ותקשורת (API & Communication)

| # | החלטה | בחירה | הערות |
|---:|---|---|---|
| D-AC-1 | API versioning | URL path: `/api/v1/...` | הברור ביותר; CDN/proxy routing קל |
| D-AC-2 | Error format | **RFC 7807 Problem Details** (`application/problem+json`) | סטנדרט; .NET 10 Minimal API תומך native |
| D-AC-3 | OpenAPI spec | auto-generated מ-Minimal API endpoints דרך `Microsoft.AspNetCore.OpenApi` | single source of truth; Flutter codegen ל-DTOs |
| D-AC-4 | WebSocket / SignalR hub | Hub יחיד `RtgHub` עם topic-based subscriptions (`SubscribeToCrane(craneId)`, `SubscribeToBlock(blocCode)`); typed events | חיבור יחיד per client; subscriptions managed server-side |
| D-AC-5 | Keepalive | SignalR built-in heartbeat 15s; cabin reconnect with exponential backoff (max 30s) | NFR-2: ≤5s reconnect after detection |
| D-AC-6 | Rate limiting | per-user + per-endpoint דרך `Microsoft.AspNetCore.RateLimiting`; defaults: 100 req/min/user reads, 30 writes | מגן על API; לא bottleneck legit |
| D-AC-7 | Cross-component comms | **In-process via DI** ל-Listener→Persistence→DualWrite; `rtg.dual_write_outbox` ל-async retry | VM יחיד = אין צורך ב-message broker |
| D-AC-8 | Listener→Cabin push path | Listener → PG `NOTIFY` → API process `LISTEN` → SignalR push ל-subscribed clients | one-way; cabin queries ב-REST, מקבל push ב-WebSocket |

---

### 3.5 ארכיטקטורת Frontend (Flutter)

| # | החלטה | בחירה | הערות |
|---:|---|---|---|
| D4.1 | Auto-update mechanism | **Self-updater** — App מוריד build חדש מ-API; check-for-updates ב-startup ו-hourly | לא תלוי ב-SCCM/Intune; ניהול דרך GitHub Actions שדוחף artifacts ל-API |
| D4.2 | Branding assets | **שימוש ב-templates ב-`CurrentSystem/RTG-Template/`** — `goldbond-logo.png`, prototypes ב-4 סגנונות (bold/conservative/data/modern) ב-JSX + HTML preview, design_handoff folders ל-login ול-cabin | אישור Yaniv 2026-04-26; בחירת style ב-step-05 (implementation patterns) או אחרי |
| D-FE-1 | State management | **Riverpod 2.x** | מודרני Flutter; compile-safe |
| D-FE-2 | Routing | **go_router** | declarative; deep-linking-ready |
| D-FE-3 | HTTP client | **Dio** + retry interceptor + auth interceptor (auto-refresh JWT) | battle-tested |
| D-FE-4 | WebSocket / SignalR client | **`signalr_netcore`** (community Dart SignalR client) | תאימות ישירה ל-.NET hub |
| D-FE-5 | Secure storage | **`flutter_secure_storage`** (Windows DPAPI-backed) ל-tokens | OS-level encryption |
| D-FE-6 | Offline cache | **Hive (CE/Lazy)** רק לפורקליפט (cache + write queue); cabin online-only | forklift נע בחצר עם network gaps; cabin = online-only |
| D-FE-7 | Forms | **flutter_form_builder** + custom validators | סטנדרט |
| D-FE-8 | L10n / RTL | **`flutter_localizations` + `intl`**, single Hebrew locale, `Directionality.rtl` ב-root | Hebrew only — אין switcher |
| D-FE-9 | Theming | **Material 3** + Goldbond brand palette (מ-RTG-Template) | accessible defaults |
| D-FE-10 | F-key handling (forklift) | **`Shortcuts` + `Actions`** widgets ל-`LogicalKeyboardKey.f2..f12` | native Flutter pattern |
| D-FE-11 | Touchpoint sizing | **min 60px primary actions** (gloves-friendly); 44px ל-info-dense | gloves > Material default |

---

### 3.6 תשתית ופריסה (Infrastructure & Deployment)

| # | החלטה | בחירה | הערות |
|---:|---|---|---|
| D5.1 | Logging stack | **Seq** (Docker container על ה-VM) | DX מעולה ל-Serilog; אין logging stack קיים ב-Goldbond |
| D5.2 | Alert channel | **Email** (Grafana Alertmanager → SMTP) | מתאים לפיילוט; קל לשנות ל-Slack/Teams בהמשך |
| D5.3 | Deploy transport | **GitHub Actions self-hosted runner על ה-VM** — builds + deploys in-place | פשוט; אין SSH/WinRM credentials מנוהלים |
| D-IN-1 | VM OS | **Windows Server 2022** | תואם IT; .NET 10 + Docker-on-Windows-Server יציבים |
| D-IN-2 | VM provisioning | **manual via existing virtualization** (vSphere/Hyper-V — IT decides) | pilot — Terraform overkill ל-VM יחידה |
| D-IN-3 | Configuration | `appsettings.json` (defaults) + `appsettings.Production.json` (overrides) + env vars (secrets) | .NET-native; קל ל-diff |
| D-IN-4 | Metrics | **Prometheus + Grafana** (ב-Docker על ה-VM) | סטנדרט; SLIs מ-NFR-5 |
| D-IN-5 | Deployment pipeline | GitHub Actions: `main` push → build → upload artifact → `dbmate up` → restart Windows Services | one-click; rollback = re-deploy previous tag |
| D-IN-6 | Blue/green | **לא ב-pilot**; מתועד ל-production scale | VM יחידה = blue/green דורש VM שנייה |
| D-IN-7 | Backup | VM snapshots + nightly `pg_dump` ל-volume נפרד + offsite copy weekly | תואם MSSQL backup model היום |

---

### 3.7 ניתוח השפעת החלטות (Decision Impact Analysis)

#### רצף יישום מוצע

1. **Foundation** — repo init, .NET solution skeleton, Flutter projects, dbmate scaffolding, GitHub Actions skeleton
2. **Wire protocol library** — `Rtg.WireProtocol` עם encode/decode/framing/checksum + xUnit tests מול chesimu logs
3. **PG schema** — `rtg.*` tables + LISTEN/NOTIFY triggers + dbmate migrations + Testcontainers integration tests
4. **Listener** — `Rtg.Listener` Worker Service מול PG; שלב ראשון רק קליטה (read-only writes ל-PG)
5. **Auth + AD sync** — `Rtg.Auth` + LDAP sync cron + JWT issuance + `rtg.refresh_tokens`
6. **API tier** — `Rtg.Api` Minimal API + SignalR hub + OpenAPI spec
7. **DualWrite** — `Rtg.DualWrite` עם outbox pattern; reconciliation cron
8. **Cabin app** — Flutter cabin: login → map → PICK/PLACE → cancel → reports
9. **Forklift app** — Flutter forklift: login → information → activity → 14 sub-screens
10. **Observability** — Serilog → Seq, Prometheus + Grafana dashboards, alert rules
11. **Pilot deployment** — VM provision, GitHub Actions self-hosted runner, services deploy, dry-run with chesimu
12. **Saturday cutover GOLD3** — flip GOLD3 listener to new system; legacy in standby; 30s rollback window
13. **Reconciliation period** — 1-4 weeks of dual-running, drift monitoring
14. **Rollout GOLD1, GOLD2** — separate Saturday windows
15. **MSSQL retirement plan** — when downstream consumers (MIS, AuditWeb, WMS) finish their own migrations

#### תלויות חוצות-רכיבים (Cross-Component Dependencies)

| תלות | משמעות |
|---|---|
| Listener → PG schema → API → Cabin | שלב ה-Cabin push (NOTIFY→SignalR) דורש שכבות 1-3 פעילות |
| AD sync → Auth → API auth → Cabin login | Cabin login לא יעבוד עד ש-AD sync רץ ויש tokens |
| WireProtocol library → Listener + Tests + chesimu | shared codec דורש tests מקיפים לפני ה-Listener |
| OpenAPI spec → Flutter codegen → cabin/forklift DTOs | ה-clients תלויים ב-spec יציב |
| DualWrite + outbox → reconciliation cron → drift alerts | רצף אופרטיבי — אם dual-write מנותק, drift alerts מתחילים לדעוך |

---

## 4. דפוסי יישום וחוקי עקביות (Implementation Patterns & Consistency Rules)

### 4.1 גבולות עיצוב

מטרת הסעיף: לנעול קונבנציות מספיק חזק כדי ששני AI agents (או שני מפתחים) שכותבים חלקים שונים של ה-codebase יייצרו קוד תואם. **כל החלטה כאן היא חוזה מחייב על ה-implementation team.** סטיות דורשות עדכון מסמך זה.

### 4.2 קונבנציות שמות (Naming Conventions)

#### 4.2.1 PostgreSQL

| תחום | קונבנציה | דוגמה |
|---|---|---|
| Schema | snake_case | `rtg`, `mssql_mirror` |
| Table | snake_case, plural | `rtg.users`, `rtg.crane_positions`, `rtg.shifting_history` |
| Column | snake_case | `user_id`, `created_at`, `crane_id`, `loc_code` |
| Primary key | `id` (BIGINT) או `id UUID DEFAULT uuidv7()` | — |
| Foreign key | `<referenced_singular>_id` | `user_id` → `users.id` |
| Index | `idx_<table>_<columns>` | `idx_users_login_name` |
| Unique constraint | `uq_<table>_<columns>` | `uq_users_login_name` |
| Check constraint | `ck_<table>_<column>_<rule>` | `ck_users_pin_min_length` |
| Trigger | `tg_<table>_<event>_<action>` | `tg_shifting_after_insert_notify` |
| Function | snake_case verb-first | `notify_position_update`, `compute_checksum` |

#### 4.2.2 REST API

| תחום | קונבנציה | דוגמה |
|---|---|---|
| Path | `/api/v1/<resource>` — plural, kebab-case | `/api/v1/cranes/gold3/jobs`, `/api/v1/work-orders` |
| Path param | `{id}` (Minimal API style) | `/api/v1/jobs/{id}` |
| Query param | camelCase | `?craneId=gold3&blocCode=BOND3` |
| Header | `X-` prefix לקסטם, kebab-case | `X-Request-Id`, `X-Idempotency-Key` |
| HTTP verb | תקני REST | GET=list/read, POST=create, PUT=full-update, PATCH=partial, DELETE |
| Status codes | סטנדרט HTTP | 200/201/204/400/401/403/404/409/422/429/500 |

#### 4.2.3 .NET / C#

| תחום | קונבנציה | דוגמה |
|---|---|---|
| Namespace | `Rtg.<Area>` | `Rtg.Listener`, `Rtg.Api.Cranes`, `Rtg.WireProtocol.V40` |
| Class | PascalCase | `CraneSessionService`, `JobRepository` |
| Interface | `I` prefix + PascalCase | `IJobRepository`, `IWireProtocolDecoder` |
| Method | PascalCase | `PublishJobAsync`, `ValidateChecksum` |
| Async method | `Async` suffix | `GetUserAsync`, `WriteToOutboxAsync` |
| Field (private) | `_camelCase` | `_logger`, `_connectionFactory` |
| Property | PascalCase | `CraneId`, `IsActive` |
| Variable / parameter | camelCase | `craneId`, `messageBytes` |
| Constant | PascalCase | `MaxRetryAttempts` (לא SCREAMING_CASE) |
| File | `<ClassName>.cs`, אחת לקובץ | `JobRepository.cs` |
| Folder | feature-based | `Cranes/`, `Forklifts/`, `Auth/`, `WireProtocol/` |

#### 4.2.4 Dart / Flutter

| תחום | קונבנציה | דוגמה |
|---|---|---|
| File | snake_case | `crane_session_service.dart`, `yard_map_screen.dart` |
| Class | PascalCase | `CraneSessionService`, `YardMapScreen` |
| Variable / method | camelCase | `craneId`, `submitJob()` |
| Private member | `_camelCase` | `_authService`, `_validatePin()` |
| Riverpod provider | `<thing>Provider` | `craneStateProvider`, `yardMapProvider` |
| Folder structure | feature-first + clean architecture | `lib/features/login/{data,domain,presentation}/` |
| Cross-cutting | `lib/core/` | `lib/core/network/`, `lib/core/auth/`, `lib/core/theme/` |
| Shared widgets | `lib/widgets/` | `lib/widgets/touch_button.dart` |

#### 4.2.5 Events (PG NOTIFY + SignalR)

| תחום | קונבנציה | דוגמה |
|---|---|---|
| PG channel | `<schema>.<crane>.<event>` | `rtg.gold3.position`, `rtg.bond3.location_updated`, `rtg.gold3.job_state_change` |
| PG NOTIFY payload | JSON minimal — id + ts + type | `{"id": 12345, "ts": "2026-04-26T13:45:00Z", "type": "position"}` |
| SignalR method (server→client) | `On<EventName>` PascalCase | `OnPositionUpdate`, `OnJobStateChange`, `OnLocationUpdated` |
| SignalR subscription | `SubscribeTo<X>(id)` / `UnsubscribeFrom<X>(id)` | `SubscribeToCrane("gold3")` |
| Event DTO | PascalCase typed class | `PositionUpdateEvent { CraneId, Time, Status, ... }` |
| JSON field in event | camelCase | `{"craneId": "gold3", "blocCode": "BOND3", ...}` |

### 4.3 דפוסי מבנה (Structure Patterns)

#### 4.3.1 .NET solution

```
server/
├── Rtg.sln
├── Rtg.WireProtocol/        # codec library, אין dependencies חיצוניות מלבד BCL
├── Rtg.Persistence/         # repositories, models, Dapper queries
├── Rtg.Auth/                # JWT, PIN hash, AD sync, lockout
├── Rtg.DualWrite/           # MSSQL outbox + sync writer
├── Rtg.Listener/            # Worker Service - תלוי ב-WireProtocol + Persistence + DualWrite
├── Rtg.Api/                 # Minimal API + SignalR - תלוי בכל החבילות
└── Rtg.Tests/               # xUnit, מקביל למבנה ה-source
    ├── WireProtocol/
    ├── Persistence/
    ├── Auth/
    └── ... (mirror folder names)
```

- **Tests**: xUnit, קבצים `*Tests.cs`, `[Theory]` + `[InlineData]` ל-parameterized.
- **Integration tests**: trait `[Trait("Category", "Integration")]`, מופרדים ב-CI ל-job נפרד.
- **Test discovery**: tests של רכיב X חיים תחת `Rtg.Tests/X/` לא co-located.

#### 4.3.2 Flutter (cabin + forklift, מבנה זהה)

```
lib/
├── main.dart
├── app.dart                 # MaterialApp + go_router config + Riverpod ProviderScope
├── core/
│   ├── network/             # Dio + interceptors (auth, retry, logging)
│   ├── auth/                # secure storage + JWT refresh logic
│   ├── theme/               # Material 3 + Goldbond palette
│   ├── l10n/                # he.arb + generated strings
│   └── config/              # build flavors, env config
├── features/
│   ├── login/
│   │   ├── data/            # repositories
│   │   ├── domain/          # entities, use-cases
│   │   └── presentation/    # screen + providers + widgets
│   ├── yard_map/
│   ├── job_submit/
│   ├── reports/
│   └── ...
└── widgets/                 # cross-feature reusable widgets
```

#### 4.3.3 Migrations (dbmate)

- מיקום: `db/migrations/`
- Filename: `<YYYYMMDDHHMMSS>_<snake_case_name>.sql`
- כל קובץ מכיל:
  ```sql
  -- migrate:up
  CREATE TABLE rtg.users ( ... );
  -- migrate:down
  DROP TABLE rtg.users;
  ```
- אחת migration לכל logical change (לא לאחד שינויים לא-קשורים).

#### 4.3.4 Configuration

- **.NET**: `appsettings.json` (defaults), `appsettings.Production.json` (overrides), env vars (secrets — JWT keys, AD bind password, MSSQL connection password)
- **Flutter**: `lib/core/config/<env>_config.dart` עם build flavors (dev/staging/prod), API base URL מ-flavor

#### 4.3.5 Localization

- **כל מחרוזת עברית מופיעה ב-catalog file בלבד** — אין hardcoded Hebrew strings ב-code.
- Flutter: `lib/core/l10n/he.arb` → generated via `flutter gen-l10n`
- .NET: `resources/he.json` (אם נדרש server-side messages — error responses, audit log entries)

### 4.4 דפוסי פורמט (Format Patterns)

#### 4.4.1 API responses

| מצב | פורמט |
|---|---|
| Single resource | direct object: `{"id": 123, "status": "pending"}` (לא wrapper) |
| List | paginated: `{"items": [...], "totalCount": N, "page": 1, "pageSize": 50}` |
| Created | 201 + `Location` header + body של ה-resource החדש |
| Update | 200 + body של ה-resource המעודכן (לא 204) |
| No content | 204 (לדוגמה DELETE) |
| Error | RFC 7807 `application/problem+json` |

#### 4.4.2 JSON field naming

- **camelCase** בכל JSON של API ו-events.
- מיפוי: PG `snake_case` → DTO `PascalCase` (.NET) → JSON `camelCase` (System.Text.Json default).

#### 4.4.3 Date/time

| מצב | פורמט |
|---|---|
| API JSON | ISO 8601 UTC עם milliseconds: `"2026-04-26T13:45:00.123Z"` |
| PG | `timestamptz` (TIMESTAMP WITH TIME ZONE) — תמיד UTC ב-DB |
| UI display | locale-aware עם Asia/Jerusalem timezone (Hebrew users) |
| Wire protocol (PLC) | HHMMSS string לפי V40 spec — לא נוגעים ב-format הזה |

#### 4.4.4 IDs

- **Primary keys**: BIGINT auto-increment ל-entities שזורמות ב-volume גבוה (positions, audit log); **UUID v7** ל-entities שמטיילות מעבר ל-DB (jobs, sessions, users) — sortable, מתאים ל-indexing.
- **Foreign keys**: same type as referenced PK.
- **Wire protocol counters** (V40): נשמרים כ-INT/SMALLINT לפי spec, לא הופכים ל-UUID.

#### 4.4.5 ערכים אחרים

- Booleans: `true`/`false` (אסור 0/1, אסור "yes"/"no").
- Nullability: explicit. `null` משמעותי, לא "missing field".
- Empty collections: `[]` ולא `null`.
- Currency / weight: integer ב-smallest unit (אגורות / kg×100) או `decimal(18,4)` — להחליט per-field.

### 4.5 דפוסי תקשורת (Communication Patterns)

#### 4.5.1 In-process (.NET)

- **Dependency Injection** דרך `Microsoft.Extensions.DependencyInjection`.
- **No direct `new`** של services / repositories — תמיד דרך constructor injection.
- **Interfaces** עבור boundaries (`IJobRepository`, `IListener`); concrete classes פנימיים.

#### 4.5.2 Cross-process (Listener → API → Cabin)

- **PG `LISTEN/NOTIFY`** הוא הגשר היחיד בין Listener ל-API. Listener writes → PG NOTIFY → API LISTEN → SignalR push.
- אין direct in-memory queue בין processes.
- אין message broker (RabbitMQ/Kafka) — overkill ל-VM יחיד.

#### 4.5.3 Logging (Serilog)

- **Levels**: `Verbose` (ב-tests בלבד) · `Debug` (dev) · `Information` (prod default) · `Warning` · `Error` · `Fatal`.
- **Structured**: תמיד placeholders — `Log.Information("Crane {CraneId} disconnected after {DurationMs}ms", craneId, durationMs)`. **אסור** `string.Format` או string interpolation ב-log calls.
- **Correlation**: כל HTTP request מקבל `RequestId` (GUID) שמופץ ל-log context. Listener events מקבלים `MessageId` (`crane_id:counter`).
- **PII**: **אסור לחלוטין** ללוגג PIN, password, JWT token, או refresh token. Audit table הוא המקום לאירועי-זהות, לא ה-log.
- **Log destinations**: Serilog → console (JSON) + Seq (Docker container על ה-VM).

### 4.6 דפוסי תהליך (Process Patterns)

#### 4.6.1 Error handling (.NET)

- **Typed exceptions**: `DomainException`, `ValidationException`, `IntegrationException`, `NotFoundException`. הירארכיה ב-`Rtg.Persistence/Exceptions/`.
- **Global error handler** ב-API: middleware שממיר exception → RFC 7807 response. אין catch-all ב-endpoints.
- **Domain code זורק**, application code (services) תופס ומפיל למטה כ-typed exception.
- **Listener catch-all**: catch ב-loop ראשי בלבד; כל exception → log + DLQ + continue (לא להפיל את ה-service על message פגום).

#### 4.6.2 Error handling (Flutter)

- **`Result<T>` pattern** או sealed classes (`Success<T>` / `Failure<T>`).
- **AsyncValue<T>** של Riverpod מטפל ב-loading/data/error consistently.
- **User-facing messages**: עברית בלבד, מ-`he.arb`. אסור להציג raw exception text.
- **Dio interceptor**: 401 → try refresh JWT once → retry; אם refresh נכשל → redirect ל-login.

#### 4.6.3 Retries

- **HTTP / DB (.NET)**: Polly עם exponential backoff + jitter, max 3 attempts. timeout 30s לכל attempt.
- **Flutter HTTP**: Dio retry interceptor, exponential backoff, max 3 attempts.
- **PG outbox retry**: per-row retry counter, max 10 attempts לפני dead-letter; backoff: 1m → 5m → 15m → 1h → 6h → 24h (capped).
- **Listener PLC retransmits**: לפי V40 spec — TOS לא retries אקטיבית; מסתמכת על PLC retransmission.

#### 4.6.4 Validation

- **API boundary**: FluentValidation על כל request DTO. שגיאות → 422 Unprocessable Entity + RFC 7807 + שדה `errors[]`.
- **Flutter UI**: `flutter_form_builder` validators ב-form. server-side **תמיד** re-validates (defense in depth).
- **Wire protocol decode**: validation מובנה ב-`Rtg.WireProtocol` — checksum, length, framing, message type.

#### 4.6.5 Authentication flow (cabin / forklift)

```
1. UI: pick username + enter PIN
2. POST /api/v1/auth/login {loginName, pin, clientType}
3. Server: validate PIN hash + AD active + group membership → issue JWT (15 min) + refreshToken (8h)
4. Client: store tokens in flutter_secure_storage; set Dio Authorization header
5. On 401: try POST /api/v1/auth/refresh {refreshToken}
6. On refresh fail: clear tokens, redirect to login
```

#### 4.6.6 Loading states (Flutter)

- **AsyncValue<T>** של Riverpod is the standard.
- UI חייב לטפל ב-3 states: `loading` (skeleton או spinner), `data` (render), `error` (Hebrew error widget + retry button).
- **Optimistic UI** ל-PICK/PLACE submit: מציג "sent" מיד, חוזר ל-"pending" אם 4xx, מציג error אם 5xx.

### 4.7 הוראות אכיפה (Enforcement Guidelines)

#### חובה לכל AI agent / מפתח שעובד על ה-codebase:

1. **לקרוא את הסעיף הזה לפני כל PR**.
2. **לעקוב אחרי כל הקונבנציות במלואן** — סטייה דורשת עדכון מסמך זה.
3. **`dotnet format` ו-`dart format`** רצים ב-pre-commit hook + CI gate. PR לא יעבור אם format לא תקין.
4. **Linter rules**: .editorconfig + StyleCop ב-.NET; `analysis_options.yaml` + `flutter_lints` ב-Flutter.
5. **API contracts**: כל endpoint דורש OpenAPI annotation. PR שמוסיף endpoint בלי spec — נחסם.
6. **Migrations**: כל schema change חייב migration. Schema drift נחסם ב-CI (השוואה בין migrations המצטברים ל-schema dump).
7. **Tests**: PR שלא מוסיף tests לקוד חדש — נחסם, אלא אם מתועד `[NoTest]` עם הצדקה.

#### יעדי כיסוי-בדיקות (Test Coverage Targets)

| תחום | יעד |
|---|---|
| `Rtg.WireProtocol` | 90% line coverage (ה-codec הוא ליבת הסיכון) |
| `Rtg.Persistence`, `Rtg.Auth`, `Rtg.DualWrite` | 80% |
| `Rtg.Listener`, `Rtg.Api` | 70% |
| Flutter (cabin + forklift) | 60% (UI tests מאתגרים; integration tests עיקר) |

### 4.8 דוגמאות חיוביות ושליליות (Examples)

#### חיובי

```sql
-- migrate:up
CREATE TABLE rtg.users (
    id          UUID PRIMARY KEY DEFAULT uuidv7(),
    login_name  VARCHAR(50) NOT NULL,
    ad_principal VARCHAR(100) NOT NULL,
    pin_hash    VARCHAR(200) NOT NULL,
    is_active   BOOLEAN NOT NULL DEFAULT true,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT now(),
    CONSTRAINT uq_users_login_name UNIQUE (login_name)
);
CREATE INDEX idx_users_ad_principal ON rtg.users (ad_principal);
```

```csharp
public sealed class JobRepository : IJobRepository
{
    private readonly NpgsqlConnection _connection;
    private readonly ILogger<JobRepository> _logger;

    public JobRepository(NpgsqlConnection connection, ILogger<JobRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<Job?> GetByIdAsync(Guid jobId, CancellationToken ct)
    {
        _logger.LogInformation("Fetching job {JobId}", jobId);
        return await _connection.QueryFirstOrDefaultAsync<Job>(
            "SELECT id, crane_id, status, ... FROM rtg.jobs WHERE id = @JobId",
            new { JobId = jobId });
    }
}
```

```dart
final jobsProvider = AsyncNotifierProvider<JobsNotifier, List<Job>>(
  JobsNotifier.new,
);

class JobsNotifier extends AsyncNotifier<List<Job>> {
  @override
  Future<List<Job>> build() async {
    final api = ref.read(apiProvider);
    return api.getJobs(craneId: ref.watch(selectedCraneProvider));
  }
}
```

#### שלילי (אסור)

```csharp
// ❌ string interpolation ב-log
_logger.LogInformation($"Job {jobId} processed in {ms}ms");

// ❌ catch-all ללא typed exceptions
try { ... } catch (Exception ex) { return BadRequest(); }

// ❌ Hebrew strings inline
return Ok(new { message = "המכולה לא נמצאה" });
```

```sql
-- ❌ MixedCase table name
CREATE TABLE rtg.Users ...

-- ❌ no constraints על login_name uniqueness
```

```dart
// ❌ inline Hebrew
Text('שלום ${user.name}')

// ❌ catching Exception in UI
try { ... } catch (e) { print(e); }
```

---

## 5. מבנה הפרויקט וגבולות (Project Structure & Boundaries)

### 5.1 עץ הפרויקט המלא

```
craines-tos/
├── README.md
├── .gitignore
├── .editorconfig
├── .gitattributes
├── LICENSE
├── docker-compose.dev.yml          # PG + Seq + Prometheus + Grafana ל-dev local
│
├── .github/
│   ├── workflows/
│   │   ├── ci-server.yml           # .NET build + test
│   │   ├── ci-flutter-cabin.yml    # cabin: analyze + test + build Windows
│   │   ├── ci-flutter-forklift.yml # forklift: analyze + test + build Windows
│   │   ├── ci-db.yml               # SQL lint + migration validation
│   │   ├── cd-staging.yml          # auto-deploy main → staging VM
│   │   └── cd-production.yml       # tag-triggered deploy → prod VM
│   ├── dependabot.yml
│   └── CODEOWNERS
│
├── server/                         # .NET 10 solution
│   ├── Rtg.sln
│   ├── Directory.Build.props       # shared build settings
│   ├── Directory.Packages.props    # central package version management
│   ├── nuget.config
│   ├── global.json                 # pin .NET SDK 10.x
│   │
│   ├── Rtg.WireProtocol/
│   │   ├── Rtg.WireProtocol.csproj
│   │   ├── V40/
│   │   │   ├── Decoder.cs          # parse ?? + msg type + offsets
│   │   │   ├── Encoder.cs          # build FFFF + ACK + broadcast frames
│   │   │   ├── Framing.cs          # length-prefix framing per V40 spec
│   │   │   ├── Checksum.cs         # additive-sum (V40-compat) + CRC16-CCITT
│   │   │   └── MessageTypes.cs     # A1, A2_03, A2_04, A3 enums
│   │   ├── Models/
│   │   │   ├── A1Position.cs
│   │   │   ├── A2PickDone.cs
│   │   │   ├── A2PlaceDone.cs
│   │   │   ├── A3CancelAck.cs
│   │   │   └── OutboundJob.cs      # B3/B4 broadcast
│   │   └── Exceptions/
│   │       ├── WireProtocolException.cs
│   │       ├── InvalidChecksumException.cs
│   │       └── MalformedFrameException.cs
│   │
│   ├── Rtg.Persistence/
│   │   ├── Rtg.Persistence.csproj
│   │   ├── Connection/
│   │   │   └── NpgsqlConnectionFactory.cs
│   │   ├── Repositories/
│   │   │   ├── IJobRepository.cs / JobRepository.cs
│   │   │   ├── ICranePositionRepository.cs / CranePositionRepository.cs
│   │   │   ├── ILocationRepository.cs / LocationRepository.cs
│   │   │   ├── IShiftingRepository.cs / ShiftingRepository.cs
│   │   │   ├── IUserRepository.cs / UserRepository.cs
│   │   │   ├── IAuditLogRepository.cs / AuditLogRepository.cs
│   │   │   └── IOutboxRepository.cs / OutboxRepository.cs
│   │   ├── Models/                  # entities + value objects
│   │   │   ├── Job.cs, CranePosition.cs, Location.cs, Shifting.cs,
│   │   │   ├── User.cs, AuditLogEntry.cs, OutboxEntry.cs
│   │   ├── Notifications/
│   │   │   ├── INotificationPublisher.cs / PgNotificationPublisher.cs
│   │   │   └── INotificationSubscriber.cs / PgNotificationSubscriber.cs
│   │   └── Exceptions/
│   │       ├── DomainException.cs
│   │       ├── NotFoundException.cs
│   │       ├── ValidationException.cs
│   │       └── IntegrationException.cs
│   │
│   ├── Rtg.Auth/
│   │   ├── Rtg.Auth.csproj
│   │   ├── Pin/
│   │   │   ├── IPinHasher.cs / Argon2idPinHasher.cs
│   │   │   └── PinValidator.cs
│   │   ├── Jwt/
│   │   │   ├── IJwtIssuer.cs / RsaJwtIssuer.cs
│   │   │   ├── IJwtValidator.cs / RsaJwtValidator.cs
│   │   │   └── RefreshTokenService.cs
│   │   ├── AdSync/
│   │   │   ├── LdapClient.cs
│   │   │   ├── AdUserSyncService.cs # cron @ 15min
│   │   │   └── AdConfig.cs
│   │   ├── Lockout/
│   │   │   ├── ILockoutTracker.cs / LockoutTracker.cs
│   │   ├── Audit/
│   │   │   └── AuthAuditPublisher.cs
│   │   └── Models/
│   │       ├── LoginRequest.cs / LoginResponse.cs
│   │       └── RefreshRequest.cs / RefreshResponse.cs
│   │
│   ├── Rtg.DualWrite/
│   │   ├── Rtg.DualWrite.csproj
│   │   ├── Outbox/
│   │   │   ├── IOutboxProcessor.cs / OutboxProcessor.cs
│   │   │   └── OutboxRetryWorker.cs # BackgroundService
│   │   ├── MssqlWriter/
│   │   │   ├── IMssqlWriter.cs / MssqlWriter.cs
│   │   │   └── MssqlConnectionFactory.cs
│   │   └── Reconciliation/
│   │       ├── IReconciliationService.cs / ReconciliationService.cs
│   │       └── ReconciliationCronWorker.cs # @ hourly
│   │
│   ├── Rtg.Listener/
│   │   ├── Rtg.Listener.csproj
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Production.json
│   │   ├── Workers/
│   │   │   ├── CraneListenerWorker.cs # one BackgroundService per crane
│   │   ├── TcpServer/
│   │   │   ├── ITcpServer.cs / PipelinesTcpServer.cs # System.IO.Pipelines-based
│   │   │   └── ConnectionHandler.cs
│   │   ├── Handlers/
│   │   │   ├── A1PositionHandler.cs
│   │   │   ├── A2PickHandler.cs
│   │   │   ├── A2PlaceHandler.cs
│   │   │   ├── A3CancelHandler.cs
│   │   │   └── DeadLetterHandler.cs
│   │   └── Configuration/
│   │       └── ListenerOptions.cs   # ports per crane
│   │
│   ├── Rtg.Api/
│   │   ├── Rtg.Api.csproj
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Production.json
│   │   ├── Endpoints/
│   │   │   ├── AuthEndpoints.cs            # POST /auth/login, /auth/refresh
│   │   │   ├── CranesEndpoints.cs          # GET /cranes/{id}, /cranes/{id}/state
│   │   │   ├── JobsEndpoints.cs            # POST /jobs, GET /jobs, DELETE /jobs/{id}
│   │   │   ├── LocationsEndpoints.cs       # GET /blocks/{id}/cells, PATCH /locations/{id}
│   │   │   ├── ShiftingEndpoints.cs        # GET /shifting (history queries)
│   │   │   ├── ReportsEndpoints.cs         # empty/no-location/expected/diary
│   │   │   ├── WorkOrdersEndpoints.cs      # GET /work-orders
│   │   │   ├── TrucksEndpoints.cs          # GET /trucks (unload/load queues)
│   │   │   ├── ContainersEndpoints.cs      # GET /containers/{id} (composite query)
│   │   │   └── ForkliftEndpoints.cs        # ~80 sp_ForkLift* equivalents
│   │   ├── Hubs/
│   │   │   └── RtgHub.cs                   # SignalR hub עם topic-based subscriptions
│   │   ├── Validators/
│   │   │   ├── LoginRequestValidator.cs
│   │   │   ├── JobSubmitRequestValidator.cs
│   │   │   └── ...                         # FluentValidation לכל request DTO
│   │   ├── Middleware/
│   │   │   ├── ErrorHandlingMiddleware.cs  # exception → RFC 7807
│   │   │   ├── CorrelationIdMiddleware.cs  # X-Request-Id propagation
│   │   │   └── JwtAuthenticationMiddleware.cs
│   │   ├── Models/
│   │   │   ├── Requests/                   # request DTOs
│   │   │   └── Responses/                  # response DTOs
│   │   ├── Hosted/
│   │   │   └── PgListenerHostedService.cs  # מקבל PG NOTIFY, דוחף ל-SignalR
│   │   └── Configuration/
│   │       └── ApiOptions.cs
│   │
│   └── Rtg.Tests/
│       ├── Rtg.Tests.csproj
│       ├── WireProtocol/
│       │   ├── DecoderTests.cs
│       │   ├── EncoderTests.cs
│       │   ├── FramingTests.cs
│       │   ├── ChecksumTests.cs
│       │   └── Fixtures/                   # captured PLC frames מ-chesimu
│       ├── Persistence/
│       │   └── *RepositoryTests.cs         # עם Testcontainers PG
│       ├── Auth/
│       │   ├── PinHasherTests.cs
│       │   ├── JwtTests.cs
│       │   └── AdSyncTests.cs              # mocked LDAP
│       ├── DualWrite/
│       │   ├── OutboxProcessorTests.cs
│       │   └── ReconciliationTests.cs
│       ├── Listener/
│       │   ├── CraneListenerWorkerTests.cs # mock TCP, replay PLC frames
│       │   └── PipelinesTcpServerTests.cs
│       └── Api/
│           ├── AuthEndpointsTests.cs       # WebApplicationFactory
│           ├── JobsEndpointsTests.cs
│           └── ...
│
├── cabin/                          # Flutter Windows desktop app
│   ├── pubspec.yaml
│   ├── analysis_options.yaml
│   ├── windows/                    # Windows runner config
│   ├── lib/
│   │   ├── main.dart
│   │   ├── app.dart                # MaterialApp + go_router + Riverpod
│   │   ├── core/
│   │   │   ├── network/            # Dio + interceptors
│   │   │   ├── auth/               # secure storage + JWT refresh
│   │   │   ├── theme/              # Goldbond palette + Material 3
│   │   │   ├── l10n/he.arb         # Hebrew strings catalog
│   │   │   └── config/             # build flavors
│   │   ├── features/
│   │   │   ├── login/              # FR-2 login עם PIN
│   │   │   │   └── {data,domain,presentation}/
│   │   │   ├── yard_map/           # FR-2 main map (FrmMap01 equivalent)
│   │   │   ├── job_submit/         # PICK/PLACE submit
│   │   │   ├── job_cancel/         # cancel עם hold-to-confirm
│   │   │   ├── manual_location/    # עדכון איתור ידני
│   │   │   ├── truck_mode/         # משאיות (פריקה + טעינה)
│   │   │   ├── work_orders/        # עבודות
│   │   │   └── reports/            # info reports (empty / no-location / etc.)
│   │   └── widgets/
│   │       ├── touch_button.dart   # 60px+ gloves-friendly
│   │       ├── yard_cell.dart      # cell ב-map
│   │       ├── crane_indicator.dart # מיקום-הקראן
│   │       ├── pin_keypad.dart     # 10-digit touch keypad
│   │       └── hebrew_text.dart    # RTL helper
│   ├── test/                       # widget tests
│   ├── integration_test/           # end-to-end
│   └── assets/
│       ├── images/goldbond-logo.png # מ-CurrentSystem/RTG-Template/
│       └── images/crane-photo.png
│
├── forklift/                       # Flutter Windows desktop app
│   ├── pubspec.yaml
│   ├── analysis_options.yaml
│   ├── windows/
│   ├── lib/
│   │   ├── main.dart, app.dart
│   │   ├── core/                   # network, auth, theme, l10n, config — זהה ל-cabin
│   │   ├── features/
│   │   │   ├── login/              # FR-3 username + 7-char + forklift number
│   │   │   ├── information/        # FR-3 frmInformation main
│   │   │   ├── activity/           # FR-3 frmActivity (PICK/PLACE reporting)
│   │   │   ├── info_menu/          # frmInfoMenu
│   │   │   ├── works/              # frmWorks (F4)
│   │   │   ├── recommended_location/
│   │   │   ├── empty_containers/
│   │   │   ├── in_out_diary/
│   │   │   ├── comment/            # frmComment (הוספת הערה)
│   │   │   ├── select_container/   # frmSelectContainer
│   │   │   ├── special_location/
│   │   │   ├── same_deal/          # frmSameDealNumber
│   │   │   ├── messages/           # frmMessage
│   │   │   └── change_forklift_number/
│   │   └── widgets/
│   │       ├── f_key_listener.dart # F2-F12 shortcuts
│   │       ├── numeric_keypad.dart
│   │       ├── damage_button.dart
│   │       └── ...
│   ├── test/, integration_test/
│   └── assets/
│
├── db/
│   ├── dbmate.env.example          # DATABASE_URL pointer
│   ├── migrations/
│   │   ├── 20260427000001_init_schema.sql           # CREATE SCHEMA rtg, mssql_mirror
│   │   ├── 20260427000002_users_and_auth.sql        # rtg.users, rtg.refresh_tokens
│   │   ├── 20260427000003_cranes_and_positions.sql  # rtg.cranes, rtg.crane_positions (partitioned)
│   │   ├── 20260427000004_jobs.sql                  # rtg.jobs, rtg.job_state_history
│   │   ├── 20260427000005_locations.sql             # rtg.locations
│   │   ├── 20260427000006_shifting.sql              # rtg.shifting (partitioned)
│   │   ├── 20260427000007_audit_log.sql             # rtg.audit_log (partitioned)
│   │   ├── 20260427000008_dual_write_outbox.sql
│   │   ├── 20260427000009_notify_triggers.sql       # NOTIFY ב-INSERT/UPDATE על relevant tables
│   │   └── 20260427000010_mssql_mirror_views.sql    # foreign-data wrappers ל-MSSQL
│   ├── seeds/
│   │   └── dev_seed.sql            # פיתוח: cranes, sample users, sample blocks
│   └── scripts/
│       ├── schema_dump.sh          # pg_dump --schema-only ל-CI drift check
│       ├── reset_dev.sh            # local dev — נקה + מיגרציה + seed
│       └── reconcile_drift.sh
│
├── ops/
│   ├── windows-services/
│   │   ├── rtg-listener.xml        # Windows Service manifest (NSSM או built-in)
│   │   ├── rtg-api.xml
│   │   └── rtg-dual-write.xml
│   ├── docker/
│   │   ├── docker-compose.prod.yml # PG + Seq + Prometheus + Grafana על ה-VM
│   │   ├── postgresql.conf         # PG config: LISTEN/NOTIFY tuned, etc.
│   │   └── prometheus.yml
│   ├── monitoring/
│   │   ├── grafana-dashboards/
│   │   │   ├── listener-throughput.json
│   │   │   ├── api-latency.json
│   │   │   ├── dual-write-lag.json
│   │   │   └── auth-failures.json
│   │   └── alert-rules.yml         # Alertmanager rules
│   ├── deployment/
│   │   ├── deploy.ps1              # GitHub Actions self-hosted runner script
│   │   ├── rollback.ps1
│   │   └── healthcheck.ps1
│   └── runbooks/
│       ├── cutover-gold3.md        # Saturday cutover step-by-step
│       ├── rollback.md             # < 30s rollback procedure
│       ├── on-call.md              # alert response playbook
│       └── ad-sync-troubleshooting.md
│
└── docs/
    ├── architecture.md             # symlink או copy של _bmad-output/planning-artifacts/architecture.md
    ├── brownfield/                 # Phase 1 outputs (preserved)
    └── decisions/                  # ADRs מנקודה זו והלאה
        ├── ADR-001-pg-on-ot-subnet.md
        ├── ADR-002-ad-sync-vs-direct.md
        └── ...
```

### 5.2 מיפוי דרישות → מבנה (FR Mapping)

| FR | תחום | מיקום עיקרי |
|---|---|---|
| FR-1 | Wire-protocol listener | `server/Rtg.WireProtocol/V40/`, `server/Rtg.Listener/Workers/`, `server/Rtg.Listener/Handlers/`, `server/Rtg.Listener/TcpServer/` |
| FR-2 | Cabin operator UI | `cabin/lib/features/{login,yard_map,job_submit,job_cancel,manual_location,truck_mode,work_orders,reports}/` |
| FR-3 | Forklift driver UI | `forklift/lib/features/{login,information,activity,info_menu,works,recommended_location,empty_containers,in_out_diary,comment,select_container,special_location,same_deal,messages,change_forklift_number}/` |
| FR-4 | REST + WebSocket API | `server/Rtg.Api/Endpoints/`, `server/Rtg.Api/Hubs/RtgHub.cs`, `server/Rtg.Api/Hosted/PgListenerHostedService.cs` |
| FR-5 | Persistence layer | `server/Rtg.Persistence/`, `db/migrations/`, `db/seeds/`, PG NOTIFY triggers ב-`db/migrations/20260427000009_notify_triggers.sql` |
| FR-6 | Auth & identity | `server/Rtg.Auth/`, `server/Rtg.Api/Endpoints/AuthEndpoints.cs`, `server/Rtg.Api/Middleware/JwtAuthenticationMiddleware.cs` |
| FR-7 | Process supervision | `ops/windows-services/`, `ops/runbooks/cutover-gold3.md`, `ops/deployment/healthcheck.ps1` |
| FR-8 | Migration / cutover | `server/Rtg.DualWrite/`, `ops/runbooks/cutover-gold3.md`, `ops/runbooks/rollback.md`, `db/migrations/20260427000010_mssql_mirror_views.sql` |

### 5.3 דאגות חוצות → מיקום (Cross-Cutting Concerns Map)

| דאגה | מיקום |
|---|---|
| Idempotency keys | `Rtg.WireProtocol/V40/Decoder.cs` (extract `crane_id:counter:msg_type`); `Rtg.Persistence/Models/Job.cs` (UUID v7); `Rtg.DualWrite/Outbox/OutboxProcessor.cs` (replay-safe) |
| Audit trail | `Rtg.Auth/Audit/AuthAuditPublisher.cs`; `Rtg.Persistence/Repositories/AuditLogRepository.cs`; PG `rtg.audit_log` (partitioned) |
| Migration consistency | `Rtg.DualWrite/Outbox/`, `Rtg.DualWrite/Reconciliation/`, hourly `ReconciliationCronWorker` |
| Observability | `Rtg.*/Program.cs` Serilog setup → Seq; `ops/monitoring/grafana-dashboards/`; `ops/monitoring/alert-rules.yml` |
| Security/transport | `Rtg.Api/Middleware/JwtAuthenticationMiddleware.cs`; firewall config ב-`ops/runbooks/cutover-gold3.md`; TLS ב-IIS reverse proxy |
| RTL & touch UX | `cabin/lib/widgets/`, `forklift/lib/widgets/`; `lib/core/theme/` ב-שני ה-apps |
| Hebrew localization | `cabin/lib/core/l10n/he.arb`; `forklift/lib/core/l10n/he.arb`; server-side messages ב-`server/Rtg.Api/resources/he.json` |

### 5.4 גבולות ארכיטקטוניים (Architectural Boundaries)

#### 5.4.1 גבולות API (External-facing)

| Boundary | Type | Endpoint Range | Auth |
|---|---|---|---|
| `/api/v1/auth/*` | REST | login, refresh, logout | חלקית (login = anonymous; refresh+logout = JWT) |
| `/api/v1/cranes/*` | REST | crane state, position queries | JWT required |
| `/api/v1/jobs/*` | REST | submit, list, cancel | JWT required + role check |
| `/api/v1/locations/*` | REST | yard map, manual edit | JWT required + role check |
| `/api/v1/reports/*` | REST | empty/no-location/diary | JWT required |
| `/api/v1/forklift/*` | REST | ~80 sp_ForkLift* equivalents | JWT required + forklift role |
| `/hub/rtg` | WebSocket (SignalR) | topic-based subscriptions | JWT in Authorization header on connect |
| TCP `:30701-30703` | Wire (V40) | per-crane listener | אין auth — firewall whitelist + OT subnet |

#### 5.4.2 גבולות רכיבים (Internal — process boundaries)

```
┌─────────────────────────────────────────────┐
│            VM (192.6.8.x)                   │
│                                             │
│  ┌──────────────┐    ┌──────────────────┐  │
│  │ Rtg.Listener │───▶│   PostgreSQL     │  │
│  │ (Win Svc)    │    │   (Docker)       │  │
│  └──────────────┘    │                  │  │
│         │             │ rtg.* schema     │  │
│         │ via         │ + LISTEN/NOTIFY  │  │
│         │ Persistence │                  │  │
│         ▼             └────────┬─────────┘  │
│  ┌──────────────┐              │            │
│  │ Rtg.DualWrite│              │ NOTIFY     │
│  │ (Win Svc)    │              ▼            │
│  └──────┬───────┘    ┌──────────────────┐  │
│         │             │   Rtg.Api        │  │
│         │ MSSQL       │   (Win Svc)      │  │
│         │ writes      │   - Endpoints    │  │
│         ▼             │   - SignalR Hub  │  │
│  ┌──────────────┐    │   - LISTEN       │  │
│  │ MSSQL        │    └────────┬─────────┘  │
│  │ (.52, ext.)  │              │            │
│  └──────────────┘              │ WebSocket  │
│                                │ + REST     │
└────────────────────────────────┼────────────┘
                                 ▼
                        ┌─────────────────┐
                        │  Cabin / Forklift│
                        │  Flutter Apps    │
                        └─────────────────┘
```

#### 5.4.3 גבולות Data

| Boundary | Type | Notes |
|---|---|---|
| `rtg.*` (PG) | Internal — owned by new system | Source of truth |
| `mssql_mirror.*` (PG views via FDW) | Read-only | Foreign Data Wrapper to legacy MSSQL during transition; for read-only consumers |
| `TerminalData` (MSSQL) | External — shared with MIS, AuditWeb, WMS, legacy ForkliftApp | Dual-write destination during transition; eventually decommissioned |
| `AuditWeb` (MSSQL) | External — Web consumer | Mirror of `rtg.audit_log` writes via DualWrite |
| AD (LDAP) | External — Goldbond AD | Read-only via LDAP sync cron |

### 5.5 זרימות נתונים (Data Flow)

#### זרימת PICK (cabin → crane → DB → cabin)

```
1. Cabin: POST /api/v1/jobs   {craneId, fromLocation, toLocation}
2. API: validate → INSERT rtg.jobs → NOTIFY rtg.gold3.job
3. Listener (LISTEN): receive notify → SELECT new job → encode B3 → TCP write to PLC
4. PLC: physical lift → A2 03 frame back
5. Listener: parse A2 03 → UPDATE rtg.jobs.pick_date → NOTIFY rtg.gold3.job_state
6. API (LISTEN): SignalR push to cabin client subscribed to GOLD3
7. Cabin: AsyncValue<JobState> updates → UI shows "picked"
8. DualWrite: outbox row → MSSQL UPDATE RG_B3 (async, eventual)
```

#### זרימת login (cabin → API → AD-sync mirror → JWT)

```
1. Cabin: POST /api/v1/auth/login   {loginName, pin}
2. API: validate FluentValidation → AuthService:
   - SELECT pin_hash FROM rtg.users WHERE login_name=? AND is_active=true
   - Argon2id.Verify(pin, pin_hash)
   - Check lockout state
3. If ok: issue access JWT (15min) + refresh token (8h) → INSERT rtg.refresh_tokens
4. If fail: increment lockout counter; return 401 + RFC 7807
5. Cabin: store tokens in flutter_secure_storage; navigate to yard_map
```

### 5.6 פיתוח ו-build flow

#### Local development
1. `docker compose -f docker-compose.dev.yml up -d` — PG + Seq + Prometheus + Grafana
2. `dbmate up` — apply migrations
3. `cd server && dotnet build && dotnet run --project Rtg.Listener` (or attach debugger)
4. `cd server && dotnet run --project Rtg.Api`
5. `cd cabin && flutter run -d windows`
6. `cd forklift && flutter run -d windows`
7. Run chesimu (vendor simulator) pointed at `localhost:30703` for GOLD3 testing

#### CI (GitHub Actions)
- PR → `ci-server.yml` runs `dotnet build && dotnet test --filter Category!=Integration` (unit) + integration tests with Testcontainers PG
- PR → `ci-flutter-cabin.yml` and `ci-flutter-forklift.yml` run `flutter analyze && flutter test && flutter build windows`
- PR → `ci-db.yml` runs SQL lint + migration replay test
- All must pass before merge

#### Deployment (CD)
- main push → `cd-staging.yml`: build artifacts → upload via self-hosted runner on staging VM → `dbmate up` → restart Windows Services
- tag `v*.*.*` push → `cd-production.yml`: same flow on prod VM, with manual approval gate

### 5.7 Build outputs

| Component | Artifact | Location |
|---|---|---|
| `Rtg.Listener` | `Rtg.Listener.exe` + `appsettings.Production.json` | `C:\Rtg\Listener\` (on VM) |
| `Rtg.Api` | `Rtg.Api.exe` + `appsettings.Production.json` | `C:\Rtg\Api\` |
| `Rtg.DualWrite` | `Rtg.DualWrite.exe` + `appsettings.Production.json` | `C:\Rtg\DualWrite\` |
| Cabin app | `cabin.exe` + `data/` + `flutter_windows.dll` | per-cabin Windows PC, distributed via self-updater |
| Forklift app | `forklift.exe` + `data/` + `flutter_windows.dll` | per-forklift tablet, self-updater |
| PG | Docker container `postgres:18.3-alpine` | on VM, data volume backed up nightly |
| Seq | Docker container `datalust/seq` | on VM |

---

## 6. אימות הארכיטקטורה ושלמות (Architecture Validation & Completion)

### 6.1 אימות לכידות (Coherence Validation)

**תאימות החלטות:** ✅ אין סתירות בין החלטות sections 1-5. Tech versions תואמות (`.NET 10 + PG 18.3 + Flutter 3.41.5`); patterns תואמים stack; structure תומך patterns.

**עקביות patterns:** ✅ קונבנציות שמות, פורמטים, ולוג עקביים בכל הרכיבים.

**יישור מבנה:** ✅ עץ הפרויקט (§5.1) מממש את כל ההחלטות (§3) ועקבי לקונבנציות (§4).

### 6.2 כיסוי דרישות (Requirements Coverage)

| Req | Status | מקום |
|---|---|---|
| FR-1 Listener | ✅ | §3.4 + Rtg.WireProtocol + Rtg.Listener |
| FR-2 Cabin UI | ✅ | §3.5 + cabin/lib/features/* |
| FR-3 Forklift UI | ✅ | §3.5 + forklift/lib/features/* |
| FR-4 REST + WebSocket API | ✅ | §3.4 + Rtg.Api/Endpoints + Rtg.Api/Hubs |
| FR-5 Persistence + pub/sub | ✅ | §3.2 + db/migrations + LISTEN/NOTIFY |
| FR-6 Auth + AD | ✅ | §3.3 + Rtg.Auth + AD-2 |
| FR-7 Supervision | ✅ | §3.6 + ops/windows-services |
| FR-8 Migration / cutover | ✅ | §3.2 D-DA-6 + Rtg.DualWrite + ops/runbooks |
| NFR-1 Security | ✅ (לאחר Gap #1) | TLS = IIS reverse proxy |
| NFR-2 Real-time UX | ✅ | §3.4 D-AC-5 |
| NFR-3 Data integrity | ✅ (לאחר Gap #2) | PLACE handler = single PG transaction |
| NFR-4 Reliability | ✅ (לאחר Gap #3) | Listener drain עם 30s timeout |
| NFR-5 Observability | ✅ | §3.6 |
| NFR-6 Migration safety | ✅ | DualWrite + reconciliation + 30s rollback |
| NFR-7 Performance | ✅ | §3.6 — VM sized מעל baseline |
| NFR-8 Compliance / audit | ✅ | rtg.audit_log + AD principal binding |
| NFR-9 i18n RTL | ✅ | §4.3.5 he.arb |
| NFR-10 Hardware compat | ✅ | §3.6 — Windows Server 2022 |

### 6.3 מוכנות יישום (Implementation Readiness)

**שלמות החלטות:** ✅ כל ההחלטות הקריטיות מתועדות עם versions ו-rationale (§3).

**שלמות מבנה:** ✅ עץ הפרויקט מלא, כל ה-folders ו-key files מוגדרים (§5).

**שלמות patterns:** ✅ קונבנציות ב-6 קטגוריות (naming, structure, format, communication, process, enforcement) — §4.

**יכולת CI לאכוף:** ✅ `dotnet format` + `dart format` + linters + OpenAPI gate + migration drift check + test coverage gates.

### 6.4 ניתוח פערים והחלטות נספחות (Gap Analysis — Validation 2026-04-26)

#### פערים שנסגרו במהלך ה-validation (אישור Yaniv 2026-04-26)

| # | Gap | רזולוציה | סיווג |
|---:|---|---|---|
| 1 | TLS terminator לא מוגדר ספציפית | **IIS** על Windows Server 2022 — boring tech, IT familiarity | NFR-1 |
| 2 | PLACE handler transaction boundary | **Single PG transaction** עוטף את כל ה-writes; אם נכשל — rollback, log, NAK; outbox ל-MSSQL רק לאחר PG commit | NFR-3 |
| 3 | Listener graceful shutdown | **Drain mode עם 30s timeout** — מפסיק לקבל TCP חדשים, מסיים processing של frames נקראים, מנקה outbox; אם לא הסתיים תוך 30s — hard abort | NFR-4 |
| 4 | Self-updater endpoint design | `GET /api/v1/clients/{cabin\|forklift}/version` → `{latest, minRequired, downloadUrl}`. Apps בודקים startup + hourly. אם `current < minRequired` — force update | FR-2/3 supplement |
| 5 | Secrets-on-VM lifecycle | IT יוצר `C:\Rtg\secrets\<service>.env` ב-provisioning (mode = service account only). Runbook ב-`ops/runbooks/secret-rotation.md`. JWT key: rotate annually; AD bind password: לפי AD policy | NFR-1 supplement |
| 6 | PG `mssql_mirror.*` setup | `postgres_fdw` extension. Foreign server + user mapping ב-migration `20260427000010_mssql_mirror_views.sql` (creds מ-env vars, לא בקובץ). Read-only views | FR-8 supplement |
| 7 | NTP time sync | VM חייב sync ל-Goldbond NTP pre-cutover (runbook). PG + services ב-UTC; cabin/forklift מציגים server-supplied timestamps | NFR-3 supplement |
| 8 | PG `wal_level` config | `wal_level = replica` מהיום הראשון (ב-`ops/docker/postgresql.conf`). מאפשר streaming replication עתידי בלי restart | NFR-4 supplement |

#### פערים שנדחו (post-pilot)

- OpenTelemetry distributed tracing
- Sentry / external error tracking
- Locust/k6 load testing harness
- PG read-replica לדוחות

### 6.5 Checklist שלמות הארכיטקטורה

#### ✅ ניתוח דרישות (Section 1)
- [x] קונטקסט הפרויקט נותח לעומק
- [x] היקף וסיבוכיות הוערכו
- [x] אילוצים טכניים זוהו
- [x] דאגות חוצות מומפו

#### ✅ Tech Stack (Section 2)
- [x] טכנולוגיות נבחרו עם versions verified web
- [x] אלטרנטיבות נשקלו ונדחו עם rationale
- [x] starter commands מתועדים

#### ✅ החלטות אדריכליות (Section 3)
- [x] החלטות קריטיות מתועדות עם versions
- [x] tech stack ממומש במלואו
- [x] integration patterns מוגדרים
- [x] performance / NFRs מטופלים

#### ✅ Implementation Patterns (Section 4)
- [x] naming conventions לכל שכבה
- [x] structure patterns מוגדרים
- [x] communication patterns specified
- [x] process patterns (errors, retries, validation, auth, loading)
- [x] דוגמאות חיוביות ושליליות

#### ✅ Project Structure (Section 5)
- [x] עץ ספריות מלא
- [x] גבולות רכיבים מוגדרים
- [x] integration points מומפים
- [x] FR → structure mapping מלא

#### ✅ Validation (Section 6)
- [x] coherence validated
- [x] requirements coverage validated (FR-1..8 + NFR-1..10)
- [x] implementation readiness confirmed
- [x] gap analysis completed (8 פערים — כולם נסגרו או נדחו במודע)

### 6.6 הערכת מוכנות הארכיטקטורה

**Status:** ✅ **READY FOR IMPLEMENTATION**

**Confidence:** **High** — הארכיטקטורה מבוססת על discovery brownfield יסודי, vendor spec מאומת, ועל boring tech שה-team מכיר. אין fundamental open questions שחוסמים יישום.

**נקודות חוזק:**

1. **Brownfield-grounded** — Mary's Phase 1 supplied a tighter requirements baseline than most PRDs.
2. **Boring tech stack** — .NET 10 + Flutter + PG הם stack שה-team מכיר; learning curve מינימלית.
3. **Security-by-design** — אין `sa`, אין credentials מובנים, AD-2 משלב ניהול-זהויות עם UX-מתאים-לכפפות.
4. **Modular cutover path** — GOLD3 first, idempotent, < 30s rollback.
5. **Dual-write pattern מוכר** — outbox + reconciliation מפחית סיכון integration.
6. **Pub/sub ראוי** — PG LISTEN/NOTIFY → SignalR מוחק את עומס ה-polling-500ms הקיים.

**שיפורים עתידיים (post-pilot):**

1. PG streaming replication ל-HA לאחר scale-out לקראנים 1+2
2. OpenTelemetry tracing distributed
3. Read-replica לדוחות
4. Sentry / external error tracking
5. Load testing harness אוטומטי (Locust/k6)

### 6.7 Implementation Handoff

**הוראות ל-AI agents שיממשו:**

1. **לפני כל עבודה — קרא את כל מסמך זה.** אין מקצרים.
2. **לעקוב אחרי כל החלטה ב-§3 ובכל pattern ב-§4** — סטיות דורשות עדכון מסמך זה.
3. **לכבד את גבולות הרכיבים ב-§5.4** — אין cross-talk בין רכיבים מחוץ למוגדרים.
4. **CI הוא binding** — `dotnet format`, `dart format`, OpenAPI annotations, migrations, tests — כל אלה חוסמים PR אם לא תקינים.
5. **כל סטייה ארכיטקטונית דורשת ADR** ב-`docs/decisions/ADR-NNN-<slug>.md` עם rationale, alternatives considered, ו-update למסמך זה.

**Story #1 של ה-implementation:**

```bash
# Repository skeleton — ביצוע ה-init commands מ-§2.5
mkdir craines-tos && cd craines-tos
git init
git remote add origin git@github.com:goldbond/craines-tos.git
# ... continue with init commands from §2.5
git add . && git commit -m "chore: repo skeleton + .NET 10 + Flutter + dbmate init"
git push -u origin main
```

ה-story הראשון לא דורש לוגיקה — רק את ה-skeleton structure שאחריו ה-stories הספציפיות יבנו.

**רצף epics מומלץ (יורט ל-bmad-create-epics-and-stories):**

1. Epic-01: Foundation (skeleton + CI + dev-env)
2. Epic-02: Wire Protocol (Rtg.WireProtocol + tests)
3. Epic-03: PG Schema + Persistence (db migrations + Rtg.Persistence + LISTEN/NOTIFY)
4. Epic-04: Listener (Rtg.Listener + chesimu integration tests)
5. Epic-05: Auth + AD Sync (Rtg.Auth + AD-2)
6. Epic-06: API + SignalR (Rtg.Api + RtgHub)
7. Epic-07: DualWrite (Rtg.DualWrite + reconciliation)
8. Epic-08: Cabin UI (cabin/* — 7 features)
9. Epic-09: Forklift UI (forklift/* — 14 features)
10. Epic-10: Observability (Serilog→Seq + Prometheus + Grafana + alerts)
11. Epic-11: Pilot Deployment (VM provisioning + GitHub Actions runner + dry-run)
12. Epic-12: GOLD3 Cutover (Saturday window + 1-4 weeks reconciliation)
13. Epic-13: GOLD1+GOLD2 Rollout (separate Saturday windows)

---
