using KvizHub.Enums;

namespace KvizHub.DTO.Quiz
{
    public class QuizAttemptInfoDTO
    {
        public int QuizId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Difficulty difficulty { get; set; }
    }
}
