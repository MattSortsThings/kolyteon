using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookBack;

internal sealed class CBJStrategy<V, D> : SearchStrategy<CBJNode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    internal CBJStrategy(int capacity)
    {
        SearchTree = new CBJTree(capacity);
    }

    public override Search Identifier => Search.ConflictDirectedBackjumping;

    protected internal override SearchTree<CBJNode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new CBJNode<V, D>(binaryCsp, i));
        }
    }

    protected override void AddSafetyCheckAtRootLevel() => CheckAllNodesForEmptyDomains();

    protected override void AddSafetyCheck()
    {
        var consistent = true;
        CBJNode<V, D> presentNode = GetPresentNode();

        for (var i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            LookBackNode<V, D> ancestorNode = presentNode.Ancestors[i];
            consistent = ancestorNode.AssignmentSupports(presentNode);
            if (!consistent)
            {
                presentNode.UpdateBacktrackLevel(ancestorNode);
            }
        }

        if (consistent)
        {
            SearchState = SearchState.Safe;
        }
    }

    protected override void UndoSafetyCheckPropagation()
    {
        // Not implemented in CBJ.
    }

    protected override void SetupForVisit() => GetPresentNode().RepopulateAncestors(SearchTree);

    protected override void SetupForBacktrack(in int backtrackLevel)
    {
        if (backtrackLevel > SearchTreeRootLevel)
        {
            SearchTree[backtrackLevel].UnionMergeBacktrackDataFrom(GetPresentNode());
        }

        for (var level = SearchLevel; level > backtrackLevel; level--)
        {
            CBJNode<V, D> node = SearchTree[level];
            node.Ancestors.Clear();
            node.ResetBacktrackLevel();
        }
    }

    private sealed class CBJTree : SearchTree<CBJNode<V, D>, V, D>
    {
        public CBJTree(int capacity) : base(capacity)
        {
        }
    }
}
