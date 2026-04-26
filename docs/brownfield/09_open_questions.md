# 09 — מרשם שאלות פתוחות (Open Questions Register)

> **פרויקט:** Goldbond · מודרניזציית מערכת איתור מנופי RTG
> **מחבר:** Mary, האנליסטית העסקית (BMad)
> **תאריך:** 26 באפריל 2026 · **עודכן:** 2026-04-26 (סבב שני, אחר תשובות Yaniv)
> **שלב:** שלב 1 — גילוי, צעד 9 מתוך 9
> **קלטים:** סינתזה של כל השאלות שנפתחו לאורך צעדים 1-8.
> **מטרת המסמך:** רשימה מאוחדת של כל השאלות שלא הצלחנו לענות עליהן מהקוד בלבד — מסווגות לפי קטגוריה, עדיפות, וצעד-מקור — כדי שתוכל לטפל בהן (לבד או מול תורמים אחרים בארגון) לפני שה-Architect agent מתחיל ב-Phase 2.
> **מצב:** טיוטה לבדיקה ואישור.

---

## 1. תקציר מנהלים

נצברו **52 שאלות פתוחות** במהלך 8 צעדי הגילוי. הן מסווגות לשלוש דרגות:

- **🔴 P0 — Hard blocker:** ✅ **0 פתוחים.** כל 6 ה-P0 ירדו / נסגרו ב-2026-04-26 — ה-GATE הוסר.
- **🟡 P1 — Soft blocker:** **19 שאלות** (Q-PROTO-04 + Q-DB-02 ירדו מ-P0 ל-P1).
- **🟢 P2 — אינפורמטיבי:** **29 שאלות.**

**12 שאלות נסגרו** עד כה (אישורי Yaniv): ForkliftApp בהיקף, TOSService כליסטנר פעיל, באג TOSConsole3 לא חוסם, קיום DB-מראה, **נתיב פריסת ה-listeners (Q-DB-01)**, **חוסר רלוונטיות של Haifa (Q-UI-07)**, **תפקיד TosReRun (Q-RE-02 — watchdog)**, **מקור TOSService זמין ב-TFS (Q-RE-01)**, **`sa` password מת — פאסיבי בייצור**. הן מסומנות ב-✅.

---

## 2. ה-P0 — אין יותר חוסמים (4 ירדו ב-2026-04-26)

✅ **כל ה-P0 ירדו / נסגרו ב-2026-04-26.** ה-GATE לפני Phase 2 הוסר. השאלות הפתוחות לא חוסמות עיצוב — חלקן יוצמדו ל-cutover plan וחלקן יתבהרו תוך כדי הארכיטקטורה.

### ירדו מ-P0 ב-2026-04-26

