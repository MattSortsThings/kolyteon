using Kolyteon.Common;

namespace Kolyteon.GraphColouring.Internals;

internal sealed class GraphColouringProblemBuilder : IGraphColouringProblemBuilder,
    IGraphColouringProblemBuilder.INodeAdder,
    IGraphColouringProblemBuilder.INodeAndColoursAdder
{
    private readonly HashSet<Edge> _edges = new(4);
    private readonly List<NodeDatum> _nodeData = new(4);
    private HashSet<Colour>? _globalColours;

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.INodeAdder UseGlobalColours(params Colour[] colours)
    {
        _globalColours = colours.ToHashSet();

        return this;
    }

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.INodeAndColoursAdder UseNodeSpecificColours() => this;

    /// <inheritdoc />
    public GraphColouringProblem Build()
    {
        GraphColouringProblem problem = new(_nodeData.OrderBy(datum => datum).ToArray(),
            _edges.OrderBy(edge => edge).ToArray());

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.IEdgeAdder AddEdge(Edge edge)
    {
        _edges.Add(edge);

        return this;
    }

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.IEdgeAdder AddEdges(IEnumerable<Edge> edges)
    {
        ArgumentNullException.ThrowIfNull(edges);

        foreach (Edge edge in edges)
        {
            _edges.Add(edge);
        }

        return this;
    }

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.INodeAdder AddNode(Node node)
    {
        _nodeData.Add(new NodeDatum(node, _globalColours!));

        return this;
    }

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.INodeAdder AddNodes(IEnumerable<Node> nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);

        foreach (Node node in nodes)
        {
            _nodeData.Add(new NodeDatum(node, _globalColours!));
        }

        return this;
    }

    /// <inheritdoc />
    public IGraphColouringProblemBuilder.INodeAndColoursAdder AddNodeAndColours(Node node, params Colour[] colours)
    {
        _nodeData.Add(new NodeDatum(node, colours.ToHashSet()));

        return this;
    }

    private static void ThrowIfInvalidProblem(GraphColouringProblem problem)
    {
        Result validationResult = ProblemValidation.AtLeastOneNode
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
