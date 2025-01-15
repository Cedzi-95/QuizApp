public class DeleteAccountCommand : Command
{
    public DeleteAccountCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService):
    base ("delete-account",  accountService, menuService, quizService) {}

    public override void Execute(string[] args)
    {
        System.Console.WriteLine($"{Colours.BLUE}This option will delete all your data.");
       Console.WriteLine($"\n DELETE {Colours.RED}[yes]{Colours.NORMAL} - CANCEL{Colours.GREEN} [no]{Colours.NORMAL} ");
        string input;
        
             input = Console.ReadLine()!;
            if (input == "yes")
            {
                accountService.RemoveUser();
                Thread.Sleep(3000);
        menuService.SetMenu(new LoginMenu(accountService, menuService, quizService));
                
        
            }
            else if (input == "no")
            {
                System.Console.WriteLine($"{Colours.GREEN} Good choice");
                Thread.Sleep(3000);       
        menuService.SetMenu(new UserMenu(accountService, menuService, quizService));
            }

        
    }
}