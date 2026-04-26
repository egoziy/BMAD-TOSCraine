// VARIATION 3 — BOLD (Cockpit HUD)
// Dark charcoal base, cyan primary, amber danger. Aviation-style HUD.
// Glowing status strip, big typographic anchors, grid uses luminance to show
// stack fullness (darker = fuller). Source and target cells pulse softly.

const B_BG = '#0b1220';
const B_SURFACE = '#121a2c';
const B_SURFACE_2 = '#1a2440';
const B_BORDER = '#22304d';
const B_INK = '#e7efff';
const B_MUTED = '#7a8bb0';
const B_CYAN = '#22d3ee';
const B_CYAN_DEEP = '#0891b2';
const B_AMBER = '#f59e0b';
const B_GREEN = '#22c55e';
const B_RED = '#ef4444';
const B_MAGENTA = '#c084fc';

const boldStyles = {
  root: {
    width: '100%', height: '100%',
    background: `radial-gradient(1200px 700px at 70% -10%, #1a2440 0%, ${B_BG} 60%)`,
    fontFamily: '"Heebo", system-ui, sans-serif',
    color: B_INK,
    display: 'grid',
    gridTemplateRows: '76px 1fr',
    direction: 'rtl',
  },
};

function BoldTopBar({ crane, job }) {
  return (
    <div style={{
      background: 'linear-gradient(180deg, #0e1629 0%, #0b1220 100%)',
      borderBottom: '1px solid ' + B_BORDER,
      display: 'grid',
      gridTemplateColumns: 'auto 1fr auto',
      alignItems: 'center',
      padding: '0 20px',
      gap: 20,
      position: 'relative',
    }}>
      {/* Glow strip */}
      <div style={{
        position: 'absolute', bottom: 0, left: 0, right: 0, height: 2,
        background: `linear-gradient(90deg, transparent, ${B_CYAN}, ${B_MAGENTA}, transparent)`,
        opacity: .6,
      }} />

      <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
        <div style={{
          width: 42, height: 42,
          background: 'conic-gradient(from 180deg, ' + B_CYAN + ', ' + B_MAGENTA + ', ' + B_CYAN + ')',
          borderRadius: 10, padding: 2,
        }}>
          <div style={{
            width: '100%', height: '100%', background: B_BG, borderRadius: 8,
            display: 'grid', placeItems: 'center', color: B_CYAN,
            fontSize: 18, fontWeight: 800, fontFamily: '"IBM Plex Mono", monospace',
          }}>RTG</div>
        </div>
        <div style={{ lineHeight: 1.1 }}>
          <div style={{ fontSize: 13, fontWeight: 700, color: B_MUTED, letterSpacing: 2 }}>CRANE · {crane.id}</div>
          <div style={{ fontSize: 17, fontWeight: 800, color: B_INK, fontFamily: '"IBM Plex Mono", monospace' }}>
            {crane.block} / {crane.position}
          </div>
        </div>
      </div>

      <div style={{ display: 'flex', justifyContent: 'center', gap: 10 }}>
        <HUDTile label="נושא כרגע" value={crane.carrying ? crane.carrying.id : '— ריק —'} muted={!crane.carrying} wide />
        <HUDTile label="סטטוס" value="פעיל" color={B_GREEN} />
        <HUDTile label="עבודה" value="העברה" color={B_CYAN} />
      </div>

      <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', lineHeight: 1 }}>
          <div style={{
            fontSize: 26, fontWeight: 700, fontFamily: '"IBM Plex Mono", monospace',
            color: B_CYAN, textShadow: '0 0 12px ' + B_CYAN + '88',
          }}>17:42:34</div>
          <div style={{ fontSize: 11, color: B_MUTED, marginTop: 2 }}>9 APR 2026</div>
        </div>
        <div style={{ width: 1, height: 38, background: B_BORDER }} />
        <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
          <SignalDot label="PLC" ok />
          <SignalDot label="GPS" ok />
        </div>
      </div>
    </div>
  );
}

