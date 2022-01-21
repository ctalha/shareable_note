using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.BaseResponse
{
    public class SuccessResponse : IResponse
    {
        public string Message { get ; set; }
        public bool IsSuccess { get ; set; }
        public SuccessResponse(string message)
        {
            Message = message;
            IsSuccess = true;
        }
        public SuccessResponse()
        {

        }
    }
}
