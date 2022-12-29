namespace WatchList.Domain.WatchItems;

public class WatchItem : Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public HashSet<string> Genre { get; private set; }
    public float? Rating { get; private set; }
    public DateOnly? ReleaseDate { get; private set; }
    public Status Status { get; private set; }
    public ImageBase64? Image { get; private set; }
    public WatchItemType Type { get; private set; }

    public WatchItem(string title, string? description, HashSet<string> genre, float rating, DateOnly releaseDate, 
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
    private WatchItem(){}
#pragma warning restore CS8618
}