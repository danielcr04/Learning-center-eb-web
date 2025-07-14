namespace si730ebu20221g120.API.Shared.Domain.Repositories;

/// <summary>
///     Unit of work interface
/// </summary>
/// <remarks>
///     This interface defines the basic operations for a unit of work
/// </remarks>
public interface IUnitOfWork
{
    /// <summary>
    ///     Commit changes to the database
    /// </summary>
    /// <returns>A Task representing the asynchronous operation</returns>
    Task CompleteAsync();
}