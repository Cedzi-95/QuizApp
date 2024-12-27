public class UserMenu : Menu
{
    public UserMenu(IAccountService accountService, IMenuService menuService) 
    {
        
    }

    public override void Display()
    {
        Console.WriteLine("Type 'help' for a list of commands.");
    }
}