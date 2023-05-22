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
    public Guid Create(T entity);
    public T Get(Guid id);
    public IEnumerable<T> GetAll();
    public void Update(T entity, Guid id);
    public void Delete(Guid id);
}