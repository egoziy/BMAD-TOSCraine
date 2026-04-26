// Modern · Suggested locations · Expected containers · Empty · No-location · RTG settings

// ────────── SUGGESTED LOCATIONS ──────────
function ModernSuggested() {
  const suggestions = [
    { loc: '170E4', row: 'E', col: 170, stack: 3, reason: 'מומלץ ראשי · קרוב למנוף · 3/6', score: 98, fit: 'מעולה' },
    { loc: '166D4', row: 'D', col: 166, stack: 4, reason: 'קרוב · מטה יציב · 4/6', score: 92, fit: 'טוב מאוד' },
    { loc: '174B4', row: 'B', col: 174, stack: 4, reason: 'אותו לקוח · 4/6', score: 88, fit: 'טוב' },
    { loc: '142D3', row: 'D', col: 142, stack: 3, reason: 'מרחק בינוני · 3/6', score: 75, fit: 'סביר' },
    { loc: '134A4', row: 'A', col: 134, stack: 4, reason: 'רחוק · אך פנוי', score: 62, fit: 'סביר' },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'flex', flexDirection: 'column', gap: 12, minHeight: 0 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
          <h1 style={{ margin: 0, fontSize: 24, fontWeight: 800 }}>⭐ איתורים מומלצים</h1>
          <div style={{ fontSize: 14, color: M_MUTED }}>עבור MSNU8552840 · 40'HC · FR</div>
          <div style={{ flex: 1 }} />
          <ModernChip kind="stack">אלגוריתם: קרוב + נמוך</ModernChip>
        </div>
        <div style={{
          flex: 1, display: 'grid', gridTemplateColumns: '1fr 1fr 1fr', gap: 14,
          gridTemplateRows: 'repeat(2, 1fr)',
        }}>
          {suggestions.map((s, i) => {
            const top = i === 0;
            return (
              <div key={s.loc} style={{
                background: top ? 'linear-gradient(135deg, #ccfbf1 0%, #fff 100%)' : M_CARD,
                border: '1.5px solid ' + (top ? M_TEAL : M_LINE),
                borderRadius: 16, padding: 18,
                display: 'flex', flexDirection: 'column', gap: 10,
                boxShadow: top ? '0 8px 24px ' + M_TEAL + '22' : '0 1px 3px rgba(15,23,42,.04)',
                position: 'relative', overflow: 'hidden',
              }}>
                {top && (
                  <div style={{
                    position: 'absolute', top: 10, left: 10,
                    background: M_TEAL, color: '#fff', fontSize: 11, fontWeight: 800,
                    padding: '3px 10px', borderRadius: 999, letterSpacing: 1,
                  }}>מומלץ</div>
                )}
                <div style={{
                  fontSize: 36, fontWeight: 800, color: M_TEAL_DEEP,
                  fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1,
                }}>{s.loc}</div>
                <div style={{ display: 'flex', gap: 6, flexWrap: 'wrap' }}>
                  <ModernChip>שורה {s.row}</ModernChip>
                  <ModernChip>עמ׳ {s.col}</ModernChip>
                  <ModernChip kind="stack">{s.stack}/6</ModernChip>
                </div>
                <div style={{ fontSize: 13, color: M_MUTED, flex: 1 }}>{s.reason}</div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
                  <div style={{ flex: 1, height: 6, background: '#f1f5f9', borderRadius: 3, overflow: 'hidden' }}>
                    <div style={{
                      width: s.score + '%', height: '100%',
                      background: s.score > 90 ? M_GREEN : s.score > 75 ? M_TEAL : M_AMBER,
                    }} />
                  </div>
                  <div style={{ fontSize: 18, fontWeight: 800, color: M_INK, fontFamily: '"IBM Plex Mono", monospace' }}>
                    {s.score}%
                  </div>
                </div>
                <button style={{
                  height: 52, background: top ? M_TEAL : M_CARD,
                  color: top ? '#fff' : M_TEAL,
                  border: '1.5px solid ' + M_TEAL, borderRadius: 12,
                  fontSize: 15, fontWeight: 700, fontFamily: 'inherit', cursor: 'pointer',
                }}>בחר איתור זה</button>
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}

// ────────── EXPECTED CONTAINERS ──────────
function ModernExpected() {
  const rows = [
    { id: 'MSNU4412890', eta: '18:20', line: 'MSC',  size: 40, type: 'HC', truck: '6B',  client: 'ZIM IL',    status: 'בדרך' },
    { id: 'TCLU7723451', eta: '18:35', line: 'ZIM',  size: 20, type: 'RG', truck: '26',  client: 'יחדיו',     status: 'בדרך' },
    { id: 'FSCU8899010', eta: '18:48', line: 'MAERSK', size: 40, type: 'HC', truck: '12', client: 'תנובה',    status: 'הגיע לשער' },
    { id: 'CAAU7654323', eta: '19:05', line: 'MSC',  size: 40, type: 'HC', truck: '88',  client: 'OSEM',      status: 'בדרך' },
    { id: 'HMMU3421098', eta: '19:22', line: 'ZIM',  size: 40, type: 'RH', truck: '—',   client: 'Israel Elec', status: 'צפוי' },
    { id: 'MEDU9087712', eta: '19:40', line: 'MAERSK', size: 20, type: 'RG', truck: '—', client: 'SHUFERSAL', status: 'צפוי' },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'flex', flexDirection: 'column', gap: 12, minHeight: 0 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
          <h1 style={{ margin: 0, fontSize: 24, fontWeight: 800 }}>⏰ מכולות צפויות</h1>
          <ModernChip>{rows.length} צפויות בשעה הקרובה</ModernChip>
        </div>
        <div style={{
          flex: 1, background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          overflow: 'hidden', display: 'flex', flexDirection: 'column',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ overflow: 'auto' }}>
            {rows.map((r, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '120px 1.3fr 1fr .8fr .5fr .5fr 1.2fr 1fr',
                padding: '16px 18px', gap: 12, alignItems: 'center',
                borderBottom: i < rows.length - 1 ? '1px solid #f1f5f9' : 'none',
              }}>
                <div style={{
                  background: r.status === 'הגיע לשער' ? M_TEAL_SOFT : '#f8fafc',
                  padding: '8px 12px', borderRadius: 10, textAlign: 'center',
                  border: '1px solid ' + (r.status === 'הגיע לשער' ? M_TEAL : M_LINE),
                }}>
                  <div style={{ fontSize: 10, color: M_MUTED, fontWeight: 700 }}>ETA</div>
                  <div style={{ fontSize: 18, fontWeight: 800, fontFamily: '"IBM Plex Mono", monospace' }}>{r.eta}</div>
                </div>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontSize: 16, fontWeight: 700 }}>{r.id}</span>
                <span style={{ color: M_INK }}>{r.line}</span>
                <span style={{ color: M_MUTED }}>{r.client}</span>
                <ModernChip>{r.size}'</ModernChip>
                <ModernChip kind="handle">{r.type}</ModernChip>
                <ModernChip>משאית {r.truck}</ModernChip>
                <span style={{
                  padding: '5px 12px', borderRadius: 999, fontSize: 12, fontWeight: 700,
                  background: r.status === 'הגיע לשער' ? '#dcfce7' : r.status === 'בדרך' ? '#fef3c7' : '#f1f5f9',
                  color: r.status === 'הגיע לשער' ? '#166534' : r.status === 'בדרך' ? '#92400e' : M_MUTED,
                  border: '1px solid ' + (r.status === 'הגיע לשער' ? '#bbf7d0' : r.status === 'בדרך' ? '#fde68a' : M_LINE),
                  textAlign: 'center',
                }}>{r.status}</span>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

// ────────── EMPTY / NO-LOC (combined split screen) ──────────
function ModernEmptyAndNoLoc() {
  const empty = [
    { id: 'TCLU4421128', loc: '154A1', size: 40, type: 'HC', days: 12 },
    { id: 'MEDU3342190', loc: '158B3', size: 40, type: 'HC', days: 8 },
    { id: 'CAAU7719020', loc: '162C2', size: 20, type: 'RG', days: 4 },
    { id: 'FSCU1234567', loc: '166C4', size: 40, type: 'HC', days: 22 },
    { id: 'HMMU8810294', loc: '170D2', size: 40, type: 'HC', days: 3 },
    { id: 'MSNU5566778', loc: '174E1', size: 20, type: 'RG', days: 15 },
  ];
  const noloc = [
    { id: 'ZIMU8080808', when: '17:20', reason: 'GPS לא עודכן' },
    { id: 'MAEU1122334', when: '16:45', reason: 'זיהוי אוטומטי נכשל' },
    { id: 'ONEU9988776', when: '15:30', reason: 'פריקה לא מאושרת' },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 14, minHeight: 0 }}>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ padding: 14, borderBottom: '1px solid ' + M_LINE, display: 'flex', gap: 10, alignItems: 'center' }}>
            <span style={{
              width: 30, height: 30, borderRadius: 8, background: M_TEAL, color: '#fff',
              display: 'grid', placeItems: 'center', fontSize: 14,
            }}>📦</span>
            <div style={{ fontSize: 16, fontWeight: 800 }}>מכולות ריקות</div>
            <ModernChip>{empty.length}</ModernChip>
          </div>
          <div style={{ overflow: 'auto', padding: 8 }}>
            {empty.map((r, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '1.4fr 1fr .5fr .5fr .8fr',
                padding: '14px 14px', borderRadius: 10, marginBottom: 4,
                background: i === 0 ? M_TEAL_SOFT : 'transparent',
                border: i === 0 ? '1.5px solid ' + M_TEAL : '1px solid transparent',
                alignItems: 'center',
              }}>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700, fontSize: 15 }}>{r.id}</span>
                <ModernChip>{r.loc}</ModernChip>
                <span style={{ fontSize: 13, color: M_MUTED, textAlign: 'center' }}>{r.size}'</span>
                <span style={{ fontSize: 13, color: M_MUTED, textAlign: 'center' }}>{r.type}</span>
                <span style={{
                  fontSize: 12, fontWeight: 700,
                  color: r.days > 14 ? M_RED : r.days > 7 ? M_AMBER : M_MUTED,
                }}>{r.days} ימים</span>
              </div>
            ))}
          </div>
        </div>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ padding: 14, borderBottom: '1px solid ' + M_LINE, display: 'flex', gap: 10, alignItems: 'center' }}>
            <span style={{
              width: 30, height: 30, borderRadius: 8, background: M_AMBER, color: '#fff',
              display: 'grid', placeItems: 'center', fontSize: 14,
            }}>❓</span>
            <div style={{ fontSize: 16, fontWeight: 800 }}>מכולות ללא איתור</div>
            <ModernChip kind="weight">{noloc.length} דחוף</ModernChip>
          </div>
          <div style={{ overflow: 'auto', padding: 8 }}>
            {noloc.map((r, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '1.4fr .8fr 2fr auto',
                padding: '16px 14px', borderRadius: 10, marginBottom: 4,
                background: '#fffbeb', border: '1px solid #fde68a',
                alignItems: 'center', gap: 10,
              }}>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700, fontSize: 15 }}>{r.id}</span>
                <span style={{ fontSize: 12, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>{r.when}</span>
                <span style={{ fontSize: 13, color: '#92400e' }}>{r.reason}</span>
                <button style={{
                  padding: '8px 14px', background: M_AMBER, color: '#fff',
                  border: 'none', borderRadius: 8, fontSize: 12, fontWeight: 700,
                  fontFamily: 'inherit', cursor: 'pointer',
                }}>עדכן איתור</button>
              </div>
            ))}
            <div style={{ padding: 20, textAlign: 'center', color: M_MUTED, fontSize: 13 }}>
              אין מכולות נוספות ללא איתור
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

