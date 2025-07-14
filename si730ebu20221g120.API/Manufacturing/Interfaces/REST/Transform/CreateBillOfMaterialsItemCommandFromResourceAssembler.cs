using si730ebu20221g120.API.Manufacturing.Domain.Model.Commands;
using si730ebu20221g120.API.Manufacturing.Interfaces.REST.Resources;

namespace si730ebu20221g120.API.Manufacturing.Interfaces.REST.Transform;

public class CreateBillOfMaterialsItemCommandFromResourceAssembler
{
    public static CreateBillOfMaterialsItemCommand ToCommandFromResource(CreateBillOfMaterialsItemResource resource)
    {
        return new CreateBillOfMaterialsItemCommand(
            resource.BillOfMaterialsId,
            resource.ItemProductNumber,
            resource.BatchId,
            resource.RequiredQuantity,
            resource.ScheduledStartAt,
            resource.RequiredAt
        );
    }
}