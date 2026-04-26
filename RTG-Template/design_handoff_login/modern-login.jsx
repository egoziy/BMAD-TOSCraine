// Login screen - Modern variant, new system feel (no cheesy AI blue glow, just clean + confident)

const LOGIN_USERS = [
  { id: 'GOLD1',   name: 'דוד כהן',       role: 'מנופאי בכיר',  shift: '07:00–15:00' },
  { id: 'MOSHE1',  name: 'משה לוי',       role: 'מנופאי',          shift: '07:00–15:00' },
  { id: 'AVI2',    name: 'אבי כהן',      role: 'מנופאי',          shift: '15:00–23:00' },
  { id: 'YONI3',   name: 'יוני שפירא',    role: 'מנופאי בכיר',  shift: '23:00–07:00' },
  { id: 'RONIT4',  name: 'רונית מזרחי',    role: 'מנופאית',         shift: '07:00–15:00' },
  { id: 'AMIR5',   name: 'עמיר בןדוד',    role: 'מתאמן',           shift: 'On-call' },
];

const LOGIN_CRANES = [
  { id: 'GOLD1', block: 'BOND1', status: 'זמין',   ok: true  },
  { id: 'GOLD2', block: 'BOND1', status: 'זמין',   ok: true  },
  { id: 'GOLD3', block: 'BOND2', status: 'בתחזוקה', ok: false },
];

