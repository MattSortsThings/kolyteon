namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Contains extension methods for converting strategy enumerations to their string short codes.
/// </summary>
public static class ConversionExtensions
{
    /// <summary>
    ///     Returns this search strategy's unique alphabetical short code.
    /// </summary>
    /// <param name="searchStrategy">The search strategy to be converted.</param>
    /// <returns>A string comprising the unique alphabetical short code for this search strategy.</returns>
    public static string ToShortCode(this Search searchStrategy)
    {
        return searchStrategy switch
        {
            Search.MaintainingArcConsistency => "MAC",
            Search.FullLookingAhead => "FLA",
            Search.PartialLookingAhead => "PLA",
            Search.ForwardChecking => "FC",
            Search.ConflictDirectedBackjumping => "CBJ",
            Search.GraphBasedBackjumping => "GBJ",
            Search.Backjumping => "BJ",
            _ => "BT"
        };
    }

    /// <summary>
    ///     Returns this ordering strategy's unique alphabetical short code.
    /// </summary>
    /// <param name="orderingStrategy">The ordering strategy to be converted.</param>
    /// <returns>A string comprising the unique alphabetical short code for the ordering strategy.</returns>
    public static string ToShortCode(this Ordering orderingStrategy)
    {
        return orderingStrategy switch
        {
            Ordering.MaxTightness => "MT",
            Ordering.MaxCardinality => "MC",
            Ordering.Brelaz => "BZ",
            _ => "NO"
        };
    }
}
