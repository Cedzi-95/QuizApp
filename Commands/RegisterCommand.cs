public class RegisterCommand : Command
{
    public RegisterCommand (IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base ("register-user", accountService, menuService, quizService)
    {

    }

    public override void Execute(string[] args)
    {
        string username = args[1];
        string password = args[2];

        accountService.RegisterUser(username, password);
        Console.WriteLine($"User{Colours.YELLOW} {username}{Colours.NORMAL} has been created. You may now login!");



    }
}