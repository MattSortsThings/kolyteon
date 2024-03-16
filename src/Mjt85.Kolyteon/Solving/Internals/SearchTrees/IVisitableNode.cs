using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.Internals.SearchTrees;

internal interface IVisitableNode : IAssignment
{
    public int SearchTreeLevel { get; set; }

    public int RemainingCandidates { get; }

    public int Degree { get; }

    public double SumTightness { get; }

    public bool AdjacentTo(IVisitableNode other);
}
