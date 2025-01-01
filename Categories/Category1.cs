using Npgsql;
public class Category1
{
     private NpgsqlConnection connection;
    public int categoryId {get; init;}
    public string? name {get; init;}
    public string? description {get; init;}
    public void Execute()
    {

    }

    private void question()
    {
      var sql = @"insert into questions (question_id, category_id, question) VALUES
      (1,1,'Which country won the recent world cup);";
      using var cmd = new NpgsqlCommand (sql, connection);

    

    
    }
}

