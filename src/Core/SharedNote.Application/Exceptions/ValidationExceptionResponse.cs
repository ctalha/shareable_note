using FluentValidation.Results;
using SharedNote.Application.Valdiations.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Exceptions
{
    public class ValidationExceptionResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationResponse> Errors { get; set; }
    }
}
