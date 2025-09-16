using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Information;

namespace KvizHub.Mapping.ConversionModel
{
    public class QuestionConversionModel
    {
        public QuizQuestionDetailBase? Details { get; set; }
        public QuizQuestionInfo? Info { get; set; }
    }
}
