namespace Kolyteon.Modelling;

/// <summary>
///     A read-only, thread-safe, index-accessible data structure representing a generic binary CSP modelling a problem.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public interface IReadOnlyBinaryCsp<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets the number of variables in the binary CSP.
    /// </summary>
    public int Variables { get; }

    /// <summary>
    ///     Gets the number of binary constraints in the binary CSP.
    /// </summary>
    public int Constraints { get; }

    /// <summary>
    ///     Gets the constraint density of the binary CSP, expressed as a real number in the range [0,1].
    /// </summary>
    /// <remarks>
    ///     The constraint density of a binary CSP is the probability that any two different variables participate in a
    ///     binary constraint. It is calculated by taking the ratio of the actual value of the <see cref="Constraints" />
    ///     property to the maximum possible <see cref="Constraints" /> value given the value of the <see cref="Variables" />
    ///     property.
    /// </remarks>
    public double ConstraintDensity { get; }

    /// <summary>
    ///     Gets the harmonic mean tightness of all the binary constraints in the binary CSP, expressed as a real number in the
    ///     range [0,1].
    /// </summary>
    public double MeanTightness { get; }

    /// <summary>
    ///     Determines whether the two variables at the specified indexes are adjacent, that is, they participate in a binary
    ///     constraint.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method is typically invoked with two different variable indexes to determine whether the corresponding
    ///         variables are adjacent or not. For the sake of completeness, the method returns <see langword="true" /> if the
    ///         arguments are the same variable index twice (i.e. any variable is always adjacent to itself).
    ///     </para>
    ///     <para>
    ///         The two variable index arguments can be passed in either order; the method returns the same result.
    ///     </para>
    /// </remarks>
    /// <param name="indexA">The zero-based index of the first variable to be queried.</param>
    /// <param name="indexB">The zero-based index of the second variable to be queried.</param>
    /// <returns>
    ///     <see langword="true" /> if the variables at the specified indexes are adjacent; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="indexA" /> is negative, or, <paramref name="indexA" /> is greater than or equal to the value of
    ///     <see cref="Variables" />, or, <paramref name="indexB" /> is negative, or, <paramref name="indexB" /> is greater
    ///     than or equal to the value of <see cref="Variables" />
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
    ///                 returns <see langword="true" /> (i.e. any assignment pair for non-adjacent variables is always
    ///                 consistent).
    ///             </item>
    ///             <item>
    ///                 If the assignment arguments are for the <i>same</i> variable, the method <see langword="true" /> if the
    ///                 assignments are also for the same value in the variable's domain, and <see langword="false" /> if they
    ///                 are for different domain values.
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
    ///     <see langword="true" /> if <paramref name="assignmentA" /> and <paramref name="assignmentB" /> are consistent, or
    ///     <see langword="false" /> if they are inconsistent.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="assignmentA" /> or <paramref name="assignmentB" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     The <see cref="IAssignment.VariableIndex" /> value of the <paramref name="assignmentA" /> parameter or the
    ///     <paramref name="assignmentB" /> parameter is negative or greater than or equal to the value of
    ///     <see cref="Variables" />.
    /// </exception>
    /// <exception cref="DomainValueIndexOutOfRangeException">
    ///     The <see cref="IAssignment.DomainValueIndex" /> value of the <paramref name="assignmentA" /> parameter or the
    ///     <paramref name="assignmentB" /> parameter is negative or greater than or equal to the number of values in the
    ///     corresponding variable's domain.
    /// </exception>
    public bool Consistent(IAssignment assignmentA, IAssignment assignmentB);

    /// <summary>
    ///     Gets the binary CSP variable at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>An instance of type <typeparamref name="TVariable" />. The variable at the specified index.</returns>
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="Variables" />.
    /// </exception>
    public TVariable GetVariableAt(int index);

    /// <summary>
    ///     Gets the domain of the binary CSP variable at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A read-only list of instances of type <typeparamref name="TDomainValue" />. The domain of the variable at the
    ///     specified index.
    /// </returns>
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="Variables" />.
    /// </exception>
    public IReadOnlyList<TDomainValue> GetDomainAt(int index);

    /// <summary>
    ///     Gets the domain size of the binary CSP variable at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A non-negative 32-bit signed integer. The domain size of the variable at the specified index.
    /// </returns>
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="Variables" />.
    /// </exception>
    public int GetDomainSizeAt(int index);

    /// <summary>
    ///     Gets the degree of the binary CSP variable at the specified index.
    /// </summary>
    /// <remarks>
    ///     The degree of a binary CSP variable is the number of other variables in the binary CSP that are adjacent to
    ///     it, or in other words the number of binary constraints in which the variable participates.
    /// </remarks>
    /// <param name="index">The zero-based index of the variable to be queried.</param>
    /// <returns>
    ///     A non-negative 32-bit signed integer. The degree of the variable at the specified index.
    /// </returns>
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="Variables" />.
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
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     <paramref name="index" /> is negative, or, <paramref name="index" /> is greater than or equal to the value of
    ///     <see cref="Variables" />.
    /// </exception>
    public double GetSumTightnessAt(int index);

    /// <summary>
    ///     Returns a data structure containing the specified index-based assignment mapped onto the binary CSP variables and
    ///     domains.
    /// </summary>
    /// <remarks>
    ///     Given an <paramref name="assignment" /> parameter with a <see cref="IAssignment.VariableIndex" /> value of
    ///     <i>x</i> and a <see cref="IAssignment.DomainValueIndex" /> value of <i>y</i>, the method returns an
    ///     <see cref="Assignment{TVariable,TDomainValue}" /> instance in which the
    ///     <see cref="Assignment{TVariable,TDomainValue}.Variable" /> value is the binary CSP variable at index <i>x</i> and
    ///     the <see cref="Assignment{TVariable,TDomainValue}.DomainValue" /> value is the value at index <i>y</i> in the
    ///     variable's domain.
    /// </remarks>
    /// <param name="assignment">The assignment to be mapped.</param>
    /// <returns>A new <see cref="Assignment{TVariable,TDomainValue}" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="assignment" /> is <see langword="null" />.</exception>
    /// <exception cref="VariableIndexOutOfRangeException">
    ///     The <see cref="IAssignment.VariableIndex" /> value of the <paramref name="assignment" /> parameter is negative or
    ///     greater than or equal to the value of <see cref="Variables" />.
    /// </exception>
    /// <exception cref="DomainValueIndexOutOfRangeException">
    ///     The <see cref="IAssignment.DomainValueIndex" /> value of the <paramref name="assignment" /> parameter is negative
    ///     or greater than or equal to the number of values in the corresponding variable's domain.
    /// </exception>
    public Assignment<TVariable, TDomainValue> Map(IAssignment assignment);
}
