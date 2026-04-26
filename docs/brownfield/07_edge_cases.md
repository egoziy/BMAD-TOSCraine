# 07 — מרשם מקרי-קצה (Edge Case Register)

> **פרויקט:** Goldbond · מודרניזציית מערכת איתור מנופי RTG
> **מחבר:** Mary, האנליסטית העסקית (BMad)
> **תאריך:** 26 באפריל 2026
> **שלב:** שלב 1 — גילוי, צעד 7 מתוך 9
> **קלטים עיקריים:** מסקנות מצעדים 2-6.
> **מטרת המסמך:** לרכז ≥25 תרחישי-קצה (חריגות, כשלים, race conditions, גיבוי-נתונים, התקפות, ושיבוש שקט) שעלו ב-discovery — לכל אחד: תרחיש, איך מערכת ה-current מטפלת, מקור (file:line), והסיכון אם לא נטופל בעיצוב החדש.
> **מצב:** טיוטה לבדיקה ואישור.

---

## 1. תקציר

נוטרל 31 מקרי-קצה בשלוש קטגוריות חומרה: **חמורה (P1)** = מסכן integrity של נתונים או security; **בינוני (P2)** = יכול לגרום outage או החלטות שגויות; **נמוך (P3)** = UX או performance.

**חלוקה:** 14 P1, 11 P2, 6 P3.

הקטגוריות הבעייתיות הגדולות:
- **5 הזמנות-checksum/integrity של פרוטוקול** — אין validation על inbound, framing שבר, drift בין forks.
- **6 מקרי security/auth** — sa hardcoded, PIN לא מאומת מול user, no lockout, SQL injection.
- **5 מקרי DB consistency** — recursive trigger, no transactions, double-write על CO_Containers, dead session.
- **4 מקרי infrastructure** — שני watchdogs, נתיבי SP לא תואמים, no graceful shutdown.

---

## 2. מרשם המקרים

טבלה: `#`, `קטגוריה`, `חומרה`, `תרחיש`, `איך הקיים מטפל`, `מקור (file:line)`, `סיכון אם לא נטופל`, `הוקיב Q?`

