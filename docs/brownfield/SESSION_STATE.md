# Session State — Resume from here in the morning

> **תאריך עצירה:** 26 באפריל 2026 (בלילה) · **עודכן:** 2026-04-26 (סבב שני, אחר תשובות Yaniv)
> **המשך מתוכנן:** בוקר למחרת
> **אגנט פעיל:** Mary (BMad Business Analyst)
> **מטרת המסמך:** קובץ-המשכיות. בקריאה אחת — אדע (או כל אגנט אחר) איפה עצרנו ומה הצעד הבא.

---

## איפה עצרנו

✅ **שלב 1 (Discovery) הושלם.** ✅ **GATE הוסר 2026-04-26 — Phase 2 (Architect) יכול להתחיל.**

10 מסמכים ב-[docs/brownfield/](.):
- `00_SUMMARY.md` — תקציר מנהלים
- `01_inventory.md` עד `09_open_questions.md` — תוצרי-עומק

---

## מה נסגר במהלך הסשן (Yaniv אישר 2026-04-26)

| # | סגירה |
|---|---|
| Q-INV-01 | ForkliftApp בהיקף — תוכנת מלגזות פעילה |
| Q-INV-02 | TOSService = הליסטנר הפעיל בייצור |
| Q-COMP-05 | באג `if (T)` ב-TOSConsole3 — ידוע, לא חוסם, חייב להיתקן ב-Phase 2 |
| Q-DB-01 | כל ה-listeners (`TOSService`, `TOSConsole{1,2,3}`, `KillToss{n}`) על **`192.6.8.52`** = אותו שרת כמו MSSQL |
| Q-UI-07 | בחיפה אין מנופים — multi-terminal לא חל על RTG |
| (חדש) | DB מראה לבדיקה: `10.10.200.51:49993` / `TerminalData_AI` / `GBDEV` |

---

## 🔴 P0 — סטטוס 2026-04-26 (סבב שני)

| # | שאלה | סטטוס / צעד |
|---:|---|---|
| **Q-RE-01** | קוד קאנוני של `TOSService.exe` 2022 | **פתוח.** Yaniv: השרת לא נגיש מהרשת, אבל המקור אמור להיות ב-TFS. **צעד:** לאתר את `RTG/TOSService/` ב-TFS, לוודא שאין drift מהבינארי המסופק. fallback: RE עם ILSpy על עותק נשלף בנפרד |
| ~~Q-RE-02~~ | ~~מה `TosReRun.exe` עושה?~~ | ✅ **נסגר.** Yaniv 2026-04-26: watchdog ל-TOSService — מפעיל אותו מחדש בקריסה. תלויות AAD/MSAL = רעש |
| ~~Q-PROTO-04~~ | ~~האם PLC משדר checksum?~~ | ⬇ **הורד ל-P1.** Yaniv 2026-04-26: KoneCranes בייצור, chesimu בפיתוח. ספק V40 + capture חי בשבת = מספיק |
| **Q-DB-02** | xp_cmdshell config + DB principals שיכולים `EXEC` על SP-ים | **פתוח, אך מועמד להורדה ל-P1.** Yaniv: Phase 2 = security-by-design (אין `sa`, אין credentials מובנים) — ה-audit של ההרשאות הישנות רלוונטי ל-cutover ולתיעוד, לא לעיצוב |

---

## 🟡 P1 שכדאי גם לסגור (אופציונלי לפני Phase 2)

מובאות מ-[09_open_questions.md §3](09_open_questions.md#3-ה-17-ה-p1--soft-blockers):

| # | שאלה |
|---:|---|
| Q-COMP-01 | איזה מ-4 ענפי RTGApp מותקן בייצור על כל crane |
| Q-COMP-02 | סטטוס בייצור של TOSConsole1/2/3 (פעילים מקביל ל-TOSService?) |
| Q-COMP-03 + Q-COMP-06 | מי מפעיל ConsolesReRun? מי TosReRun? |
| Q-COMP-04 | מאיפה RTGApp.exe לוקח connection string (config ריק) |
| Q-COMP-11 | האם זמין TFS history של TOSService 2022? יציל מ-RE |
| Q-INV-04 | מה השינוי האחרון מ-2026-04-21 ב-TOSConsole3+TOSService? |

---

## 💡 תובנה אדריכלית שעלתה בסוף הסשן

**Co-location מלא:** MSSQL + כל ה-listeners + כל ה-KillToss + שני watchdogs כולם רצים על `192.6.8.52`. השלכות ל-Phase 2:

1. **Single point of failure** — שרת זה נופל = הכל נופל.
2. **Localhost loopback** של חיבור ה-DB (אין network hop היום).
3. **בעיצוב החדש:** האם PG יישב על אותו host? אם לא — חייבים לתכנן network עם latency.
4. **שני drive letters** (`C:\RTG\` per SP, `E:\RTG\` per ConsolesReRun source) — אולי שני volumes פיזיים? שווה לבדוק עם IT.

---

## 🚨 פעולות-אבטחה — סטטוס מעודכן (2026-04-26)

ראה [00_SUMMARY.md §4](00_SUMMARY.md):

1. ~~**רוטציית `sa` password**~~ ✅ — Yaniv: הסיסמה מתה בייצור, לא רלוונטית.
2. **הסרת credentials מ-source** של ForkliftApp — עדיין רצוי ל-hygiene גם אם הסיסמה מתה.
3. **Firewall whitelist** על port 30701-30703 (כיום אין authentication; spoofed PLC אפשרי).
4. **בדיקת xp_cmdshell** — ל-audit ולתיעוד; הפלטפורמה החדשה לא תשתמש בה ממילא.

**עיקרון מרכזי ל-Phase 2 (Yaniv 2026-04-26):** המערכת החדשה תיבנה עם security-by-design — אין `sa`, אין credentials מובנים בקוד או ב-config, אין הצגת סיסמאות.

---

## איך להמשיך מחר

### תרחיש A — Yaniv מספק תשובות ל-P0
Mary תעדכן את `09_open_questions.md`, תכין תקציר עדכני ב-`00_SUMMARY.md`, ותסיר את ה-GATE. נעבור ל-`bmad-agent-architect` להתחלת Phase 2.

### תרחיש B — Yaniv זקוק לעזרה ב-RE
Mary תכוון אותו על שימוש ב-ILSpy (link: https://github.com/icsharpcode/ILSpy/releases) ותעזור לפענח את הקוד שיועלה.

### תרחיש C — תוך כדי המתנה לתשובות
Mary יכולה להתחבר ל-DB מראה (`10.10.200.51:49993`) ולסגור 6-8 שאלות P2 (Q-DB-04, Q-DB-10, Q-DB-12, Q-DB-13, Q-DB-14, Q-DB-15) — אם Yaniv ירצה.

---

## הוראת-המשך לאגנט

בהפעלת bmad-agent-analyst (Mary) מחר בבוקר:

1. קרא את הקובץ הזה ראשון.
2. שאל את Yaniv איזה תרחיש (A/B/C).
3. אל **תקרא** מחדש את `docs/discovery/`, `docs/design/`, `docs/build/`, `docs/fixes/` — אלה תוצרי ניסיון קודם, לא רלוונטיים.
4. כל המסמכים ה-fresh ב-[docs/brownfield/](.).

---

> **לילה טוב, Yaniv. נתראה בבוקר.** 📊
