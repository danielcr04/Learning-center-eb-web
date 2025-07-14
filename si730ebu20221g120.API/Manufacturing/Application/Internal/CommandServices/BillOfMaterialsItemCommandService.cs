using si730ebu20221g120.API.Inventories.Application.Internal.OutboundServices;
using si730ebu20221g120.API.Manufacturing.Application.Internal.OutboundServices;
using si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;
using si730ebu20221g120.API.Manufacturing.Domain.Model.Commands;
using si730ebu20221g120.API.Manufacturing.Domain.Repositories;
using si730ebu20221g120.API.Manufacturing.Domain.Services;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Domain.Repositories;

namespace si730ebu20221g120.API.Manufacturing.Application.Internal.CommandServices;

public class BillOfMaterialsItemCommandService(
    IBillOfMaterialsItemRepository billOfMaterialsItemRepository,
    ExternalInventoriesService externalInventoriesService,
    IUnitOfWork unitOfWork
) : IBillOfMaterialsItemCommandService
{
    public async Task<BillOfMaterialsItem> Handle(CreateBillOfMaterialsItemCommand command)
    {
        // Validar fechas
        if (command.RequiredAt > DateTime.Now)
            throw new ArgumentException("RequiredAt cannot be greater than current date");
        
        if (command.ScheduledStartAt < command.RequiredAt.AddDays(30))
            throw new ArgumentException("ScheduledStartAt must be at least 30 days greater than RequiredAt");
        
        var itemProductNumber = new ProductNumber(command.ItemProductNumber);
        
        // Verificar que el producto existe
        if (!await externalInventoriesService.ExistsByProductNumberAsync(itemProductNumber))
            throw new InvalidOperationException("Referenced product does not exist");
        
        // Verificar unicidad de la combinación
        if (await billOfMaterialsItemRepository.ExistsByItemProductNumberBatchIdAndBillOfMaterialsIdAsync(
            itemProductNumber, command.BatchId, command.BillOfMaterialsId))
            throw new InvalidOperationException("Bill of materials item with this combination already exists");
        
        // Verificar capacidad del producto
        if (!await externalInventoriesService.CanIncrementProductionQuantityAsync(itemProductNumber, command.RequiredQuantity))
            throw new InvalidOperationException("Cannot create bill of materials item: would exceed product capacity");
        
        // Crear el item
        var billOfMaterialsItem = new BillOfMaterialsItem(command);
        
        // Incrementar la cantidad de producción del producto
        await externalInventoriesService.IncrementProductionQuantityAsync(itemProductNumber, command.RequiredQuantity);
        
        // Persistir
        await billOfMaterialsItemRepository.AddAsync(billOfMaterialsItem);
        await unitOfWork.CompleteAsync();
        
        return billOfMaterialsItem;
    }
}