using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.BaseResponse
{
    public class ErrorDataResponse<T> : IDataResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorDataResponse(T data, string message)
        {
            Data = data;
            Message = message;
            IsSuccess = false;
        }
        public ErrorDataResponse(T data) : this(data, null)
        {
            Data = data;
        }

    }
}
