using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.BaseResponse
{
    public interface IDataResponse<T> : IResponse
    {
        public T Data { get; set; }
    }
}
