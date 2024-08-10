using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Shikaku;

/// <summary>
///     Can generate a random, solvableShikaku problem from parameters specified by the client.
/// </summary>
public sealed class ShikakuGenerator : IShikakuGenerator
{
    private const int MinGridSideLength = 5;
    private const int MaxGridSideLength = 20;
    private const int MinHints = 1;
    private readonly IRandom _random;

    /// <summary>
    ///     Initializes a new <see cref="ShikakuGenerator" /> instance using a default seed value.
    /// </summary>
    public ShikakuGenerator()
    {
        _random = new SystemRandom();
    }

    /// <summary>
    ///     Initializes a new <see cref="ShikakuGenerator" /> instance using the specified seed value.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the
    ///     generator algorithm. If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public ShikakuGenerator(int seed)
    {
        _random = new SystemRandom(seed);
    }

    internal ShikakuGenerator(IRandom random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    /// <inheritdoc />
    public ShikakuProblem Generate(int gridSideLength, int hints)
    {
        ThrowIfInvalidGridSideLength(gridSideLength, nameof(gridSideLength));
        ThrowIfInvalidHints(hints, nameof(hints), gridSideLength);

        Block[] blocks = GenerateBlocks(gridSideLength, hints);

        int?[,] grid = MapToGrid(gridSideLength, blocks);

        return ShikakuProblem.FromGrid(grid);
    }

    /// <inheritdoc />
    public void UseSeed(int seed) => _random.UseSeed(seed);

    private int?[,] MapToGrid(int gridSideLength, Block[] blocks)
    {
        int?[,] grid = new int?[gridSideLength, gridSideLength];

        foreach (Block block in blocks)
        {
            (int originColumn, int originRow) = block.OriginSquare;
            (int terminusColumn, int terminusRow) = block.TerminusSquare;

            int hintColumn = _random.Next(originColumn, terminusColumn + 1);
            int hintRow = _random.Next(originRow, terminusRow + 1);

            grid[hintRow, hintColumn] = block.AreaInSquares;
        }

        return grid;
    }

    private Block[] GenerateBlocks(int gridSideLength, int quantity)
    {
        Block[] blocks = new Block[quantity];
        blocks[0] = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

        (int left, int right) = (1, quantity);

        while (left > 0 && left < right)
        {
            int pointer = _random.Next(left);
            (Block blockA, Block blockB) = Divide(blocks[pointer]);
            blocks[pointer] = blocks[--left];

            if (blockA.AreaInSquares <= 3)
            {
                blocks[--right] = blockA;
            }
            else
            {
                blocks[left++] = blockA;
            }

            if (blockB.AreaInSquares <= 3)
            {
                blocks[--right] = blockB;
            }
            else
            {
                blocks[left++] = blockB;
            }
        }

        return blocks;
    }

    private BlockPair Divide(Block block)
    {
        (_, (int width, int height)) = block;

        if (width == 1)
        {
            return block.DivideOnRow(_random.Next(2, height - 1));
        }

        if (height == 1)
        {
            return block.DivideOnColumn(_random.Next(2, width - 1));
        }

        return _random.Next() % 2 == 0
            ? block.DivideOnRow(_random.Next(1, height))
            : block.DivideOnColumn(_random.Next(1, width));
    }

    private static void ThrowIfInvalidGridSideLength(int gridSideLength, string paramName)
    {
        if (gridSideLength is < MinGridSideLength or > MaxGridSideLength)
        {
            throw new ArgumentOutOfRangeException(paramName, gridSideLength,
                "Value must be not less than 5 and not greater than 20.");
        }
    }

    private static void ThrowIfInvalidHints(int hints, string paramName, int gridSideLength)
    {
        if (hints < MinHints || hints > 2 * gridSideLength)
        {
            throw new ArgumentOutOfRangeException(paramName, hints,
                "Value must be greater than 0 and not greater than twice the specified grid side length.");
        }
    }
}
