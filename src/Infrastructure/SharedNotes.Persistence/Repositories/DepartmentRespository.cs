using Microsoft.EntityFrameworkCore;
using SharedNote.Application.Interfaces.Repositories;
using SharedNote.Domain.Entites;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories.BaseRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Repositories
{
    public class DepartmentRespository : GenericBaseRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRespository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllDepartmentsByCollegeIdAsync(int id)
        {
            return await _context.Departments.Where(p => p.CollegeId == id).ToListAsync();

        }
    }
}
