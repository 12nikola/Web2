using KvizHub.DTO.Quiz;
using KvizHub.Interfaces;
using System;

namespace KvizHub.Services
{
    public class QuestionService : IQuestionService
    {
        public QuizQuestionDTO Add(int quizId, NewQuestionDTO newQuestion)
        {
            throw new NotImplementedException();
        }

        public QuizQuestionDTO? Edit(int quizId, EditQuestionDTO updatedQuestion)
        {
            throw new NotImplementedException();
        }

        public List<QuizQuestionDTO> GetAllByQuizId(int quizId)
        {
            throw new NotImplementedException();
        }

        public List<QuizQuestionDTO> GetAllWithAnswersForQuiz(int quizId)
        {
            throw new NotImplementedException();
        }

        public QuizQuestionDTO? GetAnswers(int questionId)
        {
            throw new NotImplementedException();
        }

        public QuizQuestionDTO? GetById(int questionId)
        {
            throw new NotImplementedException();
        }

        public void Remove(int questionId)
        {
            throw new NotImplementedException();
        }
    }
}
