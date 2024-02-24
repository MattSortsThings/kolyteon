using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.SearchTrees;

internal abstract class SearchTreeNode<V, D> : IVisitableNode
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    protected const int RootLevel = -1;
    protected internal const int NoAssignment = -1;
    private readonly ISolvableBinaryCsp<V, D> _binaryCsp;
    private double? _sumTightness;

    protected SearchTreeNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex)
    {
        _binaryCsp = binaryCsp;
        VariableIndex = variableIndex;
        SearchTreeLevel = variableIndex;
        DomainValueIndex = NoAssignment;
        Candidates = new Queue<int>(Enumerable.Range(0, binaryCsp.GetDomainSizeAt(variableIndex)));
        RejectedCandidates = new List<int>(Candidates.Count / 2);
        Degree = binaryCsp.GetDegreeAt(variableIndex);
    }

    public Queue<int> Candidates { get; }

    public List<int> RejectedCandidates { get; }

    public abstract int BacktrackLevel { get; }

    public int VariableIndex { get; }

    public int DomainValueIndex { get; protected internal set; }

    public int SearchTreeLevel { get; set; }

    public int RemainingCandidates => Candidates.Count;

    public int Degree { get; }

    public double SumTightness => _sumTightness ??= _binaryCsp.GetSumTightnessAt(VariableIndex);

    public bool AdjacentTo(IVisitableNode other) => _binaryCsp.Adjacent(VariableIndex, other.VariableIndex);

    public Assignment<V, D> Map() => _binaryCsp.Map(this);

    public void AssignNextCandidate() => DomainValueIndex = Candidates.Peek();

    public bool AssignmentSupports(IAssignment other) => _binaryCsp.Consistent(this, other);

    public void RejectAssignment()
    {
        RejectedCandidates.Add(Candidates.Dequeue());
        DomainValueIndex = NoAssignment;
    }

    public void RestoreRejectedCandidates()
    {
        foreach (var c in RejectedCandidates)
        {
            Candidates.Enqueue(c);
        }

        RejectedCandidates.Clear();
    }
}
