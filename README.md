# .NET 10 Web API Template
## Generic Repository & Unit of Work Pattern

A clean, reusable template for building .NET 10 Web APIs using the Generic Repository and Unit of Work patterns with Entity Framework Core.

## Features

- **.NET 10** with minimal hosting model
- **Generic Repository Pattern** - Reusable CRUD operations for any entity
- **Unit of Work Pattern** - Coordinated database transactions across repositories
- **Entity Framework Core 10** with SQLite (easily swappable)
- **Swagger/OpenAPI** documentation
- **Clean Architecture** - Separation of concerns across layers

## Project Structure

```
├── API/                    # Web API layer (controllers, configuration)
├── Logic/                  # Business logic layer
│   ├── Repository/         # Generic and specific repositories
│   └── UnitOfWork/         # Unit of Work implementation
├── Data/                   # Data access layer
│   └── Context/Entities/   # DbContext and entity models
├── Models/                 # DTOs and view models
└── Auth/                   # Authentication (placeholder)
```

## Quick Start

```bash
# Clone the repository
git clone <repo-url>
cd .net6-webapi-generic-repository-unit-of-work-template

# Run the API
dotnet run --project API

# Navigate to Swagger UI
# http://localhost:5278/swagger
```

## Usage

### Generic Repository

Access any entity through the Unit of Work:

```csharp
public class MyService(IUnitOfWork unitOfWork)
{
    private readonly IGenericRepository<Product> _products = unitOfWork.Repository<Product>();

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await _products.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        return await _products.GetByIdAsync(id);
    }

    public async Task CreateAsync(Product product)
    {
        await _products.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
    }
}
```

### Creating a Specific Repository

For complex queries, create a specific repository:

```csharp
// Interface
public interface IProductRepository
{
    Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync();
}

// Implementation
public class ProductRepository(IUnitOfWork unitOfWork) : IProductRepository
{
    private readonly IGenericRepository<Product> _repo = unitOfWork.Repository<Product>();

    public async Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync()
    {
        var products = await _repo.FindAllAsync(p => p.IsActive);
        return products.Select(ProductDto.FromEntity).ToList();
    }
}

// Register in Program.cs
builder.Services.AddTransient<IProductRepository, ProductRepository>();
```

### Adding New Entities

1. Create entity in `Data/Context/Entities/`:
```csharp
public class Product
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

2. Add DbSet in `ApplicationDbContext.cs`:
```csharp
public virtual DbSet<Product> Products { get; set; }
```

3. Create DTO in `Models/`:
```csharp
public class ProductDto : BaseDto
{
    public decimal Price { get; set; }

    public static ProductDto FromEntity(Product entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Price = entity.Price
    };
}
```

## Available Repository Methods

| Method | Description |
|--------|-------------|
| `GetAllAsync()` | Get all entities |
| `GetByIdAsync(id)` | Get entity by primary key |
| `FindAsync(predicate)` | Find first entity matching condition |
| `FindAllAsync(predicate)` | Find all entities matching condition |
| `Query()` | Get IQueryable for complex queries |
| `AddAsync(entity)` | Add new entity |
| `UpdateAsync(entity)` | Update existing entity |
| `DeleteAsync(entity)` | Delete entity |
| `ExistsAsync(predicate)` | Check if any entity matches condition |
| `CountAsync()` | Count all entities |

## Configuration

### Database Connection

Update `API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "dbContext": "Data Source=..\\Data\\demo.db"
  }
}
```

### CORS

Configured in `Program.cs` for localhost development:
- `http://localhost:4200` (Angular)
- `http://localhost:3000` (React)

## License

MIT
