using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Exceptions
{
    public class ValidationExceptionResponse : BaseExceptionResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
