using Practice.DataAccess;
namespace Practice.BusinessLogic;

public interface IClientServices
{
     Guid CreateClient(Client client);
     bool DeleteClient(Guid id);
     Client GetClient(Guid orderId);
     IEnumerable<Client> GetClients();
     IEnumerable<Client> GetClients(int take, int skip);
     void UpdateClient(Client client, Guid clientId);
}