using Kolyteon.Modelling;

namespace Kolyteon.Solving;

/// <summary>
///     Contains information about an executed step in a binary CSP solving operation.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
/// <param name="StepType">The type of the executed step.</param>
/// <param name="SearchLevel">The solving operation's level in the search tree at the end of the step.</param>
/// <param name="SolvingState">The solver's state at the end of the step.</param>
/// <param name="Assignment">The assignment made during the step, if any.</param>
public readonly record struct SolvingStepDatum<TVariable, TDomainValue>(
    SolvingStepType StepType,
    int SearchLevel,
    SolvingState SolvingState,
    Assignment<TVariable, TDomainValue>? Assignment = null)
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>;
