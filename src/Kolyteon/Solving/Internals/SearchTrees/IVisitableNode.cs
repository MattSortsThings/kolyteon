using Kolyteon.Modelling;

namespace Kolyteon.Solving.Internals.SearchTrees;

internal interface IVisitableNode : IAssignment
{
    public int SearchTreeLevel { get; set; }

    public int RemainingCandidates { get; }

    public int Degree { get; }

    public double SumTightness { get; }

    public bool AdjacentTo(IVisitableNode other);
}
