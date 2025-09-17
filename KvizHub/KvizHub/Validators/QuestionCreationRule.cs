
using KvizHub.DTO.Quiz;
using KvizHub.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KvizHub.Validator
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class QuestionCreationRule : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? obj, ValidationContext ctx)
        {
            var q = obj as NewQuestionDTO;

            if (q == null)
                return ValidationResult.Success;

            switch (q.Type)
            {
                case QuestionType.SingleOption:
                    if (q.Choices == null || q.Choices.Count != 4)
                        return new ValidationResult("Single option must provide exactly 4 options.", new[] { nameof(q.Choices) });

                    if (q.Choices.Count(a => a.Valid) != 1)
                        return new ValidationResult("Single choice must have one correct option.", new[] { nameof(q.Choices) });
                    break;

                case QuestionType.MultipleOption:
                    if (q.Choices == null || q.Choices.Count < 4)
                        return new ValidationResult("Multiple choice requires at least 4 options.", new[] { nameof(q.Choices) });

                    if (q.Choices.Count(a => a.Valid) < 2)
                        return new ValidationResult("Multiple choice must have at least 2 correct options.", new[] { nameof(q.Choices) });
                    break;

                case QuestionType.Boolean:
                    if (q.ExpectedTrueFalse == null)
                        return new ValidationResult("True/False question must include an expected answer.", new[] { nameof(q.ExpectedTrueFalse) });
                    break;

                case QuestionType.TextEntry:
                    if (string.IsNullOrWhiteSpace(q.Label))
                        return new ValidationResult("Fill-in question must include a response.", new[] { nameof(q.Label) });
                    break;

                default:
                    return new ValidationResult("Invalid question type.", new[] { nameof(q.Type) });
            }

            return ValidationResult.Success;
        }
    }
}
