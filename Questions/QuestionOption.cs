public class QuestionOption  
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string? OptionText { get; set; }
    public bool IsCorrect { get; set; }
}