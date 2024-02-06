global using DMS.Shared.Contracts;

using DMS.Data.DataAccess;

namespace DMS.Data.Services
{
    [Service]
    internal class FileStoreService : CrudServiceBase<FileStore>, IFileStoreService
    {
        public FileStoreService(IRepository<FileStore> repository) : base(repository)
        {
        }         
    }
}
