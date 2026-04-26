# 07 — Risk Register & Mitigations

> **Phase 2 — Step 2.7** · Designer: Claude Code · Date: 2026-04-21
> **Scope:** takes every risk enumerated in Phase 1 and the rollout-specific risks from `06_rollout.md`, and maps each to the concrete Phase-2 mitigation. Also flags residual risk per item.

---

## 1. Severity scale

- **🔴 Critical** — threatens delivery, data integrity, or safety.
- **🟠 High** — material impact on timeline or quality; must be actively managed.
- **🟡 Medium** — plan for it; routine handling.
- **🟢 Low** — accept or defer.

---

## 2. Top 10 risks carried forward from Phase 1

### R1 — Vendor protocol spec not available (KoneCranes)

- **Original severity:** 🔴 Critical
- **Residual after mitigation:** 🟢 Low *(updated 2026-04-21 after operator delivered the `KoneCranes/` directory)*
- **Status:** ✅ **RESOLVED.** The vendor delivered the complete protocol specification, reference simulators, and Goldbond-specific network layout. See `docs/discovery/10_konecranes_reference.md`.
- **What we have:**
  - `RTG-TD-Konecranes TOS interface_V40.pdf` — full spec covering all message types (A1/A2/A3/A4, B1/B2/B3/B4/B5), byte offsets, value enums, handshake flow.
  - `chesimu.exe` / `tossimu.exe` — vendor-authored simulators for both sides. Integration tests for `rtg-listener` run against these directly.
  - Network layout: cranes are BoxHunter G2036/G2037, NATted via mGuard 4004, connect outbound to TOS on customer network.
- **Remaining uncertainty:** exact byte layout of the `ZZ` end-of-frame (likely checksum). Resolvable via a 10-minute `chesimu.exe` session capturing packets. Until confirmed, ship `validate_inbound_checksum: false` (listener default).
- **Owner (closed):** Goldbond tech lead.

### R2 — Secrets exposed in deployed binaries (`malgezot/12345678`, `z3334606*`)

- **Original severity:** 🔴 Critical
- **Residual:** 🟢 Low
- **Mitigations:**
  - Prerequisite P-8 of rollout: rotate both credentials immediately; disable `malgezot` login on SQL01; confirm `z3334606*` is not a valid login.
  - New system uses env-var-loaded connection strings; no secret in source, in config file on disk, or in binary.
  - JWT for the cabin UI; no DB credential ever leaves the server.
  - CI hook: `grep`-based leak detector on every commit (reject pushes that match password-like regex).
- **Owner:** DBA + security.
- **Residual rationale:** after P-8, there's no path by which the legacy credentials can be used. Residual is the usual "future dev puts a secret in source by accident" — handled by CI detector.

### R3 — Source-to-binary drift (TFS snapshot lags deployed)

- **Original severity:** 🔴 Critical (blocked redesign)
- **Residual:** 🟡 Medium
- **Mitigations:**
  - Prerequisite P-1: fresh TFS checkout + ILSpy diff on all 4 .NET Framework binaries.
  - For any drift found, update either the TFS source (if the binary is the right version) or the binary (if the TFS is the right version).
  - Phase 2 design does not rewrite behaviour — it reimplements the *specified* behaviour from `01_-09_` discovery docs. As long as we validate observed-binary-behaviour in stage before cutover, drift is caught.
- **Owner:** dev lead.
- **Residual rationale:** even with fresh source there's always some drift between what's committed and what's running in long-lived systems. The Phase-2 acceptance tests in stage (recorded live-traffic replay) catch behavioural divergence.

### R4 — Write amplification + unbounded log growth (`RG_ErrorLog` 1.6M, `RG_A1_LOG` unknown)

- **Original severity:** 🟠 High
- **Residual:** 🟢 Low
- **Mitigations:**
  - PG side: `rtg.crane_status_history` is `PARTITION BY RANGE (ts)` monthly with 90-day retention. Old partitions auto-drop.
  - `rtg.movement` similarly partitioned with 2-year retention.
  - `rtg.outbox.FAILED_PERMANENT` retained 90 days then archived.
  - Application logs leave the DB entirely — structured JSON to Loki / journald.
  - Legacy `RG_ErrorLog` frozen at cutover; no new rows. Historical data archived to cold storage.
- **Owner:** DBA.

### R5 — Crane-3 fork vs Crane 1+2 mainline

