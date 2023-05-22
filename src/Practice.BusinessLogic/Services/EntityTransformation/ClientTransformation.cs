using Practice.DataAccess;
namespace Practice.BusinessLogic;

public static class ClientTransformation
{
    public static DbClient ClientToDbClient(this Client client)
        => new DbClient {FirstName = client.FirstName, LastName = client.LastName,
            Address = client.Address, PhoneNumber = client.PhoneNumber };
    public static Client DbClientToClient(this DbClient dbClient)
        => new Client {FirstName = dbClient.FirstName, LastName = dbClient.LastName,
            Address = dbClient.Address, PhoneNumber = dbClient.PhoneNumber};
}