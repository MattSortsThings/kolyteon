using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Kolyteon.Common.Internals;

namespace Kolyteon.Common;

/// <summary>
///     Represents a specific square, located on a 2-dimensional axis of squares, that is filled with an integer number.
/// </summary>
[Serializable]
public readonly partial record struct NumberedSquare : IComparable<NumberedSquare>
{
    private static readonly Regex NumberedSquareRegex = GeneratedNumberedSquareRegex();

    /// <summary>
    ///     Initializes a new <see cref="NumberedSquare" /> instance with its <see cref="NumberedSquare.Square" /> value having
    ///     <see cref="Square.Column" /> and <see cref="Square.Row" /> values of 0 and a <see cref="Number" /> value of 0.
    /// </summary>
    public NumberedSquare() : this(new Square(), 0) { }

    [JsonConstructor]
    internal NumberedSquare(Square square, int number)
    {
        Square = square;
        Number = number;
    }

    /// <summary>
    ///     Gets the square.
    /// </summary>
    public Square Square { get; }

    /// <summary>
    ///     Gets the integer number that fills the square.
    /// </summary>
    public int Number { get; }

    /// <summary>
    ///     Compares this <see cref="NumberedSquare" /> instance with another instance of the same type and returns an integer
    ///     that indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the
    ///     other instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NumberedSquare" /> instances are compared by their <see cref="NumberedSquare.Square" /> values, then
    ///     by their <see cref="Number" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="NumberedSquare" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(NumberedSquare other)
    {
        int squareComparison = Square.CompareTo(other.Square);

        return squareComparison != 0 ? squareComparison : Number.CompareTo(other.Number);
    }

    /// <summary>
    ///     Indicates whether this <see cref="NumberedSquare" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NumberedSquare" /> instances have equal value if their <see cref="NumberedSquare.Square" /> values
    ///     are equal and their <see cref="Number" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="NumberedSquare" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(NumberedSquare other) => Square.Equals(other.Square) && Number == other.Number;

    /// <summary>
    ///     Deconstructs this <see cref="NumberedSquare" /> instance.
    /// </summary>
    /// <param name="square">The square.</param>
    /// <param name="number">The integer number that fills the square.</param>
    public void Deconstruct(out Square square, out int number)
    {
        square = Square;
        number = Number;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="NumberedSquare" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Square, Number);

    /// <summary>
    ///     Returns the string representation of this <see cref="NumberedSquare" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{Square} [{Number}]"</c>.</returns>
    public override string ToString() => $"{Square} [{Number}]";

    /// <summary>
    ///     Converts the string representation of a numbered square to its <see cref="NumberedSquare" /> equivalent.
    /// </summary>
    /// <param name="value">A string in the format <c>"({Column},{Row}) [{Number}]"</c>, to be parsed.</param>
    /// <returns>A new <see cref="NumberedSquare" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException">
    ///     A valid <see cref="NumberedSquare" /> instance could not be parsed from the <see cref="value" /> parameter.
    /// </exception>
    public static NumberedSquare Parse(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        try
        {
            return TryParse(value);
        }
        catch (ArgumentException)
        {
            throw new FormatException($"String '{value}' was not recognized as a valid NumberedSquare.");
        }
    }

    private static NumberedSquare TryParse(string value)
    {
        Match match = NumberedSquareRegex.Match(value);

        return match.Success
            ? match.ToNumberedSquare()
            : throw new FormatException($"String '{value}' was not recognized as a valid NumberedSquare.");
    }

    [GeneratedRegex(@"^\((?<column>[0-9]+),(?<row>[0-9]+)\) \[(?<number>[0-9]+)\]$",
        RegexOptions.Compiled,
        500)]
    private static partial Regex GeneratedNumberedSquareRegex();
}