| # | קטגוריה | חומרה | תרחיש | איך הקיים מטפל | מקור | סיכון אם לא נטופל | קישור Q |
|---:|---|:---:|---|---|---|---|---|
| 1 | Protocol | **P1** | PLC משדר A1 עם payload corrupted (offsets תקפים, תוכן שגוי) | אין inbound checksum validation; UPDATE ל-`RG_A1` עם נתון שגוי בלי אזהרה | `Crc32.cs` (אין בדיקה); `TOSConsole1/Program.cs:225` | **שיבוש שקט** של מצב המנוף ב-DB. UI יציג מיקום שגוי | Q-PROTO-04 |
| 2 | Protocol | **P1** | TCP מפצל הודעת A1 ל-2 segments | `Substring(12, 6)` יזרוק `ArgumentOutOfRangeException` | `TOSConsole1/Program.cs:225` | A1 מאבד ב-silent. ניתוח post-mortem בלתי-אפשרי | Q-PROTO-X (חדש) |
| 3 | Protocol | **P1** | TCP coalesce-ת שתי הודעות ל-1 packet | קוד מעבד רק את הראשונה; השנייה אובדת | `TOSConsole1/Program.cs:206` | אובדן הוראות, פוטנציאל crisis | Q-PROTO-X |
| 4 | Protocol | **P1** | Spoofed PLC על אותו OT network שולח `??...A2 04 ...` | אין authentication על לקוחות-TCP. המסר מעובד כאילו הוא מהקראן | `TOSConsole1/Program.cs:155-219` | PLACE זויף ב-DB. הקראן מקבל פעולה שלא ביצע | Q-PROTO-01 |
| 5 | Protocol | **P2** | Reconnect של PLC אחרי disconnect ארוך — burst של jobs נצברים | הליסטנר שולח את כולם ב-loop בלי delay; PLC עלול לקבל 10+ jobs באלפיות שניה | `TOSConsole1/Program.cs:181-203` | Double-execute אם PLC לא עושה dedup | Q-PROTO-05, Q-FLOW-02 |
| 6 | Auth/Security | **P1** | משתמש מקליד PIN של מפעיל אחר (גם אם ה-LoginName שונה) | `SELECT COUNT(*) WHERE UserPinCode = <PIN_TEXT>` — לא בודק LoginName | `FrmLogin.cs:69-74` | Audit trail מעוות; פעולות נרשמות לאדם הלא-נכון | Q-FLOW-(login) |
| 7 | Auth/Security | **P1** | PIN מוקלד עם SQL injection: `1 OR 1=1 --` | מתבצע ישירות ב-SQL בקונקטנציה | `FrmLogin.cs:74` | זיוף הזדהות לחלוטין | Q-FLOW-(login) |
| 8 | Auth/Security | **P1** | ניחוש PIN ב-brute-force | אין lockout, אין retry counter | `FrmLogin.cs:74-95` | פריצה תוך דקות | Q-FLOW-(login) |
| 9 | Auth/Security | **P1** | קוד ForkliftApp מכיל `User ID=sa;Password=z3334606*` בקוד-מקור (`frmLogIn.cs:194`) | `SqlConnection` נפתח ישירות עם credentials embedded | `frmLogIn.cs:194` | פריצת sa → root על MSSQL | Q-COMP-(forklift), §02_components.md §7 |
| 10 | Auth/Security | **P1** | משתמש מקליד `txtUserName.Text == "sa"` ב-ForkliftApp | מטופל מיוחד: `flnu = 0` ללא בדיקת ForkliftNumber | `frmLogIn.cs:100-107` | Built-in backdoor שכל מי שיודע יכול לנצל | Q-UI-(login) |
| 11 | Auth/Security | **P2** | RG_Log session "פתוחה" כשהמפעיל יצא בלי לעשות logout (אין logout מובהק) | אין UPDATE / INSERT ב-logout | `FrmLogin.cs:90` | פעולות חדשות מקבלות OperatorID של המפעיל הקודם | Q-DB-08, Q-DB-09 |
| 12 | DB Consistency | **P1** | PLACE flow נכשל באמצע (אחרי INSERT RG_Shifting, לפני UPDATE TB_Location) | אין transaction. כל UPDATE/INSERT הוא בנפרד | `TOSConsole1/Program.cs:283-329` | DB ב-מצב inconsistent: container מצוין ב-shifting אבל TB_Location ריק | Q-FLOW-(place) |
| 13 | DB Consistency | **P1** | DB connection נופל באמצע flow | `Crc16Ccitt.ReturnDT` מחזיר `null`; הקורא מנסה `dt.Rows[0][0]` → NullReferenceException → catch → goto Outer | `Crc32.cs:35-78`; `TOSConsole1/Program.cs:259-330` | Partial writes; אין retry; אין compensating action | Q-FLOW-(db_fail) |
| 14 | DB Consistency | **P2** | recursive trigger `rg_a1_updatetime` על RG_A1 update | UPDATE → trigger → UPDATE → אם `RECURSIVE_TRIGGERS=ON` → infinite | `06_triggers.sql:493-502` | אם config שונה — overload of CTime updates | Q-DB-(triggers) |
| 15 | DB Consistency | **P2** | `update_location_in_container` trigger + UPDATE-של-הליסטנר → CO_Containers נכתב פעמיים | הקוד והטריגר רצים ברצף; ה-UPDATE השני מבטל / מחליף את הראשון | `06_triggers.sql:517-530`; `TOSConsole1/Program.cs:316-323` | אם trigger נפל — הליסטנר עדיין מצליח (אחר); אבל לכלות שני INSERT-ים ל-AuditWeb | Q-DB-(triggers) |
| 16 | DB Consistency | **P2** | `RG_B3.MAX(CounterID)` כ-correlation key ב-PICK ACK + race condition | אם UI מבצע 2 INSERT-ים תוך מילישנייה | `TOSConsole1/Program.cs:267` | ACK מתויג לעבודה הלא-נכונה | Q-FLOW-(pick) |
| 17 | DB Consistency | **P2** | Operator A פותח session לקראן GOLD1 ולא יוצא; Operator B מתחיל session ב-GOLD2; מערכת מאמתת אותו אבל לא בודקת שלא קיים session פתוח של B במקום אחר | אין בדיקה | `FrmLogin.cs:90` | פעילויות נרשמות לכאורה משני קראנים בו-זמנית עבור אותו אדם | Q-DB-08 |
| 18 | TOSConsole3 BUG | **P2** | קראן 3 מבצע PLACE על קרקע (`G`) | Branch checks `if (T)` בלבד; ה-handoff-flow למלגזה (`UPDATE CO_Containers SET ReleaseForkliftDate = ...`) **לא רץ** | `TOSConsole3/Program.cs:346` | מכולה שמונחת ב-G לא מתעדכנת ב-`ReleaseForkliftDate` — דוחות-מלגזה כושלים. מאומת ע"י Yaniv: באג מוכר, לא חוסם | Q-COMP-05 |
| 19 | TOSService BUG | **P2** | `if (sub != "A1" \|\| sub != "A2" \|\| sub != "A3")` — תמיד true | NAK נשלח **תמיד** | `TOSService/Program.cs:230` | אם הבינארי בייצור הוא קוד-מקור הזה — PLC מקבל NAK על כל הודעה. שאלה: האם הבינארי 2022 בנוי מקוד שונה? | Q-COMP-(service), Step 8 |
| 20 | Infrastructure | **P2** | שני watchdogs במקביל — `ConsolesReRun.exe` ו-`TosReRun.exe` | אם שניהם רצים: שניהם הורגים `TOSCONSOLE*` → race condition. אין delay בין Kill ל-Start | `RTG/ConsoleReRun/`, `RTG/ReRun/` (שני binaries); `TOSReRun source לא נמצא` | TCP socket עדיין ב-`TIME_WAIT` → bind ייכשל; thrashing | Q-COMP-03 |
| 21 | Infrastructure | **P2** | הזרקת SP-ים `RunEnconsoleRTG{n}`, `KillToss{n}` מצביעים ל-`c:\RTG\RTG{n}\`, אבל הבינארים פרוסים ב-`RTG/Console{n}/` | `xp_cmdshell` ייכשל אם הנתיב לא נכון | `05_module_definitions.sql:387, 412`; `02_components.md §6` | SP-ים אינם פועלים בפועל | Q-DB-01 |
| 22 | Infrastructure | **P3** | `Process.Kill` לא graceful — `KillToss{n}.exe` עושה הריגה מיידית | אין shutdown handler ב-listener; SQL connection פעיל, TCP פעיל, log file פתוח — leaked | `KillToss1/Program.cs:14-19` | Resource leaks; corrupt logs לעיתים | Q-FLOW-(start_stop) |
| 23 | Infrastructure | **P2** | המנוף שולח A2 03 (PICK) ולפני שה-listener הסיק לאות handshake → הקראן עושה reset | `RG_B3.PickDate` כבר נכתב ב-`UPDATE`; אבל `Container` לא הועבר ל-`RG_Container` | `TOSConsole1/Program.cs:266-279` | מצב חוסר-צפי — מכולה "באוויר" אבל ב-DB אין רשומה ב-RG_Container | Q-DB-07 |
| 24 | Infrastructure | **P3** | log path UNC לא זמין (`\\Broadcast\Logs\RTG\...`) | קוד `WriteLog` עטוף ב-`try/catch` ריק | `TOSConsole1/Program.cs:391-394` | אין logging — debug בלתי-אפשרי | Q-COMP-(logs) |
| 25 | Listener startup | **P2** | שני אינסטנסים של אותו ליסטנר רצים בו-זמנית (manual + watchdog) | בדיקה ראשונה `Process.GetProcesses().Count == 1`; השני נסגר מיד | `TOSConsole1/Program.cs:107-109` | אם ה-startup-check מאחר ב-millisecond — שני אינסטנסים מקבלים `port already in use` | Q-FLOW-(startup) |
| 26 | UX | **P3** | המפעיל מקליד PIN ולוחץ OK; ה-PIN שגוי; ה-MessageBox נסגר; ה-PIN עדיין ב-textbox | קוד `// Password.Text = string.Empty` מוערה | `FrmLogin.cs:81` | המפעיל יכול בטעות ללחוץ OK שוב עם PIN שגוי | UX bug |
| 27 | UX | **P3** | UI Timer פועל כל ~500ms גם כש-app focus לא ב-window | אין pause | (משוער לפי דפוס WinForms) | עומס DB מיותר במצב background | Q-UI-09 |
| 28 | UX | **P3** | `MessageBox.Show` חוסם UI thread | סטנדרטי WinForms | כל מקום ש-MessageBox נצפה | UI lockup לתקופות אם DB איטי | UX |
| 29 | Data integrity | **P2** | `RG_Container` row לא נוצרה (קוד INSERT לא נצפה ב-flow) — הליסטנר ב-PLACE flow מחפש `Container, OperatorID FROM RG_Container WHERE CHE='GOLD1'` ולא מוצא | מבצע ה-handoff-flow רק אם count > 0 — אחרת מדלג | `TOSConsole1/Program.cs:289-300` | אם ה-INSERT מתבצע במקום אחר ולא נצפה — ייתכן שיש flow שלם שלא ראינו | Q-DB-07 |
| 30 | Data integrity | **P2** | מספר מכולה בנקודות שונות בקוד נבדק כ-`ContainerPick.Length == 11` (תקן ISO) | אם המספר חסר/קצר — handoff מדלג | `TOSConsole1/Program.cs:302` | מכולות עם מזהה לא-תקני (legacy?) — לא נכנסות ל-RG_Shifting | Q-DB-(integrity) |
| 31 | Multi-terminal | **P2** | ForkliftApp תומך ב-2 terminals (`ILCXQ` Ashdod / `ILGBH` Haifa); RTGApp לא תומך מובהק. לא ידוע אם RTGApp רץ ב-Haifa | אם רץ ב-Haifa — בלי עדכון ל-CHE / Block — האפליקציה תקרא רק BOND1/2/3 שאולי לא קיימים שם | (RTGApp/FrmLogin.cs:246) קורא `C:\RTG\CHEName.txt` בלי בדיקת terminal | המערכת **לא ערוכה ל-multi-tenant** במצב המוצע | Q-UI-07 |

