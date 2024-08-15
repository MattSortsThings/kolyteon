namespace Kolyteon.Solving;

/// <summary>
///     Fluent builder for the <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> class.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public interface ISilentBinaryCspSolverBuilder<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Sets the initial <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}.Capacity" /> of the new
    ///     <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> instance.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The number of binary CSP variables that the binary CSP solver's internal data structures
    ///     can accommodate without resizing.
    /// </param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public ICheckingStrategySetter WithCapacity(int capacity);

    /// <summary>
    ///     Fluent builder for the <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> class.
    /// </summary>
    public interface ICheckingStrategySetter
    {
        /// <summary>
        ///     Sets the initial <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}.CheckingStrategy" /> of the new
        ///     <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> instance.
        /// </summary>
        /// <param name="checkingStrategy">
        ///     Specifies the initial checking strategy component of the backtracking search algorithm used by the binary CSP
        ///     solver.
        /// </param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="checkingStrategy" /> is <see langword="null" />.</exception>
        public IOrderingStrategySetter AndCheckingStrategy(CheckingStrategy checkingStrategy);
    }

    /// <summary>
    ///     Fluent builder for the <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> class.
    /// </summary>
    public interface IOrderingStrategySetter
    {
        /// <summary>
        ///     Sets the initial <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}.OrderingStrategy" /> of the new
        ///     <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> instance.
        /// </summary>
        /// <param name="orderingStrategy">
        ///     Specifies the initial ordering strategy component of the backtracking search algorithm used by the binary CSP
        ///     solver.
        /// </param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderingStrategy" /> is <see langword="null" />.</exception>
        public ITerminal AndOrderingStrategy(OrderingStrategy orderingStrategy);
    }

    /// <summary>
    ///     Fluent builder for the <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> class.
    /// </summary>
    public interface ITerminal
    {
        /// <summary>
        ///     Creates and returns a new <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> instance as specified by all
        ///     the previous fluent builder method invocations.
        /// </summary>
        /// <returns>A new <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> instance.</returns>
        public SilentBinaryCspSolver<TVariable, TDomainValue> Build();
    }
}
