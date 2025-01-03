public class UserAnswer
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
    public bool IsCorrect { get; set; }
}