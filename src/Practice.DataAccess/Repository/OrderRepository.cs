namespace Practice.DataAccess;

public sealed class OrderRepository : BaseRepository<DbOrder>, IOrderRepository
{
    public OrderRepository(IFileStorage<DbOrder> fileStorage, IDbRepository dbRepository)
        :base(fileStorage, dbRepository)
        {

        }
    /// <summary>
    ///     Represent method for get order with description
    /// </summary>
    /// <param name="description">
    ///     Description Order
    /// </param>
    public Task<DbOrder> GetOrderAsync(string description)
    {
        return Task.Run(() =>
        {
            var dbOrder = _entityList.First(x => x.Description == description);
            return dbOrder;
        });
    }
    public Task<DbOrder> GetOrderAsync(int orderId)
    {
        return Task.Run(() =>
        {
            var order = _entityList
                .First(x => x.Id == orderId);
            return order;
        });
    }
    public async IAsyncEnumerable<DbOrder> GetOrdersAsync()
    {
        foreach(var item in _entityList)
            yield return item;
    }

    public async IAsyncEnumerable<DbOrder> GetOrderAsync(DateTime fromDate, DateTime toDate)
    {
        foreach(var item in _entityList
            .Where(x => x.DateOrder >= fromDate && x.DateOrder <= toDate))
                yield return item;
    }
 
    public async IAsyncEnumerable<DbOrder> GetOrdersAsync(int take, int skip)
    {
        if(take + skip >= _entityList.Count && take > 0)
        {
            foreach(var item in _entityList.Skip(skip).Take(take))
                yield return item;
        } 
        else
            throw new ArgumentOutOfRangeException(nameof(take));
    }
    public async IAsyncEnumerable<DbOrder> GetOrdersAsync(string phoneNumberClient, int orderId)
    {
        foreach(var item in _entityList
            .Where(x => x.PhoneNumberClient == phoneNumberClient && x.Id == orderId))
                yield return item;
    }
}