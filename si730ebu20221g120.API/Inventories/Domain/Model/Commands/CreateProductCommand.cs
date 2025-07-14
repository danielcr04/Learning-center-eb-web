namespace si730ebu20221g120.API.Inventories.Domain.Model.Commands;

public record CreateProductCommand (
    string Name,
    string ProductType,
    int MaxProductionQuantity
)
{
    
}