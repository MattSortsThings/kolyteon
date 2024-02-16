using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.NQueens;

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

    [When("I deserialize an N-Queens puzzle from the JSON")]
    public void WhenIDeserializeAnNQueensPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<NQueensPuzzle>(json, Invariants.JsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [Then("the deserialized N-Queens puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedNQueensPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }
}