function HUDTile({ label, value, color, muted, wide }) {
  return (
    <div style={{
      background: B_SURFACE,
      border: '1px solid ' + B_BORDER,
      borderRadius: 10,
      padding: '6px 14px',
      minWidth: wide ? 240 : 120,
      display: 'flex', flexDirection: 'column', alignItems: 'flex-start',
      position: 'relative', overflow: 'hidden',
    }}>
      {color && <div style={{
        position: 'absolute', top: 0, right: 0, width: 3, height: '100%',
        background: color, boxShadow: '0 0 8px ' + color,
      }} />}
      <div style={{ fontSize: 10, color: B_MUTED, fontWeight: 700, letterSpacing: 1.5 }}>{label.toUpperCase()}</div>
      <div style={{
        fontSize: 16, fontWeight: 800,
        color: muted ? B_MUTED : (color || B_INK),
        fontFamily: '"IBM Plex Mono", monospace',
        marginTop: 2,
      }}>{value}</div>
    </div>
  );
}

function SignalDot({ label, ok }) {
  return (
    <div style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
      <span style={{
        width: 8, height: 8, borderRadius: 8,
        background: ok ? B_GREEN : B_RED,
        boxShadow: '0 0 10px ' + (ok ? B_GREEN : B_RED),
      }} />
      <span style={{ fontSize: 11, fontWeight: 700, color: B_MUTED, letterSpacing: 1 }}>{label}</span>
    </div>
  );
}

// Cell coloring by stack fullness
function boldStackStyle(h, isSource, isTarget) {
  if (isSource) return {
    bg: `linear-gradient(135deg, ${B_AMBER}26, ${B_AMBER}10)`,
    border: '2px solid ' + B_AMBER,
    fg: B_AMBER,
    glow: '0 0 0 2px ' + B_AMBER + '40, 0 0 20px ' + B_AMBER + '55',
  };
  if (isTarget) return {
    bg: `linear-gradient(135deg, ${B_CYAN}26, ${B_CYAN}10)`,
    border: '2px dashed ' + B_CYAN,
    fg: B_CYAN,
    glow: '0 0 0 2px ' + B_CYAN + '40, 0 0 20px ' + B_CYAN + '55',
  };
  if (h === null || h === undefined) return { bg: 'transparent', border: '1px dashed ' + B_BORDER, fg: B_MUTED + '40' };
  // Darker=fuller: 6 → near-black saturated; 1 → lightest
  const stops = {
    1: { bg: '#0f3a2b', fg: '#4ade80' },  // green
    2: { bg: '#0e3a3a', fg: '#5eead4' },  // teal
    3: { bg: '#0f2a4a', fg: '#7dd3fc' },  // blue
    4: { bg: '#3a2a0f', fg: '#fbbf24' },  // amber
    5: { bg: '#3a1f0f', fg: '#fb923c' },  // orange
    6: { bg: '#3a0f1a', fg: '#f87171' },  // red
  };
  const s = stops[h] || { bg: B_SURFACE, fg: B_INK };
  return { bg: s.bg, border: '1px solid ' + B_BORDER, fg: s.fg };
}

