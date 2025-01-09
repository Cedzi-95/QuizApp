public class DeleteAccountCommand : Command
{
    public DeleteAccountCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService):
    base ("delete-account",  accountService, menuService, quizService) {}

    public override void Execute(string[] args)
    {
        accountService.RemoveUser();
        Thread.Sleep(3000);
        menuService.SetMenu(new LoginMenu(accountService, menuService, quizService));
    }
}