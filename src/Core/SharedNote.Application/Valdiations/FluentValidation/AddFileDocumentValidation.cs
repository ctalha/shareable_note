using FluentValidation;
using FluentValidation.Results;
using SharedNote.Application.CQRS.Commands;
using SharedNote.Application.Dtos;
using SharedNote.Application.Valdiations.FluentValidation.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.Valdiations.FluentValidation
{
    public class AddFileDocumentValidation : ValidatorEngine<AddFileDocumentCommand>
    {
        public AddFileDocumentValidation()
        {
            RuleFor(p => p.DepartmentId).NotNull().NotEmpty()
                .WithMessage("Lütfen uygun bölümü seçiniz");
            RuleFor(p => p.CourseTitle).NotEmpty().NotNull()
                .WithMessage("Yüklenecek belge için ders ismi girilmek zorundadır.");
        }
    }
}
