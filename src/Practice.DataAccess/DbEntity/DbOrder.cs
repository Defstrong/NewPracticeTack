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
public record DbOrder : DbEntity
{
    public DateTime DateOrder {get; set;}
    public string? Description {get; set;}
    public decimal Price {get; set;}
    public string PhoneNumberClient {get; set;}
} 