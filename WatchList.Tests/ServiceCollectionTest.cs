using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WatchList.Domain.WatchItems.Repository;
using WatchList.Infrastructure.Database;
using WatchList.Infrastructure.WatchItems;

namespace WatchList.Tests;

public class ServiceCollectionTest : IClassFixture<DiProvider>
{
    private readonly DiProvider _provider;

    public ServiceCollectionTest(DiProvider provider)
    {
        _provider = provider;
    }

    [Fact]
    public void AddAppServices()
    {
        using var scope = _provider.ServiceProvider.CreateScope();
        var appDb = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var watchItemRepository = scope.ServiceProvider.GetRequiredService<IWatchItemRepository>();
        
        appDb.Should().NotBeNull();
        
        watchItemRepository.Should().NotBeNull();
        watchItemRepository.Should().BeAssignableTo<WatchItemRepository>();
    }
}