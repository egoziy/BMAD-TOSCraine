# db/scripts

Helper scripts for local DB management. The actual scripts land in **Story 1.2**:

| Script | Purpose | Story |
|---|---|---|
| `schema_dump.sh` | `pg_dump --schema-only` for CI drift check | 1.2 |
| `reset_dev.sh` | Reset local dev DB (drop + create + migrate + seed) | 1.2 |
| `reconcile_drift.sh` | Compare PG `rtg.shifting` vs MSSQL `RG_Shifting` | 6.6 |

## dbmate installation

- **Windows**: `scoop install dbmate` or `choco install dbmate`
- **macOS**: `brew install dbmate`
- **Linux**: see [github.com/amacneil/dbmate#installation](https://github.com/amacneil/dbmate#installation)

## Quick reference

```bash
# Apply all pending migrations
dbmate up

# Roll back the most recent migration
dbmate rollback

# Generate a new migration scaffold
dbmate new <snake_case_name>

# Drop & recreate the database (dev only)
dbmate drop && dbmate create && dbmate up
```

Every migration file in `db/migrations/` must include both `-- migrate:up` and `-- migrate:down` sections that work correctly (`dbmate down` then `up` returns to identical state). Schema lives in PG `rtg` and `mssql_mirror` schemas per architecture §5.4.3.
