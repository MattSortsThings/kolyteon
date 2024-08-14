using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Common;

internal abstract class CheckingStrategy<TNode, TVariable, TDomainValue> : ICheckingStrategy<TVariable, TDomainValue>
    where TNode : SearchTreeNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private protected abstract SearchTree<TNode, TVariable, TDomainValue> SearchTree { get; }

    public abstract CheckingStrategy Identifier { get; }

    public int Capacity
    {
        get => SearchTree.Capacity;
        set => SearchTree.Capacity = value;
    }

    public bool Safe { get; private protected set; }

    public int RootLevel => SearchTree.RootLevel;

    public int LeafLevel => SearchTree.LeafLevel;

    public int SearchLevel => SearchTree.SearchLevel;

    public void Simplify()
    {
        ReduceSearchTree();
        Safe = AllNodesHaveAtLeastOneCandidate();
    }

    public void TryAssign()
    {
        Safe = false;
        TNode presentNode = SearchTree.GetPresentNode();

        while (!Safe && presentNode.RemainingCandidates > 0)
        {
            presentNode.AssignNextCandidate();
            AddSafetyCheck();

            if (Safe)
            {
                continue;
            }

            presentNode.RejectAssignment();
            UndoLastSafetyCheck();
        }
    }

    public void Backtrack()
    {
        TNode presentNode = SearchTree.GetPresentNode();
        presentNode.RestoreRejectedCandidates();
        int backtrackLevel = presentNode.BacktrackLevel;

        SetupForBacktracking(backtrackLevel);

        SearchTree.SearchLevel--;

        while (SearchTree.SearchLevel > backtrackLevel)
        {
            UndoAllWorkAtInterveningLevel();
            SearchTree.SearchLevel--;
        }

        if (SearchTree.SearchLevel > RootLevel)
        {
            presentNode = SearchTree.GetPresentNode();
            presentNode.RejectAssignment();
            UndoLastSafetyCheck();
            Safe = presentNode.RemainingCandidates > 0;
        }
        else
        {
            Safe = false;
        }
    }

    public void Optimize(IOrderingStrategy orderingStrategy)
    {
        SearchTree.Reorder(orderingStrategy);
        SetupForAssigning();
    }

    public void Advance() => SearchTree.SearchLevel++;

    public void Populate(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) => SearchTree.Populate(binaryCsp);

    public void Reset()
    {
        SearchTree.Reset();
        Safe = true;
    }

    public Assignment<TVariable, TDomainValue>[] GetAllAssignments() => SearchTree.GetAssignments();

    private protected abstract void ReduceSearchTree();

    private protected abstract void SetupForAssigning();

    private protected abstract void SetupForBacktracking(int backtrackLevel);

    private protected abstract void AddSafetyCheck();

    private protected abstract void UndoLastSafetyCheck();

    private void UndoAllWorkAtInterveningLevel()
    {
        TNode presentNode = SearchTree.GetPresentNode();
        presentNode.RejectAssignment();
        UndoLastSafetyCheck();
        presentNode.RestoreRejectedCandidates();
    }

    private bool AllNodesHaveAtLeastOneCandidate() => SearchTree.All(node => node.RemainingCandidates > 0);
}
