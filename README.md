# VehicleApp Advanced

Enterprise-grade vehicle management system built with .NET 9, CQRS, Clean Architecture, and Dapper. This is a complete rewrite of [vehicle_management_app](https://github.com/1244Matt1244/vehicle_management_app) demonstrating production-level patterns.

## Features

- Clean Architecture - Domain, Application, Infrastructure, API layers
- CQRS - MediatR for command/query separation
- Dapper - High-performance SQL for read/write operations
- FluentValidation - Automatic validation via MediatR pipeline
- SQL Server 2022 - Code-first with EF Core migrations
- Docker Compose - Full stack in one command
- CI/CD - GitHub Actions build + test pipeline
- 25 tests - 19 unit + 6 integration tests

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | .NET 9, ASP.NET Core |
| Architecture | Clean Architecture, CQRS (MediatR) |
| Data Access | Dapper (read), EF Core (write) |
| Validation | FluentValidation + Pipeline Behavior |
| Database | SQL Server 2022 |
| Testing | xUnit, Moq, FluentAssertions, WebApplicationFactory |
| DevOps | Docker, Docker Compose, GitHub Actions |

## Architecture

src/VehicleApp.Domain/ - Entities
src/VehicleApp.Application/ - CQRS handlers, validators, DTOs
src/VehicleApp.Infrastructure/ - Dapper, EF Core
src/VehicleApp.Api/ - Controllers, middleware
tests/VehicleApp.UnitTests/ - Validators, models
tests/VehicleApp.IntegrationTests/ - HTTP pipeline

### Design Patterns

- CQRS - Commands and Queries separated via MediatR
- Repository / DbConnectionFactory - Data access abstraction
- Pipeline Behavior - Cross-cutting concerns (validation)
- Dependency Injection - Extension methods per layer

## Quick Start

### Docker

git clone https://github.com/1244Matt1244/vehicle_management_app_advanced.git
cd vehicle_management_app_advanced
docker compose up --build

Wait 3-5 minutes for SQL Server to initialize.

Then open:
- Swagger: http://localhost:5000/swagger
- SQL Server: localhost:1433

### Local

dotnet restore VehicleApp.sln
dotnet run --project src/VehicleApp.Api

## API Endpoints

### Makes
- GET /api/makes - paginated list
- GET /api/makes/{id} - single make
- POST /api/makes - create
- PUT /api/makes/{id} - update
- DELETE /api/makes/{id} - delete

### Models
- GET /api/models?makeId=1 - filter by make
- GET /api/models/{id} - single model
- POST /api/models - create
- PUT /api/models/{id} - update
- DELETE /api/models/{id} - delete

### Example

curl -X POST http://localhost:5000/api/makes -H "Content-Type: application/json" -d "{\"name\":\"BMW\",\"abrv\":\"BMW\"}"

## Testing

dotnet test VehicleApp.sln

Coverage: 25 tests
- 19 unit tests (validators, pagination)
- 6 integration tests (HTTP pipeline, validation)

## License

MIT License - see LICENSE for details.

## Author

Matej Martinovic

- GitHub: https://github.com/1244Matt1244
- Email: matt123.3a@gmail.com
