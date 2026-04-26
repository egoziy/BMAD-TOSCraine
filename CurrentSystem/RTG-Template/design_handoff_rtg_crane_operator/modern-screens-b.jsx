// Modern · Jobs list · Truck load/unload · Movements log

// ────────── JOBS LIST ──────────
function ModernJobsList() {
  const tabs = [
    { id: 'all', label: 'כל העבודות', count: JOBS.length, active: true },
    { id: 'block1', label: 'BOND1', count: JOBS.filter(j => j.block === 'BOND1').length },
    { id: 'block2', label: 'BOND2', count: JOBS.filter(j => j.block === 'BOND2').length },
    { id: 'other', label: 'HGC6', count: JOBS.filter(j => j.block === 'HGC6').length },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'flex', flexDirection: 'column', gap: 12, minHeight: 0 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
          <h1 style={{ margin: 0, fontSize: 24, fontWeight: 800 }}>רשימת עבודות</h1>
          <ModernChip>{JOBS.length} עבודות</ModernChip>
          <div style={{ flex: 1 }} />
          <input placeholder="חיפוש..." style={{
            padding: '12px 16px', border: '1px solid ' + M_LINE, borderRadius: 12,
            fontSize: 14, fontFamily: 'inherit', width: 280, background: M_CARD,
          }} />
          <button style={{
            padding: '12px 20px', background: M_CARD, color: M_INK,
            border: '1px solid ' + M_LINE, borderRadius: 12, fontSize: 14, fontWeight: 700,
            fontFamily: 'inherit', cursor: 'pointer',
          }}>סינון ▾</button>
        </div>
        <div style={{ display: 'flex', gap: 8 }}>
          {tabs.map(t => (
            <button key={t.id} style={{
              padding: '10px 18px', borderRadius: 12,
              background: t.active ? M_TEAL : M_CARD,
              color: t.active ? '#fff' : M_INK,
              border: '1px solid ' + (t.active ? M_TEAL : M_LINE),
              fontSize: 14, fontWeight: 700, fontFamily: 'inherit', cursor: 'pointer',
              display: 'flex', alignItems: 'center', gap: 8,
            }}>
              {t.label}
              <span style={{
                background: t.active ? 'rgba(255,255,255,.25)' : '#f1f5f9',
                color: t.active ? '#fff' : M_MUTED,
                padding: '1px 7px', borderRadius: 999, fontSize: 11,
              }}>{t.count}</span>
            </button>
          ))}
        </div>
        <div style={{
          flex: 1, background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          overflow: 'hidden', display: 'flex', flexDirection: 'column',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{
            display: 'grid', gridTemplateColumns: '1.4fr .5fr .5fr .9fr .8fr .7fr 1.6fr 1fr .6fr',
            padding: '12px 14px', borderBottom: '1px solid ' + M_LINE,
            background: '#f8fafc', fontSize: 12, color: M_MUTED, fontWeight: 700, letterSpacing: .5,
          }}>
            <div>מכולה</div><div>גודל</div><div>סוג</div><div>משקל</div>
            <div>איתור</div><div>מקום</div><div>עבודה</div><div>מתי</div><div></div>
          </div>
          <div style={{ overflow: 'auto', flex: 1 }}>
            {JOBS.map((j, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '1.4fr .5fr .5fr .9fr .8fr .7fr 1.6fr 1fr .6fr',
                padding: '14px 14px', borderBottom: '1px solid #f1f5f9',
                alignItems: 'center', fontSize: 14,
                background: i === 0 ? M_TEAL_SOFT : 'transparent',
              }}>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700 }}>{j.id}</span>
                <span style={{ color: M_MUTED }}>{j.size}'</span>
                <span style={{ color: M_MUTED }}>{j.type}</span>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace' }}>{j.weight.toLocaleString()}</span>
                <ModernChip>{j.loc}</ModernChip>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', color: M_MUTED }}>{j.place}</span>
                <span>{j.task}</span>
                <span style={{ fontSize: 12, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>{j.when}</span>
                <button style={{
                  padding: '6px 12px', background: M_TEAL, color: '#fff',
                  border: 'none', borderRadius: 8, fontSize: 12, fontWeight: 700,
                  fontFamily: 'inherit', cursor: 'pointer',
                }}>בחר</button>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

// ────────── TRUCK LOAD/UNLOAD ──────────
function ModernTruck() {
  const unload = [
    { id: 'MSDU8925563', size: 40, type: 'HC', weight: 4400,  code: 'PP', line: 'MSC',  truck: '6B', loc: '—',     status: 'ממתין' },
    { id: 'TCKU1278039', size: 20, type: 'RG', weight: 13863, code: 'EX', line: 'ZIM',  truck: '26', loc: '9.0',   status: 'פעיל', active: true },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateRows: 'auto 1fr 1fr', gap: 12, minHeight: 0 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
          <h1 style={{ margin: 0, fontSize: 24, fontWeight: 800 }}>🚚 טעינה/פריקת משאיות</h1>
          <div style={{ flex: 1 }} />
          <TruckBayCard bay="6B" plate="55-123-45" driver="משה לוי" waiting="12:05" active={false} />
          <TruckBayCard bay="26" plate="87-456-78" driver="אבי כהן" waiting="12:18" active={true} />
          <button style={{
            padding: '12px 20px', background: M_TEAL, color: '#fff',
            border: 'none', borderRadius: 12, fontSize: 14, fontWeight: 700,
            fontFamily: 'inherit', cursor: 'pointer',
          }}>+ משאית</button>
        </div>

        <TruckTable title="פריקה · מכולות לירידה" rows={unload} kind="unload" />
        <TruckTable title="טעינה · מכולות לעליה" rows={[
          { id: 'FSCU8345625', size: 40, type: 'HC', weight: 32000, code: 'FR', line: 'MSC', truck: '26', loc: '174B4', status: 'מוכן להעברה' },
          { id: 'CAAU8074056', size: 40, type: 'HC', weight: 27800, code: 'PP', line: 'ZIM', truck: '26', loc: '162C1', status: 'מוכן להעברה' },
          { id: 'MEDU8828289', size: 40, type: 'HC', weight: 26920, code: 'PP', line: 'MSC', truck: '26', loc: 'ממתין', status: 'ממתין' },
        ]} kind="load" />
      </div>
    </div>
  );
}

function TruckBayCard({ bay, plate, driver, waiting, active }) {
  return (
    <div style={{
      background: active ? M_TEAL_SOFT : M_CARD,
      border: '1.5px solid ' + (active ? M_TEAL : M_LINE),
      borderRadius: 12, padding: '8px 14px',
      display: 'flex', alignItems: 'center', gap: 12,
    }}>
      <div style={{
        width: 40, height: 40, borderRadius: 10,
        background: active ? M_TEAL : '#f1f5f9',
        color: active ? '#fff' : M_INK,
        display: 'grid', placeItems: 'center',
        fontWeight: 800, fontSize: 16,
        fontFamily: '"IBM Plex Mono", monospace',
      }}>{bay}</div>
      <div style={{ lineHeight: 1.2 }}>
        <div style={{ fontSize: 13, fontWeight: 700 }}>{driver}</div>
        <div style={{ fontSize: 11, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>{plate} · ממתין {waiting}</div>
      </div>
    </div>
  );
}

function TruckTable({ title, rows, kind }) {
  const color = kind === 'unload' ? M_AMBER : M_TEAL;
  const bg = kind === 'unload' ? '#fffbeb' : '#f0fdfa';
  return (
    <div style={{
      background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
      overflow: 'hidden', display: 'flex', flexDirection: 'column',
      boxShadow: '0 1px 3px rgba(15,23,42,.04)',
    }}>
      <div style={{
        padding: '12px 16px', display: 'flex', alignItems: 'center', gap: 10,
        background: bg, borderBottom: '1px solid ' + M_LINE,
      }}>
        <span style={{
          width: 30, height: 30, borderRadius: 8, background: color, color: '#fff',
          display: 'grid', placeItems: 'center', fontSize: 14, fontWeight: 800,
        }}>{kind === 'unload' ? '↓' : '↑'}</span>
        <div style={{ fontSize: 16, fontWeight: 800 }}>{title}</div>
        <ModernChip>{rows.length} מכולות</ModernChip>
      </div>
      <div style={{
        display: 'grid', gridTemplateColumns: '1.3fr .5fr .5fr .9fr .5fr .6fr .5fr .9fr 1fr .7fr',
        padding: '10px 14px', borderBottom: '1px solid ' + M_LINE,
        background: '#f8fafc', fontSize: 11, color: M_MUTED, fontWeight: 700,
      }}>
        <div>מכולה</div><div>גודל</div><div>סוג</div><div>משקל</div>
        <div>קוד</div><div>קו</div><div>שער</div><div>איתור</div><div>סטטוס</div><div></div>
      </div>
      <div style={{ overflow: 'auto', flex: 1 }}>
        {rows.map((r, i) => (
          <div key={i} style={{
            display: 'grid', gridTemplateColumns: '1.3fr .5fr .5fr .9fr .5fr .6fr .5fr .9fr 1fr .7fr',
            padding: '12px 14px', borderBottom: '1px solid #f1f5f9',
            alignItems: 'center', fontSize: 13,
            background: r.active ? M_TEAL_SOFT : 'transparent',
          }}>
            <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700 }}>{r.id}</span>
            <span style={{ color: M_MUTED }}>{r.size}'</span>
            <span style={{ color: M_MUTED }}>{r.type}</span>
            <span style={{ fontFamily: '"IBM Plex Mono", monospace' }}>{r.weight.toLocaleString()}</span>
            <ModernChip kind="handle">{r.code}</ModernChip>
            <span>{r.line}</span>
            <ModernChip>{r.truck}</ModernChip>
            <span style={{ fontFamily: '"IBM Plex Mono", monospace' }}>{r.loc}</span>
            <span style={{
              color: r.status === 'מוכן להעברה' ? M_GREEN : r.status === 'פעיל' ? M_TEAL : M_AMBER,
              fontWeight: 700,
            }}>{r.status}</span>
            <button style={{
              padding: '6px 10px', background: color, color: '#fff',
              border: 'none', borderRadius: 8, fontSize: 11, fontWeight: 700,
              fontFamily: 'inherit', cursor: 'pointer',
            }}>בחר</button>
          </div>
        ))}
      </div>
    </div>
  );
}

// ────────── MOVEMENTS LOG ──────────
function ModernLog() {
  const entries = [
    { t: '17:41:22', c: 'MSNU8552840', from: '138C6', to: '170E4', user: 'GOLD1', state: 'הושלם' },
    { t: '17:38:05', c: 'CAAU8074056', from: '162C1', to: 'משאית 26', user: 'GOLD1', state: 'הושלם' },
    { t: '17:32:41', c: 'TGHU9821299', from: 'משאית 6B', to: '154A1', user: 'GOLD1', state: 'הושלם' },
    { t: '17:28:18', c: 'FSCU8345625', from: '174B4', to: '174B5', user: 'GOLD1', state: 'בוטל', err: true },
    { t: '17:21:02', c: 'MEDU8828289', from: '146D3', to: '146D4', user: 'GOLD1', state: 'הושלם' },
    { t: '17:14:55', c: 'FFAU6601629', from: '150C6', to: 'משאית 12', user: 'GOLD1', state: 'הושלם' },
    { t: '17:09:11', c: 'TCLU1819383', from: 'משאית 88', to: '158B2', user: 'GOLD1', state: 'הושלם' },
    { t: '17:02:44', c: 'CAAU8100200', from: '162E2', to: '166E3', user: 'GOLD1', state: 'הושלם' },
    { t: '16:58:30', c: 'MSDU8284140', from: '138C5', to: '138C6', user: 'GOLD1', state: 'הושלם' },
  ];
  return (
    <div style={{ ...modernRoot, gridTemplateRows: '76px 1fr' }}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'flex', flexDirection: 'column', gap: 12, minHeight: 0 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
          <h1 style={{ margin: 0, fontSize: 24, fontWeight: 800 }}>📋 יומן תנועות</h1>
          <ModernChip>היום · {entries.length} תנועות</ModernChip>
          <div style={{ flex: 1 }} />
          <div style={{ display: 'flex', gap: 8 }}>
            {['היום','אתמול','השבוע','טווח תאריכים'].map((t, i) => (
              <button key={t} style={{
                padding: '10px 16px', borderRadius: 10,
                background: i === 0 ? M_TEAL : M_CARD, color: i === 0 ? '#fff' : M_INK,
                border: '1px solid ' + (i === 0 ? M_TEAL : M_LINE),
                fontSize: 13, fontWeight: 700, fontFamily: 'inherit', cursor: 'pointer',
              }}>{t}</button>
            ))}
          </div>
        </div>
        <div style={{
          flex: 1, background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 16,
          overflow: 'hidden', display: 'flex', flexDirection: 'column',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ overflow: 'auto', padding: 4 }}>
            {entries.map((e, i) => (
              <div key={i} style={{
                display: 'grid', gridTemplateColumns: '120px 1.5fr 1fr auto 1fr 1fr 140px',
                padding: '16px 18px', gap: 14, alignItems: 'center',
                borderBottom: i < entries.length - 1 ? '1px solid #f1f5f9' : 'none',
              }}>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontSize: 15, fontWeight: 700, color: M_MUTED }}>{e.t}</span>
                <span style={{ fontFamily: '"IBM Plex Mono", monospace', fontSize: 15, fontWeight: 700 }}>{e.c}</span>
                <ModernChip>{e.from}</ModernChip>
                <span style={{ fontSize: 20, color: M_TEAL }}>←</span>
                <ModernChip>{e.to}</ModernChip>
                <span style={{ fontSize: 13, color: M_MUTED }}>מנופאי: <b style={{ color: M_INK }}>{e.user}</b></span>
                <span style={{
                  padding: '5px 12px', borderRadius: 999, fontSize: 12, fontWeight: 700,
                  background: e.err ? '#fee2e2' : '#dcfce7',
                  color: e.err ? '#991b1b' : '#166534',
                  border: '1px solid ' + (e.err ? '#fecaca' : '#bbf7d0'),
                  textAlign: 'center',
                }}>{e.state}</span>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

Object.assign(window, { ModernJobsList, ModernTruck, ModernLog, TruckBayCard, TruckTable });
