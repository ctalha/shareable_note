using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.BaseResponse
{
    public interface IResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

    }
}
