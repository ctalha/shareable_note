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
        public string? Description { get; set; }
        public string Message { get; set; }
    }
}
