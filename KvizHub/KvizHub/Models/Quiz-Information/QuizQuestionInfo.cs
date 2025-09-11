using KvizHub.Enums;
using KvizHub.Models.Quiz;

namespace KvizHub.Models.Quiz_Information
{
    public class QuizQuestionInfo
    {
        public int QuizQuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int CategoryId { get; set; } 
        public QuizCategory? Category { get; set; }
        public QuestionType QuestionType { get; set; }
        public Quizz? ParentQuiz { get; set; }
        public int ParentQuizId { get; set; }
    }
}
