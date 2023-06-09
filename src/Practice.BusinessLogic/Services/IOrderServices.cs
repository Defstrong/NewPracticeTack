using Practice.DataAccess;
namespace Practice.BusinessLogic;
/// <summary>
///     Represent interface for order services
/// </summary>
public interface IOrderServices
{
    Task<Guid> CreateOrderAsync(string phoneNumberClient, Order order);
    Task<bool> DeleteOrderAsync(Guid orderId);
    Task<Order> GetOrderAsync(string description);
    Task<Order> GetOrderAsync(Guid orderId);
    IAsyncEnumerable<Order> GetOrdersAsync();
    IAsyncEnumerable<Order> GetOrdersAsync(Guid idClient);
    IAsyncEnumerable<Order> GetOrdersAsync(int take, int skip, Guid idClient);
    IAsyncEnumerable<Order> GetOrdersAsync(DateTime fromDate, DateTime toDate);
    Task<int> GetCountOrderAsync(Guid idClient);
    Task UpdateOrderAsync(Order order, Guid orderId);
}