using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Infrastructure.QuizConfiguration;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Models.Base;
using KvizHub.Models.Quiz;
using KvizHub.Models.Quiz_Information;
using KvizHub.Models.Quiz_Response;
using Microsoft.EntityFrameworkCore;
using QuizWebServer.Exceptions;

namespace QuizWebServer.Services
{
    public class QuizSolutionManager : IQuizSolutionManager
    {
        private readonly IMapper _mapperService;
        private readonly QuizContext _databaseContext;

        public QuizSolutionManager(IMapper mapperService, QuizContext databaseContext)
        {
            _mapperService = mapperService;
            _databaseContext = databaseContext;
        }

        public QuizAttemptResultDTO SubmitQuizSolution(string username, int quizId, SolQuizDTO solutionData)
        {
            Quizz quizEntity = _databaseContext.Quizzes.Include(q => q.Description)
                                                      .FirstOrDefault(q => q.Id == quizId);

            if (quizEntity == null)
                throw new EntityNotFoundException("Quiz", quizId);
            if (!quizEntity)
                throw new EntityUnavailableException("Quiz", quizId.ToString());

            UserAccount userEntity = _databaseContext.Users.FirstOrDefault(u => u.Username == username);
            if (userEntity == null)
                throw new EntityNotFoundException("User", username);

            List<QuizQuestionDetailBase> questionDetails = GetQuizQuestionDetails(quizEntity.);

            QuizSolutionConversionModel conversionHelper = new QuizSolutionConversionModel
            {
                QuestionDetails = questionDetails,
                User = userEntity,
                Quiz = quizEntity
            };

            _mapperService.Map(solutionData, conversionHelper);

            _databaseContext.QuizAttempts.Add(conversionHelper.Attempt);
            int result = _databaseContext.SaveChanges();
            if (result < 0)
                throw new DeletionFailedException("Quiz attempt not saved");

            int derivedResult = SaveDerivedQuestionDetails(conversionHelper.SolutionDetails);
            if (derivedResult < 0)
            {
                _databaseContext.QuizAttempts.Remove(conversionHelper.Attempt);
                throw new DeletionFailedException("Quiz attempt not saved");
            }

            return new QuizAttemptResultDTO
            {
                AttemptId = conversionHelper.Attempt.QuizAttemptId,
                QuizInfo = _mapperService.Map<QuizAttemptDTO>(conversionHelper.),
                AttemptDetails = _mapperService.Map<QuizAttemptInfoDTO>(conversionHelper.a)
            };
        }

        private List<QuizQuestionDetailBase> GetQuizQuestionDetails(List<QuizQuestionInfo> questionInfos)
        {
            List<QuizQuestionDetailBase> detailsList = new List<QuizQuestionDetailBase>();
            foreach (QuizQuestionInfo qInfo in questionInfos)
            {
                QuizQuestionDetailBase details = null;
                if (qInfo.QuestionType == QuestionType.SingleOption)
                {
                    details = _databaseContext.SingleOptionDetails.Include(x => x.QuizQuestion)
                                                                          .Include(x => x.)
                                                                          .FirstOrDefault(d => d.QuizQuestionDetailsId == qInfo.QuizQuestionId);
                }
                else if (qInfo.QuestionType == QuestionType.MultipleOption)
                {
                    details = _databaseContext.MultipleOptionDetails.Include(x => x.QuizQuestion)
                                                                            .Include(x => x.Answers)
                                                                            .FirstOrDefault(d => d.QuizQuestionDetailsId == qInfo.QuizQuestionId);
                }
                else if (qInfo.QuestionType == QuestionType.Boolean)
                {
                    details = _databaseContext.BooleanDetails.Include(x => x.QuizQuestion)
                                                                       .Include(x => x.CorrectAnswer)
                                                                       .FirstOrDefault(d => d.QuizQuestionDetailsId == qInfo.QuizQuestionId);
                }
                else if (qInfo.QuestionType == QuestionType.TextEntry)
                {
                    details = _databaseContext.TextEntryDetails.Include(x => x.QuizQuestion)
                                                                    .Include(x => x.CorrectAnswer)
                                                                    .FirstOrDefault(d => d.QuizQuestionId == qInfo.QuizQuestionId);
                }

                if (details != null)
                    detailsList.Add(details);
            }

            return detailsList;
        }

        private int SaveDerivedQuestionDetails(List<QuizSolutionDetailBase> questionDetails)
        {
            foreach (var detail in questionDetails)
            {
                if (detail.QuizSolutionInfo.SolutionType == QuestionType.SingleOption)
                    _databaseContext.SingleOptionSolution.Add((SingleOptionDetails)detail);
                else if (detail.QuizSolutionInfo.SolutionType == QuestionType.MultipleOption)
                    _databaseContext.MultipleOptionSolution.Add((QuizQuestionInfo)detail);
                else if (detail.QuizSolutionInfo.SolutionType == QuestionType.Boolean)
                    _databaseContext.BooleanSolution.Add((TrueFalseQuestionSolutionDetails)detail);
                else if (detail.QuizSolutionInfo.SolutionType == QuestionType.TextEntry)
                    _databaseContext.TextEntrySolution.Add((FillInQuestionSolutionDetails)detail);
            }

            return _databaseContext.SaveChanges();
        }
    }
}