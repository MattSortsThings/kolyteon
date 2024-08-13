using Kolyteon.Solving.Internals.Strategies.Checking.Retrospective;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Common;

internal sealed class CheckingStrategyFactory<TVariable, TDomainValue> : ICheckingStrategyFactory<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public ICheckingStrategy<TVariable, TDomainValue> Create(CheckingStrategy strategy, int capacity) =>
        new BtStrategy<TVariable, TDomainValue>(capacity);
}
