using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

internal sealed class BjStrategy<TVariable, TDomainValue> :
    CheckingStrategy<BjNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public BjStrategy(int capacity)
    {
        SearchTree = new BjTree(capacity);
    }

    public override CheckingStrategy Identifier => CheckingStrategy.Backjumping;

    private protected override SearchTree<BjNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree()
    {
        // Not implemented in Backjumping.
    }

    private protected override void SetupForAssigning() => SearchTree.GetPresentNode().PopulateAncestors(SearchTree);

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        for (int level = SearchLevel; level > backtrackLevel; level--)
        {
            BjNode<TVariable, TDomainValue> node = SearchTree[level];
            node.ClearAncestors();
            node.ResetBacktrackLevel();
        }
    }

    private protected override void AddSafetyCheck()
    {
        bool consistent = true;
        BjNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; consistent && i < presentNode.Ancestors.Count; i++)
        {
            RetrospectiveNode<TVariable, TDomainValue> ancestorNode = presentNode.Ancestors[i];
            consistent = ancestorNode.AssignmentSupports(presentNode);
            if (!consistent)
            {
                presentNode.UpdateBacktrackLevel(ancestorNode);
            }
        }

        if (consistent)
        {
            presentNode.SetBacktrackLevelToMax();
        }

        Safe = consistent;
    }

    private protected override void UndoLastSafetyCheck()
    {
        // Not implemented in Backjumping.
    }

    private sealed class BjTree : SearchTree<BjNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public BjTree(int capacity) : base(capacity)
        {
        }

        private protected override BjNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) => new(binaryCsp, variableIndex);
    }
}
