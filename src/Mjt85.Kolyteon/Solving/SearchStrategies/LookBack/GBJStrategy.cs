using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

internal sealed class GBJStrategy<V, D> : SearchStrategy<GBJNode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public GBJStrategy(int capacity)
    {
        SearchTree = new GBJTree(capacity);
    }

    public override Search Identifier => Search.GraphBasedBackjumping;

    protected internal override SearchTree<GBJNode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new GBJNode<V, D>(binaryCsp, i));
        }
    }

    protected override void AddSafetyCheckAtRootLevel() => CheckAllNodesForEmptyDomains();

    protected override void AddSafetyCheck()
    {
        var consistent = true;
        GBJNode<V, D> presentNode = GetPresentNode();

        for (var i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            consistent = presentNode.Ancestors[i].AssignmentSupports(presentNode);
        }

        if (consistent)
        {
            SearchState = SearchState.Safe;
        }
    }

    protected override void UndoSafetyCheckPropagation()
    {
        // Not implemented in GBJ.
    }

    protected override void SetupForVisit()
    {
        GBJNode<V, D> presentNode = GetPresentNode();
        presentNode.RepopulateAncestors(SearchTree);
        presentNode.ResetBacktrackLevel();
    }

    protected override void SetupForBacktrack(in int backtrackLevel)
    {
        if (backtrackLevel > SearchTreeRootLevel)
        {
            SearchTree[backtrackLevel].UnionMergeBacktrackDataFrom(GetPresentNode());
        }

        for (var level = SearchLevel; level > backtrackLevel; level--)
        {
            GBJNode<V, D> node = SearchTree[level];
            node.Ancestors.Clear();
            node.ResetBacktrackLevel();
        }
    }

    private sealed class GBJTree : SearchTree<GBJNode<V, D>, V, D>
    {
        public GBJTree(int capacity) : base(capacity)
        {
        }
    }
}
