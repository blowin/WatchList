using Microsoft.Extensions.DependencyInjection;
using WatchList.Infrastructure.Database;

namespace WatchList.Tests;

public class AppDbContextTest : IClassFixture<DiProvider>
{
    private readonly DiProvider _provider;

    public AppDbContextTest(DiProvider provider)
    {
        _provider = provider;
    }

    [Fact]
    public void EnsureCreate()
    {
        using var scope = _provider.ServiceProvider.CreateScope();
        var appDb = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        appDb.Database.EnsureCreated();
    }
}