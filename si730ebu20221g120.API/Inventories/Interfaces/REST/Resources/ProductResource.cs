namespace si730ebu20221g120.API.Inventories.Interfaces.REST.Resources;

public record ProductResource(
    int Id,
    string Name, 
    string ProductNumber,
    string ProductType, 
    int CurrentProductionQuantity,
    int MaxProductionQuantity
    );