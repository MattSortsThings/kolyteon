using System.Diagnostics.CodeAnalysis;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Shikaku;

/// <summary>
///     Can model any Shikaku puzzle as a binary CSP.
/// </summary>
public sealed class ShikakuBinaryCsp : BinaryCsp<ShikakuPuzzle, Hint, Rectangle>
{
    private static readonly RectanglesDoNotOverlapPredicate RectanglesDoNotOverlap = new();

    private readonly List<Hint> _hints;
    private int _gridSideLength;

    /// <summary>
    ///     Initializes a new <see cref="ShikakuBinaryCsp" /> instance that is not modelling a problem and has the specified
    ///     initial capacity.
    /// </summary>
    /// <param name="capacity">
    ///     The number of binary CSP variables the new <see cref="ShikakuBinaryCsp" /> instance can initially store.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public ShikakuBinaryCsp(int capacity) : base(capacity)
    {
        _hints = new List<Hint>(capacity);
    }

    /// <summary>
    ///     Ensures that the capacity of this instance is at least the specified capacity.
    /// </summary>
    /// <returns>A non-negative 32-bit signed integer. The new capacity of this instance.</returns>
    public override int EnsureCapacity(int capacity)
    {
        var newCapacity = base.EnsureCapacity(capacity);
        _ = _hints.EnsureCapacity(capacity);

        return newCapacity;
    }

    /// <summary>
    ///     Sets the capacity of this instance to the actual number of binary CSP variables, if that number is less than a
    ///     threshold value.
    /// </summary>
    /// <remarks>
    ///     This method can be used to reduce overhead if this instance is modelling a problem and it is known that it will not
    ///     need to model a larger problem.
    /// </remarks>
    public override void TrimExcess()
    {
        base.TrimExcess();
        _hints.TrimExcess();
    }

    protected override void PopulateProblemData(ShikakuPuzzle problem)
    {
        _gridSideLength = problem.GridSideLength;
        _hints.AddRange(problem.Hints);
    }

    protected override void ClearProblemData()
    {
        _gridSideLength = default;
        _hints.Clear();
    }

    protected override IEnumerable<Hint> GetVariables() => _hints;

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    protected override IEnumerable<Rectangle> GetDomainOf(Hint variable)
    {
        foreach (Rectangle r in EnumerateAllPossibleRectanglesEnclosing(variable))
        {
            if (RectangleEnclosesOnlyThisHint(in r, in variable))
            {
                yield return r;
            }
        }
    }

    protected override IBinaryPredicate<Rectangle> GetBinaryPredicateFor(Hint variable1, Hint variable2) =>
        RectanglesDoNotOverlap;

    private IEnumerable<Rectangle> EnumerateAllPossibleRectanglesEnclosing(Hint h)
    {
        var (hintColumn, hintRow, hintNumber) = h;

        foreach (var (width, height) in GetAllRectangleDimensions(hintNumber))
        {
            var (fc, fr, lc, lr) = GetOriginCellRange(in width, in height, in hintColumn, in hintRow);

            for (var upperLeftCellRow = fr; upperLeftCellRow <= lr; upperLeftCellRow++)
            {
                for (var upperLeftCellCol = fc; upperLeftCellCol <= lc; upperLeftCellCol++)
                {
                    yield return new Rectangle(upperLeftCellCol, upperLeftCellRow, width, height);
                }
            }
        }
    }

    private IEnumerable<RectangleDimensions> GetAllRectangleDimensions(int area)
    {
        var rowsUpperBound = Math.Min(area, _gridSideLength);
        var columnsUpperBound = _gridSideLength;

        for (var rows = 1; rows <= rowsUpperBound; rows++)
        {
            if (area % rows != 0)
            {
                continue;
            }

            var columns = area / rows;

            if (columns <= columnsUpperBound)
            {
                yield return new RectangleDimensions(columns, rows);
            }
        }
    }

    private OriginCellRange GetOriginCellRange(in int width, in int height, in int hintColumn, in int hintRow)
    {
        var firstColumn = Math.Max(0, hintColumn + 1 - width);
        var lastColumn = Math.Min(_gridSideLength - width, hintColumn);

        var firstRow = Math.Max(0, hintRow + 1 - height);
        var lastRow = Math.Min(_gridSideLength - height, hintRow);

        return new OriginCellRange(firstColumn, firstRow, lastColumn, lastRow);
    }

    private bool RectangleEnclosesOnlyThisHint(in Rectangle rectangle, in Hint currentHint)
    {
        foreach (Hint hint in _hints)
        {
            if (rectangle.Encloses(in hint) && hint != currentHint)
            {
                return false;
            }
        }

        return true;
    }

    private class RectanglesDoNotOverlapPredicate : IBinaryPredicate<Rectangle>
    {
        public bool CanAssign(in Rectangle firstDomainValue, in Rectangle secondDomainValue) =>
            !firstDomainValue.Overlaps(in secondDomainValue);
    }

    private readonly record struct RectangleDimensions(int WidthInCells, int HeightInCells);

    private readonly record struct OriginCellRange(int FirstColumn, int FirstRow, int LastColumn, int LastRow);
}
