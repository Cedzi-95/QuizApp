public class HelpCommand : Command
{
    public HelpCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base ("help", accountService, menuService, quizService)
    {

    }

    public override void Execute(string[] args)
    {
        Console.WriteLine($"Below are the list of quiz commands.");
        Console.WriteLine($"{Colours.YELLOW}  \nQuiz Menu:(commands are the coloured words){Colours.NORMAL}");
        Console.WriteLine($"{Colours.GREEN}[category]{Colours.NORMAL} View Categories");
        Console.WriteLine($"{Colours.GREEN}[quiz]{Colours.NORMAL} Take Quiz");
        Console.WriteLine($"{Colours.GREEN}[score]{Colours.NORMAL} View your total Score");
        Console.WriteLine($"{Colours.GREEN}[history]{Colours.NORMAL} View History");
        Console.WriteLine($"{Colours.GREEN}[delete-account]{Colours.NORMAL} to delete your account");
        Console.WriteLine($"{Colours.RED}[exit]{Colours.NORMAL} Logout");
        
    }
}