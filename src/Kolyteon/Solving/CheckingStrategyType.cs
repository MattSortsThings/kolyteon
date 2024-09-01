namespace Kolyteon.Solving;

/// <summary>
///     Specifies the type of the checking strategy component of a backtracking search algorithm.
/// </summary>
public enum CheckingStrategyType
{
    /// <summary>
    ///     Specifies a look-back checking strategy, which checks the consistency of the partial solution by comparing the
    ///     present assignment against the past assignments to see if any of the binary CSP constraints are not satisfied.
    /// </summary>
    LookBack,

    /// <summary>
    ///     Specifies a look-ahead checking strategy, which checks the consistency of the partial solution by propagating the
    ///     effects of the present assignment through the domains of the future variables to see if a complete solution has
    ///     become unreachable.
    /// </summary>
    LookAhead
}
