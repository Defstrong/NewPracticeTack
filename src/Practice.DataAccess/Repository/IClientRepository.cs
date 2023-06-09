namespace Practice.DataAccess;

/// <summary>
///     Repreesent order repository
/// </summary>

public interface IClientRepository : IBaseRepository<DbClient>
{
    /// <summary>
    ///     Returns list of order that match by specified description.
    /// </summary>
    /// <param name"description"> Order description</param>
    /// <returns>List of DbOrder</returs>
    Task<DbClient> GetClientAsync(Guid idClient);
    IAsyncEnumerable<DbClient> GetByFirstNameAsync(string firstName);
    IAsyncEnumerable<DbClient> GetByLastNameAsync(string lastName);
    IAsyncEnumerable<DbClient> GetByAddressAsync(string address);
    IAsyncEnumerable<DbClient> GetByPhoneNumberAsync(string phoneNumber);
    IAsyncEnumerable<DbClient> GetClientsAsync();

}
