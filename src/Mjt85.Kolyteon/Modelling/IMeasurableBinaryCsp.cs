namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Defines properties and methods to measure the size and complexity of a binary CSP.
/// </summary>
public interface IMeasurableBinaryCsp
{
    /// <summary>
    ///     Gets the number of variables in the binary CSP.
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The number of variables in the binary CSP. Value is 0 when instance is not
    ///     modelling a problem.
    /// </value>
    public int Variables { get; }

    /// <summary>
    ///     Gets the number of binary constraints in the binary CSP.
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The number of binary constraints in the binary CSP. Value is 0 when instance
    ///     is not modelling a problem.
    /// </value>
    public int Constraints { get; }

    /// <summary>
    ///     Gets the problem constraint density of the binary CSP.
    /// </summary>
    /// <remarks>
    ///     The constraint density of a binary CSP is the probability of any two variables being adjacent, expressed as a
    ///     real number in the range [0,1]. It is calculated by taking the ratio of <i>x</i> to <i>y</i>, where:
    ///     <list type="bullet">
    ///         <item><i>x</i> is the number of binary constraints, and</item>
    ///         <item><i>y</i> is the maximum possible number of constraints given the number of variables.</item>
    ///     </list>
    /// </remarks>
    /// <value>
    ///     A double-precision floating point number in the range [0,1]. The problem constraint density of the binary CSP.
    ///     Value is 0 when instance is not modelling a problem.
    /// </value>
    public double ConstraintDensity { get; }

    /// <summary>
    ///     Gets the problem constraint tightness of the binary CSP.
    /// </summary>
    /// <remarks>
    ///     The constraint tightness of a binary CSP is the probability of any two assignments for different variables being
    ///     inconsistent, given that the variables are adjacent, expressed as a real number in the range [0,1]. It is
    ///     calculated by taking the ratio of <i>x</i> to <i>y</i>, where:
    ///     <list type="bullet">
    ///         <item><i>x</i> is the sum total inconsistent assignment pairs for all pairs of adjacent variables, and</item>
    ///         <item><i>y</i> is the sum total domain cartesian product size for all pairs of adjacent variables.</item>
    ///     </list>
    /// </remarks>
    /// <value>
    ///     A double-precision floating point number in the range [0,1]. The problem constraint tightness of the binary CSP.
    ///     Value is 0 when instance is not modelling a problem.
    /// </value>
    public double ConstraintTightness { get; }

    /// <summary>
    ///     Returns a data structure containing problem-level descriptive metrics for the binary CSP.
    /// </summary>
    /// <returns>
    ///     A new <see cref="ProblemMetrics" /> instance describing the binary CSP. Returns a default instance if this
    ///     instance is not modelling a problem.
    /// </returns>
    public ProblemMetrics GetProblemMetrics();

    /// <summary>
    ///     Returns a data structure containing descriptive statistics for the domain size values of all the variables in the
    ///     binary CSP.
    /// </summary>
    /// <returns>
    ///     A new <see cref="DomainSizeStatistics" /> instance describing the domain size values of all the variables in
    ///     the binary CSP. Returns a default instance if this instance is not modelling a problem.
    /// </returns>
    public DomainSizeStatistics GetDomainSizeStatistics();

    /// <summary>
    ///     Returns a data structure containing descriptive statistics for the degree values of all the variables in the binary
    ///     CSP.
    /// </summary>
    /// <returns>
    ///     A new <see cref="DomainSizeStatistics" /> instance describing the degree values of all the variables in the
    ///     binary CSP. Returns a default instance if this instance is not modelling a problem.
    /// </returns>
    public DegreeStatistics GetDegreeStatistics();

    /// <summary>
    ///     Returns a data structure containing descriptive statistics for the sum constraint tightness size values of all the
    ///     variables in the binary CSP.
    /// </summary>
    /// <returns>
    ///     A new <see cref="DomainSizeStatistics" /> instance describing the sum constraint tightness values of all the
    ///     variables in the binary CSP. Returns a default instance if this instance is not modelling a problem.
    /// </returns>
    public SumTightnessStatistics GetSumTightnessStatistics();
}
