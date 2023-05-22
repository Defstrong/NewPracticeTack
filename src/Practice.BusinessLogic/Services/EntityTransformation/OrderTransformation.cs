using Practice.DataAccess;
namespace Practice.BusinessLogic;

public static class OrderTransformation 
{
    public static Order DbOrderToOrder(this DbOrder order)
    {
        return new Order {Description = order.Description, Price = order.Price, 
            DateOrder = order.DateOrder};
    }
    public static DbOrder OrderToDbOrder(this Order order)
    {
        return new DbOrder {Description = order.Description, Price = order.Price, 
            DateOrder = order.DateOrder};
    }
}