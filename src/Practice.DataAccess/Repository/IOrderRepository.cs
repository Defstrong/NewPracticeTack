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
    public DbOrder GetOrder(string description);
    public DbOrder GetOrder( Guid orderId);
    public IEnumerable<DbOrder> GetOrders();
    public IEnumerable<DbOrder> GetOrder(DateTime fromDate, DateTime toDate);
    public IEnumerable<DbOrder> GetOrders(int take, int skip);
    public IEnumerable<DbOrder> GetOrders(string phoneNumberClient, Guid orderId);
}
