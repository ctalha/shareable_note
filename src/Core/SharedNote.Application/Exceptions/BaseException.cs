using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Exceptions
{
    public class BaseException :  Exception
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string Messages { get; set; }
        public BaseException(int statusCode, string message, string description)
        {
            StatusCode = statusCode;
            IsSuccess = false;
            Description = description;
            Messages = message;
        }
        public BaseException()
        {

        }

    }
}
