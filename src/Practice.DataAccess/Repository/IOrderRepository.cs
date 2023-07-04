namespace Practice.DataAccess;

/// <summary>
///     Repreesent order repository
/// </summary>

public interface IOrderRepository : IBaseRepository<DbOrder>
{
    /// <summary>
    ///     Returns list of order that match by specified description.
    /// </summary>
    /// <param name"description"> Order description</param>
    /// <returns>List of DbOrder</returs>
    public Task<DbOrder> GetOrderAsync(string description);
    public Task<DbOrder> GetOrderAsync(int orderId);
    public IAsyncEnumerable<DbOrder> GetOrdersAsync();
    public IAsyncEnumerable<DbOrder> GetOrderAsync(DateTime fromDate, DateTime toDate);
    public IAsyncEnumerable<DbOrder> GetOrdersAsync(int take, int skip);
    public IAsyncEnumerable<DbOrder> GetOrdersAsync(string phoneNumberClient, int orderId);
}
