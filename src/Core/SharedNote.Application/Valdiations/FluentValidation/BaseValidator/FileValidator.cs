using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Valdiations.FluentValidation.BaseValidator
{
    public class FileValidator:ValidatorEngine<IFormFile>
    {
        public FileValidator()
        {

            RuleFor(x => x.ContentType).NotNull().Must(
                x => x.Equals("image/jpeg") ||
                x.Equals("image/png") ||
                x.Equals("image/jpeg") ||
                x.Equals("application/pdf ") ||
                x.Equals("application/zip") ||
                x.Equals("application/octet-stream")
                )
                .WithMessage("Desteklenmeyen Dosya Türü");
        }
    }
}
