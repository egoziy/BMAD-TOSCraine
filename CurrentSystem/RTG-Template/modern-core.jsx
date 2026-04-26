// VARIATION 2 — MODERN (Material-inspired)
// Light surface, teal primary, amber accent, cards with elevation, pill chips.
// Grid uses soft color-coded backgrounds per stack height.

const M_TEAL = '#0f766e';
const M_TEAL_DEEP = '#115e59';
const M_TEAL_SOFT = '#ccfbf1';
const M_AMBER = '#d97706';
const M_GREEN = '#16a34a';
const M_RED = '#dc2626';
const M_VIOLET = '#7c3aed';
const M_SURFACE = '#f6f8fa';
const M_CARD = '#ffffff';
const M_INK = '#0f172a';
const M_MUTED = '#64748b';
const M_LINE = '#e2e8f0';

const modernRoot = {
  width: '100%', height: '100%',
  background: M_SURFACE,
  fontFamily: '"Heebo", system-ui, sans-serif',
  color: M_INK,
  display: 'grid',
  gridTemplateRows: '76px 1fr',
  direction: 'rtl',
};

// ────────── Stack tile coloring ──────────
function modernStackTile(h, isSource, isTarget) {
  if (isSource) return { bg: 'linear-gradient(135deg, #fef3c7 0%, #fde68a 100%)', fg: '#78350f', border: '2.5px solid ' + M_AMBER };
  if (isTarget) return { bg: 'linear-gradient(135deg, #ccfbf1 0%, #99f6e4 100%)', fg: M_TEAL_DEEP, border: '2.5px dashed ' + M_TEAL };
  if (h === null || h === undefined) return { bg: '#fff', fg: '#cbd5e1', border: '1px solid #eef2f7' };
  if (h === 6) return { bg: '#fef2f2', fg: '#b91c1c', border: '1px solid #fecaca' };
  if (h === 5) return { bg: '#fff7ed', fg: '#c2410c', border: '1px solid #fed7aa' };
  if (h === 4) return { bg: '#fefce8', fg: '#a16207', border: '1px solid #fde68a' };
  if (h === 3) return { bg: '#f0fdf4', fg: '#15803d', border: '1px solid #bbf7d0' };
  if (h === 2) return { bg: '#ecfdf5', fg: '#047857', border: '1px solid #a7f3d0' };
  if (h === 1) return { bg: '#eff6ff', fg: '#1d4ed8', border: '1px solid #bfdbfe' };
  return { bg: '#fff', fg: M_INK, border: '1px solid #eef2f7' };
}

// ────────── TOP BAR with GPS ──────────
function ModernTopBar({ crane }) {
  return (
    <div style={{
      background: M_CARD,
      borderBottom: '1px solid ' + M_LINE,
      boxShadow: '0 1px 2px rgba(15,23,42,.04)',
      display: 'flex',
      alignItems: 'center',
      padding: '0 20px',
      gap: 14,
    }}>
      <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
        <div style={{
          width: 40, height: 40, borderRadius: 10,
          background: M_TEAL, color: '#fff',
          display: 'grid', placeItems: 'center',
          fontSize: 18, fontWeight: 800,
        }}>R</div>
        <div style={{ lineHeight: 1.1 }}>
          <div style={{ fontSize: 14, fontWeight: 700, color: M_INK }}>מערכת מנופאי</div>
          <div style={{ fontSize: 11, color: M_MUTED, fontFamily: '"IBM Plex Mono", monospace' }}>RTG · {crane.block}</div>
        </div>
      </div>
      <div style={{ width: 1, height: 40, background: M_LINE, marginInline: 4 }} />

      <ModernPill icon="🏗️" label="מנוף" value={crane.id} />
      <ModernPill icon="📍" label="מיקום" value={crane.position} mono />
      <ModernPill icon="📦" label="נושא" value={crane.carrying ? crane.carrying.id : 'ריק'} muted={!crane.carrying} />

      <div style={{ flex: 1 }} />

      {/* GPS DETAIL PANEL */}
      <ModernGPSPanel gps={crane.gps} />

      <Indicator ok={crane.plc.ok} label="PLC" />

      <div style={{ width: 1, height: 40, background: M_LINE }} />
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', lineHeight: 1.1 }}>
        <div style={{ fontSize: 20, fontWeight: 700, fontFamily: '"IBM Plex Mono", monospace' }}>17:42:34</div>
        <div style={{ fontSize: 11, color: M_MUTED }}>ד׳ · 24 אפריל 2026</div>
      </div>
    </div>
  );
}

