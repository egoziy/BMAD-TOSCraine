# Fixes Punch List

This directory holds paired DEV/QA briefs for outstanding defects in the TOS bridge (Rtg.Listener + Rtg.Api), the cabin Flutter app, and the PG↔MSSQL data path. Each subdirectory pairs a developer brief (`DEV.md`) with a QA brief (`QA.md`). Severity is split between CRITICAL (data-loss, divergence, or auth class issues that must land before any real-crane cutover) and SHOULD FIX (operational hazards that degrade silently in production). Read the README, pick a fix, then work both files together — the QA scenarios are the contract the dev brief must satisfy.

| ID | Severity | Summary | Link |
|----|----------|---------|------|
| 01 | CRITICAL | Outbox loses cross-DB atomicity; MSSQL replay double-writes | [01-outbox-crash-safety](01-outbox-crash-safety/DEV.md) |
| 02 | CRITICAL | Outbox `SKIP LOCKED` breaks per-aggregate ordering under concurrent workers | [02-outbox-ordering](02-outbox-ordering/DEV.md) |
| 03 | CRITICAL | One-way MSSQL→PG importer clobbers outbox-mirrored writes; no reconciliation | [03-pg-mssql-reconciliation](03-pg-mssql-reconciliation/DEV.md) |
| 04 | CRITICAL | Cabin block partition unsafe — cross-block A1 silently lost | [04-cabin-block-partition-guard](04-cabin-block-partition-guard/DEV.md) |
| 05 | CRITICAL | Anonymous operator list enables PIN enumeration | [05-pin-auth-enumeration](05-pin-auth-enumeration/DEV.md) |
| 06 | SHOULD FIX | Pick with null op-id silently skips RG_Container insert; billing undercounts | [06-pick-null-empid-warning](06-pick-null-empid-warning/DEV.md) |
| 07 | SHOULD FIX | `FAILED_PERMANENT` rows have no alerting; MSSQL diverges silently | [07-failed-permanent-alerting](07-failed-permanent-alerting/DEV.md) |
| 10 | SHOULD FIX | Cabin write queue treats 401 as permanent; queued edits lost on JWT expiry | [10-write-queue-401-handling](10-write-queue-401-handling/DEV.md) |
| 12 | SHOULD FIX | Protocol constants chesimu-pinned; inbound checksum validation off | [12-real-crane-checksum-validation](12-real-crane-checksum-validation/DEV.md) |
| 13 | SHOULD FIX | `NormalizeBlock` silently maps unknown block names to BOND1 | [13-normalize-block-strict](13-normalize-block-strict/DEV.md) |
