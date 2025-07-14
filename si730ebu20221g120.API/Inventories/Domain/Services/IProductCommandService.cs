using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Inventories.Domain.Model.Commands;

namespace si730ebu20221g120.API.Inventories.Domain.Services;

/// <summary>
///     Domain service responsible for handling product creation.
/// </summary>
/// <remarks>
///     Applies business rules before creating a new product.
///     Author: Daniel Crispin Ramos
/// </remarks>
public interface IProductCommandService
{
    /// <summary>
    ///     Handles the creation of a product using the provided command.
    /// </summary>
    /// <param name="command">Command object containing the data to create the product</param>
    /// <returns>The newly created product if successful; otherwise, null</returns>
    Task<Product?> Handle(CreateProductCommand command);
}