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
    DbClient GetClient(Guid idClient);
    IEnumerable<DbClient> GetByFirstName(string firstName);
    IEnumerable<DbClient> GetByLastName(string lastName);
    IEnumerable<DbClient> GetByAddress(string address);
    IEnumerable<DbClient> GetByPhoneNumber(string phoneNumber);
    IEnumerable<DbClient> GetClients();

}
