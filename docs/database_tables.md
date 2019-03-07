# Makehouse database tables

## PartsDB tables

### Parts table
The `parts` table should contain at least the following columns:

| Column | PK/FK |
| ------ | ----- |
| stock number | PK |
| current stock level | |
| description | |
| manufacturer p/n | |
| preferred supplier | FK - `suppliers` |
| supplier 1 | FK - `suppliers` |
| supplier 1 p/n | |
| supplier 1 price | |
| supplier 1 min qty | |
| supplier 2 | FK  - `suppliers` |
| supplier 2 p/n | |
| supplier 2 price | |
| supplier 2 min qty | |

### Suppliers table
The `suppliers` table should contain at least the following columns:

| Column | PK/FK |
| ------ | ----- |
| supplier number | PK |
| supplier name | |
| supplier contact | |
| supplier website | |
