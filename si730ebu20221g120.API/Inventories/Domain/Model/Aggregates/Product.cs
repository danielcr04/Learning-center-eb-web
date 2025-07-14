using si730ebu20221g120.API.Inventories.Domain.Model.Commands;
using si730ebu20221g120.API.Inventories.Domain.Model.ValueObjects;
using si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

namespace si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;



/// <summary>
///     Aggregate root representing a product in the inventories bounded context.
/// </summary>
///  since 1.0.0
/// <remarks>
///     Author: Daniel Crispin Ramos
/// </remarks>
public partial class Product
{
    public int Id { get; }
    public ProductNumber ProductNumber { get; private set; }
    public string Name { get; private set; }
    public EProductType ProductType { get; private set; }
    public int CurrentProductionQuantity { get; private set; }
    public int MaxProductionQuantity { get; private set; }

    public Product(){}
    
    public Product(string name, string productType, int maxProductionQuantity)
    {
        ProductNumber = ProductNumber.GenerateNew();
        Name = name;
        ProductType = EProductTypeExtensions.ParseProductType(productType);
        CurrentProductionQuantity = 0;
        MaxProductionQuantity = maxProductionQuantity;
    }

    public Product(CreateProductCommand command)
    {
        ProductNumber = ProductNumber.GenerateNew();
        Name = command.Name;
        ProductType = EProductTypeExtensions.ParseProductType(command.ProductType);
        CurrentProductionQuantity = 0;
        MaxProductionQuantity = command.MaxProductionQuantity;
    }
    
    public void IncrementProductionQuantity(int quantity)
    {
        if (CurrentProductionQuantity + quantity > MaxProductionQuantity)
            throw new InvalidOperationException("Cannot increment production quantity: would exceed maximum capacity");
    
        CurrentProductionQuantity += quantity;
    }
}