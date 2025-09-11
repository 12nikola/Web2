using KvizHub.Enums;
using KvizHub.Models.Quiz_Information;

namespace QuizWebServer.Models.QuizSolution
{
    public class QuizQuestionSolutionInfo
    {
        public int QuizQuestionSolutionInfoId { get; set; }         

        public QuestionType SolutionType { get; set; }          

        public int QuizAttemptId { get; set; }                      
        public QuizAttempt? QuizAttempt { get; set; }       

        public int QuizQuestionInfoId { get; set; }                
        public QuizQuestionInfo? QuizQuestion { get; set; }         
    }
}
