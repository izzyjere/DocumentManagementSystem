﻿using DMS.Data.DataAccess;

namespace DMS.Data.Services
{
    internal abstract class CrudServiceBase<TModel> : ICrudService<TModel> where TModel : class, IEntity
    {
        private readonly IRepository<TModel> _repository;
        public CrudServiceBase(IRepository<TModel> repository)
        {    
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool Delete(Guid id)
        {
            try
            {
                return _repository.Delete(id) != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+e.StackTrace);
                return false;
            }
            
        }

        public IQueryable<TModel> GetAll()
        {
            return _repository.GetAll();
        }

        public TModel? GetById(Guid id)
        {
            return _repository.Get(id);
        }

        public bool Save(TModel model)
        {
            return _repository.Save(model) != 0;
        }       
    }
}
