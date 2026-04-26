# Handoff: RTG Crane Operator System

## Overview

Design spec for a container-management / crane-operator touchscreen app used on **RTG (Rubber-Tyred Gantry) cranes** in a logistics yard. The operator moves containers between ground stacks, trucks, and storage rows while the crane is live — so the UI has to be **simple, unambiguous, and glanceable**.

Three visual directions were produced; the customer selected **Modern (variant 2)** as the direction. Conservative and Bold are included as reference.

---

## About the Design Files

The files in this bundle are **design references created in HTML + React (JSX)** — interactive mockups that demonstrate the intended look, layout, information hierarchy, copy, and interactions.

**They are NOT production code.** The product is being built in **Flutter** (to support multiple hardware targets). Your job is to re-implement each screen as Flutter widgets, using the app's existing routing, state management, theming, and localization. The HTML exists purely as a pixel-accurate visual spec.

Open `RTG Crane Operator.html` in any modern browser (Chrome / Edge) for the live reference. Every screen is laid out on a pan/zoom canvas — double-click any artboard to expand it.

---

## Fidelity

**High-fidelity (hifi).** Every screen in the Modern variant has final:
- Color tokens (exact hex)
- Typography (Heebo + IBM Plex Mono)
- Spacing, border radius, shadows
- RTL layout, Hebrew copy
- Placement of GPS/PLC telemetry
- Interactive states (buttons, selection, hover-equivalents)

Recreate these pixel-faithfully in Flutter using the existing design system's widgets where they cover the same primitive, and custom-built widgets elsewhere.

---

## Target Hardware

- **Display:** 21-inch industrial touchscreen, **native 1920 × 1080**, landscape.
- **Input:** Resistive or capacitive touch + possibly hardware buttons (keep buttons ≥ 56 px tall).
- **Environment:** Inside a crane cabin, often with bright sunlight glare → the **Bold** variant was explored specifically for that case but the customer chose **Modern**. Consider a runtime "high-contrast" toggle that re-skins Modern with heavier ink/borders if sunlight readability becomes an issue later.
- **Language:** Hebrew, **RTL**.

---

## Global Design Tokens (Modern)

### Color
| Token | Value | Use |
|---|---|---|
| `teal` | `#0f766e` | Primary (actions, active tab, links) |
| `tealDeep` | `#115e59` | Primary hover/active |
| `tealSoft` | `#ccfbf1` | Selected row background, success surface |
| `amber` | `#d97706` | Source/warning/pending |
| `green` | `#16a34a` | Success, confirm, ok |
| `red` | `#dc2626` | Destructive, error |
| `violet` | `#7c3aed` | Block / gush category accent |
| `surface` | `#f6f8fa` | App background |
| `card` | `#ffffff` | Card / panel surface |
| `ink` | `#0f172a` | Primary text |
| `muted` | `#64748b` | Secondary text, captions |
| `line` | `#e2e8f0` | Dividers, borders |

Stack-height palette (heatmap, used on yard grid tiles):
- 6/6 full → `#fef2f2` / `#b91c1c`
- 5/6 → `#fff7ed` / `#c2410c`
- 4/6 → `#fefce8` / `#a16207`
- 3/6 → `#f0fdf4` / `#15803d`
- 2/6 → `#ecfdf5` / `#047857`
- 1/6 → `#eff6ff` / `#1d4ed8`
- 0 (empty) → `#fff` / `#cbd5e1`

Source tile: amber 135° gradient, 2.5 px solid amber border.
Target tile: teal 135° gradient, 2.5 px **dashed** teal border.

### Typography
- **Primary:** `Heebo` (400, 500, 600, 700, 800, 900) — Hebrew-native.
- **Mono / numeric:** `IBM Plex Mono` — container IDs, coordinates, times.
- Sizes in use: 11 / 12 / 13 / 14 / 15 / 16 / 18 / 20 / 22 / 24 / 28 / 30 / 36 / 44 px. Prefer the ones used in the screens — do not invent new sizes.

