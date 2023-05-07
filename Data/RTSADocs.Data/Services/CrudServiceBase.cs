using RTSADocs.Data.DataAccess;
using RTSADocs.Shared.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Data.Services
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
            return _repository.Delete(id) != 0;
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
