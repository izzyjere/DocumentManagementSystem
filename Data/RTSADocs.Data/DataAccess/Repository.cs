using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using RTSADocs.Shared.Contracts;


namespace RTSADocs.Data.DataAccess
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class, IEntity
    {
        private readonly DbContext database;
        public Repository(DbContext database)
        {
            this.database = database;
        }
        public int Delete(Guid id)
        {
            var record = Get(id);
            if(record is null)
            {
                return 0;
            }
            database.Set<TModel>().Remove(record);
            return database.SaveChanges();
        }

        public TModel? Get(Guid id)
        {
            return database.Set<TModel>().Find(id);
        }

        public IQueryable<TModel> GetAll() => database.Set<TModel>();

        public int Save(TModel model)
        {
            database.Set<TModel>().Add(model);
            return database.SaveChanges();
        }
    }
}
