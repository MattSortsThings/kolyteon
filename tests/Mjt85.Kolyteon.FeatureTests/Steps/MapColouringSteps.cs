using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Silent;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class MapColouringSteps
{
    private readonly IModellingBinaryCsp<MapColouringPuzzle, Region, Colour> _binaryCsp;
    private readonly ISilentBinaryCspSolver<Region, Colour> _binaryCspSolver;
    private readonly ScenarioContext _scenarioContext;

    public MapColouringSteps(IModellingBinaryCsp<MapColouringPuzzle, Region, Colour> binaryCsp,
        ISilentBinaryCspSolver<Region, Colour> binaryCspSolver,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _binaryCspSolver = binaryCspSolver ?? throw new ArgumentNullException(nameof(binaryCspSolver));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
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

        var json = JsonSerializer.Serialize(puzzle, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [Given("I have obtained the following region/colour dictionary as a proposed solution to the Map Colouring puzzle")]
    public void GivenIHaveObtainedTheFollowingRegionColourDictionaryAsAProposedSolutionToTheMapColouringPuzzle(Table table)
    {
        IReadOnlyDictionary<Region, Colour> proposedSolution = table.CreateSet<(Region Region, Colour Colour)>()
            .ToDictionary(item => item.Region, item => item.Colour);

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [Given("I have modelled the Map Colouring puzzle as a binary CSP")]
    public void GivenIHaveModelledTheMapColouringPuzzleAsABinaryCsp()
    {
        var puzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.PUZZLE);
        _binaryCsp.Model(puzzle);
    }

    [Given("I have set the Map Colouring binary CSP solver to use the '(.*)' search strategy")]
    public void GivenIHaveSetTheMapColouringBinaryCspSolverToUseTheSearchStrategy(Search strategy)
    {
        _binaryCspSolver.SearchStrategy = strategy;
    }

    [Given("I have set the Map Colouring binary CSP solver to use the '(.*)' ordering strategy")]
    public void GivenIHaveSetTheMapColouringBinaryCspSolverToUseTheOrderingStrategy(Ordering strategy)
    {
        _binaryCspSolver.OrderingStrategy = strategy;
    }

    [When("I deserialize a Map Colouring puzzle from the JSON")]
    public void WhenIDeserializeAMapColouringPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<MapColouringPuzzle>(json, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [When("I ask the Map Colouring puzzle to validate the proposed solution")]
    public void WhenIAskTheMapColouringPuzzleToValidateTheProposedSolution()
    {
        var puzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.PUZZLE);
        IReadOnlyDictionary<Region, Colour>? proposedSolution =
            _scenarioContext.Get<IReadOnlyDictionary<Region, Colour>>(Invariants.PROPOSED_SOLUTION);

        ValidationResult? validationResult = puzzle.ValidSolution(proposedSolution);

        _scenarioContext.Add(Invariants.VALIDATION_RESULT, validationResult);
    }

    [When("I request the binary CSP metrics for the Map Colouring puzzle")]
    public void WhenIRequestTheBinaryCspMetricsForTheMapColouringPuzzle()
    {
        ProblemMetrics problemMetrics = _binaryCsp.GetProblemMetrics();
        DomainSizeStatistics domainSizeStatistics = _binaryCsp.GetDomainSizeStatistics();
        DegreeStatistics degreeStatistics = _binaryCsp.GetDegreeStatistics();
        SumTightnessStatistics sumTightnessStatistics = _binaryCsp.GetSumTightnessStatistics();

        _scenarioContext.Add(Invariants.PROBLEM_METRICS, problemMetrics);
        _scenarioContext.Add(Invariants.DOMAIN_SIZE_STATISTICS, domainSizeStatistics);
        _scenarioContext.Add(Invariants.DEGREE_STATISTICS, degreeStatistics);
        _scenarioContext.Add(Invariants.SUM_TIGHTNESS_STATISTICS, sumTightnessStatistics);
    }

    [When("I run the Map Colouring binary CSP solver on the binary CSP")]
    public void WhenIRunTheMapColouringBinaryCspSolverOnTheBinaryCsp()
    {
        Result<Region, Colour> result = _binaryCspSolver.Solve(_binaryCsp);
        IReadOnlyDictionary<Region, Colour> proposedSolution = result.Assignments.ToPuzzleSolution();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [Then("the deserialized Map Colouring puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedMapColouringPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<MapColouringPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }
}
