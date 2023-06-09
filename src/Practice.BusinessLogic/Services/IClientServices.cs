using Practice.DataAccess;
namespace Practice.BusinessLogic;

public interface IClientServices
{
     Task<Guid> CreateClientAsync(Client client);
     Task<bool> DeleteClientAsync(Guid id);
     Task<Client> GetClientAsync(Guid orderId);
     IAsyncEnumerable<Client> GetClientsAsync();
     IAsyncEnumerable<Client> GetClientsAsync(int take, int skip);
     Task UpdateClientAsync(Client client, Guid clientId);
}