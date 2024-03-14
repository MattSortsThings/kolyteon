using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.Shikaku;

/// <summary>
///     Represents a rectangle that has been drawn onto a Shikaku puzzle grid.
/// </summary>
/// <remarks>
///     <para>
///         A rectangle is defined by its <see cref="WidthInCells" />, <see cref="HeightInCells" />, and its upper-left
///         cell represented by its <see cref="OriginColumn" /> and <see cref="OriginRow" /> properties.
///     </para>
///     <para>Puzzle grid columns are zero-indexed from left to right. Rows are zero-indexed from top to bottom.</para>
/// </remarks>
[Serializable]
public readonly record struct Rectangle : IComparable<Rectangle>
{
    /// <summary>
    ///     Initializes a new <see cref="Rectangle" /> instance with default <see cref="OriginColumn" /> and
    ///     <see cref="OriginRow" /> values of 0 and minimum <see cref="WidthInCells" /> and <see cref="HeightInCells" />
    ///     values of 1.
    /// </summary>
    public Rectangle() : this(0, 0, 1, 1)
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="Rectangle" /> instance with the specified location and dimensions.
    /// </summary>
    /// <param name="originColumn">The zero-based column index of the rectangle's upper-left cell.</param>
    /// <param name="originRow">The zero-based row index of the rectangle's upper-left cell.</param>
    /// <param name="widthInCells">An integer greater than or equal to 1. The rectangle's width in cells.</param>
    /// <param name="heightInCells">An integer greater than or equal to 1. The rectangle's height in cells.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="originColumn" /> is negative; or, <paramref name="originRow" /> is negative; or,
    ///     <paramref name="widthInCells" /> is less than 1; or, <paramref name="heightInCells" /> is less than 1.
    /// </exception>
    [JsonConstructor]
    public Rectangle(int originColumn, int originRow, int widthInCells, int heightInCells)
    {
        OriginColumn = originColumn >= 0
            ? originColumn
            : throw new ArgumentOutOfRangeException(nameof(originColumn), originColumn, "Value must not be negative.");

        OriginRow = originRow >= 0
            ? originRow
            : throw new ArgumentOutOfRangeException(nameof(originRow), originRow, "Value must not be negative.");

        WidthInCells = widthInCells >= 1
            ? widthInCells
            : throw new ArgumentOutOfRangeException(nameof(widthInCells), widthInCells, "Value must not be less than 1.");

        HeightInCells = heightInCells >= 1
            ? heightInCells
            : throw new ArgumentOutOfRangeException(nameof(heightInCells), heightInCells, "Value must not be less than 1.");
    }

    /// <summary>
    ///     Gets the zero-based column index of the rectangle's upper-left cell.
    /// </summary>
    /// <value>A non-negative 32-bit signed integer. The zero-based column index of the rectangle's upper-left cell.</value>
    public int OriginColumn { get; }

    /// <summary>
    ///     Gets the zero-based row index of the rectangle's upper-left cell.
    /// </summary>
    /// <value>A non-negative 32-bit signed integer. The zero-based row index of the rectangle's upper-left cell.</value>
    public int OriginRow { get; }

    /// <summary>
    ///     Gets the rectangle's width in cells.
    /// </summary>
    /// <value>A 32-bit signed integer greater than or equal to 1. The rectangle's width in cells.</value>
    public int WidthInCells { get; }

    /// <summary>
    ///     Gets the rectangle's height in cells.
    /// </summary>
    /// <value>A 32-bit signed integer greater than or equal to 1. The rectangle's height in cells.</value>
    public int HeightInCells { get; }

    /// <summary>
    ///     Gets the rectangle's area in cells.
    /// </summary>
    /// <remarks>The value of this property is calculated every time it is accessed.</remarks>
    /// <value>A 32-bit signed integer greater than or equal to 1. The rectangle's area in cells.</value>
    [JsonIgnore]
    public int AreaInCells => WidthInCells * HeightInCells;

    /// <summary>
    ///     Compares this instance against the specified <see cref="Rectangle" /> instance and returns a value indicating
    ///     whether
    ///     this instance precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     <see cref="Rectangle" /> instances are sorted in ascending order by <see cref="OriginColumn" /> value, then by
    ///     <see cref="OriginRow" /> value, then by <see cref="WidthInCells" /> value, then by <see cref="HeightInCells" />
    ///     value.
    /// </remarks>
    /// <param name="other">The <see cref="Rectangle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in
    ///     the sort order as the <paramref name="other" /> parameter.
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Condition</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes the <paramref name="other" />.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>
    ///                 This instance has the same position in the sort order as the <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows the <paramref name="other" />.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(Rectangle other)
    {
        var originColumnComparison = OriginColumn.CompareTo(other.OriginColumn);

        if (originColumnComparison != 0)
        {
            return originColumnComparison;
        }

        var originRowComparison = OriginRow.CompareTo(other.OriginRow);

        if (originRowComparison != 0)
        {
            return originRowComparison;
        }

        var widthInCellsComparison = WidthInCells.CompareTo(other.WidthInCells);

        return widthInCellsComparison != 0 ? widthInCellsComparison : HeightInCells.CompareTo(other.HeightInCells);
    }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="Rectangle" /> instance have equal value, that is,
    ///     they represent two rectangles with identical dimensions drawn in the same location in the puzzle grid.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Rectangle" /> instances have equal value if:
    ///     <list type="bullet">
    ///         <item>their <see cref="OriginColumn" /> values are equal, <i>and</i></item>
    ///         <item>their <see cref="OriginRow" /> values are equal, <i>and</i></item>
    ///         <item>their <see cref="WidthInCells" /> values are equal, <i>and</i></item>
    ///         <item>their <see cref="HeightInCells" /> values are equal.</item>
    ///     </list>
    /// </remarks>
    /// <param name="other">The <see cref="Rectangle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Rectangle other) => OriginColumn == other.OriginColumn
                                           && OriginRow == other.OriginRow
                                           && WidthInCells == other.WidthInCells
                                           && HeightInCells == other.HeightInCells;

    /// <summary>
    ///     Determines whether the rectangles represented by this instance and the specified <see cref="Rectangle" /> instance
    ///     overlap at any point.
    /// </summary>
    /// <remarks>Two rectangles overlap if there exists at least one cell that is located inside both rectangles.</remarks>
    /// <param name="other">The <see cref="Rectangle" /> against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter overlap at any point; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public bool Overlaps(in Rectangle other)
    {
        var (otherOriginColumn, otherOriginRow, otherWidthInCells, otherHeightInCells) = other;

        return OriginColumn < otherOriginColumn + otherWidthInCells
               && OriginRow < otherOriginRow + otherHeightInCells
               && otherOriginColumn < OriginColumn + WidthInCells
               && otherOriginRow < OriginRow + HeightInCells;
    }

    /// <summary>
    ///     Determines whether the rectangle represented by this instance encloses the hint represented by the specified
    ///     <see cref="Hint" /> instance.
    /// </summary>
    /// <remarks>A rectangle encloses a hint if the hint's cell is located inside the rectangle.</remarks>
    /// <param name="hint">The <see cref="Hint" /> instance against which this instance is to be compared.</param>
    /// <returns><c>true</c> if this instance encloses the <paramref name="hint" /> parameter; otherwise, <c>false</c>.</returns>
    public bool Encloses(in Hint hint)
    {
        var (hintColumn, hintRow, _) = hint;

        return hintColumn >= OriginColumn
               && hintRow >= OriginRow
               && hintColumn < OriginColumn + WidthInCells
               && hintRow < OriginRow + HeightInCells;
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(OriginColumn, OriginRow, WidthInCells, HeightInCells);

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="originColumn">The zero-based column index of the rectangle's upper-left cell.</param>
    /// <param name="originRow">The zero-based row index of the rectangle's upper-left cell.</param>
    /// <param name="widthInCells">The rectangle's width in cells.</param>
    /// <param name="heightInCells">The rectangle's height in cells.</param>
    public void Deconstruct(out int originColumn, out int originRow, out int widthInCells, out int heightInCells)
    {
        originColumn = OriginColumn;
        originRow = OriginRow;
        widthInCells = WidthInCells;
        heightInCells = HeightInCells;
    }

    /// <summary>
    ///     Creates and returns the string representation of this instance.
    /// </summary>
    /// <remarks>
    ///     A <see cref="Rectangle" /> instance is represented by a string in the format
    ///     <c>"({OriginColumn},{OriginRow}) [{WidthInCells}x{HeightInCells}]"</c>.
    /// </remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     Rectangle r0 = new(0, 0, 2, 3);
    ///     Rectangle r1 = new(10, 3, 7, 1);
    /// 
    ///     Console.WriteLine(r0);
    ///     Console.WriteLine(r1);
    ///   }
    /// }
    /// // This example produces the following console output:
    /// // (0,0) [2x3]
    /// // (10,3) [7x1]
    /// </code>
    /// </example>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString() => $"({OriginColumn},{OriginRow}) [{WidthInCells}x{HeightInCells}]";
}
