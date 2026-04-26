# 11. Parity audit — legacy RTGApp vs rtg_cab MVP

Started: 2026-04-23.
Goal: enumerate every feature of the legacy `Current/From TFS/RTG/RTG1-2/RTGApp/`
cabin app, check whether rtg_cab (the new Flutter app) has an equivalent,
and flag gaps for prioritisation.

Process: legacy rebuilt with the clone DB (see scripts/run-legacy.md),
walked side-by-side with the running rtg_cab.exe against the same
`SQLDEV01\MSSQLSERVERDEV.TerminalData_AI` instance, screenshots captured
as we went.

Status key:
- **✓ shipped** — equivalent exists in rtg_cab
- **◐ partial** — partially shipped; see notes
- **✗ missing** — not in rtg_cab yet
- **✗ obsolete** — legacy-only, no plan to port (document why)

---

## 1. Login screen (`FrmLogin`)

| Element | Legacy | rtg_cab | Status |
|---|---|---|---|
| Crane (`מנוף`) dropdown | Populated from `C:\RTG\CHEName.txt` | Hard-coded from `bootstrap.json:craneId` | ◐ equivalent — rtg_cab doesn't need a dropdown since each cabin is pinned to one crane. |
| Block (`ערוגה`) dropdown | BOND1/2/3 dropdown | Hard-coded from `bootstrap.json:defaultBlockCode` | ◐ same reasoning. |
| User (`שם משתמש`) dropdown | Queries SC_Users / HR_Emp for group-22 operators | `GET /api/auth/operators` → dropdown of active operators | ✓ shipped (L23-L26 sequence). |
| PIN (`סמסא`) | 4-digit, group-PIN fallback | 4-digit, per-operator Argon2 hash | ◐ security stronger (no group-PIN hole — see discovery §4.2) but UX parity. |
| Big numeric keypad | 1-9, 0, מחק | Identical layout | ✓ shipped. |
| Goldbond logo + crane photo | Present | **Missing** | ✗ easy add — bundle the PNG as an asset. |
| Green ✓ / red ✗ buttons | Present | Text `כניסה` / `יציאה` | ◐ functional but less iconographic. |

---

## 2. Main workday screen (`FrmMap01` ↔ rtg_cab yard screen)

### 2.1 Yard grid

| Element | Legacy | rtg_cab | Status |
|---|---|---|---|
| Grid rendered as bays × rows | ✓ | ✓ | Shape matches (17/40/27 bays, 8 rows). |
| Stack count shown in cell | Number (colour-coded) | Number | ✓ |
| Stack-count colour | Digit `ForeColor`: 1=YellowGreen, 2=Blue, 3=Sienna, 4=Orange, 5=Salmon, 6=Pink (Form1.cs:615-649 — **not** cell bg) | Same palette on digit, white cell bg (P1-8) | ✅ ported 2026-04-23 |
| Stack colour — special/DG | Light-sea-green cell bg | LightSeaGreen (`#20B2AA`) cell bg | ✅ |
| Highlight current crane position | Blue cell (separate POS1 label in legacy, effect is "crane is here") | Light-blue cell fill driven by `hb_bay`/`hb_row` SignalR push (P1-9) | ✅ shipped 2026-04-23 |
| Pending-lift marker | Cell colour-coded per RG_ColDG | Deep-orange border + `↑` icon (L23) | ◐ present but different visual. |
| Tap-to-select FROM/TO | Single-click FROM auto-fills; TO by separate mechanism (unclear from screenshot, assume numpad) | Two-phase tap (L23 fix) | ◐ needs confirmation vs legacy flow. |

### 2.2 Container detail bar (bottom strip of FrmMap01) — ✅ P0-1 shipped 2026-04-23

Legacy shows, for the currently-selected container:
- `מכולה` — container ID (`ADMU5111819`)
- size field (`40`)
- cargo type code (`HH`)
- `מאיתור` (lift from) — `150E5`
- destination arrow `<----------`
- `לאיתור` (deliver to)
- `מכולת מטרה:` (target container)
- `נהג` (driver)
- `רכב` (vehicle / plate)
- `ק"ג` (weight kg)
- truck-type indicator (`משאית` button swaps mode)

rtg_cab current has only `from chip → to chip + container id`. **Missing:**
- cargo type code
- driver
- vehicle plate
- weight
- target-container concept
- truck-type mode switch

### 2.3 Status strip (bottom-right, 9 fields)

