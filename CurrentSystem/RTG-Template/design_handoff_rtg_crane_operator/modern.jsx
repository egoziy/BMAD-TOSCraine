// VARIATION 2 — MODERN (Material-inspired)
// Light surface, teal primary, amber accent, cards with elevation, pill chips.
// Grid uses soft color-coded backgrounds per stack height.
// Larger hit targets, clearer separation of source vs target, subtle shadows.

const M_TEAL = '#0f766e';
const M_TEAL_DEEP = '#115e59';
const M_AMBER = '#d97706';
const M_GREEN = '#16a34a';
const M_RED = '#dc2626';
const M_SURFACE = '#f6f8fa';
const M_INK = '#0f172a';
const M_MUTED = '#64748b';

const modernStyles = {
  root: {
    width: '100%', height: '100%',
    background: M_SURFACE,
    fontFamily: '"Heebo", system-ui, sans-serif',
    color: M_INK,
    display: 'grid',
    gridTemplateRows: '64px 1fr',
    direction: 'rtl',
  },
};

// Stack height → colored tile background
function modernStackTile(h, isSource, isTarget) {
  if (isSource) return { bg: 'linear-gradient(135deg, #fef3c7 0%, #fde68a 100%)', fg: '#78350f', border: '2.5px solid ' + M_AMBER, ring: true };
  if (isTarget) return { bg: 'linear-gradient(135deg, #ccfbf1 0%, #99f6e4 100%)', fg: M_TEAL_DEEP, border: '2.5px dashed ' + M_TEAL, ring: true };
  if (h === null || h === undefined) return { bg: '#fff', fg: '#cbd5e1', border: '1px solid #eef2f7' };
  // Fullness gradient: green (low) → amber (mid) → red (full)
  if (h === 6) return { bg: '#fef2f2', fg: '#b91c1c', border: '1px solid #fecaca' };
  if (h === 5) return { bg: '#fff7ed', fg: '#c2410c', border: '1px solid #fed7aa' };
  if (h === 4) return { bg: '#fefce8', fg: '#a16207', border: '1px solid #fde68a' };
  if (h === 3) return { bg: '#f0fdf4', fg: '#15803d', border: '1px solid #bbf7d0' };
  if (h === 2) return { bg: '#ecfdf5', fg: '#047857', border: '1px solid #a7f3d0' };
  if (h === 1) return { bg: '#eff6ff', fg: '#1d4ed8', border: '1px solid #bfdbfe' };
  return { bg: '#fff', fg: M_INK, border: '1px solid #eef2f7' };
}

