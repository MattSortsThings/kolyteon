using System.ComponentModel.DataAnnotations;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.Modelling;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class CommonSteps
{
    private readonly ScenarioContext _scenarioContext;

    public CommonSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Then("the validation result should be successful")]
    public void ThenTheValidationResultShouldBeSuccessful()
    {
        var validationResult = _scenarioContext.Get<ValidationResult?>(Invariants.VALIDATION_RESULT);

        validationResult.Should().Be(ValidationResult.Success);
    }

    [Then("the binary CSP problem metrics should be as follows")]
    public void ThenTheBinaryCspProblemMetricsShouldBeAsFollows(Table table)
    {
        var problemMetrics = _scenarioContext.Get<ProblemMetrics>(Invariants.PROBLEM_METRICS);
        var expectedMetrics = table.CreateInstance<ProblemMetrics>();

        problemMetrics.Should().BeEquivalentTo(expectedMetrics, options => options.Using<double>(e =>
            e.Subject.Should().BeApproximately(e.Expectation, Invariants.SixDecimalPlacesPrecision)).WhenTypeIs<double>());
    }

    [Then("the binary CSP variable domain size statistics should be as follows")]
    public void ThenTheBinaryCspVariableDomainSizeStatisticsShouldBeAsFollows(Table table)
    {
        var domainSizeStatistics = _scenarioContext.Get<DomainSizeStatistics>(Invariants.DOMAIN_SIZE_STATISTICS);
        var expectedDomainSizeStatistics = table.CreateInstance<DomainSizeStatistics>();

        domainSizeStatistics.Should().BeEquivalentTo(expectedDomainSizeStatistics, options => options.Using<double>(e =>
            e.Subject.Should().BeApproximately(e.Expectation, Invariants.SixDecimalPlacesPrecision)).WhenTypeIs<double>());
    }

    [Then("the binary CSP variable degree statistics should be as follows")]
    public void ThenTheBinaryCspVariableDegreeStatisticsShouldBeAsFollows(Table table)
    {
        var degreeStatistics = _scenarioContext.Get<DegreeStatistics>(Invariants.DEGREE_STATISTICS);
        var expectedDegreeStatistics = table.CreateInstance<DegreeStatistics>();

        degreeStatistics.Should().BeEquivalentTo(expectedDegreeStatistics, options => options.Using<double>(e =>
            e.Subject.Should().BeApproximately(e.Expectation, Invariants.SixDecimalPlacesPrecision)).WhenTypeIs<double>());
    }

    [Then("the binary CSP variable sum tightness statistics should be as follows")]
    public void ThenTheBinaryCspVariableSumTightnessStatisticsShouldBeAsFollows(Table table)
    {
        var sumTightnessStatistics = _scenarioContext.Get<SumTightnessStatistics>(Invariants.SUM_TIGHTNESS_STATISTICS);
        var expectedSumTightnessStatistics = table.CreateInstance<SumTightnessStatistics>();

        sumTightnessStatistics.Should().BeEquivalentTo(expectedSumTightnessStatistics, options => options.Using<double>(e =>
            e.Subject.Should().BeApproximately(e.Expectation, Invariants.SixDecimalPlacesPrecision)).WhenTypeIs<double>());
    }
}
