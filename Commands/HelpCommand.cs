public class HelpCommand : Command
{
    public HelpCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base ("help", accountService, menuService, quizService)
    {

    }

    public override void Execute(string[] args)
    {
        Console.WriteLine("Below is a list of commands for quiz app. ");
        Console.WriteLine("Type <category> to view different categories.");
        Console.WriteLine("Type <score> to view your results.");
        Console.WriteLine("Type<logout> to logout");
        
    }
}