namespace Practice.DataAccess;
/// <summary>
///     Represent class for base repository
/// </summary>
///<typeparam name="T">
///     Entity type
///</typeparam>
public class BaseRepository<T> : IBaseRepository<T>
    where T : DbEntity
{
    protected List<T> _entityList = new List<T>();
    private IFileStorage<T> _fileStorage;
    public BaseRepository(IFileStorage<T> fileStorage)
    {
        _fileStorage = fileStorage;
        _entityList = _fileStorage.Read().ToList<T>();
    }
    /// <summary>
    ///     Represent method for commit all data
    /// </summary>
    private void Commit() => _fileStorage.Save(_entityList);
    /// <summary>
    ///     Represent method for create data for save
    /// </summary>
    /// <param name = "entity">
    ///     Entity for add at _entityList 
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The exception that is thrown when a null reference 
    ///     is passed to a method that does not accept it as a valid argument.
    /// </exception>
    /// <returned>
    ///     Return Id
    /// </returned>
    public Guid Create(T entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));
        entity.Id = Guid.NewGuid();
        _entityList.Add(entity);
        Commit();
        return entity.Id;
    }
    /// <summary>
    ///     Represent method for delete data 
    /// </summary>
    /// <param name = "id">
    ///     Id data for delete data at _entityList
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     The exception that is thrown when a method call is invalid for the object's current
    ///     state.
    /// </exception>
    public void Delete(Guid id)
    {
        var entityForDelete = _entityList.Single(x => x.Id == id);
        _entityList.Remove(entityForDelete);
        Commit();
    }
    /// <summary>
    ///     Represent method for get data
    /// </summary>
    /// <param name = "id">
    ///     Id data for get data
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     The exception that is thrown when a method call is invalid for the object's current
    ///     state.
    /// </exception>
    public T Get(Guid id)
    {
        var entityForGet = _entityList.Single(x => x.Id == id);
        return entityForGet;
    }
    /// <summary>
    ///     Represent method for get all data
    /// </summary>
    public IEnumerable<T> GetAll() => _entityList;
    /// <summary>
    ///     Represent method for updata data
    /// </summary>
    /// <param name = "id">
    ///     Id data for updata data
    /// </param>
    /// <param name="entity">
    ///     Entity for swap with old data
    /// </param> 
    /// <exception cref="InvalidOperatiponException">
    ///     The exception that is thrown when a method call is invalid for the object's current
    ///     state.
    /// </exception>
    public void Update(T entity, Guid id)
    {
        var oldData = _entityList.Single(x => x.Id == id);
        oldData = entity;
    }
}