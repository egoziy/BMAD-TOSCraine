# 01 — אינוונטר מאגר המקור (Repository Inventory)

> **פרויקט:** Goldbond · מודרניזציית מערכת איתור מנופי RTG
> **מחבר:** Mary, האנליסטית העסקית (BMad)
> **תאריך:** 26 באפריל 2026
> **שלב:** שלב 1 — גילוי (Discovery), צעד 1 מתוך 9
> **שורש המקור הנסקר:** `CurrentSystem/Current/`
> **מטרת המסמך:** תמונת-על מהירה ומוצקה של נפח הקוד, סוגי הקבצים, טווחי הזמן, נקודות הכניסה, ומועמדים לקוד-מת או חוץ-היקף. זהו הצעד הראשון בלבד — סיווג רכיבים מועבר לצעד 2, ניתוח התנהגותי לצעד 3, וניתוח ה-DB לצעד 4.
> **מצב:** טיוטה לבדיקה ואישור.

> **הערה לקורא:** הניתוח שב-`docs/brownfield/` הוא **מאמץ עצמאי וטרי**. הוא אינו מתבסס על תכולת `docs/discovery/`, `docs/design/`, `docs/build/` או `docs/fixes/` שהן תוצרי ניסיון קודם. כל ממצא בא מקריאה ישירה של הקוד.

---

## 1. תקציר מנהלים

תיקיית המקור `CurrentSystem/Current/` מכילה **550 קבצים** המאורגנים ב-5 תיקיות-על. מבחינת שפות, הקוד הוא בעיקר .NET — **203 קבצי C#**, 70 קבצי `.resx` (משאבי WinForms), 18 קבצי `.csproj`, ו-14 קבצי `.sln`. תאריכי השינוי משתרעים על פני **כעשר שנים** — מ-2 ביוני 2016 ועד 21 באפריל 2026. הנגיעה האחרונה בקוד הייתה ב-`TOSConsole3` וב-`TOSService` ב-2026-04-21, מה שמעיד שהמערכת בתחזוקה פעילה ולא נטושה.

שלוש נקודות שכדאי להחזיק בראש כבר עכשיו, לפני הניתוח הלוגי:

1. **שכפול-מקור הוא הנורמה כאן, לא חריג.** קיימות **לפחות שלוש העתקות נפרדות** של פרויקט `RTGApp` (ה-UI של תא המנוף), ושלושה פרויקטי `TOSConsole` נפרדים — לא תצורות (configurations) של פרויקט אחד אלא solutions עצמאיים לחלוטין. זה ידחוף את צעד 2 (מיפוי רכיבים) להחזיק טבלת השוואה הדוקה בין הגרסאות.
2. **תיקייה אחת בעלת שם מסגיר — `New Folder/ForkliftApp/` — מכילה solution מלא של 5 פרויקטים שאינו נראה קשור ל-RTG** (אפליקציית-מלגזה, set שונה לחלוטין). יש לאשר מולך האם זה במכוון בהיקף או שריד שצריך להוציא ממנו.
3. **תיקיית `rtg-discovery/` (192 MB) אינה קוד מקור.** היא כוללת 9 קבצי טקסט שנוצרו ב-2026-04-21 והם תוצרי `BCP`/`SELECT` של מטא-נתוני בסיס הנתונים `TerminalData` שעל שרת `SQL01` (Microsoft SQL Server 2019 Standard, RTM-CU32-GDR, build 15.0.4460.4). זהו חומר-הגלם של צעד 4 (DB deep-dive).

---

## 2. ספירת קבצים לפי סיומת

