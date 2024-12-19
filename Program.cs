namespace QuizApp;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=Localhost;Username=postgres;Password=password;Database=group6quiz_app";
        using var connection = new NpgsqlConnection(connectionString);

        connection.Open();

        var createTableSql = @"CREATE TABLE IF NOT EXISTS users (id UUID PRIMARY KEY,
        name TEXT UNIQUE,
        password TEXT UNIQUE );


        CREATE TABLE categories (
    category_id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
    );
        
        CREATE TABLE IF NOT EXISTS questions (question_id INT PRIMARY KEY,
        category_id INTEGER REFERENCES categories(category_id) ON DELETE CASCADE,
        created_by UUID REFERENCES users(id) ON DELETE SET NULL,
        question TEXT NOT NULL
        );
        
        CREATE TABLE question_options (
    option_id SERIAL PRIMARY KEY,
    question_id INTEGER REFERENCES questions(question_id) ON DELETE CASCADE,
    option_text TEXT NOT NULL,
    is_correct BOOLEAN NOT NULL 
    );
    
    CREATE TABLE user_answers (
    answer_id SERIAL PRIMARY KEY,
    question_id INTEGER REFERENCES questions(question_id) ON DELETE CASCADE,
    selected_option_id INTEGER REFERENCES question_options(option_id) ON DELETE CASCADE,
    is_correct BOOLEAN NOT NULL
    );";


        using var cmd = new NpgsqlCommand(createTableSql, connection);
        cmd.ExecuteNonQuery();

    }
}
