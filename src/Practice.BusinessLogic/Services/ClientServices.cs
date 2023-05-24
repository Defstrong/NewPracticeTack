using Practice.DataAccess;

namespace Practice.BusinessLogic;

public sealed class ClientServices : IClientServices 
{
    private ClientRepository _clientRepository;
    public ClientServices(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public Guid CreateClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        return _clientRepository.Create(client.ClientToDbClient()); 
    }
    public bool DeleteClient(Guid clientId)
    {
        var client = _clientRepository.GetClient(clientId);
        _clientRepository.Delete(client.Id);
        return true;
    }
    public Client GetClient(Guid clientId)
    {
        var client = _clientRepository.GetClient(clientId);
        return client.DbClientToClient();
    }
    public IEnumerable<Client> GetClients() =>
        _clientRepository.GetClients().Select(x => x.DbClientToClient());
    public IEnumerable<Client> GetClients(int take, int skip)
    {
        if(take + skip >= _clientRepository.GetClients().Count())
            return _clientRepository.GetClients().Skip(skip).Take(take).Select(x => x.DbClientToClient());
        else
            throw new IndexOutOfRangeException();
    }
    public void UpdateClient(Client client, Guid clientId)
    {
        var dbClient = client.ClientToDbClient();
        dbClient.Id = clientId;
        _clientRepository.Update(dbClient, dbClient.Id);        
    }
}