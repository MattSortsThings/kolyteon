using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class McStrategy : IOrderingStrategy
{
    public OrderingStrategy Identifier => OrderingStrategy.MaxCardinality;

    public int GetSwapLevel(IReadOnlyList<IVisitableNode> nodes, int searchLevel)
    {
        if (searchLevel == 0 || searchLevel == nodes.Count - 1)
        {
            return searchLevel;
        }

        return GetLevelOfNodeWithMaxCardinality(nodes, searchLevel);
    }

    private int GetLevelOfNodeWithMaxCardinality(IReadOnlyList<IVisitableNode> nodes, int searchLevel)
    {
        IVisitableNode optimalNode = nodes[^1];
        int maxCardinality = CalculateCardinality(optimalNode, nodes, searchLevel);

        for (int i = nodes.Count - 2; i >= searchLevel; i--)
        {
            IVisitableNode nodeAtI = nodes[i];

            if (nodeAtI.Degree < maxCardinality)
            {
                continue;
            }

            int cardinalityAtI = CalculateCardinality(nodeAtI, nodes, searchLevel);

            if (cardinalityAtI < maxCardinality || (cardinalityAtI == maxCardinality && nodeAtI.Degree < optimalNode.Degree))
            {
                continue;
            }

            maxCardinality = cardinalityAtI;
            optimalNode = nodeAtI;
        }

        return optimalNode.SearchTreeLevel;
    }


    private static int CalculateCardinality(IVisitableNode node, IReadOnlyList<IVisitableNode> nodes, int searchLevel)
    {
        int cardinality = 0;
        int degree = node.Degree;

        for (int i = 0; i < searchLevel && cardinality < degree; i++)
        {
            if (node.AdjacentTo(nodes[i]))
            {
                cardinality++;
            }
        }

        return cardinality;
    }
}