// Top HUD with crane status
function ModernTopBar({ crane, job }) {
  return (
    <div style={{
      background: '#fff',
      borderBottom: '1px solid #e2e8f0',
      boxShadow: '0 1px 2px rgba(15,23,42,.04)',
      display: 'flex',
      alignItems: 'center',
      padding: '0 20px',
      gap: 16,
    }}>
      <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
        <div style={{
          width: 36, height: 36, borderRadius: 10,
          background: M_TEAL, color: '#fff',
          display: 'grid', placeItems: 'center',
          fontSize: 18, fontWeight: 800,
        }}>R</div>
        <div style={{ lineHeight: 1.1 }}>
          <div style={{ fontSize: 14, fontWeight: 700, color: M_INK }}>מערכת מנופאי</div>
          <div style={{ fontSize: 11, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>RTG · {crane.block}</div>
        </div>
      </div>
      <div style={{ width: 1, height: 32, background: '#e2e8f0', marginInline: 4 }} />
      <ModernPill icon="🏗️" label="מנוף" value={crane.id} />
      <ModernPill icon="📍" label="מיקום" value={crane.position} mono />
      <ModernPill icon="📦" label="נושא" value={crane.carrying ? crane.carrying.id : 'ריק'} muted={!crane.carrying} />
      <div style={{ flex: 1 }} />
      <div style={{ display: 'flex', gap: 8 }}>
        <Indicator ok label="PLC" />
        <Indicator ok label="GPS" />
      </div>
      <div style={{ width: 1, height: 32, background: '#e2e8f0' }} />
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', lineHeight: 1.1 }}>
        <div style={{ fontSize: 20, fontWeight: 700, fontFamily: '"IBM Plex Mono", monospace' }}>17:42:34</div>
        <div style={{ fontSize: 11, color: M_MUTED }}>ד׳ · 9 אפריל 2026</div>
      </div>
    </div>
  );
}

function ModernPill({ icon, label, value, mono, muted }) {
  return (
    <div style={{
      display: 'flex', alignItems: 'center', gap: 10,
      background: '#f8fafc', border: '1px solid #e2e8f0',
      borderRadius: 999, padding: '6px 14px',
    }}>
      <span style={{ fontSize: 15 }}>{icon}</span>
      <div style={{ lineHeight: 1 }}>
        <div style={{ fontSize: 10, color: M_MUTED, fontWeight: 600, letterSpacing: .3 }}>{label}</div>
        <div style={{
          fontSize: 14, fontWeight: 700,
          color: muted ? M_MUTED : M_INK,
          fontFamily: mono ? '"IBM Plex Mono", monospace' : 'inherit',
          marginTop: 1,
        }}>{value}</div>
      </div>
    </div>
  );
}

function Indicator({ label, ok }) {
  return (
    <div style={{
      display: 'flex', alignItems: 'center', gap: 6,
      padding: '6px 10px', borderRadius: 6,
      background: ok ? '#f0fdf4' : '#fef2f2',
      border: '1px solid ' + (ok ? '#bbf7d0' : '#fecaca'),
    }}>
      <span style={{
        width: 8, height: 8, borderRadius: 8,
        background: ok ? M_GREEN : M_RED,
        boxShadow: ok ? '0 0 0 3px #bbf7d088' : '0 0 0 3px #fecaca88',
      }} />
      <span style={{ fontSize: 12, fontWeight: 700, color: ok ? '#166534' : '#991b1b' }}>{label}</span>
    </div>
  );
}

function ModernGrid({ job }) {
  return (
    <div style={{
      background: '#fff',
      border: '1px solid #e2e8f0',
      borderRadius: 14,
      boxShadow: '0 1px 3px rgba(15,23,42,.04)',
      padding: 10,
      overflow: 'hidden',
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
    }}>
      <div style={{ display: 'grid', gridTemplateColumns: '44px repeat(14, 1fr) 44px', gap: 4, flex: 1 }}>
        {/* Header row */}
        <div style={{ ...mGridCorner }}>BOND2</div>
        {COLS.map(c => (
          <div key={c} style={mGridColHdr}>{c}</div>
        ))}
        <div style={mGridCorner}></div>
        {/* Body */}
        {ROWS.map((row, rIdx) => (
          <React.Fragment key={row}>
            <div style={mGridRowHdr}>{row}</div>
            {COLS.map((col, cIdx) => {
              const h = GRID[rIdx][cIdx + 1];
              const isSource = job.source.row === row && job.source.col === col;
              const isTarget = job.target.row === row && job.target.col === col;
              const style = modernStackTile(h, isSource, isTarget);
              return (
                <div key={col} style={{
                  background: style.bg,
                  border: style.border,
                  borderRadius: 8,
                  minHeight: 62,
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  position: 'relative',
                  fontSize: 26,
                  fontWeight: 800,
                  color: style.fg,
                  fontFamily: '"IBM Plex Mono", monospace',
                }}>
                  {h !== null && !isSource && !isTarget && h}
                  {isSource && <SourceCell h={h} />}
                  {isTarget && <TargetCell h={h} />}
                </div>
              );
            })}
            <div style={mGridRowHdr}>{row}</div>
          </React.Fragment>
        ))}
      </div>
      {/* Legend */}
      <div style={{ display: 'flex', gap: 14, padding: '10px 6px 2px', alignItems: 'center' }}>
        <span style={{ fontSize: 12, color: M_MUTED, fontWeight: 600 }}>גובה ערימה:</span>
        {[
          ['1-2', '#eff6ff', '#1d4ed8', 'נמוכה'],
          ['3',   '#f0fdf4', '#15803d', 'בינונית'],
          ['4',   '#fefce8', '#a16207', 'גבוהה'],
          ['5',   '#fff7ed', '#c2410c', 'גבוהה מאוד'],
          ['6',   '#fef2f2', '#b91c1c', 'מלאה'],
        ].map(([k, bg, fg, label]) => (
          <div key={k} style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
            <span style={{
              background: bg, color: fg, fontFamily: '"IBM Plex Mono", monospace',
              padding: '2px 8px', borderRadius: 4, fontSize: 12, fontWeight: 700,
            }}>{k}</span>
            <span style={{ fontSize: 11, color: M_MUTED }}>{label}</span>
          </div>
        ))}
        <div style={{ flex: 1 }} />
        <LegendSwatch color={M_AMBER} solid label="מקור" />
        <LegendSwatch color={M_TEAL} dashed label="יעד" />
      </div>
    </div>
  );
}

function SourceCell({ h }) {
  return (
    <div style={{ position: 'relative', display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: 'column' }}>
      <div style={{ fontSize: 24, fontWeight: 800, color: '#78350f', fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1 }}>{h}</div>
      <div style={{
        position: 'absolute', top: -26, left: '50%', transform: 'translateX(-50%)',
        background: M_AMBER, color: '#fff', fontSize: 10, fontWeight: 800,
        padding: '2px 8px', borderRadius: 4, letterSpacing: .5,
        fontFamily: '"Heebo", sans-serif',
        whiteSpace: 'nowrap',
      }}>מקור</div>
    </div>
  );
}
function TargetCell({ h }) {
  return (
    <div style={{ position: 'relative', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
      <div style={{ fontSize: 24, fontWeight: 800, color: M_TEAL_DEEP, fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1 }}>
        {h}<span style={{ fontSize: 14, opacity: .5 }}>→{h+1}</span>
      </div>
      <div style={{
        position: 'absolute', top: -26, left: '50%', transform: 'translateX(-50%)',
        background: M_TEAL, color: '#fff', fontSize: 10, fontWeight: 800,
        padding: '2px 8px', borderRadius: 4, letterSpacing: .5,
        fontFamily: '"Heebo", sans-serif',
        whiteSpace: 'nowrap',
      }}>יעד</div>
    </div>
  );
}

function LegendSwatch({ color, dashed, solid, label }) {
  return (
    <div style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
      <span style={{
        width: 18, height: 18, borderRadius: 4,
        border: `2px ${dashed ? 'dashed' : 'solid'} ${color}`,
        background: color + '22',
      }} />
      <span style={{ fontSize: 12, fontWeight: 700, color }}>{label}</span>
    </div>
  );
}

const mGridCorner = {
  background: M_TEAL,
  color: '#fff',
  fontSize: 12,
  fontWeight: 700,
  display: 'grid',
  placeItems: 'center',
  borderRadius: 6,
  fontFamily: '"IBM Plex Mono", monospace',
};
const mGridColHdr = {
  fontSize: 13,
  fontWeight: 700,
  color: M_MUTED,
  display: 'grid',
  placeItems: 'center',
  fontFamily: '"IBM Plex Mono", monospace',
  paddingBottom: 6,
  borderBottom: '1px solid #e2e8f0',
};
const mGridRowHdr = {
  fontSize: 17,
  fontWeight: 800,
  color: M_INK,
  background: '#f8fafc',
  display: 'grid',
  placeItems: 'center',
  borderRadius: 6,
};

// Job panel — big, clear source/target with arrow and container chips
function ModernJobPanel({ job }) {
  const c = job.container;
  return (
    <div style={{
      background: '#fff',
      border: '1px solid #e2e8f0',
      borderRadius: 14,
      boxShadow: '0 1px 3px rgba(15,23,42,.04)',
      padding: 14,
      display: 'grid',
      gridTemplateColumns: '1fr auto 1fr',
      alignItems: 'stretch',
      gap: 14,
      height: '100%',
    }}>
      {/* SOURCE */}
      <div style={{
        background: '#fffbeb',
        border: '1.5px solid #fde68a',
        borderRadius: 12,
        padding: 14,
        display: 'flex',
        flexDirection: 'column',
        gap: 8,
      }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
          <span style={{
            width: 28, height: 28, borderRadius: 8, background: M_AMBER, color: '#fff',
            display: 'grid', placeItems: 'center', fontSize: 14, fontWeight: 800,
          }}>↓</span>
          <span style={{ fontSize: 13, fontWeight: 700, color: '#92400e', letterSpacing: .5 }}>מאיתור (מקור)</span>
          <div style={{ flex: 1 }} />
          <span style={{ fontSize: 28, fontWeight: 800, color: '#78350f', fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1 }}>
            {job.source.label}
          </span>
        </div>
        <div style={{ display: 'flex', flexWrap: 'wrap', gap: 6, alignItems: 'center' }}>
          <span style={{
            fontSize: 22, fontWeight: 700, color: M_INK,
            fontFamily: '"IBM Plex Mono", monospace',
          }}>{c.id}</span>
          <ModernChip>{c.size}'</ModernChip>
          <ModernChip>{c.type}</ModernChip>
          <ModernChip kind="handle">{c.handling}</ModernChip>
          <ModernChip kind="weight">{c.weight.toLocaleString()} ק"ג</ModernChip>
          <ModernChip>לקוח {c.customer}</ModernChip>
        </div>
      </div>
      {/* ARROW */}
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', gap: 6, padding: '0 4px' }}>
        <div style={{
          background: M_TEAL, color: '#fff',
          width: 60, height: 60, borderRadius: 60,
          display: 'grid', placeItems: 'center',
          fontSize: 30, fontWeight: 800,
          boxShadow: '0 4px 12px ' + M_TEAL + '55',
        }}>←</div>
        <div style={{ fontSize: 11, color: M_MUTED, fontWeight: 700, letterSpacing: 1 }}>העברה</div>
      </div>
      {/* TARGET */}
      <div style={{
        background: '#f0fdfa',
        border: '1.5px solid #99f6e4',
        borderRadius: 12,
        padding: 14,
        display: 'flex',
        flexDirection: 'column',
        gap: 8,
      }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
          <span style={{
            width: 28, height: 28, borderRadius: 8, background: M_TEAL, color: '#fff',
            display: 'grid', placeItems: 'center', fontSize: 14, fontWeight: 800,
          }}>↑</span>
          <span style={{ fontSize: 13, fontWeight: 700, color: M_TEAL_DEEP, letterSpacing: .5 }}>לאיתור (יעד)</span>
          <div style={{ flex: 1 }} />
          <span style={{ fontSize: 28, fontWeight: 800, color: M_TEAL_DEEP, fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1 }}>
            {job.target.label}
          </span>
        </div>
        <div style={{ display: 'flex', flexWrap: 'wrap', gap: 6, alignItems: 'center' }}>
          <span style={{ fontSize: 14, color: M_INK }}>גובה נוכחי:</span>
          <ModernChip kind="stack">{job.target.height}/6</ModernChip>
          <span style={{ fontSize: 14, color: M_MUTED }}>·</span>
          <span style={{ fontSize: 14, color: '#15803d', fontWeight: 700 }}>קיבולת פנויה</span>
          <div style={{ flex: 1 }} />
          <span style={{ fontSize: 13, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>
            שורה {job.target.row} · עמ׳ {job.target.col}
          </span>
        </div>
      </div>
    </div>
  );
}

function ModernChip({ children, kind }) {
  const palette = {
    handle: { bg: '#ede9fe', fg: '#5b21b6', br: '#ddd6fe' },
    weight: { bg: '#fee2e2', fg: '#991b1b', br: '#fecaca' },
    stack:  { bg: '#dbeafe', fg: '#1e40af', br: '#bfdbfe' },
  };
  const p = palette[kind] || { bg: '#f1f5f9', fg: '#334155', br: '#e2e8f0' };
  return (
    <span style={{
      background: p.bg, color: p.fg, border: '1px solid ' + p.br,
      padding: '3px 9px', borderRadius: 999,
      fontSize: 12, fontWeight: 700,
      fontFamily: '"IBM Plex Mono", monospace',
    }}>{children}</span>
  );
}

function ModernActionBar() {
  return (
    <div style={{
      display: 'grid',
      gridTemplateColumns: '2fr 1fr 1fr 1fr 1fr 1fr',
      gap: 10,
    }}>
      <ModernBtn primary icon="✓" label="אישור העברה" color={M_GREEN} />
      <ModernBtn icon="✕" label="ביטול" color={M_RED} />
      <ModernBtn icon="🚚" label="משאית" color="#0369a1" />
      <ModernBtn icon="📋" label="עבודות" color="#0369a1" />
      <ModernBtn icon="ℹ" label="מידע" color="#6366f1" />
      <ModernBtn icon="⏻" label="יציאה" color="#475569" />
    </div>
  );
}

function ModernBtn({ icon, label, color, primary }) {
  return (
    <button style={{
      height: 76,
      background: primary ? color : '#fff',
      color: primary ? '#fff' : color,
      border: '1.5px solid ' + (primary ? color : color + '60'),
      borderRadius: 14,
      boxShadow: primary ? '0 6px 14px ' + color + '55' : '0 1px 2px rgba(15,23,42,.04)',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      gap: 10,
      fontSize: primary ? 19 : 16,
      fontWeight: 700,
      fontFamily: 'inherit',
      cursor: 'pointer',
    }}>
      <span style={{ fontSize: primary ? 24 : 20 }}>{icon}</span>
      {label}
    </button>
  );
}

function ModernMain() {
  return (
    <div style={modernStyles.root}>
      <ModernTopBar crane={CRANE} job={ACTIVE_JOB} />
      <div style={{ padding: 14, display: 'grid', gridTemplateRows: '1fr 124px 86px', gap: 12, minHeight: 0 }}>
        <ModernGrid job={ACTIVE_JOB} />
        <ModernJobPanel job={ACTIVE_JOB} />
        <ModernActionBar />
      </div>
    </div>
  );
}

// ——— Secondary ———

function ModernNumpad() {
  const [val, setVal] = React.useState('2.3');
  const press = (k) => {
    if (k === 'C') setVal('');
    else if (k === '←') setVal(v => v.slice(0, -1));
    else setVal(v => (v + k).slice(0, 6));
  };
  return (
    <div style={{ ...modernStyles.root, gridTemplateRows: '64px 1fr' }}>
      <ModernTopBar crane={CRANE} job={ACTIVE_JOB} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '380px 1fr', gap: 16, minHeight: 0 }}>
        {/* Left: numpad */}
        <div style={{
          background: '#fff', border: '1px solid #e2e8f0', borderRadius: 16,
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
                  border: '1.5px solid ' + (isDel ? '#fecaca' : isBack ? '#fde68a' : '#e2e8f0'),
                  borderRadius: 12, fontSize: 30, fontWeight: 700,
                  fontFamily: '"IBM Plex Mono", monospace',
                  cursor: 'pointer',
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
        {/* Right: list */}
        <div style={{
          background: '#fff', border: '1px solid #e2e8f0', borderRadius: 16,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ padding: 14, borderBottom: '1px solid #e2e8f0', display: 'flex', gap: 10, alignItems: 'center' }}>
            <div style={{ fontSize: 15, fontWeight: 700 }}>בחר איתור</div>
            <div style={{ flex: 1 }} />
            <input type="text" placeholder="חיפוש..." style={{
              padding: '10px 14px', border: '1px solid #e2e8f0', borderRadius: 10,
              fontSize: 14, fontFamily: 'inherit', width: 240, background: '#f8fafc',
            }} />
            <select style={{
              padding: '10px 14px', border: '1px solid #e2e8f0', borderRadius: 10,
              fontSize: 14, fontFamily: 'inherit', background: '#fff',
            }}>
              <option>— הכל —</option>
            </select>
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
                background: r.active ? '#ccfbf1' : 'transparent',
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

function ModernContainerDetail() {
  const c = ACTIVE_JOB.container;
  return (
    <div style={{ ...modernStyles.root, gridTemplateRows: '64px 1fr' }}>
      <ModernTopBar crane={CRANE} job={ACTIVE_JOB} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '1fr 360px', gap: 16, minHeight: 0 }}>
        <div style={{
          background: '#fff', border: '1px solid #e2e8f0', borderRadius: 16,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ padding: 14, borderBottom: '1px solid #e2e8f0', display: 'flex', gap: 10, alignItems: 'center' }}>
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
                background: i === 0 ? '#ccfbf1' : (i % 2 ? '#f8fafc' : 'transparent'),
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
        {/* Detail */}
        <div style={{
          background: '#fff', border: '1px solid #e2e8f0', borderRadius: 16,
          padding: 18, display: 'flex', flexDirection: 'column', gap: 14,
          boxShadow: '0 1px 3px rgba(15,23,42,.04)',
        }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div style={{ fontSize: 13, color: M_MUTED, fontWeight: 700, letterSpacing: .5 }}>פרטי מכולה</div>
            <button style={{
              width: 36, height: 36, border: '1px solid #e2e8f0',
              background: '#f8fafc', borderRadius: 10, fontSize: 16, cursor: 'pointer',
            }}>✕</button>
          </div>
          <div style={{
            background: 'linear-gradient(135deg, #ccfbf1 0%, #a7f3d0 100%)',
            borderRadius: 12, padding: 16, textAlign: 'center',
          }}>
            <div style={{ fontSize: 11, color: M_TEAL_DEEP, fontWeight: 700, letterSpacing: 1 }}>CONTAINER ID</div>
            <div style={{
              fontFamily: '"IBM Plex Mono", monospace',
              fontSize: 24, fontWeight: 800, color: M_TEAL_DEEP, marginTop: 4,
            }}>{c.id}</div>
          </div>
          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 8 }}>
            <DetailTile label="גודל" value={c.size + "'"} />
            <DetailTile label="סוג" value={c.type} />
            <DetailTile label="טיפול" value={c.handling} color={M_AMBER} />
            <DetailTile label="איתור" value={c.location} mono />
            <DetailTile label="משקל" value={c.weight.toLocaleString()} unit='ק"ג' mono />
            <DetailTile label="קפסיטי" value={c.capacity} />
          </div>
          <div style={{ background: '#f8fafc', border: '1px solid #e2e8f0', borderRadius: 10, padding: 12 }}>
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
    <div style={{
      background: '#f8fafc', border: '1px solid #e2e8f0',
      borderRadius: 10, padding: 10,
    }}>
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
      display: 'flex', justifyContent: 'space-between',
      padding: '6px 2px',
      borderBottom: last ? 'none' : '1px solid #eef2f7',
      fontSize: 13,
    }}>
      <span style={{ color: M_MUTED }}>{k}</span>
      <span style={{ fontWeight: 700 }}>{v}</span>
    </div>
  );
}

Object.assign(window, { ModernMain, ModernNumpad, ModernContainerDetail });
