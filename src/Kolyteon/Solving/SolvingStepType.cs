namespace Kolyteon.Solving;

/// <summary>
///     Specifies the type of a solving operation step.
/// </summary>
public enum SolvingStepType
{
    /// <summary>
    ///     Specifies a Simplifying step: the solver was in the <see cref="SolvingState.Simplifying" /> state at the beginning
    ///     of the step.
    /// </summary>
    Simplifying = 0,

    /// <summary>
    ///     Specifies an Assigning step: the solver was in the <see cref="SolvingState.Assigning" /> state at the beginning of
    ///     the step.
    /// </summary>
    Assigning = 1,

    /// <summary>
    ///     Specifies a Backtracking step: the solver was in the <see cref="SolvingState.Backtracking" /> state at the
    ///     beginning of the step.
    /// </summary>
    Backtracking = 2
}
