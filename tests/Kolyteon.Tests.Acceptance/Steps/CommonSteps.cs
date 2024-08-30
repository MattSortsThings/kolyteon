using Kolyteon.Common;
using Kolyteon.Tests.Acceptance.TestUtils;
using Kolyteon.Tests.Utils.TestAssertions;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class CommonSteps(ScenarioContext scenarioContext)
{
    [Then("the verification result should be successful")]
    public void ThenTheVerificationResultShouldBeSuccessful()
    {
        Result verificationResult = scenarioContext.Get<Result>(Constants.Keys.VerificationResult);

        verificationResult.Should().BeSuccessful().And.HaveNullFirstError();
    }
}
