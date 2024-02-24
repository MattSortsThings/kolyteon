using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving;

public sealed record Result<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public IReadOnlyList<Assignment<V, D>> Assignments { get; init; } = Array.Empty<Assignment<V, D>>();

    public Algorithm Algorithm { get; init; } = null!;

    public long SetupSteps { get; init; }

    public long VisitingSteps { get; init; }

    public long BacktrackingSteps { get; init; }

    public long TotalSteps { get; init; }
}
