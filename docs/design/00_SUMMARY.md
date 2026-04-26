# 00 — Phase 2 Design Report · Consolidated Summary

> **Project:** Goldbond RTG crane-location system modernization
> **Author:** Claude Code (designer)
> **Date:** 2026-04-21
> **Status:** ✅ Phase 2 (New System Design) COMPLETE. Ready to be handed to the implementation team.

---

## 1. One-page overview

The existing RTG system — three .NET Framework 4.8 console listeners, two WinForms UIs (a RTG1-2 build and a Crane-3 fork), and an entangled MSSQL schema where triggers drive UI polling and stored procedures shell-out to OS processes — is replaced with a clean, unified, modern stack:

| Layer | Today | Phase 2 design |
|---|---|---|
| Listener | 3 copy-paste .NET 4.8 binaries + 1 stale TOSService | **`rtg-listener`** — one .NET 8 service, per-crane YAML config, `System.IO.Pipelines` framing, explicit PG transactions |
| Operator UI | `RTGApp.exe` RTG1-2 build + `RTGApp.exe` RTG3 fork (WinForms) | **`rtg-cab`** — one Flutter app, Windows + Android, Hebrew-RTL, offline-first, per-crane via bootstrap config |
| Primary DB | MSSQL 2019 (single `TerminalData` shared with MIS, ForkliftApp, invoicing…) | **PostgreSQL 16** (new `rtg` schema owning the module) |
| Dual-write | — | **Transactional outbox** → MSSQL (preserves legacy shape for MIS/ForkliftApp until they migrate in Phase 3) |
| Control plane | `xp_cmdshell` SPs launching `.exe` files, two watchdogs disagreeing on paths | **`systemd` / Windows Service** supervision; `xp_cmdshell` disabled |
| Auth | 4-digit PIN, group-matched (selected user name ignored), no lockout, credentials in binary | **`rtg-api` + JWT + Argon2id + lockout + rotation**; PIN *bound* to operator identity; no DB credentials leave the server |
| Push-updates | 500 ms UI polling of `TB_Parameters` bits via MSSQL triggers | **PG `LISTEN/NOTIFY` + SignalR WebSocket** — zero baseline polling |

Migration is **crane-by-crane, Crane 3 first** (smallest, most isolated, already forked). Dual-write to MSSQL keeps legacy reports, MIS, and ForkliftApp operational throughout. Every Phase-1 risk has a Phase-2 mitigation in `07_risks.md`.

**Critical external dependency:** ~~the KoneCranes protocol specification (Q-11 confirmed vendor). The design is complete without it, but the listener cannot be coded byte-perfect without either the spec or a live packet capture from a crane.~~
**✅ Resolved 2026-04-21** — the operator delivered the KoneCranes vendor directory (`KoneCranes/RTG/`), including the V40 protocol spec, the CHE and TOS simulators, and the Goldbond-specific network layout. Phase 2 implementation has zero remaining blockers. See [`docs/discovery/10_konecranes_reference.md`](../discovery/10_konecranes_reference.md).

---

## 2. The design documents

All seven Phase-2 documents are in [`docs/design/`](.):

| Doc | Size | Content |
|---|---|---|
| [`00_SUMMARY.md`](00_SUMMARY.md) | (this file) | Executive summary + document map + open items |
| [`01_architecture.md`](01_architecture.md) | Architecture | Component diagram, tech stack per component, deployment topology, network diagram, auth/authz/audit, observability, config management |
| [`02_data_model.md`](02_data_model.md) + [`02_schema.sql`](02_schema.sql) | Data model | 12-table PG schema (executable DDL), legacy→PG mapping (11 sub-mappings), data-ownership matrix, dual-write strategy, bulk + ongoing migration |
| [`03_listener.md`](03_listener.md) | Listener redesign | .NET 8 rationale, project layout, framing with `System.IO.Pipelines`, frame parser state machine, handler contract, resilience (reconnect, backoff, keep-alive, backpressure, disk fallback) |
| [`04_flutter_ui.md`](04_flutter_ui.md) | Cabin UI | 13-screen 1:1 mapping, route structure, the `/yard` design principles, RTL/Hebrew-first, offline-first with write queue, 3 wireframes (login, yard main, container details) |
| [`05_dual_write.md`](05_dual_write.md) | Coexistence | 8 dual-write event types with exact legacy-SQL mirror, failure matrix (10 scenarios), nightly reconciliation, per-event exit criteria |
| [`06_rollout.md`](06_rollout.md) | Rollout plan | 15-week timeline, 12 prerequisites, 13 Go/No-Go gates per cutover, <30min rollback runbook, 3-session operator training, bulk historical-data migration script |
| [`07_risks.md`](07_risks.md) | Risk register | All Phase-1 risks × Phase-2 mitigations + 7 rollout risks + 7 new risks, severity matrix, top-3 for active attention |