function BoldGrid({ job }) {
  return (
    <div style={{
      background: B_SURFACE,
      border: '1px solid ' + B_BORDER,
      borderRadius: 14,
      padding: 12,
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
      position: 'relative',
      overflow: 'hidden',
    }}>
      {/* Section label */}
      <div style={{
        display: 'flex', alignItems: 'center', gap: 10, marginBottom: 10,
        paddingBottom: 10, borderBottom: '1px solid ' + B_BORDER,
      }}>
        <div style={{ fontSize: 11, color: B_MUTED, fontWeight: 700, letterSpacing: 2 }}>YARD MAP · ערוגה</div>
        <div style={{ fontSize: 16, color: B_CYAN, fontWeight: 800, fontFamily: '"IBM Plex Mono", monospace' }}>
          BOND2
        </div>
        <div style={{ flex: 1 }} />
        <div style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
          <span style={{ fontSize: 11, color: B_MUTED }}>גובה ערימה:</span>
          {[1,2,3,4,5,6].map(n => {
            const s = boldStackStyle(n, false, false);
            return (
              <span key={n} style={{
                background: s.bg, color: s.fg,
                padding: '2px 8px', borderRadius: 4,
                fontSize: 12, fontWeight: 700,
                fontFamily: '"IBM Plex Mono", monospace',
                border: '1px solid ' + B_BORDER,
              }}>{n}</span>
            );
          })}
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '40px repeat(14, 1fr) 40px', gap: 3, flex: 1 }}>
        {/* Column headers */}
        <div />
        {COLS.map(c => (
          <div key={c} style={{
            fontSize: 12, fontWeight: 700, color: B_MUTED,
            textAlign: 'center', paddingBottom: 4,
            fontFamily: '"IBM Plex Mono", monospace',
          }}>{c}</div>
        ))}
        <div />
        {/* Body */}
        {ROWS.map((row, rIdx) => (
          <React.Fragment key={row}>
            <div style={{
              display: 'grid', placeItems: 'center',
              fontSize: 15, fontWeight: 800, color: B_CYAN,
              fontFamily: '"IBM Plex Mono", monospace',
              background: B_SURFACE_2, borderRadius: 6,
            }}>{row}</div>
            {COLS.map((col, cIdx) => {
              const h = GRID[rIdx][cIdx + 1];
              const isSource = job.source.row === row && job.source.col === col;
              const isTarget = job.target.row === row && job.target.col === col;
              const s = boldStackStyle(h, isSource, isTarget);
              return (
                <div key={col} style={{
                  background: s.bg,
                  border: s.border,
                  boxShadow: s.glow,
                  borderRadius: 6,
                  minHeight: 52,
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  position: 'relative',
                  fontSize: 22,
                  fontWeight: 800,
                  color: s.fg,
                  fontFamily: '"IBM Plex Mono", monospace',
                }}>
                  {h !== null && !isSource && !isTarget && h}
                  {isSource && <BoldSrcCell h={h} />}
                  {isTarget && <BoldTgtCell h={h} />}
                </div>
              );
            })}
            <div style={{
              display: 'grid', placeItems: 'center',
              fontSize: 15, fontWeight: 800, color: B_CYAN,
              fontFamily: '"IBM Plex Mono", monospace',
              background: B_SURFACE_2, borderRadius: 6,
            }}>{row}</div>
          </React.Fragment>
        ))}
      </div>
    </div>
  );
}

function BoldSrcCell({ h }) {
  return (
    <>
      <div style={{ fontSize: 22, fontWeight: 800, color: B_AMBER, lineHeight: 1 }}>{h}</div>
      <div style={{
        position: 'absolute', top: -1, right: -1,
        background: B_AMBER, color: '#1a1300',
        fontSize: 9, fontWeight: 800,
        padding: '1px 5px', borderTopLeftRadius: 5, borderBottomRightRadius: 5,
        fontFamily: '"Heebo", sans-serif',
      }}>FROM</div>
    </>
  );
}
function BoldTgtCell({ h }) {
  return (
    <>
      <div style={{ fontSize: 20, fontWeight: 800, color: B_CYAN, lineHeight: 1 }}>
        {h}<span style={{ opacity: .5, fontSize: 14 }}>→{h+1}</span>
      </div>
      <div style={{
        position: 'absolute', top: -1, right: -1,
        background: B_CYAN, color: '#002029',
        fontSize: 9, fontWeight: 800,
        padding: '1px 5px', borderTopLeftRadius: 5, borderBottomRightRadius: 5,
        fontFamily: '"Heebo", sans-serif',
      }}>TO</div>
    </>
  );
}

