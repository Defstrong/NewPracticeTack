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
    public void CreateOrder(string phoneNumberClinet, Order order)
        => _orderServices.CreateOrder(phoneNumberClinet, order);
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
    public void UpdateOrder(Order order, Guid orderId)
        => _orderServices.UpdateOrder(order, orderId);
    /// <summary>
    ///     Represent method for get all orders
    /// </summary>
    public IEnumerable<Order> GetOrders()
        => _orderServices.GetOrders();
    /// <summary>
    ///     Represent method for get orders with date
    /// </summary>
    /// <param name="fromDate">
    ///     From date search
    /// </param>
    /// <param name="toDate">
    ///     To date search
    /// </param>
    public IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate)
        => _orderServices.GetOrders(fromDate, toDate);
    /// <summary>
    ///     Represent method for get orders
    /// </summary>
    public IEnumerable<Order> GetOrders(int skip, int take, Guid idClient)
        => _orderServices.GetOrders(skip, take, idClient);
}