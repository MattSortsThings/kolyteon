using Mjt85.Kolyteon.Solving.Internals.Builders;

namespace Mjt85.Kolyteon.Solving.Common;

public static class CreateBinaryCspSolver
{
    public static IBinaryCspSolverBuilder.ISearchStrategySetter WithInitialCapacity(int capacity) => capacity < 0
        ? throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Value must not be negative.")
        : new BinaryCspSolverBuilder(capacity);
}