- **Original severity:** 🟠 High
- **Residual:** 🟢 Low
- **Mitigations:**
  - `rtg-listener` is one code path, parameterised per crane via YAML.
  - `rtg-cab` is one Flutter binary, per-crane via `bootstrap.json` config.
  - The per-crane schema differences (V_MapRTGBond3, TB_WorkType.Gold3, etc.) are replaced by a schema where the crane is a first-class parameter. No code-level fork remains in the new system.
  - Crane 3 is the pilot (rollout §1 rationale), so the per-crane-unification is stress-tested first.
- **Owner:** dev lead.

### R6 — Control plane entangled with DB (`xp_cmdshell` SPs; two restart paths)

- **Original severity:** 🟠 High
- **Residual:** 🟢 Low
- **Mitigations:**
  - Prerequisite P-9: disable `xp_cmdshell` on SQL01.
  - `rtg-listener` supervised by systemd (Linux) or Windows Service.
  - UI's "Reconnect" button becomes an authenticated HTTPS call to `rtg-api` → `systemctl restart rtg-listener@{crane_id}`.
  - Legacy SPs `KillToss{n}` / `RunEnconsoleRTG{n}` are dropped from the DB at end of Phase 2 (rollback keeps them alive until that point).
- **Owner:** DBA + ops.

### R7 — No TCP framing + no inbound checksum validation

- **Original severity:** 🟠 High
- **Residual:** 🟡 Medium
- **Mitigations:**
  - `System.IO.Pipelines`-based Framer in `rtg-listener` (`03_listener.md` §3.1).
  - `validate_inbound_checksum` config flag — toggled on once KoneCranes spec arrives.
  - Unit tests for Framer cover every edge case from `07_edge_cases.md` #1-#6.
- **Residual rationale:** until the checksum offset is known, we can't validate inbound integrity. We can detect framing failures and short packets, which is the bigger problem. Checksum validation is the cherry on top.

### R8 — No transactions in multi-statement flows (PLACE handler, etc.)

- **Original severity:** 🟠 High
- **Residual:** 🟢 Low
- **Mitigations:**
  - Every PG write handler wraps its multi-statement logic in `BEGIN/COMMIT` (`03_listener.md` §3.4).
  - Outbox INSERTs are inside the same transaction as the biz-state writes (`05_dual_write.md` §2).
  - The MSSQL dual-write from the outbox-worker uses a local MSSQL transaction too, mirroring the legacy 7-statement block atomically via BEGIN TRAN/COMMIT TRAN.

### R9 — Auth flaws (PIN-group-match, no lockout, audit trail lies)

- **Original severity:** 🟠 High
- **Residual:** 🟢 Low
- **Mitigations:**
  - `rtg-api` `/auth/login` binds PIN to the selected `LoginName` (verify both in the same SELECT + `Argon2id` match).
  - Consecutive-failure counter + lockout (15 min after 5 failures; configurable).
  - `rtg.login_log` records every attempt (success or failure); `rtg.audit_log` records every action.
  - JWT expires after the shift window; idle timeout auto-logout.
  - Mandatory PIN rotation first month + every `pin_rotation_days` thereafter.

### R10 — DB-as-message-bus (`TB_Parameters` polled @ 2 Hz × 3 cabs = 36 q/s)

- **Original severity:** 🟡 Medium
- **Residual:** 🟢 Low
- **Mitigations:**
  - `rtg.crane_status` has `needs_refresh_map` and `container_pick_pending` columns (one bool per crane, not 5-bit TB_Parameters).
  - PG trigger `notify_crane_change` fires `pg_notify` on UPDATE; `rtg-api` subscribes; pushes via SignalR to cabs.
  - Zero baseline DB polling from the UIs.
- **Owner:** dev lead.

---

## 3. Rollout-specific risks (from `06_rollout.md` §9)

### RR-1 — Bulk migration SQL misses the maintenance window

- **Severity:** 🟡 Medium
- **Mitigations:**
  - Time the migration in stage first; cap the window needed.
  - Consider incremental migration (migrate historical data over the week preceding cutover, then just the last day during the window).
  - Scale the PG import: use `COPY` instead of `INSERT`; disable indexes during load; rebuild after.

### RR-2 — Rollback never rehearsed

- **Severity:** 🔴 Critical
- **Mitigations:** P-11 mandatory rehearsal in stage; rollback scripts stored in a runbook; each team member has walked through once.
- **Residual:** 🟢 Low after P-11.

### RR-3 — Operator can't remember / doesn't trust the new UI

