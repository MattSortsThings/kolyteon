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

    public MapColouringProblem Build()
    {
        MapColouringProblem problem = new(_canvas, _blockData.OrderBy(datum => datum).ToArray());

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    public IMapColouringProblemBuilder.IBlockAdder AddBlock(Block block)
    {
        _blockData.Add(new BlockDatum(block, _globalColours!));

        return this;
    }

    public IMapColouringProblemBuilder.IBlockAndColoursAdder AddBlockWithColours(Block block, params Colour[] colours)
    {
        _blockData.Add(new BlockDatum(block, colours.ToHashSet()));

        return this;
    }

    public IMapColouringProblemBuilder.IBlockAdder UseGlobalColours(params Colour[] colours)
    {
        _globalColours = colours.ToHashSet();

        return this;
    }

    public IMapColouringProblemBuilder.IBlockAndColoursAdder UseBlockSpecificColours() => this;

    public IMapColouringProblemBuilder.IColoursSetter WithCanvasSize(Dimensions dimensions)
    {
        _canvas = dimensions.ToBlock();

        return this;
    }

    private static void ThrowIfInvalidProblem(MapColouringProblem problem)
    {
        CheckingResult validationResult = ProblemValidation.AtLeastOneBlock
            .Then(ProblemValidation.AllBlocksInCanvas)
            .Then(ProblemValidation.NoOverlappingBlocks).Validate(problem);

        if (validationResult is { IsSuccessful: false, FirstError: not null })
        {
            throw new InvalidProblemException(validationResult.FirstError);
        }
    }
}