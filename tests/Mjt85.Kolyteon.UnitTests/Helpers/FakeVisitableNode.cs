using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

internal sealed record FakeVisitableNode : IVisitableNode
{
    public HashSet<int> AdjacentVariableIndexes { get; init; } = [];

    public int VariableIndex { get; init; }

    public int DomainValueIndex { get; init; } = -1;

    public int SearchTreeLevel { get; set; }

    public int RemainingCandidates { get; init; }

    public int Degree => AdjacentVariableIndexes.Count;

    public double SumTightness { get; init; }

    public bool AdjacentTo(IVisitableNode other) =>
        AdjacentVariableIndexes.Contains(other.VariableIndex) || ReferenceEquals(this, other);
}
