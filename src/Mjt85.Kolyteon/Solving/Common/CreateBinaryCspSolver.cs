using Mjt85.Kolyteon.Solving.Internals.Builders;

namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Exposes a single static method for building a generic binary CSP solver instance.
/// </summary>
public static class CreateBinaryCspSolver
{
    /// <summary>
    ///     Starts the process of building a generic binary CSP solver instance.
    /// </summary>
    /// <param name="capacity">
    ///     Specifies the maximum binary CSP size (in variables) the new binary CSP solver instance can initially accommodate
    ///     without resizing its internal data structures.
    /// </param>
    /// <returns>A new fluent builder instance, to which method invocations can be changed.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public static IBinaryCspSolverBuilder.ISearchStrategySetter WithInitialCapacity(int capacity) =>
        capacity < 0
            ? throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Value must not be negative.")
            : new BinaryCspSolverBuilder(capacity);
}
