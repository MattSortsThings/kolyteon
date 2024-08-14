namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal interface IArcPruner<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public void ArcPrune(ArcConsistencyNode<TVariable, TDomainValue> operandNode,
        ArcConsistencyNode<TVariable, TDomainValue> contextNode);
}
