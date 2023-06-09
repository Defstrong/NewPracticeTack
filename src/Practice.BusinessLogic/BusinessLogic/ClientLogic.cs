using Practice.DataAccess;
namespace Practice.BusinessLogic;

/// <summary>
///     Represent class for clinet logic
/// </summary>
public sealed class ClientLogic
{
    IOrderServices _orderServices;
    /// <summary>
    ///     Constructor to get an instance
    /// </summary>
    /// <param name="orderServices">
    ///     Provides an instance order services
    /// </param>
    public ClientLogic(IOrderServices orderServices)
    {
        _orderServices = orderServices;
    }
    /// <summary>
    ///     Represent method for get order with order identificer
    /// </summary>
    /// <param name="orderId">
    ///     Order identificer for get order
    /// </param>
    public Task<Order> GetOrderAsync(Guid orderId)
        => _orderServices.GetOrderAsync(orderId);

    /// <summary>
    ///     Represent method for get orders in a certain depozone 
    /// </summary>
    /// <param name="take">
    ///    Take N element at list order 
    /// </param>
    /// <param name="skip">
    ///     Skip N element at list order
    /// </param>
    public IAsyncEnumerable<Order> GetOrdersAsync(int take, int skip, Guid idClient)
        => _orderServices.GetOrdersAsync(take, skip,idClient);
    
    /// <summary>
    ///     Represent method for delete order with order identificer
    /// </summary>
    /// <param name="orderId">
    ///     Order identificer for delete order
    /// </param>
    public Task<bool> DeleteOrderAsync(Guid orderId)
        => _orderServices.DeleteOrderAsync(orderId);
    
    /// <summary>
    ///     Represent method for update order with order identificer
    /// </summary>
    /// <param name="order">
    ///     New order for update
    /// </param>
    /// <param name="orderId">
    ///     Order identificer for delete order
    /// </param>
    /// <param name="phoneNumberClient">
    ///     Phone number client for call
    /// </param>
    public Task UpdateOrderAsync(Order order, Guid orderId)
        => _orderServices.UpdateOrderAsync(order, orderId);
}