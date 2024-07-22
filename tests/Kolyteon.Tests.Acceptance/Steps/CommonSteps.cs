using Kolyteon.Common;
using Kolyteon.Tests.Acceptance.TestUtils;
using Kolyteon.Tests.Utils.TestAssertions;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class CommonSteps
{
    private readonly ScenarioContext _scenarioContext;

    public CommonSteps(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

    [Then("the verification result should be successful")]
    public void ThenTheVerificationResultShouldBeSuccessful()
    {
        CheckingResult verificationResult = _scenarioContext.Get<CheckingResult>(Constants.Keys.VerificationResult);

        verificationResult.Should().BeSuccessful().And.HaveNullFirstError();
    }
}
