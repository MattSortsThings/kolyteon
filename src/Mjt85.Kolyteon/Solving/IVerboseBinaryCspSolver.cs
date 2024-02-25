using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving;

public interface IVerboseBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public TimeSpan StepDelay { get; set; }

    public Search SearchStrategy { get; set; }

    public Ordering OrderingStrategy { get; set; }

    public Task<Result<V, D>> SolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken = default);
}
