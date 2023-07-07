using Npgsql;
using System.Data;
namespace Practice.DataAccess;

public sealed class DbClientRepository : IDbRepository<DbClient>
{

    private readonly NpgsqlConnection _connection;
    public DbClientRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public Task EnsureConnected()
        => _connection.State is not ConnectionState.Open ?
           _connection.OpenAsync() : 
            Task.CompletedTask;

    public async Task<int> CreateAsync(DbClient client)
    {
        await EnsureConnected();

        string createOrder = string.Format("INSERT INTO client (id_client, first_name, last_name, address, phone_number) VALUES (@id_client, @first_name, @last_name, @address, @phone_number)");
        using NpgsqlCommand command = new NpgsqlCommand(createOrder, _connection); 

        command.Parameters.AddWithValue("@id_client", client.Id);
        command.Parameters.AddWithValue("@first_name", client.FirstName);
        command.Parameters.AddWithValue("@last_name", client.LastName);
        command.Parameters.AddWithValue("@address", client.Address);
        command.Parameters.AddWithValue("@phone_number", client.PhoneNumber);

        await command.ExecuteNonQueryAsync();

        return client.Id;
    }
    
    public async Task DeleteAsync(int id)
    {
        await EnsureConnected();

        string sqlCommand = string.Format("DELETE FROM client WHERE id_client = @id_client"); 
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, _connection);

        await command.ExecuteNonQueryAsync();
    }
    
    public async Task UpdateAsync(DbClient client, int idOrder)
    {
        await EnsureConnected();

        string sqlCommand = string.Format("UPDATE client SET first_name = @first_name, last_name = @last_name, address = @address, phone_number = @phone_number WHERE id_client = @id_client");
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, _connection);

            command.Parameters.AddWithValue("@first_name", client.FirstName);
            command.Parameters.AddWithValue("@last_name", client.LastName);
            command.Parameters.AddWithValue("@address", client.Address);
            command.Parameters.AddWithValue("@phone_number", client.PhoneNumber);
            command.Parameters.AddWithValue("@id_client", client.Id);
        
        await command.ExecuteNonQueryAsync();
    }

    public async Task<DbClient> GetAsync(int idClient)
    {
        await EnsureConnected();

        string sqlCOmmand = string.Format("SELECT * FROM client WHERE id_client = '{0}' LIMIT 1", idClient);
        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, _connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        var client= new DbClient{
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Address = reader.GetString(3),
                    PhoneNumber = reader.GetString(4)};

        return client; 
    }

    public async Task<IEnumerable<DbClient>> GetAllAsync()
    {
        await EnsureConnected();

        string sqlCOmmand = "SELECT * FROM order_table";
        List<DbClient> listEntity = new List<DbClient>();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, _connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while(reader.Read())
        {
            listEntity.Add(
                new DbClient{
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Address = reader.GetString(3),
                    PhoneNumber = reader.GetString(4)});
        }

        return listEntity;
    }
}