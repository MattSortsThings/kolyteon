using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Verbose;

public interface IVerboseBinaryCspSolver<V, D> : IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public TimeSpan StepDelay { get; set; }

    public Task<Result<V, D>> SolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken = default);
}
