# Handoff: מסך כניסה — RTG Crane Operator

## Overview
This is the login/sign-in screen for the **גולד-בונד RTG Crane Operator** system — the touchscreen terminal mounted in the cabin of an RTG (Rubber-Tyred Gantry) crane at the container yard.

The operator lands on this screen at shift start. They:
1. Pick their identity from a **dropdown of known operators** (no keyboard typing).
2. Enter a **4-digit PIN** on the on-screen numeric keypad.
3. Pick which **RTG crane** they're climbing into (GOLD1 / GOLD2 / GOLD3).
4. Tap **"כניסה למערכת"** — goes directly to the main yard screen (no splash, no greeting modal).

Target hardware: **1920×1080, 21" touch panel**, RTL Hebrew UI.

---

## About the design files
The files in this bundle are **design references created in HTML/React** — prototypes showing the intended look and behavior. They are **not production code to ship as-is**.

Your task is to **recreate this design inside the target codebase's existing environment** (per the PRD this will be Flutter), using that stack's idiomatic patterns, state management, and widget library. If there's no existing environment yet, choose the framework that fits the rest of the product (Flutter) and implement there.

## Fidelity
**High-fidelity (hifi).** All colors, spacing, typography, radii, and shadows are final. Recreate pixel-perfectly, then wire to real data (operator list from backend, PIN verification endpoint, crane availability feed).

---

## Screen anatomy

The screen is RTL, full-bleed, with three stacked zones:

```
┌──────────────────────────────────────────────────────────────────┐
│  [LOGO]  │  TITLE + BUILD    ← spacer →    [PLC][GPS][TOS][NET]  │  ← top bar (112 px tall)
│          │                                                        │     07:12:04
├──────────────────────────────────────────────────────────────────┤
│                                                │                   │
│   RIGHT: hero + live stats (1.15fr)            │  LEFT: login card │   ← main content
│                                                │    (1.0fr)        │      (padding 14/64/32)
│   - "מערכת חדשה · דור 4" badge                │                   │
│   - H1 "בוקר טוב. מוכנים להתחיל?"              │  - username picker│
│   - lede paragraph                             │  - PIN dots       │
│   - 4-up live stats strip                      │  - crane picker   │
│                                                │  - actions        │
│                                                │  - security note  │
│                                                │                   │
│                                                │                   │
│   [floating numeric keypad, bottom-left,       │                   │
│    380 px wide]                                │                   │
└──────────────────────────────────────────────────────────────────┘
```

### Layout
- Root: `position: relative`, full viewport, `direction: rtl`, `overflow: hidden`, `font-family: "Heebo", system-ui`.
- Background stack (z:0): radial gradient wash + SVG grid overlay + SVG container-silhouette band along the bottom (all below z:2 chrome).
- Top bar (z:2): `display: flex`, `padding: 22px 32px`, `gap: 16px`.
- Main grid (z:2): `display: grid; grid-template-columns: 1.15fr 1fr; gap: 44px; padding: 14px 64px 32px; height: calc(100% - 112px);`.
- Numeric keypad (z:5): `position: absolute; bottom: 20px; left: 64px; width: 380px;`.

---

## Components

### 1. Top bar

| Element | Spec |
|---|---|
| Goldbond logo | `<img src="goldbond-logo.png">`, `height: 108px`, `width: auto`, `object-fit: contain`, drop-shadow `0 3px 6px rgba(15,23,42,.12)` |
| Divider | `1px × 72px`, `background: #cbd5e1`, `margin: 0 8px` |
| Title | "מערכת מנופאי RTG" — 22px / 800 / letter-spacing -0.2px / color `#0f172a` |
| Build line | "גרסה 4.2.1 · build 20260415" — 13px / IBM Plex Mono / color `#64748b` |
| System pills (4) | PLC / GPS / TOS / רשת — see *System pill* component |
| Clock | "07:12:04" — 24px / 700 / IBM Plex Mono / color `#0f172a`, `margin-inline-start: 14px` |

#### System pill
- Pill: `padding: 6px 12px`, `border-radius: 999px`, `background: rgba(255,255,255,.8)`, `backdrop-filter: blur(8px)`, `border: 1px solid #e2e8f0`
- Dot: `8px × 8px`, `border-radius: 8px`, `background: <okColor>`, `box-shadow: 0 0 0 3px <okColor>33`
- Label (small caps feel): 11px / 700 / color `#64748b`, letter-spacing 0.5
- Value: 12px / 600 / IBM Plex Mono / color `#0f172a`

