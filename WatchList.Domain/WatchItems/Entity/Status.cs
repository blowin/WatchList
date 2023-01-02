using Ardalis.SmartEnum;

namespace WatchList.Domain.WatchItems.Entity;

public sealed class Status : SmartEnum<Status>
{
    public static readonly Status Unknown = new("Unknown", 0);
    public static readonly Status Watching = new("Watching", 1);
    public static readonly Status Watched = new("Watched", 2);
    public static readonly Status PlanToWatch = new("PlanToWatch", 3);
    
    private Status(string name, int value) : base(name, value)
    {
    }
}