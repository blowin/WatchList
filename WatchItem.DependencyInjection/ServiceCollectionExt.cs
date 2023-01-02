using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using WatchItem.Infrastructure.Database;
using WatchList.Domain;
using WatchList.Domain.WatchItems;

namespace WatchItem.DependencyInjection;

public static class ServiceCollectionExt
{
    public static IServiceCollection AddAppServices(this IServiceCollection self, Action<DbContextOptionsBuilder> configuration, 
        params Assembly[]? assemblies)
    {
        self.Scan(selector =>
        {
            var assemblySeq = assemblies.ToAnalyzedAssemblies().Distinct();
            selector.FromAssemblies(assemblySeq)
                .AddSpecificLifetime<ITransient>(ServiceLifetime.Transient)
                .AddSpecificLifetime<IScoped>(ServiceLifetime.Scoped)
                .AddSpecificLifetime<ISingleton>(ServiceLifetime.Singleton)
                ;
        });
        
        return self.AddDbContextPool<AppDbContext>(configuration);
    }

    private static IImplementationTypeSelector AddSpecificLifetime<T>(this IImplementationTypeSelector self, ServiceLifetime lifetime)
    {
        return self.AddClasses(f => f.AssignableTo<T>())
            .AsImplementedInterfaces(p => p != typeof(T))
            .AsSelf()
            .WithLifetime(lifetime);
    }

    private static IEnumerable<Assembly> ToAnalyzedAssemblies(this Assembly[]? self)
    {
        if (self != null)
        {
            foreach (var assembly in self)
                yield return assembly;
        }

        yield return typeof(AppDbContext).Assembly;
        yield return typeof(WatchItemService).Assembly;
    }
}