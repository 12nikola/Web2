using KvizHub.Models.Answers;
using KvizHub.Models.Base;

namespace KvizHub.Models.Quiz_Details
{
    public class TextEntryDetails: QuizQuestionDetailBase
    {
        public TextEntryAnswer? CorrectAnswer { get; set; }
    }
}
