using Microsoft.EntityFrameworkCore;
using WatchItem.Infrastructure.Database;

namespace WatchList.Blazor.Data
{
    public class WatchItemService
    {
        private AppDbContext _db;

        public WatchItemService(AppDbContext db)
        {
            _db = db;
        }

        public Task<Domain.WatchItems.WatchItem[]> GetAllAsync()
        {
            return _db.WatchItems.ToArrayAsync();
        }
    }
}
