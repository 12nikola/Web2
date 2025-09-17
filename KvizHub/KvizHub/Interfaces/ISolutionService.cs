using KvizHub.DTO.Quiz;
using System;
using System.Collections.Generic;

namespace KvizHub.Interfaces
{
    public interface ISolutionService
    {

        bool IsQuestionSolutionByUser(string username, int questionSolutionId);

        bool IsQuizSolutionByUser(string username, int quizSolutionId);
    }
}