| Field | Legacy | rtg_cab |
|---|---|---|
| Len (`40`) | ✓ | ✓ (via RG_A1 → crane_status) |
| PLC (`01`) | ✓ | ✓ |
| CHE (`GOLD1`) | ✓ | ✓ |
| GPSStatus (`02`) | ✓ | ✓ |
| CraneStatus (`01`) | ✓ | ✓ |
| Position | ✓ (blank in screenshot) | ✓ |
| HBBlockName (`ERROR`) | ✓ | ✓ |
| Status (`0000`) | ✓ | ✓ |
| Time (`004031`) | ✓ | ✓ |

**Data is there** — we just render some of it in the colour strip, not as a grid. Minor layout gap. **◐**

### 2.4 Action buttons (bottom row)

| Button | Legacy icon/label | rtg_cab | Status |
|---|---|---|---|
| Submit job | ✓ | שלח | ✓ |
| Cancel job | ✗ (red X) | בטל (clears selection only) | ✗ **missing** — legacy's Cancel sends `MessageCancel` wire frame to the crane, not just UI reset. See discovery §3.5. |
| Truck mode | `משאית` + truck icon | none | ✗ **missing**. |
| Work orders (`עבודות`) | Yellow tab button | none | ✗ **missing** (see §4). |
| Info (`מידע`) | Blue ℹ button | none | ✗ **missing** (see §3). |
| EXIT (`יציאה`) | ✓ | יציאה | ✓ |
| ▶ Play | Purpose unclear from screenshot | — | ? |
| 🟥 Red square | Likely reconnect (`EnconsoleButtom`) | `ReconnectBanner` | ◐ different UX — legacy is a single big button; ours is a passive banner. |

---

## 3. Truck button (`משאית`) — loads/unloads at the crane's current position

Walked 2026-04-23. The main-map `משאית` button opens a form titled
`משאיות` with two stacked tables — what needs to be unloaded at this
bay + what needs to be loaded onto trucks here. Context-sensitive to
the crane's current position (per user note: "מראה נתונים על המיקום
של המנוף").

### 3.1 Unloading panel (`פריקה`)

Columns (RTL order, right-to-left):
`מכולה` (container) · `גודל` (size, e.g. 40/20) · `סוג` (type, e.g.
HC/RG) · `משקל` (weight) · `קוד` (code, e.g. PP/EX) · `קו` (shipping
line, e.g. MSC/ZIM) · `משאית` (truck number) · `איתור` (location) ·
`המתנה` (waiting id) · `UN` (UN / hazardous code) · `AVDM`.

### 3.2 Loading panel (`טעינה`)

Same columns plus `מיקום` (slot) and `DriverID`.

### 3.3 Buttons

