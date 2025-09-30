# Copilot Instructions for SatisfactoryTree

## Dependency Injection and Interface Usage

### When to Use Interfaces

Only use interfaces and dependency injection for **external dependencies** such as:

- External API clients (HTTP clients, database connections, etc.)
- Third-party services
- System resources (file system, network, time, etc.)
- Framework-level services that benefit from mocking in tests

### When NOT to Use Interfaces

Do **not** create interfaces for internal application services such as:

- Domain services (e.g., `PlanService`, `CalculatorService`)
- Application-specific business logic services
- UI component state management
- Internal data transformation services

### Rationale

- Internal services can be tested directly without mocking
- Interfaces add complexity without providing value for internal services
- Direct dependencies are clearer and easier to understand
- Modern testing frameworks allow testing concrete classes effectively

### Example

✅ **Good** - Interface for external dependency:
```csharp
public interface IHttpClient { }
public interface IFileSystem { }
public interface ITimeProvider { }
```

❌ **Bad** - Interface for internal service:
```csharp
public interface IPlanService { }  // Internal domain service - don't abstract
public interface ICalculatorService { }  // Internal logic - don't abstract
```

### Current Architecture

The application uses dependency injection for:
- `IFactoryItemDisplayService` - Interface for display logic (to allow multiple display strategies)
- Concrete classes for domain services (`PlanService`, `Calculator`, etc.)
