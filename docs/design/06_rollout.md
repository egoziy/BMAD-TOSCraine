# 06 — Migration & Rollout Plan

> **Phase 2 — Step 2.6** · Designer: Claude Code · Date: 2026-04-21
> **Scope:** How we get from the current production state to all three cranes running on the new stack, with dual-write active to MSSQL. This is the *operational* plan; implementation detail is in the other design docs.

---

## 1. Strategic choice: Crane 3 as pilot

Crane 3 is picked as the first crane to migrate for four cumulative reasons:

1. **Isolated subnet, isolated code.** Per `08_binaries.md` §2, Crane 3 has its own `RTGApp3.csproj` fork, its own `RTG/Console3/`, its own `V_*RTG3*` views, its own `BOND3` block. A rollback on Crane 3 touches none of the Crane 1-2 infrastructure.
2. **Smaller scope.** Crane 3 is smaller by data volume (fewer containers in BOND3 historically), simpler by PLC config (fewer coordinate edge cases), and has a simpler UI workflow (single block, hardcoded `GOLD3`).
3. **Older binary.** Crane 3's TOSConsole3.exe dates to 2023-09-06 — not rebuilt in Oct 2024 like Crane 1/2. If the legacy has bugs on Crane 3, they're ancient and well-understood.
4. **Goldbond's operational risk is lowest.** Pilot failures on a smaller, slower, more-isolated crane cost less than pilot failures on a Crane-1/2 pair that share work.

After Crane 3 stabilises for ≥ 2 weeks, migrate Crane 1 and Crane 2 together or sequentially.

---

## 2. High-level timeline

```
Week   Activity
  1-2  Phase 2 implementation starts (see §3 below for prerequisites)
  3    KoneCranes protocol spec obtained + reviewed; listener final tweaks
  4    Dev-env end-to-end test with fake crane; soak test
  5    Stage environment on real hardware; bulk-migrate historical data
  6    Pilot Crane 3 cutover (listener + UI in same night)
  7-8  Bake period for Crane 3 (metrics watch, operator feedback, triage)
  9    Crane 1 cutover
 10-11 Bake period for Crane 1
 12    Crane 2 cutover
 13-14 Bake period for Crane 2
 15    Begin turning off first dual-write flags (login, A1)
 16+   Remaining flags retired progressively as MIS reporting catches up
```

Numbered weeks from the start of Phase 2 implementation (not from this design doc).

---

## 3. Implementation prerequisites (before cutting over anything)

The team must finish these before touching production:

| # | Prereq | Owner | Done when |
|---|---|---|---|
| P-1 | Fresh TFS checkout confirming source is current (or decompile diff) | Dev lead + ILSpy pass | Binary-vs-source diff produced; all 5 drift items from `08_binaries.md` §7 either match or explained |
| P-2 | KoneCranes protocol spec obtained (or live packet capture + reverse engineer) | Dev lead + vendor contact | ✅ **SATISFIED 2026-04-21.** Vendor directory delivered: full V40 spec + chesimu/tossimu simulators. See `docs/discovery/10_konecranes_reference.md`. Residual uncertainty (inbound checksum byte layout) is resolved by 10 minutes of `chesimu.exe` observation — not a cutover-blocker. |
| P-3 | `rtg-listener` passing unit + integration + 24h soak tests | Dev team | CI green + soak report signed off |
| P-4 | `rtg-api` passing security review (auth, JWT, rate-limit, lockout, audit) | Dev team + security | Pen-test report signed off |
| P-5 | `rtg-cab` Flutter app — feature-complete against the 13-screen inventory + Hebrew review | Dev team + operator UAT | Operator sign-off on a training cabin |
| P-6 | PG schema deployed to stage; bulk-migrate dry run; reconciliation zero divergence | DBA | `rtg-reconcile --deep` zero diffs |
| P-7 | Dual-write in stage: the outbox worker mirrors 100% of test events without error | Dev team | 1h soak, zero `FAILED_PERMANENT` rows |
| P-8 | Credentials rotated: new `rtg_listener_user` / `rtg_api_user` SQL logins; old `malgezot` / `z3334606*` disabled | DBA + security | Login table verified; legacy binaries tested *not* to be able to connect using old credentials |
| P-9 | `xp_cmdshell` disabled on SQL01 | DBA | `sp_configure 'xp_cmdshell', 0; RECONFIGURE;` and confirmed via `sys.configurations` |
| P-10 | Observability stack live with required dashboards + on-call pager | Ops | Dashboards + alert routing tested |
| P-11 | Rollback procedure rehearsed end-to-end in stage | Ops + dev | Recorded run-through |
| P-12 | Operator training delivered (see §7) | Yard supervisor | Sign-off from first crane's operators |