function ModernLogin() {
  const [userId, setUserId] = React.useState('');
  const [userOpen, setUserOpen] = React.useState(false);
  const [pin, setPin] = React.useState('');
  const [crane, setCrane] = React.useState('GOLD1');
  const [focus, setFocus] = React.useState('user');
  const [showKb, setShowKb] = React.useState(true);

  const user = LOGIN_USERS.find(u => u.id === userId);
  const maxPin = 4;

  const onKey = (k) => {
    if (focus !== 'pin') setFocus('pin');
    if (k === '←') setPin(p => p.slice(0, -1));
    else if (k === 'CLR') setPin('');
    else if (pin.length < maxPin && /^[0-9]$/.test(k)) setPin(p => p + k);
  };

  const selected = LOGIN_CRANES.find(c => c.id === crane);
  const canLogin = userId && pin.length === maxPin && selected && selected.ok;

  return (
    <div style={{
      width: '100%', height: '100%', position: 'relative', overflow: 'hidden',
      fontFamily: '"Heebo", system-ui, sans-serif', color: '#0f172a',
      background: '#f6f8fa', direction: 'rtl',
    }}>

      {/* Ambient background — large soft gradient + subtle grid + port silhouette */}
      <BackgroundLayer />

      {/* Top bar */}
      <div style={{
        position: 'relative', zIndex: 2,
        display: 'flex', alignItems: 'center', padding: '22px 32px', gap: 16,
      }}>
        <img src="goldbond-logo.png" alt="גולד-בונד" style={{
          height: 108, width: 'auto', objectFit: 'contain',
          filter: 'drop-shadow(0 3px 6px rgba(15,23,42,.12))',
        }} />
        <div style={{ width: 1, height: 72, background: '#cbd5e1', margin: '0 8px' }} />
        <div style={{ lineHeight: 1.15 }}>
          <div style={{ fontSize: 22, fontWeight: 800, letterSpacing: '-0.2px' }}>מערכת מנופאי RTG</div>
          <div style={{ fontSize: 13, color: '#64748b', fontFamily: '"IBM Plex Mono", monospace' }}>
            גרסה 4.2.1 · build 20260415
          </div>
        </div>

        <div style={{ flex: 1 }} />

        <SystemPill label="PLC"  ok="#16a34a" text="מחובר" />
        <SystemPill label="GPS"  ok="#16a34a" text="RTK · 14 לוויינים" />
        <SystemPill label="TOS"  ok="#16a34a" text="online" />
        <SystemPill label="רשת"  ok="#16a34a" text="1 Gbps" />

        <div style={{
          fontFamily: '"IBM Plex Mono", monospace', fontSize: 24, fontWeight: 700,
          marginInlineStart: 14, color: '#0f172a',
        }}>07:12:04</div>
      </div>

      {/* Main content: two columns */}
      <div style={{
        position: 'relative', zIndex: 2,
        padding: '14px 64px 32px',
        display: 'grid', gridTemplateColumns: '1.15fr 1fr', gap: 44,
        height: 'calc(100% - 112px)',
      }}>

        {/* RIGHT: hero / welcome (visually first in RTL) */}
        <div style={{
          display: 'flex', flexDirection: 'column', justifyContent: 'center', gap: 28,
          paddingBlock: 20,
        }}>
          <div>
            <div style={{
              display: 'inline-flex', alignItems: 'center', gap: 10,
              padding: '8px 16px', borderRadius: 999,
              background: '#ccfbf1', color: '#115e59',
              fontSize: 13, fontWeight: 700, letterSpacing: 1,
              border: '1px solid #99f6e4',
            }}>
              <span style={{
                width: 8, height: 8, borderRadius: 8, background: '#0f766e',
                boxShadow: '0 0 0 4px rgba(15,118,110,.2)',
              }} />
              מערכת חדשה · דור 4
            </div>
            <h1 style={{
              fontSize: 72, fontWeight: 900, letterSpacing: '-1.5px',
              lineHeight: 1.02, margin: '18px 0 10px',
            }}>
              בוקר טוב.<br />
              <span style={{
                background: 'linear-gradient(135deg, #0f766e 0%, #0284c7 60%, #7c3aed 100%)',
                WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent',
              }}>מוכנים להתחיל?</span>
            </h1>
            <p style={{
              fontSize: 18, color: '#475569', lineHeight: 1.5, maxWidth: 480,
              margin: 0, textWrap: 'pretty',
            }}>
              זיהוי המנופאי וחיבור למנוף. כל התנועות בערוגה מסונכרנות בזמן אמת עם המערכת הלוגיסטית.
            </p>
          </div>

          {/* Live status strip */}
          <div style={{
            display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: 12,
            padding: 18,
            background: 'rgba(255,255,255,.7)',
            backdropFilter: 'blur(10px)',
            border: '1px solid #e2e8f0',
            borderRadius: 16,
            boxShadow: '0 8px 28px rgba(15,23,42,.04)',
          }}>
            <StatStrip label="מכולות פעילות" value="2,847" delta="+12" />
            <StatStrip label="משאיות ממתינות" value="7" sub="שער ראשי" />
            <StatStrip label="תנועות היום" value="184" delta="+4%" />
            <StatStrip label="זמן מחזור ממוצע" value="4:12" sub="דקות" />
          </div>
        </div>

        {/* LEFT: login card */}
        <div style={{
          background: '#ffffff',
          border: '1px solid #e2e8f0',
          borderRadius: 20,
          boxShadow: '0 30px 60px -20px rgba(15,23,42,.18), 0 0 0 1px rgba(15,23,42,.02)',
          padding: 28,
          display: 'flex', flexDirection: 'column', gap: 18,
          position: 'relative', overflow: 'hidden',
        }}>
          {/* corner accent */}
          <div style={{
            position: 'absolute', top: -60, left: -60,
            width: 220, height: 220, borderRadius: '50%',
            background: 'radial-gradient(circle, rgba(15,118,110,.12), transparent 70%)',
            pointerEvents: 'none',
          }} />

          <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
            <div style={{
              width: 44, height: 44, borderRadius: 12, background: '#0f766e',
              display: 'grid', placeItems: 'center', color: '#fff', fontSize: 20,
              boxShadow: '0 6px 14px rgba(15,118,110,.35)',
            }}>🔐</div>
            <div style={{ lineHeight: 1.15 }}>
              <div style={{ fontSize: 20, fontWeight: 800 }}>כניסת מפעיל</div>
              <div style={{ fontSize: 12, color: '#64748b' }}>זיהוי + בחירת מנוף</div>
            </div>
            <div style={{ flex: 1 }} />
            <div style={{
              padding: '6px 12px', borderRadius: 999, fontSize: 11, fontWeight: 700,
              background: '#f1f5f9', color: '#475569',
              fontFamily: '"IBM Plex Mono", monospace', letterSpacing: 1,
            }}>SHIFT 07:00–15:00</div>
          </div>

          {/* Username dropdown */}
          <div style={{ position: 'relative' }}>
            <div style={{ fontSize: 12, fontWeight: 700, color: '#64748b', letterSpacing: .5, marginBottom: 6 }}>
              שם משתמש
            </div>
            <button
              onClick={() => { setFocus('user'); setUserOpen(o => !o); }}
              style={{
                width: '100%', height: 62, borderRadius: 12, padding: '0 18px',
                border: '2px solid ' + (focus === 'user' || userOpen ? '#0f766e' : '#e2e8f0'),
                background: (focus === 'user' || userOpen) ? '#f0fdfa' : '#f8fafc',
                display: 'flex', alignItems: 'center', gap: 12,
                cursor: 'pointer', textAlign: 'right', fontFamily: 'inherit',
              }}>
              <span style={{
                width: 38, height: 38, borderRadius: 10,
                background: user ? '#0f766e' : '#e2e8f0',
                color: user ? '#fff' : '#94a3b8',
                display: 'grid', placeItems: 'center', fontSize: 16, fontWeight: 800,
              }}>{user ? user.name.charAt(0) : 'א'}</span>
              <div style={{ flex: 1, textAlign: 'right', lineHeight: 1.15 }}>
                {user ? (
                  <>
                    <div style={{ fontSize: 18, fontWeight: 700, color: '#0f172a' }}>{user.name}</div>
                    <div style={{ fontSize: 12, color: '#64748b', fontFamily: '"IBM Plex Mono", monospace' }}>
                      {user.id} · {user.role}
                    </div>
                  </>
                ) : (
                  <span style={{ fontSize: 18, fontWeight: 500, color: '#94a3b8' }}>בחר משתמש מהרשימה</span>
                )}
              </div>
              <span style={{ fontSize: 18, color: '#64748b', transition: 'transform .2s',
                transform: userOpen ? 'rotate(180deg)' : 'rotate(0)' }}>▾</span>
            </button>
            {userOpen && (
              <div style={{
                position: 'absolute', top: 'calc(100% + 6px)', left: 0, right: 0, zIndex: 10,
                background: '#fff', border: '1px solid #e2e8f0', borderRadius: 12,
                boxShadow: '0 20px 40px rgba(15,23,42,.18)',
                maxHeight: 280, overflowY: 'auto', padding: 6,
              }}>
                {LOGIN_USERS.map(u => {
                  const active = u.id === userId;
                  return (
                    <button key={u.id}
                      onClick={() => { setUserId(u.id); setUserOpen(false); setFocus('pin'); }}
                      style={{
                        width: '100%', padding: '10px 12px', borderRadius: 10,
                        background: active ? '#ccfbf1' : 'transparent',
                        border: 'none', cursor: 'pointer', textAlign: 'right',
                        display: 'flex', alignItems: 'center', gap: 12, fontFamily: 'inherit',
                        marginBottom: 2,
                      }}>
                      <span style={{
                        width: 34, height: 34, borderRadius: 8,
                        background: active ? '#0f766e' : '#f1f5f9',
                        color: active ? '#fff' : '#475569',
                        display: 'grid', placeItems: 'center', fontWeight: 800, fontSize: 14,
                      }}>{u.name.charAt(0)}</span>
                      <div style={{ flex: 1, lineHeight: 1.2 }}>
                        <div style={{ fontSize: 14, fontWeight: 700, color: '#0f172a' }}>{u.name}</div>
                        <div style={{ fontSize: 11, color: '#64748b', fontFamily: '"IBM Plex Mono", monospace' }}>
                          {u.id} · {u.role}
                        </div>
                      </div>
                      <span style={{ fontSize: 10, color: '#64748b', fontFamily: '"IBM Plex Mono", monospace' }}>
                        {u.shift}
                      </span>
                    </button>
                  );
                })}
              </div>
            )}
          </div>

          {/* PIN field */}
          <div>
            <div style={{ fontSize: 12, fontWeight: 700, color: '#64748b', letterSpacing: .5, marginBottom: 6 }}>
              קוד זיהוי (PIN)
            </div>
            <div
              onClick={() => setFocus('pin')}
              style={{
                height: 62, borderRadius: 12, padding: '0 18px',
                border: '2px solid ' + (focus === 'pin' ? '#0f766e' : '#e2e8f0'),
                background: focus === 'pin' ? '#f0fdfa' : '#f8fafc',
                display: 'flex', alignItems: 'center', gap: 14,
                cursor: 'pointer', transition: 'all .15s',
              }}>
              {Array.from({ length: maxPin }).map((_, i) => {
                const filled = i < pin.length;
                const caret = focus === 'pin' && i === pin.length;
                return (
                  <div key={i} style={{
                    width: 50, height: 50, borderRadius: 10,
                    background: filled ? '#0f766e' : '#fff',
                    border: '2px solid ' + (caret ? '#0f766e' : filled ? '#0f766e' : '#cbd5e1'),
                    display: 'grid', placeItems: 'center',
                    color: '#fff', fontSize: 28, fontWeight: 800,
                    fontFamily: '"IBM Plex Mono", monospace',
                    transition: 'all .12s',
                    boxShadow: caret ? '0 0 0 4px rgba(15,118,110,.15)' : 'none',
                  }}>{filled ? '●' : ''}</div>
                );
              })}
              <div style={{ flex: 1 }} />
              <span style={{ fontSize: 11, color: '#94a3b8', fontFamily: '"IBM Plex Mono", monospace' }}>
                {pin.length}/{maxPin}
              </span>
            </div>
          </div>

          {/* Crane picker */}
          <div>
            <div style={{
              display: 'flex', alignItems: 'baseline', gap: 8, marginBottom: 6,
            }}>
              <span style={{ fontSize: 12, fontWeight: 700, color: '#64748b', letterSpacing: .5 }}>
                בחירת מנוף
              </span>
              <span style={{ fontSize: 11, color: '#94a3b8' }}>· שיוך פעיל לגוש</span>
            </div>
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8 }}>
              {LOGIN_CRANES.map(c => {
                const active = c.id === crane;
                return (
                  <button key={c.id}
                    onClick={() => c.ok && setCrane(c.id)}
                    disabled={!c.ok}
                    style={{
                      padding: '12px 10px', borderRadius: 12,
                      border: '2px solid ' + (active ? '#0f766e' : '#e2e8f0'),
                      background: active ? '#0f766e' : !c.ok ? '#f8fafc' : '#fff',
                      color: active ? '#fff' : !c.ok ? '#94a3b8' : '#0f172a',
                      fontFamily: 'inherit', cursor: c.ok ? 'pointer' : 'not-allowed',
                      display: 'flex', flexDirection: 'column', alignItems: 'flex-start', gap: 4,
                      boxShadow: active ? '0 8px 20px rgba(15,118,110,.3)' : 'none',
                      transition: 'all .15s',
                      opacity: !c.ok ? .65 : 1,
                    }}>
                    <div style={{ display: 'flex', width: '100%', alignItems: 'center' }}>
                      <span style={{ fontSize: 20, fontWeight: 800 }}>{c.id}</span>
                      <div style={{ flex: 1 }} />
                      <span style={{
                        width: 8, height: 8, borderRadius: 8,
                        background: c.ok ? (active ? '#a7f3d0' : '#16a34a') : '#fbbf24',
                      }} />
                    </div>
                    <div style={{ fontSize: 11, opacity: active ? .85 : .65, fontWeight: 600 }}>
                      {c.block} · {c.status}
                    </div>
                  </button>
                );
              })}
            </div>
          </div>

          {/* Action buttons */}
          <div style={{ display: 'flex', gap: 10, marginTop: 4 }}>
            <button style={{
              flex: 1, height: 60, borderRadius: 14,
              background: '#fff', color: '#475569',
              border: '1.5px solid #e2e8f0',
              fontSize: 16, fontWeight: 700, fontFamily: 'inherit', cursor: 'pointer',
            }}>יציאה</button>
            <button disabled={!canLogin} style={{
              flex: 2, height: 60, borderRadius: 14,
              background: canLogin
                ? 'linear-gradient(135deg, #0f766e 0%, #134e4a 100%)'
                : '#cbd5e1',
              color: '#fff',
              border: 'none',
              fontSize: 17, fontWeight: 800, fontFamily: 'inherit',
              cursor: canLogin ? 'pointer' : 'not-allowed',
              display: 'flex', alignItems: 'center', justifyContent: 'center', gap: 10,
              boxShadow: canLogin ? '0 12px 28px rgba(15,118,110,.35)' : 'none',
              transition: 'all .2s',
            }}>
              כניסה למערכת
              <span style={{ fontSize: 20 }}>←</span>
            </button>
          </div>

          {/* Footer meta */}
          <div style={{
            display: 'flex', alignItems: 'center', gap: 10, marginTop: 4,
            padding: '10px 12px', background: '#f8fafc', borderRadius: 10,
            fontSize: 11, color: '#64748b',
          }}>
            <span>🛡️</span>
            <span>גישה מאובטחת · כל הפעולות נרשמות</span>
            <div style={{ flex: 1 }} />
            <span style={{ fontFamily: '"IBM Plex Mono", monospace' }}>session #A4F2-7710</span>
          </div>
        </div>
      </div>

      {/* Bottom numeric keypad (touchscreen) */}
      {showKb && (
        <NumKeypad
          mode={focus}
          onKey={onKey}
          onClose={() => setShowKb(false)}
        />
      )}
    </div>
  );
}

