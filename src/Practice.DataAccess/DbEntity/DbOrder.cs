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
    private readonly DateTime? _dateOrder;
    private readonly string? _descritption;
    private readonly decimal? _price;
    private readonly Guid? id;
    public Guid IdClient {get; set;}
    private readonly string? _phoneNumberClient;

    /// <summary>
    ///     Represent date of order
    /// </summary>
    public DateTime DateOrder
    {
        get => _dateOrder ?? new DateTime();
        init => _dateOrder = value;
    }
    /// <summary>
    ///     Represent description of order
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The exception that is thrown when the value of an argument is outside the allowable
    ///     range of values as defined by the invoked method.
    /// </exception>
    public string Description
    {
        get => _descritption ?? string.Empty;
        init => _descritption = value is {Length: > 0} 
        ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    /// <summary>
    ///     Represent price of order
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The exception that is thrown when the value of an argument is outside the allowable
    ///     range of values as defined by the invoked method.
    /// </exception>
    public decimal Price
    {
        get => _price ?? 0.0m;
        init => _price = value > 0 
        ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    public string PhoneNumberClient
    {
        get => _phoneNumberClient ?? string.Empty;
        init => _phoneNumberClient = value is {Length: > 0} 
        ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
} 