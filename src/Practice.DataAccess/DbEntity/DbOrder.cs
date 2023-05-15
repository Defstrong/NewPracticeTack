namespace Practice.DataAccess;
/// <summary> 
///     Represent a order db entity
/// </summary>
/// <param name="DataOrder">
///     Order date
/// </param>
/// <param name="Description">
///     Order description
/// </param>
/// <param name="Price">
///     Order price
/// </param>
public record struct DbOrder
    (
        DateTime DateOrder,
        string Description,
        decimal Price
    ) : IBaseDbEntity
{
    public Guid Id { get ; set; } = Guid.Empty;
} 