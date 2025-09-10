using KvizHub.DTO.Quiz;

namespace KvizHub.Interfaces
{
    public interface IQuestionService
    {
        QuizQuestionDTO? GetById(int questionId);
        QuizQuestionDTO? GetAnswers(int questionId);
        List<QuizQuestionDTO> GetAllByQuizId(int quizId);
        List<QuizQuestionDTO> GetAllWithAnswersForQuiz(int quizId);
        void Remove(int questionId);
        QuizQuestionDTO Add(int quizId, NewQuestionDTO newQuestion);
        QuizQuestionDTO? Edit(int quizId, EditQuestionDTO updatedQuestion);
    }
}
