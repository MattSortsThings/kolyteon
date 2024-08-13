using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving.Internals.Builders;

/// <summary>
///     Fluent builder for the <see cref="BinaryCspSolver{TVariable,TDomainValue}" /> class.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
internal sealed class BinaryCspSolverBuilder<TVariable, TDomainValue> : IBinaryCspSolverBuilder<TVariable, TDomainValue>,
    IBinaryCspSolverBuilder<TVariable, TDomainValue>.ICheckingStrategySetter,
    IBinaryCspSolverBuilder<TVariable, TDomainValue>.IOrderingStrategySetter,
    IBinaryCspSolverBuilder<TVariable, TDomainValue>.ITerminal
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private int _capacity;
    private CheckingStrategy? _checkingStrategy;
    private OrderingStrategy? _orderingStrategy;

    /// <inheritdoc />
    public IBinaryCspSolverBuilder<TVariable, TDomainValue>.ICheckingStrategySetter WithCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _capacity = capacity;

        return this;
    }

    /// <inheritdoc />
    public IBinaryCspSolverBuilder<TVariable, TDomainValue>.IOrderingStrategySetter AndCheckingStrategy(
        CheckingStrategy checkingStrategy)
    {
        _checkingStrategy = checkingStrategy ?? throw new ArgumentNullException(nameof(checkingStrategy));

        return this;
    }

    /// <inheritdoc />
    public IBinaryCspSolverBuilder<TVariable, TDomainValue>.ITerminal AndOrderingStrategy(OrderingStrategy orderingStrategy)
    {
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));

        return this;
    }

    /// <inheritdoc />
    public BinaryCspSolver<TVariable, TDomainValue> Build()
    {
        CheckingStrategyFactory<TVariable, TDomainValue> checkingStrategyFactory = new();
        OrderingStrategyFactory orderingStrategyFactory = new();

        return new BinaryCspSolver<TVariable, TDomainValue>(checkingStrategyFactory,
            orderingStrategyFactory,
            checkingStrategyFactory.Create(_checkingStrategy!, _capacity),
            orderingStrategyFactory.Create(_orderingStrategy!));
    }
}
