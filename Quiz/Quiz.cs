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

    public Category GetCategory(int id)
    {
       var sql = @"SELECT category_id, name, description FROM categories
       WHERE category_id = @id ";
       using var cmd = new NpgsqlCommand(sql, connection);
       cmd.Parameters.AddWithValue("@id", id);
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

    public List<Question> GetQuestionsByCategory(int categoryId)
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