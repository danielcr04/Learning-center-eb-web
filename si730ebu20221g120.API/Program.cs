using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using si730ebu20221g120.API.Inventories.Application.Internal.CommandServices;
using si730ebu20221g120.API.Inventories.Application.Internal.OutboundServices;
using si730ebu20221g120.API.Inventories.Application.Internal.QueryServices;
using si730ebu20221g120.API.Inventories.Domain.Repositories;
using si730ebu20221g120.API.Inventories.Domain.Services;
using si730ebu20221g120.API.Inventories.Infrastructure.Persistence.EFC.Repositories;
using si730ebu20221g120.API.Inventories.Interfaces.ACL;
using si730ebu20221g120.API.Manufacturing.Application.Internal.CommandServices;
using si730ebu20221g120.API.Manufacturing.Application.Internal.OutboundServices;
using si730ebu20221g120.API.Manufacturing.Domain.Repositories;
using si730ebu20221g120.API.Manufacturing.Domain.Services;
using si730ebu20221g120.API.Manufacturing.Infrastructure.Persistence.EFC.Repositories;
using si730ebu20221g120.API.Shared.Domain.Repositories;
using si730ebu20221g120.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add Configuration for Routing: Use lowercase URLs for routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    // Add custom route naming convention (kebab-case for route URLs)
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

// Add Database Connection and configure MySQL with proper logging and validation
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString is null)
    throw new InvalidOperationException("Connection string is null");

// Configure Database Context and enable detailed logging
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)  // Log SQL queries
        .EnableDetailedErrors() // Enable detailed error messages for debugging
        .EnableSensitiveDataLogging(); // Enable sensitive data logging (e.g., parameter values in queries)
});

// Configure Swagger for API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EB Web Applications",
        Version = "v1",
        Description = "RESTful API",
    });
});

// Dependency Injection Configuration

// Shared Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Inventories Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
// Registro de dependencias personalizadas
builder.Services.AddScoped<IInventoriesContextFacade, InventoriesContextFacade>();
builder.Services.AddScoped<ExternalInventoriesService>();


// BillOfMaterialsItem Bounded Context Configuration
builder.Services.AddScoped<IBillOfMaterialsItemCommandService, BillOfMaterialsItemCommandService>();
builder.Services.AddScoped<IBillOfMaterialsItemRepository, BillOfMaterialsItemRepository>();


var app = builder.Build();

// Verify Database Objects are Created (ensure the database schema is in place)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Ensures that the database is created (but doesn't apply migrations)
}

// Enable Swagger UI and API Documentation
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "EB Web Applications API v1");
    options.RoutePrefix = string.Empty; // Set Swagger UI at the root of the application
});

// Enable HTTPS redirection for secure communication
app.UseHttpsRedirection();

// Enable routing and map controllers
app.MapControllers();

app.Run();
