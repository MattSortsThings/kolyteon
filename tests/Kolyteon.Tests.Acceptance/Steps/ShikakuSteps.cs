using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.Shikaku;
using Kolyteon.Solving;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class ShikakuSteps
{
    private readonly IBinaryCsp<NumberedSquare, Block, ShikakuProblem> _binaryCsp;
    private readonly IShikakuGenerator _generator;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISilentBinaryCspSolver<NumberedSquare, Block> _solver;

    public ShikakuSteps(IBinaryCsp<NumberedSquare, Block, ShikakuProblem> binaryCsp,
        ISilentBinaryCspSolver<NumberedSquare, Block> solver,
        IShikakuGenerator generator,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _solver = solver ?? throw new ArgumentNullException(nameof(solver));
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Shikaku problem from the following grid")]
    public void GivenIHaveCreatedAShikakuProblemFromTheFollowingGrid(ShikakuProblem problem) =>
        _scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Shikaku problem to JSON")]
    public void GivenIHaveSerializedTheShikakuProblemToJson()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following blocks as a solution to the Shikaku problem")]
    public void GivenIHaveProposedTheFollowingBlocksAsASolutionToTheShikakuProblem(Table table)
    {
        Block[] proposedSolution = table.CreateSet<SolutionItem>().Select(item => item.Block).ToArray();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Given("I have set the Shikaku generator seed value to (.*)")]
    public void GivenIHaveSetTheShikakuGeneratorSeedValueTo(int seed) => _generator.UseSeed(seed);

    [Given("I have modelled the Shikaku problem as a binary CSP")]
    public void GivenIHaveModelledTheShikakuProblemAsABinaryCsp()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I deserialize a Shikaku problem from the JSON")]
    public void WhenIDeserializeAShikakuProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        ShikakuProblem? deserializedProblem = JsonSerializer.Deserialize<ShikakuProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Shikaku problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheShikakuProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        IReadOnlyList<Block> proposedSolution = _scenarioContext.Get<IReadOnlyList<Block>>(Constants.Keys.ProposedSolution);

        Result verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Shikaku problem as a binary CSP")]
    public void WhenIModelTheShikakuProblemAsABinaryCsp()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I ask the Shikaku generator for a problem with a grid side length of (.*) and (.*) hints")]
    public void WhenIAskTheShikakuGeneratorForAProblemWithAGridSideLengthOfAndHints(int gridSideLength, int hints)
    {
        ShikakuProblem problem = _generator.Generate(gridSideLength, hints);

        _scenarioContext.Add(Constants.Keys.Problem, problem);
    }

    [When(@"I solve the Shikaku binary CSP using the '(.*)'\+'(.*)' search algorithm")]
    public void WhenISolveTheShikakuBinaryCspUsingTheSearchAlgorithm(CheckingStrategy checking, OrderingStrategy ordering)
    {
        SearchAlgorithm searchAlgorithm = new(checking, ordering);

        SolvingResult<NumberedSquare, Block> result = _solver.Solve(_binaryCsp, searchAlgorithm, CancellationToken.None);

        Block[] proposedSolution = result.Solution.ToShikakuSolution();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Then("the deserialized and original Shikaku problems should be equal")]
    public void ThenTheDeserializedAndOriginalShikakuProblemsShouldBeEqual()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);
        ShikakuProblem? deserializedProblem = _scenarioContext.Get<ShikakuProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    [Then("the Shikaku binary CSP should have (.*) variables")]
    public void ThenTheShikakuBinaryCspShouldHaveVariables(int expected) => _binaryCsp.Variables.Should().Be(expected);

    [Then("the Shikaku binary CSP should have (.*) constraints")]
    public void ThenTheShikakuBinaryCspShouldHaveConstraints(int expected) => _binaryCsp.Constraints.Should().Be(expected);

    [Then("the Shikaku binary CSP should have a constraint density of (.*)")]
    public void ThenTheShikakuBinaryCspShouldHaveAConstraintDensityOf(double expected) =>
        _binaryCsp.ConstraintDensity.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Shikaku binary CSP should have a harmonic mean constraint tightness of (.*)")]
    public void ThenTheShikakuBinaryCspShouldHaveAHarmonicMeanConstraintTightnessOf(double expected) =>
        _binaryCsp.MeanTightness.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Shikaku problem should have (.*) hints")]
    public void ThenTheShikakuProblemShouldHaveHints(int expectedHints)
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        problem.Hints.Should().HaveCount(expectedHints);
    }

    [Then("the Shikaku problem should have a (.*) grid")]
    public void ThenTheShikakuProblemShouldHaveAGrid(Dimensions expectedGridDimensions)
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        problem.Grid.Dimensions.Should().Be(expectedGridDimensions);
    }

    private sealed record SolutionItem(Block Block);
}
