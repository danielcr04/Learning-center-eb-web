using si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Domain.Repositories;

namespace si730ebu20221g120.API.Manufacturing.Domain.Repositories;

public interface IBillOfMaterialsItemRepository : IBaseRepository<BillOfMaterialsItem>
{
    Task<bool> ExistsByItemProductNumberBatchIdAndBillOfMaterialsIdAsync(
        ProductNumber itemProductNumber, int batchId, int billOfMaterialsId);
}