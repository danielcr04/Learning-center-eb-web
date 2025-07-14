using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Domain.Repositories;

namespace si730ebu20221g120.API.Inventories.Domain.Repositories;

/// <summary>
///     Contract for the Product repository with domain-specific operations.
/// </summary>
/// <remarks>
///     Defines operations for querying products in the inventory bounded context.
///     Author: Daniel Crispin Ramos
/// </remarks>
public interface IProductRepository : IBaseRepository<Product>
{
    Task<bool> ExistsByProductNumberAsync(ProductNumber productNumber);
    Task<bool> ExistsByProductNameAsync(string productName);
    Task<Product?> FindByProductNameAsync(string productName);
    Task<Product?> FindByProductNumberAsync(ProductNumber productNumber);
}