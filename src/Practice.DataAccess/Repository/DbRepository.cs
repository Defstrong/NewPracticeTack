using Npgsql;
namespace Practice.DataAccess;

public sealed class DbRepository : IDbRepository
{
    private readonly string ConnectionString; 
    private NpgsqlConnection? Connection;
    public DbRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public string Create(DbEntity entity)
    {
        OpenConnection();
        string createOrder = CreateSqlCommandForCreate(entity);
        
        using NpgsqlCommand command = CommandForAddData(createOrder, entity);
        try
        {
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
    public string Delete(string clientOrOrder, int idOrder)
    {
        OpenConnection();
        string sqlCommand = string.Empty;

        if(clientOrOrder == "DbOrder") 
            sqlCommand = string.Format("DELETE FROM order_table WHERE id_order = '{0}'", idOrder); 
        else if(clientOrOrder == "DbClient")
            sqlCommand = string.Format("DELETE FROM client WHERE id_client = '{0}'", idOrder); 

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, Connection);

        try
        {
            command.ExecuteNonQuery();
            return "Delete object completed successfully";
        }
        catch(Exception ex)
        {
            return $"Sorry but delete object not completed. Exception {ex.Message}";
        }
        finally
        {
            CloseConnection();
        }
    }
    
    public string Update(DbEntity dataForUpdate, int idForUpdateOrder)
    {
        OpenConnection();
        string sqlCommand = CreateSqlCommandForUpdate(dataForUpdate);
        
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
    public string Read(int idEntity)
    {
        return default;
    }
    private string CreateSqlCommandForUpdate(DbEntity entity)
    {
        if(entity is DbOrder)
        {
            DbOrder order = (DbOrder)entity;
            return string.Format("UPDATE order_table SET description = '{0}' WHERE id_order = '{1}'", 
                order.Description, order.Id);
        }
        else
        {
            DbClient client = (DbClient)entity;
            return string.Format("UPDATE client SET first_name = '{0}', last_name = '{1}', address = '{2}', phone_number = '{3}' WHERE id_client = '{4}'",
                client.FirstName, client.LastName, client.Address, client.PhoneNumber, client.Id);
        }
    }

    private string CreateSqlCommandForCreate(DbEntity entity)
    {
        if(entity is DbOrder)
        {
            DbOrder order = (DbOrder)entity;
            return string.Format("INSERT INTO order_table (id_order, date_order, description, price) VALUES (@id_order, @date_order, @description, @price)");
        }
        else
        {
            DbClient client = (DbClient)entity;
            return string.Format("INSERT INTO client (id_client, first_name, last_name, address, phone_number) VALUES (@id_client, @first_name, @last_name, @address, @phone_number)");
        }
    }
    private NpgsqlCommand CommandForAddData(string sqlCommand, DbEntity entity)
    {
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand,Connection);
        if(entity is DbOrder)
        {
            DbOrder order = (DbOrder)entity;
            Console.WriteLine(order.Id + "Hello");
            command.Parameters.AddWithValue("@id_order", order.Id);
            command.Parameters.AddWithValue("@date_order", order.DateOrder);
            command.Parameters.AddWithValue("@description", order.Description);
            command.Parameters.AddWithValue("@price", order.Price);
        }
        else
        {
            DbClient client = (DbClient)entity;
            command.Parameters.AddWithValue("@id_client", client.Id);
            command.Parameters.AddWithValue("@first_name", client.FirstName);
            command.Parameters.AddWithValue("@last_name", client.LastName);
            command.Parameters.AddWithValue("@address", client.Address);
            command.Parameters.AddWithValue("@phone_number", client.PhoneNumber);
        }
        return command;
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