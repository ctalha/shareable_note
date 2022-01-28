using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Valdiations.FluentValidation
{
    public class ValidationResponse
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
