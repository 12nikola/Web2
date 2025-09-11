using Org.BouncyCastle.Asn1.Crmf;
using QuizWebServer.Models.QuizSolution;

namespace KvizHub.Models.Base
{
    public abstract class QuizSolutionDetailBase
    {
        public int QuizSolutionDetailsId;
        public int QuizSolutionInfoId;
        public QuizQuestionSolutionInfo? QuizSolutionInfo;
    }
}
