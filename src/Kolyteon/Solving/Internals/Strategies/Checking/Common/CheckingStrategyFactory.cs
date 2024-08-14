using Kolyteon.Solving.Internals.Strategies.Checking.Retrospective;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Common;

internal sealed class CheckingStrategyFactory<TVariable, TDomainValue> : ICheckingStrategyFactory<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private static readonly Dictionary<CheckingStrategy, Func<int, ICheckingStrategy<TVariable, TDomainValue>>> Lookup =
        new()
        {
            [CheckingStrategy.NaiveBacktracking] = capacity => new BtStrategy<TVariable, TDomainValue>(capacity),
            [CheckingStrategy.Backjumping] = capacity => new BjStrategy<TVariable, TDomainValue>(capacity)
        };

    public ICheckingStrategy<TVariable, TDomainValue> Create(CheckingStrategy strategy, int capacity) =>
        Lookup[strategy].Invoke(capacity);
}
