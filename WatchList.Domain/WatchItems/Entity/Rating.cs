namespace WatchList.Domain.WatchItems.Entity;

public readonly struct Rating : IEquatable<Rating>
{
    private readonly float _value;

    public Rating(float value) => _value = value;

    public static implicit operator float(Rating rating) => rating._value;
    public bool Equals(Rating other) => _value.Equals(other._value);

    public override bool Equals(object? obj) => obj is Rating other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => _value.ToString("0.0");
}