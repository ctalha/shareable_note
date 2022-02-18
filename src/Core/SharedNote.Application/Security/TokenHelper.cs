using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Security
{
    public class TokenHelper : ITokenHelper
    {
        private readonly TokenOption _tokenOptions;
        public TokenHelper(IOptions<TokenOption> options)
        {
            _tokenOptions = options.Value;
        }
        public JwtToken CreateToken(User user, string role)
        {
            var accessTokenExpirations = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: accessTokenExpirations,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims:GetClaims(user,role)
                );
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtTokenHandler.WriteToken(jwt);

            return new JwtToken
            {
                Token = token,
                Expirations = accessTokenExpirations
            };
        }
        private IEnumerable<Claim> GetClaims(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,role)
            };
            //claims.AddRange(roles.Select(p => new Claim(ClaimTypes.Role, p)));
            
            return claims;
        }
    }
}
