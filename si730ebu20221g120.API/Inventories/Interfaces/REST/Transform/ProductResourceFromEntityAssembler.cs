using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Inventories.Interfaces.REST.Resources;

namespace si730ebu20221g120.API.Inventories.Interfaces.REST.Transform;

public static class ProductResourceFromEntityAssembler
{
  public static ProductResource ToResourceFromEntity(Product entity)
  {
    return new ProductResource(
      entity.Id,
      entity.Name,
      entity.ProductNumber.Value.ToString(),
      entity.ProductType.ToString(),
      entity.MaxProductionQuantity,
      entity.CurrentProductionQuantity
    );
  }
}