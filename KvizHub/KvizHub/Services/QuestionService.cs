using KvizHub.DTO.Quiz;
using KvizHub.Interfaces;
using KvizHub.Mapping.Profiles;
using KvizHub.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KvizHub.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;

        public QuestionService(AppDbContext context)
        {
            _context = context;
        }

        public QuizQuestionDTO? GetById(int questionId)
        {
            var question = _context.Questions
                .AsNoTracking()
                .FirstOrDefault(q => q. == questionId);

            return question == null ? null : QuizQuestionDTO.FromEntity(question);
        }

        public QuizQuestionDTO? GetAnswers(int questionId)
        {
            var question = _context.Questions
                .Include(q => q.Answers)
                .AsNoTracking()
                .FirstOrDefault(q => q. == questionId);

            return question == null ? null : QuizQuestionDTO.FromEntity(question);
        }

        public List<QuizQuestionDTO> GetAllByQuizId(int quizId)
        {
            return _context.Questions
                .Where(q => q.QuizID == quizId)
                .AsNoTracking()
                .Select(q => QuizQuestionDTO.(q))
                .ToList();
        }

        public List<QuizQuestionDTO> GetAllWithAnswersForQuiz(int quizId)
        {
            return _context.Questions
                .Where(q => q.QuizID == quizId)
                .Include(q => q.Answers)
                .AsNoTracking()
                .Select(q => QuizQuestionDTO.FromEntity(q))
                .ToList();
        }

        public void Remove(int questionId)
        {
            var question = _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.QuestionID == questionId);

            if (question == null) return;

            _context.Answers.RemoveRange(question.Answers);
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }

        public QuizQuestionDTO Add(int quizId, NewQuestionDTO newQuestion)
        {
            var question = new Q
            {
                QuizID = quizId,
                Text = newQuestion.Text,
                Type = newQuestion.Type,
                QuestionCategoryID = newQuestion.QuestionCategoryID
            };

            if (newQuestion.Answers != null && newQuestion.Answers.Any())
            {
                question.Answers = newQuestion.Answers
                    .Select(a => new Answer
                    {
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList();
            }

            _context.Questions.Add(question);
            _context.SaveChanges();

            return QuizQuestionDTO.FromEntity(question);
        }

        public QuizQuestionDTO? Edit(int quizId, EditQuestionDTO updatedQuestion)
        {
            var question = _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.QuestionID == updatedQuestion.QuestionID && q.QuizID == quizId);

            if (question == null) return null;

            question.Text = updatedQuestion.Text;
            question.Type = updatedQuestion.Type;
            question.QuestionCategoryID = updatedQuestion.QuestionCategoryID;

            // update answers
            if (updatedQuestion.Answers != null)
            {
                _context.Answers.RemoveRange(question.Answers);

                question.Answers = updatedQuestion.Label
                    .Select(a => new EditQuestionDTO
                    {
                        Label = a.,
                        Correct = a.
                    }).ToList();
            }

            _context.SaveChanges();

            return QuizQuestionDTO.Equals(question);
        }
    }
}
