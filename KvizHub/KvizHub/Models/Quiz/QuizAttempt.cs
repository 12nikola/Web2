using KvizHub.Models.Quiz;
using KvizHub.Models.User;
using System;
using System.Collections.Generic;

namespace KvizHub.Models.QuizSolution
{
    public class QuizAttempt
    {
        public int QuizAttemptId { get; set; }                       

        public int CorrectAnswersCount { get; set; }                 

        public int TotalAnswersCount { get; set; }                   

        public int IncorrectAnswersCount { get; set; }               

        public double ScorePoints { get; set; }                     

        public double ScorePercentage { get; set; }                  

        public DateTime AttemptDate { get; set; }                   

        public TimeSpan Duration { get; set; }                      

        public List<QuizQuestionSolutionInfo>? QuestionSolutions { get; set; } 

        public string? Username { get; set; }                         

        public int QuizId { get; set; }                              
        public Quizz? ParentQuiz { get; set; }                         
    }
}

