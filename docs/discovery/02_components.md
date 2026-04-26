# 02 — Component Identification

> **Phase 1 — Step 1.2** · Analyst: Claude Code · Date: 2026-04-21
> **Scope:** map every source file / binary to the three logical components defined in the prompt (Listener · Persistence · Operator UI). Describe runtimes, deployment units, and per-crane differences. All statements are evidence-based; line references use `file:line` pointing to `From TFS/` copies (current source of truth).

---

## 0. The three logical components — actual mapping

The prompt separates the system into three components: **Listener**, **Persistence**, **Operator UI**. In the code-base they are **not** three processes; they are two:

| Logical component | Implemented by | Runs where |
|---|---|---|
| **Listener** + **Persistence** (merged) | `TOSConsole{1,2,3}.exe` — one process per crane | On a central server (inferred to be the same host as `192.6.1.8`; also near the DB server `192.6.8.52`) |
| **Operator UI** | `RTGApp.exe` — one binary per crane family (`RTGApp1-2` and `RTGApp3`) | On the **in-cab PC** of each crane |

In addition the solution contains **maintenance / watchdog** code that is not in the primary data flow but is operationally important:

| Auxiliary component | Implemented by |
|---|---|
| Watchdog / auto-restart | `ConsolesReRun.exe` (.NET 6) — kills+re-starts TOSConsole1/2/3 |
| Kill utilities | `KillToss{1,2,3}.exe` |
| Older watchdog (superseded) | `TosReRun.exe` |
| Unfinished / stale secondary listener | `TOSService.exe` — see §3.6 |
| **Not RTG (out of scope)** | `ForkliftApp.exe` (shares the DB, lives under `From TFS/RTG/New Folder/ForkliftApp/`) |

Everything else under `From TFS/` is either a duplicate, a build artifact, a TFS-era build template, or an installer bundle — covered in §5 of `01_inventory.md`.

---

## 1. File-to-component mapping

### 1.1 Listener + Persistence process

**Active source-of-truth (per `RTG.sln` at `From TFS/RTG/RTG.sln`):**

| Crane | Project file | Main source | Shared helper | Config | Built binary (deployed) |
|---|---|---|---|---|---|
| #1 (`CHE='GOLD1'`) | `TOSConsole1/TOSConsole/TOSConsole1.csproj:1-68` | `TOSConsole1/TOSConsole/Program.cs:100-406` | `TOSConsole1/TOSConsole/Crc32.cs:1-92` | `TOSConsole1/TOSConsole/app.config` (empty — only `<supportedRuntime>`) | `Current/RTG/Console1/TOSConsole1.exe` (17 408 B, 2024-10-29) |
| #2 (`CHE='GOLD2'`) | `TOSConsole2/TOSConsole/TOSConsole2.csproj` | `TOSConsole2/TOSConsole/Program.cs` | `TOSConsole2/TOSConsole/Crc32.cs` | `TOSConsole2/TOSConsole/app.config` | `Current/RTG/Console2/TOSConsole2.exe` (17 920 B, 2024-10-29) |
| #3 (`CHE='GOLD3'`) | `TOSConsole3/TOSConsole/TOSConsole3.csproj` | `TOSConsole3/TOSConsole/Program.cs` | `TOSConsole3/TOSConsole/Crc32.cs` | `TOSConsole3/TOSConsole/app.config` | `Current/RTG/Console3/TOSConsole3.exe` (17 920 B, 2023-09-06) |

All three projects share the same structure — `Program.cs` + `Crc32.cs` + `AssemblyInfo.cs` + `app.config` + root namespace `TOSConsole`. Only **`AssemblyName`** and the hard-coded constants differ (see §2.1).

### 1.2 Operator UI (RTGApp)

Two actively-referenced csproj variants, both under `From TFS/RTG/RTG.sln`:

