namespace Mjt85.Kolyteon.Modelling.Internals;

/// <summary>
///     Guard clauses.
/// </summary>
internal static class Guard
{
    /// <summary>
    ///     Checks that the binary CSP is empty and throws an exception if it is not.
    /// </summary>
    /// <param name="binaryCsp">The binary CSP to be checked.</param>
    /// <exception cref="InvalidOperationException">The <paramref name="binaryCsp" /> parameter is not empty.</exception>
    public static void AgainstOverwritingNonEmptyBinaryCsp(IMeasurableBinaryCsp binaryCsp)
    {
        if (binaryCsp.Variables > 0)
        {
            throw new InvalidOperationException("Binary CSP is already modelling a problem.");
        }
    }

    /// <summary>
    ///     Checks that the binary CSP has at least one variable and throws an exception if it is empty.
    /// </summary>
    /// <param name="binaryCsp">The binary CSP to be checked.</param>
    /// <exception cref="InvalidOperationException">The <paramref name="binaryCsp" /> parameter has no variables.</exception>
    public static void AgainstBinaryCspWithZeroVariables(IMeasurableBinaryCsp binaryCsp)
    {
        if (binaryCsp.Variables == 0)
        {
            throw new InvalidOperationException("Binary CSP has zero variables when modelling problem.");
        }
    }
}
