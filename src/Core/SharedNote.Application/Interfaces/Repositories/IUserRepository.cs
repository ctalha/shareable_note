using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Entites;
using System.Linq;

namespace SharedNote.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public IQueryable<UserDto> GetAllUserWithRoles();
    }
}
