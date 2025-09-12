using KvizHub.Models.Base;
using KvizHub.Models.User;

namespace KvizHub.Models.Solution
{
    public class MultipleOptionSolution: QuizSolutionDetailBase
    {
        public List<MultipleOptionUserAnswer>? Answers { get; set; }
    }
}
