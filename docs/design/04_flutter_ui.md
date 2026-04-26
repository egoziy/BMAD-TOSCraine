# 04 — Flutter Operator UI (`rtg-cab`)

> **Phase 2 — Step 2.4** · Designer: Claude Code · Date: 2026-04-21
> **Scope:** replacement for the two WinForms applications `RTGApp.exe` (RTG1-2) and `RTGApp.exe` (RTG3). One Flutter codebase, per-crane config, Hebrew-RTL, offline-tolerant, large-touch-target. Based on `06_operator_ui.md`.

---

## 0. CABIN UX PRINCIPLES — non-negotiable

> **Context:** operators work inside an RTG crane cabin managing real-world container movements in a busy port. Glove-thick fingers, sun-glare-visible monitor, one-hand operation, divided attention (watching the yard + the load + the UI). **Mistakes cost containers, cargo, and sometimes people.** Every design decision for this app must route through these rules before anything else.

These rules came from a 2026-04-23 walkthrough: "the people that operate this are working on a crane that operate big containers in a busy environment so the UI/UX should be simple and self-explained so they can operate it quick and safe. the request to see a tiny arrow and then to click it to open a new small drop down is very dangerous and problematic for them."

### Rules

1. **Critical info is always visible.** No "tap to see details". The current container id, weight, driver, destination, and status must be on screen without the operator interacting. If the detail requires fetching, show placeholder dashes — never a blank or tiny hint.
2. **No tiny interactive elements.** Every tappable target ≥ 72×72 logical px. Every on-screen text ≥ 18pt. No iconography-only buttons. No chevron-to-expand.
3. **Icons always carry text labels.** Glyph-only UI fails in glove / glare / glance conditions. `✓` is fine next to `שלח`, never alone.
4. **Flat, one-tap flows.** No drill-downs, no nested dropdowns, no modal→modal chains. One tap of a cell should give the operator everything they need; one more tap should commit the action.
5. **Self-explanatory labels.** Hebrew, full words, no abbreviations, no three-letter codes outside their column header. `OT`, `HC`, `RG` are fine in a known-columns grid but never as standalone button text.
6. **Destructive actions require confirmation.** Cancel a job, remove a yard slot, logout — all require a hold-to-confirm (≥2s press) or an explicit second-tap-within-3s. Never a single tap that can drop a container in the wrong place.
7. **Fast visual feedback (<200 ms).** Every tap gets immediate colour / size / haptic response. Lag breeds re-taps, re-taps breed wrong state.
8. **No modal dialogs mid-workflow.** The yard map must never be obscured. Alerts go to the status strip or a non-blocking toast.
9. **Errors speak operator Hebrew, not code.** Never `400 bad_request` — `קוד מכולה לא תקין`.
10. **Assume a 3-second glance.** If the essential answer isn't readable in a 3-second glance from a meter away, the layout is wrong.

### Direct implications for screens currently in flight

- **Container detail bar (P0-1)** — render **always** when a FROM cell is selected, with placeholder dashes for fields that are still loading or not in `CO_Containers`. Don't hide it behind any sub-interaction.
- **Pending-lift indicator** — a tiny corner `↑` fails rule 2 + rule 5. Replace with a coloured cell border (already present) plus a full-width label band above the map: `עבודה פתוחה: 100A · OERU4208202 (25 tonnes) - אספוף` so the operator never has to tap to discover what's pending.
- **Selection chips** — don't rely on the chip colour alone to convey state. Add inline text ("נבחר כמקור" / "נבחר כיעד") with icons + colour + words.
- **Cancel-job (P0-2)** — required to be a hold-to-confirm; never a single-tap red X.
- **Truck / work-orders / info reports (P0-3, P0-4, P1-7)** — rows in those tables must be finger-sized (≥ 72 px tall), not default DataTable rows.
- **Operator dropdown at login** — the current implementation is correct in spirit (dropdown of names, not typed input) but items must be big with a hitbox filling the full row width.

### Anti-patterns to refuse

