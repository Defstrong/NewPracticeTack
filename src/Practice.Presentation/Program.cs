using Practice.DataAccess;
using Practice.BusinessLogic;
string _filePathOrder = "./xmlOrder.xml";
string _filePathClient = "./xmlClient.xml";
var fileStorageOrder = FileStorageFactory<DbOrder>.GetXmlFileStorageAsync(_filePathOrder);
var fileStorageClient = FileStorageFactory<DbClient>.GetXmlFileStorageAsync(_filePathClient);
var dbRepository = new DbRepository("Server=localhost;Port=5432;User Id=postgres;Password=1111;Database=practice");
var orderRepository = new OrderRepository(fileStorageOrder,dbRepository);
var clientRepository = new ClientRepository(fileStorageClient,dbRepository);
var clientServices = new ClientServices(clientRepository);
var orderServices = new OrderServices(orderRepository, clientRepository);
var operatorLogic = new OperatorLogic(orderServices);

var client = new Client {FirstName = "Bob", LastName = "Lee", 
    Address = "Another", PhoneNumber = "94832094830"};

var _idClient = await clientServices.CreateClientAsync(client);

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
        Console.WriteLine(await ClientActionAsync(_idClient));
    else if(userAnswer == "Operator")
        Console.WriteLine(OperatorActionAsync(_idClient));
    else if(userAnswer == "Exit")
        break;
}


async Task<string> ClientActionAsync(int idClient)
{
    Console.WriteLine($"Get order\tGet orders\tDelete order\t Update order");
    Console.Write("Enter command to action: ");
    string? commandClient = Console.ReadLine();
    if(commandClient == "Get order")
        return await GetOrderAsync(idClient);
    else if(commandClient == "Get orders")
        return await GetOrdersAsync(idClient);
    else if(commandClient == "Delete order")
        return await DeleteOrderAsync(idClient);
    else if(commandClient == "Update order")
        return await ClientUpdateOrderAsync(idClient);
    else
        return "Error";
}
async Task<string> OperatorActionAsync(int idClient)
{
    Console.WriteLine("Create order\tUpdate order\tGet order");
    var command = Console.ReadLine();
    if(command == "Create order")
        await CreateOrderAsync(idClient);
    else if(command == "Update order")
        await OperatorUpdateOrder(idClient);
    else if(command == "Get order")
        OperatorGetOrder(idClient);
    return "Error";
}

async Task<string> GetOrderAsync(int idClient)
{
    Console.Write("Enter id order: ");
    string? idOrder = Console.ReadLine();
    if(string.IsNullOrEmpty(idOrder))
        return "Error id order";
    var getOrder = await orderServices?.GetOrderAsync(int.Parse(idOrder));
    Console.WriteLine(getOrder.ToString());
    return "Get order completed successfully";
}
async Task<string> GetOrdersAsync(int idClient)
{
    var countOrder = await orderServices?.GetCountOrderAsync(idClient);
    int skip = 0;
    const int take = 10;
    for(int i = 0; i < countOrder/10; i++)
    {
        var orders = orderServices?.GetOrdersAsync(take, skip, idClient);
        if(orders is not null)
            await foreach(var ii in orders)
                Console.WriteLine(ii.ToString());
        skip += 10;
        Console.ReadKey();
    }
    int balace = (int)countOrder % 10;
    await foreach(var ii in orderServices.GetOrdersAsync(balace, skip, idClient))
        Console.WriteLine(ii.ToString());
    return "Get orders completed successfully";
}
async Task<string> DeleteOrderAsync(int idClient)
{
    Console.Write("Enter id order to delete: ");
    var idOrderForDelete = Console.ReadLine();
    if(string.IsNullOrEmpty(idOrderForDelete))
        return await Task.Run(() => "Id order for delete is not corected");
    await orderServices.DeleteOrderAsync(int.Parse(idOrderForDelete));
    return "Delete order completed sucessfully"; 
}
async Task<string> ClientUpdateOrderAsync(int idClient)
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
    await orderServices.UpdateOrderAsync(newOrder, int.Parse(idOrderForUpdate));
    
    return "Update order completed successfully";
}
async Task<string> CreateOrderAsync(int idClient)
{
    Console.Write("Enter description order: ");
    var description = Console.ReadLine();
    Console.Write("Enter price order: ");
    var price = decimal.Parse(Console.ReadLine());
    Console.Write("Enter phone number client: ");
    var phoneNumberClient = Console.ReadLine();
    var newOrder = new Order {DateOrder = DateTime.Now, Description = description, Price = price};
    await orderServices.CreateOrderAsync(phoneNumberClient,newOrder);
    return "Create order completed successfully";   
}
async Task<string> OperatorUpdateOrder(int idClient)
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
    await orderServices.UpdateOrderAsync(newOrder, int.Parse(idOrderForUpdate));
    return "Update order completed successfully";
}
string OperatorGetOrder(int idClient)
{
    return "Error";
}