using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

internal sealed class CbjStrategy<TVariable, TDomainValue> :
    CheckingStrategy<CbjNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public CbjStrategy(int capacity)
    {
        SearchTree = new CbjTree(capacity);
    }

    public override CheckingStrategy Identifier => CheckingStrategy.ConflictBackjumping;

    private protected override SearchTree<CbjNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree()
    {
        // Not implemented in Conflict-Directed Backjumping.
    }

    private protected override void SetupForAssigning() => SearchTree.GetPresentNode().PopulateAncestors(SearchTree);

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        if (backtrackLevel > SearchTree.RootLevel)
        {
            SearchTree[backtrackLevel].UnionMergeBacktrackDataFrom(SearchTree.GetPresentNode());
        }

        for (int level = SearchTree.SearchLevel; level > backtrackLevel; level--)
        {
            CbjNode<TVariable, TDomainValue> node = SearchTree[level];
            node.ClearAncestors();
            node.ResetBacktrackLevel();
        }
    }

    private protected override void AddSafetyCheck()
    {
        bool consistent = true;
        CbjNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            RetrospectiveNode<TVariable, TDomainValue> ancestorNode = presentNode.Ancestors[i];
            consistent = ancestorNode.AssignmentSupports(presentNode);
            if (!consistent)
            {
                presentNode.UpdateBacktrackLevel(ancestorNode);
            }
        }

        Safe = consistent;
    }

    private protected override void UndoLastSafetyCheck()
    {
        // Not implemented in Conflict-Directed Backjumping.
    }

    private sealed class CbjTree : SearchTree<CbjNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public CbjTree(int capacity) : base(capacity)
        {
        }

        private protected override CbjNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) => new(binaryCsp, variableIndex);
    }
}
