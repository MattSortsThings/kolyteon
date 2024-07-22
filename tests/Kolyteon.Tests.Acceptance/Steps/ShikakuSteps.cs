using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Shikaku;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class ShikakuSteps
{
    private readonly ScenarioContext _scenarioContext;

    public ShikakuSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I have created a Shikaku problem from the following grid")]
    public void GivenIHaveCreatedAShikakuProblemFromTheFollowingGrid(ShikakuProblem problem) =>
        _scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Shikaku problem to JSON")]
    public void GivenIHaveSerializedTheShikakuProblemToJson()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following blocks as a solution to the Shikaku problem")]
    public void GivenIHaveProposedTheFollowingBlocksAsASolutionToTheShikakuProblem(Table table)
    {
        IReadOnlyList<Block> proposedSolution = table.Rows.Select(row =>
        {
            Square originSquare = row["Origin Square"].ToSquare();
            Dimensions dimensions = row["Dimensions"].ToDimensions();

            return originSquare.ToBlock(dimensions);
        }).ToArray();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Shikaku problem from the JSON")]
    public void WhenIDeserializeAShikakuProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        ShikakuProblem? deserializedProblem = JsonSerializer.Deserialize<ShikakuProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Shikaku problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheShikakuProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        IReadOnlyList<Block> proposedSolution = _scenarioContext.Get<IReadOnlyList<Block>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [Then("the deserialized and original Shikaku problems should be equal")]
    public void ThenTheDeserializedAndOriginalShikakuProblemsShouldBeEqual()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        ShikakuProblem? deserializedProblem = _scenarioContext.Get<ShikakuProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }
}
