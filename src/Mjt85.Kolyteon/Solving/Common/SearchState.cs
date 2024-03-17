namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Specifies the state of the search as a depth-first traversal of a search tree, starting at the root level and
///     terminating at either the leaf level or the root level.
/// </summary>
public enum SearchState
{
    /// <summary>
    ///     Initial state: no search in progress, the solver should execute a setup step next.
    /// </summary>
    Initial,

    /// <summary>
    ///     Safe state: search is in progress, leaf level is reachable, the solver should execute a visiting step next.
    /// </summary>
    Safe,

    /// <summary>
    ///     Unsafe state: search is in progress, leaf level is unreachable, the solver should execute a backtracking step next.
    /// </summary>
    Unsafe,

    /// <summary>
    ///     Final state: search has terminated at leaf level (solution found) or root level (no solution exists).
    /// </summary>
    Final
}
