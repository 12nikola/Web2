using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Models.Answers;
using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Response;
using QuizWebServer.Exceptions;

namespace QuizWebServer.Mapping.Converters
{
    public class DtoToQuestionConverter : ITypeConverter<NewQuestionDTO, QuestionConversionModel>
    {
        public QuestionConversionModel Convert(NewQuestionDTO s, QuestionConversionModel d, ResolutionContext c)
        {
            var model = new QuestionConversionModel
            {
                Info = new QuestionDetails
                {
                    Text = s.Text,
                    QuestionCategoryID = s.QuestionCategoryID,
                    QuestionDetailsType = s.Type,
                }
            };

            if (s.Type == QuestionType.SingleOption)
            {
                var answers = c.Mapper.Map<List<SingleOptionAnswer>>(s.Label);

                if (answers.GroupBy(a => a.Content.Trim().ToLower()).Any(g => g.Count() > 1))
                {
                    throw new InvalidRequestException("Each answer must be unique");
                }

                answers.ForEach(x => x.ResponseId = 0);

                model.Details = new SingleOptionDetails
                {
                    QuizQuestion = model.Info,
                    QuizQuestionDetailsId = 0,
                    QuizQuestionId = 0,
                };

                ((SingleOptionDetails)model.Details).Answers.ForEach(x => x.SingleOptionDetails = (SingleOptionDetails)model.Details);
            }
            else if (s.Type == QuestionType.MultipleOption)
            {
                var answers = c.Mapper.Map<List<MultipleOptionAnswer>>(s.Label);

                if (answers.GroupBy(a => a.Content.Trim().ToLower()).Any(g => g.Count() > 1))
                {
                    throw new InvalidRequestException("Each answer must be unique");
                }

                answers.ForEach(x => x.ResponseId = 0);

                model.Details = new MultipleOptionDetails
                {
                    QuizQuestion = model.Info,
                    QuizQuestionDetailsId = 0,
                    Answers = answers
                };

                ((MultipleOptionDetails)model.Details).Answers.ForEach(x => x.QuestionDetails = (MultipleOptionDetails)model.Details);
            }
            else if (s.Type == QuestionType.Boolean)
            {
                model.Details = new BooleanDetails
                {
                    QuizQuestion = model.Info,
                    QuizQuestionDetailsId = 0,
                    CorrectAnswer = new BooleanAnswer
                    {
                        ResponseId = 0,
                        Content = (bool)s.ExpectedTrueFalse ? "True" : "False",
                        Correct = (bool)s.ExpectedTrueFalse
                    }
                };

                ((BooleanDetails)model.Details).CorrectAnswer.QuestionDetails = (BooleanDetails)model.Details;
            }
            else if (s.Type == QuestionType.TextEntry)
            {
                model.Details = new TextEntryDetails
                {
                    QuizQuestion = model.Info,
                    QuizQuestionDetailsId = 0,
                    CorrectAnswer = new TextEntryAnswer
                    {
                        ResponseId = 0,
                        Content = s.ExpectedTextChoices,
                        Correct = true
                    }
                };

                ((TextEntryDetails)model.Details).CorrectAnswer.QuestionDetails = (TextEntryDetails)model.Details;
            }

            return model;
        }
    }
}
