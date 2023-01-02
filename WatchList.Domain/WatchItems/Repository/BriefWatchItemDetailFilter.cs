using WatchList.Domain.WatchItems.Entity;

namespace WatchList.Domain.WatchItems.Repository;

public sealed class BriefWatchItemDetailFilter
{
    public Page Page { get; } = new Page();
    public string? Title { get; set; }
    public Status Status { get; set; } = Status.Unknown;
    public WatchItemType Type { get; set; } = WatchItemType.Unknown;
    public DateTime? ReleaseDateFrom { get; set; }
    public DateTime? ReleaseDateTo { get; set; }

    public IQueryable<WatchItem> ApplyFilter(
        IQueryable<WatchItem> queryable)
    {
        if (!string.IsNullOrEmpty(Title))
            queryable = queryable.Where(e => e.Title.Contains(Title));
        
        if (Status != Status.Unknown)
            queryable = queryable.Where(e => e.Status == Status);
        
        if (Type != WatchItemType.Unknown)
            queryable = queryable.Where(e => e.Type == Type);

        if (ReleaseDateFrom != null)
        {
            var releaseDateFrom = DateOnly.FromDateTime(ReleaseDateFrom.Value);
            queryable = queryable.Where(e => e.ReleaseDate >= releaseDateFrom);
        }
        
        if (ReleaseDateTo != null)
        {
            var releaseDateTo = DateOnly.FromDateTime(ReleaseDateTo.Value);
            queryable = queryable.Where(e => e.ReleaseDate <= releaseDateTo);
        }
        
        return queryable;
    }
}