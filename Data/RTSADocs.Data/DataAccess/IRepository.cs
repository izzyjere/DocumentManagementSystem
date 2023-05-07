using RTSADocs.Shared.Contracts;

namespace RTSADocs.Data.DataAccess
{
    internal interface IRepository<TModel> where TModel : class, IEntity
    {
        IQueryable<TModel> GetAll();
        int Save(TModel model);       
        int Delete(Guid id);      
        TModel? Get(Guid id);  
    }
}
