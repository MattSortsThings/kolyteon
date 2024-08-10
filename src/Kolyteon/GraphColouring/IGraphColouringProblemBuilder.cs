using Kolyteon.Common;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Fluent builder API for the <see cref="GraphColouringProblem" /> record type.
/// </summary>
/// <remarks>
///     Any <see cref="GraphColouringProblem" /> instance built using this API is guaranteed to represent a valid (but not
///     necessarily solvable) Graph Colouring problem.
/// </remarks>
public interface IGraphColouringProblemBuilder
{
    /// <summary>
    ///     Configures the builder to use the specified global set of permitted colours for every node added to the graph.
    /// </summary>
    /// <param name="colours">The permitted colours for all nodes in the graph. Any duplicate values will be discarded.</param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    public INodeAdder UseGlobalColours(params Colour[] colours);

    /// <summary>
    ///     Configures the builder to use a different set of permitted colours for each node added to the graph.
    /// </summary>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    public INodeAndColoursAdder UseNodeSpecificColours();

    /// <summary>
    ///     Fluent builder API for the <see cref="GraphColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="GraphColouringProblem" /> instance built using this API is guaranteed to represent a valid (but not
    ///     necessarily solvable) Graph Colouring problem.
    /// </remarks>
    public interface INodeAdder : IEdgeAdder
    {
        /// <summary>
        ///     Adds the specified node to the graph.
        /// </summary>
        /// <param name="node">The node to be added to the graph.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public INodeAdder AddNode(Node node);

        /// <summary>
        ///     Adds the specified nodes to the graph.
        /// </summary>
        /// <param name="nodes">The node to be added to the graph.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="nodes" /> is <see langword="null" />.</exception>
        public INodeAdder AddNodes(IEnumerable<Node> nodes);
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="GraphColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="GraphColouringProblem" /> instance built using this API is guaranteed to represent a valid (but not
    ///     necessarily solvable) Graph Colouring problem.
    /// </remarks>
    public interface INodeAndColoursAdder : IEdgeAdder
    {
        /// <summary>
        ///     Adds the specified node to the graph, with the specified set of permitted colours.
        /// </summary>
        /// <param name="node">The node to be added to the graph.</param>
        /// <param name="colours">The permitted colours for the node. Any duplicate values will be discarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public INodeAndColoursAdder AddNodeAndColours(Node node, params Colour[] colours);
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="GraphColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="GraphColouringProblem" /> instance built using this API is guaranteed to represent a valid (but not
    ///     necessarily solvable) Graph Colouring problem.
    /// </remarks>
    public interface IEdgeAdder : ITerminal
    {
        /// <summary>
        ///     Adds the specified edge to the graph, connecting two nodes that have been added in previous method invocations.
        /// </summary>
        /// <remarks>
        ///     If an identical edge has already been added in a previous method invocation, this edge will be disregarded.
        /// </remarks>
        /// <param name="edge">The edge to be added to the graph.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IEdgeAdder AddEdge(Edge edge);

        /// <summary>
        ///     Adds the specified edges to the graph, each edge connecting two nodes that have been added in previous method
        ///     invocations.
        /// </summary>
        /// <remarks>
        ///     Any identical edges will be discarded.
        /// </remarks>
        /// <param name="edges">The edges to be added to the graph.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="edges" /> is <see langword="null" />.</exception>
        public IEdgeAdder AddEdges(IEnumerable<Edge> edges);
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="GraphColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="GraphColouringProblem" /> instance built using this API is guaranteed to represent a valid (but not
    ///     necessarily solvable) Graph Colouring problem.
    /// </remarks>
    public interface ITerminal
    {
        /// <summary>
        ///     Creates and returns a new <see cref="GraphColouringProblem" /> instance as specified by all the previous fluent
        ///     builder method invocations.
        /// </summary>
        /// <returns>A new <see cref="GraphColouringProblem" /> instance.</returns>
        /// <exception cref="InvalidProblemException">
        ///     The <see cref="GraphColouringProblem" /> as specified did not represent a valid Graph Colouring problem.
        /// </exception>
        public GraphColouringProblem Build();
    }
}
