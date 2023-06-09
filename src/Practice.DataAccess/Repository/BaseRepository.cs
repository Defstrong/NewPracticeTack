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
    private IDbRepository _dbRepository;
    private int IdEntity = 1;
    public BaseRepository(IFileStorage<T> fileStorage, IDbRepository dbRepository)
    {
        var taskFileStorage = Task.Run(async () =>
        {
            await foreach(var item in fileStorage.ReadAsync())
                _entityList.Add(item);
        });
        _dbRepository = dbRepository;
        _fileStorage = fileStorage;
    }
    /// <summary>
    ///     Represent method for commit all data
    /// </summary>
    private async Task CommitAsync() => await _fileStorage.SaveAsync(_entityList);
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
    public async Task<int> CreateAsync(T entity)
    {
        return await Task.Run(() =>
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));
            entity.Id = IdEntity++;
            _entityList.Add(entity);
            Console.WriteLine(_dbRepository.Create(entity));
            CommitAsync();
            return entity.Id;
        });
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
    public Task DeleteAsync(int id)
    {
        return Task.Run(() =>
        {
            var entityForDelete = _entityList.Single(x => x.Id == id);
            Console.WriteLine(_dbRepository.Delete(id));
            _entityList.Remove(entityForDelete);
            CommitAsync();
        });
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
    public Task<T> GetAsync(int id) =>
        Task.Run(() => _entityList.Single(x => x.Id == id));
    /// <summary>
    ///     Represent method for get all data
    /// </summary>
    public async IAsyncEnumerable<T> GetAllAsync() 
    {
        foreach (var item in _entityList)
            yield return item;
    }
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
    public Task UpdateAsync(T entity, int id)
    {
        return Task.Run(() =>
        {
            var oldData = _entityList.Single(x => x.Id == id);
            entity.Id = oldData.Id;
            _entityList.Remove(oldData);
            _entityList.Add(entity);
            Console.WriteLine(_dbRepository.Update(entity, id));
            CommitAsync();
        });
    }
}