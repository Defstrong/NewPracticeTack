namespace Practice.DataAccess;

/// <summary>
///     Represent record of order
/// </summary>

public  record Order : LogicalEntity
{
    private readonly DateTime? _dateOrder;
    private readonly string? _descritption;
    private readonly decimal? _price;
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
    public override string ToString()
    {
        return $"Date order {DateOrder}\tDescription {Description}\tPrice {Price}";
    }
}