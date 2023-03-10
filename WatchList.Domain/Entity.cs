using System.Text.Json;

namespace WatchList.Domain;

public class Entity
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public Guid Id { get; internal set; }

    public override string ToString() => JsonSerializer.Serialize(this, JsonSerializerOptions);
}