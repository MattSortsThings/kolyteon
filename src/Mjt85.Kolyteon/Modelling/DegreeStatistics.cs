namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Contains descriptive statistics for the degree values of all the variables in a binary CSP.
/// </summary>
public record DegreeStatistics
{
    /// <summary>
    ///     The minimum degree value across all variables in the binary CSP.
    /// </summary>
    public int MinimumValue { get; init; }

    /// <summary>
    ///     The arithmetic mean degree value across all variables in the binary CSP.
    /// </summary>
    public double MeanValue { get; init; }

    /// <summary>
    ///     The maximum degree value across all variables in the binary CSP.
    /// </summary>
    public int MaximumValue { get; init; }

    /// <summary>
    ///     The number of distinct degree values across all variables in the binary CSP.
    /// </summary>
    public int DistinctValues { get; init; }
}
