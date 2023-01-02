using WatchItem.Infrastructure.Database;
using WatchItem.Infrastructure.Extensions;
using WatchList.Domain.WatchItems.Repository;
using X.PagedList;

namespace WatchItem.Infrastructure.WatchItems;

public sealed class WatchItemRepository : Repository<WatchList.Domain.WatchItems.Entity.WatchItem>, IWatchItemRepository
{
    public WatchItemRepository(AppDbContext db) 
        : base(db)
    {
    }

    public Task<IPagedList<BriefWatchItemDetail>> GetBriefWatchItemDetailPageAsync(BriefWatchItemDetailFilter filter, CancellationToken cancellationToken = default)
    {
        var filteredQueryable = filter.ApplyFilter(Set);
        return filteredQueryable.OrderBy(e => e.Title)
            .Select(entity => new BriefWatchItemDetail(entity.Id, entity.Title, entity.Status, entity.Type, entity.Rating, entity.ReleaseDate))
            .ToPagedListAsync(filter.Page, cancellationToken);
    }
}