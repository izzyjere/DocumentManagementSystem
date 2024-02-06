using Microsoft.EntityFrameworkCore;

using DMS.Data.Services;

namespace DMS.Data.DataAccess
{
    public class DatabaseContext   : DbContext
    {
        public ICurrentUserService currentUserService;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, ICurrentUserService currentUserService) : base(options) { 
                     this.currentUserService = currentUserService;
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                var userName = currentUserService .GetUserName();
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy ??= userName;
                        break;
                    case EntityState.Modified:
                        entry.Property(e => e.CreatedBy).IsModified = false;
                        entry.Property(e => e.CreatedOn).IsModified = false;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy ??= currentUserService.GetUserName();
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = typeof(Document).Assembly.GetTypes()
             .Where(t => typeof(IEntity).IsAssignableFrom(t) && t != typeof(IEntity) && !t.IsAbstract && !t.IsInterface);

            foreach (var entityType in entityTypes)
            {
                var entity = modelBuilder.Entity(entityType);

                // Automatically include any navigation properties
                foreach (var navigation in entityType.GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) || p.PropertyType.IsAssignableTo(typeof(IEntity))))
                {
                    entity.Navigation(navigation.Name).AutoInclude();
                }
            }

           
        }
    }
}
