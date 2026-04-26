# 08 — Reverse Engineering of Binaries

> **פרויקט:** Goldbond · מודרניזציית מערכת איתור מנופי RTG
> **מחבר:** Mary, האנליסטית העסקית (BMad)
> **תאריך:** 26 באפריל 2026
> **שלב:** שלב 1 — גילוי, צעד 8 מתוך 9
> **קלטים:** קבצי `.exe`, `.dll`, `.pdb`, ו-`.deps.json` תחת `CurrentSystem/Current/RTG/` וב-`RTG1-2/`, `RTG3/`. **הסבירות**: לא היו לי כלי decompiler זמינים בסשן הזה (ILSpy / dnSpy / dotnet-ildasm). RE מלא של ה-IL נדרש בכלים חיצוניים — ראו §6.
> **מטרת המסמך:** לאסוף כל מה שניתן ללמוד על הבינארים בלי להריץ decompiler — סוג קובץ, framework, תלויות, גודל, תאריך build, נוכחות PDB, embedded configs, ו-listening ports — ולגבש רשימת-עדיפות ל-RE עתידי בכלים מתאימים.
> **מצב:** טיוטה לבדיקה ואישור.

> **סולם הוודאות:** כל ממצא יסומן כ-`✓ CONFIRMED` (מוודא מקובץ-מטא או דוקומנטציה ישירה), `~ INFERRED` (היגיון/השוואה), `? UNKNOWN` (לא ניתן לקבוע בלי כלים נוספים).

---

## 1. תקציר מנהלים

קיימים **15 קבצי `.exe`** פרוסים ב-`CurrentSystem/Current/RTG/` ו-`RTG1-2/`, `RTG3/`. רובם בנויים על .NET Framework 4.8 (ה-`.exe.config` שלהם מוצהר v4.8) או על .NET Core / .NET 6.0 (`ConsolesReRun`, `TosReRun`). שלושה בינארים הם ה-listeners בייצור (`TOSConsole1/2/3.exe`), אחד הוא ה-listener-בפועל-של-המנופים (`TOSService.exe`, אישור 2026-04-26), ושני watchdogs פרוסים במקביל (`ConsolesReRun.exe` ו-`TosReRun.exe`). **`TosReRun.dll` בלבד 7,168 בייטים** — תוכן זעום לעומת התלויות (~3.5MB של Azure DLLs!). העובדה שהוא טוען Azure.Identity + Microsoft.Data.SqlClient נראית מטרה — אבל הקוד עצמו זעיר. ככל הנראה הוא מחבר ל-DB עם MSAL/AAD, אבל אין לדעת בלי decompile.

**שני אזורי-RE קריטיים** לעיצוב Phase 2:
1. **`TOSService.exe` 2022-06-09** — הליסטנר בייצור. אנו זקוקים לקוד-מקור הקאנוני (לא הטיוטה הנוכחית שלא קומפלת). זוהי המסר הראשון.
2. **`TosReRun.dll` 2022-06-09** — watchdog שני, אין לו קוד-מקור ב-`From TFS/`. תלויותיו מצביעות על Azure SQL או על AAD authentication. צריך להבין מה הוא **בעצם עושה**.

---

## 2. אינוונטר הבינארים

### 2.1 ה-Listeners (TCP)

