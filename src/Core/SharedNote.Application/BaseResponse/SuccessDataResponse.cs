using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.BaseResponse
{
    public class SuccessDataResponse<T> : IDataResponse<T>
    {
        public T Data { get; set ; }
        public string Message { get; set ; }
        public bool IsSuccess { get ; set; }
        public SuccessDataResponse(T data, string message)
        {
            Data = data;
            Message = message;
            IsSuccess = true;
        }
        public SuccessDataResponse(T data):this(data,null)
        {
            Data = data;
        }

    }
}
