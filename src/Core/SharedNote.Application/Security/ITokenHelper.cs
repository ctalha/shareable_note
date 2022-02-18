using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Security
{
    public interface ITokenHelper
    {
        JwtToken CreateToken(User user, string role);
    }
}
