namespace KvizHub.DTO.Quiz
{
    public class QuizQuestion
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public int Points { get; set; }
    }
}