- **Severity:** 🟠 High
- **Mitigations:**
  - Three-session training (classroom, shadow, go-live).
  - Laminated quick-reference card in every cab.
  - Hebrew-language on-call hotline during bake.
  - UI designed to be strictly simpler than legacy (§14 of `04_flutter_ui.md`).
- **Residual:** 🟡 Medium (operators sometimes need more than training; budget dev time for 2-week bake triage).

### RR-4 — KoneCranes spec never arrives; cutover goes without it

- **Severity:** 🟠 High
- **Mitigations:**
  - Fallback plan: live packet capture from a working crane (one shift) + manual byte-level analysis.
  - Phase 2 delivers a listener byte-compatible with the legacy; spec is an upgrade, not a prerequisite.
  - Delay cutover if genuine interoperability question arises that can't be resolved without the spec.
- **Residual:** 🟡 Medium.

### RR-5 — Dual-write backlog during compound load

- **Severity:** 🟡 Medium
- **Mitigations:**
  - `rtg-outbox-worker` is horizontally scalable — add instances with `FOR UPDATE SKIP LOCKED` preventing collisions.
  - Alert at 60s lag; page at 10min lag.
  - Pre-compute capacity for worst-case (all 3 cranes at peak) and size the worker fleet accordingly.

### RR-6 — Secrets rotation breaks legacy still running

- **Severity:** 🟡 Medium
- **Mitigations:**
  - Inventory all cabin PCs and listener hosts before rotation.
  - Rotate credentials only AFTER all legacy consumers are confirmed off. Order: cutover the crane first, then when bake confirms new system is stable, rotate the creds that the legacy cabin UI used.
  - For transition, create new credentials (`rtg_listener_user`, `rtg_api_user`) with least-privilege; keep old `malgezot` alive but audit every connection.

### RR-7 — MIS/ForkliftApp drifts from RTG writes undetected

- **Severity:** 🟠 High
- **Mitigations:**
  - Nightly `rtg-reconcile` job with alerts.
  - Metrics on outbox lag and failures.
  - On-call rotation covers both systems.
  - Regular integration meeting with the MIS/ForkliftApp team during Phase 2 bake.

---

## 4. New risks introduced by the new stack

Some are inherent to any redesign — we should name them honestly.

### NR-1 — PostgreSQL operational knowledge at Goldbond

- **Severity:** 🟡 Medium
- **Rationale:** If no one on the team has deep PG operational experience (backups, WAL shipping, replication, partition maintenance), we're adding a new skill requirement.
- **Mitigations:**
  - Managed PG (cloud or appliance) is an option — deferred per Phase 2 budget.
  - Training for 2 DBAs (e.g. certified PostgreSQL courses).
  - `pg_partman` extension for partition management, reducing operational complexity.
  - Phase-2-provided runbooks for common operations (backup restore, vacuum analyze, partition drop).

### NR-2 — Flutter is new to the team

- **Severity:** 🟡 Medium
- **Rationale:** The existing code is C# WinForms. Flutter requires Dart proficiency, Material / Cupertino design familiarity, and a different testing model.
- **Mitigations:**
  - Hire or contract an experienced Flutter developer for the first 3 months.
  - Invest in Dart training for the existing team.
  - Internal code review with the Flutter contractor before merging.
  - Limit the UI surface to what's strictly required (single-page focused screens).

### NR-3 — Dependency on JWT / auth infrastructure

- **Severity:** 🟢 Low
- **Mitigations:** Use proven libraries (`Microsoft.AspNetCore.Authentication.JwtBearer`, `argon2` for hashing). Don't roll our own.

### NR-4 — Added network latency through `rtg-api` layer

- **Severity:** 🟢 Low
- **Rationale:** Legacy UI queries DB directly (~ms). New UI goes through HTTPS + API (~10-30 ms roundtrip).
- **Mitigations:**
  - `rtg-api` is stateless, co-located near DB (same datacenter), HTTPS via TLS session resumption.
  - For live updates (which matter for responsiveness), we use WebSocket, not polling — so latency is a non-issue in the critical path.

### NR-5 — Flutter Windows target maturity

- **Severity:** 🟢 Low
- **Rationale:** Flutter for Windows desktop is Stable since Flutter 2.10 but less battle-tested than mobile.
- **Mitigations:** Stay on the stable channel; avoid bleeding-edge plugins; include a Windows-specific QA pass in every release.

### NR-6 — Two separate persistence layers (PG + MSSQL) to monitor

