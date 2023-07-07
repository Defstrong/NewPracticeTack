using Npgsql;
using System.Data;
namespace Practice.DataAccess;

public sealed class DbOrderRepository : IDbRepository<DbOrder>
{
    private readonly NpgsqlConnection _connection;
    public DbOrderRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public Task EnsureConnected()
        => _connection.State is not ConnectionState.Open ?
           _connection.OpenAsync() : 
            Task.CompletedTask;

    public async Task<int> CreateAsync(DbOrder order)
    {
        await EnsureConnected();

        string createOrder = "INSERT INTO order_table (id_order, date_order, description, price) VALUES (@id_order, @date_order, @description, @price";
        using NpgsqlCommand command = new NpgsqlCommand(createOrder, _connection); 

        command.Parameters.AddWithValue("@id_order", order.Id);
        command.Parameters.AddWithValue("@date_order", order.DateOrder);
        command.Parameters.AddWithValue("@description", order.Description);
        command.Parameters.AddWithValue("@price", order.Price);

        await command.ExecuteNonQueryAsync();

        return order.Id;
    }

    public async Task<DbOrder> GetAsync(int idOrder)
    {
        await EnsureConnected();

        string sqlCOmmand = string.Format("SELECT * FROM order_table WHERE id_order = '{0}' LIMIT 1", idOrder);
        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, _connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        var order = new DbOrder{
            DateOrder = DateTime.Parse(reader.GetString(1)), 
            Description = reader.GetString(2), 
            Price = decimal.Parse(reader.GetValue(3).ToString())};

        return order; 
    }

    public async Task<IEnumerable<DbOrder>> GetAllAsync()
    {
        await EnsureConnected();

        string sqlCOmmand = "SELECT * FROM order_table";
        List<DbOrder> listEntity = new List<DbOrder>();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, _connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while(reader.Read())
        {
            listEntity.Add(
                new DbOrder{
                    DateOrder = DateTime.Parse(reader.GetString(1)), 
                    Description = reader.GetString(2), 
                    Price = decimal.Parse(reader.GetValue(3).ToString())});
        }

        return listEntity;
    }

    public async Task UpdateAsync(DbOrder order, int idOrder)
    {
        await EnsureConnected();

        string sqlCommand = string.Format("UPDATE order_table SET description = @description WHERE id_order = @id_order");
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, _connection);

        command.Parameters.AddWithValue("@description", order.Description);
        command.Parameters.AddWithValue("@id_order", idOrder);
        
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await EnsureConnected();

        string sqlCommand = string.Format("DELETE FROM order_table WHERE id_order = @idOrder");
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, _connection);

        await command.ExecuteNonQueryAsync();
    }
}