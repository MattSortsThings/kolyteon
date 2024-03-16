using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;

internal abstract class LookAheadNode<V, D> : SearchTreeNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    protected LookAheadNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        Successors = new List<LookAheadNode<V, D>>(Degree);
        PruningMemos = new List<PruningMemo<V, D>>(Degree);
    }

    public override int BacktrackLevel => SearchTreeLevel - 1;

    public List<LookAheadNode<V, D>> Successors { get; }

    public List<PruningMemo<V, D>> PruningMemos { get; }

    public void RepopulateSuccessors(IReadOnlyList<LookAheadNode<V, D>> searchTree)
    {
        for (var level = SearchTreeLevel + 1; level < searchTree.Count && Successors.Count < Degree; level++)
        {
            LookAheadNode<V, D> futureNode = searchTree[level];
            if (AdjacentTo(futureNode))
            {
                Successors.Add(futureNode);
            }
        }
    }

    public void Prune(LookAheadNode<V, D> operandNode)
    {
        int? firstSupportedCandidate = null;
        Queue<int> operandCandidates = operandNode.Candidates;

        while (operandCandidates.TryPeek(out var c) && c != firstSupportedCandidate)
        {
            operandNode.AssignNextCandidate();
            if (AssignmentSupports(operandNode))
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

    public void UndoAllPruning()
    {
        foreach ((LookAheadNode<V, D> prunedNode, var prunedCandidate) in PruningMemos)
        {
            prunedNode.Candidates.Enqueue(prunedCandidate);
        }

        PruningMemos.Clear();
    }
}
