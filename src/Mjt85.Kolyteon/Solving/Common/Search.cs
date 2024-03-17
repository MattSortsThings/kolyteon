namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Specifies the search strategy component of a binary CSP solving algorithm.
/// </summary>
public enum Search
{
    /// <summary>
    ///     Specifies that the solving algorithm uses the Backtracking search strategy (short code <c>"BT"</c>).
    /// </summary>
    Backtracking,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Backjumping search strategy (short code <c>"BJ"</c>).
    /// </summary>
    Backjumping,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Graph-Based Backjumping search strategy (short code <c>"GBJ"</c>).
    /// </summary>
    GraphBasedBackjumping,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Conflict-Directed Backjumping search strategy (short code
    ///     <c>"CBJ"</c>).
    /// </summary>
    ConflictDirectedBackjumping,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Forward Checking search strategy (short code <c>"FC"</c>).
    /// </summary>
    ForwardChecking,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Partial Looking Ahead search strategy (short code <c>"PLA"</c>).
    /// </summary>
    PartialLookingAhead,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Full Looking Ahead search strategy (short code <c>"FLA"</c>).
    /// </summary>
    FullLookingAhead,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Maintaining Arc Consistency search strategy (short code <c>"MAC"</c>
    ///     ).
    /// </summary>
    MaintainingArcConsistency
}
