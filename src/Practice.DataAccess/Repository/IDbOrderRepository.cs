namespace Practice.DataAccess;

public interface IDbOrderRepository : IBaseRepository<DbOrder> 
{
    Task GetOrderById(int idOrder);
}