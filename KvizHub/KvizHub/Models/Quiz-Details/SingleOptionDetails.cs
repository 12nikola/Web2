using KvizHub.Models.Answers;
using KvizHub.Models.Base;

namespace KvizHub.Models.Quiz_Response
{
    public class SingleOptionDetails:QuizQuestionDetailBase 
    {
        List<SingleOptionAnswer>? Answers { get; set; }
    }
}
