using DMS.Data.DataAccess;

namespace DMS.Data.Services
{
    [Service]
    internal class DocumentArchiveService : CrudServiceBase<DocumentArchive>, IDocumentArchiveService
    {
        public DocumentArchiveService(IRepository<DocumentArchive> repository) : base(repository)
        {
        }
    }
}
