using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestUtils;

public static class SolvingProgressReporterExtensions
{
    public static void VerifyEndStateMatchesResult<TVariable, TDomainValue>(
        this TestSolvingProgressReporter<TVariable, TDomainValue> reporter, SolvingResult<TVariable, TDomainValue> result)
        where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
        where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    {
        reporter.SolvingState.Should().Be(SolvingState.Finished);

        reporter.TotalSteps.Should().Be(result.TotalSteps);
        reporter.SimplifyingSteps.Should().Be(result.SimplifyingSteps);
        reporter.AssigningSteps.Should().Be(result.AssigningSteps);
        reporter.BacktrackingSteps.Should().Be(result.BacktrackingSteps);

        reporter.Assignments.Should().BeEquivalentTo(result.Assignments, options => options.WithoutStrictOrdering());
    }
}
