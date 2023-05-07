using RTSADocs.Shared.Contracts;

namespace RTSADocs.Data.Services
{
    internal interface ICrudService<TModel> where TModel : class, IEntity
    {
        IQueryable<TModel> GetAll();
        TModel GetById(Guid id);
        bool Save(TModel model);
        bool Delete(Guid id);
        bool Update(TModel model);
    }
}
