using RTSADocs.Data.DataAccess;

namespace RTSADocs.Data.Services
{
    [Service]
    internal class DocumentArchiveService : CrudServiceBase<DocumentArchive>, IDocumentArchiveService
    {
        public DocumentArchiveService(IRepository<DocumentArchive> repository) : base(repository)
        {
        }
    }
}
