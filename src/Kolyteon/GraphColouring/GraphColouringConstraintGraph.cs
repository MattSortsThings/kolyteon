using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Models a <see cref="GraphColouringProblem" /> instance as a generic binary CSP.
/// </summary>
public sealed class GraphColouringConstraintGraph : ConstraintGraph<Node, Colour, GraphColouringProblem>
{
    private readonly HashSet<Edge> _problemEdges;
    private readonly Dictionary<Node, IReadOnlyCollection<Colour>> _problemNodesAndPermittedColours;

    /// <summary>
    ///     Initializes a new <see cref="GraphColouringConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public GraphColouringConstraintGraph()
    {
        _problemNodesAndPermittedColours = [];
        _problemEdges = [];
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
        _problemNodesAndPermittedColours = new Dictionary<Node, IReadOnlyCollection<Colour>>(capacity);
        _problemEdges = new HashSet<Edge>(capacity);
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
            _problemNodesAndPermittedColours.TrimExcess(value);
            _problemEdges.TrimExcess();
            _problemEdges.EnsureCapacity(value);
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

    /// <inheritdoc />
    protected override void PopulateProblemData(GraphColouringProblem problem)
    {
        (IReadOnlyList<NodeDatum> nodesAndPermittedColours, IReadOnlyList<Edge> edges) = problem;

        PopulateProblemNodesAndPermittedColours(nodesAndPermittedColours);
        PopulateProblemEdges(edges);
    }

    /// <inheritdoc />
    protected override IEnumerable<Node> GetVariables() => _problemNodesAndPermittedColours.Keys;

    /// <inheritdoc />
    protected override IEnumerable<Colour> GetDomainValues(Node presentVariable) =>
        _problemNodesAndPermittedColours[presentVariable];

    /// <inheritdoc />
    protected override bool TryGetBinaryPredicate(Node firstVariable,
        Node secondVariable,
        [NotNullWhen(true)] out Func<Colour, Colour, bool>? binaryPredicate)
    {
        binaryPredicate = _problemEdges.Contains(Edge.Between(firstVariable, secondVariable))
            ? DifferentColours
            : null;

        return binaryPredicate is not null;
    }

    /// <inheritdoc />
    protected override void ClearProblemData()
    {
        _problemNodesAndPermittedColours.Clear();
        _problemEdges.Clear();
    }


    private void PopulateProblemNodesAndPermittedColours(IReadOnlyList<NodeDatum> nodedata)
    {
        _problemNodesAndPermittedColours.EnsureCapacity(nodedata.Count);
        foreach ((Node node, IReadOnlyCollection<Colour> permittedColours) in nodedata)
        {
            _problemNodesAndPermittedColours.Add(node, permittedColours);
        }
    }

    private void PopulateProblemEdges(IReadOnlyList<Edge> edges)
    {
        _problemEdges.EnsureCapacity(edges.Count);
        foreach (Edge edge in edges)
        {
            _problemEdges.Add(edge);
        }
    }

    private static bool DifferentColours(Colour firstColour, Colour secondColour) => firstColour != secondColour;
}
