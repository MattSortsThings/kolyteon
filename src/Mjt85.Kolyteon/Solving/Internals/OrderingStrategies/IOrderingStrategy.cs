using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

internal interface IOrderingStrategy
{
    public Ordering Identifier { get; }

    public int GetLevelOfOptimalNode<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode;
}
