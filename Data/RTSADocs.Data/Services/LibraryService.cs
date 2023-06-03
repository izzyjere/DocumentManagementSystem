using RTSADocs.Data.DataAccess;

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
