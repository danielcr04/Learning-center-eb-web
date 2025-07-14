namespace si730ebu20221g120.API.Manufacturing.Interfaces.REST.Resources;

public record BillOfMaterialsItemResource(
    int Id,
    int BillOfMaterialsId,
    Guid ItemProductNumber,
    int BatchId,
    int RequiredQuantity,
    DateTime ScheduledStartAt,
    DateTime RequiredAt
    );