| סיומת | מס׳ קבצים | משמעות |
|---|---:|---|
| `.cs` | 203 | קוד מקור C# |
| `.resx` | 70 | משאבי WinForms (טקסטים, אייקונים, layouts) |
| `.dll` | 49 | תלויות בינאריות |
| `.config` | 21 | קבצי תצורה (`app.config`, `*.exe.config`) |
| `.csproj` | 18 | פרויקטי .NET |
| `.png` | 16 | תמונות (לוגו, לחצנים, נופי טרמינל) |
| `.vspscc` + `.vssscc` | 24 | מטא-נתוני source control של Visual Studio (TFS legacy) |
| `.pdb` | 15 | סמלי דיבוג של .NET |
| `.exe` | 15 | בינארים מהודרים (deployed) |
| `.sln` | 14 | קבצי Solution |
| `.txt` | 12 | תיעוד / פרמטרים (כולל `CHEName.txt` הקריטי) |
| `.xss` + `.xsd` + `.xsc` | 33 | Schemas של Typed DataSet (תכונה ישנה של Visual Studio) |
| `.resources` | 13 | משאבים מקובצים (build artifacts) |
| `.json` | 4 | קונפיגי .NET Core (deps/runtime) — נמצאים ב-`ConsoleReRun` בלבד |
| `.sql` | 2 | סקריפטי DDL (אחד תחת `rtg-discovery`, אחד תחת `From TFS`) |
| `.xaml` | 3 | WPF (יש לוודא מי בעצם משתמש בזה — אנומליה במערכת WinForms) |
| `.settings` | 6 | קונפיגי Visual Studio per-user |
| אחרים | ~17 | jpg, ico, datasource, suo, cache |

הרכב השפות מצביע **ללא ספק על .NET WinForms** כטכנולוגיית הליבה. קיומם של 4 קבצי `.json` ב-`ConsoleReRun/` בלבד מסגיר שמדובר ברכיב יחיד שנכתב ב-.NET 6 (או חדש יותר), בעוד שאר הקוד נראה .NET Framework 4.8 (לפי קיום קובץ ההתקנה `ndp48-x86-x64-allos-enu.exe` תחת `RTG/Install/`).

---

## 3. גודל לפי תיקייה ראשית

| תיקייה | גודל | פירוט מהיר |
|---|---:|---|
| `rtg-discovery/` | 192 MB | תוצרי-ייצוא של מטא-נתוני MSSQL. הקובץ הגדול ביותר: `03_keys_foreign_keys_indexes.txt` ≈ 145 MB; אחריו `02_columns.txt` ≈ 53 MB |
| `RTG/` | 132 MB | תיקיית פריסה (deploy). הגוש הגדול ביותר: `Install/ndp48-x86-x64-allos-enu.exe` (~96 MB, מתקין .NET Framework 4.8). אחריו: `LastWorkingVer/2022/06/Console{1,2,3}/` (גרסאות-גיבוי) ועשרות DLLs של `Microsoft.Data.SqlClient`, `Azure.Identity`, וחבילות זהות נלוות תחת `ReRun/` ו-`ConsoleReRun/` |
| `From TFS/` | 6.9 MB | **קוד-המקור הקאנוני.** 13 פרויקטי `.sln`, ביניהם `RTG.sln` הראשי שמרכז את כל ה-solutions תחתיו |
| `RTG1-2/` | 5.8 MB | נראה כפריסה של תא-המנוף לקראנים 1+2 — מכיל את `RTGApp.exe`, `RTGApp.exe.config`, וגם **תת-תיקיית מקור משלה** (`RTG1-2/RTG/RTGApp/`) שהיא שכפול של אחד הענפים מ-`From TFS` |
| `RTG3/` | 973 KB | פריסת תא-המנוף לקראן 3 — **בינארים בלבד**, ללא מקור מוצמד. המקור נמצא ב-`From TFS/RTG/RTG3/RTGApp/` |

**מסקנה:** ההצהרה בבריף הראשוני ש-"`RTG/` הוא בעיקר בינאריים סגורים" אינה מדויקת לחלוטין. רוב שירותי ה-RTG **כן** קיימים כקוד מקור (תחת `From TFS/RTG/`). הבינארים ב-`RTG/` הם בעיקר תוצרי build + תלויות צד-שלישי (Microsoft, Azure). Reverse-engineering אמיתי (צעד 8) ידרוש בעיקר אימות שאין פערי גרסה בין מה שתחת `From TFS` לבין מה שמותקן בפועל ב-`RTG/`.

---

## 4. טווח תאריכי שינוי

| תאריך מוקדם ביותר | תאריך מאוחר ביותר |
|---|---|
| **2 ביוני 2016** | **21 באפריל 2026** |

הקבצים המוקדמים ביותר הם ה-WinForms של `RTG1-2/RTG/RTGApp/` (`Form1.Designer.cs`, `ContainerLocation.cs`). הקבצים המאוחרים ביותר הם `Program.cs` ו-`AssemblyInfo.cs` ב-`TOSConsole3` וב-`TOSService` (כולל `Crc32.cs` ב-`TOSService`) — כולם מ-21 באפריל 2026, חמישה ימים לפני מועד הסקירה הזו.

