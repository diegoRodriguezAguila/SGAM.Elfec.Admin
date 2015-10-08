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
            var __ValidationErrors = new StringBuilder();
            foreach (var validationFailure in result.Errors)
            {
                __ValidationErrors.Append(validationFailure.ErrorMessage);
                __ValidationErrors.Append(Environment.NewLine);
            }
            return __ValidationErrors.ToString();
        }
    }
}
