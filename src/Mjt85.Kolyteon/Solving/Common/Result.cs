using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Contains the result of a completed solving algorithm on a generic binary CSP, with performance metrics.
/// </summary>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public sealed record Result<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Gets the binary CSP variable/domain value assignments obtained by the solver.
    /// </summary>
    /// <remarks>
    ///     The contents of this list are:
    ///     <list type="bullet">
    ///         <item>
    ///             an assignment for every variable in the binary CSP, constituting the solution that was reached, <i>or</i>
    ///         </item>
    ///         <item>an empty list, indicating the binary CSP has no solution.</item>
    ///     </list>
    /// </remarks>
    public IReadOnlyList<Assignment<V, D>> Assignments { get; init; } = Array.Empty<Assignment<V, D>>();

    /// <summary>
    ///     Gets the binary CSP solving algorithm that was used.
    /// </summary>
    public Algorithm Algorithm { get; init; } = null!;

    /// <summary>
    ///     Gets the number of setup steps executed by the solving algorithm.
    /// </summary>
    /// <value>A non-negative 64-bit signed integer. The number of setup steps executed by the solver.</value>
    public long SetupSteps { get; init; }

    /// <summary>
    ///     Gets the number of visiting steps executed by the solving algorithm.
    /// </summary>
    /// <value>A non-negative 64-bit signed integer. The number of visiting steps executed by the solver.</value>
    public long VisitingSteps { get; init; }

    /// <summary>
    ///     Gets the number of backtracking steps executed by the solving algorithm.
    /// </summary>
    /// <value>A non-negative 64-bit signed integer. The number of backtracking steps executed by the solver.</value>
    public long BacktrackingSteps { get; init; }

    /// <summary>
    ///     Gets the total steps executed by the solving algorithm.
    /// </summary>
    /// <remarks>
    ///     The value of this property is equal to the sum of the values of the <see cref="SetupSteps" />,
    ///     <see cref="VisitingSteps" /> and <see cref="BacktrackingSteps" /> properties.
    /// </remarks>
    /// <value>A non-negative 64-bit signed integer. The total steps executed by the solver.</value>
    public long TotalSteps { get; init; }
}
