using Npgsql;
public class CategoryCommand : Command
{

    
    public CategoryCommand(IAccountService accountService, IMenuService menuService, IQuizService quizService) :
     base ("category", accountService, menuService, quizService)
    {
        
    }

    public override void Execute(string[] args)
    {

         var categories = quizService.GetCategories();
        Console.WriteLine("\nAvailable Categories:");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id}. {category.Name} - {category.Description}");
        }
   
    }

}