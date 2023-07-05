using Npgsql;
namespace Practice.DataAccess;

public sealed class DbOrderRepository : IDbRepository
{
    private readonly string ConnectionString; 
    private NpgsqlConnection? Connection;
    public DbOrderRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string Create(DbEntity entity)
    {
        OpenConnection();
        string createOrder = string.Format("INSERT INTO order_table (id_order, date_order, description, price) VALUES (@id_order, @date_order, @description, @price)");
        DbOrder order = (DbOrder)entity;
        
        using NpgsqlCommand command = new NpgsqlCommand(createOrder, Connection); 
        try
        {
            command.Parameters.AddWithValue("@id_order", order.Id);
            command.Parameters.AddWithValue("@date_order", order.DateOrder);
            command.Parameters.AddWithValue("@description", order.Description);
            command.Parameters.AddWithValue("@price", order.Price);
            command.ExecuteNonQuery();
            return "Create object completed successfully";
        }
        catch(Exception ex)
        {
            return $"Create object not completed. Error {ex.Message}";
        }
        finally
        {
            CloseConnection();
        }
    }

    public string Delete(int idOrder)
    {
        OpenConnection();

        string sqlCommand = string.Format("DELETE FROM order_table WHERE id_order = '{0}'", idOrder);
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, Connection);

        try
        {
            command.ExecuteNonQuery();
            return "Delete order completed successfully";
        }
        catch(Exception ex)
        {
            return $"Sorry but delete order not completed. Exception {ex.Message}";
        }
        finally
        {
            CloseConnection();
        }
    }
    
    public string Update(DbEntity entity, int idForUpdateOrder)
    {
        OpenConnection();
        
        DbOrder order = (DbOrder)entity;
        string sqlCommand = string.Format("UPDATE order_table SET description = '{0}' WHERE id_order = '{1}'",
                order.Description, order.Id);
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, Connection);

        try
        {
            command.ExecuteNonQuery();
            return "Update object completed successfully";
        }
        catch(Exception ex)
        {
            return $"Sorry but update object not completed. Exception {ex.Message}";
        }
        finally
        {
            CloseConnection();
        }
    }

    public IEnumerable<LogicalEntity> ReadAll()
    {
        OpenConnection();

        string sqlCOmmand = "SELECT * FROM order_table";
        List<Order> listEntity = new List<Order>();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, Connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        while(reader.Read())
        {
            listEntity.Add(
                new Order{
                    DateOrder = DateTime.Parse(reader.GetString(1)), 
                    Description = reader.GetString(2), 
                    Price = decimal.Parse(reader.GetValue(3).ToString())});
        }

        reader.Close();
        CloseConnection();

        return listEntity;
    }
    public LogicalEntity ReadOne(int idEntity)
    {
        OpenConnection();

        string sqlCOmmand = string.Format("SELECT * FROM order_table WHERE id_order = '{0}' LIMIT 1", idEntity);

        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, Connection);
        using NpgsqlDataReader reader = command.ExecuteReader();
        reader.Read();

        var order = new Order{
            DateOrder = DateTime.Parse(reader.GetString(1)), 
            Description = reader.GetString(2), 
            Price = decimal.Parse(reader.GetValue(3).ToString())};

        reader.Close();
        CloseConnection();

        return order;
    }

    private void OpenConnection()
    {
        Connection = new NpgsqlConnection(ConnectionString);
        Connection.Open();
    }

    private void CloseConnection()
    {
        Connection?.Close();
    }
}