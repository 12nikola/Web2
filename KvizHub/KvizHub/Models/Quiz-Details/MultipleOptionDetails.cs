using KvizHub.Models.Answers;
using KvizHub.Models.Base;

namespace KvizHub.Models.Quiz_Response
{
    public class MultipleOptionDetails:QuizQuestionDetailBase
    {
        public List<MultipleOptionAnswer>? Answers { get; set; }
    }
}
