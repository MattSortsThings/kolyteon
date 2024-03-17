using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Verbose;

/// <summary>
///     Contains data about a binary CSP solving algorithm step.
/// </summary>
/// <param name="StepType">The type of the step.</param>
/// <param name="CurrentSearchState">The state of the search at the end of the step.</param>
/// <param name="CurrentSearchLevel">The level of the search in the search tree at the end of the step.</param>
/// <param name="SearchTreeLeafLevel">The leaf level of the search tree.</param>
/// <param name="LatestAssignment">
///     The assignment that was made in the step (value is <c>null</c> unless
///     <see cref="StepNotification{V,D}.StepType" /> is <see cref="StepType.Visiting" /> <i>and</i>
///     <see cref="StepNotification{V,D}.CurrentSearchState" /> is <i>either</i> <see cref="SearchState.Safe" /> <i>or</i>
///     <see cref="SearchState.Final" />).
/// </param>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public readonly record struct StepNotification<V, D>(
    StepType StepType,
    SearchState CurrentSearchState,
    int CurrentSearchLevel,
    int SearchTreeLeafLevel,
    Assignment<V, D>? LatestAssignment = null)
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>;
