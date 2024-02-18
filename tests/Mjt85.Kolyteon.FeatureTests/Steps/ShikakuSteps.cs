using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.Shikaku;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class ShikakuSteps
{
    private readonly ScenarioContext _scenarioContext;

    public ShikakuSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I have created a Shikaku puzzle from the following grid")]
    public void GivenIHaveCreatedAShikakuPuzzleFromTheFollowingGrid(ShikakuPuzzle puzzle)
    {
        _scenarioContext.Add(Invariants.PUZZLE, puzzle);
    }

    [Given("I have serialized the Shikaku puzzle to JSON")]
    public void GivenIHaveSerializedTheShikakuPuzzleToJson()
    {
        var puzzle = _scenarioContext.Get<ShikakuPuzzle>(Invariants.PUZZLE);

        var json = JsonSerializer.Serialize(puzzle, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [Given("I have obtained the following list of rectangles as a proposed solution to the Shikaku puzzle")]
    public void GivenIHaveObtainedTheFollowingListOfRectanglesAsAProposedSolutionToTheShikakuPuzzle(Table table)
    {
        Rectangle[] proposedSolution = table.CreateSet<(int OriginColumn, int OriginRow, int WidthInCells, int HeightInCells)>()
            .Select(item => new Rectangle(item.OriginColumn, item.OriginRow, item.WidthInCells, item.HeightInCells))
            .ToArray();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [When("I deserialize a Shikaku puzzle from the JSON")]
    public void WhenIDeserializeAShikakuPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<ShikakuPuzzle>(json, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [When("I ask the Shikaku puzzle to validate the proposed solution")]
    public void WhenIAskTheShikakuPuzzleToValidateTheProposedSolution()
    {
        var puzzle = _scenarioContext.Get<ShikakuPuzzle>(Invariants.PUZZLE);
        IReadOnlyList<Rectangle>? proposedSolution =
            _scenarioContext.Get<IReadOnlyList<Rectangle>>(Invariants.PROPOSED_SOLUTION);

        ValidationResult? validationResult = puzzle.ValidSolution(proposedSolution);

        _scenarioContext.Add(Invariants.VALIDATION_RESULT, validationResult);
    }

    [Then("the deserialized Shikaku puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedShikakuPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<ShikakuPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<ShikakuPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }

    [StepArgumentTransformation]
    public static ShikakuPuzzle ShikakuPuzzleTransform(string multiLineText)
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

        return ShikakuPuzzle.FromGrid(grid);
    }
}
