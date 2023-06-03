using RTSADocs.Data.DataAccess;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Data.Services
{
    [Service]
    internal class LibraryService : CrudServiceBase<Library> , ILibraryService
    {
        public LibraryService(IRepository<Library> repository) : base(repository)
        {
        }
    }
}