| Crane family | Project file | Main form | Entry point | Logging helper | Built binary |
|---|---|---|---|---|---|
| RTG1-2 (Cranes #1 & #2) | `RTG1-2/RTGApp/RTGApp/RTGApp1-2.csproj` | `Form1.cs:23` — `public partial class FrmMap01 : Form` | `Program.cs:12-21` → `Application.Run(new FrmLogin())` | `Utils.cs:16-31` — writes to `RG_ErrorLog` table | `Current/RTG1-2/RTGApp.exe` (567 296 B, 2023-01-23) |
| RTG3 (Crane #3) | `RTG3/RTGApp/RTGApp/RTGApp3.csproj` | `Form1.cs:23` — `public partial class FrmMap01 : Form` | `Program.cs` → `Application.Run(new FrmLogin())` | `ContainerLocation.cs:297-305` — writes to `RG_ErrorLog` table | `Current/RTG3/RTGApp.exe` (562 176 B, 2023-11-07) |

The two variants share ~95% of the code but are **not the same assembly**. See §2.2 for the diff.

Both variants contain:

| `.cs` file | Class inside | Purpose (inferred) |
|---|---|---|
| `Program.cs` | `Program` | `[STAThread] Application.Run(new FrmLogin())` |
| `FrmLogin.cs` | `FrmLogin : Form` | Login screen with on-screen numeric keypad for PIN |
| `Form1.cs` | `FrmMap01 : Form` | **Main map screen** after login (note: class and file name diverged) |
| `Form2.cs` | `frmInformation : Form` | Information screen |
| `ContainerLocation.cs` | `ContainerLocation : Form` | Update-container-location dialog |
| `FrmMenu.cs` | `FrmMenu : Form` | Secondary menu — 7 buttons (EmptyContainers, EmptyLocation, InOutDiory, ForLocation, RecommendedLocation, ExpectedContainers, UpdateLocation); wiring at `FrmMenu.cs:19-78` |
| `FrmEmptyContainers.cs` | `FrmEmptyContainers : Form` | List of empty containers |
| `FrmContainerNoLocation.cs` | `FrmContainerNoLocation : Form` | Containers lacking a yard location |
| `FrmInOutDiory.cs` | `FrmInOutDiory : Form` | In/Out **diary** (file is misspelled — should be `Diary`) |
| `FrmContainersByBloc.cs` | `FrmContainersByBloc : Form` | Containers grouped by yard block |
| `FrmRecommendedLocation.cs` | `FrmRecommendedLocation : Form` | System-suggested location |
| `FrmExpectedContainers.cs` | `FrmExpectedContainers : Form` | Expected-container list |
| `Works.cs` | `FrmWorks : Form` | Works / jobs screen |
| `Class1.cs` | `ConTerminalData` | **DB helper** (connection + `ReturnDT(sql)` pattern) |
| `UserConnection.cs` | `UserConnection` | **Empty class** — placeholder never implemented |
| `Utils.cs` *(RTG1-2 only)* | `Utils` | DB-backed `WriteLog()` to `RG_ErrorLog` |
| `TerminalDataTestDataSet.Designer.cs` + `.xsd` | Typed DataSet — `RG_A1`, `RG_B3` bindings | Windows-Forms designer-generated |
| `TerminalDataTestDataSet1.Designer.cs` + `.xsd` | Additional typed DataSet — `RG_A1` only | Appears partly redundant with the above |

### 1.3 Maintenance / watchdog

| Executable | Source | Runtime | What it does |
|---|---|---|---|
| `ConsolesReRun.exe` (147 968 B, 2023-01-11) | `From TFS/RTG/ConsoleReRun/ConsolesReRun/Program.cs:1-32` | **.NET 6** (top-level-statements, uses `ConsolesReRun.deps.json` + `runtimeconfig.json`) | For each of TOSConsole1/2/3: kills running process by name then `Process.Start(@"E:\RTG\Console{n}\TOSConsole{n}.exe")`. Deploys with its own `System.Data.SqlClient.dll` — but never actually uses SqlClient (dead dependency inherited from project template). |
| `KillToss{1,2,3}.exe` | `From TFS/RTG/KillToss{n}/Program.cs` | .NET Framework (separate csproj each) | Terminates `TOSCONSOLE{n}` process by name. Trivial; swallows all exceptions. |
| `TosReRun.exe` (2022-06-09) | (source not in repo — older equivalent of ConsolesReRun) | .NET 6 | Superseded by `ConsolesReRun`. Deploy bundle still contains a full private set of .NET 6 assemblies in `RTG/ReRun/`. |

### 1.4 Unfinished / stale: `TOSService`

| Executable | Source | Status |
|---|---|---|
| `TOSService.exe` (17 920 B, 2022-06-09) | `From TFS/RTG/TOSService/TOSService/Program.cs:1-346` | ⚠ **The current source will not run.** The entry point is declared as `void Main(string[] args)` (missing `static`) and `void TOS(string gold, Int32 port)` (also non-static) at lines `Program.cs:21` and `:44`. A non-static `Main` is not a valid C# entry point; this project should not compile as an `Exe` in its current state. The **deployed** `.exe` (June 2022) must have been built from an earlier/different version. Even in that deployed form, only `TOS("Gold2", 30702)` is active in the `while(true)` loop — the Gold1/Gold3 calls are commented out (`Program.cs:35-37`). |

Interpretation: `TOSService` was likely a **never-finished second-generation listener** (running on `192.6.8.52`, the DB server) that still has stubs for all three cranes but was only ever exercised for Crane #2. It should be treated as dead until the user confirms otherwise (see `09_open_questions.md` Q-04).

### 1.5 Out-of-scope: `ForkliftApp`

`From TFS/RTG/New Folder/ForkliftApp/` is a separate product (layered architecture: `ForkliftApp.BusinessLogic`, `.DataAccess`, `.Entities`, `.Framework`, plus its own Setup project `ForkLiftAppSetup.vdproj`). It is NOT an RTG crane component. **But it shares the `TerminalData` database** — 80 `sp_ForkLift*` stored procedures (priority 4 in `01_target_objects.txt`) read/write tables RTG also uses (e.g. `CO_Containers`, `TB_Location`, `RG_Shifting`). This is an **integration boundary** for Phase 2's dual-write plan: during the transition, the legacy MSSQL side must keep behaving correctly so that ForkliftApp continues to function.

---

## 2. Technology stack and build constraints

### 2.1 Listener + Persistence (`TOSConsole{1,2,3}`)

| Attribute | Value | Evidence |
|---|---|---|
| Language | C# | `*.csproj` |
| Runtime | **.NET Framework 4.8** | `TOSConsole1.csproj:13` — `<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>` |
| Output type | `Exe` (console) | `TOSConsole1.csproj:9` |
| Platform | **x86** | `TOSConsole1.csproj:5,23,34` |
| Tooling | `ToolsVersion="12.0"` (Visual Studio 2013-era msbuild; still builds in modern VS) | `TOSConsole1.csproj:2` |
| Warning level | **0** for Debug (i.e. warnings suppressed) | `TOSConsole1.csproj:30` |
| References | Only BCL: `System`, `System.Core`, `System.Xml.Linq`, `System.Data.DataSetExtensions`, `Microsoft.CSharp`, `System.Data`, `System.Xml` | `TOSConsole1.csproj:43-51` |
| External dependencies | None (SQL Server accessed via `System.Data.SqlClient` from BCL) | `Crc32.cs:7` |
| Deployment shape | Single 17-18 KB `.exe` + single `app.config` (empty) + `.pdb` | `RTG/Console{1,2,3}/` |

**Shared structure of the three projects (same except constants):**

| Per-crane value | Console1 | Console2 | Console3 | Location in source |
|---|---|---|---|---|
| `AssemblyName` | `TOSConsole1` | `TOSConsole2` | `TOSConsole3` | `TOSConsole{n}.csproj:12` |
| TCP listen port | `30701` | `30702` | `30703` | `TOSConsole{n}/Program.cs:118` |
| TCP listen IP | `192.6.1.8` (hard-coded in all three, but only Console1's constructor `server = new TcpListener(port)` ignores it in practice — unclear if that's intended) | same | same | `Program.cs:119` |
| `CHE` constant in SQL | literal `'GOLD1'` everywhere | `'GOLD2'` | `'GOLD3'` | numerous `Program.cs` lines |
| Connection string | `Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;Integrated Security=SSPI;` — **hard-coded in source, not in config** | same | same | `Crc32.cs:45` |
| Log path | `\\Broadcast\Logs\RTG\RTG{1}\TOSConsole{1}\Log -MM-dd-yyyy.txt` | `\\Broadcast\Logs\RTG\RTG{2}\TOSConsole{2}\…` | `\\Broadcast\Logs\RTG\RTG{3}\TOSConsole{3}\…` | `Program.cs:377` |
| `TB_Parameters` flag used on PICK | `ContainerPick1` | `ContainerPick2` | `ContainerPick3` *(needs confirmation — only Console1 inspected)* | `Program.cs:261` |

### 2.2 Operator UI (`RTGApp` variants)

| Attribute | Value | Evidence |
|---|---|---|
| Language | C# (Windows Forms) | `.csproj`, `*.Designer.cs` |
| Runtime | **.NET Framework 4.8** | `app.config` in RTG1-2: `supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8"` |
| Output type | `WinExe` | `RTGApp1-2.csproj` |
| Platform | x86 | `RTGApp1-2.csproj` |
| SQL access | `System.Data.SqlClient` direct | `Class1.cs:1-10` |
| DataSet tooling | Typed DataSets (`TerminalDataTestDataSet{,1}.xsd`) bound to `RG_A1` and `RG_B3` | `.xsd` files |
| Deployment shape | Single 562-567 KB `.exe` + `.pdb` + `app.config` + `.ico` + 1-2 images (per-crane) | `Current/RTG1-2/` and `Current/RTG3/` |

### 2.3 Watchdog (`ConsolesReRun`)

| Attribute | Value |
|---|---|
| Runtime | **.NET 6 / 7 / 8-style single-file** — ships with its own `ConsolesReRun.runtimeconfig.json` and bundled `System.Data.SqlClient.dll` |
| Deploy contents | `ConsolesReRun.exe` (147 KB) + `.dll` (7 KB) + `.deps.json` + `System.Data.SqlClient.dll` + `sni.dll` + `runtimes/` |
| Source | Uses **top-level statements** (C# 9+) |

### 2.4 Framework / runtime matrix — summary

| Component | Runtime | Why it matters |
|---|---|---|
| `TOSConsole{1,2,3}` | .NET Framework 4.8 (Windows only) | Constrains migration target — Phase 2 can/should break this (move to .NET 8 Linux container or equivalent) |
| `RTGApp` | .NET Framework 4.8 + WinForms (Windows only) | Will be replaced entirely by Flutter (per prompt) |
| `ConsolesReRun` | .NET 6+ (Windows) | Self-contained; already modern |
| `TOSService` | .NET Framework 4.8 (likely — non-buildable in current state) | Disposable |

---

## 3. Configuration surface — where things are configured today

| Setting | How it is configured today | Where | ⚠ |
|---|---|---|---|
| DB connection (listener/persistence) | **Hard-coded string literal** | `TOSConsole{1,2,3}/Crc32.cs:45`, `TOSService/Crc32.cs:43` | Must be read from a real config in new system |
| DB connection (Operator UI, RTG1-2) | `Settings.Default.TerminalDataConnectionString` → `app.config`. Runtime override present in `Current/RTG1-2/RTGApp.exe.config:10`: `Data Source=192.6.8.52;Initial Catalog=TerminalData;Integrated Security=True` | |
| DB connection (Operator UI, RTG3) | `Settings.Default.TerminalDataConnectionString` → `app.config`. But the **deployed `Current/RTG3/RTGApp.exe.config` is empty**, so the app falls back to the compiled-in default value. | `From TFS/RTG/RTG3/RTGApp/RTGApp/Properties/Settings.settings:11` | 🚨 See §4 — the compiled-in default contains a **hard-coded password in cleartext** |
| Which crane is this RTGApp instance? (RTG1-2) | Reads first line of `C:\RTG\CHEName.txt` at load | `FrmLogin.cs:244-252` | File contents are `BOND1` or `BOND2` per `Current/RTG1-2/Read Me !.txt:1-6` |
| Which crane is this RTGApp instance? (RTG3) | **Hard-coded** literal strings `GOLD3` / `Bond3` | `RTG3/.../FrmLogin.cs:232-237` | RTG3 is a different code fork, not just different config — see §5 |
| TCP listen port / IP | Hard-coded in `Program.cs` | `TOSConsole{n}/Program.cs:118-119` | Different literal per crane |
| Log destination | Hard-coded UNC `\\Broadcast\Logs\RTG\…` | `TOSConsole{n}/Program.cs:377`, `TOSService/Program.cs:319`, `RTG1-2/RTGApp.exe.config:17` | |
| `ConsolesReRun` target paths | Hard-coded: `E:\RTG\Console{n}\TOSConsole{n}.exe` | `ConsolesReRun/Program.cs:11,21,31` | |
| "Start" signal paths (RTG1-2 UI) | `C:\RTG\CHEName.txt` read on every `FrmLogin_Load` | `FrmLogin.cs:246` | |

**Summary:** *There is no central configuration.* The system's operational profile is scattered across 6 or 7 distinct configuration mechanisms — file-based flags, hard-coded literals, separate per-crane csproj files, and deploy-time placement on specific folder paths. Phase 2 must consolidate.

---

## 4. ⚠ Security / secrets findings

1. **🚨 RTG3 RTGApp ships with a hard-coded SQL-Server password in the compiled binary.**
   In `From TFS/RTG/RTG3/RTGApp/RTGApp/Properties/Settings.settings:11`:
   ```
   Data Source=192.6.8.52;Initial Catalog=TerminalData;User ID=" + SystemUserName + ";Password=z3334606*
   ```
   Two problems:
   - **Password `z3334606*` is in source control.** It is also baked into the deployed `RTG3/RTGApp.exe` (since the runtime `RTG3/RTGApp.exe.config` is empty and contains no override).
   - The **connection string is malformed**. `User ID=" + SystemUserName + "` is a literal — the developer apparently intended to concatenate a variable at design time, but the design-time value was stored as-is and the build never fixed it. Net effect: either (a) the DB has a real user literally named `" + SystemUserName + "` (highly unlikely), or (b) RTG3's UI cannot actually connect with this connection string and something else is happening at runtime. This needs to be **verified against the running instance** (Q-02, Q-06).

2. **RTG1-2 RTGApp uses Integrated (Windows) Security** — no password needed. This is also the *only* RTGApp variant with a working `connectionString` in its deployed `RTGApp.exe.config`.

3. **All Listener/Persistence instances** (`TOSConsole{1,2,3}`, `TOSService`) use Integrated Security (`SSPI`). Password-free, but the **server IP (`192.6.8.52`) and DB name (`TerminalData`) are hard-coded in source**.

4. **UNC log path `\\Broadcast\Logs\RTG\…`** exposes an internal server name (`Broadcast` / `broadcaST`).

5. **ForkliftApp `_TemporaryKey.pfx`** — see `01_inventory.md` §7.

6. **SQL injection is ubiquitous.** In every component — Listener, Persistence, UI, log-writer — user input, crane-transmitted data, and internal strings are concatenated directly into SQL. Examples:
   - Listener writes: `"UPDATE RG_A1 set Time='" + data.Substring(12,6) + "'…"` — `data` is network input (`TOSConsole1/Program.cs:225-236`). An attacker who can connect to port 30701 and speak the protocol can inject arbitrary SQL against `TerminalData` under the SSPI user context.
   - UI login: `"…AND (dbo.HR_Emp.UserPinCode = " + Password.Text + ")"` (`FrmLogin.cs:74`).
   - UI log helper: `"INSERT INTO RG_ErrorLog … Msg='" + strLog + "')"` (`Utils.cs:26`). RTG3's log helper *does* use `.Replace("'","''")` (`ContainerLocation.cs:302`) — partial mitigation. RTG1-2's does not.

7. **The UI authentication logic accepts a PIN that matches *any* operator in group 22**, not specifically the selected `LoginName`. `FrmLogin.cs:69-75` `COUNT(*) WHERE UserGroupCode=22 AND UserPinCode=<PIN>` — the selected `LoginName` is only used to look up the `EmpID` after the PIN check passed. If two operators share the same PIN (or one's PIN is guessed) anyone can log in as anyone. The INSERT into `RG_Log` then uses the *selected* `LoginName` — so the audit trail will show the operator the user **picked**, not whose PIN was used.

---

## 5. Per-crane differences (RTG1-2 vs RTG3 — the schism)

This is the single most important finding for the redesign because it determines whether we can unify all three cranes or must preserve the fork.

### 5.1 Code-level differences

| Area | RTG1-2 | RTG3 |
|---|---|---|
| **csproj** | `RTGApp1-2.csproj` | `RTGApp3.csproj` (plus a stale `RTGApp.csproj`) |
| **Identity resolution** (which crane am I?) | Reads `C:\RTG\CHEName.txt`; supports two values — `BOND1`, `BOND2` (per `Read Me !.txt`). CHE is set from that file on every form-load. | Hard-coded `CHE='GOLD3'`, `LiftBlockName='Bond3'`, `LiftBlockName.Enabled = false` (cannot be changed by operator). See `RTG3/.../FrmLogin.cs:232-237` |
| **Logging (DB)** | `Utils.cs` (separate class). Program version string baked into source: `2025-08-04`. No SQL-quote escape in `Msg`. | `ContainerLocation.WriteLog` (method on the container-location form). Program version: `2025-01-08.1`. **Does** escape single quotes: `strLog.Replace("'", "''")` |
| **Logging (DB) — hardcoded CHE value** | Uses dynamic `FrmLogin.StrCHE` | Literal `'GOLD3'`, `'BOND3'` (`ContainerLocation.cs:302`) |
| **DB connection** | Integrated Security (Windows auth) | Username placeholder + hard-coded password (see §4 #1) |
| **Form sizes / form contents** | See `Form1.cs:116 757` bytes vs RTG3's `Form1.cs:116 626` bytes — near-identical but not byte-identical. `FrmContainerNoLocation.cs`: 2 591 vs 2 818. `FrmInOutDiory.cs`: 4 777 vs 5 401. Differences are small but exist throughout. | |
| **Error handling on login** | No try/catch around `btnOK_Click` body | Wrapped in `try/catch` with logging (`RTG3/.../FrmLogin.cs:50,92-95`) |
| **CodeFile1.cs** | not present | 3-byte empty stub in RTG3 — leftover |

### 5.2 Database-level differences (already visible in DB export)

From `01_target_objects.txt`:

| Object type | Bond1/Bond2 specific | Bond3 / RTG3 specific | Note |
|---|---|---|---|
| Views | `V_MapRTGBond1`, `V_MapRTGBond2` | — | There is **no `V_MapRTGBond3`**. The RTG3 UI does not use the "Map" view. |
| Views | `V_RTGLoad`, `V_LocationCount`, `V_ContainerUnloadRG` | `V_RTG3Load`, `V_LocationCountRTG3`, `V_ContainerUnloadRTG3` | RTG3 has its own "counts" and "load" views. |
| Stored procs | `KillToss1`, `KillToss2`, `RunEnconsoleRTG1`, `RunEnconsoleRTG2` | `KillToss3`, `RunEnconsoleRTG3` | 1:1 copies per crane, again. |

### 5.3 Operational / deploy differences

| Aspect | RTG1-2 | RTG3 |
|---|---|---|
| Deploy folder | `Current/RTG1-2/` — contains both the UI `.exe` (in folder root) **and** a nested `RTG/RTGApp/` with a Jan-2022 working copy of the **source** | `Current/RTG3/` — just the `.exe`, `.exe.config`, `.pdb` |
| Icon files | `rtg1.ico`, `rtg2.ico`, `rtg3.ico` all present in RTG1-2 (unclear why #3 is here) | only references `truckyellow.ico` |
| Background image | `istockphoto-700381864-170667a.jpg`, `maersk4.png` | `maersk4.png` |
| Last binary build date | 2023-01-23 | 2023-11-07 (10 months later) |

### 5.4 Why the split exists — hypothesis

Based only on artifacts (no user input yet), my hypothesis is:

> **Crane #3 was procured later**, on different mounting/coordinate conventions, with a DB user instead of Windows auth, and the developer branched a per-crane fork of RTGApp rather than refactoring the single app to be per-crane configurable. Over time the forks drifted independently. The DB side mirrors the split through `*RTG3*` views and SPs.

This needs confirmation from the user (see Q-07). If the hypothesis is right, it supports **choosing Crane #3 as the pilot** for the migration (simpler to replace a non-critical fork with a parameterized modern app).

---

## 6. Data-access pattern — consistent across the code-base

Every component uses the same pattern, with small variations:

```csharp
// From Class1.cs:21-126 (UI) and Crc32.cs:35-79 (Listener)
public DataTable ReturnDT(string instr)
{
    SqlConnection connection = new SqlConnection();
    // connection string from Settings (UI) or hard-coded (Listener)
    // …
    command.CommandText = instr;
    DataTable dt = new DataTable();
    if (instr.Substring(0, 6) == "SELECT")
    {
        adapter.SelectCommand = command;
        adapter.Fill(dt);
    }
    else
    {
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
        dt.Columns.Add(instr.Substring(0, 6));
    }
    return dt;
}
```

Characteristics of this pattern that will shape the redesign:

- **String-concatenation SQL only** — no parameterisation.
- **Classification by `instr.Substring(0,6) == "SELECT"`** — anything else (UPDATE / INSERT / DELETE / MERGE / …) goes through the non-SELECT branch. Breaks silently for `WITH` (CTE), `EXEC`, stored-procedure calls by name, single-line `select count(*)` written lower-case, etc.
- **No connection lifetime management** — `connection.Close()` only on the non-SELECT branch; `Dispose()` / `using` never used. Leaked connections plausibly hidden by SqlClient's connection pool. This is one contributing factor to the huge `RG_ErrorLog` (1.6M rows) seen in the DB — the app catches everything and re-inserts it.
- **Returns `null` on exception** (in `Crc32.cs`) — callers don't null-check; on SQL failure the listener either crashes or processes a null DataTable.
- **Two completely separate copies of this class** exist (`Crc32.cs` file in the Listener and `Class1.cs` file in the UI) — no shared data-access library.
- **Settings-driven connection string is ignored in listener code** — even if you configured the DB differently in app.config, the listener would still connect to `192.6.8.52`.

---

## 7. Build / deploy artifacts to be flagged

| Artifact | Status | Action for Phase 2 |
|---|---|---|
| `From TFS/RTG/BuildProcessTemplates/*.xaml` | Legacy TFS XAML builds | Discard |
| `From TFS/RTG/RTGApp/RTGApp/RTGApp.csproj` | Pre-fork common RTGApp | Treat as reference only; real sources are in RTG1-2/RTG3 subtrees |
| `From TFS/RTG/RTG3/RTGApp/RTGApp/RTGApp.csproj` | Duplicate of `RTGApp3.csproj` in same folder | Delete during cleanup |
| `From TFS/RTG/RTG3/RTGApp/RTGApp/CodeFile1.cs` | 3-byte stub | Delete |
| `Current/RTG1-2/RTG/` | Old local working copy from 2022 | Delete (TFS has newer) |
| `Current/RTG/Install/ndp48-x86-x64-allos-enu.exe` | .NET 4.8 installer (vendor) | Keep only as operational prerequisite for legacy cabin PCs; does not need to be in migration repo |
| `Current/RTG/LastWorkingVer/2022/06/*` + `RTG/Console{1,2}/LastWorking*/` + `RTG/Console2/NewVer/` | Historical binary backups | Archive outside repo |

---

## 8. Per-component "What the new system must replicate" checklist

> Short-form only; full behaviour goes in 03–07.

### 8.1 Listener + Persistence replacement must do all of:
1. Bind TCP sockets 30701/30702/30703 (or the equivalent new ports) and accept connections from each crane.
2. Read fixed-width ASCII packets with the `??` header + `A1` / `A2` / `A3` message-type discrimination — preserving byte offsets and `calcChecksum` semantics (`TOSConsole1/Program.cs:30-42`, `:219-336`).
3. Send outbound broadcasts from `RG_B3` (pending jobs) at connection-open time.
4. Respond with acknowledgments: `FFFF` + hex(`04B2` + seq + checksum) for A2, fallback `FFFF303342313046454641` for unknown types.
5. Update `RG_A1` on A1 packets — 11 columns derived from packet offsets.
6. Update `RG_B3`, `TB_Location`, `RG_Shifting`, `CO_Containers`, `TB_Parameters`, `RG_Container` on A2/03-PICK and A2/04-PLACE.
7. Mark `RG_B3.CancelDate` / `A3Date` on A3.
8. Log everything to a file system path that can be centralised.
9. Restart on failure (today via `goto START` + `ConsolesReRun`).

### 8.2 Operator UI replacement must do all of:
1. Replace FrmLogin (Hebrew RTL, on-screen numeric keypad, combobox of operators in UserGroupCode=22).
2. Replace the 7 menu items in FrmMenu.
3. Replace FrmMap01 (the complex main screen — details in `06_operator_ui.md`).
4. Replace ContainerLocation (update container location dialog).
5. Replace Works / FrmWorks screen.
6. Work offline-tolerantly for intermittent DB connectivity.
7. Record operator activity into `RG_Log` on login.
8. Be configurable per-crane (BOND1 / BOND2 / GOLD3 / future) from a single code base.

### 8.3 Watchdog replacement:
Absorb `ConsolesReRun` and `KillToss*` into proper process supervision (systemd, Kubernetes liveness probes, Windows Service recovery actions — to be decided in Phase 2).

---

## 9. Check-in summary

- **Listener + Persistence are one process** (not separable), repeated three times (copy-paste per crane). Only differences: TCP port, `CHE` literal, and one `TB_Parameters` flag name. All SQL is hand-built string concatenation; all connection strings are hard-coded; `\\Broadcast\Logs\RTG\…` is the log sink.
- **Operator UI is one WinForms app forked into RTG1-2 vs RTG3.** The fork is real — RTG3 has hard-coded identity (GOLD3/Bond3), a different DB connection (with a **plaintext password in `Settings.settings`**), a different logging helper, and somewhat different form code-sizes. This is the single biggest unification risk.
- **Biggest security flags to share with the user immediately:** (1) `z3334606*` SQL password in source + baked into `RTG3/RTGApp.exe`; (2) SQL injection end-to-end, reachable from the network via port 30701-30703; (3) login accepts any group-22 operator's PIN (audit trail records the LoginName the operator picked, not the PIN's real owner).
- **Next:** Step 1.3 — walk the full data flow, crane→listener→DB→UI→crane, including the 5 edge-case scenarios listed in the prompt.
