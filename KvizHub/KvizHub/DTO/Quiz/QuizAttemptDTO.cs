namespace KvizHub.DTO.Quiz
{
    public class QuizAttemptDTO
    {
        public int AttemptId { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int TotalAnswersCount { get; set; }

        public int WrongAnswersCount { get; set; }

        public double ScorePercent { get; set; }

        public double ScorePoints { get; set; }

        public DateTime AttemptedOn { get; set; }

        public TimeSpan TimeTaken { get; set; }

        public List<int>? AnswerSolutionIds { get; set; }
    }
}