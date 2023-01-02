using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WatchList.DependencyInjection;

namespace WatchList.Tests;

public class DiProvider : IDisposable
{
    public IServiceProvider ServiceProvider { get; }

    public DiProvider()
    {
        ServiceProvider = new ServiceCollection()
            .AddAppServices(builder => SqliteDbContextOptionsBuilderExtensions.UseSqlite(builder, "DataSource=:memory:"))
            .BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
    }

    public void Dispose()
    {
        if(ServiceProvider is IDisposable d)
            d.Dispose();
    }
}