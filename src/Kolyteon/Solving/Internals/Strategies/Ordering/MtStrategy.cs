using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class MtStrategy : IOrderingStrategy
{
    private const double Tolerance = 0.000000001D;

    public OrderingStrategy Identifier => OrderingStrategy.MaxTightness;

    public int GetSwapLevel(IReadOnlyList<IVisitableNode> nodes, int searchLevel)
    {
        IVisitableNode optimalNode = nodes[^1];
        double maxSumTightness = optimalNode.SumTightness;

        for (int i = nodes.Count - 2; i >= searchLevel; i--)
        {
            IVisitableNode nodeAtI = nodes[i];
            double sumTightnessAtI = nodeAtI.SumTightness;

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
