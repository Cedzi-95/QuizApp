
using System;

public class QuizCommand : Command
{
  
    public QuizCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base("Quiz", accountService, menuService, quizService)
    {
    }


    public override void Execute(string[] args)
    {
        Console.WriteLine($"{Colours.GREEN}\nEnter category name to start quiz:{Colours.NORMAL}");
        string categoryName = Console.ReadLine()!;

        var questions = quizService.GetQuestionsByCategory(categoryName);
        var user = accountService.GetLoggedInUser();
        

        foreach (var question in questions)
        {
            Console.WriteLine($"{Colours.GREEN}\n{question.QuestionText}{Colours.NORMAL}");
            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{Colours.YELLOW}[{i + 1}].{Colours.NORMAL} {question.Options[i].OptionText}");
            }

            Console.Write("\nYour answer (enter number): ");
            if (int.TryParse(Console.ReadLine(), out int answer) &&
                answer > 0 &&
                answer <= question.Options.Count)
            {
                var selectedOption = question.Options[answer - 1]; //om svaret är lika med en av svarsalternativerna
                var isCorrect = quizService.SubmitAnswer(user.Id, question.Id, selectedOption.Id);
                Console.WriteLine(isCorrect ? $"{Colours.GREEN}Correct!{Colours.NORMAL}" : $"{Colours.RED}Incorrect!{Colours.NORMAL}");
            }
            else
            {
                Console.WriteLine("Invalid answer, skipping question.");
            }
        }





    }









}