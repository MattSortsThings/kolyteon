namespace Kolyteon.Solving.Internals.Strategies.Checking.Common;

internal interface ICheckingStrategyFactory<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public ICheckingStrategy<TVariable, TDomainValue> Create(CheckingStrategy strategy, int capacity);
}
