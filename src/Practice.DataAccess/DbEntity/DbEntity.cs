namespace Practice.DataAccess;
/// <summary>
///     Represent a parent class for db entities.
/// </summary>
public abstract record DbEntity
{
    /// <summary>
    ///     Represent identificed entity.
    /// </summary>
    public int Id {get; set;}
}