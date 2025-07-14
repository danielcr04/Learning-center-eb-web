using si730ebu20221g120.API.Shared.Domain.Repositories;
using si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace si730ebu20221g120.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Unit of work implementation
/// </summary>
/// <param name="context">The database context for the application</param>
/// <remarks>
///     This class implements the basic operations for a unit of work.
///     It requires the context to be passed in the constructor.
/// </remarks>
/// <see cref="IUnitOfWork" />
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    /// <inheritdoc />
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}