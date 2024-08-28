using Kolyteon.Common;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class NQueensSolvingProgressReporter : SolvingProgressReporter<int, Square>
{
    public List<NQueensSolvingProgressReport> Reports { get; } = new(14);

    protected override void OnReset() => Reports.Clear();

    protected override void OnReport() => Reports.Add(new NQueensSolvingProgressReport
    {
        TotalSteps = TotalSteps,
        SearchLevel = SearchLevel,
        SolvingState = SolvingState,
        Squares = Assignments.Reverse().Select(assignment => assignment.DomainValue).ToArray()
    });
}
