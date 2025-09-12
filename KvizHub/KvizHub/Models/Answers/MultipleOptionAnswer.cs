using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Response;

namespace KvizHub.Models.Answers
{
    public class MultipleOptionAnswer : ResponseBase
    {

        public int QuestionDetailsId { get; set; }
        public MultipleOptionDetails? QuestionDetails { get; set; }
        
    }
}
