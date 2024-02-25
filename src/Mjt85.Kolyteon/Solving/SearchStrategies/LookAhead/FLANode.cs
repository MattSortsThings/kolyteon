using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal sealed class FLANode<V, D> : ArcConsistencyNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public FLANode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }
}
