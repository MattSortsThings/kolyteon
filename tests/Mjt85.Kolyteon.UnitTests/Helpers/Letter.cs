namespace Mjt85.Kolyteon.UnitTests.Helpers;

/// <summary>
///     Represents an alphabetical character.
/// </summary>
public readonly record struct Letter : IComparable<Letter>
{
    public static readonly Letter A = new('A');
    public static readonly Letter B = new('B');
    public static readonly Letter C = new('C');
    public static readonly Letter D = new('D');
    public static readonly Letter E = new('E');

    private Letter(char v)
    {
        Value = v;
    }

    public char Value { get; }

    public int CompareTo(Letter other) => Value.CompareTo(other.Value);

    public bool Equals(Letter other) => Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
