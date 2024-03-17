using Mjt85.Kolyteon.Solving.Silent;
using Mjt85.Kolyteon.Solving.Verbose;

namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Fluent builder for the silent and verbose generic binary CSP solvers.
/// </summary>
public interface IBinaryCspSolverBuilder
{
    /// <summary>
    ///     Fluent builder for the silent and verbose generic binary CSP solvers.
    /// </summary>
    public interface ISearchStrategySetter
    {
        /// <summary>
        ///     Sets the initial search strategy component of the binary CSP solving algorithm used by the solver.
        /// </summary>
        /// <param name="strategy">Specifies the search strategy component of the binary CSP solving algorithm.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IOrderingStrategySetter AndInitialSearchStrategy(Search strategy);
    }

    /// <summary>
    ///     Fluent builder for the silent and verbose generic binary CSP solvers.
    /// </summary>
    public interface IOrderingStrategySetter
    {
        /// <summary>
        ///     Sets the initial ordering strategy component of the binary CSP solving algorithm used by the solver.
        /// </summary>
        /// <param name="strategy">Specifies the ordering strategy component of the binary CSP solving algorithm.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IModeSetter AndInitialOrderingStrategy(Ordering strategy);
    }

    /// <summary>
    ///     Fluent builder for the silent and verbose generic binary CSP solvers.
    /// </summary>
    public interface IModeSetter
    {
        /// <summary>
        ///     Specifies that the silent (synchronous) binary CSP solver is to be created.
        /// </summary>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public ISilentTerminal Silent();

        /// <summary>
        ///     Specifies that the verbose (asynchronous) binary CSP solver is to be created.
        /// </summary>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IVerboseStepDelaySetter Verbose();
    }

    /// <summary>
    ///     Fluent builder for the verbose generic binary CSP solvers.
    /// </summary>
    public interface IVerboseStepDelaySetter
    {
        /// <summary>
        ///     Sets the initial time delay to be inserted between binary CSP solving algorithm steps of the verbose solver.
        /// </summary>
        /// <param name="delay">The time delay inserted between binary CSP solving algorithm steps.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IVerboseTerminal WithInitialStepDelay(TimeSpan delay);
    }

    /// <summary>
    ///     Fluent builder for the silent generic binary CSP solvers.
    /// </summary>
    public interface ISilentTerminal
    {
        /// <summary>
        ///     Creates and returns a new <see cref="SilentBinaryCspSolver{V,D}" /> instance matching all the previous fluent
        ///     builder method invocations.
        /// </summary>
        /// <typeparam name="V">The binary CSP variable type.</typeparam>
        /// <typeparam name="D">The binary CSP domain value type.</typeparam>
        /// <returns>A new <see cref="SilentBinaryCspSolver{V,D}" /> instance.</returns>
        public SilentBinaryCspSolver<V, D> Build<V, D>()
            where V : struct, IComparable<V>, IEquatable<V>
            where D : struct, IComparable<D>, IEquatable<D>;
    }

    /// <summary>
    ///     Fluent builder for the verbose generic binary CSP solvers.
    /// </summary>
    public interface IVerboseTerminal
    {
        /// <summary>
        ///     Creates and returns a new <see cref="VerboseBinaryCspSolver{V,D}" /> instance matching all the previous fluent
        ///     builder method invocations.
        /// </summary>
        /// <typeparam name="V">The binary CSP variable type.</typeparam>
        /// <typeparam name="D">The binary CSP domain value type.</typeparam>
        /// <returns>A new <see cref="VerboseBinaryCspSolver{V,D}" /> instance.</returns>
        public VerboseBinaryCspSolver<V, D> Build<V, D>()
            where V : struct, IComparable<V>, IEquatable<V>
            where D : struct, IComparable<D>, IEquatable<D>;
    }
}
