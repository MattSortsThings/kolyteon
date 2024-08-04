using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Modelling;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class FutoshikiSteps
{
    private readonly IBinaryCsp<Square, int, FutoshikiProblem> _binaryCsp;
    private readonly ScenarioContext _scenarioContext;

    public FutoshikiSteps(IBinaryCsp<Square, int, FutoshikiProblem> binaryCsp, ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Futoshiki problem matching the following diagram")]
    public void GivenIHaveCreatedAFutoshikiProblemMatchingTheFollowingDiagram(FutoshikiProblem problem) =>
        _scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Futoshiki problem to JSON")]
    public void GivenIHaveSerializedTheFutoshikiProblemToJson()
    {
        FutoshikiProblem problem = _scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following filled squares as a solution to the Futoshiki problem")]
    public void GivenIHaveProposedTheFollowingFilledSquaresAsASolutionToTheFutoshikiProblem(Table table)
    {
        IReadOnlyList<NumberedSquare> proposedSolution = table.CreateSet<SolutionItem>()
            .Select(item => item.FilledSquare)
            .ToArray();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Futoshiki problem from the JSON")]
    public void WhenIDeserializeAFutoshikiProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        FutoshikiProblem? deserializedProblem =
            JsonSerializer.Deserialize<FutoshikiProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Futoshiki problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheFutoshikiProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        FutoshikiProblem problem = _scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);
        IReadOnlyList<NumberedSquare> proposedSolution =
            _scenarioContext.Get<IReadOnlyList<NumberedSquare>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Futoshiki problem as a binary CSP")]
    public void WhenIModelTheFutoshikiProblemAsABinaryCsp()
    {
        FutoshikiProblem problem = _scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [Then("the deserialized and original Futoshiki problems should be equal")]
    public void ThenTheDeserializedAndOriginalFutoshikiProblemsShouldBeEqual()
    {
        FutoshikiProblem problem = _scenarioContext.Get<FutoshikiProblem>(Constants.Keys.Problem);
        FutoshikiProblem? deserializedProblem = _scenarioContext.Get<FutoshikiProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    [Then("the Futoshiki binary CSP should have (.*) variables")]
    public void ThenTheFutoshikiBinaryCspShouldHaveVariables(int expected) => _binaryCsp.Variables.Should().Be(expected);

    [Then("the Futoshiki binary CSP should have (.*) constraints")]
    public void ThenTheFutoshikiBinaryCspShouldHaveConstraints(int expected) => _binaryCsp.Constraints.Should().Be(expected);

    [Then("the Futoshiki binary CSP should have a constraint density of (.*)")]
    public void ThenTheFutoshikiBinaryCspShouldHaveAConstraintDensityOf(double expected) =>
        _binaryCsp.ConstraintDensity.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Futoshiki binary CSP should have a harmonic mean tightness of (.*)")]
    public void ThenTheFutoshikiBinaryCspShouldHaveAHarmonicMeanTightnessOf(double expected) =>
        _binaryCsp.MeanTightness.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    private readonly record struct SolutionItem(NumberedSquare FilledSquare);
}
