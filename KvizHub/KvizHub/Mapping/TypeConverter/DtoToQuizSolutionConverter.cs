using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using KvizHub.Exceptions;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Models.Base;
using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Response;
using KvizHub.Models.Solution;
using KvizHub.Models.User;
using KvizHub.Models.QuizSolution;


namespace KvizHub.Mapping.Converters
{
    public class DtoToQuizSolutionConverter : ITypeConverter<SolQuizDTO, QuizSolutionConversionModel>
    {
        public QuizSolutionConversionModel Convert(SolQuizDTO s, QuizSolutionConversionModel d, ResolutionContext c)
        {
            if (d.Quiz == null || d.Quiz.Description == null || d.User == null || d.QuestionDetails == null
                || d.QuestionDetails.Count != d.SolutionDetails.Count)
            {
                throw new ServerErrorException("Invalid internal state.");
            }

            bool allMatch = d.QuestionDetails.All(qd => d.Quiz.Description.Any(qi => qi == qd.QuizQuestionDetailsId));
            if (!allMatch)
            {
                throw new ServerErrorException("Mismatched question details.");
            }

            bool validQuestions = s.solves.All(q => d.Quiz.Description.Any(qi => qi == q.QuestionId));
            if (!validQuestions)
            {
                throw new InvalidRequestException("The request contains answers for questions not in this quiz.");
            }

            if (d.Quiz.TimeLimit < s.MaxDuration)
            {
                throw new InvalidRequestException("The quiz duration cannot exceed the time limit.");
            }

            int correctCount = 0;
            int wrongCount = 0;
            int totalCount = 0;

            var attempt = new QuizAttempt
            {
                ParentQuiz = d.Quiz,
                QuizId = d.Quiz.Id,
                Username = d.User.Username,
                Duration = s.MaxDuration,
                AttemptDate = DateTime.Now
            };

            d.QuestionDetails = new List<QuizQuestionDetailBase>();
            attempt.QuestionSolutions = new List<QuizQuestionSolutionInfo>();

            foreach (var qInfo in d.Quiz.Category)
            {
                var qDetails = d.QuestionDetails.FirstOrDefault(x => x.QuizQuestionId == qInfo);
                var userAnswer = s.solves.FirstOrDefault(x => x.QuestionId == qInfo);

                var qSolutionInfo = new QuizQuestionSolutionInfo
                {
                    QuizQuestionInfoId = qInfo,
                    QuizAttempt = attempt,
                    QuizQuestionSolutionInfoId = qInfo
                };

                if (qInfo == QuestionType.SingleOption)
                {
                    totalCount++;

                    if (userAnswer?.SingleLabel == null)
                    {
                        attempt.QuestionSolutions.Add(qSolutionInfo);
                        continue;
                    }

                    var details = (SingleOptionDetails)qDetails;

                    var sol = new SingleOptionSolution
                    {
                        QuizSolutionInfo = qSolutionInfo,
                        Answer = new SingleOptionUserAnswer()
                    };

                    sol.Answer.SOSolution = sol;

                    var correctAns = details.QuizQuestion.(x => x.AnswerText == userAnswer.SingleUserAnswer);
                    if (correctAns == null)
                        throw new InvalidRequestException("Answer must be from available options.");

                    if (correctAns.IsCorrect) correctCount++;
                    else wrongCount++;

                    sol.Answer.ResponseText = userAnswer.SingleLabel;

                    attempt.QuestionSolutions.Add(qSolutionInfo);
                    d.SolutionDetails.Add(sol);
                }
                else if (qInfo. == QuestionType.MultipleOption)
                {
                    var details = (MultipleOptionDetails)qDetails;
                    totalCount += details.Answers.Count(a => a.Correct);

                    if (userAnswer?.MultipleLabels == null)
                    {
                        attempt.QuestionSolutions.Add(qSolutionInfo);
                        continue;
                    }

                    var sol = new MultipleOptionSolution
                    {
                        QuizSolutionInfo = qSolutionInfo,
                        Answers = new MultipleOptionUserAnswer()
                    };

                    foreach (var ans in userAnswer.MultipleLabels)
                    {
                        var validAns = details.Answers.Find(x => x.Content == ans);
                        if (validAns == null)
                            throw new InvalidRequestException("Answer must be from available options.");

                        if (validAns.Correct) correctCount++;
                        else wrongCount++;

                        sol.Answers.Add(new MultipleOptionUserAnswer
                        {
                            ResponseText = ans,
                            MOSolution = sol
                        });
                    }

                    attempt.QuestionSolutions.Add(qSolutionInfo);
                    d.SolutionDetails.Add(sol);
                }
                else if (qInfo. == QuestionType.Boolean)
                {
                    totalCount++;

                    if (userAnswer?.SingleLabel == null)
                    {
                        attempt.QuestionSolutions.Add(qSolutionInfo);
                        continue;
                    }

                    var details = (BooleanDetails)qDetails;

                    var sol = new BooleanSolution
                    {
                        QuizSolutionInfo = qSolutionInfo,
                        Answer = new BooleanUserAnswer()
                    };

                    sol.Answer.BSolution = sol;

                    if (details.CorrectAnswer.IsCorrect == userAnswer.BooleanLabel) correctCount++;
                    else wrongCount++;

                    sol.Answer.ResponseText = (bool)userAnswer.BooleanLabel ? "True" : "False";

                    attempt.QuestionSolutions.Add(qSolutionInfo);
                    d.SolutionDetails.Add(sol);
                }
                else if (qInfo== QuestionType.TextEntry)
                {
                    totalCount++;

                    if (userAnswer?.SingleLabel == null)
                    {
                        attempt.QuestionSolutions.Add(qSolutionInfo);
                        continue;
                    }

                    var details = (TextEntryDetails)qDetails;

                    var sol = new TextEntrySolution
                    {
                        QuizSolutionInfo = qSolutionInfo,
                        Answer = new TextEntryUserAnswer()
                    };

                    sol.Answer.TESolution = sol;

                    if (details.CorrectAnswer.Content.Trim().ToLower() == userAnswer.SingleLabel.Trim().ToLower())
                        correctCount++;
                    else
                        wrongCount++;

                    sol.Answer.ResponseText = userAnswer.SingleLabel;

                    attempt.QuestionSolutions.Add(qSolutionInfo);
                    d.SolutionDetails.Add(sol);
                }
                else
                {
                    throw new InvalidRequestException("Unsupported question type.");
                }
            }

            d.Attempt = attempt;

            double points = Math.Max(0, (correctCount * 10 - wrongCount * 5));
            points = Math.Round(points);

            double maxPoints = totalCount * 10;
            double percentage = maxPoints > 0 ? (points / maxPoints) * 100 : 0;

            d.Attempt.TotalAnswersCount = totalCount;
            d.Attempt.IncorrectAnswersCount = wrongCount;
            d.Attempt.CorrectAnswersCount = correctCount;
            d.Attempt.ScorePercentage = percentage;
            d.Attempt.ScorePoints = points;

            return d;
        }
    }
}
