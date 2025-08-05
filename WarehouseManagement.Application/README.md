# Application Layer - Reference Data Management

This layer implements Application Services for managing reference data (Resources, Units of Measure, and Clients) using CQRS pattern with MediatR.

## Implemented Features

### Resources Management
- **Commands**: CreateResource, UpdateResource, DeleteResource, ArchiveResource
- **Queries**: GetResources, GetResourceById
- **Validation**: Name uniqueness, usage validation before deletion
- **Business Rules**: Archive instead of delete when used in documents

### Units of Measure Management
- **Commands**: CreateUnitOfMeasure, UpdateUnitOfMeasure, DeleteUnitOfMeasure, ArchiveUnitOfMeasure
- **Queries**: GetUnitsOfMeasure, GetUnitOfMeasureById
- **Validation**: Name uniqueness, usage validation before deletion
- **Business Rules**: Archive instead of delete when used in documents

### Clients Management
- **Commands**: CreateClient, UpdateClient, DeleteClient, ArchiveClient
- **Queries**: GetClients, GetClientById
- **Validation**: Name uniqueness, usage validation before deletion
- **Business Rules**: Archive instead of delete when used in shipment documents

## Architecture Components

### DTOs
- ResourceDto, CreateResourceDto, UpdateResourceDto
- UnitOfMeasureDto, CreateUnitOfMeasureDto, UpdateUnitOfMeasureDto
- ClientDto, CreateClientDto, UpdateClientDto

### Commands & Handlers
- CQRS pattern implementation using MediatR
- Separate command handlers for each operation
- Business rule validation in handlers

### Queries & Handlers
- Read operations separated from write operations
- Support for including/excluding archived entities

### Validators
- FluentValidation for input validation
- Separate validators for Create and Update operations
- Field length and required field validation

### Mapping
- AutoMapper profiles for entity-DTO mapping
- Automatic mapping configuration

### Dependency Injection
- Extension method for service registration
- AutoMapper, MediatR, and FluentValidation configuration

## Business Rules Implemented

1. **Uniqueness Validation**: Entity names must be unique within their type
2. **Usage Validation**: Entities used in documents cannot be deleted, only archived
3. **Status Management**: Entities can be active or archived
4. **Data Integrity**: Proper validation at domain and application levels

## Error Handling

- **BusinessRuleViolationException**: For general business rule violations
- **DuplicateNameException**: For name uniqueness violations
- **EntityInUseException**: When trying to delete used entities
- **InvalidEntityStatusException**: For invalid status operations