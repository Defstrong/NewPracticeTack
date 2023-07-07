namespace Practice.DataAccess;

public interface IDbRepository<T> : IBaseRepository<T>
    where T : DbEntity
{
    Task EnsureConnected(); 
}