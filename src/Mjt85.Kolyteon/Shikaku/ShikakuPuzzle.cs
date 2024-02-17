using System.Text.Json.Serialization;
using Mjt85.Kolyteon.Shikaku.Internals;

namespace Mjt85.Kolyteon.Shikaku;

/// <summary>
///     Represents a Shikaku Puzzle.
/// </summary>
/// <remarks>
///     <para>
///         A Shikaku puzzle comprises a square grid of cells (no smaller than 5x5 in size). Some of the cells contain
///         numeric hints. The minimum permitted number for any hint is 2, and the hint numbers must sum to the grid area.
///     </para>
///     <para>
///         To solve the puzzle, one must divide the entire grid into rectangles so that every rectangle encloses exactly
///         one hint, whose number is equal to the rectangle's area in cells.
///     </para>
///     <para>
///         A <see cref="ShikakuPuzzle" /> instance is an immutable data structure exposing two read-only
///         <see cref="GridSideLength" /> and <see cref="Hints" /> properties. It represents a valid (but not necessarily
///         solvable) puzzle if the sum of the <see cref="Hint.Number" /> values of its <see cref="Hints" /> list items is
///         equal to the square of its <see cref="GridSideLength" /> value.
///     </para>
///     <para>
///         This type can only be instantiated outside its assembly by one of the following means:
///         <list type="bullet">
///             <item>
///                 Using the <see cref="FromGrid" /> static factory method, which throws an exception if the instantiated
///                 puzzle is invalid thereby guaranteeing a valid puzzle.
///             </item>
///             <item>Deserialization, which does not validate the instantiated puzzle.</item>
///         </list>
///     </para>
/// </remarks>
[Serializable]
public sealed record ShikakuPuzzle
{
    /// <summary>
    ///     Initializes a new <see cref="ShikakuPuzzle" /> instance with the specified <see cref="GridSideLength" /> value and
    ///     <see cref="Hints" /> lists.
    /// </summary>
    /// <remarks>
    ///     This internal constructor is for deserialization and testing only. Use the <see cref="FromGrid" /> static factory
    ///     method to instantiate this type outside its assembly.
    /// </remarks>
    /// <param name="gridSideLength">The puzzle grid's height in cells and width in cells.</param>
    /// <param name="hints">The puzzle's hints.</param>
    /// <exception cref="ArgumentNullException"><paramref name="hints" /> is <c>null</c>.</exception>
    [JsonConstructor]
    internal ShikakuPuzzle(int gridSideLength, IReadOnlyList<Hint> hints)
    {
        GridSideLength = gridSideLength;
        Hints = hints ?? throw new ArgumentNullException(nameof(hints));
    }

    /// <summary>
    ///     Gets the puzzle grid's height in cells and width in cells.
    /// </summary>
    /// <value>A 32-bit signed integer greater than or equal to 5. The puzzle grid's height in cells and width in cells.</value>
    public int GridSideLength { get; init; }

    /// <summary>
    ///     Gets the puzzle's hints.
    /// </summary>
    /// <remarks>The contents of this list may be in any order.</remarks>
    /// <value>A read-only list of <see cref="Hint" /> instances. The puzzle's hints.</value>
    public IReadOnlyList<Hint> Hints { get; init; } = Array.Empty<Hint>();

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="ShikakuPuzzle" /> instance have equal value,
    ///     that is, they represent logically identical Shikaku puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="ShikakuPuzzle" /> instances are equal if their respective <see cref="Hints" /> lists contain
    ///     logically equivalent items (irrespective of order).
    /// </remarks>
    /// <param name="other">The <see cref="ShikakuPuzzle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise,
    ///     <c>false</c>. If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    public bool Equals(ShikakuPuzzle? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Hints.Count == other.Hints.Count && Hints.OrderBy(h => h).SequenceEqual(other.Hints.OrderBy(h => h));
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Hints.GetHashCode();

    /// <summary>
    ///     Creates and returns a new <see cref="ShikakuPuzzle" /> from the specified grid.
    /// </summary>
    /// <remarks>
    ///     Static factory method. Any <see cref="ShikakuPuzzle" /> instance returned by this method is guaranteed to represent
    ///     a valid (but not necessarily solvable) Shikaku puzzle.
    /// </remarks>
    /// <param name="grid">
    ///     A 2-dimensional square grid no smaller than 5x5 in size, in which every non-null value is a hint.
    ///     The grid to be converted.
    /// </param>
    /// <returns>A new <see cref="ShikakuPuzzle" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><see cref="grid" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    ///     <see cref="grid" /> has a rank 0 length less than 5, or has a rank 1 length not equal to its rank 0 length, or
    ///     contains a non-null value less than 2.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     The instantiated <see cref="ShikakuPuzzle" /> does not represent a valid Shikaku puzzle.
    /// </exception>
    public static ShikakuPuzzle FromGrid(int?[,] grid)
    {
        _ = grid ?? throw new ArgumentNullException(nameof(grid));

        Guard.AgainstInvalidGridDimensions(grid);
        ShikakuPuzzle puzzle = Create(grid);
        Guard.AgainstInvalidPuzzle(puzzle);

        return puzzle;
    }

    private static ShikakuPuzzle Create(int?[,] squareGrid)
    {
        var gridSideLength = squareGrid.GetLength(0);
        List<Hint> hints = new(gridSideLength * 3);

        for (var column = 0; column < gridSideLength; column++)
        {
            for (var row = 0; row < gridSideLength; row++)
            {
                var cellContent = squareGrid[row, column];
                switch (cellContent)
                {
                    case null:
                        continue;
                    case < 2:
                        throw new ArgumentException($"Grid has illegal hint number at index [{row},{column}].");
                    default:
                        hints.Add(new Hint(column, row, cellContent.Value));

                        break;
                }
            }
        }

        return new ShikakuPuzzle(gridSideLength, hints.ToArray());
    }
}
