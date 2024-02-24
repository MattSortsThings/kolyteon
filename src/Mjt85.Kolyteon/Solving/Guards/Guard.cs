using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.Guards;

internal static class Guard
{
    public static void AgainstBinaryCspNotModellingProblem(IMeasurableBinaryCsp binaryCsp)
    {
        if (binaryCsp.Variables == 0)
        {
            throw new ArgumentException("Binary CSP is not modelling a problem.");
        }
    }
}
