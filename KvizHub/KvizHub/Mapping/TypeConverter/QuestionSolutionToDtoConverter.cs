using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Response;
using KvizHub.Models.Solution;

namespace KvizHub.Mapping.TypeConverters
{
    public class QuestionSolutionToDtoConverter : ITypeConverter<QuestionSolutionConversionModel, SolQuestionDTO>
    {
        public SolQuestionDTO Convert(QuestionSolutionConversionModel source, SolQuestionDTO destination, ResolutionContext context)
        {
            var dto = new SolQuestionDTO
            {
                QuestionId = source.QuestionDetails.QuizQuestion.QuizQuestionId,
                SingleLabel = source.QuestionDetails.QuizQuestion.QuestionText,
                MultipleLabels = source.QuestionDetails.QuizQuestion.,
                BooleanLabel = source.QuestionDetails.QuizQuestion.
            };

            if (dto. == QuestionType.SingleOption)
            {
                dto.MultipleLabels = context.Mapper.Map<List<BasicAnswerDTO>>(
                    ((SingleOptionDetails)source.QuestionDetails).);

                dto.SingleLabel = null;

                if (source.QuestionDetails != null)
                {
                    dto.SingleLabel =
                        ((SingleOptionSolution)source.SolutionDetails).Answer.ResponseText;
                }
            }
            else if (dto. == QuestionType.MultipleOption)
            {
                dto.MultipleLabels = context.Mapper.Map<List<BasicAnswerDTO>>(
                    ((MultipleOptionDetails)source.SolutionDetails).Answers);


                if (source.SolutionDetails != null)
                {
                    foreach (var a in ((MultipleOptionSolution)source.SolutionDetails).Answers)
                    {
                        dto.MultipleLabels.Add(new BasicAnswerDTO { Label = a.ResponseText });
                    }
                }
            }
            else if (dto. == QuestionType.TextEntry)
            {
                dto.SingleLabel = ((TextEntryDetails)source.QuestionDetails).CorrectAnswer.Content;

                if (source.QuestionDetails != null)
                {
                    dto.SingleLabel =
                        ((TextEntrySolution)source.SolutionDetails).Answer.ResponseText;
                }
            }
            else if (dto. == QuestionType.Boolean)
            {
                dto.BooleanLabel = ((BooleanDetails)source.QuestionDetails).CorrectAnswer.Correct;

                if (source.QuestionDetails != null)
                {
                    dto.BooleanLabel =
                        ((BooleanSolution)source.SolutionDetails).Answer.ResponseText == "True";
                }
            }

            return dto;
        }
    }
}
