using Kolyteon.Modelling;

namespace Kolyteon.Solving.Internals.SearchTrees;

internal abstract class SearchTreeNode<TVariable, TDomainValue> : IVisitableNode
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected internal const int NoAssignment = -1;
    private readonly IReadOnlyBinaryCsp<TVariable, TDomainValue> _binaryCsp;
    private double? _sumTightness;

    protected SearchTreeNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex)
    {
        _binaryCsp = binaryCsp;
        VariableIndex = variableIndex;
        SearchTreeLevel = variableIndex;
        DomainValueIndex = NoAssignment;
        Candidates = new Queue<int>(Enumerable.Range(0, binaryCsp.GetDomainSizeAt(variableIndex)));
        RejectedCandidates = new List<int>(Candidates.Count / 2);
        Degree = binaryCsp.GetDegreeAt(variableIndex);
    }

    public bool Exhausted => Candidates.Count == 0;

    public Queue<int> Candidates { get; }

    public abstract int BacktrackLevel { get; }

    private List<int> RejectedCandidates { get; }

    public int VariableIndex { get; }

    public int DomainValueIndex { get; protected internal set; }

    public int SearchTreeLevel { get; set; }

    public int RemainingCandidates => Candidates.Count;

    public int Degree { get; }

    public double SumTightness => _sumTightness ??= _binaryCsp.GetSumTightnessAt(VariableIndex);

    public bool AdjacentTo(IVisitableNode other) => _binaryCsp.Adjacent(VariableIndex, other.VariableIndex);

    public Assignment<TVariable, TDomainValue> Map() => _binaryCsp.Map(this);

    public void AssignNextCandidate() => DomainValueIndex = Candidates.Peek();

    public bool AssignmentSupports(IAssignment other) => _binaryCsp.Consistent(this, other);

    public void RejectAssignment()
    {
        RejectedCandidates.Add(Candidates.Dequeue());
        DomainValueIndex = NoAssignment;
    }

    public void RestoreRejectedCandidates()
    {
        foreach (int c in RejectedCandidates)
        {
            Candidates.Enqueue(c);
        }

        RejectedCandidates.Clear();
    }
}
