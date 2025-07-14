using Microsoft.EntityFrameworkCore;
using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Inventories.Domain.Repositories;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace si730ebu20221g120.API.Inventories.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework Core implementation of the purchase order repository.
/// </summary>
/// <remarks>
///     Provides access to purchase order data in the sales bounded context.
///     Author: YourNameHere
/// </remarks>
public class ProductRepository(AppDbContext context) 
    : BaseRepository<Product>(context), IProductRepository
{
    public async Task<bool> ExistsByProductNumberAsync(ProductNumber productNumber)
    {
        return await Context.Set<Product>()
            .AnyAsync(product => product.ProductNumber == productNumber);
    }
    public async Task<bool> ExistsByProductNameAsync(string productName)
    {
        return await Context.Set<Product>()
            .AnyAsync(product => product.Name == productName);
    }
    
    public async Task<Product?> FindByProductNameAsync(string productName)
    {
        return await Context.Set<Product>()
            .FirstOrDefaultAsync(product => product.Name == productName);
    }
    
    public async Task<Product?> FindByProductNumberAsync(ProductNumber productNumber)
    {
        return await Context.Set<Product>()
            .FirstOrDefaultAsync(product => product.ProductNumber == productNumber);
    }
    
}