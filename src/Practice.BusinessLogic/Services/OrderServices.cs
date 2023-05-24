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
    public Guid CreateOrder(string phoneNumberClient, Order order)
    {
        var client = _clientRepository.GetClient(phoneNumberClient);
        if(client is not null)
        {
            var dbOrder = order.OrderToDbOrder();
            dbOrder.IdClient = client.Id;
            return _orderRepository.Create(dbOrder);
        }
        throw new Exception();
    }
    public bool DeleteOrder(Guid orderId)
    {
        var order = _orderRepository.GetOrder(orderId);
        _orderRepository.Delete(order.Id);
        return true;
    }
    /// <summary>
    ///     Represent method for get order with description
    /// </summary>
    /// <param name="description">
    ///     Description Order
    /// </param>
    public Order GetOrder(string description)
    {
        var dbOrder = _orderRepository.GetOrder(description);
        return dbOrder.DbOrderToOrder();
    }
    public Order GetOrder(Guid orderId)
    {
        var order = _orderRepository.GetOrder(orderId);
        return order.DbOrderToOrder();
    }
    public IEnumerable<Order> GetOrders(Guid idClient) =>
        _orderRepository.GetOrders().Where(x => x.IdClient == idClient).Select(x => x.DbOrderToOrder());
    
    public IEnumerable<Order>GetOrders() =>
        _orderRepository.GetOrders().Select(x => x.DbOrderToOrder());

    public IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate) =>
        _orderRepository.GetOrder(fromDate, toDate)
            .Select(x => x.DbOrderToOrder());
 
    public IEnumerable<Order> GetOrders(int take, int skip, Guid idClient)
    {
        Console.WriteLine(take + skip);
        Console.WriteLine(_orderRepository.GetOrders().Count());
        if(take + skip <= _orderRepository.GetOrders().Count())
            return _orderRepository.GetOrders().Where(x => x.IdClient == idClient)
                .Skip(skip).Take(take).Select(x => x.DbOrderToOrder());
        else
            throw new IndexOutOfRangeException();
    }
    public int GetCountOrder(Guid idClient)
        => _orderRepository.GetOrders().Count();
    public void UpdateOrder(Order order, Guid orderId)
    {
        var dbOrder = order.OrderToDbOrder();
        dbOrder.Id = orderId;
        _orderRepository.Update(dbOrder,orderId);
    }
}