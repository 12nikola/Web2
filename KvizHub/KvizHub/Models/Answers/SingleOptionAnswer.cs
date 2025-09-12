using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Response;

namespace KvizHub.Models.Answers
{
    public class SingleOptionAnswer:ResponseBase
    {
        public int QuestionDetailsId { get; set; }
        public SingleOptionDetails? QuestionDetails { get; set; }
    }
}
