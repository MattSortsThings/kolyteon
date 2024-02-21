namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Contains descriptive statistics for the sum constraint tightness values of all the variables in a binary CSP.
/// </summary>
public record SumTightnessStatistics
{
    /// <summary>
    ///     The minimum sum constraint tightness value across all variables in the binary CSP.
    /// </summary>
    public double MinimumValue { get; init; }

    /// <summary>
    ///     The arithmetic mean sum constraint tightness value across all variables in the binary CSP.
    /// </summary>
    public double MeanValue { get; init; }

    /// <summary>
    ///     The maximum sum constraint tightness value across all variables in the binary CSP.
    /// </summary>
    public double MaximumValue { get; init; }

    /// <summary>
    ///     The number of distinct sum constraint tightness values (after rounding to 6 decimal places) across all variables in
    ///     the binary CSP.
    /// </summary>
    public int DistinctValues { get; init; }
}
