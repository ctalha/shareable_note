using SharedNote.Application.Interfaces.Repositories;
using SharedNote.Domain.Entites;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories.BaseRepositories;

namespace SharedNotes.Persistence.Repositories
{
    public class FileDocumentRespository : GenericBaseRepository<FileDocument>, IFileDocumentRespository
    {
        public FileDocumentRespository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
