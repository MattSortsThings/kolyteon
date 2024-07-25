using System.Text.Json.Serialization;
using Kolyteon.Common;
using Kolyteon.GraphColouring.Internals;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Represents a valid (but not necessarily solvable) Graph Colouring problem.
/// </summary>
[Serializable]
public sealed record GraphColouringProblem : ISolutionVerifier<IReadOnlyDictionary<Node, Colour>>
{
    [JsonConstructor]
    internal GraphColouringProblem(IReadOnlyList<NodeDatum> nodeData, IReadOnlyList<Edge> edges)
    {
        NodeData = nodeData;
        Edges = edges;
    }

    /// <summary>
    ///     Gets an immutable list of all node data for the graph.
    /// </summary>
    public IReadOnlyList<NodeDatum> NodeData { get; }

    /// <summary>
    ///     Gets an immutable list of all the edges in the graph.
    /// </summary>
    public IReadOnlyList<Edge> Edges { get; }

    /// <summary>
    ///     Indicates whether this <see cref="GraphColouringProblem" /> instance has equal value to another instance of the
    ///     same type, that is, they represent logically identical Graph Colouring problems.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="GraphColouringProblem" /> instances have equal value if their <see cref="NodeData" /> collections
    ///     contain equal values and their <see cref="Edges" /> collections contain equal values.
    /// </remarks>
    /// <param name="other">The <see cref="GraphColouringProblem" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(GraphColouringProblem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return NodeData.Count == other.NodeData.Count
               && Edges.Count == other.Edges.Count
               && NodeData.OrderBy(datum => datum).SequenceEqual(other.NodeData.OrderBy(datum => datum))
               && Edges.OrderBy(edge => edge).SequenceEqual(other.Edges.OrderBy(edge => edge));
    }

    /// <inheritdoc />
    /// <remarks>
    ///     <para>
    ///         The solution to a <see cref="GraphColouringProblem" /> instance is a dictionary of <see cref="Node" /> keys and
    ///         <see cref="Colour" /> values, representing the colours with which the nodes are to be filled.
    ///     </para>
    ///     <para>
    ///         This method applies the following checks in order to the <paramref name="solution" /> parameter:
    ///         <list type="number">
    ///             <item>The number of entries in the solution must be equal to the number of nodes in the problem.</item>
    ///             <item>Every node in the problem must be a key in the solution.</item>
    ///             <item>Every node must be assigned one of its permitted colours.</item>
    ///             <item>No two adjacent nodes may be assigned the same colour.</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public CheckingResult VerifyCorrect(IReadOnlyDictionary<Node, Colour> solution)
    {
        ArgumentNullException.ThrowIfNull(solution);

        return SolutionVerification.OneEntryPerNode
            .Then(SolutionVerification.EveryNodeIsSolutionKey)
            .Then(SolutionVerification.EveryNodeHasPermittedColour)
            .Then(SolutionVerification.NoAdjacentNodesSameColour)
            .VerifyCorrect(solution, this);
    }

    /// <summary>
    ///     Deconstructs this <see cref="GraphColouringProblem" /> instance.
    /// </summary>
    /// <param name="nodeData">Contains node data for the graph.</param>
    /// <param name="edges">The edges in the graph.</param>
    public void Deconstruct(out IReadOnlyList<NodeDatum> nodeData, out IReadOnlyList<Edge> edges)
    {
        nodeData = NodeData;
        edges = Edges;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="GraphColouringProblem" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(NodeData, Edges);

    /// <summary>
    ///     Starts the process of building a new <see cref="GraphColouringProblem" /> using the fluent builder API.
    /// </summary>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static IGraphColouringProblemBuilder Create() => new GraphColouringProblemBuilder();
}
