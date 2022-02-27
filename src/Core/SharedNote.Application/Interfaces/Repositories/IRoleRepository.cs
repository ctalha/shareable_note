using Microsoft.AspNetCore.Identity;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<UserRole>
    {
    }
}
