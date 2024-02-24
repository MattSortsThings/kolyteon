using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

internal class BTNode<V, D> : LookBackNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public BTNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }

    public override int BacktrackLevel => SearchTreeLevel - 1;
}
