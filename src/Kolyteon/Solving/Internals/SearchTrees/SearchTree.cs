using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving.Internals.SearchTrees;

internal abstract class SearchTree<TNode, TVariable, TDomainValue> : List<TNode>
    where TNode : SearchTreeNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected SearchTree(int capacity) : base(capacity)
    {
        RootLevel = Constants.Levels.Root;
        SearchLevel = Constants.Levels.Root;
    }

    public int LeafLevel => Count;

    public int RootLevel { get; }

    public int SearchLevel { get; set; }

    public TNode GetPresentNode() => this[SearchLevel];

    public void Populate(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
    {
        EnsureCapacity(binaryCsp.Variables);
        for (int i = 0; i < binaryCsp.Variables; i++)
        {
            Add(GetNode(i, binaryCsp));
        }
    }

    public void Reorder(IOrderingStrategy orderingStrategy)
    {
        int swapLevel = orderingStrategy.GetSwapLevel(this, SearchLevel);
        if (swapLevel != SearchLevel)
        {
            SwapNodes(swapLevel);
        }
    }

    public void Reset()
    {
        Clear();
        SearchLevel = RootLevel;
    }

    public Assignment<TVariable, TDomainValue>[] GetAssignments() => this.Take(SearchLevel).Select(node => node.Map()).ToArray();

    private protected abstract TNode GetNode(int variableIndex, IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp);

    private void SwapNodes(int swapLevel)
    {
        (TNode presentNode, TNode swapNode) = (this[SearchLevel], this[swapLevel]);

        (presentNode.SearchTreeLevel, swapNode.SearchTreeLevel) = (swapLevel, SearchLevel);

        (this[SearchLevel], this[swapLevel]) = (swapNode, presentNode);
    }
}