---

## 3. Architectural invariants — what the design commits to

These are the non-negotiable properties the implementation team should enforce:

1. **Byte-for-byte wire protocol compatibility with KoneCranes PLCs.** Cranes are not touched. Packet structure, ACK/NAK format, checksum algorithm — all preserved. `03_listener.md` §3.
2. **PG is the source of truth for RTG.** Every RTG write commits to PG first, then asynchronously to MSSQL via outbox. No synchronous cross-DB transactions. `05_dual_write.md` §1.
3. **One process per component, parameterized per crane.** No code fork for Crane 3, no copy-paste triplication for Console1/2/3. `01_architecture.md` §3.
4. **No DB credentials on cabin PCs.** UI talks HTTPS to `rtg-api`; API talks to DB. `04_flutter_ui.md` §10.
5. **No secrets in source or binary.** Env vars + secret store only. `03_listener.md` §6.
6. **No `xp_cmdshell`.** Process lifecycle is handled by systemd / Windows Service. `01_architecture.md` §3 + `06_rollout.md` P-9.
7. **Transactions everywhere writes matter.** The A2/04 PLACE handler writes 5-7 tables in one PG transaction. `03_listener.md` §3.4.
8. **Audit trail reflects reality.** The PIN check is bound to the selected user; `rtg.audit_log` records every state-changing action. `01_architecture.md` §7.
9. **Server-push for live state.** PG `NOTIFY` + SignalR WebSocket drive the UI map. No 500 ms polling. `02_schema.sql` trigger definitions.
10. **Retention is designed in.** `crane_status_history` 90 days, `movement` 2 years, `outbox.FAILED_PERMANENT` 90 days, audit log 7 years. `02_data_model.md` §4.4.

---

## 4. Deployment model (recap)

```
┌─────────────────────────────────────────────────────────────────┐
│   Cranes (PLCs unchanged)                                       │
│   Crane 1 on 192.6.1.x   Crane 2 on 192.6.2.x   Crane 3 on 192.6.3.x
└─────────────┬────────────────┬────────────────┬────────────────┘
              │ TCP 30701       │ TCP 30702      │ TCP 30703
              ▼                 ▼                ▼
┌─────────────────────────────────────────────────────────────────┐
│   Edge host (multi-homed NICs, one per crane subnet)            │
│     rtg-listener (.NET 8)  ·  rtg-outbox-worker                 │
│     supervised by systemd / Windows Service                     │
└──┬──────────────────────────────────────────────────┬───────────┘
   │                                                  │
   │ 5432 (PG)                                        │ 1433 (MSSQL)
   ▼                                                  ▼
┌───────────────────────┐                   ┌────────────────────────┐
│ PostgreSQL 16          │                   │ MSSQL 2019 (unchanged) │
│ rtg_primary schema     │                   │ TerminalData           │
│ LISTEN/NOTIFY push     │                   │ RG_*, TB_*, CO_*, …    │
└──────────┬─────────────┘                   └──────────┬─────────────┘
           │                                            │
           │ (via rtg-api)                              │ (legacy consumers)
           │                                            │
           ▼                                            ▼
  rtg-api (.NET 8 + SignalR)                   MIS · ForkliftApp ·
           │                                    legacy reports
           │ HTTPS + WebSocket (JWT)
           ▼
  Cabin PCs with rtg-cab (Flutter Windows)
  (or rugged Android tablets)
```

---

## 5. Roadmap & milestones (15 weeks)

```
Week    Milestone
  1-2   Impl setup: repos, CI, stage env, PG stood up
  3     KoneCranes spec obtained; final listener tweaks
  4     rtg-listener feature-complete; unit+integration tests pass
  5     rtg-api + rtg-cab MVP; stage environment running
  5     Bulk historical migration dry-run in stage
  6     ━━━━━━━━━━━  PILOT CRANE 3 CUTOVER  ━━━━━━━━━━━
  7-8   Crane 3 bake; ops triage; operator feedback
  9     Crane 1 cutover
 10-11  Crane 1 bake
 12     Crane 2 cutover
 13-14  Crane 2 bake
 15+    Begin dual-write retirement flag-by-flag → hand off to Phase 3
```

