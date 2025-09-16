using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Response;

namespace KvizHub.Models.Answers
{
    public class BooleanAnswer:ResponseBase
    {
        public int QuestionDetailsId { get; set; }
        public BooleanDetails? QuestionDetails { get; set; }
    }
}
