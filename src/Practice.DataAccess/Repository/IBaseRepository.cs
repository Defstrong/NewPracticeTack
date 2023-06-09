namespace Practice.DataAccess;

///<summary>
///     
///</summary>

///<param name = "T">
///
///</param>

public interface IBaseRepository<T>
    where T : DbEntity
{
    public Task<Guid> CreateAsync(T entity);
    public Task<T> GetAsync(Guid id);
    public IAsyncEnumerable<T> GetAllAsync();
    public Task UpdateAsync(T entity, Guid id);
    public Task DeleteAsync(Guid id);
}