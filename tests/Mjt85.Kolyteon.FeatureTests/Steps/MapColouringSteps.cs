using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.MapColouring;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class MapColouringSteps
{
    private readonly ScenarioContext _scenarioContext;

    public MapColouringSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I have created a Map Colouring puzzle as follows")]
    public void GivenIHaveCreatedAMapColouringPuzzleAsFollows(Table table)
    {
        var config = table.CreateInstance<MapColouringPuzzleConfig>();

        MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
            .WithPresetMapAndGlobalColours(config.PresetMap, config.GlobalColours)
            .Build();

        _scenarioContext.Add(Invariants.PUZZLE, puzzle);
    }

    [Given("I have serialized the Map Colouring puzzle to JSON")]
    public void GivenIHaveSerializedTheMapColouringPuzzleToJson()
    {
        var puzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.PUZZLE);

        var json = JsonSerializer.Serialize(puzzle, Invariants.JsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [When("I deserialize a Map Colouring puzzle from the JSON")]
    public void WhenIDeserializeAMapColouringPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<MapColouringPuzzle>(json, Invariants.JsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [Then("the deserialized Map Colouring puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedMapColouringPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }
}
