using KvizHub.DTO.Quiz;
using KvizHub.Enums;

namespace KvizHub.Interfaces
{
    public interface IQuizService
    {
        void Remove(int quizId);
        QuizDTO? GetById(int quizId);
        (List<QuizDTO> Quizzes, int TotalCount) SearchByKeyword(string keyword, int offset, int limit);

        (List<QuizDTO> Quizzes, int TotalCount) FilterByCategory(int categoryId, int offset, int limit);

        (List<QuizDTO> Quizzes, int TotalCount) FilterByDifficulty(Difficulty difficulty, int offset, int limit);

        (List<QuizDTO> Quizzes, int TotalCount) GetAll(int offset, int limit);

        void CleanupIfEmpty(int quizId);

        bool HasSolveAttempts(int quizId);

        QuizDTO Add(NewQuizDTO newQuiz);

        QuizDTO Edit(EditQuizDTO updatedQuiz, int quizId);
    }
}
