-- ================================================================
-- RTG Crane Location System — PostgreSQL schema (Phase 2, Step 2.2)
-- Target: PostgreSQL 16
-- Author: Claude Code  ·  Date: 2026-04-21
-- Notes:  All SQL comments in English.  User-facing strings (Hebrew)
--         live in the application layer, not the DB.
-- ================================================================

-- This is a greenfield schema for the RTG module only.
-- Shared enterprise entities (containers, deals, HR, clients) remain
-- in MSSQL for the duration of the dual-write transition.
-- See 02_data_model.md §6 for the mapping from legacy MSSQL.

SET search_path = public;

CREATE SCHEMA IF NOT EXISTS rtg;

-- ================================================================
-- extensions
-- ================================================================
CREATE EXTENSION IF NOT EXISTS pgcrypto;      -- gen_random_uuid()
CREATE EXTENSION IF NOT EXISTS pg_trgm;       -- text search / fuzzy
CREATE EXTENSION IF NOT EXISTS btree_gist;    -- exclusion constraints

-- ================================================================
-- ENUM types  (replace the untyped char(N) codes of the legacy)
-- ================================================================

CREATE TYPE rtg.crane_id_t AS ENUM ('GOLD1', 'GOLD2', 'GOLD3');

CREATE TYPE rtg.block_code_t AS ENUM ('BOND1', 'BOND2', 'BOND3', 'G', 'T');
-- 'G' = ground staging; 'T' = truck; yard blocks are BOND1/2/3.
-- Additional blocks can be added with ALTER TYPE.

CREATE TYPE rtg.job_state_t AS ENUM (
    'PENDING',          -- inserted by UI, not yet sent to crane
    'SENT',             -- broadcast to crane, waiting for A3
    'ACKED',            -- A3 received
    'PICKED',           -- A2/03 received; container now with crane
    'PLACED',           -- A2/04 received; work done
    'CANCELLED',        -- cancelled via MessageCancel or manual admin action
    'FAILED'            -- protocol error, timeout, or manual fail
);

CREATE TYPE rtg.crane_connection_state_t AS ENUM (
    'CONNECTED',
    'DISCONNECTED',
    'DEGRADED'          -- e.g. packets arriving but checksum mismatches
);

CREATE TYPE rtg.role_t AS ENUM (
    'crane_operator',
    'yard_supervisor',
    'admin'
);

CREATE TYPE rtg.outbox_status_t AS ENUM (
    'PENDING',
    'SENT',
    'FAILED_PERMANENT'
);


-- ================================================================
-- Operator / auth
-- ================================================================

