using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal sealed class FCStrategy<V, D> : SearchStrategy<FCNode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public FCStrategy(int capacity)
    {
        SearchTree = new FCTree(capacity);
    }

    public override Search Identifier => Search.ForwardChecking;

    protected internal override SearchTree<FCNode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new FCNode<V, D>(binaryCsp, i));
        }
    }

    protected override void AddSafetyCheckAtRootLevel() => CheckAllNodesForEmptyDomains();

    protected override void AddSafetyCheck()
    {
        var noNodeExhausted = true;
        FCNode<V, D> presentNode = GetPresentNode();

        for (var i = 0; noNodeExhausted && i < presentNode.Successors.Count; i++)
        {
            LookAheadNode<V, D> successorNode = presentNode.Successors[i];
            presentNode.Prune(successorNode);
            noNodeExhausted = successorNode.RemainingCandidates > 0;
        }

        if (noNodeExhausted)
        {
            SearchState = SearchState.Safe;
        }
    }

    protected override void UndoSafetyCheckPropagation() => GetPresentNode().UndoAllPruning();

    protected override void SetupForVisit() => GetPresentNode().RepopulateSuccessors(SearchTree);

    protected override void SetupForBacktrack(in int backtrackLevel)
    {
        for (var level = SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].Successors.Clear();
        }
    }

    private sealed class FCTree : SearchTree<FCNode<V, D>, V, D>
    {
        public FCTree(int capacity) : base(capacity)
        {
        }
    }
}