- **Severity:** 🟡 Medium
- **Mitigations:** Observability (§8 of `01_architecture.md`) covers both sides; reconciliation job (§4 of `05_dual_write.md`) catches drift; dashboards show dual-write health.

### NR-7 — Flutter cabin app updates

- **Severity:** 🟢 Low
- **Mitigations:** Signed MSI for Windows (no browser warning); update path via the server-hinted upgrade banner; MDM for Android if used.

---

## 5. Complete risk summary matrix

| ID | Risk | Original | Residual | Owner | Status |
|---|---|---|---|---|---|
| R1 | KoneCranes protocol spec | 🔴 | 🟢 | GB tech lead | **RESOLVED 2026-04-21** — vendor docs + simulators delivered |
| R2 | Exposed credentials | 🔴 | 🟢 | DBA | to-rotate pre-cutover |
| R3 | Source-to-binary drift | 🔴 | 🟡 | dev lead | decompile diff in P-1 |
| R4 | Write amplification / log growth | 🟠 | 🟢 | DBA | design-resolved |
| R5 | Crane-3 fork | 🟠 | 🟢 | dev lead | design-resolved |
| R6 | `xp_cmdshell` control plane | 🟠 | 🟢 | DBA | P-9 resolves |
| R7 | No TCP framing / checksum | 🟠 | 🟡 | dev lead | design-resolved (framing); pending spec (checksum) |
| R8 | No transactions | 🟠 | 🟢 | dev lead | design-resolved |
| R9 | Auth flaws | 🟠 | 🟢 | dev lead | design-resolved |
| R10 | DB-as-message-bus | 🟡 | 🟢 | dev lead | design-resolved |
| RR-1 | Bulk migration window overrun | 🟡 | 🟢 | DBA | stage timing + COPY |
| RR-2 | Rollback unrehearsed | 🔴 | 🟢 | ops | P-11 mandatory |
| RR-3 | Operator trust | 🟠 | 🟡 | yard supervisor | training + bake |
| RR-4 | Cutover without spec | 🟠 | 🟡 | dev lead | packet-capture fallback |
| RR-5 | Dual-write backlog | 🟡 | 🟢 | ops | horizontal scale |
| RR-6 | Secrets rotation breaks legacy | 🟡 | 🟢 | DBA + ops | order of operations |
| RR-7 | MIS/ForkliftApp drift | 🟠 | 🟢 | dev lead + MIS | reconcile + metrics |
| NR-1 | PG operational knowledge | 🟡 | 🟡 | DBA | training + runbooks |
| NR-2 | Flutter newness | 🟡 | 🟡 | dev lead | contractor + training |
| NR-3 | JWT / auth infra | 🟢 | 🟢 | dev | proven libs |
| NR-4 | Added network latency | 🟢 | 🟢 | dev | WebSocket for live data |
| NR-5 | Flutter Windows maturity | 🟢 | 🟢 | dev | stable channel |
| NR-6 | Dual persistence monitoring | 🟡 | 🟢 | ops | observability |
| NR-7 | Cabin app updates | 🟢 | 🟢 | ops | signed installer |

---

## 6. Top 3 risks requiring active attention

*(Updated 2026-04-21: R1 resolved.)* In order of remaining attention:

1. **RR-3 — Operator adoption.** Technology we control; outcome depends on humans. Needs training discipline and respect for operator feedback. Budget 2 weeks bake per crane.
2. **R3 — Source-to-binary drift.** Needs one dedicated engineer-week (ILSpy + stage traffic replay). Don't skip.
3. **R7 — Inbound checksum validation.** Last protocol uncertainty. Resolvable with 10 minutes of `chesimu.exe` observation; tracked through Phase 2 implementation.

Everything else is routine if we execute the design as written.

---

## 7. Check-in summary

- **All 10 Phase-1 risks have concrete Phase-2 mitigations** mapped to specific docs / prerequisites / ownerships. 8 of 10 downgrade to 🟢 Low residual; 2 (R1, R3, R7) stay 🟡 Medium due to inherent "we don't control everything" factors.
- **7 rollout risks + 7 new design-introduced risks** added to the register, each with a clear owner and mitigation. Only RR-2 (unrehearsed rollback) is 🔴 pre-mitigation; P-11 makes it 🟢.
- **Top-3 for active attention:** vendor spec (external dependency), operator adoption (human factor), source drift (engineering hygiene). Everything else is controlled-by-design.
- **Next:** Step Phase-2 final — consolidated summary `00_SUMMARY.md` for design phase. After that, Phase 2 ends and hand off to implementation.
