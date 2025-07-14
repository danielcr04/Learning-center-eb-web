using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

namespace si730ebu20221g120.API.Inventories.Interfaces.ACL;

public interface IInventoriesContextFacade
{
    Task<bool> ExistsByProductNumberAsync(ProductNumber productNumber);
    Task<bool> CanIncrementProductionQuantityAsync(ProductNumber productNumber, int quantity);
    Task IncrementProductionQuantityAsync(ProductNumber productNumber, int quantity);
}