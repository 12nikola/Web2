using KvizHub.Models.Base;
using KvizHub.Models.User;

namespace KvizHub.Models.Solution
{
    public class TextEntrySolution: QuizSolutionDetailBase
    {
        public TextEntryUserAnswer? Answer { get; set; }
    }
}
