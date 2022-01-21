using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
