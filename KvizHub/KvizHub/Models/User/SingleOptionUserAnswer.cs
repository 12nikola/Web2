using KvizHub.Models.Base;
using KvizHub.Models.Solution;

namespace KvizHub.Models.User
{
    public class SingleOptionUserAnswer:ParticipantResponseBase
    {
        public int SingleOptionSolutionId { get; set; }
        public SingleOptionSolution? SOSolution { get; set; }
    }
}
