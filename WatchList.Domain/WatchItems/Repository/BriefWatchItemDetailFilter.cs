using WatchList.Domain.WatchItems.Entity;

namespace WatchList.Domain.WatchItems.Repository;

public sealed class BriefWatchItemDetailFilter
{
    public Page Page { get; } = new Page();
    public string? Title { get; set; }
    public Status Status { get; set; } = Status.Unknown;
    public WatchItemType Type { get; set; } = WatchItemType.Unknown;

    public IQueryable<WatchItem> ApplyFilter(
        IQueryable<WatchItem> queryable)
    {
        if (!string.IsNullOrEmpty(Title))
            queryable = queryable.Where(e => e.Title.Contains(Title));
        if (Status != Status.Unknown)
            queryable = queryable.Where(e => e.Status == Status);
        if (Type != WatchItemType.Unknown)
            queryable = queryable.Where(e => e.Type == Type);
        return queryable;
    }
}