function Field({ label, value, placeholder, focused, onFocus }) {
  return (
    <div>
      <div style={{ fontSize: 12, fontWeight: 700, color: '#64748b', letterSpacing: .5, marginBottom: 6 }}>
        {label}
      </div>
      <div
        onClick={onFocus}
        style={{
          height: 62, borderRadius: 12, padding: '0 18px',
          border: '2px solid ' + (focused ? '#0f766e' : '#e2e8f0'),
          background: focused ? '#f0fdfa' : '#f8fafc',
          display: 'flex', alignItems: 'center', gap: 10,
          cursor: 'pointer', transition: 'all .15s',
          fontSize: 20, fontWeight: 600,
          color: value ? '#0f172a' : '#94a3b8',
          fontFamily: '"IBM Plex Mono", monospace',
        }}>
        <span style={{ fontSize: 18 }}>👤</span>
        <span>{value || placeholder}</span>
        {focused && <span style={{
          width: 2, height: 24, background: '#0f766e',
          animation: 'blink 1s infinite',
        }} />}
      </div>
      <style>{`@keyframes blink { 50% { opacity: 0 } }`}</style>
    </div>
  );
}

function SystemPill({ label, ok, text }) {
  return (
    <div style={{
      display: 'flex', alignItems: 'center', gap: 8,
      padding: '6px 12px', borderRadius: 999,
      background: 'rgba(255,255,255,.8)',
      backdropFilter: 'blur(8px)',
      border: '1px solid #e2e8f0',
    }}>
      <span style={{
        width: 8, height: 8, borderRadius: 8, background: ok,
        boxShadow: '0 0 0 3px ' + ok + '33',
      }} />
      <span style={{ fontSize: 11, color: '#64748b', fontWeight: 700, letterSpacing: .5 }}>{label}</span>
      <span style={{ fontSize: 12, color: '#0f172a', fontWeight: 600, fontFamily: '"IBM Plex Mono", monospace' }}>{text}</span>
    </div>
  );
}

