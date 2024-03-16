using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;

internal sealed class FCNode<V, D> : LookAheadNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public FCNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }
}
