using Practice.DataAccess;
namespace Practice.BusinessLogic;

public interface IClientServices
{
     Task<int> CreateClientAsync(Client client);
     Task<bool> DeleteClientAsync(int id);
     Task<Client> GetClientAsync(int orderId);
     IAsyncEnumerable<Client> GetClientsAsync();
     IAsyncEnumerable<Client> GetClientsAsync(int take, int skip);
     Task UpdateClientAsync(Client client, int clientId);
}