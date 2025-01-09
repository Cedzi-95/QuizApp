public class LoginMenu : Menu
{

public LoginMenu(IAccountService accountService, IMenuService menuService, IQuizService quizService)
{
    AddCommand(new LoginCommand( accountService,  menuService, quizService));
    AddCommand(new RegisterCommand(accountService,  menuService, quizService));
}

    public override void Display()
    {
        Console.Clear();
          Console.WriteLine($"{Colours.GREEN}               \nWELCOME TO YOUR QUIZ APP  {Colours.NORMAL}");
        Console.WriteLine("---------------------------------------------------------");
        Console.WriteLine($"||login <username> <password> - Log into your account");
        Console.WriteLine($"||register-user <username> <password> - Create a new account");
        Console.WriteLine("-------------------------------------------------------------");
    }
}