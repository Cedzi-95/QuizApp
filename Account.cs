using Npgsql;
public class Account : IAccountService
{
    private NpgsqlConnection connection;

    private Guid? loggedInUser = null;

    public Account(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public User RegisterUser(string username, string password)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = username,
            Password = password
        };

        var sql = @"INSERT INTO users (user_id, name, password) VALUES (
            @id,
            @name,
            @password
        )";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@id", user.Id);
        cmd.Parameters.AddWithValue("@name", user.Name);
        cmd.Parameters.AddWithValue("@password", user.Password);

        cmd.ExecuteNonQuery();

        return user;
    }

    
    public User? Login(string username, string password)
    {
        var sql = @"SELECT * FROM users WHERE name = @username AND password = @password";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) {
            return null;
        }

        var user = new User {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2)
        };

        loggedInUser = user.Id;

        return user;
    }

    public void Logout()
    {
        loggedInUser = null;
    }

    public User? GetLoggedInUser()
    {
        if (loggedInUser == null) 
        {
            return null;
        }

        var sql = @"SELECT * FROM users WHERE user_id = @id";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@id", loggedInUser);
        
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) {
            return null;
        }

        var user = new User 
        {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2)
        };

        return user;
    }

 public bool DeleteAccount(Guid userId)
{
    try
    {
        connection.Open();

        string query = "DELETE FROM Users WHERE Id = @Id";
        using (var command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", userId);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting account: {ex.Message}");
        return false;
    }
    finally
    {
        if (connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
        }
    }
}

}