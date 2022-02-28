namespace SharedNote.Application.Security
{
    public class TokenOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public double AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }

    }
}
