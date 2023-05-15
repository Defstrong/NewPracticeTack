namespace Practice.DataAccess;
/// <summary>
/// Represent interface for monipulation files
/// </summary>
public interface IFileStorage<T>
    where T : IBaseDbEntity
{
    public void Save(T entities);
    public void Save(IEnumerable<T> entities);
    public IEnumerable<T> Read();
}