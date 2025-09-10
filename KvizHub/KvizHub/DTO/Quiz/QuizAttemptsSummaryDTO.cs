namespace KvizHub.DTO.Quiz
{
    public class QuizAttemptsSummaryDTO
    {
        public QuizAttemptInfoDTO? QuizInfo { get; set; }

        public List<double>? RecentScores { get; set; }

        public List<QuizAttemptDTO>? AttemptHistory { get; set; }
    }

}
