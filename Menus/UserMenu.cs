public class UserMenu : Menu
{
    public UserMenu(IAccountService accountService, IMenuService menuService, IQuizService quizService) 
    {
        // AddCommand(new HelpCommand(accountService, menuService));
        AddCommand(new QuizCommand(accountService, menuService, quizService));
        AddCommand(new CategoryCommand(accountService, menuService, quizService));
        AddCommand(new ScoreCommand(accountService, menuService, quizService));
         AddCommand(new HistoryCommand(accountService, menuService, quizService));
        AddCommand(new LogoutCommand(accountService, menuService, quizService));
    }

    public override void Display()
    {
        Console.WriteLine("Below are the list of quiz commands.");
        Console.WriteLine("\nQuiz Menu:");
        Console.WriteLine("<category> View Categories");
        Console.WriteLine("<quiz> Take Quiz");
        Console.WriteLine("<score> View Score");
        Console.WriteLine("<history> View History");
        Console.WriteLine("<exit> Logout");


    }
    
}