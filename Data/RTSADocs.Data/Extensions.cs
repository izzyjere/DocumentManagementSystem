using Microsoft.Extensions.DependencyInjection;

using RTSADocs.Data.DataAccess;
using RTSADocs.Shared.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            return services;
        }
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddDomainServices();
           return services;
        }
    }
}
