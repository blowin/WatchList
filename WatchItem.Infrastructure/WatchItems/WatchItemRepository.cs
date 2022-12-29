using WatchItem.Infrastructure.Database;
using WatchList.Domain.WatchItems;

namespace WatchItem.Infrastructure.WatchItems;

public sealed class WatchItemRepository : Repository<WatchList.Domain.WatchItems.WatchItem>, IWatchItemRepository
{
    public WatchItemRepository(AppDbContext db) 
        : base(db)
    {
    }
}