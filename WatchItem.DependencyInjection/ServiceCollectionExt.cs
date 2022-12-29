using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WatchItem.Infrastructure.Database;
using WatchList.Domain;

namespace WatchItem.DependencyInjection;

public static class ServiceCollectionExt
{
    public static IServiceCollection AddAppServices(this IServiceCollection self, Action<DbContextOptionsBuilder> configuration, 
        params Assembly[]? assemblies)
    {
        self.Scan(selector =>
        {
            if (assemblies == null || assemblies.Length == 0)
                assemblies = new[] { typeof(AppDbContext).Assembly };
            
            selector.FromAssemblies(assemblies)
                .AddClasses(f => f.AssignableTo(typeof(IRepository<>))).AsImplementedInterfaces().WithScopedLifetime();
        });
        
        return self.AddDbContextPool<AppDbContext>(configuration);
    }
}