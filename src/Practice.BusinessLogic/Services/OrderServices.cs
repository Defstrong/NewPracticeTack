using Practice.DataAccess;

namespace Practice.BusinessLogic;
/// <summary>
///     Represent order services
/// </summary>
public sealed class OrderServices : IOrderServices
{
    private OrderRepository _orderRepository;
    private ClientRepository _clientRepository;

    public OrderServices(OrderRepository orderRepository, ClientRepository clientRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
    }
    /// <summary>
    ///     Represent method for create order
    /// <summary>
    /// <param name="price">
    ///     Price order
    /// </param>
    /// <param name="description">
    ///     Description order
    /// </param>
    public async Task<int> CreateOrderAsync(string phoneNumberClient, Order order)
    {
        var client = await _clientRepository.GetClientAsync(phoneNumberClient);
        if(client is not null)
        {
            var dbOrder = order.OrderToDbOrder();
            dbOrder.IdClient = client.Id;
            var idOrder = await _orderRepository.CreateAsync(dbOrder);
            return idOrder;
        }
        else
            throw new Exception();
    }
    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        await _orderRepository.DeleteAsync(order.Id);
        return true;
    }
    /// <summary>
    ///     Represent method for get order with description
    /// </summary>
    /// <param name="description">
    ///     Description Order
    /// </param>
    public async Task<Order> GetOrderAsync(string description)
    {
        var dbOrder = await _orderRepository.GetOrderAsync(description);
        return dbOrder.DbOrderToOrder();
    }
    public async Task<Order> GetOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        return order.DbOrderToOrder();
    }
    public IAsyncEnumerable<Order> GetOrdersAsync(int idClient) =>
        _orderRepository.GetOrdersAsync()
            .Where(x => x.IdClient == idClient)
            .Select(x => x.DbOrderToOrder());
    
    public IAsyncEnumerable<Order>GetOrdersAsync() =>
        _orderRepository.GetOrdersAsync()
            .Select(x => x.DbOrderToOrder());

    public IAsyncEnumerable<Order> GetOrdersAsync(DateTime fromDate, DateTime toDate) =>
        _orderRepository.GetOrderAsync(fromDate, toDate)
            .Select(x => x.DbOrderToOrder());
 
    public async IAsyncEnumerable<Order> GetOrdersAsync(int take, int skip, int idClient)
    {
         if(take + skip <= await _orderRepository.GetOrdersAsync().CountAsync())
         {
            await foreach(var item in _orderRepository.GetOrdersAsync()
                .Where(x => x.IdClient == idClient)
                .Skip(skip).Take(take).Select(x => x.DbOrderToOrder()))
                    yield return item;
         }
        else
            throw new IndexOutOfRangeException();
    }
    public async Task<int> GetCountOrderAsync(int idClient)
        => await _orderRepository.GetOrdersAsync().CountAsync();
    public async Task UpdateOrderAsync(Order order, int orderId)
    {
        var dbOrder = order.OrderToDbOrder();
        dbOrder.Id = orderId;
        await _orderRepository.UpdateAsync(dbOrder,orderId);
    }
}