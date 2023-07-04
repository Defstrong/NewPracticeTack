using Practice.DataAccess;
namespace Practice.BusinessLogic;
/// <summary>
///     Represent interface for order services
/// </summary>
public interface IOrderServices
{
    Task<int> CreateOrderAsync(string phoneNumberClient, Order order);
    Task<bool> DeleteOrderAsync(int orderId);
    Task<Order> GetOrderAsync(string description);
    Task<Order> GetOrderAsync(int orderId);
    IAsyncEnumerable<Order> GetOrdersAsync();
    IAsyncEnumerable<Order> GetOrdersAsync(int idClient);
    IAsyncEnumerable<Order> GetOrdersAsync(int take, int skip, int idClient);
    IAsyncEnumerable<Order> GetOrdersAsync(DateTime fromDate, DateTime toDate);
    Task<int> GetCountOrderAsync(int idClient);
    Task UpdateOrderAsync(Order order, int orderId);
}