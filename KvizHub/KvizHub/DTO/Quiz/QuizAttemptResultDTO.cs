namespace KvizHub.DTO.Quiz
{
    public class QuizAttemptResultDTO
    {
        public int AttemptId { get; set; }
        
        public QuizAttemptInfoDTO? QuizInfo { get; set; }

        public QuizAttemptDTO? AttemptDetails { get; set; }
    }
}