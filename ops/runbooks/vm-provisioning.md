# VM Provisioning Runbook — Craines-TOS / RTG Modernization

> **Purpose:** Document the exact VM provisioning steps for both the **dev VM** (developer workstation, separate from production) and the **production VM** (single VM on OT subnet `192.6.8.x` running the new RTG stack).
> **Owner:** Goldbond IT, with Yaniv (project owner) as point of contact
> **Story reference:** Epic 8, Story 8.1
> **Last updated:** 2026-04-26

---

## 1. Why two VMs

| VM | Purpose | Network | Owner |
|---|---|---|---|
| **dev-vm** | Build + test + iterate locally; runs Docker/PG/Flutter for development | Corp network — must reach MSSQL `192.6.8.52` for `mssql_mirror.*` reads | Developer (Yaniv) |
| **prod-vm** | Run `Rtg.Listener`, `Rtg.Api`, `Rtg.DualWrite` Windows Services + PG (Docker) for production | OT subnet `192.6.8.x` (separate from `192.6.8.52`) | Goldbond IT (deploys via GitHub Actions self-hosted runner) |

Both are Windows Server 2022. They have different network locations and different policy postures.

---

## 2. Dev VM provisioning

### 2.1 Hardware

| Spec | Value | Rationale |
|---|---|---|
| vCPU | 4–8 | .NET build + Flutter build + Docker for PG/Seq/Prometheus/Grafana — modest concurrent load |
| RAM | 16–32 GB | Docker Desktop on Windows reserves ~6 GB for the Linux container host; 16 GB minimum |
| Storage | ~500 GB SSD | .NET tooling + Flutter SDK + Docker images + repo clone + build artifacts |
| OS | Windows Server 2022 | Matches production target; better Docker Desktop support than Server 2019 |
| Network | Corp network with route to MSSQL `192.6.8.52` (port 49993 for the mirror DB) | Dev needs to read legacy MSSQL during transition |

### 2.2 Account + admin rights

- Provision a local admin account for Yaniv (`yanive-se` or equivalent).
- AD-join the VM to Goldbond's domain so Yaniv can log in with his AD credentials.
- Grant Yaniv local admin on the VM (NOT on his corporate workstation — VM-only scope).

### 2.3 Group Policy

- Apply a **dev OU policy** that:
  - Exempts the VM from the AppLocker / SRP rule that blocks `dart.exe` execution from user paths
  - Allows `docker.exe`, `dockerd.exe`, `wsl.exe`
  - Allows `chocolatey` and `winget` installs (signed package paths)
  - Standard security still applies (Defender, BitLocker, audit logging) — only execution restrictions are relaxed

### 2.4 Software pre-install

Install in this order (each verified before moving on):

1. **Windows Updates** — bring to current monthly patch level
2. **.NET 10 SDK** — https://dotnet.microsoft.com/download — verify `dotnet --version` returns ≥ `10.0.201`
3. **Git** — https://git-scm.com/download/win — verify `git --version`
4. **Docker Desktop** — https://www.docker.com/products/docker-desktop — needs Hyper-V or WSL2 backend
   - Verify daemon starts: `docker info` returns server info (not just client)
   - Verify it pulls images: `docker pull postgres:18.3-alpine` succeeds
5. **WSL 2** (if Docker Desktop uses WSL backend) — `wsl --install`
6. **Flutter SDK 3.41.5** — https://docs.flutter.dev/install/windows
   - Extract zip to `C:\Program Files\Flutter\` (admin path; Group Policy allowed)
   - Add `C:\Program Files\Flutter\bin` to system PATH
   - Verify `flutter --version` returns Flutter 3.41.5 (or compatible)
   - Run `flutter doctor` and resolve any platform issues
7. **Visual Studio Code** — https://code.visualstudio.com/
   - Install extensions: C# Dev Kit, Flutter, Docker, EditorConfig, GitLens
8. **PowerShell 7** — https://github.com/PowerShell/PowerShell/releases (the cross-platform pwsh, not Windows PowerShell 5)
9. **dbmate** — https://github.com/amacneil/dbmate
   - Either: `choco install dbmate -y` (admin)
   - Or: download `dbmate-windows-amd64.exe` and place in `C:\Program Files\dbmate\dbmate.exe`, add to PATH

### 2.5 AD service account (for LDAP sync)

- Create AD service account `svc-rtg-ldap`:
  - Read-only access to user OUs (`OU=Users,DC=goldbond,DC=local` or equivalent) and group OUs (`OU=Groups,DC=goldbond,DC=local`)
  - Required group memberships: any AD-Read group your environment uses
  - Password: stored in Goldbond password manager + delivered to Yaniv via secure channel for his `C:\Rtg\secrets\auth.env` setup (Story 8.5)

### 2.6 Firewall (dev VM)

Outbound only — no inbound listeners on dev VM:

- Allow TCP outbound to MSSQL `192.6.8.52:49993` (mirror DB read access)
- Allow TCP outbound to GitHub `*.github.com:443` (git, releases, Actions runner)
- Allow TCP outbound to NuGet `api.nuget.org:443`
- Allow TCP outbound to Docker Hub `*.docker.io:443` (image pulls)
- Allow TCP outbound to Flutter / Dart package repos `pub.dev:443`

### 2.7 Verification (post-provision)

Yaniv signs off when all of these pass:

```powershell
# .NET
dotnet --version          # Expect 10.0.201+

