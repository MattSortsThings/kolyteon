using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal abstract class ArcConsistencyNode<V, D> : LookAheadNode<V, D>, IArcConsistencyEnforcer<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    protected ArcConsistencyNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }

    public void ArcPrune(ArcConsistencyNode<V, D> operandNode, ArcConsistencyNode<V, D> contextNode)
    {
        int? firstSupportedCandidate = null;
        Queue<int> operandCandidates = operandNode.Candidates;

        while (operandCandidates.TryPeek(out var c) && c != firstSupportedCandidate)
        {
            operandNode.AssignNextCandidate();
            if (contextNode.CandidateSupports(operandNode))
            {
                firstSupportedCandidate ??= c;
                operandCandidates.Enqueue(operandCandidates.Dequeue());
            }
            else
            {
                PruningMemos.Add(new PruningMemo<V, D>(operandNode, operandCandidates.Dequeue()));
            }
        }

        operandNode.DomainValueIndex = NoAssignment;
    }

    public bool CandidateSupports(IAssignment other)
    {
        var supported = false;

        foreach (var candidate in Candidates)
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