function StatStrip({ label, value, delta, sub }) {
  return (
    <div>
      <div style={{ fontSize: 11, color: '#64748b', fontWeight: 700, letterSpacing: .5 }}>{label}</div>
      <div style={{ display: 'flex', alignItems: 'baseline', gap: 8, marginTop: 2 }}>
        <span style={{ fontSize: 26, fontWeight: 800, fontFamily: '"IBM Plex Mono", monospace' }}>{value}</span>
        {delta && <span style={{ fontSize: 12, color: '#16a34a', fontWeight: 700 }}>{delta}</span>}
        {sub && <span style={{ fontSize: 12, color: '#94a3b8' }}>{sub}</span>}
      </div>
    </div>
  );
}

function NumKeypad({ mode, onKey, onClose }) {
  const keys = ['1','2','3','4','5','6','7','8','9','CLR','0','←'];
  return (
    <div style={{
      position: 'absolute', bottom: 20, left: 64, right: 'auto',
      width: 380, zIndex: 5,
      background: '#fff', borderRadius: 16, padding: 14,
      border: '1px solid #e2e8f0',
      boxShadow: '0 20px 40px rgba(15,23,42,.18)',
    }}>
      <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 8 }}>
        <span style={{ fontSize: 13, fontWeight: 700, color: '#0f172a' }}>מקלדת נומרית</span>
        <span style={{ fontSize: 11, color: '#64748b' }}>· קוד זיהוי</span>
        <div style={{ flex: 1 }} />
        <button onClick={onClose} style={{
          width: 28, height: 28, borderRadius: 8, border: '1px solid #e2e8f0',
          background: '#f8fafc', cursor: 'pointer', fontSize: 14,
        }}>✕</button>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8 }}>
        {keys.map(k => {
          const isClr = k === 'CLR';
          const isBack = k === '←';
          return (
            <button key={k}
              onClick={() => onKey(k)}
              style={{
                height: 64, borderRadius: 12,
                background: isClr ? '#fef2f2' : isBack ? '#fffbeb' : '#fff',
                color: isClr ? '#dc2626' : isBack ? '#d97706' : '#0f172a',
                border: '1.5px solid ' + (isClr ? '#fecaca' : isBack ? '#fde68a' : '#e2e8f0'),
                fontSize: isClr ? 15 : 26, fontWeight: 700, fontFamily: 'inherit',
                cursor: 'pointer',
              }}>{k}</button>
          );
        })}
      </div>
    </div>
  );
}

