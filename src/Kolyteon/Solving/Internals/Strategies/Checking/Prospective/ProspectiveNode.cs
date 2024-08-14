using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal abstract class ProspectiveNode<TVariable, TDomainValue> : SearchTreeNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected ProspectiveNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) :
        base(binaryCsp, variableIndex)
    {
        Successors = new List<ProspectiveNode<TVariable, TDomainValue>>(Degree);
        PruningMemos = new List<PruningMemo<TVariable, TDomainValue>>(Degree);
    }

    public override int BacktrackLevel => SearchTreeLevel - 1;

    public List<ProspectiveNode<TVariable, TDomainValue>> Successors { get; }

    private protected List<PruningMemo<TVariable, TDomainValue>> PruningMemos { get; }

    public void PopulateSuccessors(IReadOnlyList<ProspectiveNode<TVariable, TDomainValue>> searchTree)
    {
        for (int level = SearchTreeLevel + 1; level < searchTree.Count && Successors.Count < Degree; level++)
        {
            ProspectiveNode<TVariable, TDomainValue> futureNode = searchTree[level];
            if (AdjacentTo(futureNode))
            {
                Successors.Add(futureNode);
            }
        }
    }

    public void ClearSuccessors() => Successors.Clear();

    public void Prune(ProspectiveNode<TVariable, TDomainValue> operandNode)
    {
        int? firstSupportedCandidate = null;
        Queue<int> operandCandidates = operandNode.Candidates;

        while (operandCandidates.TryPeek(out int c) && c != firstSupportedCandidate)
        {
            operandNode.AssignNextCandidate();
            if (AssignmentSupports(operandNode))
            {
                firstSupportedCandidate ??= c;
                operandCandidates.Enqueue(operandCandidates.Dequeue());
            }
            else
            {
                PruningMemos.Add(new PruningMemo<TVariable, TDomainValue>(operandNode, operandCandidates.Dequeue()));
            }
        }

        operandNode.DomainValueIndex = NoAssignment;
    }

    public void UndoAllPruning()
    {
        foreach ((ProspectiveNode<TVariable, TDomainValue> prunedNode, int prunedCandidate) in PruningMemos)
        {
            prunedNode.Candidates.Enqueue(prunedCandidate);
        }

        PruningMemos.Clear();
    }
}
