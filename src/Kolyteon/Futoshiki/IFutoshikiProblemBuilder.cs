using Kolyteon.Common;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Fluent builder API for the <see cref="FutoshikiProblem" /> record type.
/// </summary>
/// <remarks>
///     Any <see cref="FutoshikiProblem" /> instance built using this API is guaranteed to represent a valid (but not
///     necessarily solvable) Futoshiki problem.
/// </remarks>
public interface IFutoshikiProblemBuilder
{
    /// <summary>
    ///     Sets the grid and filled squares for the problem.
    /// </summary>
    /// <remarks>
    ///     In order to create a valid (but not necessarily solvable) Futoshiki problem, the <paramref name="grid" /> parameter
    ///     must satisfy all the following conditions:
    ///     <list type="number">
    ///         <item>
    ///             The rank-0 and rank-1 lengths of the 2-dimensional array must be equal to each other, and not less than
    ///             4, and no greater than 9.
    ///         </item>
    ///         <item>
    ///             Every non-<see langword="null" /> value in the array must be greater than or equal to 1 and less than or
    ///             equal to rank-0 length.
    ///         </item>
    ///         <item>No non-<see langword="null" /> value may occur more than once in the same column or row.</item>
    ///     </list>
    /// </remarks>
    /// <param name="grid">
    ///     A 2-dimensional array of nullable integer values representing the problem grid, in which each
    ///     <see langword="null" /> value represents a filled square.
    /// </param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="grid" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     The rank-0 and rank-1 lengths of the <paramref name="grid" /> parameter are not
    ///     equal to each other, or are less than 4, or are greater than 9.
    /// </exception>
    public ISignAdder FromGrid(int?[,] grid);

    /// <summary>
    ///     Fluent builder API for the <see cref="FutoshikiProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="FutoshikiProblem" /> instance built using this API is guaranteed to represent a valid (but not
    ///     necessarily solvable) Futoshiki problem.
    /// </remarks>
    public interface ISignAdder : ITerminal
    {
        /// <summary>
        ///     Adds the specified greater than ( &gt; ) sign to the problem.
        /// </summary>
        /// <param name="sign">
        ///     The sign to be added. If an identical sign has already been added, the present sign will be
        ///     discarded.
        /// </param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sign" /> is <see langword="null" />.</exception>
        public ISignAdder AddSign(GreaterThanSign sign);

        /// <summary>
        ///     Adds the specified less than ( &lt; ) sign to the problem.
        /// </summary>
        /// <param name="sign">
        ///     The sign to be added. If an identical sign has already been added, the present sign will be
        ///     discarded.
        /// </param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sign" /> is <see langword="null" />.</exception>
        public ISignAdder AddSign(LessThanSign sign);

        /// <summary>
        ///     Adds the specified greater than ( &gt; ) signs to the problem.
        /// </summary>
        /// <param name="signs">The signs to be added. Any duplicate signs are will be discarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="signs" /> is <see langword="null" />.</exception>
        public ISignAdder AddSigns(IEnumerable<GreaterThanSign> signs);

        /// <summary>
        ///     Adds the specified less than ( &lt; ) signs to the problem.
        /// </summary>
        /// <param name="signs">The signs to be added. Any duplicate signs are will be discarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="signs" /> is <see langword="null" />.</exception>
        public ISignAdder AddSigns(IEnumerable<LessThanSign> signs);
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="FutoshikiProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="FutoshikiProblem" /> instance built using this API is guaranteed to represent a valid (but not
    ///     necessarily solvable) Futoshiki problem.
    /// </remarks>
    public interface ITerminal
    {
        /// <summary>
        ///     Creates and returns a new <see cref="FutoshikiProblem" /> instance as specified by all the previous fluent builder
        ///     method invocations.
        /// </summary>
        /// <returns>A new <see cref="FutoshikiProblem" /> instance.</returns>
        /// <exception cref="InvalidProblemException">
        ///     The <see cref="FutoshikiProblem" /> as specified did not represent a valid Futoshiki problem.
        /// </exception>
        public FutoshikiProblem Build();
    }
}
