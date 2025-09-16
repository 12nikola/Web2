using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KvizHub.CustomValidation
{
    public class FileSizeLimit : ValidationAttribute
    {
        private readonly int _limit;

        public FileSizeLimit(int limit)
        {
            _limit = limit;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext ctx)
        {
            var upload = value as IFormFile;

            if (upload != null && upload.Length > _limit)
            {
                return new ValidationResult($"File cannot exceed {_limit / 1024 / 1024} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
