using Practice.DataAccess;
namespace Practice.BusinessLogic;
/// <summary>
///     Reperesent class for operator logic
/// </summary>
public sealed class OperatorLogic
{
    IOrderServices _orderServices;
    /// <param name="orderServices">
    ///     Order services
    /// </param>
    public OperatorLogic(IOrderServices orderServices)
    {
        _orderServices = orderServices;
    }
    /// <summary>
    ///     Represent method for create order
    /// </summary>
    /// <param name="phoneNumber">
    ///     Phone number client
    /// </param>
    /// <param name="order">
    ///    Instance order for clreate order
    /// </param> 
    public async Task CreateOrderAsync(string phoneNumberClinet, Order order)
        => await _orderServices.CreateOrderAsync(phoneNumberClinet, order);
    /// <summary>
    ///     Represent method for update order
    /// </summary>
    /// <param name="order">
    ///     New order for update
    /// </param>
    /// <param name="orderId">
    ///     Order identificer for search old order
    /// </param>
    /// <param name="phoneNumberClient">
    ///     Phone number client for order
    /// </param>
    public async Task UpdateOrderAsync(Order order, Guid orderId)
        => await _orderServices.UpdateOrderAsync(order, orderId);
    /// <summary>
    ///     Represent method for get all orders
    /// </summary>
    public IAsyncEnumerable<Order> GetOrders()
        =>  _orderServices.GetOrdersAsync();
    /// <summary>
    ///     Represent method for get orders with date
    /// </summary>
    /// <param name="fromDate">
    ///     From date search
    /// </param>
    /// <param name="toDate">
    ///     To date search
    /// </param>
    public IAsyncEnumerable<Order> GetOrdersAsync(DateTime fromDate, DateTime toDate)
        => _orderServices.GetOrdersAsync(fromDate, toDate);
    /// <summary>
    ///     Represent method for get orders
    /// </summary>
    public IAsyncEnumerable<Order> GetOrdersAsync(int skip, int take, Guid idClient)
        => _orderServices.GetOrdersAsync(skip, take, idClient);
}