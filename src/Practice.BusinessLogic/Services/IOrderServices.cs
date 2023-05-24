using Practice.DataAccess;
namespace Practice.BusinessLogic;
/// <summary>
///     Represent interface for order services
/// </summary>
public interface IOrderServices
{
    Guid CreateOrder(string phoneNumberClient, Order order);
    bool DeleteOrder(Guid orderId);
    Order GetOrder(string description);
    Order GetOrder(Guid orderId);
    IEnumerable<Order> GetOrders();
    IEnumerable<Order> GetOrders(Guid idClient);
    IEnumerable<Order> GetOrders(int take, int skip, Guid idClient);
    IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate);
    int GetCountOrder(Guid idClient);
    void UpdateOrder(Order order, Guid orderId);
}