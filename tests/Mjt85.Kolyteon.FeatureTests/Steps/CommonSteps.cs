using System.ComponentModel.DataAnnotations;
using Mjt85.Kolyteon.FeatureTests.Helpers;

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
}
