namespace Kolyteon.Solving;

/// <summary>
///     Specifies the type of the checking strategy component of a backtracking search algorithm.
/// </summary>
public enum CheckingStrategyType
{
    /// <summary>
    ///     The checking strategy is retrospective: it checks the consistency of the partial solution by comparing the present
    ///     assignment against all past assignments.
    /// </summary>
    Retrospective,

    /// <summary>
    ///     The checking strategy is prospective: it checks the consistency of the partial solution by propagating the effects
    ///     of the present assignment through the domains of the future variables.
    /// </summary>
    Prospective
}
