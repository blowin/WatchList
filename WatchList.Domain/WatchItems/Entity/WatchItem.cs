namespace WatchList.Domain.WatchItems.Entity;

public class WatchItem : Domain.Entity
{
    public string Title { get; internal set; }
    public string? Description { get; internal set; }
    public HashSet<string> Genre { get; internal set; }
    public Rating? Rating { get; internal set; }
    public DateOnly? ReleaseDate { get; internal set; }
    public Status Status { get; internal set; }
    public ImageBase64? Image { get; internal set; }
    public WatchItemType Type { get; internal set; }

    public WatchItem(string title, string? description, HashSet<string> genre, Rating? rating, DateOnly releaseDate, 
        Status status, ImageBase64 image, WatchItemType type)
    {
        Title = title;
        Description = description;
        Genre = genre;
        Rating = rating;
        ReleaseDate = releaseDate;
        Status = status;
        Image = image;
        Type = type;
    }
    
    // EF core
#pragma warning disable CS8618
    public WatchItem(){}
#pragma warning restore CS8618
}