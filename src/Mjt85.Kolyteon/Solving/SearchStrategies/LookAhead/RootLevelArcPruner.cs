using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal sealed class RootLevelArcPruner<V, D> : IArcConsistencyEnforcer<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
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
                operandCandidates.Dequeue();
            }
        }

        operandNode.DomainValueIndex = SearchTreeNode<V, D>.NoAssignment;
    }
}
