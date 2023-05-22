using Practice.DataAccess;
namespace Practice.BusinessLogic;
/// <summary>
///     Represent interface for order services
/// </summary>
public interface IOrderServices
{
    void CreateOrder(string phoneNumberClient, Order order);
    Order GetOrder(string description);
    Order GetOrder(Guid orderId);
    IEnumerable<Order> GetOrders();
    IEnumerable<Order> GetOrders(int take, int skip);
    IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate);
    int GetCountOrder();
    void UpdateOrder(Order order, Guid orderId);
}