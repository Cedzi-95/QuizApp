public class ScoreCommand : Command
{
    public ScoreCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
    base ("score", accountService, menuService, quizService)
    {

    }

    public override void Execute(string[] args)
    {
         var user = accountService.GetLoggedInUser();
        var score = quizService.GetUserScore(user.Id);
        Console.WriteLine($"\nYour total score:{Colours.GREEN} {score} correct answers.{Colours.NORMAL}");
    }
}