function BoldJobPanel({ job }) {
  const c = job.container;
  return (
    <div style={{
      background: B_SURFACE,
      border: '1px solid ' + B_BORDER,
      borderRadius: 14,
      padding: 14,
      display: 'grid',
      gridTemplateColumns: '1fr auto 1fr',
      gap: 14,
      alignItems: 'stretch',
      height: '100%',
      position: 'relative', overflow: 'hidden',
    }}>
      {/* Source block */}
      <div style={{
        background: `linear-gradient(135deg, ${B_AMBER}14, transparent)`,
        border: '1px solid ' + B_AMBER + '55',
        borderRadius: 10, padding: 12,
        display: 'flex', flexDirection: 'column', gap: 8,
        position: 'relative',
      }}>
        <div style={{
          position: 'absolute', top: 0, right: 0, width: 3, height: '100%',
          background: B_AMBER, boxShadow: '0 0 10px ' + B_AMBER,
        }} />
        <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
          <span style={{ fontSize: 10, fontWeight: 800, color: B_AMBER, letterSpacing: 2 }}>FROM · מקור</span>
          <div style={{ flex: 1 }} />
          <span style={{
            fontSize: 28, fontWeight: 800, color: B_AMBER,
            fontFamily: '"IBM Plex Mono", monospace',
            textShadow: '0 0 12px ' + B_AMBER + '66',
          }}>{job.source.label}</span>
        </div>
        <div style={{ display: 'flex', flexWrap: 'wrap', gap: 6, alignItems: 'center' }}>
          <span style={{
            fontSize: 22, fontWeight: 800, color: B_INK,
            fontFamily: '"IBM Plex Mono", monospace',
          }}>{c.id}</span>
          <BoldChip>{c.size}'</BoldChip>
          <BoldChip>{c.type}</BoldChip>
          <BoldChip tone="magenta">{c.handling}</BoldChip>
          <BoldChip tone="red">{c.weight.toLocaleString()} ק"ג</BoldChip>
        </div>
      </div>
      {/* Arrow */}
      <div style={{
        display: 'flex', flexDirection: 'column', alignItems: 'center',
        justifyContent: 'center', gap: 6, minWidth: 80,
      }}>
        <div style={{
          width: 56, height: 56, borderRadius: 56,
          background: `conic-gradient(from 90deg, ${B_AMBER}, ${B_CYAN})`,
          padding: 2,
        }}>
          <div style={{
            width: '100%', height: '100%', borderRadius: 56,
            background: B_BG, display: 'grid', placeItems: 'center',
            color: B_CYAN, fontSize: 26, fontWeight: 800,
          }}>←</div>
        </div>
        <div style={{ fontSize: 10, color: B_MUTED, letterSpacing: 2, fontWeight: 700 }}>MOVE</div>
      </div>
      {/* Target block */}
      <div style={{
        background: `linear-gradient(135deg, ${B_CYAN}14, transparent)`,
        border: '1px solid ' + B_CYAN + '55',
        borderRadius: 10, padding: 12,
        display: 'flex', flexDirection: 'column', gap: 8,
        position: 'relative',
      }}>
        <div style={{
          position: 'absolute', top: 0, right: 0, width: 3, height: '100%',
          background: B_CYAN, boxShadow: '0 0 10px ' + B_CYAN,
        }} />
        <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
          <span style={{ fontSize: 10, fontWeight: 800, color: B_CYAN, letterSpacing: 2 }}>TO · יעד</span>
          <div style={{ flex: 1 }} />
          <span style={{
            fontSize: 28, fontWeight: 800, color: B_CYAN,
            fontFamily: '"IBM Plex Mono", monospace',
            textShadow: '0 0 12px ' + B_CYAN + '66',
          }}>{job.target.label}</span>
        </div>
        <div style={{ display: 'flex', flexWrap: 'wrap', gap: 10, alignItems: 'center' }}>
          <div>
            <div style={{ fontSize: 10, color: B_MUTED, fontWeight: 700, letterSpacing: 1 }}>STACK</div>
            <div style={{ fontSize: 20, fontWeight: 800, color: B_INK, fontFamily: '"IBM Plex Mono", monospace' }}>
              {job.target.height}/6
            </div>
          </div>
          <div style={{ width: 1, height: 30, background: B_BORDER }} />
          <div>
            <div style={{ fontSize: 10, color: B_MUTED, fontWeight: 700, letterSpacing: 1 }}>ROW · COL</div>
            <div style={{ fontSize: 16, fontWeight: 800, color: B_INK, fontFamily: '"IBM Plex Mono", monospace' }}>
              {job.target.row} · {job.target.col}
            </div>
          </div>
          <div style={{ flex: 1 }} />
          <div style={{
            fontSize: 11, color: B_GREEN, fontWeight: 800, letterSpacing: 1,
            background: B_GREEN + '1a', border: '1px solid ' + B_GREEN + '55',
            padding: '4px 10px', borderRadius: 999,
          }}>● CLEAR TO DROP</div>
        </div>
      </div>
    </div>
  );
}

