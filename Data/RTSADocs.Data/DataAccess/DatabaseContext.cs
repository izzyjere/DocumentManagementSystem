using Microsoft.EntityFrameworkCore;

using RTSADocs.Shared.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Data.DataAccess
{
    public class DatabaseContext   : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = Assembly.GetExecutingAssembly().GetTypes()
             .Where(t => typeof(IEntity).IsAssignableFrom(t) && t != typeof(IEntity));

            foreach (var entityType in entityTypes)
            {
                var entity = modelBuilder.Entity(entityType);

                // Automatically include any navigation properties
                foreach (var navigation in entityType.GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) || p.PropertyType.IsAssignableTo(typeof(IEntity))))
                {
                    entity.Navigation(navigation.Name).AutoInclude();
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
