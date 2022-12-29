using Ardalis.SmartEnum;

namespace WatchList.Domain.WatchItems;

public sealed class WatchItemType : SmartEnum<WatchItemType>
{
    public static readonly WatchItemType Unknown = new("Unknown", 0);
    public static readonly WatchItemType Film = new("Film", 1);
    public static readonly WatchItemType Serial = new("Serial", 2);
    public static readonly WatchItemType Anime = new("Anime", 3);
    public static readonly WatchItemType Carton = new("Carton", 4);

    private WatchItemType(string name, int value) : base(name, value)
    {
    }
}