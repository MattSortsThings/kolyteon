using Kolyteon.Common;
using Kolyteon.Common.Internals;
using Kolyteon.GraphColouring.Internals;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Can generate a random, solvable Graph Colouring problem from parameters specified by the client.
/// </summary>
public sealed class GraphColouringGenerator : IGraphColouringGenerator
{
    private const int MinNodes = 1;
    private const int MaxNodes = 50;
    private const int MinPermittedColours = 4;
    private readonly IRandom _random;

    /// <summary>
    ///     Initializes a new <see cref="GraphColouringGenerator" /> instance using a default seed value.
    /// </summary>
    public GraphColouringGenerator()
    {
        _random = new SystemRandom();
    }

    /// <summary>
    ///     Initializes a new <see cref="GraphColouringGenerator" /> instance using the specified seed value.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the
    ///     generator algorithm. If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public GraphColouringGenerator(int seed)
    {
        _random = new SystemRandom(seed);
    }

    internal GraphColouringGenerator(IRandom random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    /// <inheritdoc />
    public GraphColouringProblem Generate(int nodes, IReadOnlySet<Colour> permittedColours)
    {
        ThrowIfInvalidNodes(nodes, nameof(nodes));
        ArgumentNullException.ThrowIfNull(permittedColours);
        ThrowIfInsufficientColours(permittedColours, nameof(permittedColours));

        Block[] blocks = GenerateBlocks(nodes);

        return GraphColouringProblem.Create()
            .UseGlobalColours(permittedColours.ToArray())
            .AddNodes(blocks.ToNodes())
            .AddEdges(blocks.ToEdges())
            .Build();
    }

    /// <inheritdoc />
    public void UseSeed(int seed) => _random.UseSeed(seed);

    private Block[] GenerateBlocks(int quantity)
    {
        Block[] blocks = new Block[quantity];
        blocks[0] = Dimensions.FromWidthAndHeight(10, 10).ToBlock();

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

    private static void ThrowIfInvalidNodes(int nodes, string paramName)
    {
        if (nodes is < MinNodes or > MaxNodes)
        {
            throw new ArgumentOutOfRangeException(paramName, nodes,
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
