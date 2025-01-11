public class LoginCommand : Command
{
    public LoginCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base("Login", accountService, menuService, quizService)
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
        menuService.SetMenu(new UserMenu(accountService, menuService, quizService));
    }
}






public class LogoutCommand : Command
{
    public LogoutCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
   base("exit", accountService, menuService, quizService)
    {

    }

    public override void Execute(string[] args)
    {
        accountService.Logout();
        menuService.SetMenu(new LoginMenu(accountService, menuService, quizService));
    }
}