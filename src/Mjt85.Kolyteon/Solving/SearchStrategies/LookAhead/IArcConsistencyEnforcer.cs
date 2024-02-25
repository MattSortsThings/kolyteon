namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal interface IArcConsistencyEnforcer<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public void ArcPrune(ArcConsistencyNode<V, D> operandNode, ArcConsistencyNode<V, D> contextNode);
}