// ────────── RTG SETTINGS ──────────
function ModernRTGSettings() {
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '1fr 380px', gap: 14, minHeight: 0 }}>
        <div style={{ display: 'flex', flexDirection: 'column', gap: 14 }}>
          <div style={{
            background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16, padding: 18,
          }}>
            <div style={{ fontSize: 12, color: M_MUTED, fontWeight: 700, letterSpacing: 1 }}>מצב חיבור</div>
            <h2 style={{ margin: '4px 0 14px', fontSize: 20, fontWeight: 800 }}>חיבורים ותקשורת</h2>
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 10 }}>
              <DetailTile label="PLC" value="מחובר" color={M_GREEN} />
              <DetailTile label="GPS Fix" value="RTK" color={M_GREEN} />
              <DetailTile label="לווינים" value="14" mono />
              <DetailTile label="דיוק" value="0.8" unit="מ׳" mono />
              <DetailTile label="HMI" value="מחובר" color={M_GREEN} />
              <DetailTile label="שרת TOS" value="online" mono color={M_GREEN} />
            </div>
          </div>
          <div style={{
            background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16, padding: 18,
            flex: 1,
          }}>
            <h2 style={{ margin: '0 0 14px', fontSize: 20, fontWeight: 800 }}>מיקום נוכחי</h2>
            <div style={{
              background: '#f8fafc', border: '1px solid ' + M_LINE, borderRadius: 12,
              padding: 16, display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 10,
            }}>
              <DetailTile label="Latitude"  value="32.01834°N" mono />
              <DetailTile label="Longitude" value="34.75531°E" mono />
              <DetailTile label="גובה" value="12.4" unit="מ׳" mono />
              <DetailTile label="כיוון" value="87°" mono />
              <DetailTile label="מהירות" value="0.4" unit="מ/ש" mono />
              <DetailTile label="עדכון אחרון" value="1 שנ׳" mono color={M_GREEN} />
            </div>
          </div>
        </div>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16, padding: 18,
          display: 'flex', flexDirection: 'column', gap: 14,
        }}>
          <h2 style={{ margin: 0, fontSize: 20, fontWeight: 800 }}>פעולות</h2>
          {[
            { label: 'כיול מחדש של GPS',   color: M_TEAL, icon: '🛰️' },
            { label: 'בדיקת PLC',           color: '#0369a1', icon: '⚙️' },
            { label: 'סנכרון עם TOS',      color: M_VIOLET, icon: '🔄' },
            { label: 'מצב תחזוקה',          color: M_AMBER, icon: '🔧' },
            { label: 'הפעלה מחדש',          color: M_RED, icon: '⟳' },
          ].map(a => (
            <button key={a.label} style={{
              height: 64, background: '#fff', color: a.color,
              border: '1.5px solid ' + a.color + '60', borderRadius: 12,
              display: 'flex', alignItems: 'center', gap: 12, padding: '0 18px',
              fontSize: 16, fontWeight: 700, fontFamily: 'inherit', cursor: 'pointer',
              justifyContent: 'flex-start',
            }}>
              <span style={{ fontSize: 22 }}>{a.icon}</span>
              {a.label}
              <span style={{ flex: 1 }} />
              <span style={{ fontSize: 18 }}>←</span>
            </button>
          ))}
          <div style={{ flex: 1 }} />
          <div style={{ fontSize: 11, color: M_MUTED, textAlign: 'center' }}>
            גרסה 4.2.1 · build 20260415
          </div>
        </div>
      </div>
    </div>
  );
}

Object.assign(window, { ModernSuggested, ModernExpected, ModernEmptyAndNoLoc, ModernRTGSettings });
