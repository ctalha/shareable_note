using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Exceptions
{
    public class ExceptionReponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
#nullable enable
        public string? Description { get; set; }
#nullable disable
        public string Message { get; set; }
    }
}
