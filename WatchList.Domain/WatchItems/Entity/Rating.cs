namespace WatchList.Domain.WatchItems.Entity;

public readonly struct Rating : IEquatable<Rating>, IComparable<Rating>, IComparable
{
    private readonly float _value;

    public Rating(float value) => _value = value;

    public static bool operator <(Rating lft, Rating rgt) => lft.CompareTo(rgt) < 0;
    public static bool operator >(Rating lft, Rating rgt) => lft.CompareTo(rgt) > 0;
    public static bool operator !=(Rating lft, Rating rgt) => !(lft == rgt);
    public static bool operator ==(Rating lft, Rating rgt) => lft.Equals(rgt);
    public static implicit operator float(Rating rating) => rating._value;
    public bool Equals(Rating other) => _value.Equals(other._value);
    public int CompareTo(Rating other) => _value.CompareTo(other._value);
    public int CompareTo(object? obj) => obj is Rating r ? CompareTo(r) : 1;
    public override bool Equals(object? obj) => obj is Rating other && Equals(other);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString("0.0");
}