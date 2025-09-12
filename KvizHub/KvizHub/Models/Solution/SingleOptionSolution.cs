using KvizHub.Models.Base;
using KvizHub.Models.User;

namespace KvizHub.Models.Solution
{
    public class SingleOptionSolution: QuizSolutionDetailBase
    {
        public SingleOptionUserAnswer? Answer { get; set; }
    }
}
