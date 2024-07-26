using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Sudoku;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class SudokuSteps(ScenarioContext scenarioContext)
{
    [Given("I have created a Sudoku problem from the following grid")]
    public void GivenIHaveCreatedASudokuProblemFromTheFollowingGrid(SudokuProblem problem) =>
        scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Sudoku problem to JSON")]
    public void GivenIHaveSerializedTheSudokuProblemToJson()
    {
        SudokuProblem problem = scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following filled squares as a solution to the Sudoku problem")]
    public void GivenIHaveProposedTheFollowingFilledSquaresAsASolutionToTheSudokuProblem(Table table)
    {
        IReadOnlyList<NumberedSquare> proposedSolution = table.CreateSet<SolutionItem>()
            .Select(item => item.FilledSquare)
            .ToArray();

        scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Sudoku problem from the JSON")]
    public void WhenIDeserializeASudokuProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        SudokuProblem? deserializedProblem = JsonSerializer.Deserialize<SudokuProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Sudoku problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheSudokuProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        SudokuProblem problem = scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);
        IReadOnlyList<NumberedSquare> proposedSolution =
            scenarioContext.Get<IReadOnlyList<NumberedSquare>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [Then("the deserialized and original Sudoku problems should be equal")]
    public void ThenTheDeserializedAndOriginalSudokuProblemsShouldBeEqual()
    {
        SudokuProblem problem = scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);
        SudokuProblem? deserializedProblem = scenarioContext.Get<SudokuProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    private readonly record struct SolutionItem(NumberedSquare FilledSquare);
}