שתי השלכות מיידיות מכך:

- **המערכת רגישה ל-merge.** אם נתחיל לבנות מחדש רכיב כלשהו ובמקביל יבוצע שינוי דחוף בקוד הייצור, נצטרך מנגנון rebase ברור.
- **השינוי האחרון של 2026-04-21 הוא חתימת זמן חשובה.** מה היה השינוי? מי ביצע? למה? — מתועד כשאלה פתוחה (Q-INV-04).

---

## 5. נקודות כניסה (Entry Points)

13 קבצי `Program.cs` נמצאו, מתוכם 12 ייחודיים מבחינת תפקיד:

| בינארי / יישום | מקור | תפקיד מוערך (יאומת בצעד 2) |
|---|---|---|
| `RTGApp.exe` (גרסה גנרית) | `From TFS/RTG/RTGApp/RTGApp/RTGApp/` | UI תא המנוף — נראה כגרסת-מקור היסטורית או טמפלייט |
| `RTGApp1-2.exe` | `From TFS/RTG/RTG1-2/RTGApp/RTGApp/RTGApp1-2.csproj` | UI תא המנוף לקראנים 1+2 |
| `RTGApp3.exe` | `From TFS/RTG/RTG3/RTGApp/RTGApp/RTGApp3.csproj` | UI תא המנוף לקראן 3 — פיצול נפרד |
| (פריסה משוכפלת) | `RTG1-2/RTG/RTGApp/Program.cs` | שכפול-מקור בתוך תיקיית הפריסה |
| `TOSConsole1.exe` | `From TFS/RTG/TOSConsole1/TOSConsole/` | listener TCP לקראן 1 |
| `TOSConsole2.exe` | `From TFS/RTG/TOSConsole2/TOSConsole/` | listener TCP לקראן 2 |
| `TOSConsole3.exe` | `From TFS/RTG/TOSConsole3/TOSConsole/` | listener TCP לקראן 3 |
| `TOSService.exe` | `From TFS/RTG/TOSService/TOSService/` | שירות (Windows Service?) — תפקיד עדיין לא ברור |
| `KillToss1.exe` | `From TFS/RTG/KillToss1/` | utility להריגת `TOSConsole1` (שם מסגיר) |
| `KillToss2.exe` | `From TFS/RTG/KillToss2/` | אותו דבר עבור 2 |
| `KillToss3.exe` | `From TFS/RTG/KillToss3/` | אותו דבר עבור 3 |
| `ConsolesReRun.exe` | `From TFS/RTG/ConsoleReRun/ConsolesReRun/` | Watchdog שמפעיל מחדש console-ים שנפלו (.NET 6 — לפי `deps.json`) |
| `ForkliftApp.exe` | `From TFS/RTG/New Folder/ForkliftApp/ForkliftApp/` | **תוכנת המלגזות הפעילה היום בייצור.** רכיב שני לצד RTGApp שצורך ומעדכן את אותו `TerminalData` (אישור: 2026-04-26 ע"י Yaniv) |

**Solution-ים:** `From TFS/RTG/RTG.sln` הוא ה-solution המרכז. ה-4 solutions של `ForkliftApp/` עומדים בפני עצמם.

**שלוש נקודות שמושכות את העין:**
- שלושה פרויקטים נפרדים ב-`TOSConsole{1,2,3}` — לא configurations של פרויקט אחד.
- שלושה פרויקטים נפרדים ב-`KillToss{1,2,3}`.
- `RTGApp` קיים בארבעה ענפים מובחנים (גנרי, RTG1-2, RTG3, ושכפול ב-`RTG1-2/RTG/`).

---

## 6. תצורות בינאריות פרוסות

| נתיב פריסה | מרכיבים | קיים? | הערות |
|---|---|---|---|
| `RTG/Console1/TOSConsole1.exe` | + `.config` + `.pdb` + `LastWorkingVersion/` | ✓ | listener קראן 1 |
| `RTG/Console2/TOSConsole2.exe` | + `.config` + `.pdb` + `LastWorkingVer/` + `NewVer/` | ✓ | שלושה גיבויים — סימן ל-rollback ידני בעבר |
| `RTG/Console3/TOSConsole3.exe` | + `.config` + `.pdb` | ✓ | listener קראן 3 |
| `RTG/TosService/TOSService.exe` | + `.config` + `.pdb` | ✓ | שירות; תפקיד לא ברור |
| `RTG/ConsoleReRun/ConsolesReRun.exe` | + 6 DLLs | ✓ | watchdog .NET 6 |
| `RTG/ReRun/TosReRun.exe` | + 20+ DLLs כולל `Azure.Identity`, `Microsoft.Data.SqlClient` | ✓ | watchdog שני? **דורש בדיקה.** קיומן של תלויות Azure רומז על חיבור ל-Azure SQL — לא מתועד בשום מקום אחר |
| `RTG1-2/RTGApp.exe` | + `.config` + `.pdb` | ✓ | UI קראנים 1-2 |
| `RTG3/RTGApp.exe` | + `.config` + `.pdb` | ✓ | UI קראן 3 |

**שלוש תצפיות כבדות-משקל:**

- **שני watchdogs נפרדים** (`ConsolesReRun` ו-`TosReRun`). ייתכן שאחד מהם נטוש או שהם רצים במקביל בשגיאה. השאלה "מי באמת מפעיל-מחדש את ה-listeners בייצור" — קריטית לעיצוב רכיב ה-Listener החדש.
- **`TOSService.exe`** קיים — Windows Service שלכאורה מנהל את ה-listeners — אבל גם `KillToss{1,2,3}.exe` קיימים, וגם stored procedures (לפי הבריף) שמפעילים את ה-listeners מצד ה-DB. שלושה מסלולי-בקרה במקביל אינם בריאים.
- **קבצי `.pdb` נשמרים בצד כל בינארי** — דווקא חיובי; יקל על reverse-engineering בצעד 8 אם ייווצר צורך.

---

## 7. קוד-מת או חוץ-להיקף — מועמדים

הרשימה הזו דורשת אישור או הפרכה ממך לפני שנוציא קבצים מההיקף. שום החלטה לא מתקבלת מצדי באופן חד-צדדי.

| נתיב | הערכת מצב | מה צריך לאשר |
|---|---|---|
| `RTG/LastWorkingVer/2022/06/Console{1,2,3}/` | **גיבוי קפוא של גרסת ייצור מיוני 2022.** לא נטמע בו שינוי מאז | להוציא לחלוטין מההיקף, או יש סיבה לשמר (rollback path)? |
| `RTG/Console2/LastWorkingVer/` ו-`RTG/Console2/NewVer/` | **שני גיבויים נוספים לתיקיית פריסה אחת.** סימן ל-rollback ידני בעבר | זהה |
| `RTG/Console1/LastWorkingVersion/` | גיבוי דומה תחת קראן 1 | זהה |
| ~~`From TFS/RTG/New Folder/ForkliftApp/`~~ | **בהיקף — אישור 2026-04-26.** ForkliftApp היא תוכנת המלגזות הפעילה היום, רכיב שני לצד RTGApp באותו `TerminalData`. תזוז ל-Step 2 לסיווג רכיבי מלא | — |
| `From TFS/RTG/RTGApp/` (גרסה "גנרית" של RTGApp) | קיימת כפיצול נפרד מ-`RTG1-2` ו-`RTG3`. ייתכן שזו גרסת-בסיס מוקפאת או טמפלייט | האם הגרסה הזאת **מורצת** איפשהו, או שהיא רק היסטוריה? |
| `RTG1-2/RTG/RTGApp/` | **שכפול-מקור** של ה-source מ-`From TFS`. תאריכי השינוי שלו ישנים יחסית (2022) | האם זה מקור משני שצריך להישמר, או שאריות ישנות? |

---

## 8. נקודות שמושכות תשומת לב לצעדים הבאים

מהאינוונטר הראשוני עולות תהיות שיש לתעדף לצעדים 2-9:

1. **למה שלוש solutions של `TOSConsole`?** אם הקוד 95% זהה, זה פיצול שעלול להיתפס היום כקבוע ובעצם הוא חוב טכני קשה.
2. **למה `RTGApp` מופיע בארבעה ענפים?** צעד 2 חייב להראות diff ברור.
3. **שני watchdogs** — `ConsolesReRun` ו-`TosReRun`. מי מהם פעיל?
4. **לפחות 33 קבצי DataSet typed schemas** (`xss`/`xsd`/`xsc`). ה-UI עובד מול ה-DB דרך typed DataSets ישנים — דבר שמשפיע מאוד על שכבת data access במעבר ל-Flutter + API.
5. **שלושה קבצי XAML** במערכת WinForms — אנומליה. צריך להבין למי הם שייכים.
6. **`Crc32.cs`** ב-`TOSService/` (לעומת אלגוריתם 16-bit שאוזכר בבריף עבור ה-Console-ים). ייתכן שיש כאן שני אלגוריתמי checksum שונים בו-זמנית. נושא לצעד 5.
7. **`Class1.cs`** ו-`CodeFile1.cs`** קיימים ב-`From TFS/RTG/RTG3/RTGApp/RTGApp/` (שמות-ברירת-מחדל של Visual Studio). סימן לקוד שנכתב בחיפזון או נשכח.

---

## 9. שאלות פתוחות שנפתחו בצעד הזה

יועברו ל-`09_open_questions.md` בסוף השלב. מובאות כאן לתיעוד שוטף בלבד:

| # | שאלה | למה זה חשוב |
|---:|---|---|
| ~~Q-INV-01~~ | ~~האם ForkliftApp בתוך ההיקף או מחוץ לו?~~ **נסגר 2026-04-26.** ForkliftApp בתוך ההיקף כתוכנת מלגזות פעילה. נשאלה שאלה חדשה: מה ממשק האינטראקציה בין ForkliftApp ל-RTGApp ברמת ה-DB? (מועברת ל-Step 2/4) | — |
| Q-INV-02 | מה תפקיד `TOSService.exe` ביחס ל-`TOSConsole{1,2,3}.exe`? | מבנה השכבות לא ברור; שלושה מסלולי-בקרה במקביל |
| Q-INV-03 | מה ההבדל בין `ConsolesReRun.exe` ל-`TosReRun.exe`, ומי מהם פעיל בייצור? | אם שניהם רצים — race condition פוטנציאלי |
| Q-INV-04 | מה היה השינוי האחרון מ-2026-04-21 ב-`TOSConsole3` ו-`TOSService`? מי ביצע ולמה? | עדכניות הקוד; מציין שמישהו עדיין נוגע בקוד |
| Q-INV-05 | האם הגרסה ה"גנרית" של `RTGApp` (תחת `From TFS/RTG/RTGApp/`) רצה בייצור או היא טמפלייט בלבד? | משפיע על מספר ה-UI builds בפועל |
| Q-INV-06 | האם ל-`TosReRun` יש חיבור ל-Azure SQL (לפי תלויותיו `Azure.Identity` + `Microsoft.Data.SqlClient`)? | אם כן — יש כאן עוד destination שלא תועד |
| Q-INV-07 | מה הסטטוס של `LastWorkingVer/2022/06/` ושאר ה-`NewVer`/`LastWorkingVersion` subfolders? | קוד-מת או rollback paths חיוניים? |
| Q-INV-08 | ב-`CHEName.txt` היחיד הנמצא תחת `RTG1-2/RTG/RTGApp/`, מה תוכנו ב-deployment של קראן 1 לעומת קראן 2 בייצור? | ההפרדה בין הקראנים מבוססת על קובץ זה |
| Q-INV-09 | האם יש מקום שבו רשום במפורש מי המפתחים שעבדו על המערכת ומי בעל ה-PDB? | ייעוץ קוד; הבנת היסטוריה |

---

## 10. צעדים הבאים

1. **המתנה לאישורך** על:
   - שיטת התיעוד (עברית בנרטיב, אנגלית בנתיבי-קבצים, סגנון הטבלאות, רמת העומק).
   - תשובה לשאלת ForkliftApp (Q-INV-01) — האם להמשיך לצרף אותו לאינוונטר או להסיר?
2. בכפוף לאישור — מעבר ל-**צעד 2 (מיפוי רכיבים)**, שבו כל קובץ יסווג ל-{Listener, Persistence, Operator UI, Shared, Unknown} ויוצג diff בין `RTG1-2` ל-`RTG3` ובין שלוש גרסאות `TOSConsole`.

---

> **סיום צעד 1.** המשך מותנה באישור.
