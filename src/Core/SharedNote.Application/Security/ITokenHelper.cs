using SharedNote.Domain.Entites;

namespace SharedNote.Application.Security
{
    public interface ITokenHelper
    {
        JwtToken CreateToken(User user, string role);
    }
}
