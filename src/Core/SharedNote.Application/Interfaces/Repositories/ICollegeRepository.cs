using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Entites;
using System.Threading.Tasks;

namespace SharedNote.Application.Interfaces.Repositories
{
    public interface ICollegeRepository : IGenericRepository<College>
    {
        Task<College> GetCollegesWithDepartmentByIdAsync(int id);
    }
}
