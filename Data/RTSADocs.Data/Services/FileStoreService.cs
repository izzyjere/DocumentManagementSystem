global using RTSADocs.Shared.Contracts;

using Microsoft.EntityFrameworkCore;

using RTSADocs.Data.DataAccess;

namespace RTSADocs.Data.Services
{
    [Service]
    internal class FileStoreService : CrudServiceBase<FileStore>, IFileStoreService
    {
        public FileStoreService(IRepository<FileStore> repository) : base(repository)
        {
        }         
    }
}
