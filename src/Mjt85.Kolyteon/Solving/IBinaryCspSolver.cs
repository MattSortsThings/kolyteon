using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving;

public interface IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public Search SearchStrategy { get; set; }

    public Ordering OrderingStrategy { get; set; }

    public Result<V, D> Solve(ISolvableBinaryCsp<V, D> binaryCsp, CancellationToken cancellationToken = default);
}
