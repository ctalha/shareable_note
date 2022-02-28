using SharedNote.Application.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace SharedNote.Application.Interfaces.Common
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICollegeRepository CollegeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IFileDocumentRespository FileDocumentRespository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task<bool> CompleteAsync();

    }
}
