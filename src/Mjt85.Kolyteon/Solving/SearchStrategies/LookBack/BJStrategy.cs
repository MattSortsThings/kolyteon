using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

internal sealed class BJStrategy<V, D> : SearchStrategy<BJNode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public BJStrategy(int capacity)
    {
        SearchTree = new BJTree(capacity);
    }

    public override Search Identifier => Search.Backjumping;

    protected internal override SearchTree<BJNode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new BJNode<V, D>(binaryCsp, i));
        }
    }

    protected override void AddSafetyCheckAtRootLevel() => CheckAllNodesForEmptyDomains();

    protected override void AddSafetyCheck()
    {
        var consistent = true;
        BJNode<V, D> presentNode = GetPresentNode();

        for (var i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            LookBackNode<V, D> ancestorNode = presentNode.Ancestors[i];
            consistent = ancestorNode.AssignmentSupports(presentNode);
            if (!consistent)
            {
                presentNode.UpdateBacktrackLevel(ancestorNode);
            }
        }

        if (!consistent)
        {
            return;
        }

        presentNode.SetBacktrackLevelToMax();
        SearchState = SearchState.Safe;
    }

    protected override void UndoSafetyCheckPropagation()
    {
        // Not implemented in BJ.
    }

    protected override void SetupForVisit() => GetPresentNode().RepopulateAncestors(SearchTree);

    protected override void SetupForBacktrack(in int backtrackLevel)
    {
        for (var level = SearchLevel; level > backtrackLevel; level--)
        {
            BJNode<V, D> node = SearchTree[level];
            node.Ancestors.Clear();
            node.ResetBacktrackLevel();
        }
    }

    private sealed class BJTree : SearchTree<BJNode<V, D>, V, D>
    {
        public BJTree(int capacity) : base(capacity)
        {
        }
    }
}
