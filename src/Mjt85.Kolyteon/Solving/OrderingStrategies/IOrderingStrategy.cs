using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.OrderingStrategies;

internal interface IOrderingStrategy
{
    public Ordering Identifier { get; }

    public int GetLevelOfOptimalNode<N>(IList<N> searchTree, int searchLevel) where N : IVisitableNode;
}
