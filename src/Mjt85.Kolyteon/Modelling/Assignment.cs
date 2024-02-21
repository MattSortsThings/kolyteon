namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Represents the assignment to a binary CSP variable of a value from its domain.
/// </summary>
/// <param name="Variable">The binary CSP variable.</param>
/// <param name="DomainValue">The domain value assigned to the variable.</param>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public readonly record struct Assignment<V, D>(V Variable, D DomainValue)
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>;
