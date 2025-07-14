using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Inventories.Domain.Model.Queries;
using si730ebu20221g120.API.Inventories.Domain.Repositories;
using si730ebu20221g120.API.Inventories.Domain.Services;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

namespace si730ebu20221g120.API.Inventories.Application.Internal.QueryServices;

public class ProductQueryService (IProductRepository productRepository) : IProductQueryService
{
    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.FindByIdAsync(query.Id);
    } 
}