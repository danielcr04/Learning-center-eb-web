using Microsoft.EntityFrameworkCore;
using si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;
using si730ebu20221g120.API.Manufacturing.Domain.Repositories;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace si730ebu20221g120.API.Manufacturing.Infrastructure.Persistence.EFC.Repositories;

public class BillOfMaterialsItemRepository(AppDbContext context) 
    : BaseRepository<BillOfMaterialsItem>(context), IBillOfMaterialsItemRepository 
{
    public async Task<bool> ExistsByItemProductNumberBatchIdAndBillOfMaterialsIdAsync(
        ProductNumber itemProductNumber, int batchId, int billOfMaterialsId)
    {
        return await Context.Set<BillOfMaterialsItem>()
            .AnyAsync(item => item.ItemProductNumber == itemProductNumber &&
                              item.BatchId == batchId &&
                              item.BillOfMaterialsId == billOfMaterialsId);
    }
}