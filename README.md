# Warehouse Management System

Система управления складом, построенная на ASP.NET Core с использованием Clean Architecture.

## Архитектура проекта

- **WarehouseManagement.Domain** - Доменный слой с бизнес-логикой
- **WarehouseManagement.Application** - Слой приложения с use cases
- **WarehouseManagement.Infrastructure** - Инфраструктурный слой с репозиториями и EF Core
- **WarehouseManagement.API** - Web API слой

## Технологии

- ASP.NET Core 9.0
- Entity Framework Core 9.0
- SQL Server / PostgreSQL
- AutoMapper
- FluentValidation
- MediatR

## Настройка базы данных

Система поддерживает два типа баз данных:

### SQL Server (по умолчанию)
```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WarehouseManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### PostgreSQL
```json
{
  "DatabaseProvider": "PostgreSQL",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=WarehouseManagementDb;Username=postgres;Password=your_password"
  }
}
```

## Запуск приложения

```bash
dotnet run --project WarehouseManagement.API
```

База данных будет создана автоматически при первом запуске.

## Структура проекта

```
WarehouseManagement/
├── WarehouseManagement.Domain/
│   ├── Entities/
│   ├── Enums/
│   ├── Interfaces/
│   └── Services/
├── WarehouseManagement.Application/
│   ├── Commands/
│   ├── Queries/
│   └── DTOs/
├── WarehouseManagement.Infrastructure/
│   ├── Data/
│   ├── Repositories/
│   └── Extensions/
└── WarehouseManagement.API/
```