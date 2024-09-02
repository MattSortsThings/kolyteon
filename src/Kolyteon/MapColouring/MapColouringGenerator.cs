using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.MapColouring;

/// <summary>
///     Can generate a random, solvable Map Colouring problem from parameters specified by the client.
/// </summary>
public sealed class MapColouringGenerator : IMapColouringGenerator
{
    private const int MinBlocks = 1;
    private const int MaxBlocks = 50;
    private const int MinPermittedColours = 4;
    private readonly IRandom _random;

    /// <summary>
    ///     Initializes a new <see cref="MapColouringGenerator" /> instance using a default seed value.
    /// </summary>
    public MapColouringGenerator()
    {
        _random = new SystemRandom();
    }

    /// <summary>
    ///     Initializes a new <see cref="MapColouringGenerator" /> instance using the specified seed value.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the
    ///     generator algorithm. If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public MapColouringGenerator(int seed)
    {
        _random = new SystemRandom(seed);
    }

    internal MapColouringGenerator(IRandom random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    /// <inheritdoc />
    /// <remarks>The generated problem has a 10x10 canvas, covered by a tessellation of blocks with no gaps.</remarks>
    public MapColouringProblem Generate(int blocks, IReadOnlySet<Colour> permittedColours)
    {
        ThrowIfInvalidBlocks(blocks, nameof(blocks));
        ArgumentNullException.ThrowIfNull(permittedColours);
        ThrowIfInsufficientColours(permittedColours, nameof(permittedColours));

        Dimensions tenByTenCanvas = Dimensions.FromWidthAndHeight(10, 10);

        return MapColouringProblem.Create()
            .WithCanvasSize(tenByTenCanvas)
            .UseGlobalColours([..permittedColours])
            .AddBlocks(GenerateBlocks(tenByTenCanvas, blocks))
            .Build();
    }

    /// <inheritdoc />
    public void UseSeed(int seed) => _random.UseSeed(seed);

    private Block[] GenerateBlocks(Dimensions canvasDimensions, int quantity)
    {
        Block[] blocks = new Block[quantity];
        blocks[0] = canvasDimensions.ToBlock();

        (int left, int right) = (1, quantity);

        while (left > 0 && left < right)
        {
            int pointer = _random.Next(left);
            (Block blockA, Block blockB) = Divide(blocks[pointer]);
            blocks[pointer] = blocks[--left];

            if (blockA.AreaInSquares <= 1)
            {
                blocks[--right] = blockA;
            }
            else
            {
                blocks[left++] = blockA;
            }

            if (blockB.AreaInSquares <= 1)
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
            return block.DivideOnRow(_random.Next(1, height));
        }

        if (height == 1)
        {
            return block.DivideOnColumn(_random.Next(1, width));
        }

        return _random.Next() % 2 == 0
            ? block.DivideOnRow(_random.Next(1, height))
            : block.DivideOnColumn(_random.Next(1, width));
    }

    private static void ThrowIfInvalidBlocks(int blocks, string paramName)
    {
        if (blocks is < MinBlocks or > MaxBlocks)
        {
            throw new ArgumentOutOfRangeException(paramName, blocks,
                "Value must be greater than 0 and less than or equal to 50.");
        }
    }

    private static void ThrowIfInsufficientColours(IReadOnlyCollection<Colour> colours, string paramName)
    {
        if (colours.Count < MinPermittedColours)
        {
            throw new ArgumentException("Must supply a set of at least 4 permitted colours.", paramName);
        }
    }
}
