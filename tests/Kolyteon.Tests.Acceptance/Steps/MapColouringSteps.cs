using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.MapColouring;
using Kolyteon.Modelling;
using Kolyteon.Solving;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class MapColouringSteps
{
    private readonly IBinaryCsp<Block, Colour, MapColouringProblem> _binaryCsp;
    private readonly IMapColouringGenerator _generator;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISilentBinaryCspSolver<Block, Colour> _solver;

    public MapColouringSteps(IBinaryCsp<Block, Colour, MapColouringProblem> binaryCsp,
        ISilentBinaryCspSolver<Block, Colour> solver,
        IMapColouringGenerator generator,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _solver = solver ?? throw new ArgumentNullException(nameof(solver));
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Map Colouring problem with a (.*) canvas and the following blocks")]
    public void GivenIHaveCreatedAMapColouringProblemWithACanvasAndTheFollowingBlocks(Dimensions size, Table table)
    {
        IMapColouringProblemBuilder.IBlockAndColoursAdder builder =
            MapColouringProblem.Create().WithCanvasSize(size).UseBlockSpecificColours();

        foreach ((Block block, Colour[] permittedColours) in table.CreateSet<BlockItem>())
        {
            builder = builder.AddBlockAndColours(block, permittedColours);
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

    [Given("I have set the Map Colouring generator seed value to (.*)")]
    public void GivenIHaveSetTheMapColouringGeneratorSeedValueTo(int seed) => _generator.UseSeed(seed);

    [Given("I have modelled the Map Colouring problem as a binary CSP")]
    public void GivenIHaveModelledTheMapColouringProblemAsABinaryCsp()
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
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

        Result verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Map Colouring problem as a binary CSP")]
    public void WhenIModelTheMapColouringProblemAsABinaryCsp()
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I ask the Map Colouring generator for a problem with (.*) blocks and the colours (.*)")]
    public void WhenIAskTheMapColouringGeneratorForAProblemWithBlocksAndTheColours(int blocks, HashSet<Colour> colours)
    {
        MapColouringProblem problem = _generator.Generate(blocks, colours);

        _scenarioContext.Add(Constants.Keys.Problem, problem);
    }

    [When(@"I solve the Map Colouring binary CSP using the '(.*)'\+'(.*)' search algorithm")]
    public void WhenISolveTheMapColouringBinaryCspUsingTheSearchAlgorithm(CheckingStrategy checking, OrderingStrategy ordering)
    {
        SearchAlgorithm searchAlgorithm = new(checking, ordering);

        SolvingResult<Block, Colour> result = _solver.Solve(_binaryCsp, searchAlgorithm, CancellationToken.None);

        Dictionary<Block, Colour> proposedSolution = result.Solution.ToMapColouringSolution();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
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

    [Then("the Map Colouring problem should have (.*) blocks")]
    public void ThenTheMapColouringProblemShouldHaveBlocks(int expectedBlocks)
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        problem.BlockData.Should().HaveCount(expectedBlocks);
    }

    [Then("every block in the Map Colouring problem should have the colours (.*)")]
    public void ThenEveryBlockInTheMapColouringProblemShouldHaveTheColours(HashSet<Colour> expectedColours)
    {
        MapColouringProblem problem = _scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        problem.BlockData.Should().AllSatisfy(datum =>
            datum.PermittedColours.Should().BeEquivalentTo(expectedColours, options => options.WithoutStrictOrdering()));
    }

    private sealed record BlockItem(Block Block, Colour[] PermittedColours);

    private sealed record SolutionItem(Block Block, Colour Colour);
}
