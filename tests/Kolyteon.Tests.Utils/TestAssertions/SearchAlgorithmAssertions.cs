using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Utils.TestAssertions;

public sealed class SearchAlgorithmAssertions : ReferenceTypeAssertions<SearchAlgorithm, SearchAlgorithmAssertions>
{
    public SearchAlgorithmAssertions(SearchAlgorithm subject) : base(subject)
    {
        Identifier = "searchAlgorithm";
    }

    protected override string Identifier { get; }

    public AndConstraint<SearchAlgorithmAssertions> HaveCheckingStrategy(CheckingStrategy expected)
    {
        Execute.Assertion.Given(() => Subject.CheckingStrategy)
            .ForCondition(actualStrategy => actualStrategy.Equals(expected))
            .FailWith("Expected SearchAlgorithm.CheckingStrategy to be '{0}', but found {1}.",
                _ => expected,
                actualStrategy => actualStrategy);

        return new AndConstraint<SearchAlgorithmAssertions>(this);
    }

    public AndConstraint<SearchAlgorithmAssertions> HaveOrderingStrategy(OrderingStrategy expected)
    {
        Execute.Assertion.Given(() => Subject.OrderingStrategy)
            .ForCondition(actualStrategy => actualStrategy.Equals(expected))
            .FailWith("Expected SearchAlgorithm.OrderingStrategy to be '{0}', but found {1}.",
                _ => expected,
                actualStrategy => actualStrategy);

        return new AndConstraint<SearchAlgorithmAssertions>(this);
    }
}
