public abstract class Command
{
    public string name {get; init;}
    protected IAccountService accountService;
    protected IMenuService menuService;
    protected IQuizService quizService;
    private string v;

    public Command (string name, IAccountService accountService, IMenuService menuService,IQuizService quizService)
    {
        this.name = name;
        this.accountService = accountService;
        this.menuService = menuService;
        this.quizService = quizService;
    }

    protected Command(string v, IAccountService accountService, IMenuService menuService)
    {
        this.v = v;
        this.accountService = accountService;
        this.menuService = menuService;
    }

    public abstract void Execute(string [] args);
}