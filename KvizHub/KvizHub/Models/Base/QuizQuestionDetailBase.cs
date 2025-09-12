using KvizHub.Models.Quiz_Information;

namespace KvizHub.Models.Base
{
    public abstract class QuizQuestionDetailBase
    {
        public int QuizQuestionDetailsId { get; set; }
        public QuizQuestionInfo? QuizQuestion { get; set; }
        public int QuizQuestionId { get; set; }
    }
}
