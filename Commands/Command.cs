public abstract class Command
{
    public string name {get; init;}
    protected IAccountService accountService;
    protected IMenuService menuService;
    protected IQuizService quizService;
    



    public Command (string name, IAccountService accountService, IMenuService menuService,IQuizService quizService)
    {
        this.name = name;
        this.accountService = accountService;
        this.menuService = menuService;
        this.quizService = quizService;
    }



    public abstract void Execute(string [] args);
}