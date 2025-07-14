namespace si730ebu20221g120.API.Inventories.Interfaces.REST.Resources;

public record CreateProductResource(
    string Name,
    string ProductType,
    int MaxProductionQuantity
    );