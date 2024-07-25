using Kolyteon.Common;

namespace Kolyteon.GraphColouring.Internals;

internal sealed class GraphColouringProblemBuilder : IGraphColouringProblemBuilder,
    IGraphColouringProblemBuilder.INodeAdder,
    IGraphColouringProblemBuilder.INodeAndColoursAdder
{
    private readonly HashSet<Edge> _edges = new(4);
    private readonly List<NodeDatum> _nodeData = new(4);
    private HashSet<Colour>? _globalColours;

    public IGraphColouringProblemBuilder.INodeAdder UseGlobalColours(params Colour[] colours)
    {
        _globalColours = colours.ToHashSet();

        return this;
    }

    public IGraphColouringProblemBuilder.INodeAndColoursAdder UseNodeSpecificColours() => this;

    public GraphColouringProblem Build()
    {
        GraphColouringProblem problem = new(_nodeData.OrderBy(datum => datum).ToArray(),
            _edges.OrderBy(edge => edge).ToArray());

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    public IGraphColouringProblemBuilder.IEdgeAdder AddEdge(Edge edge)
    {
        _edges.Add(edge);

        return this;
    }

    public IGraphColouringProblemBuilder.INodeAdder AddNode(Node node)
    {
        _nodeData.Add(new NodeDatum(node, _globalColours!));

        return this;
    }

    public IGraphColouringProblemBuilder.INodeAndColoursAdder AddNodeAndColours(Node node, params Colour[] colours)
    {
        _nodeData.Add(new NodeDatum(node, colours.ToHashSet()));

        return this;
    }

    private static void ThrowIfInvalidProblem(GraphColouringProblem problem)
    {
        CheckingResult validationResult = ProblemValidation.AtLeastOneNode
            .Then(ProblemValidation.AllNodesUnique)
            .Then(ProblemValidation.EveryEdgeHasFirstNodeInGraph)
            .Then(ProblemValidation.EveryEdgeHasSecondNodeInGraph)
            .Validate(problem);

        if (validationResult is { IsSuccessful: false, FirstError: not null })
        {
            throw new InvalidProblemException(validationResult.FirstError);
        }
    }
}
