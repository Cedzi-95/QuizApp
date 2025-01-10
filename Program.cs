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
   (1, 1, 'Which country won the FIFA World Cup 2022?'),
(2, 1, 'Who holds the record for most Olympic medals in history?'),
(3, 1, 'In which sport would you perform a slam dunk?'),
(4, 1, 'What is the length of a standard Olympic swimming pool?'),
(5, 1, 'Which tennis player has won the most Grand Slam titles in men''s singles?');

    

    -- History
    (6, 2, 'In which year did World War II end?'),
(7, 2, 'Who was the first President of the United States?'),
(8, 2, 'Which ancient wonder of the world was located in Egypt?'),
(9, 2, 'Who painted the Mona Lisa?'),
(10, 2, 'Which empire was ruled by Julius Caesar?');

    -- Geography
   (11, 3, 'What is the capital of Japan?'),
(12, 3, 'Which is the largest continent by land area?'),
(13, 3, 'Which river is the longest in the world?'),
(14, 3, 'What is the smallest country in the world?'),
(15, 3, 'Which desert is the largest hot desert in the world?')
     ;";
    using var insertQuestionsCmd = new NpgsqlCommand(insertQuestionsSql, connection);
    insertQuestionsCmd.ExecuteNonQuery();



            var insertOptionSql = @"INSERT INTO question_options (question_id, option_text, is_correct) VALUES 
   (1, 'Argentina', true),
(1, 'France', false),
(1, 'Brazil', false),
(1, 'Germany', false),

(2, 'Michael Phelps', true),
(2, 'Usain Bolt', false),
(2, 'Simone Biles', false),
(2, 'Carl Lewis', false),

(3, 'Basketball', true),
(3, 'Volleyball', false),
(3, 'Soccer', false),
(3, 'Tennis', false),
(4, '50 meters', true),
(4, '25 meters', false),
(4, '100 meters', false),
(4, '75 meters', false),

(5, 'Novak Djokovic', true),
(5, 'Rafael Nadal', false),
(5, 'Roger Federer', false),
(5, 'Pete Sampras', false),

    -- Options for History questions
    (6, '1945', true),
(6, '1944', false),
(6, '1946', false),
(6, '1943', false),

(7, 'George Washington', true),
(7, 'Thomas Jefferson', false),
(7, 'John Adams', false),
(7, 'Benjamin Franklin', false),

(8, 'The Great Pyramid of Giza', true),
(8, 'The Hanging Gardens', false),
(8, 'The Colossus of Rhodes', false),
(8, 'The Lighthouse of Alexandria', false),

(9, 'Leonardo da Vinci', true),
(9, 'Michelangelo', false),
(9, 'Raphael', false),
(9, 'Donatello', false),

(10, 'Roman Empire', true),
(10, 'Greek Empire', false),
(10, 'Persian Empire', false),
(10, 'Egyptian Empire', false)


--geography
(11, 'Tokyo', true),
(11, 'Kyoto', false),
(11, 'Osaka', false),
(11, 'Seoul', false),

(12, 'Asia', true),
(12, 'Africa', false),
(12, 'North America', false),
(12, 'Europe', false),

(13, 'Nile River', true),
(13, 'Amazon River', false),
(13, 'Mississippi River', false),
(13, 'Yangtze River', false),

(14, 'Vatican City', true),
(14, 'Monaco', false),
(14, 'San Marino', false),
(14, 'Liechtenstein', false),

(15, 'Sahara Desert', true),
(15, 'Arabian Desert', false),
(15, 'Gobi Desert', false),
(15, 'Kalahari Desert', false);
 ;";
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
