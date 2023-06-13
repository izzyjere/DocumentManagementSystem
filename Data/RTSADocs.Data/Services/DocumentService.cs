global using RTSADocs.Shared.Models;

using Microsoft.EntityFrameworkCore;

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
            return GetAll().Include(d => d.Library).ThenInclude(l => l.Cabinet).Where(d => d.Code.Contains(query)||
                                             d.Description.Contains(query)||
                                             d.Library.Name.Contains(query) ||
                                             d.Library.Code.Contains(query) ||
                                             d.Library.Cabinet.Code.Contains(query) ||
                                             d.Library.Cabinet.Name.Contains(query));
        }
    }
}
