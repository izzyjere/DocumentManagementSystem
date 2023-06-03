global using RTSADocs.Shared.Models;

using RTSADocs.Data.DataAccess;
using RTSADocs.Shared.Constants;

namespace RTSADocs.Data.Services
{
    [Service]
    internal class DocumentService : CrudServiceBase<Document>, IDocumentService
    {
        public DocumentService(IRepository<Document> repository) : base(repository)
        {
        }

        public IEnumerable<Document> Search(string query)
        {
            return GetAll().Where(d => d.Code.Contains(query)||
                                             d.Description.Contains(query)||
                                             d.Source.Name.Contains(query)||
                                             d.Source.Description.Contains(query));         }
    }
}
