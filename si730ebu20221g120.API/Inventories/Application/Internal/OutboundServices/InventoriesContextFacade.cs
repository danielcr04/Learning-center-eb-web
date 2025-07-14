using si730ebu20221g120.API.Inventories.Domain.Repositories;
using si730ebu20221g120.API.Inventories.Interfaces.ACL;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Domain.Repositories;

namespace si730ebu20221g120.API.Inventories.Application.Internal.OutboundServices;

public class InventoriesContextFacade(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : IInventoriesContextFacade
{
    public async Task<bool> ExistsByProductNumberAsync(ProductNumber productNumber)
    {
        return await productRepository.ExistsByProductNumberAsync(productNumber);
    }

    public async Task<bool> CanIncrementProductionQuantityAsync(ProductNumber productNumber, int quantity)
    {
        var product = await productRepository.FindByProductNumberAsync(productNumber);
        if (product == null) return false;
        
        return product.CurrentProductionQuantity + quantity <= product.MaxProductionQuantity;
    }

    public async Task IncrementProductionQuantityAsync(ProductNumber productNumber, int quantity)
    {
        var product = await productRepository.FindByProductNumberAsync(productNumber);
        if (product == null) 
            throw new InvalidOperationException("Product not found");
        
        product.IncrementProductionQuantity(quantity);
        await unitOfWork.CompleteAsync();
    }
}