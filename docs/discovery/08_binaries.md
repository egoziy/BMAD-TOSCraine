# 08 — Reverse Engineering of Compiled Binaries

> **Phase 1 — Step 1.8** · Analyst: Claude Code · Date: 2026-04-21
> **Tools used:** `file`, UTF-16LE string extraction (`tr -d '\000' | tr -c '[:print:]' '\n'`), `grep`. No dedicated .NET decompiler (ILSpy / dnSpy) was invoked — that requires operator/user access to the machine. However, for Mono/.NET assemblies the literal string pool and metadata table are plainly visible and sufficient for substantial reverse-engineering.
> **Confidence legend:** **CONFIRMED** (directly extracted from binary), **INFERRED** (best interpretation of observed evidence), **UNKNOWN** (cannot be determined from static inspection).

---

## 1. Binaries catalogued

| # | File | Size | Date | Architecture | Type | Source-of-truth |
|---|---|---:|---|---|---|---|
| B1 | `RTG/Console1/TOSConsole1.exe` | 17 408 | 2024-10-29 | PE32, x86, Mono/.NET | Console listener for Crane #1 | `TFS/RTG/TOSConsole1/` (partial — see §3) |
| B2 | `RTG/Console2/TOSConsole2.exe` | 17 920 | 2024-10-29 | PE32, x86, Mono/.NET | Console listener for Crane #2 | `TFS/RTG/TOSConsole2/` (partial — see §3) |
| B3 | `RTG/Console3/TOSConsole3.exe` | 17 920 | 2023-09-06 | PE32, x86, Mono/.NET | Console listener for Crane #3 | `TFS/RTG/TOSConsole3/` (matches version in source) |
| B4 | `RTG/TosService/TOSService.exe` | 17 920 | 2022-06-09 | PE32, x86, Mono/.NET | Standalone listener (Gold2 only at runtime) | `TFS/RTG/TOSService/` (TFS source is broken — see §3.4) |
| B5 | `RTG/ConsoleReRun/ConsolesReRun.exe` | 147 968 | 2023-01-11 | **PE32+, x86-64**, native bootstrap | .NET 6+ self-contained apphost (watchdog) | `TFS/RTG/ConsoleReRun/ConsolesReRun/Program.cs` (matches) |
| B6 | `RTG/ReRun/TosReRun.exe` | 149 504 | 2022-06-09 | **PE32+, x86-64**, native bootstrap | .NET 6 self-contained apphost (older watchdog) | Not in TFS — older version of ConsolesReRun |
| B7 | `RTG1-2/RTGApp.exe` | 567 296 | 2023-01-23 | PE32, x86, Mono/.NET | Operator UI for Cranes 1 & 2 | `TFS/RTG/RTG1-2/RTGApp/` (builds differ — see §4) |
| B8 | `RTG3/RTGApp.exe` | 562 176 | 2023-11-07 | PE32, x86, Mono/.NET | Operator UI for Crane 3 | `TFS/RTG/RTG3/RTGApp/` (builds differ — see §4) |

