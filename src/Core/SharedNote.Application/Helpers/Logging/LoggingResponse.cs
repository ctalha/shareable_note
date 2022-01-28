using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedNote.Application.Helpers.Logging
{
    public class LoggingResponse
    {
        public string Path { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}
