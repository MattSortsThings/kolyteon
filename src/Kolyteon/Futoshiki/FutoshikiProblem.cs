using System.Text.Json.Serialization;
using Kolyteon.Common;
using Kolyteon.Futoshiki.Internals;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Represents a valid (but not necessarily solvable) Futoshiki problem.
/// </summary>
[Serializable]
public sealed record FutoshikiProblem
{
    internal const int MinGridSideLength = 4;
    internal const int MaxGridSideLength = 9;
    internal const int MinNumber = 1;

    [JsonConstructor]
    internal FutoshikiProblem(Block grid,
        IReadOnlyList<NumberedSquare> filledSquares,
        IReadOnlyList<GreaterThanSign> greaterThanSigns,
        IReadOnlyList<LessThanSign> lessThanSigns)
    {
        Grid = grid;
        FilledSquares = filledSquares;
        GreaterThanSigns = greaterThanSigns;
        LessThanSigns = lessThanSigns;
    }

    /// <summary>
    ///     Gets the maximum number value for the problem.
    /// </summary>
    [JsonIgnore]
    internal int MaxNumber => Grid.Dimensions.WidthInSquares;

    /// <summary>
    ///     Gets a <see cref="Block" /> value representing the problem grid.
    /// </summary>
    public Block Grid { get; }

    /// <summary>
    ///     Gets an immutable list of <see cref="NumberedSquare" /> values representing the filled squares in the problem grid.
    /// </summary>
    public IReadOnlyList<NumberedSquare> FilledSquares { get; }

    /// <summary>
    ///     Gets an immutable list of <see cref="GreaterThanSign" /> values representing all the greater than ( &gt; ) signs in
    ///     the problem grid.
    /// </summary>
    public IReadOnlyList<GreaterThanSign> GreaterThanSigns { get; }

    /// <summary>
    ///     Gets an immutable list of <see cref="LessThanSign" /> values representing all the less than ( &lt; ) signs in the
    ///     problem grid.
    /// </summary>
    public IReadOnlyList<LessThanSign> LessThanSigns { get; }

    /// <summary>
    ///     Indicates whether this <see cref="FutoshikiProblem" /> instance has equal value to another instance of the same
    ///     type, that is, they represent logically identical problems.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="FutoshikiProblem" /> instances have equal value if their <see cref="Grid" /> values are equal, and
    ///     their <see cref="FilledSquares" /> lists contain the same values, and their <see cref="LessThanSigns" /> lists
    ///     contain the same values, and their <see cref="FilledSquares" /> lists contain the same values.
    /// </remarks>
    /// <param name="other">The <see cref="FutoshikiProblem" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(FutoshikiProblem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Grid.Equals(other.Grid)
               && FilledSquares.Count == other.FilledSquares.Count
               && GreaterThanSigns.Count == other.GreaterThanSigns.Count
               && LessThanSigns.Count == other.LessThanSigns.Count
               && FilledSquares.OrderBy(square => square).SequenceEqual(other.FilledSquares.OrderBy(square => square))
               && GreaterThanSigns.OrderBy(sign => sign).SequenceEqual(other.GreaterThanSigns.OrderBy(sign => sign))
               && LessThanSigns.OrderBy(sign => sign).SequenceEqual(other.LessThanSigns.OrderBy(sign => sign));
    }

    /// <summary>
    ///     Deconstructs this <see cref="FutoshikiProblem" /> instance.
    /// </summary>
    /// <param name="grid">The problem grid.</param>
    /// <param name="filledSquares">The filled squares in the problem grid.</param>
    /// <param name="greaterThanSigns">The greater than signs in the problem grid.</param>
    /// <param name="lessThanSigns">The less than signs in the problem grid.</param>
    public void Deconstruct(out Block grid,
        out IReadOnlyList<NumberedSquare> filledSquares,
        out IReadOnlyList<GreaterThanSign> greaterThanSigns,
        out IReadOnlyList<LessThanSign> lessThanSigns)
    {
        grid = Grid;
        filledSquares = FilledSquares;
        greaterThanSigns = GreaterThanSigns;
        lessThanSigns = LessThanSigns;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="FutoshikiProblem" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Grid, FilledSquares, GreaterThanSigns, LessThanSigns);

    /// <summary>
    ///     Starts the process of building a new <see cref="FutoshikiProblem" /> using the fluent builder API.
    /// </summary>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static IFutoshikiProblemBuilder Create() => new FutoshikiProblemBuilder();
}
