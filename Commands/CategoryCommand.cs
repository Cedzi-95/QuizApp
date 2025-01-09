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
        Console.WriteLine($"{Colours.GREEN}\n       Available Categories:{Colours.NORMAL}");
        foreach (var category in categories)
        {
            Console.WriteLine($"{Colours.YELLOW}{category.Id}. {category.Name}{Colours.NORMAL} - {category.Description}");
        }
        Console.WriteLine("\npress key to continue...");
        Console.ReadKey();
        menuService.SetMenu(new UserMenu(accountService, menuService, quizService));
   
    }

}