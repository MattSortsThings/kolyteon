using Kolyteon.Common.Internals;

namespace Kolyteon.Tests.Unit.TestUtils;

internal sealed class MinimalFakeRandom : IRandom
{
    private static readonly Lazy<MinimalFakeRandom> Lazy =
        new(() => new MinimalFakeRandom(), LazyThreadSafetyMode.ExecutionAndPublication);

    public static MinimalFakeRandom Instance = Lazy.Value;

    private MinimalFakeRandom() { }

    public int Next() => 0;

    public int Next(int maxValue) => 0;

    public int Next(int minValue, int maxValue) => minValue;

    public void UseSeed(int seed) { }
}
