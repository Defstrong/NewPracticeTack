namespace Practice.DataAccess;

public sealed class ClientRepository : BaseRepository<DbClient>, IClientRepository
{
    public ClientRepository(IFileStorage<DbClient> fileStorage, DbRepository dbRepository)
        :base(fileStorage, dbRepository)
        {
        }
    public async IAsyncEnumerable<DbClient> GetByAddressAsync(string address)
    {
        foreach(var item in _entityList.Where(client => client.Address == address))
            yield return item;
    }
    public async IAsyncEnumerable<DbClient> GetByFirstNameAsync(string firstName)
    {
        foreach(var item in _entityList.Where(client => client.FirstName == firstName))
            yield return item;
    }
    public async IAsyncEnumerable<DbClient> GetByLastNameAsync(string lastName)
    {
        foreach(var item in _entityList.Where(client => client.LastName == lastName))
            yield return item;
    }
    public async IAsyncEnumerable<DbClient> GetByPhoneNumberAsync(string phoneNumber)
    {
        foreach(var item in _entityList.Where(client => client.PhoneNumber == phoneNumber))
            yield return item;
    }
    public Task<DbClient> GetClientAsync(string phoneNumberClient) => 
        Task.Run(() =>_entityList.First(client => client.PhoneNumber == phoneNumberClient));
    public Task<DbClient> GetClientAsync(int idClient) =>
        Task.Run(() => _entityList.First(client => client.Id == idClient));
    public async IAsyncEnumerable<DbClient> GetClientsAsync()
    {
        foreach(var item in _entityList)
            yield return item;
    }
}