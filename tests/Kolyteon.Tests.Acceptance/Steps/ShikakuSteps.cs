using System.Text.Json;
using Kolyteon.Shikaku;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class ShikakuSteps(ScenarioContext scenarioContext)
{
    [Given("I have created a Shikaku problem from the following grid")]
    public void GivenIHaveCreatedAShikakuProblemFromTheFollowingGrid(ShikakuProblem problem) =>
        scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Shikaku problem to JSON")]
    public void GivenIHaveSerializedTheShikakuProblemToJson()
    {
        ShikakuProblem problem = scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.Json, json);
    }

    [When("I deserialize a Shikaku problem from the JSON")]
    public void WhenIDeserializeAShikakuProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        ShikakuProblem? deserializedProblem = JsonSerializer.Deserialize<ShikakuProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [Then("the deserialized and original Shikaku problems should be equal")]
    public void ThenTheDeserializedAndOriginalShikakuProblemsShouldBeEqual()
    {
        ShikakuProblem problem = scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        ShikakuProblem? deserializedProblem = scenarioContext.Get<ShikakuProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }
}
