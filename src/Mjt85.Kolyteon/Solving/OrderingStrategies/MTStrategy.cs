using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.OrderingStrategies;

internal sealed class MTStrategy : IOrderingStrategy
{
    private const double Tolerance = 0.000000001D;

    public Ordering Identifier => Ordering.MaxTightness;

    public int GetLevelOfOptimalNode<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode
    {
        N optimalNode = searchTree[^1];
        var maxSumTightness = optimalNode.SumTightness;

        for (var i = searchTree.Count - 2; i >= searchLevel; i--)
        {
            N nodeAtI = searchTree[i];
            var sumTightnessAtI = nodeAtI.SumTightness;

            if (sumTightnessAtI < maxSumTightness ||
                (Math.Abs(sumTightnessAtI - maxSumTightness) < Tolerance && nodeAtI.Degree < optimalNode.Degree))
            {
                continue;
            }

            maxSumTightness = sumTightnessAtI;
            optimalNode = nodeAtI;
        }

        return optimalNode.SearchTreeLevel;
    }
}
