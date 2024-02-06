using DMS.Data.DataAccess;

namespace DMS.Data.Services
{
    [Service]
    internal class DashboardService : IDashboardService
    {
        readonly DatabaseContext db;
        public DashboardService(DatabaseContext db)
        {
            this.db = db;
        }
        public int Count<TModel>() where TModel : class, IEntity
        {
            return db.Set<TModel>().Count();
        }
    }
}
