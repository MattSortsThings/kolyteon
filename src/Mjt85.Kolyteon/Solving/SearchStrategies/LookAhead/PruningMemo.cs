namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal readonly record struct PruningMemo<V, D>(LookAheadNode<V, D> PrunedNode, int PrunedCandidate)
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>;
