using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using RTSADocs.Data.DataAccess;
using RTSADocs.Data.Services;
namespace RTSADocs.Data
{
    public static class Extensions
    {
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var modelTypes = typeof(Entity).Assembly.GetTypes()
            .Where(t => typeof(IEntity).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var modelType in modelTypes)
            {
                var repositoryType = typeof(IRepository<>).MakeGenericType(modelType);
                var implementationType = typeof(Repository<>).MakeGenericType(modelType);
                services.AddScoped(repositoryType, implementationType);
            }

            return services;
        }
        private static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var service = typeof(ServiceAttribute);
            var types = typeof(DocumentService).Assembly.GetTypes()
                               .Where(s=> s.IsDefined(service,true)  && !s.IsInterface).Select(s=> new
                               {
                                   Service = s.GetInterface($"I{s.Name}"),
                                   Implementation = s
                               });
            foreach (var type in types)
            {
                if(type.Service == null)
                {
                    throw new ArgumentException($"No Interface of type: I{type.Implementation.Name} was found.");
                }
                services.AddScoped(type.Service, type.Implementation);
            }
            return services;
        }
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
   
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            }, ServiceLifetime.Transient);
            services.AddRepositories();
            services.AddDomainServices();
            return services;
        }
        public static void PrintStackTrace(this Exception exception)
        {
            Console.WriteLine(exception.Message + exception.StackTrace);
        }
    }
}
