using WatchList.Domain.WatchItems.Entity;

namespace WatchList.Domain.WatchItems.Services.WatchItemUpdateService_;

public class WatchItemUpdateRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HashSet<string> Genre { get; set; } = new HashSet<string>();
    public Rating? Rating { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public Status Status { get; set; } = Status.Unknown;
    public WatchItemType Type { get; set; } = WatchItemType.Unknown;

    public WatchItemUpdateRequest(WatchItem entity)
    {
        Id = entity.Id;
        Title = entity.Title;
        Description = entity.Description;
        if (entity.Genre is { Count: > 0 })
        {
            foreach (var genre in entity.Genre)
                Genre.Add(genre);
        }
        Rating = entity.Rating;
        ReleaseDate = entity.ReleaseDate?.ToDateTime(new TimeOnly());
        Status = entity.Status;
        Type = entity.Type;
    }
    
    public void Apply(WatchItem item)
    {
        item.Title = Title;
        item.Description = Description;
        item.Genre.Clear();
        foreach (var genre in Genre)
            item.Genre.Add(genre);
        item.Rating = Rating;
        item.ReleaseDate = ReleaseDate == null ? null : DateOnly.FromDateTime(ReleaseDate.Value);
        item.Status = Status;
        item.Type = Type;
    }
}