function ModernPill({ icon, label, value, mono, muted }) {
  return (
    <div style={{
      display: 'flex', alignItems: 'center', gap: 10,
      background: '#f8fafc', border: '1px solid ' + M_LINE,
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

// Compact GPS with live detail
function ModernGPSPanel({ gps }) {
  const ok = gps.ok;
  return (
    <div style={{
      display: 'flex', alignItems: 'stretch', gap: 0,
      background: ok ? '#f0fdf4' : '#fef2f2',
      border: '1px solid ' + (ok ? '#bbf7d0' : '#fecaca'),
      borderRadius: 10,
      overflow: 'hidden',
    }}>
      {/* Satellite icon + bar */}
      <div style={{
        padding: '6px 10px',
        borderLeft: '1px solid ' + (ok ? '#bbf7d0' : '#fecaca'),
        display: 'flex', alignItems: 'center', gap: 8,
        background: ok ? '#dcfce7' : '#fee2e2',
      }}>
        <SatelliteGlyph ok={ok} />
        <div style={{ lineHeight: 1 }}>
          <div style={{ fontSize: 10, color: '#166534', fontWeight: 800, letterSpacing: .5 }}>GPS · {gps.fixType}</div>
          <div style={{ fontSize: 13, fontWeight: 800, color: '#166534', marginTop: 2 }}>
            {gps.satellites} לווינים · דיוק {gps.accuracy} מ׳
          </div>
        </div>
      </div>
      {/* Coordinates */}
      <div style={{ padding: '6px 12px', display: 'flex', flexDirection: 'column', justifyContent: 'center', gap: 2 }}>
        <div style={{ display: 'flex', gap: 8, alignItems: 'baseline' }}>
          <span style={{ fontSize: 10, color: M_MUTED, fontWeight: 700 }}>LAT</span>
          <span style={{ fontSize: 12, fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700 }}>{gps.lat.toFixed(5)}°N</span>
        </div>
        <div style={{ display: 'flex', gap: 8, alignItems: 'baseline' }}>
          <span style={{ fontSize: 10, color: M_MUTED, fontWeight: 700 }}>LNG</span>
          <span style={{ fontSize: 12, fontFamily: '"IBM Plex Mono", monospace', fontWeight: 700 }}>{gps.lng.toFixed(5)}°E</span>
        </div>
      </div>
      {/* Heading + speed */}
      <div style={{
        padding: '6px 10px', borderRight: '1px solid ' + (ok ? '#bbf7d0' : '#fecaca'),
        display: 'flex', alignItems: 'center', gap: 8,
      }}>
        <CompassGlyph deg={gps.heading} />
        <div style={{ lineHeight: 1 }}>
          <div style={{ fontSize: 10, color: M_MUTED, fontWeight: 700 }}>כיוון</div>
          <div style={{ fontSize: 13, fontWeight: 800, fontFamily: '"IBM Plex Mono", monospace' }}>{gps.heading}° · {gps.speed} מ/ש</div>
        </div>
      </div>
    </div>
  );
}

function SatelliteGlyph({ ok }) {
  return (
    <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
      <circle cx="12" cy="12" r="10" stroke={ok ? '#16a34a' : '#dc2626'} strokeWidth="1.5" strokeDasharray="3 2" />
      <circle cx="12" cy="12" r="5"  stroke={ok ? '#16a34a' : '#dc2626'} strokeWidth="1.5" />
      <circle cx="12" cy="12" r="1.5" fill={ok ? '#16a34a' : '#dc2626'} />
      {[0, 90, 180, 270].map(a => (
        <circle key={a} cx={12 + 9 * Math.cos(a * Math.PI / 180)} cy={12 + 9 * Math.sin(a * Math.PI / 180)} r="1" fill={ok ? '#16a34a' : '#dc2626'} />
      ))}
    </svg>
  );
}

function CompassGlyph({ deg }) {
  return (
    <svg width="22" height="22" viewBox="0 0 24 24">
      <circle cx="12" cy="12" r="10" stroke="#64748b" strokeWidth="1.5" fill="#fff" />
      <g transform={`rotate(${deg} 12 12)`}>
        <path d="M 12 4 L 14 12 L 12 11 L 10 12 Z" fill="#dc2626" />
        <path d="M 12 20 L 14 12 L 12 13 L 10 12 Z" fill="#64748b" />
      </g>
    </svg>
  );
}

function Indicator({ label, ok }) {
  return (
    <div style={{
      display: 'flex', alignItems: 'center', gap: 6,
      padding: '8px 12px', borderRadius: 10,
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

Object.assign(window, {
  M_TEAL, M_TEAL_DEEP, M_TEAL_SOFT, M_AMBER, M_GREEN, M_RED, M_VIOLET,
  M_SURFACE, M_CARD, M_INK, M_MUTED, M_LINE,
  modernRoot, modernStackTile,
  ModernTopBar, ModernPill, ModernGPSPanel, Indicator,
});
