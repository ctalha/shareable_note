using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.ValidationResponse
{
    public class ValidationResponseGlobal
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ValidationError Errors { get; set; }

    }
    public class ValidationError
    {
        public Dictionary<string, List<string>> Fields { get; set; }
        public ValidationError()
        {
            Fields = new Dictionary<string, List<string>>();
        }
    }
    public class ErrorDescription
    {
        public string FieldName { get; set; }
        public List<string> FieldDescription { get; set; }
    }
}
