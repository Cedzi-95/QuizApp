
using System;

public class QuizCommand : Command
{
    private IQuizService quizService;
    public QuizCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base ("Quiz", accountService, menuService, quizService)
    {
    }

    public override void Execute(string[] args) 
    {
//    Console.WriteLine("Enter your choice 1-5: ");

   
    
//         switch (inputCommand.ToLower())
//         {
//             case "1":
//                 DisplayCategories();
//                 break;
//             case "2":
//                 StartQuiz();
//                 break;
//             case "3":
//                 DisplayScore();
//                 break;
//             case "4":
//                 DisplayHistory();
//                 break;
//             case "5":
//                 Logout();
//                 break;
//             default:
//                 Console.WriteLine("Invalid command. Please try again.");
//                 break;
//         }

   
        
    }

    private void While(bool v)
    {
        throw new NotImplementedException();
    }

    private void DisplayCategories()
    {
        var categories = quizService.GetCategories();
        Console.WriteLine("\nAvailable Categories:");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id}. {category.Name} - {category.Description}");
        }
    }

    private void StartQuiz()
    {
        Console.WriteLine("\nEnter category ID to start quiz:");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid category ID.");
            return;
        }

        var questions = quizService.GetQuestionsByCategory(categoryId);
        var user = accountService.GetLoggedInUser();

        foreach (var question in questions)
        {
            Console.WriteLine($"\n{question.QuestionText}");
            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i].OptionText}");
            }

            Console.Write("\nYour answer (enter number): ");
            if (int.TryParse(Console.ReadLine(), out int answer) && 
                answer > 0 && 
                answer <= question.Options.Count)
            {
                var selectedOption = question.Options[answer - 1];
                var isCorrect = quizService.SubmitAnswer(user.Id, question.Id, selectedOption.Id);
                Console.WriteLine(isCorrect ? "Correct!" : "Incorrect!");
            }
            else
            {
                Console.WriteLine("Invalid answer, skipping question.");
            }
        }
    }

    private void DisplayScore()
    {
        var user = accountService.GetLoggedInUser();
        var score = quizService.GetUserScore(user.Id);
        Console.WriteLine($"\nYour total score: {score} correct answers");
    }

    private void DisplayHistory()
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

    private void Logout()
    {
        accountService.Logout();
        menuService.SetMenu(new LoginMenu(accountService, menuService, quizService));
    }
}