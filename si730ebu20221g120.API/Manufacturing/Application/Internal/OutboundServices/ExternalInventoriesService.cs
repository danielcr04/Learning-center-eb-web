using si730ebu20221g120.API.Inventories.Interfaces.ACL;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

namespace si730ebu20221g120.API.Manufacturing.Application.Internal.OutboundServices;

public class ExternalInventoriesService(IInventoriesContextFacade inventoriesContextFacade)
{
    public async Task<bool> ExistsByProductNumberAsync(ProductNumber productNumber)
    {
        return await inventoriesContextFacade.ExistsByProductNumberAsync(productNumber);
    }

    public async Task<bool> CanIncrementProductionQuantityAsync(ProductNumber productNumber, int quantity)
    {
        return await inventoriesContextFacade.CanIncrementProductionQuantityAsync(productNumber, quantity);
    }

    public async Task IncrementProductionQuantityAsync(ProductNumber productNumber, int quantity)
    {
        await inventoriesContextFacade.IncrementProductionQuantityAsync(productNumber, quantity);
    }
}