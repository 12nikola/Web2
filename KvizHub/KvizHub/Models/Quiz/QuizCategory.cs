using KvizHub.Models.Quiz_Information;

namespace KvizHub.Models.Quiz
{
    public class QuizCategory
    {
        public int CategoryId { get; set; } 
        public string? CategoryName { get; set; }
        public List<QuizQuestionInfo>? Questions { get; set; }
    }
}
