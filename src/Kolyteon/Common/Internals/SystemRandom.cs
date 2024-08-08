namespace Kolyteon.Common.Internals;

internal sealed class SystemRandom : IRandom
{
    private Random _random;

    public SystemRandom()
    {
        _random = new Random();
    }

    public SystemRandom(int seed)
    {
        _random = new Random(seed);
    }

    public int Next() => _random.Next();

    public int Next(int maxValue) => _random.Next(maxValue);

    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);

    public void UseSeed(int seed) => _random = new Random(seed);
}
