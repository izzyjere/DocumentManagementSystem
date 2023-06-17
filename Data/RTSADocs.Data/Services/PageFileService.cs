using Microsoft.EntityFrameworkCore;

using RTSADocs.Data.DataAccess;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Data.Services
{
    [Service]
    internal class PageFileService : CrudServiceBase<PageFile> ,IPageFileService
    {
        public PageFileService(IRepository<PageFile> repository) : base(repository)
        {
        }
        public async Task<bool> IsCleanable(string fileName)
        {
            return !(await GetAll().AnyAsync(f => Path.GetFileName(f.Path)==fileName));
        }
    }
}
