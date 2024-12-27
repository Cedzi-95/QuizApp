public class LoginCommand : Command
{
    public LoginCommand(IAccountService accountService, IMenuService menuService) : base ("Login", accountService, menuService)
    {

    }

    public override void Execute(string[] args)
    {
         string username = args[1];
        string password = args[2];

        User? user = accountService.Login(username, password);
        if (user == null)
        {
            Console.WriteLine("Wrong username or password.");
            return;
        }

        Console.WriteLine("You successfully logged in.");
        menuService.SetMenu(new UserMenu(accountService, menuService));
    }
}