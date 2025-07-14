using si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;
using si730ebu20221g120.API.Inventories.Domain.Model.Commands;
using si730ebu20221g120.API.Inventories.Domain.Repositories;
using si730ebu20221g120.API.Inventories.Domain.Services;
using si730ebu20221g120.API.Inventories.Infrastructure.Persistence.EFC.Repositories;
using si730ebu20221g120.API.Shared.Domain.Repositories;

namespace si730ebu20221g120.API.Inventories.Application.Internal.CommandServices;

/// <summary>
///     Application service for handling the creation of products.
/// </summary>
/// <remarks>
///     Applies domain rules before persisting the product.
///     Author: Daniel Crispin Ramos
/// </remarks>
public class ProductCommandService(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IConfiguration configuration
    ) : IProductCommandService
{
    public async Task<Product?> Handle(CreateProductCommand command)
    {
        // Validar que el nombre no esté vacío
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ArgumentException("Product name is required and cannot be empty");
        
        // Validar longitud máxima del nombre
        if (command.Name.Length > 60)
            throw new ArgumentException("Product name must be 60 characters or less");
        
        // Validar que el nombre sea único (necesitas implementar este método)
        if (await productRepository.ExistsByProductNameAsync(command.Name))
            throw new InvalidOperationException("A product with this name already exists");
        
        // Validar capacidad según configuración
        var minCapacity = configuration.GetValue<int>("CapacityThresholds:minCapacityThreshold");
        var maxCapacity = configuration.GetValue<int>("CapacityThresholds:maxCapacityThreshold");

        if (command.MaxProductionQuantity < minCapacity || command.MaxProductionQuantity > maxCapacity)
            throw new ArgumentException($"Max production capacity must be between {minCapacity} and {maxCapacity}");
        
        var product = new Product(command);
        if (await productRepository.ExistsByProductNumberAsync(product.ProductNumber))
        {
            throw new InvalidOperationException("Generated ProductNumber already exists. Please try again.");
        }
        // Persistir
        await productRepository.AddAsync(product);
        await unitOfWork.CompleteAsync();
        
        return product;
    }
}