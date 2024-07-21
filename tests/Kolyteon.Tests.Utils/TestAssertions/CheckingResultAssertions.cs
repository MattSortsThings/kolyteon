using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Kolyteon.Common;

namespace Kolyteon.Tests.Utils.TestAssertions;

public sealed class CheckingResultAssertions : ObjectAssertions<CheckingResult, CheckingResultAssertions>
{
    public CheckingResultAssertions(CheckingResult value) : base(value)
    {
    }

    public AndConstraint<CheckingResultAssertions> BeSuccessful()
    {
        Execute.Assertion.Given(() => Subject.IsSuccessful)
            .ForCondition(isSuccessful => isSuccessful)
            .FailWith("Expected CheckingResult.IsSuccessful to be 'true', but found 'false'.");

        return new AndConstraint<CheckingResultAssertions>(this);
    }

    public AndConstraint<CheckingResultAssertions> BeUnsuccessful()
    {
        Execute.Assertion.Given(() => Subject.IsSuccessful)
            .ForCondition(isSuccessful => isSuccessful == false)
            .FailWith("Expected CheckingResult.IsSuccessful to be 'false', but found 'true'.");

        return new AndConstraint<CheckingResultAssertions>(this);
    }

    public AndConstraint<CheckingResultAssertions> HaveNullFirstError()
    {
        Execute.Assertion.Given(() => Subject.FirstError)
            .ForCondition(firstError => firstError is null)
            .FailWith("Expected CheckingResult.FirstError to be 'null', but found '{0}'.", firstError => firstError);

        return new AndConstraint<CheckingResultAssertions>(this);
    }

    public AndConstraint<CheckingResultAssertions> HaveFirstError(string expected)
    {
        Execute.Assertion.Given(() => Subject.FirstError)
            .ForCondition(firstError => firstError is not null && firstError.Equals(expected))
            .FailWith("Expected CheckingResult.FirstError to be '{0}', but found '{1}'.", _ => expected,
                firstError => firstError);

        return new AndConstraint<CheckingResultAssertions>(this);
    }
}
