using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class NoStrategy : IOrderingStrategy
{
    public OrderingStrategy Identifier => OrderingStrategy.NaturalOrdering;

    public int GetSwapLevel(IReadOnlyList<IVisitableNode> nodes, int searchLevel) => searchLevel;
}
