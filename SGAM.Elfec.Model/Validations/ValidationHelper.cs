using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text;

namespace SGAM.Elfec.Model.Validations
{
    public class ValidationHelper
    {
        public static ValidationResult Validate<T, K>(K entity)
            where T : IValidator<K>, new()
            where K : class
        {
            IValidator<K> validator = new T();
            return validator.Validate(entity);
        }

        public static string GetError(ValidationResult result)
        {
            var __validationErrors = new StringBuilder();
            if (result.Errors.Count == 1)
                return __validationErrors.Append(result.Errors[0]).ToString();
            foreach (var validationFailure in result.Errors)
            {
                __validationErrors.Append('\u25CF');
                __validationErrors.Append(' ');
                __validationErrors.Append(validationFailure.ErrorMessage);
                __validationErrors.Append(Environment.NewLine);
            }
            return __validationErrors.ToString();
        }
    }
}
