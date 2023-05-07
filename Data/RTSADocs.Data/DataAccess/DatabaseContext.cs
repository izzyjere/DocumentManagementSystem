using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Data.DataAccess
{
    public class DatabaseContext   : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }               
        
    }
}
