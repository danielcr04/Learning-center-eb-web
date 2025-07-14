namespace si730ebu20221g120.API.Inventories.Domain.Model.Queries;

/// <summary>
///     Represents a query to get a product by id.
/// </summary>
/// <param name="Id">
///     The id of the product to get
/// </param>
public record GetProductByIdQuery (int Id);