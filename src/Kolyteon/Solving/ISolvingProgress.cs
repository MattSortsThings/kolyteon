namespace Kolyteon.Solving;

/// <summary>
///     A reusable provider for verbose binary CSP solving operation updates.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public interface ISolvingProgress<TVariable, TDomainValue> : IProgress<SolvingStepDatum<TVariable, TDomainValue>>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Resets the provider to an empty state with the specified search tree leaf level, reflecting the state of the
    ///     verbose solver immediately after the solving operation has been set up but before the first step has been executed.
    /// </summary>
    /// <param name="searchTreeLeafLevel">The search tree leaf level.</param>
    public void Reset(int searchTreeLeafLevel);

    /// <summary>
    ///     Resets the provider to an empty state.
    /// </summary>
    public void Reset();
}
