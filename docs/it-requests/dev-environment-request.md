# IT Request — Dev Environment for Craines-TOS / RTG Modernization

**Requested by:** Yaniv (yaniv@goldbond.co.il)
**Project:** RTG crane TOS modernization (Goldbond Ashdod, replacing legacy `TOSService.exe` + `RTGApp.exe` cabin UI + `ForkliftApp.exe`)
**Date:** 2026-04-26
**Priority:** Blocking — implementation cannot proceed without this

---

## Summary

I need a dev environment where I can build, test, and run the new RTG codebase before pilot cutover (Epic 8 / Saturday window). My current corporate workstation is too policy-restricted to support the required tooling — specifically Docker daemon and Flutter SDK execution are blocked.

This request is for either: (a) a dedicated dev VM with appropriate admin access, OR (b) policy whitelisting of specific tools on my current workstation.

---

## What's currently blocked on my workstation

| Tool | Status | Impact |
|---|---|---|
| .NET 10 SDK (10.0.201) | ✅ Works | — |
| Git, Docker client | ✅ Installed | — |
| **Docker daemon** | ❌ Cannot start | All PG / Seq / Prometheus / Grafana services that the architecture runs locally are unavailable |
| **Flutter SDK** (`dart.exe`) | ❌ `"This program is blocked by group policy"` | Cabin + forklift UI development entirely blocked |
| **Admin rights** | ❌ Standard user only | Cannot install / whitelist / modify policy |
| **Chocolatey install** | ❌ `Access denied to C:\ProgramData\chocolatey\lib-bad` | Can't install dev tools |
| **PowerShell** | ❌ Restricted (per Yaniv) | — |

Symptoms verified by direct test on 2026-04-26.

---

## What I'm requesting (Option A — preferred)

A dedicated **dev VM**, separate from my corporate workstation:

| Spec | Value |
|---|---|
| OS | Windows Server 2022 (matches the production target) |
| vCPU | 4–8 |
| RAM | 16–32 GB |
| Storage | ~500 GB SSD |
| Network | OT subnet `192.6.8.x` so it can reach existing MSSQL `192.6.8.52` and (eventually) the new PG host |
| Admin rights | Full local admin on the VM for my account |
| Pre-installed software | .NET 10 SDK · Docker Desktop or Docker Engine · Flutter 3.41.5 SDK · Git · PowerShell 7 · Visual Studio Code · `dbmate` |
| AD service accounts | `svc-rtg-ldap` (read-only on user/group OUs) — for the LDAP sync feature |
| Group Policy | Either exempted (it's a dev VM) OR with `flutter`, `dart.exe`, `docker.exe` execution whitelisted |

This VM is **separate** from the production VM the architecture describes (which runs the actual deployed services). It's purely a developer workstation.

---

## What I'm requesting (Option B — fallback)

If a dedicated dev VM isn't possible quickly, please apply the following exceptions to my current workstation:

1. **Whitelist `E:\AI\flutter\bin\dart.exe`** (or the parent directory `E:\AI\flutter\`) in AppLocker / Software Restriction Policy — Flutter SDK is already cloned and on disk; only execution is blocked.
2. **Enable Docker Desktop** to run for my user account (this likely means provisioning Hyper-V or WSL2 access, depending on the underlying restriction).
3. **Grant chocolatey install rights** to my account, OR pre-install `dbmate` and any other dev tools system-wide.

---

## Why this matters / business context

The RTG modernization project replaces legacy crane TOS infrastructure that's been in production for ~10 years. Phase 1 (discovery) and Phase 2 (architecture) are complete. Phase 3 implementation has begun but is currently bottlenecked at Story 1.1 — the very first story — because the foundation requires Flutter and Docker.

Without dev environment access, the project cannot reach the Saturday GOLD3 cutover (Epic 8) on schedule. Every day of delay on the dev environment is a day of delay on the modernization timeline.

---

## Timeline

- **Ideally:** dev VM provisioned within 1 week
- **Acceptable:** policy exceptions on current workstation within 2-3 days
- **Currently:** ~50% of Story 1.1 done (the .NET parts that don't need Docker/Flutter); rest of the project blocked

---

## Attached

`ops/runbooks/vm-provisioning.md` — full VM provisioning runbook with the exact software install steps, Group Policy considerations, AD service account requirements, and firewall rules. IT can follow this directly.

---

## Contact

Reply to this ticket or contact Yaniv directly at yaniv@goldbond.co.il. I'm available to clarify any technical specifics or to demonstrate the blocking symptoms if needed.
