namespace Mjt85.Kolyteon.UnitTests.Helpers;

/// <summary>
///     Represents a numeric digit.
/// </summary>
public readonly record struct Digit : IComparable<Digit>
{
    public static readonly Digit One = new(1);
    public static readonly Digit Two = new(2);
    public static readonly Digit Three = new(3);
    public static readonly Digit Four = new(4);
    public static readonly Digit Five = new(5);

    private Digit(int v)
    {
        Value = v;
    }

    public int Value { get; }

    public int CompareTo(Digit other) => Value.CompareTo(other.Value);

    public bool Equals(Digit other) => Value == other.Value;

    public override int GetHashCode() => Value;

    public override string ToString() => Value.ToString();
}
