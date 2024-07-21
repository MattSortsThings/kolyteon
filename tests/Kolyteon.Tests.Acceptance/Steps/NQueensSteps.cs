using System.Text.Json;
using Kolyteon.NQueens;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class NQueensSteps(ScenarioContext scenarioContext)
{
    [Given("I have created an N-Queens problem for N = (.*)")]
    public void GivenIHaveCreatedAnNQueensProblemForN(int n)
    {
        NQueensProblem problem = NQueensProblem.FromN(n);

        scenarioContext.Add(Constants.Keys.Problem, problem);
    }

    [Given("I have serialized the N-Queens problem to JSON")]
    public void GivenIHaveSerializedTheNQueensProblemToJson()
    {
        NQueensProblem problem = scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.Json, json);
    }

    [When("I deserialize an N-Queens problem from the JSON")]
    public void WhenIDeserializeAnNQueensProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        NQueensProblem? deserializedProblem = JsonSerializer.Deserialize<NQueensProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [Then("the deserialized and original N-Queens problem should be equal")]
    public void ThenTheDeserializedAndOriginalNQueensProblemShouldBeEqual()
    {
        NQueensProblem problem = scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);
        NQueensProblem? deserializedProblem = scenarioContext.Get<NQueensProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }
}
