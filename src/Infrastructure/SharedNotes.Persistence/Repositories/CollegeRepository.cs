using Microsoft.EntityFrameworkCore;
using SharedNote.Application.Interfaces.Repositories;
using SharedNote.Domain.Entites;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories.BaseRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Repositories
{
    public class CollegeRepository : GenericBaseRepository<College>, ICollegeRepository
    {
        private readonly ApplicationDbContext _context;
        public CollegeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<College> GetCollegesWithDepartmentByIdAsync(int id)
        {
            return await _context.Colleges.Include(p => p.Departments)
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
