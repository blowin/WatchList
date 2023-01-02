using WatchList.Domain.WatchItems.Entity;
using X.PagedList;

namespace WatchList.Domain.WatchItems.Repository;

public interface IWatchItemRepository : IRepository<WatchItem>
{
    Task<IPagedList<BriefWatchItemDetail>> GetBriefWatchItemDetailPageAsync(BriefWatchItemDetailFilter filter,
        CancellationToken cancellationToken = default);
}