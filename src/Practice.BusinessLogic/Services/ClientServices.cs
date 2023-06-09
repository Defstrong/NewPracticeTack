using Practice.DataAccess;

namespace Practice.BusinessLogic;

public sealed class ClientServices : IClientServices 
{
    private ClientRepository _clientRepository;
    public ClientServices(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task<Guid> CreateClientAsync(Client client)
    {
        if(client is not null)
        {
            return await Task.Run(async () =>
            {
                var dbClient = client.ClientToDbClient();
                return await _clientRepository.CreateAsync(dbClient);
            });
        }
        else
            throw new Exception();
        // ArgumentNullException.ThrowIfNull(client);
        // return await Task.Run(async () =>
        // {
        //     var dbClient = client.ClientToDbClient();
        //     return await _clientRepository.CreateAsync(dbClient);
        // });
        // return await _clientRepository.CreateAsync(client.ClientToDbClient()); 
    }
    public async Task<bool> DeleteClientAsync(Guid clientId)
    {
        var client = await _clientRepository.GetClientAsync(clientId);
        await _clientRepository.DeleteAsync(client.Id);
        return true;
    }
    public async Task<Client> GetClientAsync(Guid clientId)
    {
        var client = await _clientRepository.GetClientAsync(clientId);
        return client.DbClientToClient();
    }
    public async IAsyncEnumerable<Client> GetClientsAsync() 
    {
        await foreach (var item in _clientRepository.GetClientsAsync().Select(x => x.DbClientToClient()))
            yield return item;
    }
    public async IAsyncEnumerable<Client> GetClientsAsync(int take, int skip)
    {
        if(take + skip >= await _clientRepository.GetClientsAsync().CountAsync())
            await foreach(var item in _clientRepository.GetClientsAsync().Skip(skip).Take(take).Select(x => x.DbClientToClient()))
                yield return item;
        else
            throw new IndexOutOfRangeException();
    }
    public async Task UpdateClientAsync(Client client, Guid clientId)
    {
        var dbClient = client.ClientToDbClient();
        dbClient.Id = clientId;
        await _clientRepository.UpdateAsync(dbClient, dbClient.Id);        
    }
}