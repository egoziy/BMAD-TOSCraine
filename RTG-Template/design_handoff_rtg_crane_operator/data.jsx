// Shared mock data for all variations

const ROWS = ['A','B','C','D','E','F','G','T'];
const COLS = [185,182,178,174,170,166,162,158,154,150,146,142,138,134];

// Generate the stack-height grid matching the screenshots
// Most cells: 6 (full). Some lower numbers. Some empty.
const GRID = (() => {
  // Seeded pseudo-random for stable output
  const seed = [
    // row A:   185 182 178 174 170 166 162 158 154 150 146 142 138 134
    [null, 2, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4],
    [null, 4, 5, 4, 6, 6, 6, 5, 5, 4, 5, 6, 3, 5],
    [null, 6, 3, 5, 6, 6, 4, 1, 4, 6, 1, 3, 6, 6],
    [null, 6, 6, 6, 5, 4, 6, 5, 6, 5, 5, 6, 6, 4],
    [null, 5, 6, 5, 6, 3, 6, 4, 6, 5, 6, 3, 5, 6],
    [null, 6, 6, 6, 1, 5, 5, 6, 6, 6, 6, 6, 6, 6],
    [null, null, null, null, null, null, null, null, null, null, null, null, null, null],
    [null, null, null, null, null, null, null, null, null, null, null, null, null, null],
  ];
  return seed;
})();

// The currently-active move / job
const ACTIVE_JOB = {
  source: { row: 'C', col: 138, label: '138C6', height: 6 },
  target: { row: 'E', col: 170, label: '170E4', height: 3 },
  container: {
    id: 'MSNU8552840',
    size: '40',
    type: 'HC',
    handling: 'FR',
    weight: 32200,
    location: '138C6',
    customer: 'יחדיו',
    capacity: 'מלא',
    seal: '—',
    approval: '—',
    line: '—',
  },
};

const CRANE = {
  id: 'GOLD1',
  block: 'BOND1',
  position: '099D4',
  row: 'D',
  col: 99,
  plc: { status: 'מחובר', ok: true },
  gps: {
    status: 'מעולה',
    ok: true,
    lat: 32.0183,
    lng: 34.7553,
    accuracy: 0.8,      // meters
    satellites: 14,
    fixType: 'RTK-Fix',
    heading: 87,        // degrees
    speed: 0.4,         // m/s
    lastUpdate: '1s',
  },
  carrying: null,
};

const DEAL_CONTAINERS = [
  { id: 'CAAU8074056', loc: '162C1', size: 40, type: 'HC', weight: 27800 },
  { id: 'CAAU8100200', loc: '62',    size: 40, type: 'HC', weight: 29000 },
  { id: 'FFAU6601629', loc: '150C6', size: 40, type: 'HC', weight: 27600 },
  { id: 'FSCU8345625', loc: '174B4', size: 40, type: 'HC', weight: 32000 },
  { id: 'MEDU8828289', loc: '62',    size: 40, type: 'HC', weight: 26920 },
  { id: 'MSDU8284140', loc: '62',    size: 40, type: 'HC', weight: 32450 },
  { id: 'TCLU1819383', loc: '68',    size: 40, type: 'HC', weight: 33400 },
  { id: 'TGHU9821299', loc: '56',    size: 40, type: 'HC', weight: 33000 },
];

const JOBS = [
  { id: 'ADMU5111399', size: 40, type: 'HC', weight: 12180, loc: '112B6', place: '6/6', when: '15:00 9/4', task: 'שיקוף מכולה', block: 'BOND1' },
  { id: 'BEAU5499190', size: 40, type: 'HC', weight: 2200,  loc: '112F5', place: '5/6', when: '17:00 9/4', task: 'שיקוף מכולה', block: 'BOND1' },
  { id: 'CAAU4560532', size: 40, type: 'HC', weight: 17688, loc: '134B4', place: '4/5', when: '16:00 9/4', task: 'שיקוף מכולה', block: 'BOND2' },
  { id: 'CAAU4560532', size: 40, type: 'HC', weight: 17688, loc: '134B4', place: '4/5', when: '16:00 9/4', task: 'שיקוף מכולה', block: 'HGC6'  },
  { id: 'CAIU9026271', size: 40, type: 'HC', weight: 17688, loc: '124E4', place: '4/6', when: '16:00 9/4', task: 'שיקוף מכולה', block: 'BOND1' },
  { id: 'HMMU4246857', size: 40, type: 'HC', weight: 15015, loc: '170E5', place: '5/6', when: '10:00 14/4', task: 'שיקוף מכולה', block: 'BOND2' },
  { id: 'JXLU6242104', size: 40, type: 'HC', weight: 11581, loc: '178F6', place: '6/6', when: '6/4',       task: 'איסוף מכולות במשאית גולד בונד', block: 'BOND2' },
  { id: 'LCGU8083818', size: 40, type: 'HC', weight: 5946,  loc: '158A5', place: '5/6', when: '15:00 9/4', task: 'שיקוף מכולה', block: 'BOND1' },
  { id: 'MEDU7796504', size: 40, type: 'HC', weight: 17688, loc: '120A3', place: '3/6', when: '16:00 9/4', task: 'שיקוף מכולה', block: 'BOND1' },
  { id: 'MEDU8107528', size: 40, type: 'HC', weight: 17688, loc: '106B5', place: '5/4', when: '16:00 9/4', task: 'שיקוף מכולה', block: 'BOND1' },
  { id: 'MEDU9098879', size: 40, type: 'RH', weight: 22589, loc: '128B4', place: '4/3', when: '6/4',       task: 'איסוף מכולות במשאית גולד בונד', block: 'BOND1' },
  { id: 'MEDU9619616', size: 40, type: 'RH', weight: 19350, loc: '132B3', place: '3/4', when: '6/4',       task: 'איסוף מכולות במשאית גולד בונד', block: 'BOND1' },
  { id: 'MEDU9679240', size: 40, type: 'RH', weight: 19510, loc: '128A4', place: '4/4', when: '6/4',       task: 'איסוף מכולות במשאית גולד בונד', block: 'BOND1' },
];

const TRUCK_UNLOAD = [
  { id: 'MSDU8925563', size: 40, type: 'HC', weight: 4400,  code: 'PP', line: 'MSC', truck: '6B', loc: '', wait: '', un: '', avdm: 'AV' },
  { id: 'TCKU1278039', size: 20, type: 'RG', weight: 13863, code: 'EX', line: 'ZIM', truck: '26', loc: '', wait: '', un: '9.0', avdm: 'AV' },
];

const TRUCK_LOAD = [];

const MENU_TILES = [
  { id: 'empty',     label: 'מכולות ריקות', icon: '📦' },
  { id: 'noloc',     label: 'מכולות ללא איתור', icon: '❓' },
  { id: 'log',       label: 'יומן תנועות', icon: '📋' },
  { id: 'block',     label: 'מכולות לגוש', icon: '🔲' },
  { id: 'update',    label: 'עדכון איתור', icon: '✏️' },
  { id: 'suggested', label: 'איתורים מומלצים', icon: '⭐' },
  { id: 'expected',  label: 'מכולות צפויות', icon: '⏰' },
  { id: 'rtg',       label: 'RTG', icon: '🏗️' },
];

Object.assign(window, {
  ROWS, COLS, GRID, ACTIVE_JOB, CRANE,
  DEAL_CONTAINERS, JOBS, TRUCK_UNLOAD, TRUCK_LOAD, MENU_TILES,
});