function BoldChip({ children, tone }) {
  const palettes = {
    magenta: { bg: B_MAGENTA + '1a', fg: B_MAGENTA, br: B_MAGENTA + '55' },
    red:     { bg: B_RED + '1a',     fg: '#fca5a5', br: B_RED + '55' },
  };
  const p = palettes[tone] || { bg: B_SURFACE_2, fg: B_MUTED, br: B_BORDER };
  return (
    <span style={{
      background: p.bg, color: p.fg, border: '1px solid ' + p.br,
      padding: '3px 9px', borderRadius: 6,
      fontSize: 12, fontWeight: 700,
      fontFamily: '"IBM Plex Mono", monospace',
    }}>{children}</span>
  );
}

function BoldActionBar() {
  return (
    <div style={{
      display: 'grid',
      gridTemplateColumns: '2.2fr 1fr 1fr 1fr 1fr 1fr',
      gap: 10,
    }}>
      <BoldBtn primary icon="✓" label="אישור העברה" color={B_GREEN} />
      <BoldBtn icon="✕" label="ביטול" color={B_RED} />
      <BoldBtn icon="◎" label="משאית" color={B_CYAN} />
      <BoldBtn icon="☰" label="עבודות" color={B_CYAN} />
      <BoldBtn icon="i" label="מידע" color={B_MAGENTA} />
      <BoldBtn icon="⏻" label="יציאה" color={B_MUTED} />
    </div>
  );
}

function BoldBtn({ icon, label, color, primary }) {
  return (
    <button style={{
      height: 68,
      background: primary
        ? `linear-gradient(180deg, ${color}, ${color}cc)`
        : B_SURFACE,
      color: primary ? '#002b11' : color,
      border: '1px solid ' + (primary ? color : color + '55'),
      borderRadius: 12,
      boxShadow: primary
        ? '0 0 0 1px ' + color + '55, 0 10px 24px ' + color + '55'
        : 'inset 0 0 0 1px ' + color + '22',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      gap: 10,
      fontSize: primary ? 18 : 15,
      fontWeight: 800,
      fontFamily: 'inherit',
      cursor: 'pointer',
      letterSpacing: 1,
    }}>
      <span style={{ fontSize: primary ? 22 : 18, fontFamily: '"IBM Plex Mono", monospace' }}>{icon}</span>
      {label}
    </button>
  );
}

function BoldMain() {
  return (
    <div style={boldStyles.root}>
      <BoldTopBar crane={CRANE} job={ACTIVE_JOB} />
      <div style={{ padding: 14, display: 'grid', gridTemplateRows: '1fr 128px 80px', gap: 12, minHeight: 0 }}>
        <BoldGrid job={ACTIVE_JOB} />
        <BoldJobPanel job={ACTIVE_JOB} />
        <BoldActionBar />
      </div>
    </div>
  );
}

