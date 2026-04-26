// Modern · All secondary screens

// ────────── 1. MENU (8 tiles) ──────────
function ModernMenu() {
  const tiles = [
    { id: 'empty',     label: 'מכולות ריקות', icon: '📦', color: M_TEAL, desc: 'רשימת מכולות ריקות בערוגה' },
    { id: 'noloc',     label: 'מכולות ללא איתור', icon: '❓', color: M_AMBER, desc: 'מכולות הדורשות עדכון' },
    { id: 'log',       label: 'יומן תנועות', icon: '📋', color: '#0369a1', desc: 'היסטוריית העברות' },
    { id: 'block',     label: 'מכולות לגוש', icon: '🔲', color: M_VIOLET, desc: 'חיפוש לפי קוד גוש' },
    { id: 'update',    label: 'עדכון איתור', icon: '✏️', color: '#0891b2', desc: 'עדכון מיקום מכולה' },
    { id: 'suggested', label: 'איתורים מומלצים', icon: '⭐', color: '#ea580c', desc: 'המלצות המערכת' },
    { id: 'expected',  label: 'מכולות צפויות', icon: '⏰', color: '#6366f1', desc: 'מכולות שצפויות להגיע' },
    { id: 'rtg',       label: 'הגדרות RTG', icon: '⚙️', color: '#475569', desc: 'הגדרות מנוף' },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 24, display: 'flex', flexDirection: 'column', gap: 16, minHeight: 0 }}>
        <div style={{ display: 'flex', alignItems: 'baseline', gap: 12 }}>
          <h1 style={{ margin: 0, fontSize: 28, fontWeight: 800, color: M_INK }}>תפריט ראשי</h1>
          <div style={{ fontSize: 14, color: M_MUTED }}>בחר פעולה</div>
        </div>
        <div style={{
          display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gridTemplateRows: '1fr 1fr',
          gap: 18, flex: 1,
        }}>
          {tiles.map(t => (
            <button key={t.id} style={{
              background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
              padding: 20, display: 'flex', flexDirection: 'column', gap: 10,
              alignItems: 'flex-start', textAlign: 'right',
              boxShadow: '0 1px 3px rgba(15,23,42,.05)', cursor: 'pointer',
              fontFamily: 'inherit', position: 'relative', overflow: 'hidden',
            }}>
              <div style={{
                position: 'absolute', top: 0, right: 0, width: 5, height: '100%',
                background: t.color,
              }} />
              <div style={{
                width: 56, height: 56, borderRadius: 14,
                background: t.color + '1a', color: t.color,
                display: 'grid', placeItems: 'center', fontSize: 28,
              }}>{t.icon}</div>
              <div style={{ fontSize: 20, fontWeight: 800, color: M_INK }}>{t.label}</div>
              <div style={{ fontSize: 13, color: M_MUTED, lineHeight: 1.3 }}>{t.desc}</div>
            </button>
          ))}
        </div>
        <button style={{
          height: 64, background: '#fff', color: M_INK,
          border: '1.5px solid ' + M_LINE, borderRadius: 14,
          fontSize: 17, fontWeight: 700, fontFamily: 'inherit', cursor: 'pointer',
          display: 'flex', alignItems: 'center', justifyContent: 'center', gap: 10,
        }}>← חזרה למסך הערוגה</button>
      </div>
    </div>
  );
}

