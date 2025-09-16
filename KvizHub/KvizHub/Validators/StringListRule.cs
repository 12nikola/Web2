using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KvizHub.Validator
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringListRule : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public StringListRule(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            var items = value as IEnumerable<string>;

            if (items != null)
            {
                foreach (var str in items)
                {
                    if (string.IsNullOrWhiteSpace(str) || str.Length < _minLength || str.Length > _maxLength)
                    {
                        return new ValidationResult($"Each item must be between {_minLength} and {_maxLength} characters.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
