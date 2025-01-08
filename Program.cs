namespace QuizApp;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=Localhost;Username=postgres;Password=password;Database=group6quiz_app";
        using var connection = new NpgsqlConnection(connectionString);

        connection.Open();

        var createTableSql = @"CREATE TABLE IF NOT EXISTS users (user_id UUID PRIMARY KEY,
        name TEXT UNIQUE,
        password TEXT UNIQUE );



        CREATE TABLE IF NOT EXISTS categories (
    category_id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
    );
        
        CREATE TABLE IF NOT EXISTS questions (question_id INT PRIMARY KEY,
        category_id INTEGER REFERENCES categories(category_id) ON DELETE CASCADE,
        question TEXT NOT NULL,
        UNIQUE(question_id)
        );
        
        CREATE TABLE IF NOT EXISTS question_options (
    option_id SERIAL PRIMARY KEY,
    question_id INTEGER REFERENCES questions(question_id) ON DELETE CASCADE,
    option_text TEXT NOT NULL,
    is_correct BOOLEAN NOT NULL 
    );
    
    CREATE TABLE IF NOT EXISTS user_answers (
    answer_id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES users(user_id),
    question_id INTEGER REFERENCES questions(question_id) ON DELETE CASCADE,
    selected_option_id INTEGER REFERENCES question_options(option_id) ,
    is_correct BOOLEAN NOT NULL
   
    );";
     using var cmd = new NpgsqlCommand(createTableSql, connection);
        cmd.ExecuteNonQuery();

//question options


        var insertSql =@"INSERT INTO categories (name, description) VALUES 
    ('sports', 'Questions about sports and athletes'),
    ('history', 'Questions about historical events and figures'),
    ('geography', 'Questions about countries, capitals, and geography') ON CONFLICT (name) DO NOTHING;";
    using var insertCmd = new NpgsqlCommand(insertSql, connection);
    insertCmd.ExecuteNonQuery();



    var insertQuestionsSql = @"INSERT INTO questions (question_id, category_id, question) 
VALUES 
    -- Sports
    (1, 1, 'Which country won the FIFA World Cup in 2018?'),
    (2, 1, 'How many players are there in a basketball team on the court?'),
    

    -- History
    (3, 2, 'In which year did World War II end?'),
    (4, 2, 'Who was the first emperor of the Roman Empire?'),

    -- Geography
    (5, 3, 'What is the capital of Japan?'),
    (6, 3, 'Which country is known as the Land of the Midnight Sun?') ON CONFLICT (question_id) DO NOTHING;";
    using var insertQuestionsCmd = new NpgsqlCommand(insertQuestionsSql, connection);
    insertQuestionsCmd.ExecuteNonQuery();



            var insertOptionSql = @"INSERT INTO question_options (question_id, option_text, is_correct) VALUES 
    (1, 'France', TRUE),
    (1, 'Germany', FALSE),
    (1, 'Brazil', FALSE),
    (1, 'Argentina', FALSE),

    (2, '5', TRUE),
    (2, '6', FALSE),
    (2, '7', FALSE),
    (2, '4', FALSE),

    -- Options for History questions
    (3, '1945', TRUE),
    (3, '1940', FALSE),
    (3, '1939', FALSE),
    (3, '1950', FALSE),

    (4, 'Augustus', TRUE),
    (4, 'Julius Caesar', FALSE),
    (4, 'Nero', FALSE),
    (4, 'Caligula', FALSE),

    -- Options for Geography questions
    (5, 'Tokyo', TRUE),
    (5, 'Seoul', FALSE),
    (5, 'Beijing', FALSE),
    (5, 'Bangkok', FALSE),

    (6, 'Norway', TRUE),
    (6, 'Sweden', FALSE),
    (6, 'Canada', FALSE),
    (6, 'Iceland', FALSE) ;";

    using var insertQuestionsOptionsCmd = new NpgsqlCommand(insertOptionSql, connection);

    insertQuestionsOptionsCmd.ExecuteNonQuery();



        IAccountService accountService = new Account(connection);
        IMenuService menuService = new SimpleMenuService();
        IQuizService quizService = new Quiz(connection);
        

        Menu initialMenu = new LoginMenu(accountService, menuService, quizService);
        menuService.SetMenu(initialMenu);

         while(true)
          {
            try
            {
            Console.WriteLine("> ");
            string? inputCommand = Console.ReadLine()!;
            if (inputCommand.ToLower() != null)
             {
                menuService.GetMenu().ExecuteCommand(inputCommand);
            }
             else
             {
                break;
            }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


              
            

    }
}
}
