using Microsoft.EntityFrameworkCore;
using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Repositories;
using SharedNote.Domain.Entites;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Repositories
{
    public class UserRepository : GenericBaseRepository<User> , IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<UserDto> GetAllUserWithRoles()
        {
            var result = from u in _context.Users
                         select new UserDto
                         {
                             UserName = u.UserName,
                             Id = u.Id,
                             Email = u.Email,
                             Roles = (from us in _context.UserRoles
                                      join r in _context.Roles on us.RoleId equals r.Id
                                      where us.UserId == u.Id
                                      select r.Name).FirstOrDefault()
                         };
            return result;
        }
    }
}
