using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.GraphColouring.Internals;

internal static class SolutionValidation
{
    internal static ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem> OneEntryPerNode =>
        new OneEntryPerNodeVerifier();

    internal static ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem> EveryNodeIsSolutionKey =>
        new EveryNodeIsSolutionKeyVerifier();

    internal static ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem> EveryNodeHasPermittedColour =>
        new EveryNodeHasPermittedColourVerifier();

    internal static ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem> NoAdjacentNodesSameColour =>
        new NoAdjacentNodesSameColourVerifier();

    private sealed class OneEntryPerNodeVerifier : ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Node, Colour> solution, GraphColouringProblem problem)
        {
            int entries = solution.Count;
            int nodes = problem.NodeData.Count;

            return entries == nodes
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Solution has {(entries == 1 ? "1 entry" : entries + " entries")}, " +
                                         $"but problem has {(nodes == 1 ? "1 node" : nodes + " nodes")}.");
        }
    }

    private sealed class EveryNodeIsSolutionKeyVerifier :
        ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Node, Colour> solution, GraphColouringProblem problem) => problem
            .NodeData.Select(datum => datum.Node)
            .Where(node => !solution.ContainsKey(node))
            .Select(node => CheckingResult.Failure($"Node {node} is not a key in the solution."))
            .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class EveryNodeHasPermittedColourVerifier
        : ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Node, Colour> solution, GraphColouringProblem problem) =>
            problem.NodeData.Join(solution,
                    datum => datum.Node,
                    entry => entry.Key,
                    (datum, entry) => new CheckingItem(datum.Node, entry.Value, datum.PermittedColours.Contains(entry.Value))
                ).Where(item => item.IsPermitted == false)
                .Select(item => CheckingResult.Failure($"Node {item.Node} is assigned the colour '{item.AssignedColour}', " +
                                                       $"which is not a member of its permitted colours set."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(
            Node Node,
            Colour AssignedColour,
            bool IsPermitted);
    }

    private sealed class
        NoAdjacentNodesSameColourVerifier : ISolutionVerifier<IReadOnlyDictionary<Node, Colour>, GraphColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Node, Colour> solution, GraphColouringProblem problem) => problem
            .Edges.Select(edge =>
            {
                Colour firstColour = solution[edge.FirstNode];
                Colour secondColour = solution[edge.SecondNode];

                return new CheckingItem(edge.FirstNode, firstColour, edge.SecondNode, secondColour);
            })
            .Where(item => item.FirstColour == item.SecondColour)
            .Select(item => CheckingResult.Failure($"Adjacent nodes {item.FirstNode} and {item.SecondNode} " +
                                                   $"are both assigned the colour '{item.FirstColour}'."))
            .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(
            Node FirstNode,
            Colour FirstColour,
            Node SecondNode,
            Colour SecondColour);
    }
}
