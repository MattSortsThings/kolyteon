namespace Kolyteon.Common.Internals;

internal interface IRandom
{
    public int Next();

    public int Next(int maxValue);

    public int Next(int minValue, int maxValue);

    public void UseSeed(int seed);
}
