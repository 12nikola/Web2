using Org.BouncyCastle.Asn1.Crmf;

namespace KvizHub.Models.Base
{
    public abstract class QuizSolutionDetailBase
    {
        public int QuizSolutionDetailsId;
        public int QuizSolutionInfoId;
        public QuizQuestionSolutionInfo? QuizSolutionInfo;
    }
}
