using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Inventories.Domain.Model.Queries;

namespace si730ebu20221g120.API.Inventories.Domain.Services;

/// <summary>
///     Represents the product query service.
/// </summary>
public interface IProductQueryService
{
    /// <summary>
    ///     Handles the get product by id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetProductByIdQuery" /> query to handle.
    /// </param>
    /// <returns>
    ///     The <see cref="Product" /> entity.
    /// </returns>
    Task<Product?> Handle(GetProductByIdQuery query);
}