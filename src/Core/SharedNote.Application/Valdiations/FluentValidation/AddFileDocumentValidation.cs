using FluentValidation;
using SharedNote.Application.CQRS.Commands;
using SharedNote.Application.Valdiations.FluentValidation.BaseValidator;

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
