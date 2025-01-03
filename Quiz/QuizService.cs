public interface IQuizService 
{
    List<Category> GetCategories();
    Category GetCategory(int id);
    List<Question> GetQuestionsByCategory(int categoryId);
    bool SubmitAnswer(Guid userId, int questionId, int selectedOptionId);
    int GetUserScore(Guid userId);
    List<UserAnswer> GetUserHistory(Guid userId);
}
