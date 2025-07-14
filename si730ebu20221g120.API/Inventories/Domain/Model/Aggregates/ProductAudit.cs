using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace si730ebu20221g120.API.Inventories.Domain.Model.Aggregates;

/// <summary>
///     Represents a product with audit information and business rules.
/// </summary>
/// <remarks>
///     Author: Daniel Crispin Ramos
/// </remarks>
public partial class Product : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}