- Any "i" / info-button → modal.
- Tooltips as primary information carriers (operators with gloves can't hover).
- Any drag-to-confirm gesture.
- Any feature that requires zooming.
- Any text smaller than the surrounding labels — especially in the status strip.

---

## 1. Target platforms

| Platform | Priority | Hardware | Why |
|---|---|---|---|
| **Windows desktop (x64)** | **Primary** | Existing cabin PCs (small-form-factor desktops + touchscreen monitor, or rugged Windows tablets) | Zero new hardware; aligns with the existing operator workflow; the team is Windows-ops |
| **Android tablet** | Secondary | Optional future: 10" rugged tablet (Samsung Galaxy Tab Active / Getac T800 / etc.) | Future-proofs for true in-cab tablet; lets us prove the form factor before procurement |
| **Web (Chrome)** | Emergency fallback only | Browsers in yard supervisor offices | NOT a primary target — used only for supervisor monitoring |

Flutter gives us all three from one codebase (Flutter 3.19+, Dart 3.3+). Android + Windows are the deliverables. Linux/macOS desktop are not supported (and not needed).

---

## 2. Screen inventory — 1:1 mapping from legacy

All 13 legacy WinForms are accounted for. Some merge, some split, none drop.

| # | Legacy screen | New screen / route | Simplification |
|---|---|---|---|
| 1 | `FrmLogin` | `/login` | Same shape; fix auth (PIN-bound-to-user, lockout) |
| 2 | `FrmMap01` (the main map + live status + job submit, 4000 LOC) | `/yard` (main) — but **split into 3 focused panels** | Biggest simplification; see §3 |
| 3 | `FrmMenu` | Side drawer / hamburger menu on `/yard` | No longer a separate screen |
| 4 | `ContainerLocation` (update a container's yard location) | `/containers/:container/edit-location` | — |
| 5 | `frmInformation` (container details dialog) | bottom-sheet on `/yard` when tapping a cell | — |
| 6 | `FrmWorks` (outstanding work orders) | `/work-orders` | — |
| 7 | `FrmContainersByBloc` (containers in a given block) | `/containers?bloc=BOND1` | filter param on containers list |
| 8 | `FrmContainerNoLocation` (containers missing a LocationCode) | `/containers?filter=no-location` | — |
| 9 | `FrmEmptyContainers` (list of empty containers) | `/containers?filter=empty` | — |
| 10 | `FrmExpectedContainers` (expected to arrive/unload) | `/containers?filter=expected` | — |
| 11 | `FrmInOutDiory` (in/out diary / 7-day report) | `/reports/daily-movement` | fix "Diory" → "Diary" (file name) |
| 12 | `FrmRecommendedLocation` (edit recommended loc. per client) | `/admin/recommended-location` | supervisor/admin role only |
| 13 | `Works.FrmWorks` — already covered by #6 | | |

### 2.1 Route structure

```
/login
/yard                                  <— post-login home; map + status + submit-job
/yard/container/:container             <— bottom-sheet / drawer (non-destructive to map)
/containers                             <— filterable list
/containers/:container/edit-location   <— manual override
/work-orders
/reports/daily-movement
/admin/recommended-location             <— supervisor+
/admin/operators                        <— supervisor+
/admin/audit                            <— admin
/settings                               <— theme, font size, logout
```

### 2.2 Role-gated screens

- `crane_operator`: `/yard`, `/containers`, `/work-orders`, `/reports/daily-movement`, `/settings`.
- `yard_supervisor`: everything above + `/admin/recommended-location`, `/admin/operators`.
- `admin`: everything + `/admin/audit`.

---

## 3. The `/yard` screen — design philosophy

This is the screen the operator spends the shift on. Three principles:

1. **Glanceable live status.** Crane position + connection state at the top, visible from across the cab.
2. **Direct-manipulation map.** Tap a cell to select a source or destination. No typing if avoidable. Minimal numeric keypad for container IDs when the camera / scanner doesn't help.
3. **One big action button.** "שלח" (Send job). Large, high-contrast, bottom-centre. **One hand, eyes on yard.**

### 3.1 Layout

```
┌──────────────────────────────────────────────────────────────────────────┐
│ ┌─ STATUS STRIP ──────────────────────────────────────────────────┐  ☰  │
│ │ ● GOLD1 ONLINE · BAY 128 A / 2 · GPS OK · Twist ✓ · 14:30:22    │      │
│ └─────────────────────────────────────────────────────────────────┘      │
│                                                                          │
│ ┌─ YARD MAP (BOND1) ──────────────────────────────────────────────────┐  │
│ │       100 102 104 106 108 110 ...  130 132                          │  │
│ │   A [ ][3][X][ ][1][X]...             <-- colored cells: empty/full │  │
│ │   B [X][X][ ][ ][2][X]...                                           │  │
│ │   C [ ][X][X][1][X][ ]...                                           │  │
│ │   D [ ][ ][X][ ][ ][X]...                                           │  │
│ │   E ...                                                             │  │
│ │   G [ ][ ][X]                                                       │  │
│ │   T [X]                                                             │  │
│ │                                                                     │  │
│ │  [selected]  FROM: 106 B 1  →  TO: 128 A 2                          │  │
│ └─────────────────────────────────────────────────────────────────────┘  │
│                                                                          │
│ ┌─ SELECTION DETAIL ─────────────────────────────────────────────────┐   │
│ │ מכולה: TCLU 1234567   גודל: 40 סוג: GP  משקל: 24T  נהג: 123-45      │   │
│ │ יעד: 128 A 2 (בלוק BOND1)                                           │   │
│ │                                                                    │   │
│ │        [  שלח  ]          [  בטל  ]                                │   │
│ └────────────────────────────────────────────────────────────────────┘   │
│                                                                          │
│ ┌─ JOB QUEUE (expandable; collapsed by default) ─┐                       │
│ │ 3 ממתינות לביצוע   ▼                          │                       │
│ └────────────────────────────────────────────────┘                       │
└──────────────────────────────────────────────────────────────────────────┘
```

Hebrew labels are RTL-aligned; layout flips for RTL automatically (Flutter's `Directionality` widget handles the whole tree).

### 3.2 Interaction model

- **Tap a filled cell** → populate "FROM" + show container details in bottom sheet.
- **Tap an empty cell** → populate "TO".
- **שלח** (Send) → confirm dialog → POST `/api/jobs` → server returns job id → UI shows status "ממתין לאישור מנוף..." → live update via SignalR.
- **Long-press a cell** → admin override (container-location edit, role-gated).

### 3.3 Status strip colors

- **Green** = `crane_status.connection_state = CONNECTED` and last packet < 5s ago.
- **Amber** = last packet 5-30s (possibly stale).
- **Red** = > 30s, or `DISCONNECTED`. Also triggers a "Reconnect" button (replaces legacy `EnconsoleButtom`).

---

## 4. RTL & Hebrew-first

### 4.1 Principles

1. **App-wide `Directionality: TextDirection.rtl`.** All widgets inherit.
2. **Icons chosen to look the same in either direction** (avoid directional arrows; use Material-symbolic icons that have RTL variants).
3. **Numbers** stay LTR inside RTL text (Flutter handles this via Unicode BiDi algorithm — tested).
4. **Dates, times** formatted via `intl` package with Hebrew locale (`he_IL`). Day names, month names auto-localize.
5. **Strings** live in `l10n/app_he.arb` — never baked into code. A future English / Arabic / Russian translation is adding an `.arb` file.

### 4.2 Hebrew string table (first pass excerpt)

```arb
{
  "@@locale": "he_IL",
  "loginTitle": "כניסת מפעיל",
  "loginUsername": "שם משתמש",
  "loginPin": "קוד זיהוי",
  "loginSubmit": "כניסה",
  "loginExit": "יציאה",
  "loginErrorInvalid": "סיסמה שגויה",
  "loginErrorLocked": "נעילה — נסה בעוד {minutes} דקות",
  "yardTitle": "מפת חצר",
  "yardStatusOnline": "מקוון",
  "yardStatusOffline": "לא מקוון",
  "yardStatusStale": "נתונים לא עדכניים",
  "yardActionSend": "שלח",
  "yardActionCancel": "בטל",
  "yardActionReconnect": "חבר מחדש",
  "containerDetails": "פרטי מכולה",
  "containerLength": "גודל",
  "containerType": "סוג",
  "containerWeight": "משקל",
  "containerDriver": "נהג",
  "containerPosition": "מיקום",
  "jobSubmitted": "המשימה נשלחה למנוף",
  "jobAcknowledged": "המנוף אישר",
  "jobPicked": "המכולה הורמה",
  "jobPlaced": "המכולה הונחה",
  "jobCancelled": "בוטל",
  "menuMap": "מפה",
  "menuContainers": "מכולות",
  "menuWorkOrders": "עבודות",
  "menuReports": "דוחות",
  "menuSettings": "הגדרות",
  "menuLogout": "יציאה"
}
```

---

## 5. Offline-first behavior

### 5.1 Local cache

`rtg-cab` uses `drift` (SQLite wrapper) for local storage. Mirrors a read-only snapshot of:
- `rtg.crane_status` for this cabin's crane.
- Yard map cells (block scoped).
- Last 50 container detail lookups (LRU).
- Operator session info (so refresh doesn't force re-login while token valid).

### 5.2 Write queue

Outbound writes (submit job, cancel job, edit location) go through a `local_write_queue` table:

```sql
CREATE TABLE local_write_queue (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    created_at INTEGER NOT NULL,     -- epoch ms
    endpoint TEXT NOT NULL,          -- '/api/jobs' etc.
    method TEXT NOT NULL,            -- 'POST', 'PATCH'
    body TEXT NOT NULL,              -- JSON
    attempts INTEGER DEFAULT 0,
    last_attempt INTEGER,
    status TEXT NOT NULL,            -- 'pending', 'sent', 'failed'
    server_response TEXT,
    idempotency_key TEXT UNIQUE NOT NULL
);
```

When online: drain in FIFO order, call API with `Idempotency-Key` header, mark sent.
When offline: items accumulate; UI shows a banner `"ממתינים לשליחה: N פעולות"`.
On app start: drain the queue before subscribing to live updates, so the operator's recent actions don't fight the server's state.

### 5.3 Conflict resolution

- **Server wins** for read state (crane_status, map, container details).
- **Client write wins** at the moment of submission, but the server may reject with a specific reason (e.g. "this bay is full now"). Display the server's Hebrew reason, keep the form open.

### 5.4 Degraded states

| State | What the UI shows |
|---|---|
| API down | amber banner `"שרת לא זמין — עובד במצב לא-מקוון"`; write queue accepts submissions |
| Crane offline | red status strip; `"המנוף לא מקוון — משימות ימתינו"`; submit button disabled |
| PG down (rare — apparent via API 500s) | amber banner; read shows last-known state |

---

## 6. Large-touch-target design

### 6.1 Minimum tap target sizes

- **48×48 logical pixels** is the Flutter / Material default; we go **72×72** for primary action buttons (שלח / בטל).
- **Yard cell** buttons: **44×44** minimum so fingers (even with gloves) can hit them cleanly. Grid dynamically resizes to fill the screen — on a 12" tablet that's ~50px cells, on a 24" kiosk monitor that's ~70px.
- **On-screen keypad keys** for PIN / container number: **96×96**.
- **Form fields** (where typed input is unavoidable): min height 64px; 24pt font.

### 6.2 Typography

- Body text: 20pt (not 14 — this is not a laptop).
- Strip status: 24pt bold.
- Alerts / errors: 28pt with icon + color.
- Font: `Heebo` (Hebrew-friendly Open-source, Google Fonts) — bundled, not downloaded at runtime.

### 6.3 Color system (high-contrast, glove-friendly)

- **Backgrounds**: off-white (`#F6F6F4`) primary; high contrast black text for legibility in sunlight.
- **Actions**: teal `#00796B` (שלח) — not green because green can confuse with status; danger red `#C62828` (בטל).
- **Status strip**: bright green `#00C853` / amber `#FFB300` / red `#D32F2F` — large, unambiguous.
- **Map cells**: empty = light gray `#E0E0E0`; 1 container = sand `#FFECB3`; 2 = `#FFCA28`; 3+ = `#FF8F00`; dangerous-goods overlay = light sea-green highlight (preserving legacy behavior).

### 6.4 Night mode / day mode

Cab PCs in glass cabs see direct sunlight; outdoor visibility matters. Ship **day mode (high contrast)** by default; night mode optional via `/settings`.

---

## 7. "One-hand, eyes-on-yard" principles

- **Primary actions are at the bottom 1/3 of the screen.** Easier to reach from a seated crane operator's hand. Bottom sheet / drawer UI pattern.
- **Sound feedback** on every state change: a short ding for job-accepted, a different ding for picked, a warning buzz for any error. The operator should be able to know outcomes *without looking*.
- **Haptic feedback** on Android; a small vibration on confirmation. Windows doesn't have this; we rely on sound + color.
- **No modal dialogs mid-workflow.** Never a popup that blocks the map view. Errors go to the status strip and persist until acknowledged.
- **Accidental-tap prevention** for destructive actions (cancel job, logout): require a second tap within 3 seconds, or a long-press.

---

## 8. Accessibility

- **High contrast** ≥ 7:1 for all text per WCAG AAA where possible.
- **Large font scaling**: `/settings` lets operator bump up to 32pt base. UI layout never clips.
- **Color is not the only indicator.** Every colored status has a matching icon and text label.
- **Audio alerts** configurable per event type.
- **Screen reader** support is not critical for this audience but must not be actively broken; use `Semantics()` widgets on interactive elements.

---

## 9. Wireframes — the 3 most-used screens

### 9.1 Login

```
┌──────────────────────────────────────────────────┐
│              ━━━━━━━━━━━━━━━━━━━━━━━━━           │
│                                                  │
│           ┌───── כניסת מפעיל ─────┐               │
│           │                       │               │
│           │ ▼  מנוף: BOND1 ▾     │               │
│           │                       │               │
│           │ ▼  בלוק: BOND1 ▾     │               │
│           │                       │               │
│           │ ▼  שם משתמש: ▾       │               │
│           │    יעקב כהן          │               │
│           │                       │               │
│           │   קוד זיהוי           │               │
│           │   ●●●●                │               │
│           │                       │               │
│           └───────────────────────┘               │
│                                                  │
│         ┌───┐ ┌───┐ ┌───┐                        │
│         │ 1 │ │ 2 │ │ 3 │                        │
│         ├───┤ ├───┤ ├───┤                        │
│         │ 4 │ │ 5 │ │ 6 │                        │
│         ├───┤ ├───┤ ├───┤                        │
│         │ 7 │ │ 8 │ │ 9 │                        │
│         ├───┤ ├───┤ ├───┤                        │
│         │ ← │ │ 0 │ │ ✓ │                        │
│         └───┘ └───┘ └───┘                        │
│                                                  │
│                         ┌───────── כניסה ─────┐   │
│                         └────────────────────┘   │
│                                                  │
│                                     [ יציאה ]    │
└──────────────────────────────────────────────────┘
```

### 9.2 Main yard (`/yard`)

```
┌──────────────────────────────────────────────────────────────────────────┐
│ ●  GOLD1 מקוון  ·  BAY 128 A/2  ·  GPS OK  ·  Twist ✓  ·  14:30:22 ☰    │
├──────────────────────────────────────────────────────────────────────────┤
│                                                                          │
│                      ━━━━  מפת חצר BOND1  ━━━━                           │
│   100  102  104  106  108  110  112  114  116  118  120  122  124  126   │
│ A  ·    2    X    ·    ·    1    ·    X    2    X    ·    ·    1    ·   │
│ B  X    2    X    ·    ·    ·    X    ·    ·    ·    X    X    ·    1   │
│ C  ·    ·    X    X    3    ·    ·    X    X    ·    ·    1    ·    ·   │
│ D  ·    ·    ·    X    X    1    ·    ·    X    X    2    ·    ·    X   │
│ E  X    ·    ·    ·    ·    X    2    ·    ·    X    ·    ·    2    X   │
│ F  ·    ·    X    ·    X    X    ·    ·    ·    X    X    ·    1    ·   │
│ G  ·  ·  ·                                                                │
│ T  X                                                                      │
│                                                                           │
│   מכולה נבחרה: TCLU1234567  ·  גודל: 40  ·  משקל: 24T                    │
│   ┌─ מ ─────────────────┐ ┌─ ל ─────────────────┐                        │
│   │  BOND1 · 106 · B · 1│ │  BOND1 · 128 · A · 2│                        │
│   └─────────────────────┘ └─────────────────────┘                        │
│                                                                           │
│                 ┌─────── שלח ───────┐     ┌──── בטל ────┐                │
│                                                                           │
│ ▼ ממתינות (3): 1. 128→130  2. MSCU9876...  3. TCLU1111...                │
└──────────────────────────────────────────────────────────────────────────┘
```

### 9.3 Container details (bottom sheet, triggered by map tap)

```
(map remains visible above this sheet)
┌──────────────────────────────────────────────────────────────────────────┐
│ ━━━ TCLU 123 4567 ━━━                                              ✕    │
│                                                                          │
│   גודל:            40 ft                                                 │
│   סוג:              GP (General Purpose)                                 │
│   משקל:            24,000 kg                                              │
│   קוד טיפול:        EM                                                   │
│   לקוח:            כריס ולדן בע"מ                                         │
│   קו הובלה:        Maersk                                                │
│   מספר עסקה:       123-45-67                                              │
│   נהג כניסה:       אמיר חליל  ·  יחידה 75-123-45                         │
│   מיקום נוכחי:     BOND1 · 106 · B · 1                                    │
│   חומר מסוכן:      אין                                                   │
│                                                                          │
│   ─────── היסטוריית תנועה ────────                                       │
│   2026-04-20 14:22  כניסה  ·  מלגזן דני  ·  BOND1 106B1                  │
│   (גלול למעלה לראות פרטים נוספים)                                        │
│                                                                          │
│   [ עדכון מיקום ]   [ דו"ח תנועה ]   [ סגור ]                            │
└──────────────────────────────────────────────────────────────────────────┘
```

---

## 10. State management (code architecture)

- **`riverpod`** for app state. Immutable providers for auth, crane_status, yard map, job queue, jobs-in-flight, write queue.
- **Repositories** wrap API + local DB. Single responsibility: `CraneRepository`, `ContainerRepository`, `JobRepository`, `AuthRepository`.
- **Live-state provider** subscribes to the SignalR WebSocket channel `/hubs/crane/:crane_id` and emits snapshot updates; UI rebuilds reactively.

### 10.1 Sketch — yard provider

```dart
final craneStatusProvider = StreamProvider.family<CraneStatus, String>((ref, craneId) {
  final ws = ref.watch(signalRProvider);
  return ws.stream<CraneStatus>('rtg_crane_$craneId');
});

class YardMapController extends AsyncNotifier<YardMap> {
  @override
  Future<YardMap> build() async {
    final craneId = ref.watch(currentCraneIdProvider);
    final status = ref.watch(craneStatusProvider(craneId)).valueOrNull;
    final blockCode = status?.hbBlockCode ?? ref.watch(selectedBlockProvider);
    return ref.read(yardRepoProvider).loadMap(blockCode);
  }

  Future<void> refresh() async {
    // triggered by crane_status.needs_refresh_map NOTIFY event
    ref.invalidateSelf();
  }
}
```

### 10.2 Auth flow

1. User submits login form.
2. `AuthRepository.login(loginName, pin, craneId, blockCode)` POSTs to `/api/auth/login`.
3. On success, stores JWT in secure storage (Flutter `flutter_secure_storage`; wraps Windows DPAPI / Android Keystore).
4. JWT interceptor attaches `Authorization: Bearer <jwt>` to every subsequent API request.
5. On 401 (JWT expired), redirects to login.

---

## 11. Packaging and deployment

### 11.1 Windows

- `flutter build windows --release` → `rtg_cab.exe` + DLLs (~30 MB).
- Packaged into MSI installer (`wix` toolset) with:
  - Install to `C:\Program Files\Goldbond\RTGCab\`.
  - Create start-menu shortcut.
  - **Auto-start on login** (optional; operator preference).
  - **Bootstrap config file** `%LOCALAPPDATA%\RTGCab\bootstrap.json` specifies API base URL + crane id for this cabin:
    ```json
    { "apiBaseUrl": "https://rtg-api.gb.internal", "craneId": "GOLD1" }
    ```
- Signed with Goldbond code-signing certificate (avoids SmartScreen warning).
- Auto-updates via Squirrel or custom check-for-update on startup.

### 11.2 Android

- `flutter build apk --release --split-per-abi` → `rtg_cab.apk` for arm64.
- Distributed via a private channel (MDM if available, or sideload + pin to home screen).
- Bootstrap config is entered on first launch (4-character setup code → maps to per-crane profile on the API).

### 11.3 Updates

- On app start, hit `GET /api/client/version-check` with installed version.
- If server recommends upgrade: banner "עדכון זמין — הורד" that the operator can dismiss per session (supervisor can force upgrade).
- Major releases: supervisor push via MDM (Android) or MSI upgrade (Windows).

---

## 12. Testing strategy for the UI

- **Widget tests** — Flutter's first-class testing. Every screen has at least a "renders successfully with mock data" test.
- **Golden-image tests** — take screenshots of key states (login, yard/online, yard/offline, container bottom sheet) in Hebrew RTL, compare to committed reference images. Catches visual regressions.
- **Integration tests** — end-to-end in a simulator: login → pick → place → logout, asserting UI transitions.
- **Manual testing in cab** — critical. Schedule operator shadowing sessions in Phase 3 before cutover.

---

## 13. Differences from legacy, summarized

| Aspect | Legacy RTGApp.exe | New rtg-cab |
|---|---|---|
| Tech | WinForms, .NET 4.8, Windows-only, x86 | Flutter / Dart, Windows + Android, x64 |
| Forks | RTG1-2 fork vs RTG3 fork (separate .exes) | Single app; per-crane config |
| Polling | `TimerCHE` at 500ms, ~6 SQL/sec/cab | Zero DB polling; SignalR push |
| Auth | PIN matched against group-22 table (not LoginName) | PIN bound to operator + hashed + lockout + rotation |
| Connection | Direct SQL with plaintext creds in binary | HTTPS to API with JWT; no DB creds in client |
| Offline | Crashes / floods RG_ErrorLog | Write queue; UI stays usable |
| DB-injection surface | `Con1.ReturnDT(raw_sql)` in 50+ call sites | API calls only; server parameterises |
| Error display | `MessageBox.Show("...")` mid-workflow | Persistent status strip, non-blocking |
| Localization | Hebrew baked into `SELECT X AS מכולה` | `.arb` files with translation keys |
| Accessibility | None | Contrast, font scaling, audio/haptic |
| Logout | Implicit (close window) | Explicit button + idle timeout |
| Multi-window | FrmMap01 + FrmMenu + child stacked | Single-window nav with drawer |
| Keypad | 2 copies per form (`_1` suffix) | One shared `NumericKeypad` widget |
| Platform per crane | RTG1-2 vs RTG3 differ in code | Same binary; differs via config |

---

## 14. Open questions for the UI

### Q-64 🟡 Medium — Does the cabin PC have sound output?
If not, reconsider audio feedback and lean on haptic + visual.

### Q-65 🟡 Medium — Kiosk mode?
Should the Windows build auto-start full-screen and prevent Alt-Tab? Operators sometimes need to run other apps (weight display, etc.). Likely: not kiosk mode. Confirm.

### Q-66 🟡 Medium — Barcode / OCR integration?
Is there a camera or barcode scanner on the cab that can read container IDs? If yes, integrate with `mobile_scanner` package. If not, stick with manual keypad entry.

### Q-67 🟢 Low — Language options beyond Hebrew?
Q-55 revisited at UI scope. Currently Hebrew-only; `l10n` infrastructure is in place so English can be added with one `app_en.arb` file.

### Q-68 🟡 Medium — Session length / shift model
Default JWT expiry 8 hours ≈ one shift. Operators sometimes stay past shift (overlap with relief). Should the UI silently refresh on activity, or force re-login at 8h? Decide with the yard supervisor.

### Q-69 🟡 Medium — RTG3 "bulk mark empty" action
The legacy RTG3 binary has a bulk `UPDATE TB_Location SET empty=1` action. Should this function be exposed for **all** cranes in the new UI, or retired? Likely retire; the empty-location detection should be automatic.

---

## 15. Check-in summary

- **One Flutter codebase** replacing both RTG1-2 and RTG3 UIs; per-crane via bootstrap config. Targets Windows (primary) + Android tablet (secondary). Hebrew-RTL throughout with `.arb` externalised strings.
- **The `/yard` screen becomes the centre of gravity** — live status strip on top, direct-manipulation yard map in the middle, selection+send at the bottom. No more 2500-line `Form1.cs` rendering everything at once. Secondary screens accessed via drawer.
- **Offline-first by design:** local SQLite cache + durable write-queue with idempotency keys. Operators never see a crash or a blank screen when connectivity hiccups. SignalR push replaces the 500ms-polling pattern → zero baseline DB load from the UIs.
- **Next:** Step 2.5 — Coexistence & Dual-Write detailed design (sequence diagrams for every write, failure-handling matrix, reconciliation, exit criteria).
