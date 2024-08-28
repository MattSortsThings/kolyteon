using Kolyteon.Modelling;

namespace Kolyteon.Solving;

public interface IVerboseBinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets or sets the checking strategy component of the backtracking search algorithm used by this instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="CheckingStrategy" /> is set while a solving operation is in progress.
    /// </exception>
    public CheckingStrategy CheckingStrategy { get; set; }

    /// <summary>
    ///     Gets or sets the ordering strategy component of the backtracking search algorithm used by this instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="OrderingStrategy" /> is set while a solving operation is in progress.
    /// </exception>
    public OrderingStrategy OrderingStrategy { get; set; }

    /// <summary>
    ///     Gets or sets the delay between each step of the backtracking search algorithm.
    /// </summary>
    public TimeSpan StepDelay { get; set; }

    public Task<SolvingResult<TVariable, TDomainValue>> SolveAsync(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        ISolvingProgress<TVariable, TDomainValue> progress,
        CancellationToken cancellationToken = default);
}
