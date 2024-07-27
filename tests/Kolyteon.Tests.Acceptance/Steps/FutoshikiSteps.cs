using System.Text.Json;
using Kolyteon.Common;
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

    [Given("I have proposed the following filled squares as a solution to the Futoshiki problem")]
    public void GivenIHaveProposedTheFollowingFilledSquaresAsASolutionToTheFutoshikiProblem(Table table)
    {
        IReadOnlyList<NumberedSquare> proposedSolution = table.CreateSet<SolutionItem>()
            .Select(item => item.FilledSquare)
            .ToArray();

        scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Futoshiki problem from the JSON")]
    public void WhenIDeserializeAFutoshikiProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        FutoshikiProblem? deserializedProblem =
            JsonSerializer.Deserialize<FutoshikiProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Futoshiki problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheFutoshikiProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        FutoshikiProblem problem = scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);
        IReadOnlyList<NumberedSquare> proposedSolution =
            scenarioContext.Get<IReadOnlyList<NumberedSquare>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [Then("the deserialized and original Futoshiki problems should be equal")]
    public void ThenTheDeserializedAndOriginalFutoshikiProblemsShouldBeEqual()
    {
        FutoshikiProblem problem = scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);
        FutoshikiProblem? deserializedProblem = scenarioContext.Get<FutoshikiProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    private readonly record struct SolutionItem(NumberedSquare FilledSquare);
}
