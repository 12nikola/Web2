using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Exceptions;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Response;

namespace KvizHub.Mapping.Converters
{
    public class DtoToUpdateQuestionConverter : ITypeConverter<EditQuestionDTO, QuestionConversionModel>
    {
        public QuestionConversionModel Convert(EditQuestionDTO s, QuestionConversionModel d, ResolutionContext c)
        {
            if (d.Info == null || d.Details == null)
            {
                return null;
            }

            var qType = d.Info.QuestionType;

            d.Info.QuestionText = s.Label ?? d.Info.QuestionText;
            d.Info.CategoryId = s.CategoryId ?? d.Info.CategoryId;

            if (qType == QuestionType.SingleOption)
            {
                if (s.Options != null)
                {
                    if (s.Options.Count > 4)
                        throw new InvalidRequestException("SingleChoice can have at most 4 answers.");

                    if (s.Options.Any(a => a.OptionKey == 0))
                        throw new InvalidRequestException("AnswerID cannot be zero.");

                    foreach (var ans in s.Options)
                    {
                        if (!((SingleOptionDetails)d.Details).a.Any(a => a.AnswerID == ans.OptionKey))
                            throw new InvalidRequestException("Answer must belong to the given question.");
                    }

                    foreach (var ans in s.Options)
                    {
                        var toUpdate = ((SingleOptionDetails)d.Details).a.First(a => a.AnswerID == ans.OptionKey);

                        if (!string.IsNullOrWhiteSpace(ans.ResponseLabel) && ans.ResponseLabel.Length > 3)
                        {
                            toUpdate.AnswerText = ans.ResponseLabel;
                        }

                        if (ans.Correct.HasValue && ans.Correct.Value)
                        {
                            toUpdate.IsCorrect = true;
                        }
                    }

                    int correctCount = ((SingleOptionDetails)d.Details).a.Count(a => a.IsCorrect);
                    if (correctCount != 1)
                        throw new InvalidRequestException("SingleChoice must have exactly 1 correct answer.");
                }
            }
            else if (qType == QuestionType.MultipleOption)
            {
                if (s.Options != null)
                {
                    if (s.Options.Count > 4)
                        throw new InvalidRequestException("MultipleChoice can have at most 4 answers.");

                    if (s.Options.Any(a => a.OptionKey == 0))
                        throw new InvalidRequestException("AnswerID cannot be zero.");

                    foreach (var ans in s.Options)
                    {
                        if (!((MultipleOptionDetails)d.Details).Answers.Any(a => a.ResponseId == ans.OptionKey))
                            throw new InvalidRequestException("Answer must belong to the given question.");
                    }

                    foreach (var ans in s.Options)
                    {
                        var toUpdate = ((MultipleOptionDetails)d.Details).Answers.First(a => a.ResponseId == ans.OptionKey);

                        if (!string.IsNullOrWhiteSpace(ans.ResponseLabel) && ans.ResponseLabel.Length > 3)
                        {
                            toUpdate.Content = ans.ResponseLabel;
                        }

                        if (ans.Correct.HasValue && ans.Correct.Value)
                        {
                            toUpdate.Correct = true;
                        }
                    }

                    int correctCount = ((MultipleOptionDetails)d.Details).Answers.Count(a => a.Correct);
                    if (correctCount < 2)
                        throw new InvalidRequestException("MultipleChoice must have at least 2 correct answers.");
                }
            }
            else if (qType == QuestionType.Boolean)
            {
                if (s.CategoryId != null && s.LinkedAnswerId != null)
                {
                    if (s.LinkedAnswerId != ((BooleanDetails)d.Details).CorrectAnswer.ResponseId)
                        throw new InvalidRequestException("Provided AnswerID does not belong to this question.");

                    ((BooleanDetails)d.Details).CorrectAnswer.Correct = s.ExpectedTrueFalse.Value;
                    ((BooleanDetails)d.Details).CorrectAnswer.Content = s.ExpectedTrueFalse.Value ? "True" : "False";
                }
            }
            else if (qType == QuestionType.TextEntry)
            {
                if (s.Options != null && s.LinkedAnswerId != null)
                {
                    if (s.LinkedAnswerId != ((TextEntryDetails)d.Details).CorrectAnswer.ResponseId)
                        throw new InvalidRequestException("Provided AnswerID does not belong to this question.");

                    ((TextEntryDetails)d.Details).CorrectAnswer.Content = s.ExpectedTextAnswer;
                }
            }

            return d;
        }
    }
}
