using WatchList.Domain.WatchItems.Entity;
using WatchList.Domain.WatchItems.Repository;
using X.PagedList;

namespace WatchList.Domain.WatchItems
{
    public class WatchItemService : IScoped
    {
        private readonly IWatchItemRepository _db;

        public WatchItemService(IWatchItemRepository db)
        {
            _db = db;
        }

        public Task<IPagedList<BriefWatchItemDetail>> GetBriefWatchItemDetailPageAsync(BriefWatchItemDetailFilter filter)
        {
            return _db.GetBriefWatchItemDetailPageAsync(filter);
        }
    }
}
