
using System;

public class QuizCommand : Command
{
    private IQuizService quizService;
    public QuizCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base("Quiz", accountService, menuService, quizService)
    {
    }


    public override void Execute(string[] args)
    {
        Console.WriteLine("\nEnter category name to start quiz:");
        string categoryName = Console.ReadLine()!;

        var questions = quizService.GetQuestionsByCategory(categoryName);
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









}