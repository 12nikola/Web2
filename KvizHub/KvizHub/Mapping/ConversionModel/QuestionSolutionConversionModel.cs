using KvizHub.Models.Base;
using KvizHub.Models.QuizSolution;

namespace KvizHub.Mapping.ConversionModel
{
    public class QuestionSolutionConversionModel
    {
        public QuizQuestionSolutionInfo? Info { get; set; }
        public QuizSolutionDetailBase? SolutionDetails { get; set; }
        public QuizQuestionDetailBase? QuestionDetails { get; set; }
    }
}
