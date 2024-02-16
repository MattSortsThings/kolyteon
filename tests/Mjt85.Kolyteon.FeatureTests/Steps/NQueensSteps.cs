using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.NQueens;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class NQueensSteps
{
    private readonly ScenarioContext _scenarioContext;

    public NQueensSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I have created an N-Queens puzzle in which N = (.*)")]
    public void GivenIHaveCreatedAnNQueensPuzzleInWhichN(int n)
    {
        NQueensPuzzle puzzle = NQueensPuzzle.FromN(n);

        _scenarioContext.Add(Invariants.PUZZLE, puzzle);
    }

    [Given("I have serialized the N-Queens puzzle to JSON")]
    public void GivenIHaveSerializedTheNQueensPuzzleToJson()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);

        var json = JsonSerializer.Serialize(puzzle, Invariants.JsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [Given("I have obtained the following list of queens as a proposed solution to the N-Queens puzzle")]
    public void GivenIHaveObtainedTheFollowingListOfQueensAsAProposedSolutionToTheNQueensPuzzle(Table table)
    {
        IReadOnlyList<Queen> proposedSolution = table.CreateSet<(int Column, int Row)>()
            .Select(item => new Queen(item.Column, item.Row))
            .ToArray();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [When("I deserialize an N-Queens puzzle from the JSON")]
    public void WhenIDeserializeAnNQueensPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<NQueensPuzzle>(json, Invariants.JsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [When("I ask the N-Queens puzzle to validate the proposed solution")]
    public void WhenIAskTheNQueensPuzzleToValidateTheProposedSolution()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);
        IReadOnlyList<Queen>? proposedSolution = _scenarioContext.Get<IReadOnlyList<Queen>>(Invariants.PROPOSED_SOLUTION);

        ValidationResult? validationResult = puzzle.ValidSolution(proposedSolution);

        _scenarioContext.Add(Invariants.VALIDATION_RESULT, validationResult);
    }

    [Then("the deserialized N-Queens puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedNQueensPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }
}
