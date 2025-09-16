using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Mapping.ConversionModel;

namespace QuizWebServer.Mapping.TypeConverters
{
    public class QuestionSolutionToDtoConverter : ITypeConverter<QuestionSolutionConversionModel, SolQuestionDTO>
    {
        public SolQuestionDTO Convert(QuestionSolutionConversionModel source, SolQuestionDTO destination, ResolutionContext context)
        {
            var dto = new SolQuestionDTO
            {
                QuestionId = source.Info..QuestionInformationID,
                SingleLabel = source.QuestionSolutionInformation.QuestionInformation.QuestionDetailsType,
                MultipleLabels = source.QuestionSolutionInformation.QuestionInformation.QuestionCategory.Name,
                BooleanLabel = source.QuestionSolutionInformation.QuestionInformation.Text
            };

            if (dto.QuestionType == QuizQuestionType.SingleChoice)
            {
                dto.MultipleLabels = context.Mapper.Map<List<SimplifiedAnswerDTO>>(
                    ((SingleChoiceQuestionDetails)source.QuestionDetailsBase).Answers);

                dto.SingleLabel = null;

                if (source.QuestionSolutionDetailsBase != null)
                {
                    dto.SingleLabel =
                        ((SingleChoiceQuestionSolutionDetails)source.QuestionSolutionDetailsBase)
                        .SingleChoiceQuestionUserAnswer.UserAnswerText;
                }
            }
            else if (dto.QuestionType == QuizQuestionType.MultipleChoice)
            {
                dto.MultipleLabels = context.Mapper.Map<List<SimplifiedAnswerDTO>>(
                    ((MultipleChoiceQuestionDetails)source.QuestionDetailsBase).Answers);

                dto.MultipleLabels ??= new List<SimplifiedAnswerDTO>();

                if (source.QuestionSolutionDetailsBase != null)
                {
                    foreach (var a in ((MultipleChoiceQuestionSolutionDetails)source.QuestionSolutionDetailsBase).UserAnswers)
                    {
                        dto.MultipleLabels.Add(new SimplifiedAnswerDTO { AnswerText = a.UserAnswerText });
                    }
                }
            }
            else if (dto.QuestionType == QuizQuestionType.FillIn)
            {
                dto.SingleLabel = ((FillInQuestionDetails)source.QuestionDetailsBase).CorrectAnswer.AnswerText;

                if (source.QuestionSolutionDetailsBase != null)
                {
                    dto.SingleLabel =
                        ((FillInQuestionSolutionDetails)source.QuestionSolutionDetailsBase)
                        .FillInQuestionUserAnswer.UserAnswerText;
                }
            }
            else if (dto.QuestionType == QuizQuestionType.TrueFalse)
            {
                dto.BooleanLabel = ((TrueFalseQuestionDetails)source.QuestionDetailsBase).CorrectAnswer.IsCorrect;

                if (source.QuestionSolutionDetailsBase != null)
                {
                    dto.BooleanLabel =
                        ((TrueFalseQuestionSolutionDetails)source.QuestionSolutionDetailsBase)
                        .TrueFalseQuestionUserAnswer.UserAnswerText == "True";
                }
            }

            return dto;
        }
    }
}
