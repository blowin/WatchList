using WatchList.Domain.WatchItems.Entity;

namespace WatchList.Domain.WatchItems.Repository;

public sealed class BriefWatchItemDetail
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Status Status { get; set; }
    public WatchItemType Type { get; set; }
    public Rating? Rating { get; set; }
    public DateOnly? ReleaseDate { get; set; }

    public BriefWatchItemDetail(Guid id, string title, Status status, WatchItemType type, Rating? rating, DateOnly? releaseDate)
    {
        Id = id;
        Title = title;
        Status = status;
        Type = type;
        Rating = rating;
        ReleaseDate = releaseDate;
    }
}