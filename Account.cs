using System.Security.Cryptography;
using System.Text;
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
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        string saltstring = GetHexString(salt);

        byte[] fullbytes = System.Text.Encoding.UTF8.GetBytes(password + saltstring);

        using (HashAlgorithm algorithm = SHA256.Create())
        {
            byte[] hash = algorithm.ComputeHash(fullbytes);
            password = GetHexString(hash);
        }
        password += ":" + saltstring;

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

    private static string GetHexString(byte[] array)
    {
        StringBuilder sb = new StringBuilder();

        foreach(byte b in array)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }


    public User? Login(string username, string password)
    {
        var sql = @"SELECT * FROM users WHERE name = @username";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@username", username);

        using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        
        var user = new User
        {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2)
        };

        string [] passwordSplit = user.Password.Split(":");
        string storedHash = passwordSplit[0];
        string salt = passwordSplit[1];

        byte[] fullbytes = Encoding.UTF8.GetBytes(password + salt);
        string computeHash;
        using(HashAlgorithm algorithm = SHA256.Create())
        {
            byte[] hash = algorithm.ComputeHash(fullbytes);
            computeHash = GetHexString(hash);
        }
        
        if (!storedHash.Equals(computeHash))
        {
            continue;
        }

        loggedInUser = user.Id;

        return user;
      }

        return null;
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
        if (!reader.Read())
        {
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

    public void RemoveUser()
    {
        var user = GetLoggedInUser();


        var sql = @"
        BEGIN;
  DELETE FROM user_answers 
  WHERE user_id = @userId;
  
  DELETE FROM users 
  WHERE user_id = @userId;
COMMIT;";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@userId", user.Id);
        cmd.ExecuteNonQuery();
        Console.WriteLine($"{Colours.RED} your account has been deleted!{Colours.NORMAL}");


    }

}