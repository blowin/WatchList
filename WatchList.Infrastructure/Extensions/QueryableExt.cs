using WatchList.Domain;
using X.PagedList;

namespace WatchList.Infrastructure.Extensions;

public static class QueryableExt
{
    public static Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> self, Page page, CancellationToken cancellationToken)
    {
        return self.ToPagedListAsync(page.PageNumber, page.PageSize, cancellationToken);
    }
}