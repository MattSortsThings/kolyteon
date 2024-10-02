using System.Text.Json.Serialization;

namespace Kolyteon.Solving;

/// <summary>
///     Contains metrics that describe a completed backtracking search on a binary CSP.
/// </summary>
[Serializable]
public sealed record SearchMetrics
{
    [JsonConstructor]
    internal SearchMetrics(SearchAlgorithm searchAlgorithm,
        int simplifyingSteps,
        int assigningSteps,
        int backtrackingSteps,
        int totalSteps,
        double efficiency)
    {
        SearchAlgorithm = searchAlgorithm;
        SimplifyingSteps = simplifyingSteps;
        AssigningSteps = assigningSteps;
        BacktrackingSteps = backtrackingSteps;
        TotalSteps = totalSteps;
        Efficiency = efficiency;
    }

    /// <summary>
    ///     Gets a <see cref="SearchAlgorithm" /> instance representing the search algorithm that was used.
    /// </summary>
    public SearchAlgorithm SearchAlgorithm { get; }

    /// <summary>
    ///     Gets the number of simplifying steps executed.
    /// </summary>
    public int SimplifyingSteps { get; }

    /// <summary>
    ///     Gets the number of assigning steps executed.
    /// </summary>
    public int AssigningSteps { get; }

    /// <summary>
    ///     Gets the number of backtracking steps executed.
    /// </summary>
    public int BacktrackingSteps { get; }

    /// <summary>
    ///     Gets the total number of steps executed.
    /// </summary>
    /// <remarks>
    ///     The value of this property is equal to the sum of the <see cref="SimplifyingSteps" />,
    ///     <see cref="AssigningSteps" /> and <see cref="BacktrackingSteps" /> properties.
    /// </remarks>
    public int TotalSteps { get; }

    /// <summary>
    ///     Gets the efficiency of the backtracking search, which is the non-backtracking proportion of the total steps,
    ///     expressed as a real number in the range (0.0,1.0].
    /// </summary>
    /// <remarks>A search that never backtracks has an efficiency of 1.0.</remarks>
    public double Efficiency { get; }

    /// <summary>
    ///     Indicates whether this <see cref="SearchMetrics" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="SearchMetrics" /> instances have equal value if their <see cref="SearchMetrics.SearchAlgorithm" />
    ///     are equal, and their <see cref="SimplifyingSteps" /> values are equal, and their <see cref="AssigningSteps" />
    ///     values are equal, and their <see cref="BacktrackingSteps" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="SearchAlgorithm" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(SearchMetrics? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return SearchAlgorithm.Equals(other.SearchAlgorithm)
               && SimplifyingSteps == other.SimplifyingSteps
               && AssigningSteps == other.AssigningSteps
               && BacktrackingSteps == other.BacktrackingSteps;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="SearchMetrics" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(SearchAlgorithm, SimplifyingSteps, AssigningSteps, BacktrackingSteps);

    /// <summary>
    ///     Creates and returns a new <see cref="SearchMetrics" /> instance from the specified values.
    /// </summary>
    /// <param name="searchAlgorithm">Represents the search algorithm that was used.</param>
    /// <param name="simplifyingSteps">An integer greater than or equal to 1. The number of simplifying steps executed.</param>
    /// <param name="assigningSteps">A non-negative integer. The number of assigning steps executed.</param>
    /// <param name="backtrackingSteps">A non-negative integer. The number of backtracking steps executed.</param>
    /// <returns>A new <see cref="SearchMetrics" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="searchAlgorithm" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="simplifyingSteps" /> is less than 1; or,
    ///     <paramref name="assigningSteps" /> is negative; or, <paramref name="backtrackingSteps" /> is negative.
    /// </exception>
    public static SearchMetrics Create(SearchAlgorithm searchAlgorithm,
        int simplifyingSteps = 1,
        int assigningSteps = 0,
        int backtrackingSteps = 0)
    {
        ArgumentNullException.ThrowIfNull(searchAlgorithm, nameof(searchAlgorithm));
        ArgumentOutOfRangeException.ThrowIfLessThan(simplifyingSteps, 1, nameof(simplifyingSteps));
        ArgumentOutOfRangeException.ThrowIfNegative(assigningSteps, nameof(assigningSteps));
        ArgumentOutOfRangeException.ThrowIfNegative(backtrackingSteps, nameof(backtrackingSteps));

        int totalSteps = simplifyingSteps + assigningSteps + backtrackingSteps;
        double efficiency = (totalSteps - backtrackingSteps) / (double)totalSteps;

        return new SearchMetrics(searchAlgorithm, simplifyingSteps, assigningSteps, backtrackingSteps, totalSteps, efficiency);
    }
}
