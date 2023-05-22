namespace Practice.DataAccess;

public sealed class OrderRepository : BaseRepository<DbOrder>, IOrderRepository
{
    public OrderRepository(IFileStorage<DbOrder> fileStorage)
        :base(fileStorage)
        {

        }
    /// <summary>
    ///     Represent method for get order with description
    /// </summary>
    /// <param name="description">
    ///     Description Order
    /// </param>
    public DbOrder GetOrder(string description)
    {
        var dbOrder = _entityList.First(x => x.Description == description);
        return dbOrder;
    }
    public DbOrder GetOrder(Guid orderId)
    {
        var order = _entityList
            .First(x => x.Id == orderId);
        return order;
    }
    public IEnumerable<DbOrder> GetOrders() =>
        _entityList;

    public IEnumerable<DbOrder> GetOrder(DateTime fromDate, DateTime toDate) =>
        _entityList.Where(x => x.DateOrder >= fromDate && x.DateOrder <= toDate);
 
    public IEnumerable<DbOrder> GetOrders(int take, int skip)
    {
        if(take + skip >= _entityList.Count && take > 0)
            return _entityList.Skip(skip).Take(take);
        else
            throw new ArgumentOutOfRangeException();
    }
    public IEnumerable<DbOrder> GetOrders(string phoneNumberClient, Guid orderId)
    {
        var order = _entityList
            .Where(x => x.PhoneNumberClient == phoneNumberClient && x.Id == orderId);
        return order;
    }
}