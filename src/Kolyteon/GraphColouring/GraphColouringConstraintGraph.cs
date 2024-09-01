using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Models a <see cref="GraphColouringProblem" /> instance as a generic binary CSP.
/// </summary>
public sealed class GraphColouringConstraintGraph : ConstraintGraph<Node, Colour, GraphColouringProblem>
{
    private readonly HashSet<Edge> _edges;
    private readonly Dictionary<Node, IReadOnlyCollection<Colour>> _nodesAndPermittedColours;

    /// <summary>
    ///     Initializes a new <see cref="GraphColouringConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public GraphColouringConstraintGraph()
    {
        _nodesAndPermittedColours = [];
        _edges = [];
    }

    /// <summary>
    ///     Initializes a new <see cref="GraphColouringConstraintGraph" /> instance with the specified initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" />.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The initial capacity of the <see cref="GraphColouringConstraintGraph" /> instance.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public GraphColouringConstraintGraph(int capacity) : base(capacity)
    {
        _nodesAndPermittedColours = new Dictionary<Node, IReadOnlyCollection<Colour>>(capacity);
        _edges = new HashSet<Edge>(capacity);
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
            _nodesAndPermittedColours.TrimExcess(value);
            _edges.TrimExcess();
            _edges.EnsureCapacity(value);
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="GraphColouringConstraintGraph" /> instance that is modelling the specified
    ///     Graph Colouring problem as a binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    /// <returns>A new <see cref="GraphColouringConstraintGraph" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    public static GraphColouringConstraintGraph ModellingProblem(GraphColouringProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        GraphColouringConstraintGraph constraintGraph = new(problem.NodeData.Count);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    protected override void PopulateProblemData(GraphColouringProblem problem)
    {
        (IReadOnlyList<NodeDatum> nodesAndPermittedColours, IReadOnlyList<Edge> edges) = problem;

        PopulateNodes(nodesAndPermittedColours);
        PopulateEdges(edges);
    }

    protected override IEnumerable<Node> GetVariables() => _nodesAndPermittedColours.Keys;

    protected override IEnumerable<Colour> GetDomainValues(Node presentVariable) =>
        _nodesAndPermittedColours[presentVariable];

    protected override bool TryGetBinaryPredicate(Node firstVariable,
        Node secondVariable,
        [NotNullWhen(true)] out Func<Colour, Colour, bool>? binaryPredicate)
    {
        binaryPredicate = _edges.Contains(Edge.Between(firstVariable, secondVariable))
            ? DifferentColours
            : null;

        return binaryPredicate is not null;
    }

    protected override void ClearProblemData()
    {
        _nodesAndPermittedColours.Clear();
        _edges.Clear();
    }

    private void PopulateNodes(IReadOnlyList<NodeDatum> nodesAndPermittedColours)
    {
        _nodesAndPermittedColours.EnsureCapacity(nodesAndPermittedColours.Count);
        foreach ((Node node, IReadOnlyCollection<Colour> permittedColours) in nodesAndPermittedColours)
        {
            _nodesAndPermittedColours.Add(node, permittedColours);
        }
    }

    private void PopulateEdges(IReadOnlyList<Edge> edges)
    {
        _edges.EnsureCapacity(edges.Count);
        foreach (Edge edge in edges)
        {
            _edges.Add(edge);
        }
    }

    private static bool DifferentColours(Colour firstColour, Colour secondColour) => firstColour != secondColour;
}
