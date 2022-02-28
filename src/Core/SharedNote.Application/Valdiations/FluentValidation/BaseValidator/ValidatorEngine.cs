using FluentValidation;
using FluentValidation.Results;

namespace SharedNote.Application.Valdiations.FluentValidation.BaseValidator
{
    public class ValidatorEngine<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            return result;
        }
    }
}
