namespace Practice.DataAccess;

public sealed class ClientRepository : BaseRepository<DbClient>, IClientRepository
{
    public ClientRepository(IFileStorage<DbClient> fileStorage)
        :base(fileStorage)
        {

        }

    public IEnumerable<DbClient> GetByAddress(string address)
        => _entityList.Where(client => client.Address == address);
    public IEnumerable<DbClient> GetByFirstName(string firstName)
        => _entityList.Where(client => client.FirstName == firstName);
    public IEnumerable<DbClient> GetByLastName(string lastName)
        => _entityList.Where(client => client.LastName == lastName);
    public IEnumerable<DbClient> GetByPhoneNumber(string phoneNumber)
        => _entityList.Where(client => client.PhoneNumber == phoneNumber);
    public DbClient GetClient(string phoneNumberClient)
        => _entityList.First(client => client.PhoneNumber == phoneNumberClient);
    public DbClient GetClient(Guid idClient)
        => _entityList.First(client => client.Id == idClient);
    public IEnumerable<DbClient> GetClients()
        => _entityList;
}