// ────────── 2. NUMPAD ──────────
function ModernNumpad() {
  const [val, setVal] = React.useState('2.3');
  const press = (k) => {
    if (k === 'C') setVal('');
    else if (k === '←') setVal(v => v.slice(0, -1));
    else setVal(v => (v + k).slice(0, 6));
  };
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '380px 1fr', gap: 16, minHeight: 0 }}>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          padding: 16, display: 'flex', flexDirection: 'column', gap: 12,
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
            <span style={{
              width: 30, height: 30, borderRadius: 8, background: M_TEAL, color: '#fff',
              display: 'grid', placeItems: 'center', fontSize: 16,
            }}>🔲</span>
            <div style={{ lineHeight: 1.1 }}>
              <div style={{ fontSize: 15, fontWeight: 700 }}>בחר חומ"ס</div>
              <div style={{ fontSize: 11, color: M_MUTED }}>הזן קוד גוש</div>
            </div>
          </div>
          <div style={{
            background: '#f8fafc', border: '2px solid ' + M_TEAL, borderRadius: 12,
            padding: '18px 18px', fontSize: 44, fontWeight: 800, textAlign: 'center',
            fontFamily: '"IBM Plex Mono", monospace', color: M_TEAL_DEEP,
            letterSpacing: 3, minHeight: 76,
          }}>
            {val || <span style={{ color: '#cbd5e1' }}>—</span>}
          </div>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8 }}>
            {['1','2','3','4','5','6','7','8','9','←','0','C'].map(k => {
              const isDel = k === 'C';
              const isBack = k === '←';
              return (
                <button key={k} onClick={() => press(k)} style={{
                  height: 72,
                  background: isDel ? '#fef2f2' : isBack ? '#fffbeb' : '#fff',
                  color: isDel ? M_RED : isBack ? M_AMBER : M_INK,
                  border: '1.5px solid ' + (isDel ? '#fecaca' : isBack ? '#fde68a' : M_LINE),
                  borderRadius: 12, fontSize: 30, fontWeight: 700,
                  fontFamily: '"IBM Plex Mono", monospace', cursor: 'pointer',
                  boxShadow: '0 1px 2px rgba(15,23,42,.04)',
                }}>{k}</button>
              );
            })}
          </div>
          <button style={{
            height: 68, background: M_GREEN, color: '#fff',
            border: 'none', borderRadius: 14, fontSize: 22, fontWeight: 700,
            fontFamily: 'inherit', cursor: 'pointer',
            boxShadow: '0 6px 14px ' + M_GREEN + '55',
          }}>אישור</button>
        </div>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ padding: 14, borderBottom: '1px solid ' + M_LINE, display: 'flex', gap: 10, alignItems: 'center' }}>
            <div style={{ fontSize: 15, fontWeight: 700 }}>בחר איתור</div>
            <ModernChip>{13} תוצאות</ModernChip>
            <div style={{ flex: 1 }} />
            <input type="text" placeholder="חיפוש..." style={{
              padding: '10px 14px', border: '1px solid ' + M_LINE, borderRadius: 10,
              fontSize: 14, fontFamily: 'inherit', width: 240, background: '#f8fafc',
            }} />
          </div>
          <div style={{ flex: 1, overflow: 'auto', padding: 8 }}>
            {[
              { id: 'SEGU8099692', sz: 20, when: '10:03 26/10/25', loc: 'שטיפה TK', active: true },
              { id: 'SEGU8099120', sz: 20, when: '12:17 27/11/25', loc: 'שטיפה TK' },
              { id: 'GOJU9225095', sz: 20, when: '12:21 27/11/25', loc: '969' },
              { id: 'SEGU8099141', sz: 20, when: '10:49 4/12/25',  loc: '994A' },
              { id: 'SEGU8099579', sz: 20, when: '10:51 4/12/25',  loc: '994A' },
              { id: 'SEGU8099753', sz: 20, when: '6:09 1/1/26',    loc: '994A' },
              { id: 'TMLU9251392', sz: 20, when: '6:09 1/1/26',    loc: '972' },
              { id: 'SEGU8099373', sz: 20, when: '6:11 1/1/26',    loc: '994A' },
              { id: 'SEGU8099563', sz: 20, when: '6:11 1/1/26',    loc: '975' },
              { id: 'SEGU8099331', sz: 20, when: '11:45 18/1/26',  loc: '992A' },
              { id: 'SEGU8099394', sz: 20, when: '11:45 18/1/26',  loc: '992A' },
              { id: 'PCVU7228105', sz: 20, when: '9:34 18/2/26',   loc: '994A' },
              { id: 'TMLU9250760', sz: 20, when: '9:35 18/2/26',   loc: '994A' },
            ].map((r, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '1.4fr .5fr 1fr .8fr',
                padding: '12px 14px', borderRadius: 10, marginBottom: 4,
                background: r.active ? M_TEAL_SOFT : 'transparent',
                border: r.active ? '1.5px solid ' + M_TEAL : '1px solid transparent',
                alignItems: 'center', cursor: 'pointer',
              }}>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700, fontSize: 15 }}>{r.id}</span>
                <span style={{ fontSize: 13, color: M_MUTED }}>{r.sz}'</span>
                <span style={{ fontSize: 13, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>{r.when}</span>
                <ModernChip>{r.loc}</ModernChip>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

// ────────── 3. CONTAINER DETAIL + Deal list ──────────
function ModernContainerDetail() {
  const c = ACTIVE_JOB.container;
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '1fr 360px', gap: 16, minHeight: 0 }}>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ padding: 14, borderBottom: '1px solid ' + M_LINE, display: 'flex', gap: 10, alignItems: 'center' }}>
            <div style={{ fontSize: 16, fontWeight: 700 }}>מכולות בעיסקה</div>
            <ModernChip>{DEAL_CONTAINERS.length}</ModernChip>
            <div style={{ flex: 1 }} />
            <div style={{ fontSize: 12, color: M_MUTED }}>סינון לפי סוג/גודל</div>
          </div>
          <div style={{ flex: 1, overflow: 'auto', padding: 8 }}>
            {DEAL_CONTAINERS.map((cc, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '1.4fr 1fr .5fr .5fr 1fr',
                padding: '14px 14px', borderRadius: 10, marginBottom: 4,
                background: i === 0 ? M_TEAL_SOFT : (i % 2 ? '#f8fafc' : 'transparent'),
                border: i === 0 ? '1.5px solid ' + M_TEAL : '1px solid transparent',
                alignItems: 'center',
              }}>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700, fontSize: 15 }}>{cc.id}</span>
                <ModernChip>{cc.loc}</ModernChip>
                <span style={{ fontSize: 13, color: M_MUTED, textAlign: 'center' }}>{cc.size}'</span>
                <span style={{ fontSize: 13, color: M_MUTED, textAlign: 'center' }}>{cc.type}</span>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontSize: 14, fontWeight: 600, textAlign: 'left' }}>{cc.weight.toLocaleString()} <span style={{ color: M_MUTED, fontSize: 11 }}>ק"ג</span></span>
              </div>
            ))}
          </div>
        </div>
        <div style={{
          background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          padding: 18, display: 'flex', flexDirection: 'column', gap: 14,
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div style={{ fontSize: 13, color: M_MUTED, fontWeight: 700, letterSpacing: .5 }}>פרטי מכולה</div>
            <button style={{
              width: 36, height: 36, border: '1px solid ' + M_LINE,
              background: '#f8fafc', borderRadius: 10, fontSize: 16, cursor: 'pointer',
            }}>✕</button>
          </div>
          <div style={{
            background: 'linear-gradient(135deg, #ccfbf1 0%, #a7f3d0 100%)',
            borderRadius: 12, padding: 16, textAlign: 'center',
          }}>
            <div style={{ fontSize: 11, color: M_TEAL_DEEP, fontWeight: 700, letterSpacing: 1 }}>CONTAINER ID</div>
            <div style={{ fontFamily: '"IBM Plex Mono", monospace', fontSize: 24, fontWeight: 800, color: M_TEAL_DEEP, marginTop: 4 }}>{c.id}</div>
          </div>
          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 8 }}>
            <DetailTile label="גודל" value={c.size + "'"} />
            <DetailTile label="סוג" value={c.type} />
            <DetailTile label="טיפול" value={c.handling} color={M_AMBER} />
            <DetailTile label="איתור" value={c.location} mono />
            <DetailTile label="משקל" value={c.weight.toLocaleString()} unit='ק"ג' mono />
            <DetailTile label="קפסיטי" value={c.capacity} />
          </div>
          <div style={{ background: '#f8fafc', border: '1px solid ' + M_LINE, borderRadius: 10, padding: 12 }}>
            <Row k="לקוח" v={c.customer} />
            <Row k="סיכון" v="—" />
            <Row k="עבודה" v="שיקוף מכולה" />
            <Row k="התרה" v="—" last />
          </div>
          <div style={{ flex: 1 }} />
          <button style={{
            height: 60, background: M_GREEN, color: '#fff',
            border: 'none', borderRadius: 14, fontSize: 19, fontWeight: 700,
            fontFamily: 'inherit', cursor: 'pointer',
            boxShadow: '0 6px 14px ' + M_GREEN + '55',
          }}>בחר מכולה זו</button>
        </div>
      </div>
    </div>
  );
}

function DetailTile({ label, value, unit, mono, color }) {
  return (
    <div style={{ background: '#f8fafc', border: '1px solid ' + M_LINE, borderRadius: 10, padding: 10 }}>
      <div style={{ fontSize: 11, color: M_MUTED, fontWeight: 700 }}>{label}</div>
      <div style={{
        fontSize: 20, fontWeight: 800, color: color || M_INK, marginTop: 2,
        fontFamily: mono ? '"IBM Plex Mono", monospace' : 'inherit',
      }}>
        {value} {unit && <span style={{ fontSize: 11, color: M_MUTED, fontWeight: 600 }}>{unit}</span>}
      </div>
    </div>
  );
}

function Row({ k, v, last }) {
  return (
    <div style={{
      display: 'flex', justifyContent: 'space-between', padding: '6px 2px',
      borderBottom: last ? 'none' : '1px solid #eef2f7', fontSize: 13,
    }}>
      <span style={{ color: M_MUTED }}>{k}</span>
      <span style={{ fontWeight: 700 }}>{v}</span>
    </div>
  );
}

Object.assign(window, { ModernMenu, ModernNumpad, ModernContainerDetail, DetailTile, Row });
