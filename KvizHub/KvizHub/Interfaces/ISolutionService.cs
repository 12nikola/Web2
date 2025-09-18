using KvizHub.DTO.Quiz;
using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Information;
using System;
using System.Collections.Generic;

namespace KvizHub.Interfaces
{
    public interface ISolutionService
    {
        public QuizAttemptResultDTO SubmitQuizSolution(string username, int quizId, SolQuizDTO solutionData);
        int SaveDerivedQuestionDetails(List<QuizSolutionDetailBase> questionDetails);
        List<QuizQuestionDetailBase> GetQuizQuestionDetails(List<QuizQuestionInfo> questionInfos);
        bool IsQuestionSolutionByUser(string username, int questionSolutionId);

        bool IsQuizSolutionByUser(string username, int quizSolutionId);
    }
}

