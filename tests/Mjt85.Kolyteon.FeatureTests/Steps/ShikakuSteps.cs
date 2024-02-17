using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.Shikaku;

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

    [When("I deserialize a Shikaku puzzle from the JSON")]
    public void WhenIDeserializeAShikakuPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<ShikakuPuzzle>(json, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
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
