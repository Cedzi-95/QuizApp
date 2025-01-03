public interface IQuizService 
{
    List<Category> GetCategories();
    Category GetCategory(string name);
    List<Question> GetQuestionsByCategory(string name);
    bool SubmitAnswer(Guid userId, int questionId, int selectedOptionId);
    int GetUserScore(Guid userId);
    List<UserAnswer> GetUserHistory(Guid userId);
}
