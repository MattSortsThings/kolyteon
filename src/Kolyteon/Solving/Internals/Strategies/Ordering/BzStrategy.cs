using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class BzStrategy : IOrderingStrategy
{
    public OrderingStrategy Identifier => OrderingStrategy.BrelazHeuristic;

    public int GetSwapLevel(IReadOnlyList<IVisitableNode> nodes, int searchLevel)
    {
        IVisitableNode optimalNode = nodes[^1];
        int minCandidates = optimalNode.RemainingCandidates;

        for (int i = nodes.Count - 2; i >= searchLevel; i--)
        {
            IVisitableNode nodeAtI = nodes[i];
            int candidatesAtI = nodeAtI.RemainingCandidates;

            if (candidatesAtI > minCandidates || (candidatesAtI == minCandidates && nodeAtI.Degree < optimalNode.Degree))
            {
                continue;
            }

            minCandidates = candidatesAtI;
            optimalNode = nodeAtI;
        }

        return optimalNode.SearchTreeLevel;
    }
}