| # | שאלה | סגירה / שינוי |
|---:|---|---|
| ~~Q-DB-01~~ ✅ | היכן באמת מותקנים ה-listeners על שרת ה-DB? | **כל ה-listeners יושבים על `192.6.8.52` — אותו שרת בו רץ MSSQL בייצור.** ה-SPs `RunEnconsoleRTG{n}` ו-`KillToss{n}` משתמשים ב-`xp_cmdshell` לפתיחת בינארים מקומיים (`c:\RTG\RTG{n}\`); אין hop רשת. **השלכה אדריכלית ל-Phase 2:** אם ה-listener החדש יושב על שרת אחר ויעבד מ-PostgreSQL, יהיה צורך לעבור מ-loopback ל-network hop |
| ~~Q-UI-07~~ ✅ | האם ForkliftApp רץ גם ב-Goldbond Haifa (`ILGBH`)? | **בחיפה אין מנופים** — RTG רלוונטי לאשדוד בלבד. בעיצוב Phase 2 — ניתן להתעלם מ-multi-terminal complexity |
| ~~Q-RE-02~~ ✅ | מה הקוד של `TosReRun.exe`? watchdog או Azure-related? | **`TosReRun` הוא watchdog ל-`TOSService`** — מפעיל אותו מחדש כש-/אם קורס (Yaniv 2026-04-26). AAD/MSAL = רעש. **Phase 2:** השרת החדש יחליף גם את TOSService וגם את ה-watchdog; supervision תיבנה בפלטפורמה |
| ~~Q-PROTO-04~~ ⬇ P1 | האם ה-PLC משדר checksum נכנס? | **הורדה ל-P1.** בייצור הקראנים הם KoneCranes; ב-test ה-PLC משוחק ע"י **chesimu**. הספק (KoneCranes V40 PDF) קיים, ולידציה ע"י chesimu + packet-capture חי בשבת. **לא חוסם עיצוב** — הליסטנר החדש ירשום unknown frames ל-DLQ |
| ~~Q-RE-01~~ ✅ | מה הקוד הקאנוני של `TOSService.exe` 2022? | **המקור זמין ב-TFS:** `CurrentSystem/Current/From TFS/RTG/TOSService/TOSService/Program.cs` (Yaniv 2026-04-26). ה-`Main` לא `static` (drift קל מהבינארי) אבל ה-behavior — listener loop, `TOS()` per-crane, DB writes — קריא ומספיק לעיצוב. **Phase 2:** התנהגות הליסטנר ידועה; drift בין source ל-binary יאומת ב-ILSpy לפני cutover (לא חוסם עיצוב) |
| ~~Q-DB-02~~ ⬇ P1 | מי ה-DB principals שיכולים `EXEC` על `RunEnconsoleRTG{n}` / `KillToss{n}`? `xp_cmdshell` מופעל? | **הורדה ל-P1** (Yaniv אישר 2026-04-26). Phase 2 = security-by-design — לא נשתמש ב-`xp_cmdshell` כלל ואין צורך לשחזר את מודל ההרשאות הישן. ה-audit נדרש ל-cutover plan ול-documentation, לא לעיצוב |

---

## 3. ה-17 ה-P1 — Soft Blockers

| # | שאלה | מקור |
|---:|---|---|
| Q-COMP-02 | מה הסטטוס בייצור של `TOSConsole1.exe`, `TOSConsole2.exe`, `TOSConsole3.exe`? פעילים-במקביל ל-TOSService, פרוסים-לא-פעילים, או מכוונים לקראנים אחרים? | Step 2 |
| Q-COMP-03 | מי מפעיל את `ConsolesReRun` ומי את `TosReRun`? Task Scheduler? שירות? שניהם רצים יחד? | Step 2 |
| Q-COMP-04 | מאיפה `RTGApp.exe` (בייצור) לוקח את ה-connection string, אם ה-`.exe.config` ריק? Settings.settings? embedded? | Step 2 |
| Q-COMP-06 | מה התפקיד של `TosReRun.exe` (בנוסף ל-Q-RE-02)? בכלל אינטגרציה ל-Azure SQL? | Step 2 |
| Q-COMP-08 | האם `ForkLiftNumber.xml` הוא הפרמטר היחיד שמבדיל בין מלגזות בפריסות שונות? | Step 2 |
| Q-COMP-11 | האם זמין TFS history של `TOSService/Program.cs` ב-revision של ה-build מ-2022-06-09? | Step 2 |
| Q-FLOW-01 / Q-DB-05 | מה מקור השדה `RG_B3.PreMessage`? Trigger? Default? Computed? UI INSERT? | Step 3 / Step 4 |
| Q-FLOW-02 / Q-PROTO-05 | האם ה-PLC מבצע deduplication על job-burst אחרי reconnect? | Step 3 / Step 5 |
| Q-FLOW-03 / Q-PROTO-06 | מה ה-PLC עושה כשהוא מקבל NAK `FFFF03B10FEFA`? Retry? Halt? | Step 3 / Step 5 |
| Q-FLOW-04 | האם `xp_cmdshell` מופעל בייצור עם הרשאות SA, או יש sandbox? | Step 3 |
| Q-FLOW-05 | האם נמצאים `RunEnconsoleRTG{n}` ו-`KillToss{n}` SP-ים ב-DB? — ✅ אומת ב-Step 4 (קיימים, בעלי `xp_cmdshell`) | Step 3 ✅ |
| Q-PROTO-01 | מי בעל זכות-גישה ל-port 30701-30703 ב-OT network? יש Firewall/whitelist? | Step 5 |
| Q-PROTO-07 | האם זה פרוטוקול KoneCranes או Goldbond proprietary או vendor אחר? | Step 5 |
| Q-DB-07 | מי INSERT ל-`RG_Container`? הליסטנר רק DELETE, ה-UI רק SELECT count | Step 4 |
| Q-DB-08 / Q-DB-09 | האם session ב-`RG_Log` נסגרת תקין? יש logout? יש auto-close? | Step 4 |
| Q-DB-11 | האם ForkliftApp כותב ל-`TB_Parameters`? | Step 4 |
| Q-RE-05 | האם יש בינארים נוספים פרוסים ב-prod שלא קיימים ב-`CurrentSystem/Current/RTG/`? | Step 8 |

---

## 4. ה-29 ה-P2 — אינפורמטיבי / טוב-לדעת

| # | שאלה | מקור |
|---:|---|---|
| Q-INV-02 | מה תפקיד `TOSService.exe` (פתוחה לפני Step 8 — נסגרה לחלקה ע"י Yaniv) | ✅ — Step 1 |
| Q-INV-03 | מה ההבדל בין `ConsolesReRun.exe` ל-`TosReRun.exe`? — מתועד חלקית | Step 1 |
| Q-INV-04 | מה היה השינוי האחרון מ-2026-04-21 ב-`TOSConsole3` ו-`TOSService`? מי ביצע? | Step 1 |
| Q-INV-05 | האם הגרסה ה"גנרית" של `RTGApp` (תחת `From TFS/RTG/RTGApp/`) רצה בייצור? | Step 1 |
| Q-INV-06 | האם ל-`TosReRun` יש חיבור ל-Azure SQL? | Step 1 |
| Q-INV-07 | מה הסטטוס של `LastWorkingVer/2022/06/`? קוד-מת? | Step 1 |
| Q-INV-08 | מה תוכן `CHEName.txt` ב-cabin 1 לעומת cabin 2? | Step 1 |
| Q-INV-09 | מי המפתחים שעבדו על המערכת? | Step 1 |
| Q-COMP-01 | איזה מבין 3 הענפים של RTGApp באמת מותקן בייצור על כל crane? | Step 2 |
| Q-COMP-05 | באג `if (T)` ב-TOSConsole3 — באג מוכר | ✅ — Step 2 (אישור Yaniv) |
| Q-COMP-07 | מי "Moshe" (שם תחנה ב-app.config של ForkliftApp.Framework)? | Step 2 |
| Q-COMP-09 | האם שלוש app.config של ForkliftApp באמת בשימוש בפריסה? | Step 2 |
| Q-COMP-10 | מה משמעות `RG_ColDG`? | Step 2 |
| Q-FLOW-06 | האם ה-cabin polling-rate של 500ms הוא קונפיגורבילי? | Step 3 |
| Q-FLOW-07 | מהי התנהגות ה-PLC של המנוף ב-startup קר? | Step 3 |
| Q-DB-03 | למה יש 10+ SPs עם סיומת `_App`? | Step 4 |
| Q-DB-04 | כמה שורות ב-`RG_A1_LOG`? מדיניות ניקוי? | Step 4 |
| Q-DB-06 | מי כותב ל-`RG_B3.FinishDate`? | Step 4 |
| Q-DB-10 | מה הסטטוס של `CO_ContainersLocation`? | Step 4 |
| Q-DB-12 | מה עושה הטריגר `KerorMoves` על `CP_Order`? | Step 4 |
| Q-DB-13 | מה עושה הטריגר `create_RSHNLVFile` על `CP_Deal`? | Step 4 |
| Q-DB-14 | מה ה-load של ה-Web triggers? | Step 4 |
| Q-DB-15 | למה Bond1 ו-Bond2 ב-`V_MapRTGBond1/2` מציגים רק חצי מהעמודות? יש Bond3 view דומה? | Step 4 |
| Q-PROTO-02 | מה תוכן הבייטים בין שדות פרוטוקול (offsets 6-11, 26-29, 44-54, 56-74)? Header? Padding? | Step 5 |
| Q-PROTO-03 | מה משמעות `Substring(114, 2)` — `Len`? | Step 5 |
| Q-PROTO-08 | האם ה-PLC מבצע connection-keep-alive (keepalive packets)? | Step 5 |
| Q-UI-01 / Q-UI-02 | מה תפקיד `Works.cs` ו-`Form2.cs` ב-RTGApp? | Step 6 |
| Q-UI-03 | למה `if (CHE.Text == "GOLD1")` ו-`if (CHE.Text == "GOLD2")` ב-FrmMap01 — ולא parametric? | Step 6 |
| Q-UI-04 / 05 / 06 / 08 / 09 / 10 / 11 / 12 | פרטי תפעול והגדרה של מסכי-משנה ב-ForkliftApp | Step 6 |

---

## 5. שאלות שנסגרו (✅) ב-2026-04-26

| # | שאלה | סגירה |
|---:|---|---|
| Q-INV-01 | האם ForkliftApp בהיקף או מחוץ לו? | **בהיקף** — Yaniv אישר שזה תוכנת המלגזות הפעילה היום |
| Q-INV-02 | האם TOSService רץ בייצור? | **כן** — Yaniv אישר שזה הליסטנר הפעיל |
| Q-COMP-05 | באג `if (T)` ב-TOSConsole3 — באג או התנהגות מכוונת? | **באג מוכר**, לא חוסם בייצור, חייב להיתקן ב-Phase 2 |
| Q-DB-01 | היכן בייצור מותקנים ה-listeners? | **על `192.6.8.52` — אותו שרת כמו MSSQL פרודקשן.** ה-`xp_cmdshell` מצביע ל-paths מקומיים (`c:\RTG\RTG{n}\`) |
| Q-UI-07 | ForkliftApp רץ גם ב-Goldbond Haifa? | **לא — בחיפה אין מנופים.** הקוד multi-terminal לא חל על RTG modernization |
| Q-RE-02 | תפקיד `TosReRun.exe`? | **watchdog ל-TOSService** — מפעיל אותו מחדש כש-/אם קורס. תלויות AAD/MSAL הן רעש |
| (sa password) | האם הסיסמה `z3334606*` ב-ForkliftApp source עדיין פעילה? | **לא — היא מתה** (Yaniv 2026-04-26). פאסיבית בייצור. **עדיין** לרוטט/למחוק את הופעות הסיסמה מ-source/binaries אם נשארות. **עיקר ה-takeaway:** Phase 2 = אין `sa`, אין credentials מובנים, security-by-design |
| (חדש) | DB מראה לבדיקות זמין? | **כן** — `10.10.200.51:49993` / `TerminalData_AI` / `GBDEV` |

---

## 6. תוכנית-פעולה מומלצת לסגירת השאלות

### 6.1 שיחה ישירה עם Yaniv (כ-30 דקות)

יוכל לסגור: Q-COMP-01, Q-COMP-02, Q-COMP-03, Q-COMP-04, Q-COMP-08, Q-COMP-11, Q-INV-04, Q-INV-05, Q-INV-07, Q-INV-08, Q-FLOW-04, Q-UI-07, Q-UI-12.

### 6.2 חיפוש ב-TFS / Git history

יסגור: Q-RE-01 (אם הקוד הקאנוני של 2022 שמור), Q-COMP-11.

### 6.3 RE עם ILSpy / dnSpy

יסגור: Q-RE-01, Q-RE-02, Q-RE-03, Q-RE-04, Q-RE-05.

### 6.4 שיחה עם vendor של PLC (KoneCranes או vendor אחר)

יסגור: Q-PROTO-02, Q-PROTO-03, Q-PROTO-04, Q-PROTO-05, Q-PROTO-06, Q-PROTO-07, Q-PROTO-08, Q-FLOW-02, Q-FLOW-03, Q-FLOW-07.

### 6.5 בדיקות ב-DB מראה (`10.10.200.51:49993`)

יסגור: Q-DB-04 (`SELECT COUNT(*) FROM RG_A1_LOG`), Q-DB-10 (`SELECT COUNT(*) FROM CO_ContainersLocation`), Q-DB-14 (load של AuditWeb), Q-DB-15 (קיום של `V_MapRTGBond3`), Q-DB-07 (חיפוש INSERT ל-RG_Container ב-SPs ובקוד-Web), חלק מ-Q-DB-12, Q-DB-13.

### 6.6 שיחה עם DBA או IT Security

יסגור: Q-DB-02 (xp_cmdshell config), Q-DB-04, Q-DB-14, Q-PROTO-01 (Firewall).

---

## 7. מסקנות לעיצוב Phase 2

לפני התחלת Phase 2, ה-P0 ירדו מ-4 ל-2:
- **Q-RE-01** — קוד מקור של TOSService מ-TFS (אם נמצא — סוגר; אם לא — RE).
- **Q-DB-02** — ניתן להוריד ל-P1 לאור עמדת Yaniv שמיקוד Phase 2 הוא מערכת חדשה (security-by-design); עדיין נדרש כ-cutover prerequisite ול-audit של הקיים.

**עדכון 2026-04-26:** Q-RE-02 (`TosReRun` = watchdog) נסגר; Q-PROTO-04 הורד ל-P1 (KoneCranes spec + chesimu מספיקים לעיצוב). RE הוא רק fallback אם מקור TFS לא זמין — לא חובה לפני Architect.

---

## 8. צעדים הבאים

1. **המתנה לאישורך** על הסיווג והעדיפויות.
2. בכפוף לאישור — מעבר ל-**צעד 10 (Executive SUMMARY)** — `00_SUMMARY.md`. לאחר מכן הגעה ל-🛑 GATE: בקשה לאישורך לפני שה-Architect agent מתחיל ב-Phase 2.

---

> **סיום צעד 9.** המשך מותנה באישור.