| בינארי | תאריך build | גודל | Framework | PDB? | סטטוס | הערות |
|---|---|---:|---|:---:|---|---|
| `RTG/TosService/TOSService.exe` | **2022-06-09 12:37** | 17,920 B | .NET 4.x (Framework) | ✓ | **Active in production (אישור 2026-04-26)** | ה-listener שמדבר עם המנופים. קוד-המקור הנוכחי לא קומפלי. נדרש RE לחילוץ הלוגיקה האמיתית |
| `RTG/Console1/TOSConsole1.exe` | 2024-10-29 15:32 | 17,408 B | .NET 4.8 | ✓ | סטטוס בייצור לא ידוע (Q-COMP-02) | הבינארי המעודכן ביותר. אם רץ בייצור — מקביל ל-TOSService |
| `RTG/Console2/TOSConsole2.exe` | 2024-10-29 15:32 | 17,920 B | .NET 4.8 | ✓ | סטטוס בייצור לא ידוע | זהה לעיל |
| `RTG/Console3/TOSConsole3.exe` | 2023-09-06 13:43 | 17,920 B | .NET 4.8 | ✓ | סטטוס בייצור לא ידוע | הוותיק מבין השלושה |
| `RTG/Console2/NewVer/TOSConsole2.exe` | 2024-08-14 10:30 | 17,920 B | .NET 4.8 | ? | "NewVer" — גיבוי של 2024-08 | סוג של staging |
| `RTG/Console2/LastWorkingVer/2024/08/TOSConsole2.exe` | 2023-09-06 13:43 | 17,920 B | .NET 4.8 | ? | "LastWorkingVer" — גיבוי | שמות לא תואמים את התאריך! |
| `RTG/LastWorkingVer/2022/06/Console{1,2,3}/TOSConsole{n}.exe` | 2022-03-21 / 2022-03-24 | 17,920-18,432 B | .NET 4.8 | ? | פוסיל לא בשימוש | גיבוי-קור מ-2022 |
| `RTG/Console1/LastWorkingVersion/...` | (לא נבדק) | — | — | — | פוסיל-נוסף | — |

**🔥 העובדה הקריטית:** ה-`TOSService.exe` שבייצור (לפי אישור Yaniv) הוא **הוותיק ביותר** מבין כל ה-listeners — מ-2022-06-09. זה אומר שהוא לא מוחזק בעדכונים פעילים (אף שהמקור שלו תוקן לאחרונה ב-2026-04-21 אבל לא הצליח לקמפל).

### 2.2 ה-Watchdogs

| בינארי | תאריך build | גודל DLL | Framework | תלויות עיקריות |
|---|---|---:|---|---|
| `RTG/ConsoleReRun/ConsolesReRun.exe` | **2023-01-23** | 7,168 B (DLL) + 147,968 B (EXE) | **.NET 6.0** (per `runtimeconfig.json: tfm=net6.0`) | `System.Data.SqlClient 4.8.5` בלבד |
| `RTG/ReRun/TosReRun.exe` | **2022-06-09** (אותו יום של TOSService!) | 7,168 B (DLL) | **.NET 6.0** | `Microsoft.Data.SqlClient 4.1.0`, `Azure.Identity 1.3.0`, `Microsoft.Identity.Client (MSAL) 4.22.0`, `System.IdentityModel.Tokens.Jwt`, ועוד 20+ |

**מסקנות מהתלויות:**
- `ConsolesReRun` הוא watchdog "פשוט" — רק SqlClient. סביר שעושה רק kill+restart לפי קריאה ל-DB (כי קוד המקור מ-`From TFS/RTG/ConsoleReRun/ConsolesReRun/Program.cs` שראינו קודם **מאשש**: kill TOSCONSOLE{1,2,3} ואז `Process.Start(@"E:\RTG\Console{n}\TOSConsole{n}.exe")`).
- `TosReRun` הוא watchdog "מסובך יותר" — תלויות AAD/MSAL מצביעות על:
  - אופציה 1: מתחבר ל-Azure SQL Database עם token-based auth.
  - אופציה 2: מתחבר ל-on-prem MSSQL **דרך Azure AD** (שילוב מודרני שתומך ב-`Authentication=Active Directory Default`).
  - אופציה 3: התלויות bundled-by-default אבל לא בשימוש בפועל.
- `TosReRun.dll` הוא **7,168 בייטים בלבד** — גודל זהה ל-`ConsolesReRun.dll`. סביר שמדובר בקוד דומה (kill+restart) שנוצר עם תבנית Visual Studio שכוללת אוטומטית את התלויות הללו.

### 2.3 Operator UI binaries

| בינארי | תאריך build | גודל | Framework | PDB? | source-of-truth |
|---|---|---:|---|:---:|---|
| `RTG1-2/RTGApp.exe` | 2023-01-23 | 567,296 B | .NET 4.8 | ✓ | `From TFS/RTG/RTG1-2/RTGApp/RTGApp/RTGApp1-2.csproj` |
| `RTG1-2/RTG/RTGApp/bin/Debug/RTGApp.exe` | 2023-01-23 | 567,296 B | .NET 4.8 | ✓ | אותו source — debug build |
| `RTG3/RTGApp.exe` | 2023-11-07 | 562,176 B | .NET 4.8 | ✓ | `From TFS/RTG/RTG3/RTGApp/RTGApp/RTGApp3.csproj` |

