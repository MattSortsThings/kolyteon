using System.Text.Json;
using Kolyteon.Common;
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

    [Given("I have proposed the following squares as a solution to the N-Queens problem")]
    public void GivenIHaveProposedTheFollowingSquaresAsASolutionToTheNQueensProblem(Table table)
    {
        IReadOnlyList<Square> proposedSolution = table.CreateSet<SolutionItem>().Select(item => item.Square).ToArray();

        scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize an N-Queens problem from the JSON")]
    public void WhenIDeserializeAnNQueensProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        NQueensProblem? deserializedProblem = JsonSerializer.Deserialize<NQueensProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the N-Queens problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheNQueensProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        NQueensProblem problem = scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);
        IReadOnlyList<Square> proposedSolution = scenarioContext.Get<IReadOnlyList<Square>>(Constants.Keys.ProposedSolution);

        CheckingResult result = problem.VerifyCorrect(proposedSolution);

        scenarioContext.Add(Constants.Keys.VerificationResult, result);
    }

    [Then("the deserialized and original N-Queens problems should be equal")]
    public void ThenTheDeserializedAndOriginalNQueensProblemsShouldBeEqual()
    {
        NQueensProblem problem = scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);
        NQueensProblem? deserializedProblem = scenarioContext.Get<NQueensProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    internal sealed record SolutionItem
    {
        public Square Square { get; set; }
    }
}