CREATE TABLE rtg.operator (
    id                     bigserial PRIMARY KEY,
    emp_id                 text NOT NULL UNIQUE,                         -- matches HR_Emp.EmpID in MSSQL
    login_name             text NOT NULL UNIQUE,                          -- matches HR_Emp.LoginName
    full_name              text NOT NULL,                                 -- Hebrew names supported
    role                   rtg.role_t NOT NULL DEFAULT 'crane_operator',
    pin_hash               text NOT NULL,                                 -- Argon2id(pin)
    pin_algo               text NOT NULL DEFAULT 'argon2id',
    pin_set_at             timestamptz NOT NULL DEFAULT now(),
    pin_must_change_after  timestamptz,                                   -- rotation policy deadline
    consecutive_failures   int NOT NULL DEFAULT 0,
    locked_until           timestamptz,                                   -- NULL = not locked
    active                 boolean NOT NULL DEFAULT true,
    created_at             timestamptz NOT NULL DEFAULT now(),
    updated_at             timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX operator_role_idx ON rtg.operator (role) WHERE active;
CREATE INDEX operator_login_name_trgm_idx ON rtg.operator USING GIN (login_name gin_trgm_ops);


CREATE TABLE rtg.session (
    id                 uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    operator_id        bigint NOT NULL REFERENCES rtg.operator(id),
    crane_id           rtg.crane_id_t NOT NULL,
    block_code         rtg.block_code_t NOT NULL,                -- the block the operator selected at login
    started_at         timestamptz NOT NULL DEFAULT now(),
    last_seen_at       timestamptz NOT NULL DEFAULT now(),
    ended_at           timestamptz,
    end_reason         text,                                     -- 'logout', 'timeout', 'forced'
    client_fingerprint text,                                     -- cabin PC host id
    created_at         timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX session_open_per_crane_idx ON rtg.session (crane_id) WHERE ended_at IS NULL;
CREATE INDEX session_operator_idx ON rtg.session (operator_id, started_at DESC);


CREATE TABLE rtg.login_log (
    id          bigserial PRIMARY KEY,
    ts          timestamptz NOT NULL DEFAULT now(),
    login_name  text NOT NULL,                                   -- even if operator lookup failed
    success     boolean NOT NULL,
    failure_reason text,                                         -- 'invalid_pin', 'locked', 'inactive', 'unknown_user'
    crane_id    rtg.crane_id_t,
    source_ip   inet,
    request_id  uuid
);

CREATE INDEX login_log_ts_idx ON rtg.login_log (ts DESC);
CREATE INDEX login_log_fail_idx ON rtg.login_log (login_name, ts DESC) WHERE success = false;


-- ================================================================
-- Yard
-- ================================================================

CREATE TABLE rtg.location (
    -- explicit structured coordinates instead of legacy's packed nvarchar(11)
    block_code   rtg.block_code_t NOT NULL,
    bay          text NOT NULL,                                  -- 3 chars legacy; flexible now
    row_letter   text NOT NULL,                                  -- 'A'..'F' or 'G'/'T'
    height       text NOT NULL,                                  -- 1..N
    container    char(11),                                       -- current container; NULL if empty
    crane_id     rtg.crane_id_t,                                 -- last crane to touch this slot
    active       boolean NOT NULL DEFAULT true,
    empty        boolean NOT NULL DEFAULT true,
    special      boolean NOT NULL DEFAULT false,
    terminal     text NOT NULL DEFAULT 'ILCXQ',
    legacy_location_code text GENERATED ALWAYS AS (               -- 5-char concat matching TB_Location.LocationCode
        bay || row_letter || height
    ) STORED,
    created_at   timestamptz NOT NULL DEFAULT now(),
    updated_at   timestamptz NOT NULL DEFAULT now(),
    PRIMARY KEY (block_code, bay, row_letter, height)
);

CREATE INDEX location_container_idx ON rtg.location (container) WHERE container IS NOT NULL;
CREATE INDEX location_empty_idx ON rtg.location (block_code, empty) WHERE active;
CREATE INDEX location_legacy_code_idx ON rtg.location (legacy_location_code);


CREATE TABLE rtg.recommended_location (
    id                        bigserial PRIMARY KEY,
    client_code               char(9) NOT NULL,
    container_length          char(2) NOT NULL,
    container_type_code       char(2) NOT NULL,
    handling_type_code        char(2) NOT NULL DEFAULT 'EM',
    carrier_code              char(9),
    container_capacity        char(2),
    classification_class_code char(3),
    recommended_block         rtg.block_code_t,
    recommended_bay           text,
    recommended_row           text,
    recommended_height        text,
    terminal                  text NOT NULL DEFAULT 'ILCXQ',
    updated_by                bigint REFERENCES rtg.operator(id),
    updated_at                timestamptz NOT NULL DEFAULT now(),
    UNIQUE (client_code, container_length, container_type_code, handling_type_code, carrier_code, terminal)
);


-- ================================================================
-- Crane live state
-- ================================================================

CREATE TABLE rtg.crane_status (
    crane_id               rtg.crane_id_t PRIMARY KEY,
    connection_state       rtg.crane_connection_state_t NOT NULL DEFAULT 'DISCONNECTED',
    last_packet_at         timestamptz,                               -- server time of last A1
    last_crane_time        text,                                       -- HHmmss as sent by crane
    -- position (from A1)
    hb_block_code          rtg.block_code_t,
    hb_bay                 text,
    hb_row                 text,
    hb_height              text,
    crane_status_code      text,                                       -- opaque 2-char code (see Q-20)
    gps_status_code        text,
    plc_code               text,
    container_length       text,                                       -- Len field
    twist_lock             text,                                       -- 1 char
    -- control plane
    active_session_id      uuid REFERENCES rtg.session(id),
    active_operator_id     bigint REFERENCES rtg.operator(id),
    needs_refresh_map      boolean NOT NULL DEFAULT false,              -- replaces TB_Parameters.RefreshMapRTG{n}
    container_pick_pending boolean NOT NULL DEFAULT false,              -- replaces ContainerPick{n}
    carrying_container     char(11),                                    -- replaces RG_Container single row
    carrying_operator_id   bigint REFERENCES rtg.operator(id),
    updated_at             timestamptz NOT NULL DEFAULT now()
);

-- Append-only history (replaces RG_A1_LOG trigger).  Partitioned for retention.
CREATE TABLE rtg.crane_status_history (
    id                bigserial,
    ts                timestamptz NOT NULL DEFAULT now(),
    crane_id          rtg.crane_id_t NOT NULL,
    hb_block_code     rtg.block_code_t,
    hb_bay            text,
    hb_row            text,
    hb_height         text,
    crane_status_code text,
    gps_status_code   text,
    plc_code          text,
    container_length  text,
    twist_lock        text,
    crane_time        text,
    PRIMARY KEY (id, ts)
) PARTITION BY RANGE (ts);

-- Create the first monthly partition.
CREATE TABLE rtg.crane_status_history_2026_04
    PARTITION OF rtg.crane_status_history
    FOR VALUES FROM ('2026-04-01') TO ('2026-05-01');

-- In production, use pg_partman or a scheduled job to pre-create partitions
-- monthly and drop partitions older than 90 days (or whatever retention).


-- ================================================================
-- Job queue (replaces RG_B3 with a proper state machine)
-- ================================================================

CREATE TABLE rtg.job (
    id               bigserial PRIMARY KEY,
    crane_id         rtg.crane_id_t NOT NULL,
    counter          int NOT NULL,                                      -- per-crane cycling 1..255; matches protocol
    state            rtg.job_state_t NOT NULL DEFAULT 'PENDING',
    state_reason     text,
    container        char(11),
    container_length char(2),
    -- source
    lift_block_code  rtg.block_code_t,
    lift_bay         text,
    lift_row         text,
    lift_height      text,
    -- destination
    place_block_code rtg.block_code_t,
    place_bay        text,
    place_row        text,
    place_height     text,
    truck_type       text,                                              -- 'גורר' / 'משאית' / NULL
    -- auth
    created_by       bigint NOT NULL REFERENCES rtg.operator(id),
    session_id       uuid REFERENCES rtg.session(id),
    -- client dedup (N11): cabins include an Idempotency-Key header when
    -- draining their offline queue; a retry with the same key returns the
    -- original job instead of creating a duplicate.
    client_idempotency_key text,
    -- wire protocol
    wire_message     bytea,                                             -- the actual bytes sent to crane (preserves FFFF + hex payload)
    wire_message_cancel bytea,
    -- state transition timestamps
    created_at       timestamptz NOT NULL DEFAULT now(),
    sent_at          timestamptz,
    acked_at         timestamptz,
    picked_at        timestamptz,
    placed_at        timestamptz,
    finished_at      timestamptz,
    cancelled_at     timestamptz,
    UNIQUE (crane_id, counter)                                          -- no race on MAX+1 like legacy
);

CREATE INDEX job_queue_pending_idx ON rtg.job (crane_id, created_at)
    WHERE state IN ('PENDING', 'SENT', 'ACKED', 'PICKED');

CREATE INDEX job_container_idx ON rtg.job (container);
CREATE INDEX job_operator_idx ON rtg.job (created_by, created_at DESC);

-- Sparse UNIQUE so duplicate POST /api/jobs with the same Idempotency-Key
-- collides and we return the original row via ON CONFLICT lookup.
CREATE UNIQUE INDEX job_client_idempotency_idx
    ON rtg.job (client_idempotency_key)
    WHERE client_idempotency_key IS NOT NULL;


-- ================================================================
-- Movement history (replaces RG_Shifting) — append-only, partitioned
-- ================================================================

CREATE TABLE rtg.movement (
    id             bigserial,
    ts             timestamptz NOT NULL DEFAULT now(),
    crane_id       rtg.crane_id_t NOT NULL,
    operator_id    bigint NOT NULL REFERENCES rtg.operator(id),
    block_code     rtg.block_code_t NOT NULL,
    container      char(11) NOT NULL,
    from_block     rtg.block_code_t,
    from_bay       text,
    from_row       text,
    from_height    text,
    to_block       rtg.block_code_t,
    to_bay         text,
    to_row         text,
    to_height      text,
    job_id         bigint REFERENCES rtg.job(id),
    PRIMARY KEY (id, ts)
) PARTITION BY RANGE (ts);

CREATE TABLE rtg.movement_2026_04
    PARTITION OF rtg.movement
    FOR VALUES FROM ('2026-04-01') TO ('2026-05-01');

CREATE INDEX movement_container_idx ON rtg.movement_2026_04 (container, ts DESC);
CREATE INDEX movement_crane_ts_idx  ON rtg.movement_2026_04 (crane_id, ts DESC);


-- ================================================================
-- Config (replaces the RTG-relevant parts of TB_Parameters)
-- ================================================================

CREATE TABLE rtg.config_kv (
    key        text PRIMARY KEY,
    value      jsonb NOT NULL,
    updated_by bigint REFERENCES rtg.operator(id),
    updated_at timestamptz NOT NULL DEFAULT now()
);

INSERT INTO rtg.config_kv (key, value) VALUES
    ('pin_rotation_days',                 '90'::jsonb),
    ('session_idle_timeout_minutes',      '30'::jsonb),
    ('listener_keepalive_probe_seconds',  '15'::jsonb),
    ('dual_write_to_mssql_enabled',       'true'::jsonb),
    ('offline_cache_seconds',             '3600'::jsonb);

-- NB: the RTG-specific RefreshMapRTG* / ContainerPick* flags are modelled
-- directly on rtg.crane_status (needs_refresh_map, container_pick_pending).
-- We do NOT re-create a single-row god-table.


-- ================================================================
-- Dangerous-goods column lookup (legacy RG_ColDG)
-- ================================================================

CREATE TABLE rtg.dangerous_goods_column (
    id          bigserial PRIMARY KEY,
    col_s       text NOT NULL,              -- 3-char bay number
    block_code  rtg.block_code_t NOT NULL,
    col_d       int NOT NULL,
    col_s1      int,
    col_s2      int,
    index_col   int NOT NULL,
    UNIQUE (col_s, block_code)
);


-- ================================================================
-- Audit log (security-sensitive trail)
-- ================================================================

CREATE TABLE rtg.audit_log (
    id                 bigserial PRIMARY KEY,
    ts                 timestamptz NOT NULL DEFAULT now(),
    actor_operator_id  bigint REFERENCES rtg.operator(id),
    actor_login_name   text,
    actor_role         rtg.role_t,
    action             text NOT NULL,
    target_entity      text,
    crane_id           rtg.crane_id_t,
    before             jsonb,
    after              jsonb,
    request_id         uuid,
    source_ip          inet
);

CREATE INDEX audit_log_ts_idx     ON rtg.audit_log (ts DESC);
CREATE INDEX audit_log_action_idx ON rtg.audit_log (action, ts DESC);
CREATE INDEX audit_log_target_idx ON rtg.audit_log (target_entity) WHERE target_entity IS NOT NULL;


-- ================================================================
-- Transactional outbox  (dual-write to MSSQL)
-- ================================================================

CREATE TABLE rtg.outbox (
    id              bigserial PRIMARY KEY,
    created_at      timestamptz NOT NULL DEFAULT now(),
    aggregate_type  text NOT NULL,      -- 'crane_status' | 'job' | 'movement' | 'operator' | 'session' | 'location'
    aggregate_id    text NOT NULL,
    event_type      text NOT NULL,      -- 'a1_received' | 'a2_pick' | 'a2_place' | 'a3_cancel' | 'job_created' | 'login' | ...
    payload         jsonb NOT NULL,
    status          rtg.outbox_status_t NOT NULL DEFAULT 'PENDING',
    attempts        int NOT NULL DEFAULT 0,
    last_attempt_at timestamptz,
    last_error      text,
    sent_at         timestamptz,
    -- idempotency: the worker computes a deterministic key from
    -- (aggregate_type, aggregate_id, event_type, payload-hash)
    idempotency_key text NOT NULL
);

CREATE INDEX outbox_pending_idx ON rtg.outbox (created_at) WHERE status = 'PENDING';
CREATE UNIQUE INDEX outbox_idem_idx ON rtg.outbox (idempotency_key);

-- NOTIFY on new outbox rows so the worker can react quickly
CREATE OR REPLACE FUNCTION rtg.outbox_notify() RETURNS trigger AS $$
BEGIN
    PERFORM pg_notify('rtg_outbox_new', NEW.id::text);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER outbox_notify_trg
    AFTER INSERT ON rtg.outbox
    FOR EACH ROW EXECUTE FUNCTION rtg.outbox_notify();


-- ================================================================
-- Server-push events to the UI (rtg-api LISTENs for these)
-- ================================================================

CREATE OR REPLACE FUNCTION rtg.notify_crane_change() RETURNS trigger AS $$
BEGIN
    PERFORM pg_notify('rtg_crane_' || NEW.crane_id::text, row_to_json(NEW)::text);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER crane_status_notify_trg
    AFTER UPDATE ON rtg.crane_status
    FOR EACH ROW
    WHEN (OLD IS DISTINCT FROM NEW)
    EXECUTE FUNCTION rtg.notify_crane_change();

CREATE OR REPLACE FUNCTION rtg.notify_job_change() RETURNS trigger AS $$
BEGIN
    PERFORM pg_notify('rtg_job_' || NEW.crane_id::text, row_to_json(NEW)::text);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER job_notify_trg
    AFTER INSERT OR UPDATE ON rtg.job
    FOR EACH ROW EXECUTE FUNCTION rtg.notify_job_change();


-- ================================================================
-- Views (replace legacy V_* views)
-- ================================================================

-- Current operator per crane (replaces V_RG_CurrentOperator)
CREATE OR REPLACE VIEW rtg.v_current_operator AS
    SELECT DISTINCT ON (crane_id)
           crane_id,
           operator_id,
           id        AS session_id,
           started_at
    FROM rtg.session
    WHERE ended_at IS NULL
    ORDER BY crane_id, started_at DESC;

-- Map pivot for the yard (replaces V_MapRTGBond1/2/3).  Block is a parameter
-- rather than per-view; the app passes it in.
CREATE OR REPLACE FUNCTION rtg.yard_pivot(p_block rtg.block_code_t)
    RETURNS TABLE (row_letter text, bay text, container_count int)
    LANGUAGE sql STABLE AS $$
    SELECT row_letter, bay, count(container)::int AS container_count
    FROM rtg.location
    WHERE block_code = p_block
      AND active
    GROUP BY row_letter, bay
    ORDER BY row_letter, bay;
$$;


-- ================================================================
-- updated_at trigger helper
-- ================================================================

CREATE OR REPLACE FUNCTION rtg.touch_updated_at() RETURNS trigger AS $$
BEGIN
    NEW.updated_at = now();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER operator_touch_trg
    BEFORE UPDATE ON rtg.operator
    FOR EACH ROW EXECUTE FUNCTION rtg.touch_updated_at();

CREATE TRIGGER location_touch_trg
    BEFORE UPDATE ON rtg.location
    FOR EACH ROW EXECUTE FUNCTION rtg.touch_updated_at();

CREATE TRIGGER crane_status_touch_trg
    BEFORE UPDATE ON rtg.crane_status
    FOR EACH ROW EXECUTE FUNCTION rtg.touch_updated_at();


-- ================================================================
-- Seed — the three cranes (always exist, 1 row each)
-- ================================================================

INSERT INTO rtg.crane_status (crane_id) VALUES
    ('GOLD1'), ('GOLD2'), ('GOLD3')
ON CONFLICT DO NOTHING;

-- ================================================================
-- End of schema.
-- ================================================================
