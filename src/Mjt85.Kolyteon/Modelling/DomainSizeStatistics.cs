namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Contains descriptive statistics for the domain size values of all the variables in a binary CSP.
/// </summary>
public record DomainSizeStatistics
{
    /// <summary>
    ///     The minimum domain size value across all variables in the binary CSP.
    /// </summary>
    public int MinimumValue { get; init; }

    /// <summary>
    ///     The arithmetic mean domain size value across all variables in the binary CSP.
    /// </summary>
    public double MeanValue { get; init; }

    /// <summary>
    ///     The maximum domain size value across all variables in the binary CSP.
    /// </summary>
    public int MaximumValue { get; init; }

    /// <summary>
    ///     The number of distinct domain size values across all variables in the binary CSP.
    /// </summary>
    public int DistinctValues { get; init; }
}
