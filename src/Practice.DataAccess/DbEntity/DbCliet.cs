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
    private readonly string? _firstName;
    private readonly string? _lastName;
    private readonly string? _address;
    private readonly string? _phoneNumber;
    /// <summary>
    ///     Represents a first name of client
    /// </summary>
    public string FirstName
    {
        get => _firstName ?? string.Empty;
        init => _firstName = value is {Length: > 0} 
        ? value : throw new ArgumentOutOfRangeException(nameof(value)); 
    }
    /// <summary>
    ///     Represents a last name of client.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The exception that is thrown when the value of an argument is outside the allowable
    ///     range of values as defined by the invoked method.
    /// </exception>
    public string LastName 
    {
        get => _lastName ?? string.Empty;
        init => _lastName = value is {Length: > 0} 
        ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    /// <summary>
    ///     Represent a address of client
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The exception that is thrown when the value of an argument is outside the allowable
    ///     range of values as defined by the invoked method.
    /// </exception>
    public string Address
    {
        get => _address ?? string.Empty;
        init => _address = value is {Length: > 0} 
        ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    /// <summary>
    ///     Represent a phone number of client
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The exception that is thrown when the value of an argument is outside the allowable
    ///     range of values as defined by the invoked method.
    /// </exception>
    public string PhoneNumber
    {
        get => _phoneNumber ?? string.Empty;
        init => _phoneNumber = value is {Length: > 0} 
        ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
}