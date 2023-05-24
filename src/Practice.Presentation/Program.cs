using Practice.DataAccess;
using Practice.BusinessLogic;
string _filePathOrder = "./xmlOrder.xml";
string _filePathClient = "./xmlClient.xml";
var fileStorageOrder = FileStorageFactory<DbOrder>.GetXmlFileStorage(_filePathOrder);
var fileStorageClient = FileStorageFactory<DbClient>.GetXmlFileStorage(_filePathClient);
var orderRepository = new OrderRepository(fileStorageOrder);
var clientRepository = new ClientRepository(fileStorageClient);
var clientServices = new ClientServices(clientRepository);
var orderServices = new OrderServices(orderRepository, clientRepository);
var operatorLogic = new OperatorLogic(orderServices);

var client = new Client {FirstName = "Bob", LastName = "Lee", 
    Address = "Another", PhoneNumber = "94832094830"};
var _idClient = clientServices.CreateClient(client);

int N = 100;
while(N-- > 0)
{
    Console.Write("Who are you Client or Operator?");
    string? userAnswer = Console.ReadLine();
    if(string.IsNullOrEmpty(userAnswer))
    {
        Console.WriteLine("Sorry but you answer is wrong");
        continue;
    }
    else if(userAnswer == "Client")
        Console.WriteLine(ClientAction(_idClient));
    else if(userAnswer == "Operator")
        Console.WriteLine(OperatorAction(_idClient));
    else if(userAnswer == "Exit")
        break;
}


string ClientAction(Guid idClient)
{
    Console.WriteLine($"Get order\tGet orders\tDelete order\t Update order");
    Console.Write("Enter command to action: ");
    string? commandClient = Console.ReadLine();
    if(commandClient == "Get order")
        return GetOrder(idClient);
    else if(commandClient == "Get orders")
        GetOrders(idClient);
    else if(commandClient == "Delete order")
        DeleteOrder(idClient);
    else if(commandClient == "Update order")
        ClientUpdateOrder(idClient);
    return "Error";
}
string OperatorAction(Guid idClient)
{
    Console.WriteLine("Create order\tUpdate order\tGet order");
    var command = Console.ReadLine();
    if(command == "Create order")
        CreateOrder(idClient);
    else if(command == "Update order")
        OperatorUpdateOrder(idClient);
    else if(command == "Get order")
        OperatorGetOrder(idClient);
    return "Error";
}

string GetOrder(Guid idClient)
{
    Console.Write("Enter id order: ");
    string? idOrder = Console.ReadLine();
    if(string.IsNullOrEmpty(idOrder))
        return "Error id order";
    Console.WriteLine(orderServices?.GetOrder(Guid.Parse(idOrder)).ToString());
    return "Get order completed successfully";
}
string GetOrders(Guid idClient)
{
    int? countOrder = orderServices?.GetCountOrder(idClient);
    int skip = 0;
    const int take = 10;
    for(int i = 0; i < countOrder/10; i++)
    {
        var orders = orderServices?.GetOrders(take, skip, idClient);
        if(orders is not null)
            foreach(var ii in orders)
            Console.WriteLine(ii.ToString());
        skip += 10;
        Console.ReadKey();
    }
    int balace = (int)countOrder % 10;
    foreach(var ii in orderServices.GetOrders(balace, skip, idClient))
        Console.WriteLine(ii.ToString());
    return "Get orders completed successfully";
}
string DeleteOrder(Guid idClient)
{
    Console.Write("Enter id order to delete: ");
    var idOrderForDelete = Console.ReadLine();
    if(string.IsNullOrEmpty(idOrderForDelete))
        return "Id order for delete is not corected";
    orderServices.DeleteOrder(Guid.Parse(idOrderForDelete));
    return "Delete order completed sucessfully"; 
}
string ClientUpdateOrder(Guid idClient)
{
    Console.Write("Enter id order to update: ");
    var idOrderForUpdate = Console.ReadLine();
    Console.Write("Enter description order: ");
    var description = Console.ReadLine();
    if(string.IsNullOrEmpty(description))
        return "Description order is empty";
    if(string.IsNullOrEmpty(idOrderForUpdate))
        return "Id order is empty";
    var newOrder = new Order {DateOrder = DateTime.Now,Description = description,Price = 123};
    orderServices.UpdateOrder(newOrder, Guid.Parse(idOrderForUpdate));
    return "Update order completed successfully";
}
string CreateOrder(Guid idClient)
{
    Console.Write("Enter description order: ");
    var description = Console.ReadLine();
    Console.Write("Enter price order: ");
    var price = decimal.Parse(Console.ReadLine());
    Console.Write("Enter phone number client: ");
    var phoneNumberClient = Console.ReadLine();
    var newOrder = new Order {DateOrder = DateTime.Now, Description = description, Price = price};
    for(int i = 0; i < 36; i++)
        orderServices.CreateOrder(phoneNumberClient,newOrder);
    return "Create order completed successfully";   
}
string OperatorUpdateOrder(Guid idClient)
{
    Console.Write("Enter id order to update: ");
    var idOrderForUpdate = Console.ReadLine();
    Console.Write("Enter description order: ");
    var description = Console.ReadLine();
    if(string.IsNullOrEmpty(description))
        return "Description order is empty";
    var newOrder = new Order {DateOrder = DateTime.Now,Description = description,Price = 123};
    if(string.IsNullOrEmpty(idOrderForUpdate))
        return "Id order is empty";
    orderServices.UpdateOrder(newOrder, Guid.Parse(idOrderForUpdate));
    return "Update order completed successfully";
}
string OperatorGetOrder(Guid idClient)
{
    return "Error";
}