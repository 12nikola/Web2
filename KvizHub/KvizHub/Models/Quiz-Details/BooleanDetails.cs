using KvizHub.Models.Answers;
using KvizHub.Models.Base;

namespace KvizHub.Models.Quiz_Response
{
    public class BooleanDetails: QuizQuestionDetailBase
    {
        public BooleanAnswer? CorrectAnswer { get; set; }
    }
}