# Flutter
flutter --version         # Expect 3.41.5
flutter doctor -v         # All green or "warning" only

# Docker
docker --version          # Expect 29.x
docker info               # Expect Server section populated
docker pull postgres:18.3-alpine   # Expect "Pull complete"

# Git + GitHub access
git --version
git ls-remote https://github.com/flutter/flutter.git HEAD   # No auth error

# dbmate
dbmate --version          # Expect 2.x

# MSSQL mirror reach
Test-NetConnection 192.6.8.52 -Port 49993   # Expect TcpTestSucceeded: True
```

### 2.8 Repo bootstrap (dev runs after provision)

```bash
git clone https://github.com/goldbond/craines-tos.git
cd craines-tos
docker compose -f docker-compose.dev.yml up -d   # PG + Seq + Prometheus + Grafana
cd server && dotnet build && dotnet test
cd ../cabin && flutter pub get && flutter build windows --debug
cd ../forklift && flutter pub get && flutter build windows --debug
```

If all succeed: dev VM is ready for active development.

---

## 3. Production VM provisioning

> Provisioned later in Epic 8 (Story 8.1) after dev VM is working and pilot is on the calendar.

### 3.1 Hardware

| Spec | Value | Rationale |
|---|---|---|
| vCPU | 4–8 | Listener + API + DualWrite + PG concurrent load |
| RAM | 16–32 GB | PG cache + .NET service heap |
| Storage | ~500 GB SSD | PG data + WAL + backups + logs |
| OS | Windows Server 2022 | Matches dev VM |
| Network | OT subnet `192.6.8.x` (assigned IP TBD by IT) | Cranes/cabins reach it on the same subnet |

### 3.2 Software pre-install

Subset of dev VM:

1. .NET 10 Runtime (NOT SDK — runtime only on prod)
2. Docker Desktop OR Docker Engine (for PG container)
3. PowerShell 7 (for deploy scripts)
4. NSSM or built-in Windows Service tooling (for Service registration in Story 8.3)
5. Goldbond Cert (internal CA-signed cert for IIS TLS terminator) — issued by IT cert team

### 3.3 IIS reverse proxy

- Install IIS with WebSocket support enabled
- Configure HTTPS site terminating TLS, forwarding to Kestrel on `localhost:5000` (for the `Rtg.Api`)
- Bind cert (internal CA-signed, see §3.2)
- Configure HTTP→HTTPS redirect + HSTS

### 3.4 Network

- **Inbound TCP 30701-30703** allowed only from PLC IPs (whitelist managed by IT — see Story 8.7)
- **Inbound TCP 443** for cabin/forklift HTTPS access (REST + WebSocket)
- **Inbound TCP 5985 / 5986** WinRM for GitHub Actions self-hosted runner deploy
- **Outbound** to MSSQL `192.6.8.52:1433` for dual-write
- **Outbound** to AD/LDAP for sync (port 389 or 636 LDAPS)

### 3.5 Backup

- VM-level snapshots: weekly
- PG dumps: nightly via cron in Docker container, written to a separate backup volume
- Offsite backup: weekly copy of the backup volume to Goldbond's offsite backup destination

### 3.6 Time sync

- NTP sync to Goldbond NTP server (`ntp.goldbond.local` or equivalent)
- Verify drift < 1s (Story 8.6)

---

## 4. AppLocker / SRP rules — current state vs. needed state

### Current corporate-default rules (causing blockers)

Likely path-based AppLocker rules:
- Allow execution from `C:\Program Files\` and `C:\Windows\`
- Block execution from user-writable paths (`C:\Users\*`, `E:\*`, etc.)

This blocks Flutter SDK because `dart.exe` from a fresh git clone lives in user paths.

### Needed exceptions (dev VM only, NOT prod VM)

- Allow execution from `C:\Program Files\Flutter\` (after IT installs Flutter to admin path) — implicitly allowed by default rule
- OR: Allow execution from a dedicated dev path like `C:\dev\` or `E:\dev\` for tools
- OR: Path exception specifically for `flutter.exe`, `dart.exe`, `docker.exe`

The cleanest answer is **install Flutter to `C:\Program Files\Flutter\`** (admin) — then no policy change needed.

---

## 5. Sign-off

When this runbook is fully executed:

- [ ] Dev VM provisioned + Yaniv has admin
- [ ] All software installed and verified per §2.7
- [ ] AD service account `svc-rtg-ldap` created and credential delivered
- [ ] Firewall rules per §2.6 in effect
- [ ] Yaniv successfully runs the §2.8 bootstrap end-to-end

Sign-off: ____ (IT) / ____ (Yaniv)

---

## Notes for IT

- The Group Policy block on `dart.exe` is the immediate symptom; the root cause is broad AppLocker path restrictions. Installing Flutter to `C:\Program Files\Flutter\` sidesteps the issue without policy changes.
- Docker daemon access is the second blocker. If your environment has hyper-v restrictions, Docker Engine (without Desktop) on Windows Server is also viable.
- The architecture document at `_bmad-output/planning-artifacts/architecture.md` and the epics at `_bmad-output/planning-artifacts/epics.md` give full context if you want to dig in.
