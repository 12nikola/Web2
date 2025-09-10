using KvizHub.DTO.Quiz;
using KvizHub.Enums;

namespace KvizHub.Interfaces
{
    public interface ICategoryService
    {
        List<QuestionType> GetAll();
        QuestionType? GetById(int id);
        QuestionType Add(NewQuestionDTO categoryToCreate);
        QuestionType? Edit(int categoryIdenitifier, EditQuestionDTO categoryToUpdate);
        void Remove(int categoryIdenitifier);
    }
}
