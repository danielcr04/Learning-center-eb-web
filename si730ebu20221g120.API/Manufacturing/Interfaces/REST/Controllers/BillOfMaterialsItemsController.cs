using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using si730ebu20221g120.API.Manufacturing.Domain.Services;
using si730ebu20221g120.API.Manufacturing.Interfaces.REST.Resources;
using si730ebu20221g120.API.Manufacturing.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace si730ebu20221g120.API.Manufacturing.Interfaces.REST.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Products Endpoints")]
public class BillOfMaterialsItemsController(
    IBillOfMaterialsItemCommandService billOfMaterialsItemCommandService) : ControllerBase
{
    /// <summary>
    /// Creates a new Bill of Materials Item based on the provided input.
    /// </summary>
    /// <param name="resource">The request body containing Bill of Materials Item details.</param>
    /// <returns>
    /// A 201 Created response with the created Bill of Materials Item resource,
    /// or a 400 Bad Request if the input is invalid,
    /// or a 500 Internal Server Error if something unexpected occurs.
    /// </returns>
    /// <remarks>
    /// Author: Daniel Crispin Ramos
    /// </remarks>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Bill of Materials Item",
        Description = "Create a new Bill of Materials Item in the inventory system",
        OperationId = "CreateBillOfMaterialsItem")]
    [SwaggerResponse(201, "Bill of Materials Item created successfully", typeof(BillOfMaterialsItemResource))]
    [SwaggerResponse(400, "Invalid input or business rule violation")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateBillOfMaterialsItem([FromBody] CreateBillOfMaterialsItemResource resource)
    {
        try
        {
            // Convertir el recurso a un comando
            var createBillOfMaterialsItemCommand = CreateBillOfMaterialsItemCommandFromResourceAssembler.ToCommandFromResource(resource);

            // Llamar al servicio de comando para manejar la lógica de negocio y crear el Bill of Materials Item
            var billOfMaterialsItem = await billOfMaterialsItemCommandService.Handle(createBillOfMaterialsItemCommand);

            // Si la creación falla, devolver BadRequest
            if (billOfMaterialsItem == null)
                return BadRequest("Failed to create Bill of Materials Item. Check business rules and input data.");

            // Mapear la entidad a un recurso para devolverlo
            var resourceResponse = BillOfMaterialsItemResourceFromEntityAssembler.ToResourceFromEntity(billOfMaterialsItem);

            // Retornar el recurso con un código 201 y la URL del nuevo recurso
            return CreatedAtAction(nameof(CreateBillOfMaterialsItem), new { billOfMaterialsItem.Id }, resourceResponse);

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
}