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
        public int StatusCode { get; set; }
        public ErrorDataResponse(T data, string message,int statusCode)
        {
            Data = data;
            Message = message;
            IsSuccess = false;
            StatusCode = statusCode;
        }
        public ErrorDataResponse(T data,int statusCode) : this(data, null,statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }

    }
}
