using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Verbose;

public readonly record struct StepNotification<V, D>(
    StepType StepType,
    SearchState CurrentSearchState,
    int CurrentSearchLevel,
    int SearchTreeLeafLevel,
    Assignment<V, D>? LatestAssignment = null)
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>;