Four pills, all `ok = #16a34a`:
- `PLC` — "מחובר"
- `GPS` — "RTK · 14 לוויינים"
- `TOS` — "online"
- `רשת` — "1 Gbps"

### 2. Right column — hero + stats

#### Badge "מערכת חדשה · דור 4"
- `display: inline-flex; gap: 10px; padding: 8px 16px; border-radius: 999px`
- `background: #ccfbf1; color: #115e59; border: 1px solid #99f6e4`
- 13px / 700 / letter-spacing 1
- Leading 8×8 dot `#0f766e` with halo `0 0 0 4px rgba(15,118,110,.2)`

#### Hero title
```
בוקר טוב.
מוכנים להתחיל?
```
- 72px / 900 / letter-spacing -1.5px / line-height 1.02
- First line color `#0f172a`
- Second line: gradient text
  `background: linear-gradient(135deg, #0f766e 0%, #0284c7 60%, #7c3aed 100%)`
  with `-webkit-background-clip: text` + transparent fill

#### Lede paragraph
"זיהוי המנופאי וחיבור למנוף. כל התנועות בערוגה מסונכרנות בזמן אמת עם המערכת הלוגיסטית."
- 18px / 400 / line-height 1.5 / color `#475569` / `max-width: 480px` / `text-wrap: pretty`

#### Live stats strip (4 columns)
Container: `padding: 18px; border-radius: 16px; background: rgba(255,255,255,.7); backdrop-filter: blur(10px); border: 1px solid #e2e8f0; box-shadow: 0 8px 28px rgba(15,23,42,.04)`.

Each cell:
- Label: 11px / 700 / color `#64748b` / letter-spacing 0.5
- Value: 26px / 800 / IBM Plex Mono / color `#0f172a`
- Delta (optional): 12px / 700 / color `#16a34a`
- Sub (optional): 12px / 400 / color `#94a3b8`

Data shown on mock:
| Label | Value | Delta / Sub |
|---|---|---|
| מכולות פעילות | 2,847 | +12 |
| משאיות ממתינות | 7 | שער ראשי |
| תנועות היום | 184 | +4% |
| זמן מחזור ממוצע | 4:12 | דקות |

### 3. Left column — login card

Card:
- `background: #ffffff`
- `border: 1px solid #e2e8f0`
- `border-radius: 20px`
- `padding: 28px`
- `box-shadow: 0 30px 60px -20px rgba(15,23,42,.18), 0 0 0 1px rgba(15,23,42,.02)`
- Decorative radial glow pinned top-left inside the card:
  `position: absolute; top: -60px; left: -60px; width: 220px; height: 220px; border-radius: 50%; background: radial-gradient(circle, rgba(15,118,110,.12), transparent 70%)`
- Flex column, `gap: 18px`

#### Card header
- 44×44 icon tile: `border-radius: 12px; background: #0f766e; color: #fff; font-size: 20px; box-shadow: 0 6px 14px rgba(15,118,110,.35)` — glyph 🔐 (swap for real lock icon)
- Title "כניסת מפעיל" — 20px / 800 / color `#0f172a`
- Subtitle "זיהוי + בחירת מנוף" — 12px / 400 / color `#64748b`
- Shift chip (pushed to end): `padding: 6px 12px; border-radius: 999px; background: #f1f5f9; color: #475569; font: 700 11px IBM Plex Mono; letter-spacing: 1` — text "SHIFT 07:00–15:00"

#### Username dropdown
Label: "שם משתמש" — 12px / 700 / `#64748b` / letter-spacing 0.5 / margin-bottom 6.

Trigger button:
- `width: 100%; height: 62px; border-radius: 12px; padding: 0 18px`
- Idle: `border: 2px solid #e2e8f0; background: #f8fafc`
- Focused / open: `border: 2px solid #0f766e; background: #f0fdfa`
- Flex row, gap 12, text-align right
- Avatar tile 38×38, `border-radius: 10px`, initial letter, 16px / 800
  - Empty state: background `#e2e8f0`, color `#94a3b8`, glyph "א"
  - Selected: background `#0f766e`, color `#fff`
- Text block (flex:1): name 18px / 700 / `#0f172a`, sub line 12px / IBM Plex Mono / `#64748b` ("ID · role")
- Placeholder when empty: "בחר משתמש מהרשימה" — 18px / 500 / `#94a3b8`
- Chevron ▾ — 18px / `#64748b`, `transform: rotate(180deg)` when open, `transition: transform .2s`

Dropdown panel (when open):
- `position: absolute; top: calc(100% + 6px); left: 0; right: 0; z-index: 10`
- `background: #fff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 6px`
- `box-shadow: 0 20px 40px rgba(15,23,42,.18); max-height: 280px; overflow-y: auto`