// Secondary screens

function BoldNumpad() {
  const [val, setVal] = React.useState('2.3');
  const press = (k) => {
    if (k === 'C') setVal('');
    else if (k === '←') setVal(v => v.slice(0, -1));
    else setVal(v => (v + k).slice(0, 6));
  };
  return (
    <div style={{ ...boldStyles.root, gridTemplateRows: '76px 1fr' }}>
      <BoldTopBar crane={CRANE} job={ACTIVE_JOB} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '400px 1fr', gap: 14, minHeight: 0 }}>
        <div style={{
          background: B_SURFACE, border: '1px solid ' + B_BORDER, borderRadius: 14,
          padding: 14, display: 'flex', flexDirection: 'column', gap: 12,
        }}>
          <div style={{ fontSize: 11, color: B_MUTED, fontWeight: 700, letterSpacing: 2 }}>BLOCK CODE · בחר חומ"ס</div>
          <div style={{
            background: B_BG, border: '1px solid ' + B_CYAN,
            boxShadow: 'inset 0 0 24px ' + B_CYAN + '22, 0 0 20px ' + B_CYAN + '22',
            borderRadius: 10, padding: '18px', textAlign: 'center',
            fontSize: 48, fontWeight: 800, fontFamily: '"IBM Plex Mono", monospace',
            color: B_CYAN, textShadow: '0 0 18px ' + B_CYAN + 'aa',
            letterSpacing: 4,
          }}>
            {val || <span style={{ color: B_MUTED + '50' }}>—</span>}
          </div>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8 }}>
            {['1','2','3','4','5','6','7','8','9','←','0','C'].map(k => {
              const isDel = k === 'C';
              const isBack = k === '←';
              const color = isDel ? B_RED : isBack ? B_AMBER : B_INK;
              return (
                <button key={k} onClick={() => press(k)} style={{
                  height: 74, background: B_SURFACE_2,
                  color,
                  border: '1px solid ' + (isDel ? B_RED + '55' : isBack ? B_AMBER + '55' : B_BORDER),
                  borderRadius: 10, fontSize: 30, fontWeight: 800,
                  fontFamily: '"IBM Plex Mono", monospace',
                  cursor: 'pointer',
                }}>{k}</button>
              );
            })}
          </div>
          <button style={{
            height: 72, background: 'linear-gradient(180deg, ' + B_GREEN + ', #15803d)',
            color: '#002b11', border: '1px solid ' + B_GREEN,
            boxShadow: '0 10px 24px ' + B_GREEN + '55',
            borderRadius: 12, fontSize: 22, fontWeight: 800,
            fontFamily: 'inherit', cursor: 'pointer', letterSpacing: 1,
          }}>✓ אישור</button>
        </div>
        <div style={{
          background: B_SURFACE, border: '1px solid ' + B_BORDER, borderRadius: 14,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
        }}>
          <div style={{
            padding: 14, borderBottom: '1px solid ' + B_BORDER,
            display: 'flex', gap: 10, alignItems: 'center',
          }}>
            <div style={{ fontSize: 11, color: B_MUTED, fontWeight: 700, letterSpacing: 2 }}>CONTAINERS · בחר איתור</div>
            <div style={{ flex: 1 }} />
            <input placeholder="חיפוש מכולה..." style={{
              padding: '10px 14px', border: '1px solid ' + B_BORDER, background: B_BG,
              color: B_INK, borderRadius: 8, fontSize: 14, fontFamily: 'inherit', width: 260,
            }} />
          </div>
          <div style={{ flex: 1, overflow: 'auto' }}>
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
              <thead>
                <tr>
                  <th style={bTableHdr}>MKL</th>
                  <th style={bTableHdr}>גודל</th>
                  <th style={bTableHdr}>כניסה</th>
                  <th style={bTableHdr}>איתור</th>
                </tr>
              </thead>
              <tbody>
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
                ].map((r, i) => (
                  <tr key={i} style={{ background: r.active ? B_CYAN + '14' : 'transparent' }}>
                    <td style={{ ...bTableCell, color: r.active ? B_CYAN : B_INK }}>{r.id}</td>
                    <td style={{ ...bTableCell, textAlign: 'center', color: B_MUTED }}>{r.sz}'</td>
                    <td style={{ ...bTableCell, color: B_MUTED }}>{r.when}</td>
                    <td style={{ ...bTableCell, fontWeight: 700, color: r.active ? B_CYAN : B_INK }}>{r.loc}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}

const bTableHdr = {
  fontSize: 11, fontWeight: 700, color: B_MUTED, letterSpacing: 2,
  padding: '10px 14px', textAlign: 'right',
  borderBottom: '1px solid ' + B_BORDER,
  fontFamily: '"IBM Plex Mono", monospace',
  position: 'sticky', top: 0, background: B_SURFACE,
};
const bTableCell = {
  fontSize: 14, padding: '12px 14px',
  borderBottom: '1px solid ' + B_BORDER + '66',
  fontFamily: '"IBM Plex Mono", monospace',
};

function BoldContainerDetail() {
  const c = ACTIVE_JOB.container;
  return (
    <div style={{ ...boldStyles.root, gridTemplateRows: '76px 1fr' }}>
      <BoldTopBar crane={CRANE} job={ACTIVE_JOB} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '1fr 380px', gap: 14, minHeight: 0 }}>
        <div style={{
          background: B_SURFACE, border: '1px solid ' + B_BORDER, borderRadius: 14,
          display: 'flex', flexDirection: 'column', overflow: 'hidden',
        }}>
          <div style={{
            padding: 14, borderBottom: '1px solid ' + B_BORDER,
            display: 'flex', gap: 10, alignItems: 'center',
          }}>
            <div style={{ fontSize: 11, color: B_MUTED, fontWeight: 700, letterSpacing: 2 }}>DEAL CONTAINERS · מכולות בעיסקה</div>
            <span style={{
              background: B_CYAN + '1a', color: B_CYAN, border: '1px solid ' + B_CYAN + '55',
              padding: '2px 8px', borderRadius: 999, fontSize: 12, fontWeight: 800,
            }}>{DEAL_CONTAINERS.length}</span>
          </div>
          <div style={{ flex: 1, overflow: 'auto' }}>
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
              <thead>
                <tr>
                  <th style={bTableHdr}>מכולה</th>
                  <th style={bTableHdr}>איתור</th>
                  <th style={bTableHdr}>גודל</th>
                  <th style={bTableHdr}>סוג</th>
                  <th style={bTableHdr}>משקל</th>
                </tr>
              </thead>
              <tbody>
                {DEAL_CONTAINERS.map((cc, i) => (
                  <tr key={i} style={{ background: i === 0 ? B_CYAN + '14' : 'transparent' }}>
                    <td style={{ ...bTableCell, fontWeight: 700, color: i === 0 ? B_CYAN : B_INK }}>{cc.id}</td>
                    <td style={{ ...bTableCell, color: B_AMBER, fontWeight: 700 }}>{cc.loc}</td>
                    <td style={{ ...bTableCell, textAlign: 'center', color: B_MUTED }}>{cc.size}'</td>
                    <td style={{ ...bTableCell, textAlign: 'center', color: B_MUTED }}>{cc.type}</td>
                    <td style={{ ...bTableCell, textAlign: 'left', color: B_INK }}>{cc.weight.toLocaleString()} <span style={{ color: B_MUTED, fontSize: 11 }}>ק"ג</span></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
        <div style={{
          background: B_SURFACE, border: '1px solid ' + B_BORDER, borderRadius: 14,
          padding: 18, display: 'flex', flexDirection: 'column', gap: 14,
        }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div style={{ fontSize: 11, color: B_MUTED, fontWeight: 700, letterSpacing: 2 }}>CONTAINER DETAIL</div>
            <button style={{
              width: 36, height: 36, border: '1px solid ' + B_BORDER,
              background: B_SURFACE_2, color: B_INK,
              borderRadius: 8, fontSize: 16, cursor: 'pointer',
            }}>✕</button>
          </div>
          <div style={{
            background: B_BG, border: '1px solid ' + B_CYAN + '66',
            boxShadow: 'inset 0 0 24px ' + B_CYAN + '22',
            borderRadius: 12, padding: 16, textAlign: 'center',
          }}>
            <div style={{ fontSize: 10, color: B_MUTED, letterSpacing: 2, fontWeight: 700 }}>CONTAINER ID</div>
            <div style={{
              fontSize: 26, fontWeight: 800, color: B_CYAN,
              fontFamily: '"IBM Plex Mono", monospace',
              textShadow: '0 0 16px ' + B_CYAN + '88',
              marginTop: 4,
            }}>{c.id}</div>
          </div>
          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 8 }}>
            <BoldDTile label="גודל" value={c.size + "'"} />
            <BoldDTile label="סוג" value={c.type} />
            <BoldDTile label="טיפול" value={c.handling} color={B_MAGENTA} />
            <BoldDTile label="איתור" value={c.location} color={B_AMBER} />
            <BoldDTile label="משקל" value={c.weight.toLocaleString()} unit='ק"ג' wide />
          </div>
          <div style={{
            background: B_SURFACE_2, border: '1px solid ' + B_BORDER,
            borderRadius: 10, padding: 12,
          }}>
            <BoldRow k="לקוח" v={c.customer} />
            <BoldRow k="קפסיטי" v={c.capacity} />
            <BoldRow k="עבודה" v="שיקוף מכולה" />
            <BoldRow k="התרה" v="—" last />
          </div>
          <div style={{ flex: 1 }} />
          <button style={{
            height: 64, background: 'linear-gradient(180deg, ' + B_GREEN + ', #15803d)',
            color: '#002b11', border: 'none', borderRadius: 12,
            fontSize: 19, fontWeight: 800, fontFamily: 'inherit',
            cursor: 'pointer', boxShadow: '0 10px 24px ' + B_GREEN + '55',
            letterSpacing: 1,
          }}>✓ בחר מכולה זו</button>
        </div>
      </div>
    </div>
  );
}

function BoldDTile({ label, value, unit, color, wide }) {
  return (
    <div style={{
      background: B_SURFACE_2, border: '1px solid ' + B_BORDER,
      borderRadius: 10, padding: 10,
      gridColumn: wide ? 'span 2' : 'auto',
    }}>
      <div style={{ fontSize: 10, color: B_MUTED, letterSpacing: 2, fontWeight: 700 }}>{label.toUpperCase()}</div>
      <div style={{
        fontSize: 20, fontWeight: 800, color: color || B_INK, marginTop: 2,
        fontFamily: '"IBM Plex Mono", monospace',
      }}>
        {value} {unit && <span style={{ fontSize: 11, color: B_MUTED, fontWeight: 600 }}>{unit}</span>}
      </div>
    </div>
  );
}

function BoldRow({ k, v, last }) {
  return (
    <div style={{
      display: 'flex', justifyContent: 'space-between',
      padding: '6px 2px',
      borderBottom: last ? 'none' : '1px solid ' + B_BORDER + '66',
      fontSize: 13,
    }}>
      <span style={{ color: B_MUTED }}>{k}</span>
      <span style={{ fontWeight: 700, color: B_INK }}>{v}</span>
    </div>
  );
}

Object.assign(window, { BoldMain, BoldNumpad, BoldContainerDetail });
