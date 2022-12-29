using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WatchItem.DependencyInjection;

namespace WatchItem.Tests;

public class DiProvider : IDisposable
{
    public IServiceProvider ServiceProvider { get; }

    public DiProvider()
    {
        ServiceProvider = new ServiceCollection()
            .AddAppServices(builder => builder.UseSqlite("DataSource=:memory:"))
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