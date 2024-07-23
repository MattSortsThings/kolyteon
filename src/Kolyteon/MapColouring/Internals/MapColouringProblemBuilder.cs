using Kolyteon.Common;

namespace Kolyteon.MapColouring.Internals;

internal sealed class MapColouringProblemBuilder : IMapColouringProblemBuilder, IMapColouringProblemBuilder.IColoursSetter,
    IMapColouringProblemBuilder.IGlobalColoursBuilder, IMapColouringProblemBuilder.IBlockSpecificColoursBuilder
{
    private readonly List<BlockDatum> _blockData = new(4);
    private Block _canvas;
    private HashSet<Colour>? _globalColours;

    public IMapColouringProblemBuilder.IBlockSpecificColoursBuilder AddBlockWithColours(Block block, params Colour[] colours)
    {
        _blockData.Add(new BlockDatum(block, colours.ToHashSet()));

        return this;
    }

    public IMapColouringProblemBuilder.IGlobalColoursBuilder UseGlobalColours(params Colour[] colours)
    {
        _globalColours = colours.ToHashSet();

        return this;
    }

    public IMapColouringProblemBuilder.IBlockSpecificColoursBuilder UseBlockSpecificColours() => this;

    public MapColouringProblem Build()
    {
        _blockData.Sort();

        MapColouringProblem problem = new(_canvas, _blockData);

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    public IMapColouringProblemBuilder.IGlobalColoursBuilder AddBlock(Block block)
    {
        _blockData.Add(new BlockDatum(block, _globalColours!));

        return this;
    }

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
