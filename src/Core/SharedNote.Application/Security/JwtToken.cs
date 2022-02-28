using System;

namespace SharedNote.Application.Security
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expirations { get; set; }
    }
}
