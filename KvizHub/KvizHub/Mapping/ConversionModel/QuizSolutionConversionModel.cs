using KvizHub.Models.Base;
using KvizHub.Models.Quiz;
using KvizHub.Models.QuizSolution;
using KvizHub.Models.User;

namespace KvizHub.Mapping.ConversionModel
{
    public class QuizSolutionConversionModel
    {
        public Users? User { get; set; }
        public Quizz? Quiz { get; set; }
        public QuizAttempt? Attempt { get; set; }
        public List<QuizQuestionDetailBase>? QuestionDetails { get; set; }
        public List<QuizSolutionDetailBase>? SolutionDetails { get; set; }

    }
}
