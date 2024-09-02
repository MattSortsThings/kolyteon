using Kolyteon.Common;

namespace Kolyteon.MapColouring.Internals;

internal sealed class MapColouringProblemBuilder : IMapColouringProblemBuilder,
    IMapColouringProblemBuilder.IColoursSetter,
    IMapColouringProblemBuilder.IBlockAdder,
    IMapColouringProblemBuilder.IBlockAndColoursAdder
{
    private readonly List<BlockDatum> _blockData = new(4);
    private Block _canvas;
    private HashSet<Colour>? _globalColours;

    /// <inheritdoc />
    public MapColouringProblem Build()
    {
        MapColouringProblem problem = new(_canvas, [.. _blockData.OrderBy(datum => datum)]);

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    /// <inheritdoc />
    public IMapColouringProblemBuilder.IBlockAdder AddBlock(Block block)
    {
        _blockData.Add(new BlockDatum(block, _globalColours!));

        return this;
    }

    /// <inheritdoc />
    public IMapColouringProblemBuilder.IBlockAdder AddBlocks(IEnumerable<Block> blocks)
    {
        ArgumentNullException.ThrowIfNull(blocks);

        foreach (Block block in blocks)
        {
            _blockData.Add(new BlockDatum(block, _globalColours!));
        }

        return this;
    }

    /// <inheritdoc />
    public IMapColouringProblemBuilder.IBlockAndColoursAdder AddBlockAndColours(Block block, params Colour[] colours)
    {
        _blockData.Add(new BlockDatum(block, [.. colours.Distinct()]));

        return this;
    }

    /// <inheritdoc />
    public IMapColouringProblemBuilder.IBlockAdder UseGlobalColours(params Colour[] colours)
    {
        _globalColours = [.. colours.Distinct()];

        return this;
    }

    /// <inheritdoc />
    public IMapColouringProblemBuilder.IBlockAndColoursAdder UseBlockSpecificColours() => this;

    /// <inheritdoc />
    public IMapColouringProblemBuilder.IColoursSetter WithCanvasSize(Dimensions dimensions)
    {
        _canvas = dimensions.ToBlock();

        return this;
    }

    private static void ThrowIfInvalidProblem(MapColouringProblem problem)
    {
        Result validationResult = ProblemValidation.AtLeastOneBlock
            .Then(ProblemValidation.AllBlocksInCanvas)
            .Then(ProblemValidation.NoOverlappingBlocks).Validate(problem);

        if (validationResult is { IsSuccessful: false, FirstError: not null })
        {
            throw new InvalidProblemException(validationResult.FirstError);
        }
    }
}
