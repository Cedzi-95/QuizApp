using Npgsql;
public class postgresQuizService : IQuizService
{


      private NpgsqlConnection connection;
    
    public postgresQuizService( NpgsqlConnection connection)
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

         var questions = new List<Question>();
        var sql = @"
            SELECT q.question_id, q.category_id, q.question, 
                   qo.option_id, qo.option_text, qo.is_correct
            FROM questions q
            INNER JOIN question_options qo ON q.question_id = qo.question_id
            INNER JOIN categories c on c.category_id = q.category_id
            WHERE c.name = @name";

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@name", name);
        using var reader = cmd.ExecuteReader();

        Question currentQuestion = null;
        while (reader.Read())
        {
            var questionId = reader.GetInt32(0);
            
            if (currentQuestion == null || currentQuestion.Id != questionId)
            {
                currentQuestion = new Question
                {
                    Id = questionId,
                    CategoryId = reader.GetInt32(1),
                    QuestionText = reader.GetString(2)
                };
                questions.Add(currentQuestion);
            }

            currentQuestion.Options.Add(new QuestionOption
            {
                Id = reader.GetInt32(3),
                QuestionId = questionId,
                OptionText = reader.GetString(4),
                IsCorrect = reader.GetBoolean(5)
            });
        }
        return questions;
    }

   public List<UserAnswer> GetUserHistory(Guid userId)
{
    var userAnswers = new List<UserAnswer>();
    var sql = @"SELECT answer_id, user_id, question_id, selected_option_id, is_correct
                FROM user_answers
                WHERE user_id = @userId";

    using var cmd = new NpgsqlCommand(sql, connection);
    cmd.Parameters.AddWithValue("@userId", userId);

    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        userAnswers.Add(new UserAnswer
        {
            Id = reader.GetInt32(0), // answer_id
            UserId = reader.GetGuid(1),
            QuestionId = reader.GetInt32(2),
            SelectedOptionId = reader.GetInt32(3),
            IsCorrect = reader.GetBoolean(4)
        });
    }

    return userAnswers;
}

    public int GetUserScore(Guid userId)
    {
       var sql = @"SELECT COUNT(*) is_correct FROM user_answers
       WHERE user_id = @userId AND is_correct = true";

       using var cmd = new NpgsqlCommand(sql, connection);
       cmd.Parameters.AddWithValue("@userId", userId);


       return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool SubmitAnswer(Guid userId, int questionId, int selectedOptionId)
    {
       //query from question_options table to show answer options
        var sql = @"SELECT is_correct FROM question_options WHERE option_id = @optionId AND question_id = @questionId";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@optionId", selectedOptionId);
        cmd.Parameters.AddWithValue("@questionId", questionId);
        var isCorrect = (bool)cmd.ExecuteScalar();

        //lägger in user svaret i user_answers tabellen
        var userAnswerSql = @"INSERT INTO user_answers (user_id, question_id, selected_option_id, is_correct) VALUES
        (@userId, @questionId, @optionId, @isCorrect)";
        using var userAnswerCmd = new NpgsqlCommand(userAnswerSql, connection);
        userAnswerCmd.Parameters.AddWithValue("@userId", userId);
        userAnswerCmd.Parameters.AddWithValue("@questionId", questionId);
        userAnswerCmd.Parameters.AddWithValue("@optionId", selectedOptionId);
        userAnswerCmd.Parameters.AddWithValue("@isCorrect", isCorrect);

       userAnswerCmd.ExecuteNonQuery();

        return isCorrect;

    }
}