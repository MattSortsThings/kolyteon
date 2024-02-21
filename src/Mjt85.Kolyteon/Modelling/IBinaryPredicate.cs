namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Defines the binary predicate rule for the domains of two ordered binary CSP variables.
/// </summary>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface IBinaryPredicate<D>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Determines whether the values from the first and second variables' domains can be assigned simultaneously as part
    ///     of any solution to the problem.
    /// </summary>
    /// <param name="domainValue1">The value from the first variable's domain.</param>
    /// <param name="domainValue2">The value from the second variable's domain.</param>
    /// <returns>
    ///     <c>true</c> if the <paramref name="domainValue1" /> parameter may be assigned to the first variable and
    ///     simultaneously the <paramref name="domainValue2" /> parameter may be assigned to the second variable; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public bool CanAssign(in D domainValue1, in D domainValue2);
}