### Radii
- Chips / small controls: 8 px
- Inputs: 10 px
- Cards: 12 px
- Big cards / screens: 16 px

### Shadow (card elevation)
- Base: `0 1px 3px rgba(15,23,42,.04)`
- Elevated (hover / focus / top-bar): `0 1px 3px rgba(15,23,42,.05)`
- Primary button: `0 6px 14px <color>55`

### Spacing
- 4 / 6 / 8 / 10 / 12 / 14 / 16 / 18 / 20 / 24 px
- Touch target **minimum 44 px**; primary action buttons 60–72 px.

---

## Screens (Modern variant — the one to build)

For each screen, open the corresponding HTML mockup and the numbered artboard on the canvas. JSX source files are named in parentheses.

### M1 — Main yard screen + GPS HUD (`modern-main.jsx` / `modern.jsx`)
**Purpose:** The operator's home base. Shows the current job (source tile → target tile), yard grid with color-coded stack heights, and live telemetry.
- Top bar: crane ID, block, current position, **GPS (fix type, satellites, accuracy in m)**, PLC status, time/date.
- Grid: 2D map of rows (A–F) × columns (94–106). Tile color encodes stack height (see heatmap). Source tile pulses amber with "מקור" label, target tile is dashed teal with "→h+1" label.
- Bottom: source container panel (left) + transfer arrow + target panel (right), then action bar (אישור / ביטול / משאית / עבודות / תפריט).

### M2 — Main menu (`modern-screens-a.jsx`)
8 action tiles in a 4×2 grid: Empty containers / No-location / Log / Containers-by-block / Update location / Suggested locations / Expected / RTG settings. Each tile has an accent stripe in its category color and a one-line description.

### M3 — Jobs list (`modern-screens-b.jsx`)
Filterable table of all open jobs. Columns: container, size, type, weight, current location, destination, task description, time, action. Tabs filter by block (BOND1 / BOND2 / HGC6). Search input top right.

### M4 — Numpad + container picker (`modern-screens-a.jsx`)
Two-column: (left) big numeric keypad to enter a block/gush code, (right) filtered list of containers matching that code. Backspace is amber, Clear is red; Confirm is a 68 px green button.

### M5 — Container detail + deal containers (`modern-screens-a.jsx`)
Left: list of all containers in the same deal (shipping order). Right: full detail panel for the selected container — ID (hero treatment on teal gradient), size, type, handling code, location, weight, capacity, customer, risk, job, release info. Big green "בחר מכולה זו" action.

### M6 — Truck load/unload (`modern-screens-b.jsx`)
Top: truck bays with driver/plate/wait time. Middle: "Unload" table (amber header, down arrow) — containers coming **off** trucks. Bottom: "Load" table (teal header, up arrow) — containers going **onto** trucks. Operator picks which to move next.

### M7 — Suggested locations (`modern-screens-c.jsx`)
3×2 grid of ranked suggestions for where to place the active container. Card #1 highlighted as "מומלץ" with teal gradient. Each card shows: location code (hero mono), row/col/stack chips, reason text, a score bar (0–100), and a "select" button.

### M8 — Expected containers (`modern-screens-c.jsx`)
Timeline-style list of containers arriving in the next hour. Each row: ETA badge (highlighted if at gate), container ID, line, customer, size, type, truck bay, status pill (בדרך / הגיע לשער / צפוי).

### M9 — Empty containers + No-location (`modern-screens-c.jsx`)
Split screen. Left: list of empty containers with days-in-yard (amber >7 days, red >14). Right: "No-location" — containers the system couldn't auto-locate (GPS failed, auto-ID failed, unauthorized drop). Each has an "Update location" action.

### M10 — Movements log (`modern-screens-b.jsx`)
Chronological history of every container move today. One row per move: time → container ID → from-chip → arrow → to-chip → operator → status pill (completed/cancelled). Filter tabs: today / yesterday / this week / custom range.

