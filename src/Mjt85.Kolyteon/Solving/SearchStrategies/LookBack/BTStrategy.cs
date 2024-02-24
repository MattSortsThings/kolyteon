using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

internal sealed class BTStrategy<V, D> : SearchStrategy<BTNode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public BTStrategy(int capacity)
    {
        SearchTree = new BTTree(capacity);
    }

    public override Search Identifier => Search.Backtracking;

    protected internal override SearchTree<BTNode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new BTNode<V, D>(binaryCsp, i));
        }
    }

    protected override void AddSafetyCheckAtRootLevel() => CheckAllNodesForEmptyDomains();

    protected override void AddSafetyCheck()
    {
        var consistent = true;
        BTNode<V, D> presentNode = GetPresentNode();

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
        // Not implemented in BT.
    }

    protected override void SetupForVisit() => GetPresentNode().RepopulateAncestors(SearchTree);

    protected override void SetupForBacktrack(in int backtrackLevel)
    {
        for (var level = SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].Ancestors.Clear();
        }
    }

    private sealed class BTTree : SearchTree<BTNode<V, D>, V, D>
    {
        public BTTree(int capacity) : base(capacity)
        {
        }
    }
}
