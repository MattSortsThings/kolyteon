using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Retrospective;

internal sealed class GbjStrategy<TVariable, TDomainValue> :
    CheckingStrategy<GbjNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public GbjStrategy(int capacity)
    {
        SearchTree = new GbjTree(capacity);
    }

    public override CheckingStrategy Identifier => CheckingStrategy.GraphBasedBackjumping;

    private protected override SearchTree<GbjNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree()
    {
        // Not implemented in Graph-Based Backjumping.  
    }

    private protected override void SetupForAssigning()
    {
        GbjNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();
        presentNode.PopulateAncestors(SearchTree);
        presentNode.ResetBacktrackLevel();
    }

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        if (backtrackLevel > SearchTree.RootLevel)
        {
            SearchTree[backtrackLevel].UnionMergeBacktrackDataFrom(SearchTree.GetPresentNode());
        }

        for (int level = SearchLevel; level > backtrackLevel; level--)
        {
            GbjNode<TVariable, TDomainValue> node = SearchTree[level];
            node.ClearAncestors();
            node.ResetBacktrackLevel();
        }
    }

    private protected override void AddSafetyCheck()
    {
        bool consistent = true;
        GbjNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            consistent = presentNode.Ancestors[i].AssignmentSupports(presentNode);
        }

        Safe = consistent;
    }

    private protected override void UndoLastSafetyCheck()
    {
        // Not implemented in Graph-Based Backjumping.  
    }

    private sealed class GbjTree : SearchTree<GbjNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public GbjTree(int capacity) : base(capacity)
        {
        }

        private protected override GbjNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) =>
            new(binaryCsp, variableIndex);
    }
}
