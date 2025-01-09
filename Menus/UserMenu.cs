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
         AddCommand(new HistoryCommand(accountService, menuService, quizService));
         AddCommand(new DeleteAccountCommand( accountService, menuService, quizService));
    }

    public override void Display()
    {
        Console.WriteLine($"Below are the list of quiz commands.");
        Console.WriteLine($"{Colours.YELLOW}  \nQuiz Menu:(commands are the coloured word){Colours.NORMAL}");
        Console.WriteLine($"<{Colours.GREEN}category{Colours.NORMAL}> View Categories");
        Console.WriteLine($"<{Colours.GREEN}quiz{Colours.NORMAL}> Take Quiz");
        Console.WriteLine($"<{Colours.GREEN}score{Colours.NORMAL}> View Score");
        Console.WriteLine($"<{Colours.GREEN}history{Colours.NORMAL}> View History");
        Console.WriteLine($"<{Colours.GREEN}delete-account{Colours.NORMAL}> to delete your account");
        Console.WriteLine($"<{Colours.RED}exit{Colours.NORMAL}> Logout");


    }
    
}