using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

internal sealed class MCStrategy : IOrderingStrategy
{
    public Ordering Identifier => Ordering.MaxCardinality;

    public int GetLevelOfOptimalNode<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode =>
        searchLevel == 0 || searchLevel == searchTree.Count - 1
            ? searchLevel
            : GetLevelOfNodeWithMaxCardinality(searchTree, searchLevel);

    private static int GetLevelOfNodeWithMaxCardinality<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode
    {
        N optimalNode = searchTree[^1];
        var maxCardinality = CalculateCardinality(optimalNode, searchTree, searchLevel);

        for (var i = searchTree.Count - 2; i >= searchLevel; i--)
        {
            N nodeAtI = searchTree[i];

            if (nodeAtI.Degree < maxCardinality)
            {
                continue;
            }

            var cardinalityAtI = CalculateCardinality(nodeAtI, searchTree, searchLevel);

            if (cardinalityAtI < maxCardinality || (cardinalityAtI == maxCardinality && nodeAtI.Degree < optimalNode.Degree))
            {
                continue;
            }

            maxCardinality = cardinalityAtI;
            optimalNode = nodeAtI;
        }

        return optimalNode.SearchTreeLevel;
    }

    private static int CalculateCardinality<N>(N node, IList<N> nodes, int searchLevel) where N : IVisitableNode
    {
        var cardinality = 0;
        var degree = node.Degree;

        for (var i = 0; i < searchLevel && cardinality < degree; i++)
        {
            if (node.AdjacentTo(nodes[i]))
            {
                cardinality++;
            }
        }

        return cardinality;
    }
}
