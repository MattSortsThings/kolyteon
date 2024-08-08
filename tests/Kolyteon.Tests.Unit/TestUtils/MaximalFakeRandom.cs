using Kolyteon.Common.Internals;

namespace Kolyteon.Tests.Unit.TestUtils;

internal sealed class MaximalFakeRandom : IRandom
{
    private static readonly Lazy<MaximalFakeRandom> Lazy =
        new(() => new MaximalFakeRandom(), LazyThreadSafetyMode.ExecutionAndPublication);

    public static readonly MaximalFakeRandom Instance = Lazy.Value;

    private MaximalFakeRandom() { }

    public int Next() => int.MaxValue - 1;

    public int Next(int maxValue) => maxValue - 1;

    public int Next(int minValue, int maxValue) => minValue == maxValue ? minValue : maxValue - 1;

    public void UseSeed(int seed) { }
}
