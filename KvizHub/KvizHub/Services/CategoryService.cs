using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Exceptions;
using KvizHub.Infrastructure.QuizConfiguration;
using KvizHub.Interfaces;

public class CategoryService : ICategoryService
{
    private readonly QuizContext _context;

    public CategoryService(QuizContext context)
    {
        _context = context;
    }

    public List<QuestionType> GetAll()
    {
        return _context.ToList();
    }

    public QuestionType? GetById(int id)
    {
        return _context.QuestionTypes.FirstOrDefault(c => c.QuestionTypeID == id);
    }

    public QuestionType Add(NewQuestionDTO categoryToCreate)
    {
        var newCategory = new QuestionType
        {
            Name = categoryToCreate.Name
        };

        _context..Add(newCategory);
        int result = _context.SaveChanges();

        if (result <= 0)
        {
            throw new SaveFailedException("Category cannot be saved");
        }

        return newCategory;
    }

    public QuestionType? Edit(int categoryIdenitifier, EditQuestionDTO categoryToUpdate)
    {
        var categoryRecord = _context.QuestionTypes.FirstOrDefault(c => c.QuestionTypeID == categoryIdenitifier);

        if (categoryRecord == null)
        {
            return null;
        }

        categoryRecord.Name = categoryToUpdate.Label;

        int result = _context.SaveChanges();
        if (result <= 0)
        {
            throw new SaveFailedException("Category cannot be updated");
        }

        return categoryRecord;
    }

    public void Remove(int categoryIdenitifier)
    {
        var categoryRecord = _context.QuestionType.FirstOrDefault(c => c.QuestionTypeID == categoryIdenitifier);

        if (categoryRecord == null)
        {
            throw new EntityNotFoundException("Category", categoryIdenitifier);
        }

        bool isReferenced = _context.QuizQuestionInfo.Any(q => q.QuizQuestionId == categoryIdenitifier);
        if (isReferenced)
        {
            throw new EntityReferenceConflictException("Category", categoryIdenitifier.ToString());
        }

        _context.Remove(categoryRecord);
        int result = _context.SaveChanges();

        if (result <= 0)
        {
            throw new DeletionFailedException("Category", categoryIdenitifier.ToString());
        }
    }
}
