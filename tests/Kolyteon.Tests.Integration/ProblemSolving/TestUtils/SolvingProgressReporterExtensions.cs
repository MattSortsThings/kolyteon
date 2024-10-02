using Kolyteon.Modelling;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestUtils;

public static class SolvingProgressReporterExtensions
{
    public static void VerifyEndStateMatchesResult<TVariable, TDomainValue>(
        this TestSolvingProgressReporter<TVariable, TDomainValue> reporter, SolvingResult<TVariable, TDomainValue> result)
        where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
        where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    {
        (IReadOnlyList<Assignment<TVariable, TDomainValue>> solution, SearchMetrics metrics) = result;

        reporter.SolvingState.Should().Be(SolvingState.Finished);

        reporter.TotalSteps.Should().Be(metrics.TotalSteps);
        reporter.SimplifyingSteps.Should().Be(metrics.SimplifyingSteps);
        reporter.AssigningSteps.Should().Be(metrics.AssigningSteps);
        reporter.BacktrackingSteps.Should().Be(metrics.BacktrackingSteps);
        reporter.Efficiency.Should().BeApproximately(metrics.Efficiency, 0.000001);

        reporter.Assignments.Should().BeEquivalentTo(solution, options => options.WithoutStrictOrdering());
    }
}