Row:
- `padding: 10px 12px; border-radius: 10px; margin-bottom: 2px`
- Active: `background: #ccfbf1`; inactive hover should highlight the same
- 34×34 avatar (active: `#0f766e` bg, white; inactive: `#f1f5f9` bg, `#475569` text)
- Name 14px / 700 / `#0f172a`; sub 11px / IBM Plex Mono / `#64748b`
- Shift (flex-end): 10px / IBM Plex Mono / `#64748b`

**Operator list (mock — replace with backend feed):**
| id | name | role | shift |
|---|---|---|---|
| GOLD1 | דוד כהן | מנופאי בכיר | 07:00–15:00 |
| MOSHE1 | משה לוי | מנופאי | 07:00–15:00 |
| AVI2 | אבי כהן | מנופאי | 15:00–23:00 |
| YONI3 | יוני שפירא | מנופאי בכיר | 23:00–07:00 |
| RONIT4 | רונית מזרחי | מנופאית | 07:00–15:00 |
| AMIR5 | עמיר בנדוד | מתאמן | On-call |

On selection: close dropdown, move focus to the PIN row.

#### PIN row
Label: "קוד זיהוי (PIN)" — same style as username label.

Row container:
- `height: 62px; border-radius: 12px; padding: 0 18px`
- Idle: `border: 2px solid #e2e8f0; background: #f8fafc`
- Focused: `border: 2px solid #0f766e; background: #f0fdfa`
- Flex row, `gap: 14px`

4 dot cells (fixed count):
- `50×50; border-radius: 10px`
- Empty: `background: #fff; border: 2px solid #cbd5e1`
- Filled: `background: #0f766e; border: 2px solid #0f766e`, dot glyph ●, 28px / 800 / IBM Plex Mono / white
- Current caret cell: `border: 2px solid #0f766e; box-shadow: 0 0 0 4px rgba(15,118,110,.15)`
- `transition: all .12s`

Trailing counter: "1/4" — 11px / IBM Plex Mono / `#94a3b8`.

#### Crane picker
Label row: "בחירת מנוף · שיוך פעיל לגוש" — same small-caps look, with the "· שיוך פעיל לגוש" as 11px / `#94a3b8` inline.

Grid: `grid-template-columns: repeat(3, 1fr); gap: 8px`.

Button (each crane):
- `padding: 12px 10px; border-radius: 12px`
- Default: `border: 2px solid #e2e8f0; background: #fff; color: #0f172a`
- Active: `border: 2px solid #0f766e; background: #0f766e; color: #fff; box-shadow: 0 8px 20px rgba(15,118,110,.3)`
- Disabled (maintenance): `background: #f8fafc; color: #94a3b8; opacity: .65; cursor: not-allowed`
- Inside, top row: `GOLD1` (20px / 800) on the right, 8×8 status dot on the far-left (available `#16a34a`, disabled `#fbbf24`, active & available `#a7f3d0`)
- Sub-line: `BOND1 · זמין` (11px / 600, opacity 0.85 when active, 0.65 otherwise)
- `transition: all .15s`

**Crane data (mock):**
| id | block | status | available |
|---|---|---|---|
| GOLD1 | BOND1 | זמין | ✓ |
| GOLD2 | BOND1 | זמין | ✓ |
| GOLD3 | BOND2 | בתחזוקה | ✗ |

#### Action row
Flex row, gap 10, margin-top 4.

"יציאה" button (`flex: 1`):
- `height: 60px; border-radius: 14px`
- `background: #fff; color: #475569; border: 1.5px solid #e2e8f0`
- 16px / 700

"כניסה למערכת" button (`flex: 2`):
- `height: 60px; border-radius: 14px; border: none; color: #fff`
- Enabled: `background: linear-gradient(135deg, #0f766e 0%, #134e4a 100%); box-shadow: 0 12px 28px rgba(15,118,110,.35)`
- Disabled: `background: #cbd5e1; cursor: not-allowed; box-shadow: none`
- 17px / 800
- Label + trailing arrow "←" (20px), `gap: 10px`
- `transition: all .2s`

Enabled iff: `userId && pin.length === 4 && selectedCrane.available`.

#### Security footer
- `padding: 10px 12px; border-radius: 10px; background: #f8fafc`
- Flex row, gap 10, 11px / 400 / `#64748b`
- 🛡️ glyph + "גישה מאובטחת · כל הפעולות נרשמות" + session id "session #A4F2-7710" (IBM Plex Mono)

