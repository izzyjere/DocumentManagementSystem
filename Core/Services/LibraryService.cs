using DMS.Data.DataAccess;

namespace DMS.Data.Services
{
    [Service]
    internal class LibraryService : CrudServiceBase<Library> , ILibraryService
    {
        public LibraryService(IRepository<Library> repository) : base(repository)
        {
        }
    }
}
