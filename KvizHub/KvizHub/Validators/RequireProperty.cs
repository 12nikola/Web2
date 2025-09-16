using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace KvizHub.CustomValidation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RequireProperty : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object instance, ValidationContext ctx)
        {
            if (instance == null)
                return new ValidationResult("Entity cannot be null.");

            var props = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            bool hasValue = props.Any(p =>
            {
                var val = p.GetValue(instance);
                var type = p.PropertyType;

                if (type == typeof(string))
                    return !string.IsNullOrWhiteSpace(val as string);

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type) && type != typeof(string))
                    return val is System.Collections.IEnumerable seq && seq.Cast<object>().Any();

                if (Nullable.GetUnderlyingType(type) != null)
                    return val != null;

                return val != null;
            });

            return hasValue ? ValidationResult.Success
                            : new ValidationResult(ErrorMessage ?? "Provide at least one property value.");
        }
    }
}