---

## 3. סיכום לפי קטגוריה

| קטגוריה | P1 | P2 | P3 | סך |
|---|---:|---:|---:|---:|
| Protocol | 4 | 1 | 0 | 5 |
| Auth/Security | 5 | 1 | 0 | 6 |
| DB Consistency | 2 | 4 | 0 | 6 |
| Bug-known | 0 | 2 | 0 | 2 |
| Infrastructure | 0 | 4 | 2 | 6 |
| Data integrity | 0 | 2 | 0 | 2 |
| UX | 0 | 0 | 3 | 3 |
| Multi-terminal | 0 | 1 | 0 | 1 |
| **סך** | **11** | **15** | **5** | **31** |

> 11 מקרי P1 חיים-בייצור היום. הם מהווים את **התעדוף הראשון** של החלפת המערכת.

---

## 4. צעדים הבאים

1. **המתנה לאישורך** על ה-register — בייחוד על שלוש המקרים החמורים: #4 (TCP spoofing על OT), #6-8 (PIN/auth), #9-10 (sa hardcoded + backdoor).
2. בכפוף לאישור — מעבר ל-**צעד 8 (Reverse Engineering של בינארים)**: בעיקר `TOSService.exe` (2022, ה-listener בייצור), `TosReRun.exe` (.NET 6 watchdog בלי source).

---

> **סיום צעד 7.** המשך מותנה באישור.
