using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mjt85.Kolyteon.Shikaku.Internals;

namespace Mjt85.Kolyteon.Shikaku;

/// <summary>
///     Represents a Shikaku puzzle.
/// </summary>
/// <remarks>
///     <para>
///         A Shikaku puzzle comprises a square grid of cells. Some of the cells are filled with numeric hints that sum to
///         the grid's area in cells.
///     </para>
///     <para>
///         To solve the puzzle, one must sub-divide the grid into rectangles so that every rectangle encloses exactly one
///         hint, whose number is equal to the rectangle's area in cells.
///     </para>
///     <para>
///         A Shikaku puzzle is valid (but not necessarily solvable) if:
///         <list type="bullet">
///             <item>the grid is a square no smaller than 5x5 is size, <i>and</i></item>
///             <item>the hint numbers sum to the grid's area, <i>and</i></item>
///             <item>no hint number is less than 2.</item>
///         </list>
///     </para>
///     <para>
///         A <see cref="ShikakuPuzzle" /> instance is an immutable data structure representing a Shikaku puzzle. This type
///         can only be instantiated outside its assembly by:
///         <list type="bullet">
///             <item>
///                 using the <see cref="FromGrid" /> static factory method, which validates the instantiated puzzle and
///                 throws an exception if it is invalid, <i>or</i>
///             </item>
///             <item>deserialization.</item>
///         </list>
///     </para>
/// </remarks>
[Serializable]
public sealed record ShikakuPuzzle
{
    /// <summary>
    ///     Initializes a new <see cref="ShikakuPuzzle" /> instance with the specified <see cref="GridSideLength" /> value and
    ///     <see cref="Hints" /> list.
    /// </summary>
    /// <remarks>This internal constructor is for deserialization and testing only.</remarks>
    /// <param name="gridSideLength">The puzzle grid height and width in cells.</param>
    /// <param name="hints">The puzzle's hints.</param>
    /// <exception cref="ArgumentNullException"><paramref name="hints" /> is <c>null</c>.</exception>
    [JsonConstructor]
    internal ShikakuPuzzle(int gridSideLength, IReadOnlyList<Hint> hints)
    {
        GridSideLength = gridSideLength;
        Hints = hints ?? throw new ArgumentNullException(nameof(hints));
    }

    /// <summary>
    ///     Gets the puzzle grid height and width in cells.
    /// </summary>
    /// <value>A 32-bit signed integer greater than or equal to 5. The puzzle grid height and width in cells.</value>
    public int GridSideLength { get; init; }

    /// <summary>
    ///     Gets the puzzle's hints.
    /// </summary>
    /// <remarks>No assumptions should be made about the ordering of the values in this list.</remarks>
    /// <value>A read-only list of <see cref="Hint" /> instances. The puzzle's hints.</value>
    public IReadOnlyList<Hint> Hints { get; init; } = Array.Empty<Hint>();

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="ShikakuPuzzle" /> instance have equal value, that is,
    ///     they represent logically identical Shikaku puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="ShikakuPuzzle" /> instances are equal if their respective <see cref="Hints" /> lists contain the
    ///     exact same values (irrespective of order).
    /// </remarks>
    /// <param name="other">The <see cref="ShikakuPuzzle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    ///     If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
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
    ///     Determines whether the proposed solution is valid for the Shikaku puzzle represented by this instance.
    /// </summary>
    /// <remarks>
    ///     This method applies the following validation checks to the <paramref name="solution" /> parameter sequentially, and
    ///     returns the first validation error encountered (if any):
    ///     <list type="number">
    ///         <item>
    ///             The number of rectangles in the <paramref name="solution" /> list is equal to the number of hints in this
    ///             instance's <see cref="Hints" />  list.
    ///         </item>
    ///         <item>The sum of the areas of the rectangles is equal to the puzzle grid area.</item>
    ///         <item>No rectangle is partially or entirely outside the grid.</item>
    ///         <item>No pair of rectangles overlap.</item>
    ///         <item>Every rectangle encloses exactly one hint.</item>
    ///         <item>Every rectangle's area is equal to the number of the hint it encloses.</item>
    ///     </list>
    /// </remarks>
    /// <param name="solution">A list of <see cref="Rectangle" /> values. The proposed solution to the puzzle.</param>
    /// <returns>
    ///     <see cref="ValidationResult.Success" /> (i.e. <c>null</c>) if the <paramref name="solution" /> parameter is a
    ///     valid solution; otherwise, a <see cref="ValidationResult" /> instance with an error message reporting the first
    ///     validation error encountered.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="solution" /> is <c>null</c>.</exception>
    public ValidationResult? ValidSolution(IReadOnlyList<Rectangle> solution)
    {
        _ = solution ?? throw new ArgumentNullException(nameof(solution));

        return ApplyChainedValidators(solution);
    }

