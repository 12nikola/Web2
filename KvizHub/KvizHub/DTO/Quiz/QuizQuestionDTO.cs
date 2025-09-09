
using KvizHub.Enums;

namespace KvizHub.DTO.Quiz
{
    public class QuizQuestionDTO
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }

        public string? Label { get; set; }
        public string? Category { get; set; }
        public List<UserAnswerDTO>? Answers { get; set; }
        public BasicAnswerDTO? CorrectAnswer { get; set; }
    }
}