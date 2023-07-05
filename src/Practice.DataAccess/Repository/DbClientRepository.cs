using Npgsql;
namespace Practice.DataAccess;

public sealed class DbClientRepository : IDbRepository
{
    private readonly string ConnectionString; 
    private NpgsqlConnection? Connection;
    public DbClientRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string Create(DbEntity entity)
    {
        OpenConnection();

        string createOrder = string.Format("INSERT INTO client (id_client, first_name, last_name, address, phone_number) VALUES (@id_client, @first_name, @last_name, @address, @phone_number)");
        using NpgsqlCommand command = new NpgsqlCommand(createOrder, Connection);

        DbClient client = (DbClient)entity;

        try
        {
            command.Parameters.AddWithValue("@id_client", client.Id);
            command.Parameters.AddWithValue("@first_name", client.FirstName);
            command.Parameters.AddWithValue("@last_name", client.LastName);
            command.Parameters.AddWithValue("@address", client.Address);
            command.Parameters.AddWithValue("@phone_number", client.PhoneNumber);
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
        
        string sqlCommand = string.Format("DELETE FROM client WHERE id_client = '{0}'", idOrder); 
        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, Connection);

        try
        {
            command.ExecuteNonQuery();
            return "Delete client completed successfully";
        }
        catch(Exception ex)
        {
            return $"Sorry but delete client not completed. Exception {ex.Message}";
        }
        finally
        {
            CloseConnection();
        }
    }
    
    public string Update(DbEntity entity, int idForUpdateOrder)
    {
        OpenConnection();

        DbClient client = (DbClient)entity;

        string sqlCommand = string.Format("UPDATE client SET first_name = '{0}', last_name = '{1}', address = '{2}', phone_number = '{3}' WHERE id_client = '{4}'",
                client.FirstName, client.LastName, client.Address, client.PhoneNumber, client.Id);
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

        string sqlCOmmand = "SELECT * FROM client";
        List<Client> listEntity = new List<Client>();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, Connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        while(reader.Read())
        {
            listEntity.Add(
                new Client{
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Address = reader.GetString(3),
                    PhoneNumber = reader.GetString(4)});
        }

        reader.Close();
        CloseConnection();

        return listEntity;
    }

    public LogicalEntity ReadOne(int idEntity)
    {
        OpenConnection();

        string sqlCOmmand = string.Format("SELECT * FROM client WHERE id_order = '{0}' LIMIT 1", idEntity);

        using NpgsqlCommand command = new NpgsqlCommand(sqlCOmmand, Connection);
        using NpgsqlDataReader reader = command.ExecuteReader();
        reader.Read();

        var order = new Order{
            DateOrder = DateTime.Parse(reader.GetString(1)), 
            Description = reader.GetString(2), 
            Price = (decimal)reader.GetValue(3)};

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