---

## 6. Open questions carried forward

From Phase 1 and Phase 2 combined:

| # | Title | Severity | Status |
|---|---|---|---|
| Q-11 | KoneCranes protocol spec | 🔴 Blocker | ✅ **RESOLVED 2026-04-21** — vendor delivered full documentation + simulators. See `10_konecranes_reference.md` |
| Q-47 | Which IP does each crane's PLC target? | 🟠 High | Defer to pre-cutover audit |
| Q-49 | Why does TOSService exist? | 🟡 | Defer — retire after cutover regardless |
| Q-50 | NIC strategy (VLAN vs physical) | 🟡 | Ops preference |
| Q-51 | Existing observability stack | 🟡 | Ops input |
| Q-52 | Container / orchestration platform | 🟡 | Ops input |
| Q-53 | Existing CI/CD | 🟡 | Azure DevOps likely; confirm |
| Q-54 | DB licensing | 🟡 | Finance |
| Q-56-58 | PG collation, retention horizons | 🟡 / 🟢 | Ops confirm |
| Q-59 | CO_Containers.LocationCode contention | 🟠 | ForkliftApp owner |
| Q-60 | Inbound checksum layout (tied to Q-11) | 🟡 | Improved — spec envelope defines `ZZ` at end of frame; exact bytes resolvable via 10-min `chesimu.exe` observation |
| Q-61 | Listener+outbox: 1 process or 2? | 🟡 | Design decision: 1 binary, mode flag |
| Q-62 | Windows service vs Linux daemon | 🟢 | Ops preference |
| Q-63 | A3-pending broadcast on reconnect? | 🟡 | Implement as config flag |
| Q-64-68 | UI: sound, kiosk, scanner, shift length, bulk-empty | 🟡 | Operator / supervisor input |
| Q-69 | Retire RTG3 "bulk mark empty"? | 🟡 | Yes, automatic detection |
| Q-70 | ForkliftApp writes TB_Location? | 🟠 High | ForkliftApp owner interview |
| Q-71 | When to retire PG-side legacy Message computation | 🟡 | Post-all-cranes-stable |
| Q-72-76 | Retention, reconciliation schedule, rollback auth, cleanup | 🟡 / 🟢 | Ops confirm |

**✅ No remaining 🔴 blockers as of 2026-04-21.** All listed 🟡/🟢 questions are either design-phase-acceptable or resolvable inside the implementation phase.

---

## 7. Hand-off to implementation

The implementation team receives:

- **9 discovery documents** (`docs/discovery/`) — everything known about the legacy system.
- **8 design documents** (`docs/design/`) — everything decided about the new system.
- **1 executable schema file** (`02_schema.sql`) — creates the PG database.
- **This summary** as the north star.

Recommended first-week of implementation *(updated 2026-04-21 — step 3 dropped, replaced with simulator bring-up)*:

1. **Stand up the stage PG** and run `02_schema.sql`. Confirm it applies cleanly. (~0.5 day)
2. **Fetch fresh TFS** and run ILSpy diff against the deployed binaries. Document the drift list. (~1 day)
3. **Install the KoneCranes CHE simulator** (`KcCheSimulatorInstaller.exe`) on a dev machine; send a few A1 / A2 packets; capture the bytes; confirm the `ZZ` end-of-frame checksum layout. (~0.5 day)
4. **Create the 3 repos** (`rtg-listener`, `rtg-api`, `rtg-cab`) with skeleton CI pipelines. (~1 day)
5. **Read every design doc, front to back**, and file issues for anything unclear — before writing production code. (~1 day)

---

## 8. Explicit end of Phase 2

> **Phase 2 complete.**
>
> The design is ready for implementation.
>
> Proceed to Phase 3 (implementation + rollout) under a separate mandate from the operator / product owner.
>
> The 10 architectural invariants in §3 and the risk register in `07_risks.md` should be treated as the contract between design and implementation — any deviation in implementation should come back for design review.
>
> The remaining open questions (§6) should be tracked in the project issue tracker and answered before they block. ~~Q-11 (KoneCranes spec) is the sole 🔴 blocker for starting listener coding.~~ **Updated 2026-04-21: Q-11 resolved; zero remaining 🔴 blockers. Listener coding can start week 1.**

---

*— End of Phase 2 Design Report —*
