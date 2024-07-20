using FluentAssertions.Primitives;
using NetArchTest.Rules;

namespace Kolyteon.Tests.Architecture.TestUtils;

internal sealed class TestResultAssertions : ReferenceTypeAssertions<TestResult, TestResultAssertions>
{
    public TestResultAssertions(TestResult subject) : base(subject)
    {
        Identifier = "TestResult";
    }

    protected override string Identifier { get; }

    public void BeSuccessful() => Execute.Assertion.Given(() => Subject.IsSuccessful)
        .ForCondition(isSuccessful => isSuccessful)
        .FailWith("Expected TestResult.IsSuccessful to be 'true', but found 'false'.");
}
