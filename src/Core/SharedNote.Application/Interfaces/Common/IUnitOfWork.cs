using SharedNote.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Interfaces.Common
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICollegeRepository collegeRepository { get; }
        IDepartmentRepository departmentRepository { get; }
        IFileDocumentRespository fileDocumentRespository { get; }
        Task<bool> CompleteAsync();

    }
}
