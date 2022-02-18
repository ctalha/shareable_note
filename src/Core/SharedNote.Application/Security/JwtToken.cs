using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Security
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expirations { get; set; }
    }
}
