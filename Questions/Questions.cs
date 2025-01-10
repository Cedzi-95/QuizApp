public class Question
{
 public int Id { get; set; }
    public int CategoryId { get; set; }
    public string? QuestionText { get; set; }
    public List<QuestionOption> Options { get; set; } = new List<QuestionOption>();
}