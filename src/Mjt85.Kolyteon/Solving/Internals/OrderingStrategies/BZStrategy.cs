using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

internal sealed class BZStrategy : IOrderingStrategy
{
    public Ordering Identifier => Ordering.Brelaz;

    public int GetLevelOfOptimalNode<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode
    {
        N optimalNode = searchTree[^1];
        var minCandidates = optimalNode.RemainingCandidates;

        for (var i = searchTree.Count - 2; i >= searchLevel; i--)
        {
            N nodeAtI = searchTree[i];
            var candidatesAtI = nodeAtI.RemainingCandidates;

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
