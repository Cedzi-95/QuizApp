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
    selected_option_id INTEGER REFERENCES question_options(option_id) ON DELETE CASCADE,
    is_correct BOOLEAN NOT NULL
    );";
     using var cmd = new NpgsqlCommand(createTableSql, connection);
        cmd.ExecuteNonQuery();




//         var insertSql =@"INSERT INTO categories (name, description) VALUES 
//     ('sports', 'Questions about sports and athletes'),
//     ('history', 'Questions about historical events and figures'),
//     ('geography', 'Questions about countries, capitals, and geography') ON CONFLICT (name) DO NOTHING;";
//     using var insertCmd = new NpgsqlCommand(insertSql, connection);
//     insertCmd.ExecuteNonQuery();



//     var insertQuestionsSql = @"INSERT INTO questions (question_id, category_id, question) 
// VALUES 
//     -- Sports

//     (1, 1, 'Which country won the FIFA World Cup in 2018?'),
//     (2, 1, 'How many players are there in a basketball team on the court?'),
//     (3, 1, 'Who holds the record for the most Olympic gold medals?'),
//     (4, 1, 'What is the maximum score in a single game of ten-pin bowling?'),
//     (5, 1, 'Which country is famous for the sport sumo wrestling?'),
    

//     -- History
//     (6, 2, 'In which year did World War II end?'),
//     (7, 2, 'Who was the first emperor of the Roman Empire?'),
//     (8, 2, 'What was the name of the ship that sank on its maiden voyage in 1912?'),
//     (9, 2, 'Which treaty ended World War I?'),
//     (10, 2, 'Who discovered the Americas in 1492?'),


//     -- Geography
//     (11, 3, 'What is the capital of Japan?'),
//     (12, 3, 'Which country is known as the Land of the Midnight Sun?'),
//     (13, 3, 'What is the longest river in the world?'),
//     (14, 3, 'What is the smallest country in the world?'),
//     (15, 3, 'Which desert is the largest in the world?') ON CONFLICT (question_id) DO NOTHING;";
//     using var insertQuestionsCmd = new NpgsqlCommand(insertQuestionsSql, connection);
//     insertQuestionsCmd.ExecuteNonQuery();

//     //question options
//         var insertOptionSql = @"INSERT INTO question_options (question_id, option_text, is_correct) VALUES 
//      -- Sports
//     (1, 'France', TRUE),
//     (1, 'Germany', FALSE),
//     (1, 'Brazil', FALSE),
//     (1, 'Argentina', FALSE),
//     (1, 'Italy', FALSE),

//     (2, '5', TRUE),
//     (2, '6', FALSE),
//     (2, '7', FALSE),
//     (2, '8', FALSE),
//     (2, '9', FALSE),

//     (3, 'Michael Phelps', TRUE),
//     (3, 'Usain Bolt', FALSE),
//     (3, 'Carl Lewis', FALSE),
//     (3, 'Mark Spitz', FALSE),
//     (3, 'Simone Biles', FALSE),

//     (4, '300', FALSE),
//     (4, '450', FALSE),
//     (4, '500', FALSE),
//     (4, '600', TRUE),
//     (4, '750', FALSE),

//     (5, 'China', FALSE),
//     (5, 'Japan', TRUE),
//     (5, 'South Korea', FALSE),
//     (5, 'Thailand', FALSE),
//     (5, 'Mongolia', FALSE),

//     -- History
//     (6, '1945', TRUE),
//     (6, '1939', FALSE),
//     (6, '1940', FALSE),
//     (6, '1950', FALSE),
//     (6, '1960', FALSE),

//     (7, 'Julius Caesar', FALSE),
//     (7, 'Augustus', TRUE),
//     (7, 'Nero', FALSE),
//     (7, 'Caligula', FALSE),
//     (7, 'Trajan', FALSE),

//     (8, 'Titanic', TRUE),
//     (8, 'Lusitania', FALSE),
//     (8, 'Queen Mary', FALSE),
//     (8, 'Carpathia', FALSE),
//     (8, 'Bismarck', FALSE),

//     (9, 'Treaty of Versailles', TRUE),
//     (9, 'Treaty of Paris', FALSE),
//     (9, 'Treaty of Tordesillas', FALSE),
//     (9, 'Treaty of Ghent', FALSE),
//     (9, 'Treaty of Utrecht', FALSE),

//     (10, 'Christopher Columbus', TRUE),
//     (10, 'Ferdinand Magellan', FALSE),
//     (10, 'Amerigo Vespucci', FALSE),
//     (10, 'Marco Polo', FALSE),
//     (10, 'Leif Erikson', FALSE),

//     -- Options for Geography questions
//     (11, 'Tokyo', TRUE),
//     (11, 'Kyoto', FALSE),
//     (11, 'Osaka', FALSE),
//     (11, 'Nagoya', FALSE),

//     -- Fråga 2
//     (12, 'Norway', TRUE),
//     (12, 'Sweden', FALSE),
//     (12, 'Finland', FALSE),
//     (12, 'Iceland', FALSE),

//     -- Fråga 3
//     (13, 'Nile', TRUE),
//     (13, 'Amazon', FALSE),
//     (13, 'Yangtze', FALSE),
//     (13, 'Mississippi', FALSE),

//     -- Fråga 4
//     (14, 'Vatican City', TRUE),
//     (14, 'Monaco', FALSE),
//     (14, 'San Marino', FALSE),
//     (14, 'Liechtenstein', FALSE),

//     -- Fråga 5
//     (15, 'Sahara', TRUE),
//     (15, 'Arctic', FALSE),
//     (15, 'Gobi', FALSE),
//     (15, 'Kalahari', FALSE);";

    // using var insertquestionsoptionscmd = new NpgsqlCommand(insertOptionSql, connection);

    // insertquestionsoptionscmd.ExecuteNonQuery();

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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        
}
}
