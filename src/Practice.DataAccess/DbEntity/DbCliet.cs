namespace Practice.DataAccess;
///<summary>
///     Represent a client db entity
///</summary>
///<param name = "Id">
///     Unique identification
///</param>
///<param name = "FirstName">
///     Client First Name
///</param>
///<param name = "LastName">
///     Client Last Name
///</param>
///<param name = "Address">
///     Client address
///</param>
///<param name = "PhoneNumber">
///     Client Phone Number
///</param>
public record struct DbClient
    (
        string FirstName,
        string LastName,
        string Address,
        string PhoneNumber
    ) : IBaseDbEntity
{
    public Guid Id { get ; set; } = Guid.Empty;
}