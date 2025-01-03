using Npgsql;
public class Quiz : IQuizService
{
      private NpgsqlConnection connection;
    //   private IAccountSerice accountService;
    //   private IMenuService menuService;

    public Quiz( NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public List<Category> GetCategories()
    {
       var categories = new List<Category>();
       var sql = "SELECT category_id, name, description FROM categories";

       using var cmd = new NpgsqlCommand(sql, connection);
       using var reader = cmd.ExecuteReader();

       while(reader.Read())
       {
        categories.Add(new Category
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Description = reader.GetString(2)
        });
       }
       return categories;
    }

    public Category GetCategory(string name)
    {
       var sql = @"SELECT category_id, name, description FROM categories
       WHERE name = @name ";
       using var cmd = new NpgsqlCommand(sql, connection);
       cmd.Parameters.AddWithValue("@name", name);
       using var reader = cmd.ExecuteReader();
       if(reader.Read())
       {
        return new Category()
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Description = reader.GetString(2)
        };
       }
       return null;

    }

    public List<Question> GetQuestionsByCategory(string name)
    {
        throw new NotImplementedException();
    }

    public List<UserAnswer> GetUserHistory(Guid userId)
    {
        throw new NotImplementedException();
    }

    public int GetUserScore(Guid userId)
    {
        throw new NotImplementedException();
    }

    public bool SubmitAnswer(Guid userId, int questionId, int selectedOptionId)
    {
        throw new NotImplementedException();
    }
}