    /// <summary>
    ///     Creates and returns a new <see cref="ShikakuPuzzle" /> instance from the specified grid.
    /// </summary>
    /// <remarks>
    ///     Static factory method. Any <see cref="ShikakuPuzzle" /> instance returned from this method is guaranteed to
    ///     represent a valid (but not necessarily solvable) Shikaku puzzle.
    /// </remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     ShikakuPuzzle puzzle = ShikakuPuzzle.FromGrid(new int?[,]
    ///     {
    ///       { null,    4, null, null, null },
    ///       { null, null, null, null, null },
    ///       { null, null,   10, null, null },
    ///       {    6, null, null, null, null },
    ///       { null, null, null, null,    5 },
    ///     });
    ///   }
    /// }
    /// </code>
    /// </example>
    /// <param name="grid">
    ///     A 2-dimension array of nullable integers, with square dimensions and neither length less than 5, in
    ///     which every non-null value is a number no less than 2 and represents a hint in the puzzle.
    /// </param>
    /// <returns>A new <see cref="ShikakuPuzzle" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="grid" /> is <c>null</c>.</exception>
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

    private ValidationResult? ApplyChainedValidators(IReadOnlyList<Rectangle> solution) => SolutionHasCorrectSize(solution);

    private ValidationResult? SolutionHasCorrectSize(IReadOnlyList<Rectangle> solution) => solution.Count != Hints.Count
        ? new ValidationResult($"Solution size is {solution.Count}, should be {Hints.Count}.")
        : RectangleAreasSumToGridArea(solution);

    private ValidationResult? RectangleAreasSumToGridArea(IReadOnlyList<Rectangle> solution)
    {
        var gridArea = GridSideLength * GridSideLength;
        var sumRectangleAreas = solution.Sum(r => r.AreaInCells);

        return sumRectangleAreas != gridArea
            ? new ValidationResult($"Sum of rectangle areas is {sumRectangleAreas}, grid area is {gridArea}.")
            : NoRectangleOutsideGrid(solution);
    }

    private ValidationResult? NoRectangleOutsideGrid(IReadOnlyList<Rectangle> solution)
    {
        IEnumerable<ValidationResult> errorQuery = from rectangle in solution
            where OutsideGrid(rectangle)
            select new ValidationResult($"Rectangle {rectangle} outside grid.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? NoPairOfRectanglesOverlap(solution);
    }

    private bool OutsideGrid(in Rectangle rectangle)
    {
        var (originColumn, originRow, widthInCells, heightInCells) = rectangle;

        return originColumn >= GridSideLength
               || originRow >= GridSideLength
               || originColumn + widthInCells > GridSideLength
               || originRow + heightInCells > GridSideLength;
    }

    private ValidationResult? NoPairOfRectanglesOverlap(IReadOnlyList<Rectangle> solution)
    {
        IEnumerable<OverlapQueryItem> pairQuery = solution.SelectMany((_, i) =>
            solution.Take(i), (rectangleAtI, rectangleAtH) =>
            new OverlapQueryItem(rectangleAtH, rectangleAtI));

        IEnumerable<ValidationResult> errorQuery = from item in pairQuery
            where item.FirstRectangle.Overlaps(item.SecondRectangle)
            select new ValidationResult($"Rectangles {item.FirstRectangle} and {item.SecondRectangle} overlap.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? EveryRectangleEnclosesExactlyOneHint(solution);
    }

    private ValidationResult? EveryRectangleEnclosesExactlyOneHint(IReadOnlyList<Rectangle> solution)
    {
        IEnumerable<CountQueryItem> countQuery = from rectangle in solution
            let enclosedHintsCount = (from h in Hints where rectangle.Encloses(h) select h.Number).Take(2)
            select new CountQueryItem(rectangle, enclosedHintsCount.Count());

        IEnumerable<ValidationResult> errorQuery = from item in countQuery
            where item.EnclosedHintsCount != 1
            select item.EnclosedHintsCount == 0
                ? new ValidationResult($"Rectangle {item.Rectangle} encloses zero hints.")
                : new ValidationResult($"Rectangle {item.Rectangle} encloses multiple hints.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? EveryRectangleHasAreaEqualToItsEnclosedHintNumber(solution);
    }

    private ValidationResult? EveryRectangleHasAreaEqualToItsEnclosedHintNumber(IEnumerable<Rectangle> solution)
    {
        IEnumerable<AreaQueryItem> areaQuery = from rectangle in solution
            let enclosedHint = (from h in Hints where rectangle.Encloses(h) select h).Single()
            select new AreaQueryItem(rectangle, enclosedHint);

        IEnumerable<ValidationResult> errorQuery = from item in areaQuery
            where item.Rectangle.AreaInCells != item.EnclosedHint.Number
            select new ValidationResult($"Rectangle {item.Rectangle} encloses hint {item.EnclosedHint} with incorrect number.");

        return errorQuery.FirstOrDefault(ValidationResult.Success);
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

    private readonly record struct AreaQueryItem(Rectangle Rectangle, Hint EnclosedHint);

    private readonly record struct CountQueryItem(Rectangle Rectangle, int EnclosedHintsCount);

    private readonly record struct OverlapQueryItem(Rectangle FirstRectangle, Rectangle SecondRectangle);
}
