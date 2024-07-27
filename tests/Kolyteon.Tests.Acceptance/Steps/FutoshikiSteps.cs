using System.Text.Json;
using Kolyteon.Futoshiki;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class FutoshikiSteps(ScenarioContext scenarioContext)
{
    [Given("I have created a Futoshiki problem matching the following diagram")]
    public void GivenIHaveCreatedAFutoshikiProblemMatchingTheFollowingDiagram(FutoshikiProblem problem) =>
        scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Futoshiki problem to JSON")]
    public void GivenIHaveSerializedTheFutoshikiProblemToJson()
    {
        FutoshikiProblem problem = scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.Json, json);
    }

    [When("I deserialize a Futoshiki problem from the JSON")]
    public void WhenIDeserializeAFutoshikiProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        FutoshikiProblem? deserializedProblem =
            JsonSerializer.Deserialize<FutoshikiProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [Then("the deserialized and original Futoshiki problems should be equal")]
    public void ThenTheDeserializedAndOriginalFutoshikiProblemsShouldBeEqual()
    {
        FutoshikiProblem problem = scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);
        FutoshikiProblem? deserializedProblem = scenarioContext.Get<FutoshikiProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }
}
