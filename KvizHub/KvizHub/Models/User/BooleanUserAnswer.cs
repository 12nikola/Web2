using KvizHub.Models.Base;
using KvizHub.Models.Solution;

namespace KvizHub.Models.User
{
    public class BooleanUserAnswer:ParticipantResponseBase
    {
        public int BooleanSolutionId { get; set; }
        public BooleanSolution? BSolution { get; set; }
    }
}
