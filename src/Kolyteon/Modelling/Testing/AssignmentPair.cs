namespace Kolyteon.Modelling.Testing;

/// <summary>
///     Represents a pair of assignments from the domains of two ordered binary CSP variables that participate in a binary
///     constraint.
/// </summary>
/// <param name="FirstDomainValue">The first variable's assigned domain value.</param>
/// <param name="SecondDomainValue">The second variable's assigned domain value.</param>
/// <param name="Consistent">A boolean value indicating whether the assignment pair is consistent with the constraint.</param>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public readonly record struct AssignmentPair<TDomainValue>(
    TDomainValue FirstDomainValue,
    TDomainValue SecondDomainValue,
    bool Consistent)
    where TDomainValue : struct, IEquatable<TDomainValue>;
