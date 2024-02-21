namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Contains problem-level descriptive metrics for a binary CSP.
/// </summary>
public record ProblemMetrics
{
    /// <summary>
    ///     The number of variables in the binary CSP.
    /// </summary>
    public int Variables { get; init; }

    /// <summary>
    ///     The number of binary constraints in the binary CSP.
    /// </summary>
    public int Constraints { get; init; }

    /// <summary>
    ///     The problem constraint density of the binary CSP, expressed as a real number in the range [0,1].
    /// </summary>
    public double ConstraintDensity { get; init; }

    /// <summary>
    ///     The problem constraint tightness of the binary CSP, expressed as a real number in the range [0,1].
    /// </summary>
    public double ConstraintTightness { get; init; }
}