// Ambient background: soft radial wash + subtle grid + container silhouettes at the bottom
function BackgroundLayer() {
  return (
    <>
      <div style={{
        position: 'absolute', inset: 0, zIndex: 0,
        background: 'radial-gradient(1100px 700px at 85% -20%, rgba(15,118,110,.14), transparent 60%),' +
                    'radial-gradient(900px 600px at 10% 110%, rgba(2,132,199,.10), transparent 60%),' +
                    'linear-gradient(180deg, #f6f8fa 0%, #eef2f7 100%)',
      }} />
      {/* faint grid */}
      <svg style={{ position: 'absolute', inset: 0, width: '100%', height: '100%', zIndex: 0, opacity: .5 }}
           xmlns="http://www.w3.org/2000/svg">
        <defs>
          <pattern id="grid" width="44" height="44" patternUnits="userSpaceOnUse">
            <path d="M 44 0 L 0 0 0 44" fill="none" stroke="#cbd5e1" strokeWidth=".5" />
          </pattern>
        </defs>
        <rect width="100%" height="100%" fill="url(#grid)" />
      </svg>
      {/* bottom container silhouette - stacked containers */}
      <svg style={{
        position: 'absolute', bottom: 0, left: 0, right: 0,
        width: '100%', height: 180, zIndex: 0, opacity: .08,
      }} viewBox="0 0 1920 180" preserveAspectRatio="xMidYMax slice">
        <g fill="#0f172a">
          {/* row 1 */}
          {Array.from({ length: 16 }).map((_, i) => (
            <rect key={'a' + i} x={i * 122 + 8} y="120" width="108" height="52" rx="2" />
          ))}
          {/* row 2 */}
          {Array.from({ length: 14 }).map((_, i) => (
            <rect key={'b' + i} x={i * 122 + 68} y="70" width="108" height="52" rx="2" />
          ))}
          {/* row 3 */}
          {Array.from({ length: 10 }).map((_, i) => (
            <rect key={'c' + i} x={i * 122 + 180} y="20" width="108" height="52" rx="2" />
          ))}
        </g>
      </svg>
    </>
  );
}

Object.assign(window, { ModernLogin });
