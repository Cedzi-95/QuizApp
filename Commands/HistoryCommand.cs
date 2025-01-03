public class HistoryCommand : Command
{
    public HistoryCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
    base ("history", accountService, menuService, quizService) {}

    public override void Execute(string[] args)
    {
        var user = accountService.GetLoggedInUser();
        var history = quizService.GetUserHistory(user.Id);
        Console.WriteLine("\nYour Quiz History:");
        foreach (var answer in history)
        {
            Console.WriteLine($"Question ID: {answer.QuestionId}, " +
                            $"Selected Option: {answer.SelectedOptionId}, " +
                            $"Correct: {answer.IsCorrect}");
        }
    }
}