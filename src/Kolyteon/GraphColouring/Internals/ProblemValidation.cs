using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.GraphColouring.Internals;

internal static class ProblemValidation
{
    internal static GraphColouringProblemValidator AtLeastOneNode => new AtLeastOneNodeValidator();

    internal static GraphColouringProblemValidator AllNodesUnique => new AllNodesUniqueValidator();

    internal static GraphColouringProblemValidator EveryEdgeHasFirstNodeInGraph => new EveryEdgeHasFirstNodeInGraphValidator();

    internal static GraphColouringProblemValidator EveryEdgeHasSecondNodeInGraph => new EveryEdgeHasSecondNodeInGraphValidator();

    internal abstract class GraphColouringProblemValidator : ProblemValidator<GraphColouringProblem>;

    private sealed class AtLeastOneNodeValidator : GraphColouringProblemValidator
    {
        internal override CheckingResult Validate(GraphColouringProblem problem) =>
            problem.NodeData.Count == 0
                ? CheckingResult.Failure("Problem has zero nodes.")
                : CheckingResult.Success();
    }

    private sealed class AllNodesUniqueValidator : GraphColouringProblemValidator
    {
        internal override CheckingResult Validate(GraphColouringProblem problem) =>
            problem.NodeData.Select(datum => datum.Node)
                .SelectMany((nodeAtI, i) => problem.NodeData.Take(i)
                    .Where(pastDatum => pastDatum.Node.Equals(nodeAtI))
                    .Select(_ => CheckingResult.Failure($"Node {nodeAtI} occurs more than once."))
                )
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class EveryEdgeHasFirstNodeInGraphValidator : GraphColouringProblemValidator
    {
        internal override CheckingResult Validate(GraphColouringProblem problem) =>
            problem.Edges.ExceptBy(problem.NodeData.Select(datum => datum.Node),
                    edge => edge.FirstNode)
                .Select(edge => CheckingResult.Failure($"Edge {edge} is invalid: node {edge.FirstNode} not present in graph."))
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class EveryEdgeHasSecondNodeInGraphValidator : GraphColouringProblemValidator
    {
        internal override CheckingResult Validate(GraphColouringProblem problem) =>
            problem.Edges.ExceptBy(problem.NodeData.Select(datum => datum.Node),
                    edge => edge.SecondNode)
                .Select(edge => CheckingResult.Failure($"Edge {edge} is invalid: node {edge.SecondNode} not present in graph."))
                .FirstOrDefault(CheckingResult.Success());
    }
}
