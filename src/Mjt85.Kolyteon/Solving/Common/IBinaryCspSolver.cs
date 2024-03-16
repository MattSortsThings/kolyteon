namespace Mjt85.Kolyteon.Solving.Common;

public interface IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public Search SearchStrategy { get; set; }

    public Ordering OrderingStrategy { get; set; }
}