### 4. Numeric keypad (floating)

Container:
- `position: absolute; bottom: 20px; left: 64px; width: 380px; z-index: 5`
- `background: #fff; border: 1px solid #e2e8f0; border-radius: 16px; padding: 14px`
- `box-shadow: 0 20px 40px rgba(15,23,42,.18)`

Header:
- "מקלדת נומרית · קוד זיהוי" — 13px / 700 / `#0f172a` + 11px / `#64748b`
- Close X (right): 28×28, `border-radius: 8px; border: 1px solid #e2e8f0; background: #f8fafc; font-size: 14px`

Grid: `grid-template-columns: repeat(3, 1fr); gap: 8px`.

Keys: 1–9, then `CLR`, `0`, `←` in a 4th row.
- Height 64px; border-radius 12px
- Digits: `background: #fff; color: #0f172a; border: 1.5px solid #e2e8f0`; font 700 26px
- `CLR`: `background: #fef2f2; color: #dc2626; border: 1.5px solid #fecaca`; font 700 15px
- `←` (backspace): `background: #fffbeb; color: #d97706; border: 1.5px solid #fde68a`; font 700 26px

Behavior:
- Every key first forces `focus = 'pin'` (even if the dropdown had focus).
- `CLR` clears the PIN.
- `←` removes last digit.
- `0–9` append when `pin.length < 4`.

### 5. Ambient background

Three layered SVGs/CSS at z:0, non-interactive:

1. Base wash:
   ```css
   background:
     radial-gradient(1100px 700px at 85% -20%, rgba(15,118,110,.14), transparent 60%),
     radial-gradient(900px 600px at 10% 110%, rgba(2,132,199,.10), transparent 60%),
     linear-gradient(180deg, #f6f8fa 0%, #eef2f7 100%);
   ```
2. Faint 44×44 grid SVG, `opacity: 0.5`, stroke `#cbd5e1` at 0.5px.
3. Container-silhouette band (bottom 180px), `opacity: 0.08`, fill `#0f172a`:
   - 3 rows of 108×52 rectangles (r=2), staggered: 16 along bottom, 14 offset, 10 on top — evokes a stack of containers in the yard.

---

## Interactions & behavior

| Trigger | Behavior |
|---|---|
| Tap username field | Open dropdown, focus ring turns teal |
| Pick a user from dropdown | Close dropdown, set `userId`, move focus to PIN |
| Tap a PIN cell / numeric key | Focus → PIN; key handlers run |
| Digit key (0–9) | Append if `pin.length < 4` |
| `CLR` | Empty the PIN |
| `←` | Pop last digit |
| Tap a crane button | Set `crane` if the crane is available; no-op if disabled |
| "כניסה למערכת" tap | Only enabled when `userId && pin.length === 4 && craneAvailable`. On tap: call login endpoint, on success navigate to main yard screen. |
| "יציאה" tap | Shut down / return to OS (depends on kiosk wrapper) |
| Close button on keypad | Hide keypad; tapping the PIN row should bring it back |
| Clock | Updates every second (not currently wired — add a 1s interval) |
| System pills | Bind to live heartbeat feeds from PLC / GPS receiver / TOS WebSocket / network check. Green dot when healthy; change color + text when degraded. |

No transitions between login → main screen (user asked for immediate handoff — no greeting / splash).

Focus states are visible (teal borders) so touchscreen users always know where input is going.

---

## State

```ts
type LoginState = {
  userId: string;        // '' until picked
  userOpen: boolean;     // dropdown open
  pin: string;           // '' | '1' | '12' | ... (max 4)
  crane: 'GOLD1' | 'GOLD2' | 'GOLD3';
  focus: 'user' | 'pin';
  showKeypad: boolean;
};
```

Derived:
- `user = operators.find(u => u.id === userId)`
- `selectedCrane = cranes.find(c => c.id === crane)`
- `canLogin = Boolean(userId) && pin.length === 4 && selectedCrane?.available`

Remote dependencies:
- `GET /api/operators` → `{ id, name, role, shift }[]` (for dropdown)
- `GET /api/cranes` → `{ id, block, status, available }[]`
- `POST /api/login { userId, pin, craneId }` → session token + operator profile, or 401
- Live heartbeats for PLC/GPS/TOS/network pills

---

## Design tokens