### M11 — RTG settings + GPS panel (`modern-screens-c.jsx`)
Diagnostics screen. Two cards on the left: "Connections" (PLC, GPS fix, satellites, accuracy, HMI, TOS server — each as a colored metric tile) and "Current position" (lat/long/altitude/heading/speed/last update). Right column: action list — recalibrate GPS, test PLC, sync with TOS, maintenance mode, reboot. Bottom: version/build.

---

## GPS Integration (important)

GPS is not just a diagnostic — it's part of the core workflow:
- **Top bar on every Modern screen** surfaces: fix type (RTK / DGPS / 2D / NO FIX), satellite count, accuracy in meters. When RTK is lost, show a subtle amber pulse on the GPS chip.
- **M11 settings screen** has a dedicated "Current position" panel with live lat/long/altitude/heading/speed.
- **M9 no-location list** calls out "GPS לא עודכן" as a specific reason a container lacks a location.
- When placing a container, the crane's measured GPS drop position is what updates the TOS (Terminal Operating System) record — this is why accuracy matters.

In Flutter, wire this to the real GPS/RTK receiver (via platform channel if needed) and update the HUD at ≥1 Hz. Debounce the status chip color (don't flash red for a single-frame dropout).

---

## Conservative variant (C1–C3)

Same workflow as Modern but styled to match older industrial UIs the operators are already used to:
- Higher-density tables
- Grey/navy palette
- Simpler chip and button treatments
- No gradients or large radii

Use as reference only unless the customer changes direction.

---

## Bold variant (B1–B3)

Dark-mode "cockpit" variant explored for extreme-sunlight readability:
- Black background, cyan/amber accents
- Large mono numerics
- HUD-style treatment of telemetry

Not selected. Keep the CSS around in case a high-contrast mode is needed later.

---

## Implementation Notes (Flutter)

- **Routing:** every screen above is a full-screen route; M2 Menu is the hub.
- **State:** the active job (source + target + container metadata) is global app state. Yard grid data, truck bays, jobs list, expected list are all read from the TOS backend.
- **Localization:** all copy is Hebrew RTL. Use `Directionality.of(context)` guards and flutter_localizations properly. Mono numbers (container IDs, GPS coords) should render **LTR inside RTL context**.
- **Fonts:** bundle Heebo + IBM Plex Mono in `pubspec.yaml` assets.
- **Touch targets:** min 44 px; primary actions 60+ px.
- **Theming:** build a `CraneTheme` class from the tokens above; don't hard-code colors in widgets.

---

## Files in this bundle

- `RTG Crane Operator.html` — the live canvas with all 17 screens across 3 variants. Open in a browser.
- `RTG Crane Operator-print.html` — print-ready version. Open in Chrome → Print → Save as PDF (A4 landscape, no margins).
- `data.jsx` — shared mock data (crane, grid, active job, containers, jobs, trucks).
- `design-canvas.jsx` — the pan/zoom layout host (design tool only, not part of the product).
- `conservative.jsx` — variant 1 (all 3 screens).
- `modern.jsx` + `modern-main.jsx` — Modern main yard screen + shared primitives.
- `modern-core.jsx` — Modern tokens (colors, styles, top bar, grid tiles).
- `modern-screens-a.jsx` — Menu, Numpad, Container detail.
- `modern-screens-b.jsx` — Jobs list, Truck load/unload, Movements log.
- `modern-screens-c.jsx` — Suggested, Expected, Empty+No-location, RTG settings.
- `bold.jsx` — variant 3 (all 3 screens).

## Assets

No bitmap assets yet — all iconography is emoji or CSS. Replace with the design system's real icon set in the Flutter build (suggestion: **Material Symbols Rounded** or the customer's existing Android icon set).

---

## Questions for the dev team

If anything is ambiguous, the source of truth is the **rendered HTML**, not this README. When in doubt, open `RTG Crane Operator.html`, pick the Modern row, and measure.
