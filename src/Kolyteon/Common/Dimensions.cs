using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Kolyteon.Common.Internals;

namespace Kolyteon.Common;

/// <summary>
///     Represents the dimensions of a rectangular block of squares.
/// </summary>
[Serializable]
public readonly partial record struct Dimensions : IComparable<Dimensions>
{
    private const int MinSideLengthInSquares = 1;
    private static readonly Regex DimensionsRegex = GeneratedDimensionsRegex();

    /// <summary>
    ///     Initializes a new <see cref="Dimensions" /> instance with the minimum <see cref="WidthInSquares" /> value of 1 and
    ///     the minimum <see cref="HeightInSquares" /> value of 1.
    /// </summary>
    public Dimensions() : this(MinSideLengthInSquares, MinSideLengthInSquares) { }

    [JsonConstructor]
    internal Dimensions(int widthInSquares, int heightInSquares)
    {
        WidthInSquares = widthInSquares;
        HeightInSquares = heightInSquares;
    }

    /// <summary>
    ///     Gets the width in squares of the dimensions.
    /// </summary>
    public int WidthInSquares { get; }

    /// <summary>
    ///     Gets the height in squares of the dimensions.
    /// </summary>
    public int HeightInSquares { get; }

    /// <summary>
    ///     Compares this <see cref="Dimensions" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Dimensions" /> instances are compared by their <see cref="WidthInSquares" /> values, then by their
    ///     <see cref="HeightInSquares" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="Dimensions" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Dimensions other)
    {
        int widthInSquaresComparison = WidthInSquares.CompareTo(other.WidthInSquares);

        return widthInSquaresComparison != 0 ? widthInSquaresComparison : HeightInSquares.CompareTo(other.HeightInSquares);
    }

    /// <summary>
    ///     Indicates whether this <see cref="Dimensions" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Dimensions" /> instances have equal value if their <see cref="WidthInSquares" /> values are equal
    ///     and their <see cref="HeightInSquares" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Dimensions" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Dimensions other) => WidthInSquares == other.WidthInSquares && HeightInSquares == other.HeightInSquares;

    /// <summary>
    ///     Creates and returns a new <see cref="Block" /> instance. with the default <see cref="Block.OriginSquare" /> at
    ///     column 0, row 0, and this <see cref="Dimensions" /> instance as its <see cref="Block.Dimensions" />.
    /// </summary>
    /// <returns>A new <see cref="Block" /> instance.</returns>
    public Block ToBlock() => new(new Square(0, 0), this);

    /// <summary>
    ///     Deconstructs this <see cref="Dimensions" /> instance.
    /// </summary>
    /// <param name="widthInSquares">The width in squares of the dimensions.</param>
    /// <param name="heightInSquares">The height in squares of the dimensions.</param>
    public void Deconstruct(out int widthInSquares, out int heightInSquares)
    {
        widthInSquares = WidthInSquares;
        heightInSquares = HeightInSquares;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="Dimensions" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(WidthInSquares, HeightInSquares);

    /// <summary>
    ///     Returns the string representation of this <see cref="Dimensions" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{WidthInSquares}x{HeightInSquares}"</c>.</returns>
    public override string ToString() => $"{WidthInSquares}x{HeightInSquares}";

    /// <summary>
    ///     Creates and returns a new <see cref="Dimensions" /> instance with the specified <see cref="WidthInSquares" /> and
    ///     <see cref="HeightInSquares" />.
    /// </summary>
    /// <param name="widthInSquares">An integer greater than or equal to 1. The width in squares of the dimensions.</param>
    /// <param name="heightInSquares">An integer greater than or equal to 1. The height in squares of the dimensions.</param>
    /// <returns>A new <see cref="Dimensions" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="widthInSquares" /> is less than 1, or <paramref name="heightInSquares" /> is less than 1.
    /// </exception>
    public static Dimensions FromWidthAndHeight(int widthInSquares, int heightInSquares)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(widthInSquares, MinSideLengthInSquares);
        ArgumentOutOfRangeException.ThrowIfLessThan(heightInSquares, MinSideLengthInSquares);

        return new Dimensions(widthInSquares, heightInSquares);
    }

    /// <summary>
    ///     Converts the string representation of a set of dimensions to its <see cref="Dimensions" /> equivalent.
    /// </summary>
    /// <param name="value">A string in the format <c>"{WidthInSquares}x{HeightInSquares}"</c>, to be parsed.</param>
    /// <returns>A new <see cref="Dimensions" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException">
    ///     A valid <see cref="Dimensions" /> instance could not be parsed from the <paramref name="value" /> parameter.
    /// </exception>
    public static Dimensions Parse(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        try
        {
            return TryParse(value);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new FormatException($"String '{value}' was not recognized as a valid Dimensions.");
        }
    }

    private static Dimensions TryParse(string value)
    {
        Match match = DimensionsRegex.Match(value);

        return match.Success
            ? match.ToDimensions()
            : throw new FormatException($"String '{value}' was not recognized as a valid Dimensions.");
    }

    [GeneratedRegex(@"^(?<width>[0-9]+)x(?<height>[0-9]+)$",
        RegexOptions.Compiled,
        500)]
    private static partial Regex GeneratedDimensionsRegex();
}
