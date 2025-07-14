namespace si730ebu20221g120.API.Manufacturing.Domain.Model.Commands;

public record CreateBillOfMaterialsItemCommand(
    int BillOfMaterialsId,
    Guid ItemProductNumber,
    int BatchId,
    int RequiredQuantity,
    DateTime ScheduledStartAt,
    DateTime RequiredAt
);