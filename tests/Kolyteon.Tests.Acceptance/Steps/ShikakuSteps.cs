using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.Shikaku;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class ShikakuSteps
{
    private readonly IBinaryCsp<NumberedSquare, Block, ShikakuProblem> _binaryCsp;
    private readonly ScenarioContext _scenarioContext;

    public ShikakuSteps(IBinaryCsp<NumberedSquare, Block, ShikakuProblem> binaryCsp, ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
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

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Shikaku problem as a binary CSP")]
    public void WhenIModelTheShikakuProblemAsABinaryCsp()
    {
        ShikakuProblem problem = _scenarioContext.Get<ShikakuProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
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

    private sealed record SolutionItem(Block Block);
}
