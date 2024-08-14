using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal sealed class RootLevelArcPruner<TVariable, TDomainValue> : IArcPruner<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
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
                operandCandidates.Dequeue();
            }
        }

        operandNode.DomainValueIndex = SearchTreeNode<TVariable, TDomainValue>.NoAssignment;
    }
}
