﻿using DMS.Data.DataAccess;

namespace DMS.Data.Services
{
    [Service]
    internal class CabinetService : CrudServiceBase<Cabinet>, ICabinetService
    {
        public CabinetService(IRepository<Cabinet> repository) : base(repository)
        {
        }
    }
}
