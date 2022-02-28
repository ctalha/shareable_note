using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.Interfaces.Repositories;
using SharedNotes.Persistence.Context;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Repositories.BaseRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            CollegeRepository = new CollegeRepository(_context);
            DepartmentRepository = new DepartmentRespository(_context);
            FileDocumentRespository = new FileDocumentRespository(_context);
            UserRepository = new UserRepository(_context);
        }
        public ICollegeRepository CollegeRepository { get; private set; }

        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IFileDocumentRespository FileDocumentRespository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async ValueTask DisposeAsync()
        {
            if (_context == null) return;
            await _context.DisposeAsync();
        }
    }
}