`בחר` (green — select the highlighted row; probably auto-fills the
main map's source/target) · `חזרה` (red — back to map).

### rtg_cab parity

None. Truck-mode switch hasn't shipped. P0 gap (was flagged in §6).
Likely needs: new endpoints `GET /api/crane/{id}/truck/unloading` and
`/loading`, a new Flutter screen, and a "truck mode" toggle on the
main map that flips container-detail mode + enables this view.

---

## 4. Info menu (`FrmMenu` → 7 sub-screens)

The `מידע` button opens a 2×4 grid of buttons. Each opens a dedicated
report/editor form. Seven walked 2026-04-23; columns captured below.

### 4.1 `מכולות ריקות` — Empty containers (`FrmEmptyContainers`)

- **Filters (top):** shipping-line dropdown, size dropdown, type
  dropdown, and a pink `הכל` (All) button.
- **Columns:** `מכולה` · `גודל` · `סוג` · `איתור` · `תאריך כניסה`
  (entry datetime) · `קו` · `לקוח` (customer).
- **Footer:** `סהכ 1751 מכולות` (total count, live).
- **Row shapes observed:** container ids like `XXXX3510669`,
  `TCLU9561320`, sizes 20/40, types OT/HC/RG/TK; mix of empty and
  filled location fields.
- `יציאה EXIT` button bottom-left.

### 4.2 `מכולות ללא איתור` — No-location containers (`FrmContainerNoLocation`)

- No filter bar.
- **Columns:** `מכולה` · `תאריך כניסה` · `קוד טיפול` (handling code,
  mostly `EM`) · `קו ספנות` (mostly `BOR`).
- **Footer:** `סהכ 197 מכולות`.
- `יציאה` button.
- Clone snapshot: 197 containers flagged as "entered but unplaced"
  — exactly the population the MIS / yard planning needs to assign
  slots for.

### 4.3 `יומן תנועות` — Movement log (`FrmInOutDiory`)

- **Filters (top):** `בחר תנועה` dropdown (choose entry OR exit) +
  pink `הכל` button.
- **Body:** table below. Empty in the screenshot (`אין נתונים` big
  red placeholder when no movement matches the chosen filter — e.g.
  no entries today yet).
- **Footer:** small free-text box (probably count / free filter).
- `יציאה` button.
- Likely backed by `dbo.RG_Shifting` + `dbo.CO_Containers` entry /
  exit datestamps. Parallel in the new schema is `rtg.movement`.

### 4.4 `מכולות לגוש` — Containers by block (`FrmContainersByBloc`)

- **Filters (top):** `בחר חומ"ס` (choose dangerous-goods material) +
  `בחר איתור` (choose location).
- **Left panel:** full 0–9 keypad with `אישור` (confirm) and `מחק`
  (backspace). Looks like the operator types a bay number here.
- **Body:** empty container-detail grid (populates once filters set).
- `יציאה` button.
- Probably a multi-filter drill-down across the whole block with the
  option to zero in on a bay or DG class.

### 4.5 `עדכון איתור` — Update container location (`ContainerLocation`)

- **Inputs:**
  - `מכולה` — container id (text box with a dropdown beside it for
    selecting from a short list). Keypad-entered.
  - `מיקום` — 3 small dropdowns in a row (looks like bay / row /
    height pickers).
- **Right panel:** 0–9 keypad + `מחק`.
- **Actions:**
  - ✓ green — save the new slot.
  - ✗ red — cancel.
  - `הסר מערוגה` (yellow) — "remove from yard" — clears the slot
    entirely, presumably setting `TB_Location.Container = NULL` for
    that container (equivalent of `rtg.location.container = NULL`).
- Writes go to `dbo.TB_Location`.

### 4.6 `איתורים מומלצים` — Recommended locations (`FrmRecommendedLocation`)

- **Filter bar (top):** `לקוח` (customer) · `גודל` · `סוג` ·
  `טיפול` (handling — `EM` selected in screenshot) · `קו ספנות` ·
  `איתור מומלץ` (recommended location, text+keypad).
- **Body (right):** grid with columns `לקוח` (customer — 001 אוגרין,
  005 קוסקו, 006 כרמל ספנות, 009 סקנדינבית, 012 לוסי ספנות…),
  `גודל`, `סוג`, `ספנות` (3-letter shipping code — EMC, COS, ADM,
  OOL, BOR), `א מומלץ` (recommended location — most entries are
  `אין כניסה` "no entry" i.e. null; some have values like `64`,
  `68`, `רכבת` "train").
- **Left panel:** digit keypad 0–9 + **purple `A` and `D` buttons**
  (row letters — likely A/B/C/D/E/F and the two shown are the most-
  used ones) + `אישור` / `מחק` / `יציאה`.
- Writes go to `dbo.TB_RecommendedLocation`.
- This is the yard-planner's "put future shipments from customer X,
  size Y, via line Z at recommended slot N" config screen.

### 4.7 `מכולות צפויות` — Expected containers (`FrmExpectedContainers`)

- **Filter bar (top):** `קו ספנות` · `גודל` · `סוג` · `טיפול` +
  pink `הכל`.
- **Columns:** `מכולה` · `גודל` · `סוג` · `טיפול` · `לקוח` ·
  `איתור צפוי` (expected location) · `קו` · `תאריך שידור`
  (broadcast / ETA date).
- Empty in screenshot — likely filtered; remove filters to populate.
- `יציאה` button.
- Data source: probably a joins-view of `CO_Containers` with entries
  marked `EntranceDate IS NULL` + future-dated `RegisterDate`, or an
  external feed table.

### 4.8 Gap table — all 7 info screens + truck button

| # | Legacy form | Hebrew | Source table(s) | rtg_cab | Status |
|---|---|---|---|---|---|
| M1 | משאיות | `משאית` button (on main) | probably `CO_Containers` + `TB_Drivers` | none | ✗ **P0 missing** |
| M2 | FrmEmptyContainers | `מכולות ריקות` | `CO_Containers` where empty | none | ✗ **P1** |
| M3 | FrmContainerNoLocation | `מכולות ללא איתור` | `CO_Containers` where `LocationCode IS NULL` | none | ✗ **P1** |
| M4 | FrmInOutDiory | `יומן תנועות` | `RG_Shifting` / `CO_Containers` entrance/exit dates | none | ✗ **P1** |
| M5 | FrmContainersByBloc | `מכולות לגוש` | `TB_Location` + `CO_Containers` | none | ✗ **P1** |
| M6 | ContainerLocation | `עדכון איתור` | writes `TB_Location` | none | ✗ **P0 missing** (operators need it to fix bad data) |
| M7 | FrmRecommendedLocation | `איתורים מומלצים` | writes `TB_RecommendedLocation` | none | ✗ **P2** (yard-planner tool, not daily operator) |
| M8 | FrmExpectedContainers | `מכולות צפויות` | unknown view, probably `CO_Containers` | none | ✗ **P1** |

---

## 4. Work orders (`עבודות` → `FrmWorks`)

Legacy screenshot (2026-04-23 walkthrough, screen 3):

Shows a spreadsheet-style table with columns:
- **מכולה** (container id)
- **גודל** (size — 40)
- **סוג** (type — HC, RH, …)
- **משקל** (weight)
- **איתור** (5-char location code — e.g. `112B6`, `134B4`)
- **מיקום** (slot — e.g. `6/6`, `4/5` — probably current/max stack height)
- **לתאריך** (scheduled date/time — e.g. `4/9/2026 PM 3:00`)
- **סוג עבודה** (work type — `שיקוף מכולה` (scan), `איסוף מכולות במשאית ו...` (truck-load collection), `איתות מכולה…`)

Filter controls at the top:
- Date picker (`לתאריך`)
- Work-name dropdown
- Block tabs (`הכל` / `BOND1` / `BOND2`)

Backing data is probably `dbo.RG_Work*` or similar (confirmed in discovery §6.7 — `FrmWorks`). Needs its own server endpoint in rtg-api + a dedicated screen in rtg_cab.

**rtg_cab equivalent:** none. The cabin's `pendingJobsProvider` is vaguely
related but only shows jobs the crane has to do *right now* — `עבודות`
is the full schedule across all work types, across all blocks,
across a date range. Different semantic.

---

## 5. Other observations

- The legacy main screen uses the **BOND2** block in screen 1 but the crane is **GOLD1** (CHE field) — which means an operator can switch the block dropdown on login to work a different yard. rtg_cab's `bootstrap.json:defaultBlockCode` is single-valued. Maybe promote it to a runtime dropdown?
- Status strip shows `HBBlockName = ERROR` — likely the real crane isn't connected, so the last-known position is stale / invalid. Our equivalent would render the amber "stale" state after >5s.
- A `"BOND2"` label appears top-right of screen 1 alongside the row labels, suggesting the block name is rendered as a column label for the rightmost column. Our rtg_cab has it at the top of the status strip instead — minor layout difference.

---

## 7. Priority buckets (revised after 2026-04-23 walkthrough)

### Must have before cabin rollout (P0)

1. **Full container-detail bar** on the main map — driver, vehicle,
   weight, cargo-type code, target container.
2. **Cancel-job wire send** (`btmCancel_Click` → `MessageCancel`).
   Without it, operators can't abort a pending job from the cab.
3. **Truck mode / משאית form** — shows unloading + loading tables at
   the crane's current position, with `בחר` to auto-fill the lift/place.
4. **Work orders screen (עבודות)** — full scheduled-jobs table with
   date + block + work-type filters. Operators plan their shift from
   here.
5. **Update container location (עדכון איתור)** — operators fix wrong
   yard slots on the fly. Without it, bad `TB_Location` data stays bad.
6. **Goldbond logo + crane photo on login** — cheap brand parity;
   operators will notice its absence.

### Nice to have (P1)

7. **Info-menu reports** (4 of 6 most useful):
   - `מכולות ריקות` (empty containers)
   - `מכולות ללא איתור` (no location)
   - `יומן תנועות` (movement log)
   - `מכולות צפויות` (expected incoming containers)
8. **Colour palette match** — pink for full-stack cells, per legacy.
9. **Current-crane-position cell highlight** — blue outline on the
   bay/row where the crane physically is (legacy reads from RG_A1).
10. `▶` play button (if its purpose is not just test/debug — to
    confirm during walkthrough).

### Nice to have but low-frequency (P2)

11. **Recommended-locations editor (איתורים מומלצים)** — yard-planner
    tool, not daily cabin operator workflow. Safe to defer or move to
    a separate admin UI.
12. **Containers by block (מכולות לגוש)** — drill-down with DG
    material + bay picker. Could fold into yard map tap-and-filter.
13. Block dropdown at login — switch block without redeploying cabinet.

---

## 7. Open questions (for the walkthrough)

- What does the `▶` play button do?
- What is "מכולת מטרה" (target container)? Discovery didn't capture this.
- Is the `HH` in the container detail bar the `HandlingTypeCode` from discovery §3.1? Confirm.
- Does clicking a work order in `עבודות` auto-fill the main map's source/target?
- What granularity of "recommended locations" does `FrmRecommendedLocation` edit — per container? Per weight class?

---

_Living document — extend as more screens are walked._
