using si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;
using si730ebu20221g120.API.Manufacturing.Interfaces.REST.Resources;

namespace si730ebu20221g120.API.Manufacturing.Interfaces.REST.Transform;

public class BillOfMaterialsItemResourceFromEntityAssembler
{
    public static BillOfMaterialsItemResource ToResourceFromEntity(BillOfMaterialsItem entity)
    {
        return new BillOfMaterialsItemResource(
            entity.Id,
            entity.BillOfMaterialsId,
            entity.ItemProductNumber.Value,
            entity.BatchId,
            entity.RequiredQuantity,
            entity.ScheduledStartAt,
            entity.RequiredAt
        );
    }
}