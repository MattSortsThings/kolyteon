using System.Diagnostics.CodeAnalysis;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Retrospective;

internal sealed class BtStrategy<TVariable, TDomainValue> :
    CheckingStrategy<BtNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public BtStrategy(int capacity)
    {
        SearchTree = new BtTree(capacity);
    }

    public override CheckingStrategy Identifier => CheckingStrategy.NaiveBacktracking;

    private protected override SearchTree<BtNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree()
    {
        // Not implemented in Naive Backtracking.
    }

    private protected override void SetupForAssigning() => SearchTree.GetPresentNode().PopulateAncestors(SearchTree);

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        for (int level = SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].ClearAncestors();
        }
    }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    private protected override void AddSafetyCheck()
    {
        bool consistent = true;
        BtNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            consistent = presentNode.Ancestors[i].AssignmentSupports(presentNode);
        }

        Safe = consistent;
    }

    private protected override void UndoLastSafetyCheck()
    {
        // Not implemented in Naive Backtracking.
    }

    private sealed class BtTree : SearchTree<BtNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public BtTree(int capacity) : base(capacity)
        {
        }

        private protected override BtNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) => new(binaryCsp, variableIndex);
    }
}