**RTG3 binary מוקדם בעוד 10 חודשים מ-RTG1-2** (Nov 2023 vs Jan 2023). שונה ב-5KB בלבד — שינויים מינימליים בין הענפים, כפי שצפינו בצעד 2.

### 2.4 KillToss + ForkliftApp

| בינארי | תאריך build | מקור | הערות |
|---|---|---|---|
| `From TFS/.../KillToss{1,2,3}.exe` | (לא בודק זמנים — נקיים בקוד-מקור) | `KillToss{n}/Program.cs` | קוד מינימלי — `Process.GetProcessesByName("TOSCONSOLE{n}").Kill()` |
| ForkliftApp.exe (פרוס ב-Goldbond — לא ב-repo) | (לא בקוד-המקור) | `From TFS/RTG/New Folder/ForkliftApp/` | המקור קיים; הבינארי לא בעצי-המקור שראינו |

---

## 3. ניתוח-עומק לפי בינארי

### 3.1 `TOSService.exe` — הליסטנר בייצור

| מאפיין | ערך | בטחון |
|---|---|---|
| תאריך build | 2022-06-09 12:37:?? | ✓ CONFIRMED (filesystem timestamp) |
| גודל | 17,920 בייטים | ✓ CONFIRMED |
| Framework | .NET Framework 4.x | ~ INFERRED (per `.exe.config` של גרסת המקור הנוכחית) |
| `.pdb` | קיים, 26,112 בייטים | ✓ CONFIRMED — מאפשר RE עם שמות סימליים |
| `.exe.config` | קיים (161 בייטים, 2022-03-13) | ✓ CONFIRMED — קטן, מציין רק supportedRuntime |
| Listening port | משוער 30702 בלבד (כי המקור-בטיוטה מציין רק `TOS("Gold2", 30702)`) | ? UNKNOWN — דורש RE לאמת |
| Listening IP | משוער `192.6.8.52` (לפי המקור-בטיוטה) | ~ INFERRED |
| מבצע handshake עם DB | `192.6.8.52` SSPI (אותו class `Crc16Ccitt`) | ~ INFERRED — דורש RE |
| Cranes שהוא מטפל | רק GOLD2 (per source); או 3 (אם הקוד הקאנוני שונה) | ? UNKNOWN |

**שאלות לחקירה ב-RE:**
1. האם `Main` הוא `static` בקוד הקאנוני? (חייב להיות — אחרת לא רץ).
2. האם 3 ה-`TOS()` calls פעילים, או רק GOLD2?
3. האם `stream.Read` בלולאת `while`, או single-shot?
4. האם יש `if (G || T)` או רק `if (T)` ב-PLACE branch?
5. האם יש Web triggers integration כלשהי?

### 3.2 `TosReRun.exe` — Watchdog שני (.NET 6, AAD-aware)

| מאפיין | ערך | בטחון |
|---|---|---|
| תאריך build | 2022-06-09 (אותו יום של TOSService — לא מקרי) | ✓ CONFIRMED |
| גודל DLL | 7,168 בייטים (זעום) | ✓ CONFIRMED |
| Framework | .NET 6.0 | ✓ CONFIRMED (deps.json) |
| תלויות מרכזיות | `Microsoft.Data.SqlClient 4.1.0`, `Azure.Identity 1.3.0`, `Microsoft.Identity.Client 4.22.0`, `System.IdentityModel.Tokens.Jwt` | ✓ CONFIRMED (deps.json) |
| Source code | **לא נמצא** ב-`From TFS/RTG/` | ✓ CONFIRMED |
| תפקיד מוערך | watchdog מקביל ל-ConsolesReRun, אולי עם connection ל-Azure SQL | ~ INFERRED |
| Hard-coded paths/IPs/strings | ? UNKNOWN — דורש decompile |

