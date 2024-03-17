namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Defines methods for an indexed, generic binary CSP that may be queried by a binary CSP solving algorithm in order
///     to find a solution to the binary CSP.
/// </summary>
/// <remarks>
///     In binary CSP of <i>n</i> variables, the variables are arranged in an immutable order and individually
///     referenced by their zero-based index from 0 to <i>n</i>-1. Each variable's domain is arranged in an immutable
///     order, with individual values referenced by their zero-based index.
/// </remarks>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface ISolvableBinaryCsp<V, D> : IMeasurableBinaryCsp
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Determines whether the two variables at the specified indexes are adjacent, that is, they participate in a binary
    ///     constraint.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method is typically invoked with two different variable indexes to determine whether the corresponding
    ///         variables are adjacent or not. For the sake of completeness, the method returns <c>true</c> if the arguments
    ///         are the same variable index twice (i.e. any variable is always adjacent to itself).
    ///     </para>
    ///     <para>
    ///         The two variable index arguments can be passed in either order; the method returns the same result.
    ///     </para>
    /// </remarks>
    /// <param name="indexA">The zero-based index of the first variable to be queried.</param>
    /// <param name="indexB">The zero-based index of the second variable to be queried.</param>
    /// <returns><c>true</c> if the variables at the specified indexes are adjacent; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="indexA" /> is negative, or, <paramref name="indexA" /> is greater than or equal to the value of
    ///     <see cref="IMeasurableBinaryCsp.Variables" />, or, <paramref name="indexB" /> is negative, or,
    ///     <paramref name="indexB" /> is greater than or equal to the value of <see cref="IMeasurableBinaryCsp.Variables" />
    /// </exception>
    public bool Adjacent(int indexA, int indexB);

    /// <summary>
    ///     Determines whether the two specified assignments are consistent with the binary CSP constraints, that is, they may
    ///     both form part of a potential solution to the binary CSP.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method is typically invoked with assignments for two different variables that are known to be adjacent, to
    ///         determine whether the assignments are consistent or not. For the sake of completeness:
    ///         <list type="bullet">
    ///             <item>
    ///                 If the assignment arguments are for two different <i>non-adjacent</i> variables, the method always
    ///                 returns <c>true</c> (i.e. any assignment pair for non-adjacent variables is always consistent).
    ///             </item>
    ///             <item>
    ///                 If the assignment arguments are for the <i>same</i> variable, the method <c>true</c> if the
    ///                 assignments are also for the same value in the variable's domain, and <c>false</c> if they are for
    ///                 different domain values.
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         The two assignment arguments can be passed in either order; the method returns the same result.
    ///     </para>
    /// </remarks>
    /// <param name="assignmentA">The first assignment to be queried.</param>
    /// <param name="assignmentB">The second assignment to be queried.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="assignmentA" /> and <paramref name="assignmentB" /> are consistent, or <c>false</c>
    ///     if they are inconsistent.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="assignmentA" /> or <paramref name="assignmentB" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The <paramref name="assignmentA" /> parameter's <see cref="IAssignment.VariableIndex" /> value is negative or
    ///     greater than or equal to the value of <see cref="IMeasurableBinaryCsp.Variables" />, or, its
    ///     <see cref="IAssignment.DomainValueIndex" /> value is negative or greater than or equal to the variable's domain
    ///     size, or the <paramref name="assignmentB" /> parameter's <see cref="IAssignment.VariableIndex" /> value is negative
    ///     or greater than or equal to the value of <see cref="IMeasurableBinaryCsp.Variables" />, or, its
    ///     <see cref="IAssignment.DomainValueIndex" /> value is negative or greater than or equal to the variable's domain
    ///     size
    /// </exception>
    public bool Consistent(IAssignment assignmentA, IAssignment assignmentB);

    /// <summary>
    ///     Maps the specified index-based assignment onto the binary CSP variables and domains and returns a decomposable
    ///     structure containing the mapped assignment.
    /// </summary>
    /// <param name="assignment">The index-based assignment to a variable of a value from its domain.</param>
    /// <returns>A new <see cref="Assignment{V,D}" /> instance containing the mapped assignment.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="assignment" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The <paramref name="assignment" /> parameter's <see cref="IAssignment.VariableIndex" /> value is negative or
    ///     greater than or equal to the value of <see cref="IMeasurableBinaryCsp.Variables" />, or, its
    ///     <see cref="IAssignment.DomainValueIndex" /> value is negative or greater than or equal to the variable's domain
    ///     size.
    /// </exception>
    public Assignment<V, D> Map(IAssignment assignment);

    /// <summary>
    ///     Gets the binary CSP variable at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>An instance of type <typeparamref name="V" />. The variable at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="IMeasurableBinaryCsp.Variables" />.
    /// </exception>
    public V GetVariableAt(int index);

    /// <summary>
    ///     Gets the domain of the binary CSP variable at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A read-only list of instances of type <typeparamref name="D" />. The domain of the variable at the specified index.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="IMeasurableBinaryCsp.Variables" />.
    /// </exception>
    public IReadOnlyList<D> GetDomainAt(int index);

    /// <summary>
    ///     Gets the domain size of the binary CSP variable at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A non-negative 32-bit signed integer. The domain size of the variable at the specified index.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="IMeasurableBinaryCsp.Variables" />.
    /// </exception>
    public int GetDomainSizeAt(int index);

    /// <summary>
    ///     Gets the degree of the binary CSP variable at the specified index.
    /// </summary>
    /// <remarks>
    ///     The degree of a binary CSP variable is the number of other variables in the binary CSP that are adjacent to
    ///     it, or the number of binary constraints in which the variable participates.
    /// </remarks>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A non-negative 32-bit signed integer. The degree of the variable at the specified index.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="IMeasurableBinaryCsp.Variables" />.
    /// </exception>
    public int GetDegreeAt(int index);

    /// <summary>
    ///     Gets the sum constraint tightness of the binary CSP variable at the specified index.
    /// </summary>
    /// <remarks>
    ///     The sum constraint tightness of a binary CSP variable is the sum total tightness of all the binary constraints in
    ///     which the variable participates. An individual constraint's tightness is expressed as a real number in the range
    ///     (0,1]. A variable with a degree of <i>n</i> therefore has a sum constraint tightness in the range (0,<i>n</i>]. A
    ///     variable participating in no binary constraints has a sum constraint tightness of 0.
    /// </remarks>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A non-negative double-precision floating-point number. The sum constraint tightness of the variable at the
    ///     specified index.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="IMeasurableBinaryCsp.Variables" />.
    /// </exception>
    public double GetSumTightnessAt(int index);
}
