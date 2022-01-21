using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.Interfaces.Repositories;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace SharedNotes.Persistence.Repositories.BaseRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            collegeRepository = new CollegeRepository(_context);
            departmentRepository = new DepartmentRespository(_context);
            fileDocumentRespository = new FileDocumentRespository(_context);
        }
        public ICollegeRepository collegeRepository { get; private set; }

        public IDepartmentRepository departmentRepository { get; private set; }

        public IFileDocumentRespository fileDocumentRespository { get; private set; }

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
