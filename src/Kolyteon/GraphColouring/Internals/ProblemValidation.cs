using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.GraphColouring.Internals;

internal static class ProblemValidation
{
    internal static IProblemValidator<GraphColouringProblem> AtLeastOneNode =>
        new AtLeastOneNodeValidator();

    internal static IProblemValidator<GraphColouringProblem> AllNodesUnique =>
        new AllNodesUniqueValidator();

    internal static IProblemValidator<GraphColouringProblem> EveryEdgeHasFirstNodeInGraph =>
        new EveryEdgeHasFirstNodeInGraphValidator();

    internal static IProblemValidator<GraphColouringProblem> EveryEdgeHasSecondNodeInGraph =>
        new EveryEdgeHasSecondNodeInGraphValidator();

    private sealed class AtLeastOneNodeValidator : IProblemValidator<GraphColouringProblem>
    {
        public CheckingResult Validate(GraphColouringProblem problem) => problem.NodeData.Count == 0
            ? CheckingResult.Failure("Problem has zero nodes.")
            : CheckingResult.Success();
    }

    private sealed class AllNodesUniqueValidator : IProblemValidator<GraphColouringProblem>
    {
        public CheckingResult Validate(GraphColouringProblem problem) =>
            problem.NodeData.Select(datum => datum.Node)
                .SelectMany((nodeAtI, i) => problem.NodeData.Take(i)
                    .Where(pastDatum => pastDatum.Node.Equals(nodeAtI))
                    .Select(_ => CheckingResult.Failure($"Node {nodeAtI} occurs more than once."))
                )
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class EveryEdgeHasFirstNodeInGraphValidator : IProblemValidator<GraphColouringProblem>
    {
        public CheckingResult Validate(GraphColouringProblem problem) =>
            problem.Edges.ExceptBy(problem.NodeData.Select(datum => datum.Node),
                    edge => edge.FirstNode)
                .Select(edge => CheckingResult.Failure($"Edge {edge} is invalid: node {edge.FirstNode} not present in graph."))
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class EveryEdgeHasSecondNodeInGraphValidator : IProblemValidator<GraphColouringProblem>
    {
        public CheckingResult Validate(GraphColouringProblem problem) =>
            problem.Edges.ExceptBy(problem.NodeData.Select(datum => datum.Node),
                    edge => edge.SecondNode)
                .Select(edge => CheckingResult.Failure($"Edge {edge} is invalid: node {edge.SecondNode} not present in graph."))
                .FirstOrDefault(CheckingResult.Success());
    }
}