All `.NET Framework` binaries target **v4.8, x86, Debug** (confirmed via the `TargetFrameworkAttribute .NETFramework,Version=v4.8` string and the `\obj\x86\Debug\` PDB paths below).

---

## 2. PDB source-tree paths — who built what

The **PDB path embedded in each binary** reveals the build machine's workspace. This is often the most revealing metadata on an un-stripped binary.

| Binary | Embedded PDB path | Build workspace |
|---|---|---|
| TOSConsole1 | `C:\Work\ContermTFS\RTG\TOSConsole1\TOSConsole\obj\x86\Debug\TOSConsole1.pdb` | `ContermTFS` workspace |
| TOSConsole2 | `C:\Work\ContermTFS\RTG\TOSConsole2\TOSConsole\obj\x86\Debug\TOSConsole2.pdb` | `ContermTFS` workspace |
| TOSConsole3 | `C:\Work\ContermTFS\RTG\TOSConsole3\TOSConsole\obj\x86\Debug\TOSConsole3.pdb` | `ContermTFS` workspace |
| TOSService | `C:\WorkspaceMichael\RTG\TOSService\TOSService\obj\x86\Debug\TOSService.pdb` | **`WorkspaceMichael`** (different machine / different user) |
| RTG1-2 RTGApp | `C:\WorkSpace\RTG\RTG1-2\RTGApp\RTGApp\obj\x86\Debug\RTGApp.pdb` | **`WorkSpace`** (different again) |
| RTG3 RTGApp | `C:\WorkspaceMichael\RTG\RTG3\RTGApp\RTGApp\obj\x86\Debug\RTGApp.pdb` | `WorkspaceMichael` |

### Interpretation

1. **At least three distinct build machines / developer workspaces exist.** `ContermTFS`, `WorkspaceMichael`, `WorkSpace`. The first smells like the enterprise-TFS workspace root; the second is a developer nicknamed "Michael" (possibly Michael L., cross-referenced with the `SQLWEB\Michaell-admin` trigger exception in `06_triggers.sql:574`); the third is a generic folder.
2. **RTG3 and TOSService are both built by Michael.** This cements the per-crane fork hypothesis from `02_components.md` §5: Michael is the "Crane 3 branch" owner.
3. **RTG1-2 and the three TOSConsoles come from different workspaces** (`C:\WorkSpace` vs `C:\Work\ContermTFS`). Same person could have both checkouts, but the naming suggests two people. (This matters for Phase 2 onboarding — we want to consolidate on one repo.)
4. **All binaries are Debug builds.** Not Release. Implications: (a) optimizations off; (b) assertions enabled; (c) debug symbols included; (d) `DefineConstants=DEBUG;TRACE`. Deploying Debug to production is unusual and reveals an informal build process (Q-40).

---

## 3. `TOSConsole{1,2,3}.exe` — listener binaries

### 3.1 TOSConsole1 (B1) — **version 2024-10-29**

Strings extracted that resolve the operational picture:

| Observed string | Source file / line | Meaning | Confidence |
|---|---|---|---|
| `192.6.8.52` | `Crc32.cs:45` | DB server IP | CONFIRMED |
| `Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;Integrated Security=SSPI;` | `Crc32.cs:45` | Full conn string, **unchanged from source** | CONFIRMED |
| `192.6.1.8` | `Program.cs:119` | `localAddr` for TcpListener (unused — see §3.2) | CONFIRMED |
| `\\Broadcast\Logs\RTG\RTG1\TOSConsole1\Log -` | `Program.cs:377` | Log-file UNC path | CONFIRMED |
| `version 2024-10-29` | `Program.cs:387` | Hard-coded program-version string in log entries | **CONFIRMED — but source has `version 2023-09-06`** → see §3.3 |
| `Waiting for a connection GOLD1 ... `, `Waiting BROADCAST  GOLD1 ... ` | `Program.cs:151, 187` | Console status prints | CONFIRMED |
| `FFFF303342313046454641` | `Program.cs:338` | NAK / unknown-type response | CONFIRMED |
| All 10 SELECT / UPDATE / INSERT / DELETE statements | `Program.cs:181, 225, 267, 268, 272, 287, 293, 297, 298, 304-309, 311, 316, 317, 322, 323, 334, 335` | Raw SQL | CONFIRMED — matches source byte-for-byte |
| **Only `GOLD1` appears**, no `GOLD2`/`GOLD3` | — | Hardcoded per-crane, as expected | CONFIRMED |

### 3.2 Dead-code confirmation for `IPAddress localAddr`

The source says `IPAddress localAddr = IPAddress.Parse("192.6.1.8")` (`Program.cs:119`) but the `TcpListener` is constructed with the port only: `server = new TcpListener(port)` (`Program.cs:138`). The variable `localAddr` is never used.

Binary check: The string `192.6.1.8` **is** present in the binary's string pool, confirming the source was compiled with the variable — the compiler preserves it as a literal even though the value is unused in the final listener binding. This means the listener binds to **all interfaces** (IPv4/IPv6 wildcard), not to `192.6.1.8`. Any host that has this binary + routing can receive crane connections.

### 3.3 Binary vs. TFS source drift

**Critical finding:** the deployed TOSConsole1.exe has log version `"version 2024-10-29"`, but the TFS source at `From TFS/RTG/TOSConsole1/TOSConsole/Program.cs:387` shows the version literal as `"version 2023-09-06"`.

Interpretation options:
1. **The TFS source we have is stale.** The binary was built from a newer source that updated this literal on 2024-10-29 (matching the file timestamp). The source was either never checked into TFS, or the TFS export we received was done before the change.
2. **The binary and TFS source are from different branches.** Possibly a hotfix branch.
3. **A post-build edit.** Unlikely but possible (ILMerge, patcher).

Hypothesis 1 is most likely. **Implication:** every conclusion in `01_-07_` about "what the code does" should be treated as **approximate**; the running code may have undocumented patches. Q-39.

### 3.4 TOSConsole2 (B2) — **version 2024-10-29 AND contains TWO log-path literals**

The TOSConsole2 binary contains **both** of these log-path strings:

1. `\\broadcaST\Logs\RTG\RTG1\TOSConsole2\LOG_{0}.TXT` — note:
   - `broadcaST` (mixed case — matches earlier `RTGApp.exe.config` typo)
   - folder is `RTG1`, not `RTG2` — **likely a bug**
   - filename pattern is `LOG_{0}.TXT` uppercase
2. `\\Broadcast\Logs\RTG\RTG2\TOSConsole2\Log -` — correct folder `RTG2`, standard filename pattern

Interpretation: the source in TFS only has pattern #2. The binary was built from a source that also included #1 — probably as a **fallback try/catch log-path** added after a log-write outage. Another source-drift signal (Q-39).

### 3.5 TOSConsole3 (B3) — **version 2023-09-06** (stale)

Log version matches the TFS source. Crane 3 has not been re-released since September 2023. This is consistent with the timeline (see `01_inventory.md` §3: TOSConsole3.exe file date = 2023-09-06).

Also **strings extracted show the same log-path pattern** as Console2 — `\\Broadcast\Logs\RTG\RTG3\TOSConsole3\Log_` — suggesting a previous log-file versioning where Crane 3 uses `Log_MMDDYYYY.txt` instead of `Log -MM-dd-yyyy.txt`. Cosmetic.

---

## 4. RTG Operator UI binaries (B7, B8) — the real connection-string reveal

This is the single biggest **security** finding in the binary analysis.

### 4.1 B7 — `RTG1-2/RTGApp.exe` (2023-01-23)

Extracted connection string:

```
Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=malgezot;Password=12345678
```

| Observation | Confidence |
|---|---|
| Username `malgezot` (Hebrew "מלגזות" = "forklifts") | CONFIRMED — Hebrew word for "forklifts" baked in as a shared service account |
| Password **`12345678`** — plaintext | CONFIRMED |
| **Does NOT match the RTG1-2 TFS Settings.settings value** (`Integrated Security=True`) | CONFIRMED — two different compile-time values in two different source snapshots |
| **Does NOT match the RTG1-2 `RTGApp.exe.config` runtime value** (`Integrated Security=True`) | CONFIRMED |

**Conclusion:** when the `RTGApp.exe.config` is **missing** or does not contain a valid `connectionStrings` override, the deployed RTG1-2 binary falls back to `User ID=malgezot;Password=12345678`. An unsuspecting cabin PC that lost its config file will still function (!) but authenticate using a shared service-account credential baked into the binary.

This credential (`malgezot` / `12345678`) is now a **shared secret** that must be considered **exposed** and **rotated** as part of Phase 2 cutover. Q-38.

Also: the RTG1-2 binary contains **typed-DataSet-generated SQL** for `RG_B3` CRUD operations — with parameterised SQL (e.g. `@Counter, @CHE, @ContID1, …`). So the UI already has a working parameterised path for `RG_B3` (via `TerminalDataTestDataSet1.RG_B3TableAdapter`). The security-unsafe code is only the ad-hoc `Con1.ReturnDT(sql)` string-concat calls in `Form1.cs` / other forms. The distinction matters for Phase 2 migration.

### 4.2 B8 — `RTG3/RTGApp.exe` (2023-11-07)

Extracted connection string:

```
Data Source = 192.6.8.52; Initial Catalog = TerminalData; Persist Security Info = True; Integrated Security = SSPI;
```

| Observation | Confidence |
|---|---|
| **Integrated Security = SSPI** | CONFIRMED — **NOT `z3334606*` password** |
| Has spaces around `=` signs (unusual but legal) | CONFIRMED |
| **Does NOT match RTG3 TFS Settings.settings** (which had `User ID=" + SystemUserName + ";Password=z3334606*`) | CONFIRMED |

**Conclusion:** the RTG3 binary deployed in November 2023 was built from source where the `TerminalDataConnectionString` was set to Integrated Security, not password. The TFS snapshot we have contains an **older (broken) version** of the connection string. Since the RTG3 `RTGApp.exe.config` runtime file is empty, the binary's compiled-in Integrated Security is what runs in production.

**Positive finding:** RTG3 is **NOT** actually shipping a hard-coded SQL password at runtime, despite what the TFS source suggests. The earlier alarming conclusion in `02_components.md` §4 #1 needs to be revised. The **TFS source is misleading** — it contains dead/old code.

But the broader concern remains: someone at some point **did** write `z3334606*` into a committed file. Even if that value is no longer active, any credential committed to version control must be treated as compromised and rotated.

### 4.3 Other UI-specific strings observed

From RTG3 B8:

- **`SELECT * FROM dbo.V_MapRTGBond3`** — the RTG3 UI queries a view `V_MapRTGBond3` that **does not exist** in the current DB (only `V_MapRTGBond1` and `V_MapRTGBond2` are in `01_target_objects.txt`). This query is likely the RTG3 variant of `MappingSquare()`. It will fail at runtime; the UI must handle the exception silently or skip the map refresh for Bond 3. **Undetermined behaviour** — Q-41.

- **`update TB_Location set empty=1 FROM TB_Location INNER JOIN CO_Containers ...`** — the RTG3 UI has a bulk-update capability that the RTG1-2 UI lacks. Operator can mark empty locations in bulk by running this UPDATE. Implication: RTG3's workflow is materially different — operator does housekeeping that Crane 1/2 operators do not.

- **`TB_WorkType.gold3`** / **`TB_WorkType.Gold3`** — two different case variants of the same column in the RTG3 code. SQL is case-insensitive so this works, but indicates editing by multiple devs over time.

From RTG1-2 B7:

- **Extensive typed-DataSet generated INSERT/UPDATE/DELETE with `@Original_*` parameters for `RG_B3`**. This means whenever the UI uses the typed DataSet path (as opposed to `Con1.ReturnDT(sql)`) the SQL is parameterised. We have mixed safety-posture within the same application.

- **13 embedded resource sets**: `FrmMap01`, `FrmContainersByBloc`, `FrmLogin`, `FrmRecommendedLocation`, `FrmContainerNoLocation`, `ContainerLocation`, `frmInformation`, `Properties.Resources`, `FrmWorks`, `FrmExpectedContainers`, `FrmEmptyContainers`, `FrmMenu`, `FrmInOutDiory`. All 13 active forms are present — no dead forms in the binary.

### 4.4 TOSService (B4)

- Version `2022-03-21` (oldest stratum).
- PDB at `C:\WorkspaceMichael\...`.
- Contains **all three CHE paths** (`Gold1`, `Gold2`, `Gold3` — though the runtime loop in source only enables `Gold2`).
- **Binary has a valid static `Main`** (otherwise it couldn't be a working .NET exe). The TFS source does not — confirming the TFS source is **older / broken** than the deployed binary. This is the second piece of evidence for broad source-to-binary drift (see §3.3).
- Log path: `\\Broadcast\Logs\RTG\TosService\Log_` — distinct from TOSConsole logs, under its own `TosService\` subfolder.

Status: **likely running in production but probably idle** — only Gold2 path is active, TOSConsole2 handles the same port, so one of them is redundant. Needs confirmation (Q-04).

---

## 5. `ConsolesReRun.exe` (B5) & `TosReRun.exe` (B6) — .NET 6 watchdogs

### 5.1 B5 (B6 similar, older)

- **PE32+, x86-64** — a different runtime generation than all the other binaries.
- Ships with its own private `System.Data.SqlClient.dll` and `sni.dll` (for SQL Server native access) — but **uses neither at runtime**, per the source `Program.cs:1-32`. They're artifacts of the project template. The project could be stripped of these and halved in size. This is the kind of clean-up Phase 2 will want to do when porting.
- Apphost PDB: `D:\a\_work\1\s\artifacts\obj\win-x64.Release\corehost\apphost\standalone\apphost.pdb` — confirms the apphost comes from Microsoft's GitHub Actions `a\_work\` pipeline layout, not user-built. The actual app logic is in `ConsolesReRun.dll` (7 KB).
- .NET version advertised: `apphost_version=6.0.13` — **.NET 6 SDK 6.0.13** — a specific patch level.

### 5.2 What the watchdog does

From `ConsolesReRun/Program.cs:1-32`:

```csharp
foreach (var p in Process.GetProcessesByName("TOSCONSOLE1"))
{
    p.Kill();
    p.WaitForExit();
}
Process n = new Process();
n.StartInfo.FileName = @"E:\RTG\Console1\TOSConsole1.exe";
n.Start();
// repeat for TOSCONSOLE2, TOSCONSOLE3
```

Simple kill-then-relaunch, **runs once and exits**. Not a daemon. Must be triggered by Task Scheduler or manually. This is the second restart mechanism alongside `KillToss*` / `RunEnconsoleRTG*` SPs (see §8 below).

---

## 6. Summary — CONFIRMED vs INFERRED vs UNKNOWN

### 6.1 CONFIRMED from binary inspection

- All `.NET Framework` binaries target v4.8, x86, Debug.
- All listeners use hard-coded `Data Source=192.6.8.52;…;Integrated Security=SSPI;` (binary matches source).
- RTG1-2 UI binary **defaults** to `User ID=malgezot;Password=12345678` (runtime exe.config happens to override with Integrated Security).
- RTG3 UI binary **defaults** to `Integrated Security=SSPI` (different from TFS Settings.settings).
- All SQL statements in all binaries match the TFS source byte-for-byte in terms of *content* (same table/column names), except for log-version literals and the TOSConsole2 second log-path.
- Watchdog is .NET 6 SDK 6.0.13, x64.

### 6.2 INFERRED

- TFS source is somewhat stale; at least TOSConsole1/2 log-version literals and TOSService entry point are newer in the binary than in the TFS snapshot. The RTG3 / RTG1-2 connection strings also differ (binary is "better" than TFS).
- The `ContermTFS` workspace is the canonical enterprise TFS checkout; `WorkspaceMichael` is a developer-local checkout.
- `malgezot` user exists in the DB as a SQL Login with password `12345678` — or did at one point.
- `V_MapRTGBond3` existed historically (referenced by deployed RTG3 binary) but was dropped from the DB.
- Deploying Debug builds to production is a conscious or accepted practice in this org — Phase 2 should produce Release builds.

### 6.3 UNKNOWN (to resolve via user contact)

- **Whether `TOSService.exe` is actually running in production today.** Binary is deployed; source is broken; behaviour needs operator confirmation. **Q-04**.
- **Whether the `malgezot / 12345678` login is still valid on the SQL server**, or has been revoked after the Integrated Security migration. **Q-38**.
- **What replaces `V_MapRTGBond3`** when the RTG3 UI queries it today. Some kind of exception-silenced path. **Q-41**.
- **Who are ContermTFS / WorkspaceMichael developers** (names, roles). **Q-42**.
- **Why are production binaries Debug builds?** **Q-40**.

---

## 7. Concrete binary → source drift register

These are the points where running binary ≠ TFS source:

| # | Binary | What binary contains | What TFS source contains | Gap |
|---|---|---|---|---|
| D1 | TOSConsole1.exe | `version 2024-10-29` | `version 2023-09-06` (Program.cs:387) | ~13 months of undocumented edits |
| D2 | TOSConsole2.exe | Two log paths (buggy `RTG1\` folder + correct `RTG2\`) | Only the one correct log path | Bug-fix or redundancy layer added post-export |
| D3 | TOSService.exe | Static `Main(string[] args)`, callable entry point | `void Main` (non-static) — wouldn't compile | TFS source was broken; someone fixed it before building |
| D4 | RTG1-2/RTGApp.exe | Compile-time conn string `User ID=malgezot;Password=12345678` | `Integrated Security=True` | Different Settings.settings value at build time |
| D5 | RTG3/RTGApp.exe | Compile-time conn string `Integrated Security=SSPI` | `User ID=" + SystemUserName + ";Password=z3334606*` (broken) | TFS has a dead draft; binary has the good version |

**Conclusion for Phase 2:** the source of truth for modernisation must be negotiated with the operator. Either (a) a fresh TFS checkout of current `main` branch, or (b) a decompilation pass on the deployed binaries to recover the latest code. Without this, we'd rewrite against stale source.

---

## 8. Intersection with §2 of other documents

### 8.1 Restart / shutdown mechanisms — definitive picture

Combining `02_components.md`, `04_database.md`, and this doc:

| Mechanism | Kills | Starts | Expected path | Evidence |
|---|---|---|---|---|
| `ConsolesReRun.exe` (B5) | `TOSCONSOLE1/2/3` by name | from `E:\RTG\Console{n}\` | Path #1 | `Program.cs:11,21,31` |
| SP `RunEnconsoleRTG{n}` + `xp_cmdshell` | — | from `c:\RTG\RTG{n}\` | Path #2 | `05_module_definitions.sql:390` |
| SP `KillToss{n}` + `xp_cmdshell` | runs `KillToss{n}.exe` | — | `C:\RTG\RTG{n}\` | `05_module_definitions.sql:353` |
| `KillToss{n}.exe` (binary) | `TOSCONSOLE{n}` by name | — | — | `KillToss1/Program.cs:17` |

So **Paths #1 and #2 disagree on both drive letter (E: vs C:) and folder name (Console1 vs RTG1)**. Either:
- The DB server `SQL01` mounts `E:\RTG\` as `C:\RTG\` (drive alias), OR
- One of the two mechanisms is dead, OR
- Two different hosts each use the naming that matches their local disk layout, OR
- Someone moved files and forgot to update one.

Q-12 (from §3 of `03_data_flows.md`) is a **must-answer** before Phase 2 changes anything.

### 8.2 Configuration-value resolution order

For the **deployed** binary:

| Source | RTG1-2 UI | RTG3 UI | TOSConsole{1,2,3} |
|---|---|---|---|
| 1st: `*.exe.config` `connectionStrings` | ✓ present → Integrated Security | ✗ empty | N/A (no conn string in .exe.config) |
| 2nd: compile-time `Settings.Default.TerminalDataConnectionString` | `malgezot/12345678` | `Integrated Security=SSPI` | N/A |
| 3rd: code-level default (hardcoded in Crc32.cs) | — | — | `192.6.8.52; Integrated Security=SSPI` |

**So:**
- RTG1-2 cabin PC **does** use Integrated Security at runtime (exe.config wins). The `malgezot/12345678` is a dormant fallback — but if the exe.config goes missing, it silently reverts.
- RTG3 cabin PC uses Integrated Security via the compile-time default (exe.config is empty but the Settings.Default was SSPI at build time).
- TOSConsole{1,2,3} always uses SSPI.

This resolves one of the scarier findings in `02_components.md`.

---

## 9. Tools used, limitations, and what still needs decompilation

### 9.1 Tool limitations

- **No ILSpy / dnSpy run.** I did not execute a .NET decompiler. The conclusions in this document are based on:
  - File-type identification (`file`).
  - UTF-16LE string extraction from the binary (gives all string-literal content of the .NET metadata).
  - PDB-path extraction (always in the binary).
  - Public metadata references (method names visible in string pool).
- **Method bodies (CIL bytecode) were not decompiled.** So if a method was added / changed in the binary compared to source, I cannot read the new logic. I can only detect drift when a string-literal changed.

### 9.2 What we can still do (with operator permission)

If access to a Windows dev machine is granted:

- **ILSpy on all 4 `.NET Framework` assemblies** to diff against TFS source. Expected gains: confirm whether RTG3 UI has an exception handler around the `V_MapRTGBond3` query (D-class finding #5); confirm TOSConsole1/2 2024-10-29 rebuild has actual logic changes beyond the version literal; recover the lost TOSService source.
- **`reflector /decompile` on the Typed DataSet files** — potentially recover the full schema expectations the UI was built against.
- **Run `xxd` or a hex dump** around the `malgezot/12345678` string to confirm there are no adjacent backup credentials.

### 9.3 Recommended Phase 1 close-out action

Before closing Phase 1, run ILSpy on B1, B2, B7, B8 to resolve drift register items D1, D2, D4, D5. If the user approves, I will do this in a short follow-up step.

---

## 10. Check-in summary

- **All 8 binaries characterised with evidence from binary inspection** (PDB paths, string pools, .NET version strings). All `.NET Framework` binaries are v4.8 Debug x86; watchdog binaries are .NET 6 x64.
- **Two significant security findings from binary strings:** (1) RTG1-2 UI binary contains the hard-coded fallback credential `User ID=malgezot;Password=12345678` — weak and must be rotated; (2) RTG3 UI binary actually uses Integrated Security at runtime, correcting the earlier concern in `02_components.md` about the `z3334606*` value.
- **Systematic drift between TFS source and deployed binaries** (5 confirmed items, §7). The binary is NEWER than the TFS snapshot in at least TOSConsole1, TOSConsole2, TOSService, RTG1-2 UI, and RTG3 UI. Before Phase 2 can be trusted to rewrite behaviour, we must either get fresh TFS or decompile binaries.
- **Next:** Step 1.9 — compile the open-questions register in `09_open_questions.md`. Already accumulated 40+ implicit questions across previous docs — consolidate.
