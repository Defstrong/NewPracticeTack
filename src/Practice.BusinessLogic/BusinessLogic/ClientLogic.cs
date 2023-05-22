using Practice.DataAccess;
namespace Practice.BusinessLogic;

public sealed class ClientLogic
{
    IOrderServices _orderServices;

    public ClientLogic(IOrderServices orderServices)
    {
        _orderServices = orderServices;
    }

    public Order GetOrder(Guid orderId)
        => _orderServices.GetOrder(orderId);

    public IEnumerable<Order> GetOrders(int take, int skip)
        => _orderServices.GetOrders(take, skip);
}