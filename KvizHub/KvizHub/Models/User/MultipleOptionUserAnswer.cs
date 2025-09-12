using KvizHub.Models.Base;
using KvizHub.Models.Solution;

namespace KvizHub.Models.User
{
    public class MultipleOptionUserAnswer:ParticipantResponseBase
    {
        public int MultipleOptionSolutionId { get; set; }
        public MultipleOptionSolution? MOSolution { get; set; }
    }
}
