using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.Shikaku.Internals;

namespace Kolyteon.Shikaku;

/// <summary>
///     Models a <see cref="ShikakuProblem" /> as a generic binary CSP.
/// </summary>
public sealed class ShikakuConstraintGraph : ConstraintGraph<NumberedSquare, Block, ShikakuProblem>
{
    private readonly Queue<NumberedSquare> _orderedHints;
    private int _gridSideLength;

    /// <summary>
    ///     Initializes a new <see cref="ShikakuConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public ShikakuConstraintGraph()
    {
        _orderedHints = new Queue<NumberedSquare>(0);
    }

    /// <summary>
    ///     Initializes a new <see cref="ShikakuConstraintGraph" /> instance with the specified initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" />.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The initial capacity of the <see cref="ShikakuConstraintGraph" /> instance.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public ShikakuConstraintGraph(int capacity) : base(capacity)
    {
        _orderedHints = new Queue<NumberedSquare>(capacity);
    }

    /// <summary>
    ///     Gets or sets the total number of binary CSP variables the internal data structures of this instance can hold
    ///     without resizing.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <see cref="Capacity" /> is set to a value that is less than the value of
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Variables" />.
    /// </exception>
    public override int Capacity
    {
        get => base.Capacity;
        set
        {
            base.Capacity = value;
            _orderedHints.TrimExcess();
            _orderedHints.EnsureCapacity(value);
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="ShikakuConstraintGraph" /> instance that is modelling the specified
    ///     Shikaku problem as a binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    /// <returns>A new <see cref="ShikakuConstraintGraph" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    public static ShikakuConstraintGraph ModellingProblem(ShikakuProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        ShikakuConstraintGraph constraintGraph = new(problem.Hints.Count);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    private protected override void PopulateProblemData(ShikakuProblem problem)
    {
        ((_, (int gridSideLength, _)), IReadOnlyList<NumberedSquare> hints) = problem;

        _gridSideLength = gridSideLength;

        PopulateOrderedHints(hints);
    }

    private protected override IEnumerable<NumberedSquare> GetVariables() => _orderedHints;

    private protected override IEnumerable<Block> GetDomainValues(NumberedSquare presentVariable)
    {
        _orderedHints.Remove(in presentVariable);

        foreach (Dimensions dimensions in GetAllDimensionsWithArea(presentVariable.Number))
        {
            foreach (Block block in GetAllBlocksContainingSquare(dimensions, presentVariable.Square))
            {
                if (ContainsNoOtherHint(block))
                {
                    yield return block;
                }
            }
        }

        _orderedHints.Enqueue(presentVariable);
    }

    private IEnumerable<Dimensions> GetAllDimensionsWithArea(int area)
    {
        for (int width = Math.Min(area, _gridSideLength); width >= 1; width--)
        {
            if (area % width != 0)
            {
                continue;
            }

            if (area / width is var height && height <= _gridSideLength)
            {
                yield return Dimensions.FromWidthAndHeight(width, height);
            }
        }
    }

    private IEnumerable<Block> GetAllBlocksContainingSquare(Dimensions dimensions, Square square)
    {
        (int startColumn, int endColumn) = GetBlockOriginSquareStartAndEndColumns(dimensions.WidthInSquares, square.Column);
        (int startRow, int endRow) = GetBlockOriginSquareStartAndEndRows(dimensions.HeightInSquares, square.Row);


        for (int originColumn = startColumn; originColumn <= endColumn; originColumn++)
        {
            for (int originRow = startRow; originRow <= endRow; originRow++)
            {
                yield return Square.FromColumnAndRow(originColumn, originRow).ToBlock(dimensions);
            }
        }
    }

    private (int, int) GetBlockOriginSquareStartAndEndColumns(in int blockWidth, in int hintColumn) => (
        Math.Max(0, hintColumn + 1 - blockWidth), Math.Min(_gridSideLength - blockWidth, hintColumn));

    private (int, int) GetBlockOriginSquareStartAndEndRows(in int blockHeight, in int hintRow) => (
        Math.Max(0, hintRow + 1 - blockHeight), Math.Min(_gridSideLength - blockHeight, hintRow));

    private bool ContainsNoOtherHint(Block block) => !_orderedHints.Any(hint => block.Contains(hint));

    private protected override bool TryGetBinaryPredicate(NumberedSquare firstVariable,
        NumberedSquare secondVariable,
        [NotNullWhen(true)] out Func<Block, Block, bool>? binaryPredicate)
    {
        binaryPredicate = BlocksDoNotOverlap;

        return true;
    }

    private protected override void ClearProblemData()
    {
        _gridSideLength = default;
        _orderedHints.Clear();
    }

    private void PopulateOrderedHints(IReadOnlyList<NumberedSquare> hints)
    {
        _orderedHints.EnsureCapacity(hints.Count);
        foreach (NumberedSquare hint in hints.OrderBy(hint => hint))
        {
            _orderedHints.Enqueue(hint);
        }
    }

    private static bool BlocksDoNotOverlap(Block firstBlock, Block secondBlock) => !firstBlock.Overlaps(in secondBlock);
}
