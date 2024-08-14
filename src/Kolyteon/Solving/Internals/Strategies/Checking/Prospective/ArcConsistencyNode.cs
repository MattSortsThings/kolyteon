using Kolyteon.Modelling;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal abstract class ArcConsistencyNode<TVariable, TDomainValue> : ProspectiveNode<TVariable, TDomainValue>,
    IArcPruner<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected ArcConsistencyNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) : base(binaryCsp,
        variableIndex)
    {
    }

    public void ArcPrune(ArcConsistencyNode<TVariable, TDomainValue> operandNode,
        ArcConsistencyNode<TVariable, TDomainValue> contextNode)
    {
        int? firstSupportedCandidate = null;
        Queue<int> operandCandidates = operandNode.Candidates;

        while (operandCandidates.TryPeek(out int c) && c != firstSupportedCandidate)
        {
            operandNode.AssignNextCandidate();
            if (contextNode.CandidateSupports(operandNode))
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

    public bool CandidateSupports(IAssignment other)
    {
        bool supported = false;

        foreach (int candidate in Candidates)
        {
            DomainValueIndex = candidate;
            supported = AssignmentSupports(other);

            if (supported)
            {
                break;
            }
        }

        DomainValueIndex = NoAssignment;

        return supported;
    }
}
