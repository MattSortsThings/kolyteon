using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal sealed class FcStrategy<TVariable, TDomainValue> :
    CheckingStrategy<FcNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public FcStrategy(int capacity)
    {
        SearchTree = new FcTree(capacity);
    }

    public override CheckingStrategy Identifier => CheckingStrategy.ForwardChecking;

    private protected override SearchTree<FcNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree()
    {
        // Not implemented in Forward Checking.
    }

    private protected override void SetupForAssigning() => SearchTree.GetPresentNode().PopulateSuccessors(SearchTree);

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        for (int level = SearchTree.SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].ClearSuccessors();
        }
    }

    private protected override void AddSafetyCheck()
    {
        bool noNodeExhausted = true;
        FcNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; noNodeExhausted && i < presentNode.Successors.Count; i++)
        {
            ProspectiveNode<TVariable, TDomainValue> successorNode = presentNode.Successors[i];
            presentNode.Prune(successorNode);
            noNodeExhausted = !successorNode.Exhausted;
        }

        Safe = noNodeExhausted;
    }

    private protected override void UndoLastSafetyCheck() => SearchTree.GetPresentNode().UndoAllPruning();

    private sealed class FcTree : SearchTree<FcNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public FcTree(int capacity) : base(capacity)
        {
        }

        private protected override FcNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) =>
            new(binaryCsp, variableIndex);
    }
}
