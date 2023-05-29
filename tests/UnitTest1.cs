using Practice.BusinessLogic;
using Practice.DataAccess;
namespace tests;

public class ClientServicesTests 
{
    [Fact]
    public void ClientServices_CreateClient_Scenario()
    {
        string filePath = ".\\xml.xml";
        var fileStorage = new XmlFileStorage<DbClient>(filePath);
        var clientRepository = new ClientRepository(fileStorage);
        var clientServices = new ClientServices(clientRepository);
        Assert.Throws<ArgumentNullException>(() => clientServices.CreateClient(client));
    }
    [Fact]
    public void ClientServices_DeleteClient_AnswerTrue_Scenario(Guid idClient, IFileStorage<DbClient> fileStorage)
    {
        var clientRepository = new ClientRepository(fileStorage);
        var clientServices = new ClientServices(clientRepository);
        Assert.True(clientServices.DeleteClient(idClient));
    }
    [Fact]
    public void ClientServices_DeleteClient_AnswerFalse_Scenario(Guid idClient, IFileStorage<DbClient> fileStorage)
    {
        var clientRepository = new ClientRepository(fileStorage);
        var clientServices = new ClientServices(clientRepository);
        Assert.False(clientServices.DeleteClient(idClient));
    }
    public void ClientServices_GetClient_ThrowInvalidOperationException_Scenario(Guid idClient, IFileStorage<DbClient> fileStorage)
    {
        var clientRepository = new ClientRepository(fileStorage);
        var clientServices = new ClientServices(clientRepository);
        Assert.Throws<InvalidOperationException>(() => clientServices.GetClient(idClient));
    }
    public void ClientServices_GetClient_ArgumentNullException_Scenario(Guid idClient, IFileStorage<DbClient> fileStorage)
    {
        var clientRepository = new ClientRepository(fileStorage);
        var clientServices = new ClientServices(clientRepository);
        Assert.Throws<ArgumentNullException>(() => clientServices.GetClient(idClient));
    }
    public void ClientServices_GetClients
}