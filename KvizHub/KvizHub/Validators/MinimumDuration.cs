using System;
using System.ComponentModel.DataAnnotations;

namespace KvizHub.Validator
{
    public class MinimumDuration : ValidationAttribute
    {
        private readonly TimeSpan _threshold;

        public MinimumDuration(int hours = 0, int minutes = 0, int seconds = 0)
        {
            _threshold = new TimeSpan(hours, minutes, seconds);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if (value is TimeSpan span)
            {
                if (span < _threshold)
                {
                    return new ValidationResult($"Duration must be at least {_threshold}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
