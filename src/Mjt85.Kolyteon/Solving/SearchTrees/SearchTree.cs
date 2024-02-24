using Mjt85.Kolyteon.Solving.OrderingStrategies;

namespace Mjt85.Kolyteon.Solving.SearchTrees;

internal class SearchTree<N, V, D> : List<N>
    where N : SearchTreeNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public SearchTree()
    {
    }

    public SearchTree(int capacity) : base(capacity)
    {
    }

    public void ReorderNodes(IOrderingStrategy orderingStrategy, int searchLevel)
    {
        var levelOfOptimalNode = orderingStrategy.GetLevelOfOptimalNode(this, searchLevel);
        if (levelOfOptimalNode > searchLevel)
        {
            SwapNodes(searchLevel, levelOfOptimalNode);
        }
    }

    private void SwapNodes(int presentLevel, int futureLevel)
    {
        N presentNode = this[presentLevel];
        N futureNode = this[futureLevel];

        presentNode.SearchTreeLevel = futureLevel;
        futureNode.SearchTreeLevel = presentLevel;

        this[presentLevel] = futureNode;
        this[futureLevel] = presentNode;
    }
}
