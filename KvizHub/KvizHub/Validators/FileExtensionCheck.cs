using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace KvizHub.CustomValidation
{
    public class FileExtensionCheck : ValidationAttribute
    {
        private readonly string[] _permitted;

        public FileExtensionCheck(string[] allowed)
        {
            _permitted = allowed;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext context)
        {
            var upload = value as IFormFile;

            if (upload != null)
            {
                var ext = Path.GetExtension(upload.FileName).ToLowerInvariant();

                if (!_permitted.Contains(ext))
                {
                    return new ValidationResult($"Extension {ext} is not supported.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

