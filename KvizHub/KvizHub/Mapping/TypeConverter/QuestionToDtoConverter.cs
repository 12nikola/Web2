using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Information;
using KvizHub.Models.Quiz_Response;

namespace KvizHub.Mapping.Converters
{
    public class QuestionToDtoConverter : ITypeConverter<QuestionConversionModel, QuizQuestionDTO>
    {
        public QuizQuestionDTO Convert(QuestionConversionModel s, QuizQuestionDTO d, ResolutionContext c)
        {
            bool includeDetails = c.Items.ContainsKey("IncludeDetails") && (bool)c.Items["IncludeDetails"];

            if (s.Details == null || s.Info == null)
            {
                return null;
            }

            QuizQuestionInfo qInfo = s.Info;
            QuizQuestionDetailBase qDetails = s.Details;
            QuizQuestionDTO qDto = null;

            if (qInfo.QuestionType == QuestionType.SingleOption)
            {
                var answers = ((SingleOptionDetails)qDetails).a.Select(a => new NewAnswerDTO
                {
                    Label = a.AnswerText,
                    Valid = includeDetails ? a.IsCorrect : (bool?)null
                }).ToList();

                qDto = new QuizQuestionDTO
                {
                    Id = qInfo.QuizQuestionId,
                    Type = QuestionType.SingleOption,
                    Label = qInfo.QuestionText,
                    Category = qInfo.Category.CategoryName,
                    CorrectAnswer = answers
                };
            }
            else if (qInfo.QuestionType == QuestionType.MultipleOption)
            {
                var answers = ((MultipleOptionDetails)qDetails).Answers.Select(a => new NewAnswerDTO
                {
                    Label = a.Content,
                    Valid = includeDetails ? a.Correct : (bool?)null
                }).ToList();

                qDto = new QuizQuestionDTO
                {
                    Id = qInfo.QuizQuestionId,
                    Type = QuestionType.MultipleOption,
                    Label = qInfo.QuestionText,
                    Category = qInfo.Category.CategoryName,
                    CorrectAnswer = answers
                };
            }
            else if (qInfo.QuestionType == QuestionType.Boolean)
            {
                var trueFalse = (BooleanDetails)qDetails;

                var answers = ((MultipleOptionDetails)qDetails).Answers.Select(a => new NewAnswerDTO
                {
                    Label = a.Content,
                    Valid = includeDetails ? a.Correct : (bool?)null
                }).ToList();

                qDto = new QuizQuestionDTO
                {
                    Id = qInfo.QuizQuestionId,
                    Type = QuestionType.Boolean,
                    Label = qInfo.QuestionText,
                    Category = qInfo.Category.CategoryName,
                    CorrectAnswer = answers
                };
            }
            else if (qInfo.QuestionType == QuestionType.TextEntry)
            {
                var fillIn = (TextEntryDetails)qDetails;

                var answers = ((BooleanDetails)qDetails).CorrectAnswer.Select(a => new NewAnswerDTO
                {
                    Label = a.Content,
                    Valid = includeDetails ? a.Correct : (bool?)null
                }).ToList();

                qDto = new QuizQuestionDTO
                {
                    Id = qInfo.QuizQuestionId,
                    Type = QuestionType.Boolean,
                    Label = qInfo.QuestionText,
                    Category = qInfo.Category.CategoryName,
                    CorrectAnswer = answers
  
                };
            }

            return qDto;
        }
    }
}
