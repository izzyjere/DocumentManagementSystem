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
            return GetAll()
                 .Include(d => d.Pages)
                                          .Include(d => d.Library)
                                          .ThenInclude(l => l.Cabinet)
                                          .ThenInclude(c => c.FileStore)
                .Where(d =>  d.Status!=Status.ARCHIVE && ( d.Code.Contains(query)||
                                             d.Description.Contains(query)||
                                             d.Pages.Any(p=>p.FileName.Contains(query))||
                                             d.Library.Name.Contains(query) ||
                                             d.Library.Code.Contains(query) ||
                                             d.SubmittedBy.Contains(query) ||
                                             d.Library.Cabinet.Code.Contains(query) ||
                                             d.Library.Cabinet.Name.Contains(query)));
          
        }
    }
}