**הסבר אפשרי לקיום שני watchdogs:**
- ייתכן ש-`TosReRun` הוא הניסיון הראשון (2022) שלא הצליח, ו-`ConsolesReRun` (2023) הוא הניסיון השני שעובד.
- שניהם פרוסים יחד כי איש לא הסיר את הראשון.

**שאלות לחקירה ב-RE:**
1. מה ה-`Main` עושה?
2. האם הוא מחבר ל-DB? אם כן — לאיזה?
3. האם ה-Azure dependencies בשימוש בפועל או רק bundled?
4. אם רץ בייצור — בקצב מה? (Task Scheduler? Service?)

### 3.3 `ConsolesReRun.exe` — Watchdog ראשי

| מאפיין | ערך | בטחון |
|---|---|---|
| תאריך build | 2023-01-23 | ✓ CONFIRMED |
| Framework | .NET 6.0 | ✓ CONFIRMED |
| Source code | קיים — `From TFS/RTG/ConsoleReRun/ConsolesReRun/Program.cs` | ✓ CONFIRMED |
| לוגיקה | kill TOSCONSOLE{1,2,3} ואז Process.Start `E:\RTG\Console{n}\TOSConsole{n}.exe` | ✓ CONFIRMED מהמקור |
| בעיה ידועה | אין delay בין kill ל-start; הנתיבים `E:\RTG\Console{n}\` שונים מ-`C:\RTG\RTG{n}\` של ה-SP | ✓ CONFIRMED |

### 3.4 `RTGApp.exe` (RTG1-2 ו-RTG3)

| מאפיין | RTG1-2 | RTG3 |
|---|---|---|
| תאריך build | 2023-01-23 | 2023-11-07 |
| גודל | 567,296 B | 562,176 B |
| Framework | .NET Framework 4.8 | .NET Framework 4.8 |
| Source-of-truth | `RTGApp1-2.csproj` | `RTGApp3.csproj` (וגם `RTGApp.csproj` נוסף!) |
| Connection string ב-binary | משתנה `Settings` שנוצר בזמן build (לא ב-config) — Q-COMP-04 | אותו דבר |
| Hardcoded paths | `C:\RTG\CHEName.txt`, `\\Broadcast\Logs\RTG\` | זהה |

**בדיקה ב-RE שאצטרך:** מאיפה ה-binary לוקח את ה-connection string בייצור (כי ה-`.exe.config` ריק). ה-`Properties\Settings.settings` מ-Visual Studio נצפה את הערך בזמן build. ייתכן שיש `User-Scoped` settings ב-`%LOCALAPPDATA%`.

---

## 4. PDB files — מקור-מידע לתוויות

PDB files של .NET מכילים שמות-סימליים של מתודות, namespaces, וכ"כ paths של קוד-המקור (למפתח שבנה). אפשר לשלוף מהם:
- שמות classes ומתודות.
- Source paths (גילוי מי בנה ומאיפה).
- Line numbers.

**בלי כלי חיצוני** (`dotnet-symbol`, `pdb2json` וכד') לא הצלחתי להוציא תוכן בר-קריאה מ-PDB-ים בסשן הזה. **שאלה ל-RE עתידי:** PDB של `TOSService.exe` 2022 יחשוף את שמות המתודות ואולי `MachineName\username` של מי שבנה אותו — מידע יקר לעבר היסטוריה.

---

## 5. ניתוח אבטחה של הבינארים

| בינארי | מצב חשוף | סיבה |
|---|---|---|
| `TOSService.exe` | ⚠ אם פגום או מוחלף — שליטה מלאה ב-DB (SSPI ל-`192.6.8.52`) | כל מי שיש לו root על שרת ה-listener יכול להחליף בקוד שכותב ל-DB אחרת |
| `TosReRun.exe` | ⚠ אם מתחבר ל-Azure — secret נמצא איפשהו | המבנה שלו לא ידוע; אם יש creds — איפה מוצפנות? |
| `ConsolesReRun.exe` | ✓ — עובד עם Process.Start בלי secret | בטוח יחסית |
| `RTGApp.exe` (1-2 ו-3) | ⚠ אם ה-connection string ב-Settings.settings — הוא ב-binary בטקסט גלוי | RTG1-2 binary בעבר (per docs/discovery, שלא נקרא במאמץ הנוכחי) הכיל `User ID=malgezot;Password=12345678` כ-fallback. נדרש לאמת ב-RE |
| `KillToss{n}.exe` | ✓ — עושה רק Kill | בטוח |
| `ForkliftApp.exe` | 🚨 — ה-source מכיל `sa/z3334606*` בקוד; הבינארי כנראה כולל זאת | רוטציית סיסמה דחופה |

---

## 6. כלי-RE הנדרשים לעומק

לחקירת הבינארים ב-Phase 2 (לפני שמתחילים ב-Architect work) — מומלץ:

| כלי | למה משמש | מקור |
|---|---|---|
| **ILSpy** | Decompiler חינמי .NET. יוציא C# קוד-מ-IL ב-fidelity טוב | https://github.com/icsharpcode/ILSpy |
| **dnSpy** | Decompiler + debugger אינטראקטיבי | https://github.com/dnSpyEx/dnSpy |
| **dotPeek** (JetBrains) | Decompiler מעולה (חינמי) | https://www.jetbrains.com/decompiler/ |
| **Wireshark + filter `tcp port 30701-30703`** | חבילת-תקשורת חיה בין PLC ל-listener | (קיים) |
| **DnSpy / dotnet-symbol** | חילוץ נתוני PDB | — |

**הליך מומלץ ל-RE של `TOSService.exe`:**
1. פתח עם ILSpy.
2. עבור על `Program.Main`. צפה: `static void Main(string[] args)`.
3. השווה לקוד הטיוטה הנוכחי של `TOSService/Program.cs`. הבדלים אמיתיים יחשפו מה ה-flow בייצור.
4. בייחוד תבדוק:
   - האם 3 ה-`TOS()` calls פעילים?
   - האם ה-`while` של stream.Read קיים?
   - האם ה-`if (G || T)` של PLACE קיים?
   - האם יש handlers שלא ראינו (heartbeat? PLC-side handshake?)
5. צא לעבר `TosReRun.dll` ובצע אותו דבר.

**הערה:** ה-RE הזה דחוף יותר מ-Phase 2 — הוא **קודם** ל-Architect, כי בלי הבנת הליסטנר בייצור — קל לבנות מחדש דבר שונה ולהפיל הופעה.

---

## 7. שאלות פתוחות שנפתחו בצעד הזה

| # | שאלה | למה זה חשוב |
|---:|---|---|
| Q-RE-01 | מה הקוד הקאנוני של `TOSService.exe` 2022? פיענוח דרוש | חיוני להבנת הליסטנר בייצור |
| Q-RE-02 | מה הקוד של `TosReRun.exe`? האם מחבר ל-Azure SQL? | תאר תשתית סודות-קונפיג |
| Q-RE-03 | האם ה-`Settings.settings` ב-`RTGApp` הפרוס מכיל credentials נוספים מעבר ל-app.config (כפי שהיה לכאורה ב-historic build)? | אבטחה של רוטציית סיסמה |
| Q-RE-04 | מה ה-PDB של `TOSService.exe` חושף לגבי המפתח שבנה (machine name, source path)? | היסטוריה |
| Q-RE-05 | האם יש בינארים נוספים פרוסים ב-prod שלא קיימים ב-`CurrentSystem/Current/RTG/`? (למשל, על שרתים אחרים) | היקף מלא |

---

## 8. צעדים הבאים

1. **המתנה לאישורך** על הסקירה ועל פערי-ה-RE שזיהינו.
2. **קריאה אופרטיבית:** RE של `TOSService.exe` ו-`TosReRun.exe` נדרש **לפני** ש-Architect מתחיל לתכנן את ה-listener החדש. אבקש ממך פתיחת access לכלי decompile (ILSpy) וזמן-מנהל לבצע זאת בנפרד.
3. בכפוף לאישור — מעבר ל-**צעד 9 (Open questions register)** — סינתזה של כל ~50 השאלות הפתוחות בכל הצעדים, מסומנות לפי קטגוריה ועדיפות.

---

> **סיום צעד 8.** המשך מותנה באישור.
