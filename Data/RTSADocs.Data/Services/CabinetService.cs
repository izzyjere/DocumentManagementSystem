using RTSADocs.Data.DataAccess;

namespace RTSADocs.Data.Services
{
    [Service]
    internal class CabinetService : CrudServiceBase<Cabinet>, ICabinetService
    {
        public CabinetService(IRepository<Cabinet> repository) : base(repository)
        {
        }
    }
}
