# Copilot Instructions for SatisfactoryTree

## Coding Standards

### Variable Declarations

**Always use explicit type declarations instead of `var`.**

✅ **Good** - Explicit type:
```csharp
ExportedItem? item = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "IronPlate");
List<string> ingredients = new List<string>();
Dictionary<string, double> quantities = new Dictionary<string, double>();
```

❌ **Bad** - Using var:
```csharp
var item = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "IronPlate");
var ingredients = new List<string>();
var quantities = new Dictionary<string, double>();
```

### Rationale

- Explicit types improve code readability and make the intent clearer
- Helps prevent type confusion, especially with nullable types
- Makes code reviews easier by showing exactly what type is being used

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