**None of these can be skipped.** A cutover without P-2 (spec) could break the listener silently; without P-11 (rollback rehearsal) a botched cutover is a production incident.

---

## 4. Per-crane cutover runbook

Each crane follows the same ordered sequence. **For Crane 3** it takes ~4 hours of coordinated work including rollback-if-needed. For Cranes 1-2 ~3 hours (easier because the process is known).

### 4.1 Pre-cutover (D-1, day before)

- [ ] Verify stage mirror of the crane has been running dual-write for ≥ 7 days without divergence.
- [ ] Freeze operator schedule: announce maintenance window; assign a supervisor on-call.
- [ ] Take full MSSQL backup (`BACKUP DATABASE TerminalData TO DISK=...`).
- [ ] Snapshot the PG DB (`pg_dump` of `rtg` schema).
- [ ] Confirm the rollback binaries are on the listener host at their expected paths (`E:\RTG\Console3\TOSConsole3.exe`, `C:\RTG\RTG3\TOSConsole3.exe`, `RTGApp.exe` on cabin PC).
- [ ] Confirm the new binaries are on the listener host at their expected paths and signed.
- [ ] Dry-run the cutover command sequence in stage one last time.

### 4.2 Cutover (D-0, window typically 22:00-02:00)

```
T-00:00  Announce start to operators; yard pauses work on Crane N
T-00:05  Stop legacy:
           $ systemctl stop rtg-listener         # (wait — new one not running yet, no-op)
           $ taskkill /F /IM TOSConsoleN.exe     # or equivalent
           $ pause ConsolesReRun via Task Scheduler disable
           # If crane 3: also kill TOSService.exe if it binds port 30703 (it doesn't, but check)
T-00:10  Verify legacy off:  nothing listening on 30703; no TOSConsole3.exe in process list
T-00:15  Drain legacy outbox:  last time the old RG_B3.PickDate etc. are written via the legacy listener — let any in-flight jobs complete
T-00:20  Reconfigure listener host firewall if needed (shouldn't be needed: port stays same)
T-00:25  Bring up new listener in single-crane mode:
           $ export RTG_PG_CONN=...
           $ export RTG_MSSQL_CONN=...
           $ systemctl start rtg-listener@crane3      # or Windows service equivalent
T-00:30  Verify:
           - health endpoint 127.0.0.1:9099/health/ready returns 200
           - port 30703 is LISTEN
           - crane 3 PLC reconnects automatically within 30 seconds (its TCP client retries)
           - first A1 packets appear in rtg.crane_status
           - outbox rows being emitted and drained (outbox_lag_seconds < 5)
T-00:45  Cabin-PC UI switch on Crane 3:
           - stop RTGApp.exe (close app)
           - uninstall RTGApp.exe via Programs & Features
           - install rtg-cab.msi
           - launch rtg-cab; login as a supervisor account
           - verify: yard map renders, live status updates, container detail opens
T-01:00  Operator smoke test with the supervisor:
           - submit a test job (pick from BOND3 bay, place back)
           - verify the crane physically executes
           - verify PG rtg.job state progresses PENDING → SENT → ACKED → PICKED → PLACED
           - verify MSSQL RG_B3 mirror gets PickDate/PlaceDate via outbox
           - verify rtg.movement row and RG_Shifting mirror
T-01:30  Go / No-Go decision point.
           - Go: operators return to work with rtg-cab; supervisors monitor dashboards
           - No-Go: initiate rollback (§4.4)
T-02:00  Hand off to on-call; cutover window closes
```

