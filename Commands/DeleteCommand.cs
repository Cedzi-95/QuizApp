public class DeleteCommand : Command
{
    public DeleteCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
        base("delete-user", accountService, menuService, quizService)
    {
    }

    public override void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: delete <userId>");
            return;
        }

        string userIdInput = args[1];
        if (!Guid.TryParse(userIdInput, out Guid userId))
        {
            Console.WriteLine("Invalid user ID format. Please try again.");
            return;
        }

        bool isDeleted = accountService.DeleteAccount(userId);
        if (isDeleted)
        {
            Console.WriteLine("The account has been successfully deleted.");
        }
        else
        {
            Console.WriteLine("No account found with the specified ID.");
        }
    }
}