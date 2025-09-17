using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Exceptions;
using KvizHub.Infrastructure.QuizConfiguration;
using KvizHub.Interfaces;
using KvizHub.Models.Quiz;
using Microsoft.EntityFrameworkCore;

public class QuizService : IQuizService
{
    private readonly QuizContext _context;
    private readonly IMapper _mapper;

    public QuizService(QuizContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Remove(int quizId)
    {
        var quiz = _context.Quizzes
            .Include(q => q.Description)
            .FirstOrDefault(q => q.Id == quizId);

        if (quiz == null)
        {
            throw new EntityNotFoundException("Quiz", quizId);
        }

        if (quiz.Description.Any())
        {
            throw new EntityReferenceConflictException("Quiz", quizId.ToString());
        }

        _context.Quizzes.Remove(quiz);
        int result = _context.SaveChanges();

        if (result <= 0)
        {
            throw new DeletionFailedException("Quiz", quizId.ToString());
        }
    }

    public QuizDTO? GetById(int quizId)
    {
        var quiz = _context.Quizzes
            .Include(q => q.Title)
            .ThenInclude(q => q.)
            .FirstOrDefault(q => q.Id == quizId);

        return quiz == null ? null : _mapper.Map<QuizDTO>(quiz);
    }

    public (List<QuizDTO> Quizzes, int TotalCount) SearchByKeyword(string keyword, int offset, int limit)
    {
        var query = _context.Quizzes
            .Where(q => q.Title.Contains(keyword));

        int totalCount = query.Count();

        var results = query
            .Skip(offset)
            .Take(limit)
            .ToList();

        return (_mapper.Map<List<QuizDTO>>(results), totalCount);
    }

    public (List<QuizDTO> Quizzes, int TotalCount) FilterByCategory(int categoryId, int offset, int limit)
    {
        var query = _context.Quizzes
            .Where(q => q.Description.Any(c => c. == categoryId));

        int totalCount = query.Count();

        var results = query
            .Skip(offset)
            .Take(limit)
            .ToList();

        return (_mapper.Map<List<QuizDTO>>(results), totalCount);
    }

    public (List<QuizDTO> Quizzes, int TotalCount) FilterByDifficulty(Difficulty difficulty, int offset, int limit)
    {
        var query = _context.Quizzes
            .Where(q => q.Difficulty == difficulty);

        int totalCount = query.Count();

        var results = query
            .Skip(offset)
            .Take(limit)
            .ToList();

        return (_mapper.Map<List<QuizDTO>>(results), totalCount);
    }

    public (List<QuizDTO> Quizzes, int TotalCount) GetAll(int offset, int limit)
    {
        var query = _context.Quizzes.AsQueryable();

        int totalCount = query.Count();

        var results = query
            .Skip(offset)
            .Take(limit)
            .ToList();

        return (_mapper.Map<List<QuizDTO>>(results), totalCount);
    }

    public void CleanupIfEmpty(int quizId)
    {
        var quiz = _context.Quizzes
            .Include(q => q.Description)
            .FirstOrDefault(q => q.Id == quizId);

        if (quiz != null && !quiz.Description.Any())
        {
            _context.Quizzes.Remove(quiz);
            _context.SaveChanges();
        }
    }

    public bool HasSolveAttempts(int quizId)
    {
        return _context.QuizAttempts.Any(a => a.QuizAttemptId == quizId);
    }

    public QuizDTO Add(NewQuizDTO newQuiz)
    {
        var entity = _mapper.Map<Quizz>(newQuiz);

        _context.Quizzes.Add(entity);
        int result = _context.SaveChanges();

        if (result <= 0)
        {
            throw new SaveFailedException("Quiz cannot be saved");
        }

        return _mapper.Map<QuizDTO>(entity);
    }

    public QuizDTO Edit(EditQuizDTO updatedQuiz, int quizId)
    {
        var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);

        if (quiz == null)
        {
            throw new EntityNotFoundException("Quiz", quizId);
        }

        _mapper.Map(updatedQuiz, quiz);

        int result = _context.SaveChanges();

        if (result <= 0)
        {
            throw new SaveFailedException("Quiz cannot be updated");
        }

        return _mapper.Map<QuizDTO>(quiz);
    }
}

