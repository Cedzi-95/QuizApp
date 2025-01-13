public class HistoryCommand : Command
{
    public HistoryCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
        base("quiz-history", accountService, menuService, quizService) {}

    public override void Execute(string[] args)
{
    var user = accountService.GetLoggedInUser();
    var history = quizService.GetUserHistory(user.Id);
    var categories = quizService.GetCategories();
    var allQuestions = new List<Question>();

    foreach (var category in categories)
    {
        allQuestions.AddRange(quizService.GetQuestionsByCategory(category.Name));
    }

    Console.WriteLine("\nYour Quiz History:");
    foreach (var answer in history)
    {
        var question = allQuestions.FirstOrDefault(q => q.Id == answer.QuestionId);
        string questionText = question?.QuestionText ?? "[Question not found]";
        string categoryName = categories.FirstOrDefault(c => c.Id == question?.CategoryId)?.Name ?? "[Category not found]";

        Console.WriteLine($"{Colours.YELLOW}Category: {Colours.NORMAL}{categoryName}, " +
                          $"{Colours.YELLOW}Question: {Colours.NORMAL}{questionText}, " +
                          $"{Colours.YELLOW}Correct: {Colours.NORMAL}{answer.IsCorrect}");
    }

    Console.WriteLine("\nPress any key to return to menu..");
    Console.ReadKey();
    menuService.SetMenu(new UserMenu(accountService, menuService, quizService));
}
}