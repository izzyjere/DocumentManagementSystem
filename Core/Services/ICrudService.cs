﻿namespace DMS.Data.Services
{
    public interface ICrudService<TModel> where TModel : class, IEntity
    {
        IQueryable<TModel> GetAll();
        TModel? GetById(Guid id);
        bool Save(TModel model);
        bool Delete(Guid id);         
    }
}