### 4.3 Post-cutover (D+1 .. D+14)

- **D+1 morning:** supervisor reviews overnight metrics; collects operator feedback.
- **D+2..D+7:** daily 30-min standup to triage any issues; no code changes unless critical.
- **D+7:** bake-complete review. Go / No-Go for proceeding to the next crane.

### 4.4 Rollback procedure (must be < 30 minutes)

```
R+00:00  Announce rollback to operators.
R+00:02  Stop new:
           $ systemctl stop rtg-listener
R+00:05  Reinstall legacy RTGApp.exe on cabin PC (MSI from rollback bundle)
R+00:10  Start legacy listener:
           $ C:\RTG\RTG3\TOSConsole3.exe   # from DB server per legacy pattern
           $ E:\RTG\Console3\TOSConsole3.exe   # OR from edge host, whichever was live
R+00:15  Crane 3 PLC reconnects
R+00:20  Operators can log in with legacy RTGApp.exe
R+00:25  Smoke test a test job
R+00:30  Rollback complete; post-mortem planned
```

**Data forensics after rollback:** PG state up to the rollback moment is preserved for post-mortem. Any job that was in-flight at rollback-start may show up in both PG (as whatever state) and MSSQL RG_B3 (as whatever state the legacy listener wrote). Operators double-check those jobs manually before resuming.

---

## 5. Go / No-Go checklist per crane

Hard gates. Every item must be green before declaring the cutover successful.

| # | Check | How |
|---|---|---|
| G-01 | All 3 listener ports bound on the edge host with the new process | `ss -ltn` shows `0.0.0.0:3070N LISTEN` owned by Rtg.Listener PID |
| G-02 | Crane N's PLC is connected | `netstat -an` shows `ESTABLISHED` between edge host and the crane's subnet |
| G-03 | `rtg.crane_status.last_packet_at` advancing within 2s | PG query live in dashboard |
| G-04 | No `rtg_pg_write_errors_total` increments in last 15 min | Prometheus query |
| G-05 | Outbox is empty / draining | `rtg_outbox_pending_total < 5` and `rtg_outbox_lag_seconds < 5` |
| G-06 | MSSQL `RG_A1 WHERE CHE='GOLD{n}'` updated in last 2s | direct SQL |
| G-07 | A successful end-to-end pick-and-place cycle recorded with correct state transitions | Both PG and MSSQL consistent |
| G-08 | Cabin UI renders the live status strip with ONLINE + current position | Visual verification |
| G-09 | Cabin UI's offline write queue is empty | `SELECT COUNT(*) FROM local_write_queue WHERE status='pending'` inside the app |
| G-10 | Supervisor can log in, view audit log for the cutover window | Visual |
| G-11 | Operator successfully logged in using their new hashed PIN | Visible in `rtg.login_log` + `rtg.session` |
| G-12 | No alerts firing | Observability dashboard |
| G-13 | Rollback binaries verified operational in hot-standby (touched but not run in last test) | File timestamps + MD5 verified |

**G-04, G-05, G-06** are the "live-fire" checks. **G-07** is the functional verification. The others are infrastructure.

---

## 6. Dual-write retirement timeline

After all three cranes are live and stable (week 14+):

| Week | Event type flag to flip OFF |
|---|---|
| 15 | `login_recorded` (PG = `rtg.session`; legacy reports rewritten to read PG) |
| 16 | `a1_received` (position data; no legacy live reader) |
| 17-22 | (continuous reconciliation; observe for issues) |
| 23 | `job_created` — after confirming no scenario still requires falling back to legacy TOSConsole*.exe |
| 24 | `a3_cancel` |
| 25 | `a2_pick` |
| 26 | `a2_place` |
| 27+ | `location_manual_edit`, `container_forklift_update` — only when ForkliftApp migrates or is retired (Phase 3; outside our scope) |

