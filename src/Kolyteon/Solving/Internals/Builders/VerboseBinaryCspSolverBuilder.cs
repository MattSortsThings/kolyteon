using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving.Internals.Builders;

/// <summary>
///     Fluent builder for the <see cref="VerboseBinaryCspSolver{TVariable,TDomainValue}" /> class.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
internal sealed class VerboseBinaryCspSolverBuilder<TVariable, TDomainValue> :
    IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>,
    IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.ICheckingStrategySetter,
    IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.IOrderingStrategySetter,
    IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.IStepDelaySetter,
    IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.ITerminal
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private int _capacity;
    private CheckingStrategy? _checkingStrategy;
    private OrderingStrategy? _orderingStrategy;
    private TimeSpan _stepDelay;

    /// <inheritdoc />
    public IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.ICheckingStrategySetter WithCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _capacity = capacity;

        return this;
    }

    /// <inheritdoc />
    public IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.IOrderingStrategySetter AndCheckingStrategy(
        CheckingStrategy checkingStrategy)
    {
        _checkingStrategy = checkingStrategy ?? throw new ArgumentNullException(nameof(checkingStrategy));

        return this;
    }

    /// <inheritdoc />
    public IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.IStepDelaySetter AndOrderingStrategy(
        OrderingStrategy orderingStrategy)
    {
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));

        return this;
    }

    /// <inheritdoc />
    public IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue>.ITerminal AndStepDelay(TimeSpan stepDelay)
    {
        _stepDelay = stepDelay;

        return this;
    }

    /// <inheritdoc />
    public VerboseBinaryCspSolver<TVariable, TDomainValue> Build()
    {
        CheckingStrategyFactory<TVariable, TDomainValue> checkingStrategyFactory = new();
        OrderingStrategyFactory orderingStrategyFactory = new();

        return new VerboseBinaryCspSolver<TVariable, TDomainValue>(checkingStrategyFactory,
            orderingStrategyFactory,
            checkingStrategyFactory.Create(_checkingStrategy!, _capacity),
            orderingStrategyFactory.Create(_orderingStrategy!),
            _stepDelay);
    }
}
