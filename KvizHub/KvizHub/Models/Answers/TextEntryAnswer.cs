using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Details;

namespace KvizHub.Models.Answers
{
    public class TextEntryAnswer: ResponseBase
    {
        public int QuestionDetailsId { get; set; }
        public TextEntryDetails? QuestionDetails { get; set; }
    }
}
