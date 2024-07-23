using System.Text.Json;
using Kolyteon.Common;
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

    [Given("I have proposed the following blocks as a solution to the Shikaku problem")]
    public void GivenIHaveProposedTheFollowingBlocksAsASolutionToTheShikakuProblem(Table table)
    {
        Block[] proposedSolution = table.CreateSet<SolutionItem>().Select(item => item.Block).ToArray();

        scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Shikaku problem from the JSON")]
    public void WhenIDeserializeAShikakuProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        ShikakuProblem? deserializedProblem = JsonSerializer.Deserialize<ShikakuProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Shikaku problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheShikakuProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        ShikakuProblem problem = scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        IReadOnlyList<Block> proposedSolution = scenarioContext.Get<IReadOnlyList<Block>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [Then("the deserialized and original Shikaku problems should be equal")]
    public void ThenTheDeserializedAndOriginalShikakuProblemsShouldBeEqual()
    {
        ShikakuProblem problem = scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        ShikakuProblem? deserializedProblem = scenarioContext.Get<ShikakuProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    internal sealed record SolutionItem
    {
        public Block Block { get; set; }
    }
}
