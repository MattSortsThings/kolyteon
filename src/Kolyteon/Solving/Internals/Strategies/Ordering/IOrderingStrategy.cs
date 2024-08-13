using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal interface IOrderingStrategy
{
    public OrderingStrategy Identifier { get; }

    public int GetSwapLevel(IReadOnlyList<IVisitableNode> nodes, int searchLevel);
}
