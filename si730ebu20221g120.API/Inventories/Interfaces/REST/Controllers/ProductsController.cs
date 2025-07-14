using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using si730ebu20221g120.API.Inventories.Domain.Model.Queries;
using si730ebu20221g120.API.Inventories.Domain.Services;
using si730ebu20221g120.API.Inventories.Interfaces.REST.Resources;
using si730ebu20221g120.API.Inventories.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace si730ebu20221g120.API.Inventories.Interfaces.REST.Controllers;

/// <summary>
/// Controller for managing products in the inventory system.
/// Provides endpoints for creating and retrieving products.
/// </summary>
/// <remarks>
/// Author: [Your Full Name]
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Products Endpoints")]
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService) : ControllerBase
{
    /// <summary>
    /// Creates a new product based on the provided input.
    /// </summary>
    /// <param name="resource">The request body containing product details.</param>
    /// <returns>
    /// A 201 Created response with the created product resource,
    /// or a 400 Bad Request if the input is invalid,
    /// or a 500 Internal Server Error if something unexpected occurs.
    /// </returns>
    /// <remarks>
    /// Author: [Your Full Name]
    /// </remarks>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Product",
        Description = "Create a new product in the inventory system",
        OperationId = "CreateProduct")]
    [SwaggerResponse(201, "Product created successfully", typeof(ProductResource))]
    [SwaggerResponse(400, "Invalid input or business rule violation")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource)
    {
        try
        {
            var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource);
            var product = await productCommandService.Handle(createProductCommand);
            
            if (product is null)
                return BadRequest("The product could not be created.");
                
            var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
            
            return CreatedAtAction(
                nameof(GetProductById),
                new { id = productResource.Id },
                productResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error." });
        }
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>
    /// A 200 OK response with the product resource,
    /// or a 404 Not Found if the product doesn't exist,
    /// or a 500 Internal Server Error if something unexpected occurs.
    /// </returns>
    /// <remarks>
    /// Author: [Your Full Name]
    /// </remarks>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Product by ID",
        Description = "Retrieve a product by its unique identifier",
        OperationId = "GetProductById")]
    [SwaggerResponse(200, "Product retrieved successfully", typeof(ProductResource))]
    [SwaggerResponse(404, "Product not found")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var getProductByIdQuery = new GetProductByIdQuery(id);
            var product = await productQueryService.Handle(getProductByIdQuery);
            
            if (product is null)
                return NotFound($"Product with ID {id} not found.");
                
            var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
            
            return Ok(productResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error." });
        }
    }
}