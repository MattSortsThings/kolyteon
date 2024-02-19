using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class SudokuSteps
{
    private readonly ScenarioContext _scenarioContext;

    public SudokuSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I have created a Sudoku puzzle from the following grid")]
    public void GivenIHaveCreatedASudokuPuzzleFromTheFollowingGrid(SudokuPuzzle puzzle)
    {
        SudokuPuzzle x = puzzle;
        _scenarioContext.Add(Invariants.PUZZLE, puzzle);
    }

    [Given("I have serialized the Sudoku puzzle to JSON")]
    public void GivenIHaveSerializedTheSudokuPuzzleToJson()
    {
        var puzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.PUZZLE);

        var json = JsonSerializer.Serialize(puzzle, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [When("I deserialize a Sudoku puzzle from the JSON")]
    public void WhenIDeserializeASudokuPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<SudokuPuzzle>(json, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [Then("the deserialized Sudoku puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedSudokuPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }

    [StepArgumentTransformation]
    public static SudokuPuzzle SudokuPuzzleTransform(string multiLineText)
    {
        var lines = multiLineText.Split('\n')
            .Select(line => line.Trim())
            .Select(line => line.Split(" ").ToArray())
            .ToArray();

        var length = lines.Length;

        var grid = new int?[length, length];

        for (var column = 0; column < length; column++)
        {
            for (var row = 0; row < length; row++)
            {
                if (int.TryParse(lines[row][column], out var number))
                {
                    grid[row, column] = number;
                }
            }
        }

        return SudokuPuzzle.FromGrid(grid);
    }
}
