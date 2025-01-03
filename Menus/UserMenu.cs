public class UserMenu : Menu
{
    public UserMenu(IAccountService accountService, IMenuService menuService, IQuizService quizService) 
    {
        // AddCommand(new HelpCommand(accountService, menuService));
        AddCommand(new QuizCommand(accountService, menuService, quizService));
    }

    public override void Display()
    {
        Console.WriteLine("Below are the list of quiz commands.");
        Console.WriteLine("\nQuiz Menu:");
        Console.WriteLine("1. View Categories");
        Console.WriteLine("2. Take Quiz");
        Console.WriteLine("3. View Score");
        Console.WriteLine("4. View History");
        Console.WriteLine("5. Logout");


    }
    
}