using KvizHub.Models.Base;
using KvizHub.Models.Solution;

namespace KvizHub.Models.User
{
    public class TextEntryUserAnswer: ParticipantResponseBase
    {
        public int TextEntrySolutionId { get; set; }
        public TextEntrySolution? TESolution { get; set; }
       
    }
}
