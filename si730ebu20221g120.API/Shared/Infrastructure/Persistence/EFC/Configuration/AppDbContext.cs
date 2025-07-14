using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

namespace si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // **Product Bounded Context**
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd(); // ID autogenerado
        builder.Entity<Product>().Property(p => p.Id).IsRequired(); // ID requerido
        builder.Entity<Product>().Property(p => p.ProductNumber).IsRequired(); // ProductNumber obligatorio
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(60); // Name obligatorio con longitud máxima
        builder.Entity<Product>().Property(p => p.ProductType).IsRequired(); // ProductType obligatorio
        builder.Entity<Product>().Property(p => p.CurrentProductionQuantity).IsRequired().HasDefaultValue(0); // CurrentProductionQuantity obligatorio con valor por defecto
        builder.Entity<Product>().Property(p => p.MaxProductionQuantity).IsRequired(); // MaxProductionQuantity obligatorio

        builder.Entity<Product>()
            .Property(p => p.ProductNumber)
            .HasConversion(
                v => v.Value.ToString(),
                v => new ProductNumber(Guid.Parse(v))
            );
        
        // **BillOfMaterialsItem Bounded Context**
        builder.Entity<BillOfMaterialsItem>().HasKey(bom => bom.Id);
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.Id).ValueGeneratedOnAdd(); // ID autogenerado
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.Id).IsRequired(); // ID requerido
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.ItemProductNumber).IsRequired(); // ItemProductNumber obligatorio
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.BatchId).IsRequired(); // BatchId obligatorio
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.RequiredQuantity).IsRequired(); // RequiredQuantity obligatorio
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.ScheduledStartAt).IsRequired(); // ScheduledStartAt obligatorio
        builder.Entity<BillOfMaterialsItem>().Property(bom => bom.RequiredAt).IsRequired(); // RequiredAt obligatorio

        builder.Entity<BillOfMaterialsItem>()
            .Property(bom => bom.ItemProductNumber)
            .HasConversion(
                v => v.Value.ToString(),
                v => new ProductNumber(Guid.Parse(v))
            );
        
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseNamingConvention();
    }
}