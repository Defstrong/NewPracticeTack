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
public record DbClient: DbEntity
{
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Address {get; set;}
    public string PhoneNumber {get; set;}
}