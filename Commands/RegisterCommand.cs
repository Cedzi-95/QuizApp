public class RegisterCommand : Command
{
    public RegisterCommand (IAccountService accountService, IMenuService menuService) : base ("Register-user", accountService, menuService)
    {

    }

    public override void Execute(string[] args)
    {
        string username = args[1];
        string password = args[2];

        accountService.RegisterUser(username, password);
        Console.WriteLine($"User {username} has been created. You may now login!");



    }
}