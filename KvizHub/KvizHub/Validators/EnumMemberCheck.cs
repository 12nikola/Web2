using System;
using System.ComponentModel.DataAnnotations;

namespace KvizHub.CustomValidation
{
    public class EnumMemberCheck : ValidationAttribute
    {
        private readonly Type _targetEnum;

        public EnumMemberCheck(Type targetEnum)
        {
            _targetEnum = targetEnum;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext ctx)
        {
            if (!Enum.IsDefined(_targetEnum, value))
            {
                return new ValidationResult($"{value} is not a valid member of {_targetEnum.Name}.");
            }

            return ValidationResult.Success;
        }
    }
}
