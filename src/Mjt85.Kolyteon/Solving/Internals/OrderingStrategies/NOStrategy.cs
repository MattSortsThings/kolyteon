using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

internal sealed class NOStrategy : IOrderingStrategy
{
    public Ordering Identifier => Ordering.None;

    public int GetLevelOfOptimalNode<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode => searchLevel;
}
