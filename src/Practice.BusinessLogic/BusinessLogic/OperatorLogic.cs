using Practice.DataAccess;
namespace Practice.BusinessLogic;

public sealed class OperatorLogic
{
    IOrderServices _orderServices;

    public OperatorLogic(IOrderServices orderServices)
    {
        _orderServices = orderServices;
    }
    
    public void CreateOrder(string phoneNumber, Order order)
        => _orderServices.CreateOrder(phoneNumber, order);
    
    public void UpdateOrder(Order order, Guid orderId)
        => _orderServices.UpdateOrder(order, orderId);
    
    public IEnumerable<Order> GetOrders()
        => _orderServices.GetOrders();
    
    public IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate)
        => _orderServices.GetOrders(fromDate, toDate);

    public IEnumerable<Order> GetOrders(int skip, int take)
        => _orderServices.GetOrders(skip, take);
}