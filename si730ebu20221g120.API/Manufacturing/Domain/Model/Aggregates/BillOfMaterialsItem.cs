using si730ebu20221g120.API.Manufacturing.Domain.Model.Commands;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

namespace si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;


/// <summary>
///     Aggregate root representing a bill of materials item in the manufacturing bounded context.
/// </summary>
/// <remarks>
///     Author: Daniel Crispin Ramos
/// </remarks>
public partial class BillOfMaterialsItem
{
    public int Id { get; }
    public int BillOfMaterialsId { get; private set; }
    public ProductNumber ItemProductNumber { get; private set; }
    public int BatchId { get; private set; }
    public int RequiredQuantity { get; private set; }
    public DateTime ScheduledStartAt { get; private set; }
    public DateTime RequiredAt { get; private set; }

    /// <summary>
    ///     Creates a new bill of materials item with the specified details.
    /// </summary>
    /// <param name="billOfMaterialsId">The bill of materials identifier</param>
    /// <param name="itemProductNumber">The product number for the item</param>
    /// <param name="batchId">The batch identifier</param>
    /// <param name="requiredQuantity">The required quantity</param>
    /// <param name="scheduledStartAt">The scheduled start date</param>
    /// <param name="requiredAt">The required date</param>
    public BillOfMaterialsItem(
        int billOfMaterialsId,
        ProductNumber itemProductNumber,
        int batchId,
        int requiredQuantity,
        DateTime scheduledStartAt,
        DateTime requiredAt)
    {
        BillOfMaterialsId = billOfMaterialsId;
        ItemProductNumber = itemProductNumber;
        BatchId = batchId;
        RequiredQuantity = requiredQuantity;
        ScheduledStartAt = scheduledStartAt;
        RequiredAt = requiredAt;
    }

    /// <summary>
    ///     Creates a new bill of materials item from a command.
    /// </summary>
    /// <param name="command">The create command</param>
    public BillOfMaterialsItem(CreateBillOfMaterialsItemCommand command)
    {
        BillOfMaterialsId = command.BillOfMaterialsId;
        ItemProductNumber = new ProductNumber(command.ItemProductNumber);
        BatchId = command.BatchId;
        RequiredQuantity = command.RequiredQuantity;
        ScheduledStartAt = command.ScheduledStartAt;
        RequiredAt = command.RequiredAt;
    }

    /// <summary>
    ///     Protected constructor for Entity Framework.
    /// </summary>
    protected BillOfMaterialsItem() { }
}