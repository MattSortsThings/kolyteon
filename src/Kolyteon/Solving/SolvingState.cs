namespace Kolyteon.Solving;

/// <summary>
///     Specifies the current state of a binary CSP solver.
/// </summary>
public enum SolvingState
{
    /// <summary>
    ///     Specifies the Ready state: the solver is ready to begin a solving operation.
    /// </summary>
    Ready = 0,

    /// <summary>
    ///     Specifies the Simplifying state: the solver is attempting to simplify the entire search tree from the root level.
    /// </summary>
    Simplifying = 1,

    /// <summary>
    ///     Specifies the Assigning state: the solver is attempting to find a safe assignment at the present search tree level.
    /// </summary>
    Assigning = 2,

    /// <summary>
    ///     Specifies the Backtracking state: the solver is backtracking from the present search tree level to an earlier
    ///     level.
    /// </summary>
    Backtracking = 3,

    /// <summary>
    ///     Specifies the Finished state: the solving operation has terminated at either the leaf level of the search tree
    ///     (i.e. a solution to the binary CSP has been found) or the root level (i.e. the binary CSP has no solution).
    /// </summary>
    Finished = 4
}