Each flag flip runs for a full week in "shadow mode" first (mirror still happens; reports run from PG; compare) before turning off in earnest. This is in addition to the §5 exit criteria in `05_dual_write.md`.

---

## 7. Operator training plan

**The operators are not technical.** Training has to be pragmatic, on-cab, and short.

### 7.1 Training structure

**Session 1 — classroom (1 hour, per crane's operator group)**
- Walkthrough of the new login screen (PIN unchanged for first 30 days).
- Walkthrough of the new map + submit-job UI.
- Hands-on with a training cabin PC connected to stage.
- Q&A.

**Session 2 — on-cab shadow (1 shift per operator)**
- Operator does their normal shift using the old UI.
- A trainer (yard supervisor / dev) observes and shows how the new UI handles the same tasks.
- Operator tries the new UI in parallel (read-only mode during this session).

**Session 3 — go-live** — after cutover, trainer stays on-call for the first two days.

### 7.2 What to emphasize

- **"Look and feel":** things they will notice immediately:
  - Screen is more spacious (larger fonts, more whitespace).
  - Submit button is much bigger.
  - Status strip shows connection health **in color**.
  - An explicit "Logout" button exists now.
- **PIN policy:** within 30 days, you will be asked to change your PIN. The system will lead you through it.
- **What to do if "המנוף לא מקוון"** (crane offline): wait, check the network, use the Reconnect button if it appears.
- **What to do if it crashes:** restart the app (double-click shortcut). Their session will pick up where they left off. Jobs they submitted but hadn't reached the crane are queued and will go out on reconnect.
- **Reporting bugs:** contact the supervisor; the supervisor has a direct channel to the dev team during bake.

### 7.3 Training materials

- **Printed quick-reference card** in Hebrew, to be laminated and placed in the cab. Shows the 4 most common actions.
- **Short video (5 min)** of a supervisor performing a shift, in Hebrew.
- **24/7 Hebrew helpline** during bake weeks.

### 7.4 Sample quick-reference card content

```
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
  RTG Crane System — הוראות מהירות
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

1. כניסה
   - הזן שם משתמש
   - הזן קוד 4 ספרות
   - לחץ "כניסה"

2. שליחת משימה
   - הקש על תא מלא → "מ"
   - הקש על תא ריק → "ל"
   - לחץ "שלח"

3. ביטול משימה
   - לחץ "בטל" על המשימה בתחתית

4. סטטוס המנוף
   ● מקוון  — עובד כרגיל
   ⚠ איטי    — נתונים לא עדכניים; המתן
   ● נפל     — לחץ "חבר מחדש"

5. בעיות?
   טל' תמיכה בעברית:  XXX-XXX-XXXX
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
```

### 7.5 Training success criteria

- Each operator completes at least one live pick-and-place job in the training cabin before go-live.
- Each operator successfully logs in and out with the new PIN flow.
- Each operator can identify "crane offline" state and respond.
- Signed acknowledgment from the yard supervisor that the team is ready.

---

## 8. Bulk historical-data migration procedure

This happens ONCE, in the maintenance window before Crane 3 cutover.

### 8.1 Source selection

From MSSQL `TerminalData`, export:

| MSSQL table | Rows | Target |
|---|---|---|
| `RG_A1` | 3 | `rtg.crane_status` seed (merge by CHE) |
| `RG_B3` | 475 | `rtg.job` (history) — restrict to last 90 days for MVP |
| `RG_Log` | 31 654 | `rtg.session` (started rows only; `ended_at` NULL) |
| `RG_Shifting` | 503 414 | `rtg.movement` — last 2 years (most useful for reports) |
| `RG_ColDG` | 84 | `rtg.dangerous_goods_column` |
| `TB_Location` RTG subset | 35 174 | `rtg.location` |
| `TB_RecommendedLocation` | 80 | `rtg.recommended_location` |
| `HR_Emp` WHERE (exists in SC_Users UserGroupCode=22) | ~100 | `rtg.operator` |
| `TB_Parameters` (RTG bits only) | 1 row | `rtg.crane_status` flags + `rtg.config_kv` |

Not migrated (frozen):
- `RG_ErrorLog` (1.6M) — archived.
- `RG_A1_LOG` — archived.
- All shared MIS / ForkliftApp tables — stay in MSSQL.

### 8.2 Migration script structure

Pseudocode (real script is Python + psycopg + pyodbc or similar):

```python
# migrate_bulk.py
# Usage: migrate_bulk.py --mssql "..." --pg "..." --cutoff-date 2024-04-01

sql_source = {
    'operators': """
        SELECT e.EmpID, e.LoginName, e.EmpName, e.UserPinCode, e.isActive,
               e.DateOfPinCodeUpdate, s.UserGroupCode
        FROM HR_Emp e JOIN SC_Users s ON e.EmpID = s.EmpID
        WHERE s.UserGroupCode = 22""",
    'locations': """
        SELECT BlocCode, LocationCode, Container, CHE, Active, Empty, Special, Terminal
        FROM TB_Location
        WHERE BlocCode IN ('BOND1','BOND2','BOND3')""",
    # … one entry per table
}

def insert_operators(rows):
    for r in rows:
        pin4 = str(r['UserPinCode']).zfill(4) if r['UserPinCode'] else None
        pin_hash = argon2_hash(pin4) if pin4 else None
        pg.execute("""
            INSERT INTO rtg.operator (emp_id, login_name, full_name, pin_hash,
                pin_set_at, pin_must_change_after, active)
            VALUES (%s, %s, %s, %s, %s, now() + interval '30 days', %s)
            ON CONFLICT (emp_id) DO NOTHING
        """, (r['EmpID'], r['LoginName'], r['EmpName'], pin_hash,
              r['DateOfPinCodeUpdate'], r['isActive']))

# Similar functions for each table …

def main():
    assert_schema_version(pg)
    with pg.transaction():
        for entity in ['operators', 'locations', 'recommended', 'col_dg',
                       'jobs', 'shifting', 'sessions']:
            rows = mssql.execute(sql_source[entity])
            globals()[f'insert_{entity}'](rows)
        verify_counts(pg, mssql)
    print("MIGRATION OK")

if __name__ == '__main__':
    main()
```

### 8.3 Validation after bulk migration

| Check | Expected |
|---|---|
| `SELECT COUNT(*) FROM rtg.operator` | = `SELECT COUNT(*) FROM HR_Emp e JOIN SC_Users s WHERE s.UserGroupCode=22` |
| `SELECT COUNT(*) FROM rtg.location` | = `SELECT COUNT(*) FROM TB_Location WHERE BlocCode IN ('BOND1','BOND2','BOND3')` |
| `SELECT COUNT(*) FROM rtg.movement` since cutoff | = `SELECT COUNT(*) FROM RG_Shifting` since cutoff |
| All PG enum casts succeed | zero "invalid input value for enum" errors |
| No orphan FK violations | zero |

### 8.4 If migration fails

Rollback is trivial (truncate PG `rtg` schema; MSSQL untouched). Re-run after fixing.

---

## 9. Risk during rollout

The big ones specific to rollout (not to the system design itself):

| # | Risk | Mitigation |
|---|---|---|
| RR-1 | Bulk migration SQL takes too long and misses the window | Run it in stage first; time it; scale PG if needed |
| RR-2 | Rollback is needed but rollback procedure has never been rehearsed | P-11 enforces rehearsal |
| RR-3 | Operator can't remember / doesn't trust the new UI | Training; laminated card; on-call during bake |
| RR-4 | KoneCranes spec never arrives; we cutover without it and discover protocol bug | Delay cutover until spec is in hand OR do live packet capture + reverse-engineer the missing fields |
| RR-5 | Dual-write falls behind during the 3rd crane's cutover due to compound load | Scale outbox worker to 2 instances; add capacity |
| RR-6 | Secrets rotation breaks legacy `RTGApp.exe` that someone didn't notice was still running | Inventory all cabin PCs before rotating credentials |
| RR-7 | MIS / ForkliftApp begins to drift from RTG writes and nobody notices | Nightly reconciliation with alerts |

These are added to `07_risks.md`.

---

## 10. Acceptance tests per phase

### 10.1 Stage acceptance (before Crane 3 cutover)

- [ ] Fake crane (Python script reproducing `??` packets) can drive the full cycle.
- [ ] Simulated network drop → reconnect works.
- [ ] Simulated PG down → listener buffers to disk; replays cleanly.
- [ ] Simulated MSSQL down → outbox backlog grows; replays cleanly.
- [ ] 24-hour soak test with synthetic load — no memory leak, no stuck connections.
- [ ] Cabin UI survives connectivity loss + resume without data loss.
- [ ] PIN rotation flow works end-to-end.
- [ ] Audit log records every expected action.
- [ ] Reconciliation job runs nightly in stage and reports zero divergence.

### 10.2 Cutover acceptance (Crane N on D-0)

Covered by the Go/No-Go checklist §5.

### 10.3 Bake period acceptance (Crane N D+1 .. D+14)

- [ ] Zero data loss incidents.
- [ ] `rtg_outbox_permanent_failures_total` stays at 0.
- [ ] Operator feedback survey: ≥ 4/5 satisfaction.
- [ ] Nightly reconciliation zero divergence for 7 consecutive days.
- [ ] Support tickets trending down (not up) by week 2.

### 10.4 Production completion (all cranes + bake)

- [ ] All three cranes on the new stack for ≥ 2 weeks each.
- [ ] Legacy binaries still present on disk for rollback but not restarted.
- [ ] Dual-write lag SLO met (p99 < 5s).
- [ ] Phase 2 handoff to Phase 3 (MIS migration) team.

---

## 11. Communication plan

| Audience | Channel | Frequency |
|---|---|---|
| Operators | in-person at shift start; posters in break room | before cutover + daily in bake |
| Yard supervisors | direct daily standup | weekly pre-cutover, daily during bake |
| MIS / ForkliftApp teams | email + interface contract review | 4 weeks before first cutover |
| IT / infra / DBAs | change-management ticketing system | per change |
| Goldbond executives | monthly status summary | monthly |

---

## 12. Open questions

### Q-74 🟡 Medium — Preferred cutover window
Night (22:00-02:00) is industry default; is there a Goldbond maintenance window that works best? Depends on vessel schedules.

### Q-75 🟡 Medium — Who owns rollback decision authority
During the cutover window, who can call "rollback" — the cutover lead, the yard supervisor, the CTO? Needs to be named in advance.

### Q-76 🟢 Low — Post-migration cleanup of legacy binaries
After all 3 cranes are stable for 90 days, should we delete the legacy `.exe` files, or keep them permanently for historical forensics?

---

## 13. Check-in summary

- **Crane 3 is the pilot;** Crane 1 and 2 follow after a 2-week bake. 15-week timeline from Phase 2 start to full retirement plan kickoff.
- **12-item prerequisite list + 13-item Go/No-Go checklist** gate every cutover. Rollback must be < 30 minutes and rehearsed in stage (P-11).
- **Operator training** is three sessions: classroom, on-cab shadow, go-live with trainer. Laminated Hebrew quick-reference card in every cab.
- **Dual-write retirement** proceeds event-type-by-event-type starting 8 weeks after the last crane cutover, in the safe order: login → a1 → jobs → movements → locations (last).
- **Next:** Step 2.7 — update the risk register to show Phase-2 mitigations against every Phase-1 risk.
