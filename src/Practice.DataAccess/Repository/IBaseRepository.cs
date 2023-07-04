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
    public Task<int> CreateAsync(T entity);
    public Task<T> GetAsync(int id);
    public IAsyncEnumerable<T> GetAllAsync();
    public Task UpdateAsync(T entity, int id);
    public Task DeleteAsync(int id);
}