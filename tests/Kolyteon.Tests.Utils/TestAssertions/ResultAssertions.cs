using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Kolyteon.Common;

namespace Kolyteon.Tests.Utils.TestAssertions;

public sealed class ResultAssertions : ObjectAssertions<Result, ResultAssertions>
{
    public ResultAssertions(Result value) : base(value)
    {
    }

    public AndConstraint<ResultAssertions> BeSuccessful()
    {
        Execute.Assertion.Given(() => Subject.IsSuccessful)
            .ForCondition(isSuccessful => isSuccessful)
            .FailWith("Expected Result.IsSuccessful to be 'true', but found 'false'.");

        return new AndConstraint<ResultAssertions>(this);
    }

    public AndConstraint<ResultAssertions> BeUnsuccessful()
    {
        Execute.Assertion.Given(() => Subject.IsSuccessful)
            .ForCondition(isSuccessful => isSuccessful == false)
            .FailWith("Expected Result.IsSuccessful to be 'false', but found 'true'.");

        return new AndConstraint<ResultAssertions>(this);
    }

    public AndConstraint<ResultAssertions> HaveNullFirstError()
    {
        Execute.Assertion.Given(() => Subject.FirstError)
            .ForCondition(firstError => firstError is null)
            .FailWith("Expected Result.FirstError to be 'null', but found '{0}'.", firstError => firstError);

        return new AndConstraint<ResultAssertions>(this);
    }

    public AndConstraint<ResultAssertions> HaveFirstError(string expected)
    {
        Execute.Assertion.Given(() => Subject.FirstError)
            .ForCondition(firstError => firstError is not null && firstError.Equals(expected))
            .FailWith("Expected Result.FirstError to be '{0}', but found '{1}'.", _ => expected,
                firstError => firstError);

        return new AndConstraint<ResultAssertions>(this);
    }
}
