using si730ebu20221g120.API.Inventories.Domain.Model.Commands;
using si730ebu20221g120.API.Inventories.Interfaces.REST.Resources;

namespace si730ebu20221g120.API.Inventories.Interfaces.REST.Transform;

public class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource (CreateProductResource resource)
    {
        return new CreateProductCommand(
            resource.Name,
            resource.ProductType,
            resource.MaxProductionQuantity
        );
    }
}