### Colors
| Token | Hex | Use |
|---|---|---|
| surface-base | `#f6f8fa` | bg wash start |
| surface-base-2 | `#eef2f7` | bg wash end |
| surface-card | `#ffffff` | card, dropdown, keypad |
| surface-subtle | `#f8fafc` | inputs idle, footer |
| surface-accent-subtle | `#f0fdfa` | inputs focused |
| surface-accent-tint | `#ccfbf1` | badge bg, active row |
| border | `#e2e8f0` | card, input idle, keys |
| border-strong | `#cbd5e1` | divider, empty dot |
| border-accent-light | `#99f6e4` | badge border |
| text | `#0f172a` | titles, values |
| text-muted | `#475569` | lede, secondary btn label |
| text-dim | `#64748b` | small labels, subs |
| text-faint | `#94a3b8` | placeholder, counter |
| accent-700 | `#0f766e` | primary teal (brand) |
| accent-900 | `#134e4a` | primary gradient end |
| accent-100 | `#a7f3d0` | active-crane dot |
| info | `#0284c7` | gradient mid |
| violet | `#7c3aed` | gradient end |
| success | `#16a34a` | status dots, delta |
| warn | `#fbbf24` | maintenance dot |
| warn-bg | `#fffbeb` | backspace key |
| warn-border | `#fde68a` | backspace key |
| warn-text | `#d97706` | backspace key text |
| danger-bg | `#fef2f2` | CLR key |
| danger-border | `#fecaca` | CLR key |
| danger-text | `#dc2626` | CLR key text |

### Spacing
4 / 6 / 8 / 10 / 12 / 14 / 16 / 18 / 22 / 28 / 32 / 44 / 64 px (no strict scale — see component-level paddings).

### Radii
8 / 10 / 12 / 14 / 16 / 20 / 999 px.

### Shadows
| Use | Value |
|---|---|
| Card | `0 30px 60px -20px rgba(15,23,42,.18), 0 0 0 1px rgba(15,23,42,.02)` |
| Stats strip | `0 8px 28px rgba(15,23,42,.04)` |
| Dropdown / keypad | `0 20px 40px rgba(15,23,42,.18)` |
| Active crane btn | `0 8px 20px rgba(15,118,110,.3)` |
| Login btn (enabled) | `0 12px 28px rgba(15,118,110,.35)` |
| Icon tile | `0 6px 14px rgba(15,118,110,.35)` |
| Status pill dot halo | `0 0 0 3px <color>33` |

### Typography
- UI font: **Heebo** — weights 400 / 500 / 600 / 700 / 800 / 900. Load from Google Fonts.
- Mono font: **IBM Plex Mono** — weights 400 / 500 / 600 / 700. Load from Google Fonts.
- Use Heebo for all body/UI/labels.
- Use IBM Plex Mono for numeric values, ids, timestamps, build strings, session id.
- RTL layout; `direction: rtl` on the root.

### Transitions
- Focus state: `all .15s`
- Dropdown chevron: `transform .2s`
- PIN cells: `all .12s`
- Login button: `all .2s`

---

## Assets

| File | Source | Notes |
|---|---|---|
| `goldbond-logo.png` | cropped from the existing system screenshot | Replace with the proper SVG/PNG asset from brand before ship. Render at `height: 108px`. |

Icons currently rendered as emoji for speed — replace with proper icons in the target codebase:
- 🔐 card header → lock icon
- 👤 (no longer used after dropdown refactor, but kept in helper component)
- 🛡️ security footer → shield icon
- ▾ chevron — already rendered as a triangle glyph; swap to an icon if desired

---

## Files in this bundle

| File | Purpose |
|---|---|
| `README.md` | This doc |
| `modern-login.jsx` | The React component source (design reference) |
| `goldbond-logo.png` | Logo asset used in the mock |
| `login-preview.html` | Self-contained preview — open in a browser to see the design live, scales to any viewport |
| `design-canvas.jsx` | (dependency of other screens — not needed for login standalone; included for completeness) |

To view the design: open `login-preview.html` in any modern browser.

---

## Notes for the developer

- This is one screen out of a larger RTG operator system. The visual language here (teal primary, Heebo + IBM Plex Mono, card-on-wash, soft shadows, status pill pattern) is shared with the rest of the prototype — reuse the same tokens across all screens.
- PIN is 4 digits to match yard practice; if the real auth system uses a different length, adjust `maxPin` and the cell count together.
- Nothing about this screen assumes a hardware keyboard — build for touch. Keep all hit targets ≥ 44px (most here are 50–62 px).
- The badge "מערכת חדשה · דור 4" is a one-time welcome cue for the rollout. Consider hiding it after a few months or making it a feature-flag.
- "בוקר טוב / צהריים טובים / ערב טוב" should be time-aware if you wire it up — mock currently hardcodes "בוקר טוב".
