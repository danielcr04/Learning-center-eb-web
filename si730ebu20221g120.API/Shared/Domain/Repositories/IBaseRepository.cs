namespace si730ebu20221g120.API.Shared.Domain.Repositories;

/// <summary>
///     Base repository interface for all repositories
/// </summary>
/// <typeparam name="TEntity">
///     The type of the entity being managed by the repository. Must be a domain entity
/// </typeparam>
/// <remarks>
///     This interface defines the basic CRUD operations for all repositories
/// </remarks>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    ///     Add entity to the repository
    /// </summary>
    /// <param name="entity">Entity object to add</param>
    /// <returns>A Task representing the asynchronous operation</returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    ///     Find entity by id
    /// </summary>
    /// <param name="id">The Entity ID to Find</param>
    /// <returns>Entity object if found</returns>
    Task<TEntity> FindByIdAsync(int id);
    
    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    void Update(TEntity entity);
    
    /// <summary>
    ///     Remove an entity
    /// </summary>
    /// <param name="entity">The entity object to remove</param>
    void Remove(TEntity entity);
    
    /// <summary>
    ///     Get all entities
    /// </summary>
    /// <returns>An Enumerable containing all entity objects</returns>
    Task<IEnumerable<TEntity>> ListAsync();
}