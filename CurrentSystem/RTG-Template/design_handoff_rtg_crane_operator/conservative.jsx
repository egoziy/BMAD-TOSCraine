// VARIATION 1 — CONSERVATIVE
// Familiar to crane operators coming from the old WinForms app.
// Light neutral base, blue primary, clear borders, high readability.
// No fancy animations — instant feedback, dense info, but cleaned up.

const conservativeStyles = {
  root: {
    width: '100%',
    height: '100%',
    background: '#eef1f5',
    fontFamily: '"Heebo", "Segoe UI", system-ui, sans-serif',
    color: '#1e293b',
    display: 'grid',
    gridTemplateRows: '56px 1fr 88px',
    direction: 'rtl',
  },
};

const C_BLUE = '#1e5fbd';
const C_BLUE_DEEP = '#154891';
const C_GREEN = '#2f8d3e';
const C_AMBER = '#c77a06';
const C_RED = '#c13535';

// Top bar
function ConservativeTopBar({ crane }) {
  return (
    <div style={{
      background: '#fff',
      borderBottom: '1px solid #cbd3de',
      display: 'flex',
      alignItems: 'center',
      padding: '0 20px',
      gap: 24,
      fontSize: 15,
    }}>
      <div style={{ fontWeight: 700, fontSize: 17, color: C_BLUE_DEEP }}>
        RTG · עמדת מנופאי
      </div>
      <div style={{ width: 1, height: 28, background: '#e2e8f0' }} />
      <TopStat label="מנוף" value={crane.id} />
      <TopStat label="גוש" value={crane.block} />
      <TopStat label="מיקום" value={crane.position} mono />
      <TopStat label="PLC" value={crane.plc.status} dot={crane.plc.ok ? "#2f8d3e" : "#dc2626"} />
      <TopStat label="GPS" value={crane.gps.status} dot={crane.gps.ok ? "#2f8d3e" : "#dc2626"} />
      <div style={{ flex: 1 }} />
      <div style={{ fontFamily: '"IBM Plex Mono", monospace', fontSize: 20, fontWeight: 600 }}>
        17:42:34
      </div>
    </div>
  );
}

function TopStat({ label, value, mono, dot }) {
  return (
    <div style={{ display: 'flex', alignItems: 'baseline', gap: 8 }}>
      {dot && <span style={{ width: 8, height: 8, borderRadius: 8, background: dot, display: 'inline-block' }} />}
      <span style={{ color: '#64748b', fontSize: 13 }}>{label}</span>
      <span style={{ fontWeight: 600, fontFamily: mono ? '"IBM Plex Mono", monospace' : 'inherit' }}>
        {value}
      </span>
    </div>
  );
}

// Stack height colours — conservative: blues + amber + red
function conservativeStackColor(h) {
  if (h === null || h === undefined) return { bg: '#fff', fg: '#cbd5e1' };
  if (h === 6) return { bg: '#fff', fg: '#fecdd3' };       // full, faded red-pink
  if (h === 5) return { bg: '#fff', fg: '#fcbf93' };
  if (h === 4) return { bg: '#fff', fg: '#e68a2a' };
  if (h === 3) return { bg: '#fff', fg: '#2563eb' };
  if (h === 2) return { bg: '#fff', fg: '#1e40af' };
  if (h === 1) return { bg: '#fff', fg: '#16a34a' };
  return { bg: '#fff', fg: '#64748b' };
}

