// Modern · Main yard grid screen + Job panel + Action bar

function ModernGrid({ job }) {
  return (
    <div style={{
      background: M_CARD,
      border: '1px solid ' + M_LINE,
      borderRadius: 14,
      boxShadow: '0 1px 3px rgba(15,23,42,.04)',
      padding: 12,
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
    }}>
      <div style={{ display: 'flex', alignItems: 'center', gap: 10, marginBottom: 10 }}>
        <div style={{
          fontSize: 12, color: M_MUTED, fontWeight: 700, letterSpacing: 1.5,
        }}>מפת ערוגה</div>
        <div style={{
          fontSize: 15, color: M_TEAL_DEEP, fontWeight: 800,
          background: M_TEAL_SOFT, border: '1px solid #99f6e4',
          padding: '2px 10px', borderRadius: 999,
          fontFamily: '"IBM Plex Mono", monospace',
        }}>BOND2</div>
        <div style={{ flex: 1 }} />
        <div style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
          <span style={{ fontSize: 11, color: M_MUTED, fontWeight: 600 }}>גובה ערימה:</span>
          {[
            ['1-2', '#eff6ff', '#1d4ed8'],
            ['3',   '#f0fdf4', '#15803d'],
            ['4',   '#fefce8', '#a16207'],
            ['5',   '#fff7ed', '#c2410c'],
            ['6',   '#fef2f2', '#b91c1c'],
          ].map(([k, bg, fg]) => (
            <span key={k} style={{
              background: bg, color: fg, fontFamily: '"IBM Plex Mono", monospace',
              padding: '2px 10px', borderRadius: 4, fontSize: 12, fontWeight: 800,
            }}>{k}</span>
          ))}
          <div style={{ width: 1, height: 20, background: M_LINE }} />
          <LegendSwatch color={M_AMBER} label="מקור" />
          <LegendSwatch color={M_TEAL} dashed label="יעד" />
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '44px repeat(14, 1fr) 44px', gap: 4, flex: 1 }}>
        <div style={mGridCorner}>BOND2</div>
        {COLS.map(c => (<div key={c} style={mGridColHdr}>{c}</div>))}
        <div style={mGridCorner}></div>

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
                  minHeight: 60,
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
    </div>
  );
}

function SourceCell({ h }) {
  return (
    <>
      <div style={{ fontSize: 24, fontWeight: 800, color: '#78350f', fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1 }}>{h}</div>
      <div style={{
        position: 'absolute', top: -12, left: '50%', transform: 'translateX(-50%)',
        background: M_AMBER, color: '#fff', fontSize: 10, fontWeight: 800,
        padding: '2px 8px', borderRadius: 4, letterSpacing: .5,
        whiteSpace: 'nowrap',
      }}>מקור</div>
    </>
  );
}

function TargetCell({ h }) {
  return (
    <>
      <div style={{ fontSize: 24, fontWeight: 800, color: M_TEAL_DEEP, fontFamily: '"IBM Plex Mono", monospace', lineHeight: 1 }}>
        {h}<span style={{ fontSize: 14, opacity: .5 }}>→{h+1}</span>
      </div>
      <div style={{
        position: 'absolute', top: -12, left: '50%', transform: 'translateX(-50%)',
        background: M_TEAL, color: '#fff', fontSize: 10, fontWeight: 800,
        padding: '2px 8px', borderRadius: 4, letterSpacing: .5,
        whiteSpace: 'nowrap',
      }}>יעד</div>
    </>
  );
}

function LegendSwatch({ color, dashed, label }) {
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
  background: M_TEAL, color: '#fff', fontSize: 12, fontWeight: 700,
  display: 'grid', placeItems: 'center', borderRadius: 6,
  fontFamily: '"IBM Plex Mono", monospace',
};
const mGridColHdr = {
  fontSize: 13, fontWeight: 700, color: M_MUTED,
  display: 'grid', placeItems: 'center',
  fontFamily: '"IBM Plex Mono", monospace',
  paddingBottom: 4, borderBottom: '1px solid ' + M_LINE,
};
const mGridRowHdr = {
  fontSize: 17, fontWeight: 800, color: M_INK,
  background: '#f8fafc', display: 'grid', placeItems: 'center', borderRadius: 6,
};

function ModernJobPanel({ job }) {
  const c = job.container;
  return (
    <div style={{
      background: M_CARD, border: '1px solid ' + M_LINE, borderRadius: 14,
      boxShadow: '0 1px 3px rgba(15,23,42,.04)',
      padding: 14,
      display: 'grid',
      gridTemplateColumns: '1fr auto 1fr',
      alignItems: 'stretch',
      gap: 14,
      height: '100%',
    }}>
      <div style={{
        background: '#fffbeb', border: '1.5px solid #fde68a', borderRadius: 12,
        padding: 14, display: 'flex', flexDirection: 'column', gap: 8,
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
          <span style={{ fontSize: 22, fontWeight: 700, color: M_INK, fontFamily: '"IBM Plex Mono", monospace' }}>{c.id}</span>
          <ModernChip>{c.size}'</ModernChip>
          <ModernChip>{c.type}</ModernChip>
          <ModernChip kind="handle">{c.handling}</ModernChip>
          <ModernChip kind="weight">{c.weight.toLocaleString()} ק"ג</ModernChip>
          <ModernChip>לקוח {c.customer}</ModernChip>
        </div>
      </div>

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

      <div style={{
        background: '#f0fdfa', border: '1.5px solid #99f6e4', borderRadius: 12,
        padding: 14, display: 'flex', flexDirection: 'column', gap: 8,
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
      padding: '3px 9px', borderRadius: 999, fontSize: 12, fontWeight: 700,
      fontFamily: '"IBM Plex Mono", monospace',
    }}>{children}</span>
  );
}

function ModernActionBar() {
  return (
    <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr 1fr 1fr 1fr 1fr', gap: 10 }}>
      <ModernBtn primary icon="✓" label="אישור העברה" color={M_GREEN} />
      <ModernBtn icon="✕" label="ביטול" color={M_RED} />
      <ModernBtn icon="🚚" label="משאית" color="#0369a1" />
      <ModernBtn icon="📋" label="עבודות" color="#0369a1" />
      <ModernBtn icon="ℹ" label="מידע" color="#6366f1" />
      <ModernBtn icon="☰" label="תפריט" color="#475569" />
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
      display: 'flex', alignItems: 'center', justifyContent: 'center',
      gap: 10, fontSize: primary ? 19 : 16, fontWeight: 700,
      fontFamily: 'inherit', cursor: 'pointer',
    }}>
      <span style={{ fontSize: primary ? 24 : 20 }}>{icon}</span>
      {label}
    </button>
  );
}

function ModernMain() {
  return (
    <div style={modernRoot}>
      <ModernTopBar crane={CRANE} />
      <div style={{ padding: 14, display: 'grid', gridTemplateRows: '1fr 124px 86px', gap: 12, minHeight: 0 }}>
        <ModernGrid job={ACTIVE_JOB} />
        <ModernJobPanel job={ACTIVE_JOB} />
        <ModernActionBar />
      </div>
    </div>
  );
}

Object.assign(window, { ModernGrid, ModernJobPanel, ModernActionBar, ModernMain, ModernChip, ModernBtn });
