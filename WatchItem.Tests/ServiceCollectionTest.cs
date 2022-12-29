using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WatchItem.Infrastructure.Database;
using WatchItem.Infrastructure.WatchItems;
using WatchList.Domain.WatchItems;

namespace WatchItem.Tests;

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