function ConservativeGrid({ job, crane }) {
  return (
    <div style={{
      background: '#fff',
      border: '1px solid #cbd3de',
      borderRadius: 6,
      overflow: 'hidden',
      height: '100%',
    }}>
      <table style={{ width: '100%', height: '100%', borderCollapse: 'collapse', tableLayout: 'fixed' }}>
        <thead>
          <tr style={{ background: '#f1f5f9' }}>
            <th style={conservativeHdrCorner}>BOND2</th>
            {COLS.map(c => (
              <th key={c} style={conservativeHdr}>{c}</th>
            ))}
            <th style={{ ...conservativeHdr, width: 40 }}></th>
          </tr>
        </thead>
        <tbody>
          {ROWS.map((row, rIdx) => (
            <tr key={row}>
              <td style={conservativeRowLabel}>{row}</td>
              {COLS.map((col, cIdx) => {
                const h = GRID[rIdx][cIdx + 1]; // offset
                const isSource = job.source.row === row && job.source.col === col;
                const isTarget = job.target.row === row && job.target.col === col;
                const isCrane = crane.row === row && Math.abs(col - 138) < 3 && row === 'D'; // approximate crane position
                const { fg } = conservativeStackColor(h);
                let bg = '#fff';
                let border = '1px solid #e2e8f0';
                let ring = null;
                if (isSource) { bg = '#fef3c7'; border = '2px solid ' + C_AMBER; }
                else if (isTarget) { bg = '#dbeafe'; border = '2px solid ' + C_BLUE; }
                return (
                  <td key={col} style={{
                    border,
                    background: bg,
                    textAlign: 'center',
                    verticalAlign: 'middle',
                    position: 'relative',
                    padding: 0,
                  }}>
                    {h !== null && (
                      <span style={{
                        fontSize: 34,
                        fontWeight: 800,
                        color: isSource ? '#92400e' : isTarget ? C_BLUE_DEEP : fg,
                        fontFamily: '"IBM Plex Mono", monospace',
                      }}>{h}</span>
                    )}
                    {isSource && (
                      <div style={{
                        position: 'absolute',
                        top: 3, right: 3,
                        fontSize: 10,
                        fontWeight: 700,
                        background: C_AMBER,
                        color: '#fff',
                        padding: '1px 5px',
                        borderRadius: 3,
                      }}>מקור</div>
                    )}
                    {isTarget && (
                      <div style={{
                        position: 'absolute',
                        top: 3, right: 3,
                        fontSize: 10,
                        fontWeight: 700,
                        background: C_BLUE,
                        color: '#fff',
                        padding: '1px 5px',
                        borderRadius: 3,
                      }}>יעד</div>
                    )}
                  </td>
                );
              })}
              <td style={conservativeRowLabel}>{row}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

const conservativeHdr = {
  fontSize: 13,
  fontWeight: 600,
  color: '#475569',
  padding: '6px 0',
  borderBottom: '1px solid #cbd3de',
  borderLeft: '1px solid #e2e8f0',
  fontFamily: '"IBM Plex Mono", monospace',
};
const conservativeHdrCorner = { ...conservativeHdr, background: '#1e5fbd', color: '#fff', width: 56, fontSize: 14 };
const conservativeRowLabel = {
  fontSize: 18, fontWeight: 700, textAlign: 'center',
  background: '#f8fafc', color: '#0f172a',
  borderRight: '1px solid #cbd3de', borderLeft: '1px solid #cbd3de',
  width: 56,
};

// Source/target info panel
function ConservativeJobPanel({ job }) {
  return (
    <div style={{
      background: '#fff',
      border: '1px solid #cbd3de',
      borderRadius: 6,
      padding: 16,
      display: 'grid',
      gridTemplateColumns: '1fr auto 1fr',
      alignItems: 'center',
      gap: 20,
      height: '100%',
    }}>
      <JobSide kind="source" job={job} />
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: 6 }}>
        <div style={{ fontSize: 42, color: C_BLUE, lineHeight: 1 }}>⬅</div>
        <div style={{ fontSize: 11, color: '#64748b', letterSpacing: 1 }}>העברה</div>
      </div>
      <JobSide kind="target" job={job} />
    </div>
  );
}

function JobSide({ kind, job }) {
  const isSrc = kind === 'source';
  const data = isSrc ? job.source : job.target;
  const c = job.container;
  const tint = isSrc ? C_AMBER : C_BLUE;
  const bg = isSrc ? '#fffbeb' : '#eff6ff';
  return (
    <div style={{
      background: bg, border: '1px solid ' + tint + '40',
      borderRadius: 6, padding: '10px 14px',
      borderRight: '4px solid ' + tint,
    }}>
      <div style={{ fontSize: 12, color: '#64748b', fontWeight: 600, marginBottom: 6 }}>
        {isSrc ? 'מקור · מאיתור' : 'יעד · לאיתור'}
      </div>
      <div style={{ display: 'flex', alignItems: 'baseline', gap: 12, flexWrap: 'wrap' }}>
        <span style={{ fontSize: 26, fontWeight: 800, color: tint, fontFamily: '"IBM Plex Mono", monospace' }}>
          {data.label}
        </span>
        {isSrc && (
          <>
            <span style={{ fontSize: 17, color: '#0f172a', fontFamily: '"IBM Plex Mono", monospace' }}>
              {c.id}
            </span>
            <Chip>{c.size}'</Chip>
            <Chip>{c.type}</Chip>
            <Chip>{c.handling}</Chip>
            <Chip>{c.weight.toLocaleString()} ק"ג</Chip>
          </>
        )}
        {!isSrc && (
          <span style={{ fontSize: 14, color: '#334155' }}>
            גובה נוכחי: {data.height}/6 · קיבולת פנויה
          </span>
        )}
      </div>
    </div>
  );
}

function Chip({ children }) {
  return (
    <span style={{
      background: '#fff',
      border: '1px solid #cbd3de',
      borderRadius: 4,
      padding: '2px 8px',
      fontSize: 13,
      fontWeight: 600,
      color: '#334155',
      fontFamily: '"IBM Plex Mono", monospace',
    }}>{children}</span>
  );
}

// Bottom action bar
function ConservativeActionBar() {
  const btns = [
    { id: 'confirm', label: 'אישור', color: C_GREEN, icon: '✓', primary: true },
    { id: 'cancel',  label: 'ביטול', color: C_RED,   icon: '✕' },
    { id: 'truck',   label: 'משאית', color: '#0369a1', icon: '🚚' },
    { id: 'jobs',    label: 'עבודות', color: '#0369a1', icon: '📋' },
    { id: 'info',    label: 'מידע',  color: '#0369a1', icon: 'ℹ' },
    { id: 'exit',    label: 'יציאה', color: '#475569', icon: '⏻' },
  ];
  return (
    <div style={{
      background: '#fff',
      borderTop: '1px solid #cbd3de',
      padding: '10px 16px',
      display: 'flex',
      gap: 10,
      alignItems: 'center',
    }}>
      {btns.map(b => (
        <button key={b.id} style={{
          flex: b.primary ? 1.4 : 1,
          height: 68,
          background: b.primary ? b.color : '#fff',
          color: b.primary ? '#fff' : b.color,
          border: '1.5px solid ' + b.color,
          borderRadius: 6,
          fontSize: 18,
          fontWeight: 700,
          fontFamily: 'inherit',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          gap: 10,
          cursor: 'pointer',
        }}>
          <span style={{ fontSize: 22 }}>{b.icon}</span>
          {b.label}
        </button>
      ))}
    </div>
  );
}

function ConservativeMain() {
  return (
    <div style={conservativeStyles.root}>
      <ConservativeTopBar crane={CRANE} />
      <div style={{ padding: 12, display: 'grid', gridTemplateRows: '1fr 100px', gap: 10, minHeight: 0 }}>
        <ConservativeGrid job={ACTIVE_JOB} crane={CRANE} />
        <ConservativeJobPanel job={ACTIVE_JOB} />
      </div>
      <ConservativeActionBar />
    </div>
  );
}

// ——— Secondary screens ———

function ConservativeNumpad() {
  const [val, setVal] = React.useState('2.3');
  const press = (k) => {
    if (k === 'C') setVal('');
    else if (k === '←') setVal(v => v.slice(0, -1));
    else setVal(v => (v + k).slice(0, 6));
  };
  return (
    <div style={{ ...conservativeStyles.root, gridTemplateRows: '56px 1fr' }}>
      <ConservativeTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '340px 1fr', gap: 14, minHeight: 0 }}>
        {/* Left: numpad */}
        <div style={{ background: '#fff', border: '1px solid #cbd3de', borderRadius: 6, padding: 14, display: 'flex', flexDirection: 'column', gap: 10 }}>
          <div style={{ fontSize: 14, color: '#64748b', fontWeight: 600 }}>בחר חומ"ס</div>
          <div style={{
            background: '#f8fafc', border: '2px solid ' + C_BLUE,
            borderRadius: 6, padding: '14px 18px',
            fontSize: 36, fontWeight: 800, textAlign: 'center',
            fontFamily: '"IBM Plex Mono", monospace', color: C_BLUE_DEEP,
            letterSpacing: 2,
          }}>
            {val || <span style={{ color: '#cbd5e1' }}>—</span>}
          </div>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8, flex: 1 }}>
            {['1','2','3','4','5','6','7','8','9','←','0','C'].map(k => {
              const wide = false;
              const isDel = k === 'C';
              const isBack = k === '←';
              return (
                <button key={k} onClick={() => press(k)} style={{
                  height: 62,
                  background: isDel ? '#fef2f2' : isBack ? '#fefce8' : '#f1f5f9',
                  color: isDel ? C_RED : isBack ? C_AMBER : '#0f172a',
                  border: '1px solid ' + (isDel ? '#fecaca' : isBack ? '#fde68a' : '#cbd3de'),
                  borderRadius: 6, fontSize: 26, fontWeight: 700,
                  fontFamily: '"IBM Plex Mono", monospace',
                  cursor: 'pointer',
                }}>{k}</button>
              );
            })}
          </div>
          <button style={{
            height: 62, background: C_GREEN, color: '#fff',
            border: 'none', borderRadius: 6, fontSize: 22, fontWeight: 700,
            fontFamily: 'inherit', cursor: 'pointer',
          }}>אישור</button>
        </div>
        {/* Right: list */}
        <div style={{ background: '#fff', border: '1px solid #cbd3de', borderRadius: 6, display: 'flex', flexDirection: 'column', overflow: 'hidden' }}>
          <div style={{ display: 'flex', gap: 10, padding: 12, borderBottom: '1px solid #e2e8f0', alignItems: 'center' }}>
            <div style={{ fontSize: 15, color: '#64748b', fontWeight: 600 }}>בחר איתור:</div>
            <select style={{ padding: '8px 10px', border: '1px solid #cbd3de', borderRadius: 4, fontSize: 15, flex: 1 }}>
              <option>— הכל —</option>
            </select>
            <button style={{
              padding: '8px 18px', background: '#475569', color: '#fff',
              border: 'none', borderRadius: 4, fontSize: 15, fontWeight: 600, cursor: 'pointer',
            }}>יציאה</button>
          </div>
          <div style={{ flex: 1, overflow: 'auto' }}>
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
              <thead style={{ background: '#f1f5f9', position: 'sticky', top: 0 }}>
                <tr>
                  <th style={tableHdr}>מכולה</th>
                  <th style={tableHdr}>גודל</th>
                  <th style={tableHdr}>תאריך כניסה</th>
                  <th style={tableHdr}>איתור</th>
                </tr>
              </thead>
              <tbody>
                {[
                  { id: 'SEGU8099692', sz: 20, when: '10:03 26/10/25', loc: 'שטיפה TK' },
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
                  <tr key={i} style={{ background: i === 0 ? '#dbeafe' : (i % 2 ? '#f8fafc' : '#fff') }}>
                    <td style={{ ...tableCell, fontFamily: '"IBM Plex Mono", monospace', fontWeight: 600 }}>{r.id}</td>
                    <td style={{ ...tableCell, textAlign: 'center' }}>{r.sz}</td>
                    <td style={{ ...tableCell, fontFamily: '"IBM Plex Mono", monospace' }}>{r.when}</td>
                    <td style={{ ...tableCell, fontFamily: '"IBM Plex Mono", monospace', fontWeight: 600 }}>{r.loc}</td>
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

const tableHdr = {
  fontSize: 13, fontWeight: 700, color: '#334155', padding: '10px 12px',
  textAlign: 'right', borderBottom: '1px solid #cbd3de',
};
const tableCell = {
  fontSize: 14, padding: '10px 12px', borderBottom: '1px solid #f1f5f9',
  color: '#1e293b',
};

function ConservativeContainerDetail() {
  const c = ACTIVE_JOB.container;
  const rows = [
    ['מכולה', c.id],
    ['גודל', c.size + "'"],
    ['סוג', c.type],
    ['טיפול', c.handling],
    ['איתור', c.location],
    ['משקל', c.weight.toLocaleString() + ' ק"ג'],
    ['לקוח', c.customer],
    ['קפסיטי', c.capacity],
  ];
  return (
    <div style={{ ...conservativeStyles.root, gridTemplateRows: '56px 1fr' }}>
      <ConservativeTopBar crane={CRANE} />
      <div style={{ padding: 16, display: 'grid', gridTemplateColumns: '1fr 320px', gap: 14, minHeight: 0 }}>
        {/* Deal containers list */}
        <div style={{ background: '#fff', border: '1px solid #cbd3de', borderRadius: 6, overflow: 'hidden', display: 'flex', flexDirection: 'column' }}>
          <div style={{ background: '#f1f5f9', padding: '12px 16px', borderBottom: '1px solid #cbd3de', fontWeight: 700, fontSize: 15 }}>
            מכולות בעיסקה ({DEAL_CONTAINERS.length})
          </div>
          <div style={{ overflow: 'auto', flex: 1 }}>
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
              <thead>
                <tr style={{ background: '#f8fafc' }}>
                  <th style={tableHdr}>מכולה</th>
                  <th style={tableHdr}>איתור</th>
                  <th style={tableHdr}>גודל</th>
                  <th style={tableHdr}>סוג</th>
                  <th style={tableHdr}>משקל</th>
                </tr>
              </thead>
              <tbody>
                {DEAL_CONTAINERS.map((c, i) => (
                  <tr key={i} style={{ background: i === 0 ? '#dbeafe' : (i % 2 ? '#f8fafc' : '#fff') }}>
                    <td style={{ ...tableCell, fontFamily: '"IBM Plex Mono", monospace', fontWeight: 600 }}>{c.id}</td>
                    <td style={{ ...tableCell, fontFamily: '"IBM Plex Mono", monospace' }}>{c.loc}</td>
                    <td style={{ ...tableCell, textAlign: 'center' }}>{c.size}'</td>
                    <td style={{ ...tableCell, textAlign: 'center' }}>{c.type}</td>
                    <td style={{ ...tableCell, textAlign: 'left', fontFamily: '"IBM Plex Mono", monospace' }}>{c.weight.toLocaleString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
        {/* Detail card */}
        <div style={{ background: '#fff', border: '1px solid #cbd3de', borderRadius: 6, padding: 20, display: 'flex', flexDirection: 'column', gap: 14 }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div style={{ fontSize: 14, color: '#64748b', fontWeight: 600 }}>פרטי מכולה</div>
            <button style={{
              width: 40, height: 40, border: '1px solid #cbd3de',
              background: '#f8fafc', borderRadius: 6, fontSize: 18, cursor: 'pointer',
            }}>✕</button>
          </div>
          <div style={{
            background: '#f0f9ff', border: '2px solid ' + C_BLUE, borderRadius: 6,
            padding: 14, fontFamily: '"IBM Plex Mono", monospace',
            fontSize: 22, fontWeight: 800, color: C_BLUE_DEEP, textAlign: 'center',
          }}>
            {c.id}
          </div>
          <div style={{ display: 'flex', flexDirection: 'column', gap: 0 }}>
            {rows.slice(1).map(([k, v], i) => (
              <div key={k} style={{
                display: 'flex', justifyContent: 'space-between',
                padding: '10px 4px',
                borderBottom: i < rows.length - 2 ? '1px solid #f1f5f9' : 'none',
              }}>
                <span style={{ color: '#64748b', fontSize: 14 }}>{k}</span>
                <span style={{ fontWeight: 700, fontSize: 15, fontFamily: k === 'איתור' || k === 'משקל' ? '"IBM Plex Mono", monospace' : 'inherit' }}>
                  {v}
                </span>
              </div>
            ))}
          </div>
          <div style={{ flex: 1 }} />
          <button style={{
            height: 54, background: C_GREEN, color: '#fff',
            border: 'none', borderRadius: 6, fontSize: 18, fontWeight: 700,
            fontFamily: 'inherit', cursor: 'pointer',
          }}>בחר מכולה זו</button>
        </div>
      </div>
    </div>
  );
}

window.ConservativeMain = ConservativeMain;
window.ConservativeNumpad = ConservativeNumpad;
window.ConservativeContainerDetail = ConservativeContainerDetail;
