using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.MapColouring;
using Kolyteon.Modelling;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class MapColouringSteps
{
    private readonly IBinaryCsp<Block, Colour, MapColouringProblem> _binaryCsp;
    private readonly ScenarioContext _scenarioContext;

    public MapColouringSteps(IBinaryCsp<Block, Colour, MapColouringProblem> binaryCsp, ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Map Colouring problem with a (.*) canvas and the following blocks")]
    public void GivenIHaveCreatedAMapColouringProblemWithACanvasAndTheFollowingBlocks(Dimensions size, Table table)
    {
        IMapColouringProblemBuilder.IBlockAndColoursAdder builder =
            MapColouringProblem.Create().WithCanvasSize(size).UseBlockSpecificColours();

        foreach ((Block block, Colour[] permittedColours) in table.CreateSet<BlockItem>())
        {
            builder = builder.AddBlockWithColours(block, permittedColours);
        }

        _scenarioContext.Add(Constants.Keys.Problem, builder.Build());
    }

    [Given("I have serialized the Map Colouring problem to JSON")]
    public void GivenIHaveSerializedTheMapColouringProblemToJson()
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following block and colour dictionary as a solution to the Map Colouring problem")]
    public void GivenIHaveProposedTheFollowingBlockAndColourDictionaryAsASolutionToTheMapColouringProblem(Table table)
    {
        IReadOnlyDictionary<Block, Colour> proposedSolution = table.CreateSet<SolutionItem>()
            .ToDictionary(item => item.Block, item => item.Colour);

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Map Colouring problem from the JSON")]
    public void WhenIDeserializeAMapColouringProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        MapColouringProblem? deserializedProblem =
            JsonSerializer.Deserialize<MapColouringProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Map Colouring problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheMapColouringProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);
        IReadOnlyDictionary<Block, Colour> proposedSolution =
            _scenarioContext.Get<IReadOnlyDictionary<Block, Colour>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Map Colouring problem as a binary CSP")]
    public void WhenIModelTheMapColouringProblemAsABinaryCsp()
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [Then("the deserialized and original Map Colouring problems should be equal")]
    public void ThenTheDeserializedAndOriginalMapColouringProblemsShouldBeEqual()
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);
        MapColouringProblem? deserializedProblem =
            _scenarioContext.Get<MapColouringProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    [Then("the Map Colouring binary CSP should have (.*) variables")]
    public void ThenTheMapColouringBinaryCspShouldHaveVariables(int expected) => _binaryCsp.Variables.Should().Be(expected);

    [Then("the Map Colouring binary CSP should have (.*) constraints")]
    public void ThenTheMapColouringBinaryCspShouldHaveConstraints(int expected) => _binaryCsp.Constraints.Should().Be(expected);

    [Then("the Map Colouring binary CSP should have a constraint density of (.*)")]
    public void ThenTheMapColouringBinaryCspShouldHaveAConstraintDensityOf(double expected) =>
        _binaryCsp.ConstraintDensity.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Map Colouring binary CSP should have a harmonic mean constraint tightness of (.*)")]
    public void ThenTheMapColouringBinaryCspShouldHaveAHarmonicMeanConstraintTightnessOf(double expected) =>
        _binaryCsp.MeanTightness.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    private sealed record BlockItem(Block Block, Colour[] PermittedColours);

    private sealed record SolutionItem(Block Block, Colour Colour);
}
