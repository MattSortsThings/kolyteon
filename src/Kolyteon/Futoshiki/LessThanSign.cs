using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Kolyteon.Common;
using Kolyteon.Futoshiki.Internals;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Represents a less than ( &lt; ) sign between two adjacent squares in a problem grid.
/// </summary>
[Serializable]
public sealed partial record LessThanSign : IComparable<LessThanSign>
{
    private static readonly Regex LessThanSignRegex = GeneratedLessThanSignRegex();

    [JsonConstructor]
    internal LessThanSign(Square firstSquare, Square secondSquare)
    {
        FirstSquare = firstSquare;
        SecondSquare = secondSquare;
    }

    /// <summary>
    ///     Gets the left/top square of the adjacent pair between which the sign is located.
    /// </summary>
    public Square FirstSquare { get; }

    /// <summary>
    ///     Gets the right/bottom square of the adjacent pair between which the sign is located.
    /// </summary>
    public Square SecondSquare { get; }

    /// <summary>
    ///     Compares this <see cref="LessThanSign" /> instance with another instance of the same type and returns an integer
    ///     that indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the
    ///     other instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="LessThanSign" /> instances are compared by their <see cref="FirstSquare" /> values, then by their
    ///     <see cref="SecondSquare" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="LessThanSign" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Meaning</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>
    ///                 This instance occurs in the same position in the sort order as <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(LessThanSign? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        int firstSquareComparison = FirstSquare.CompareTo(other.FirstSquare);

        return firstSquareComparison != 0 ? firstSquareComparison : SecondSquare.CompareTo(other.SecondSquare);
    }

    /// <summary>
    ///     Indicates whether this <see cref="LessThanSign" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="LessThanSign" /> instances have equal value if their <see cref="FirstSquare" /> values are equal and
    ///     their <see cref="SecondSquare" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="LessThanSign" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(LessThanSign? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return FirstSquare.Equals(other.FirstSquare) && SecondSquare.Equals(other.SecondSquare);
    }

    /// <summary>
    ///     Deconstructs this <see cref="LessThanSign" /> instance.
    /// </summary>
    /// <param name="firstSquare">The left/top square of the adjacent pair between which the sign is located.</param>
    /// <param name="secondSquare">The right/bottom square of the adjacent pair between which the sign is located.</param>
    public void Deconstruct(out Square firstSquare, out Square secondSquare)
    {
        firstSquare = FirstSquare;
        secondSquare = SecondSquare;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="LessThanSign" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(FirstSquare, SecondSquare);

    /// <summary>
    ///     Returns the string representation of this <see cref="LessThanSign" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{FirstSquare}&lt;{SecondSquare}"</c>.</returns>
    public override string ToString() => $"{FirstSquare}<{SecondSquare}";

    /// <summary>
    ///     Creates and returns a new <see cref="LessThanSign" /> instance representing a less than ( &lt; ) sign between the
    ///     two specified adjacent squares.
    /// </summary>
    /// <remarks>
    ///     The two adjacent squares must be different. The arguments can be passed in either order. The method sorts the
    ///     arguments so that the new instance's <see cref="FirstSquare" /> value always precedes its
    ///     <see cref="SecondSquare" /> value.
    /// </remarks>
    /// <param name="squareA">One of the two adjacent squares.</param>
    /// <param name="squareB">The other of the two adjacent squares.</param>
    /// <returns>A new <see cref="LessThanSign" /> instance.</returns>
    /// <exception cref="ArgumentException"><paramref name="squareA" /> is not adjacent to <paramref name="squareB" />.</exception>
    public static LessThanSign Between(Square squareA, Square squareB)
    {
        if (!squareA.AdjacentTo(squareB))
        {
            throw new ArgumentException("Squares must be adjacent to each other.");
        }

        return squareA.CompareTo(squareB) < 0 ? new LessThanSign(squareA, squareB) : new LessThanSign(squareB, squareA);
    }

    /// <summary>
    ///     Converts the string representation of a less than sign to its <see cref="LessThanSign" /> equivalent.
    /// </summary>
    /// <param name="value">A string in the format <c>"({Column1},{Row1})&lt;({Column2},{Row2})"</c>, to be parsed.</param>
    /// <returns>A new <see cref="LessThanSign" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException">
    ///     A valid <see cref="LessThanSign" /> instance could not be parsed from the <paramref name="value" /> parameter.
    /// </exception>
    public static LessThanSign Parse(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        try
        {
            return TryParse(value);
        }
        catch (ArgumentException)
        {
            throw new FormatException($"String '{value}' was not recognized as a valid LessThanSign.");
        }
    }

    private static LessThanSign TryParse(string value)
    {
        Match match = LessThanSignRegex.Match(value);

        return match.Success
            ? match.ToLessThanSign()
            : throw new FormatException($"String '{value}' was not recognized as a valid LessThanSign.");
    }

    [GeneratedRegex(@"^\((?<column1>[0-9]+),(?<row1>[0-9]+)\)\<\((?<column2>[0-9]+),(?<row2>[0-9]+)\)$",
        RegexOptions.Compiled,
        500)]
    private static partial Regex GeneratedLessThanSignRegex();
}
