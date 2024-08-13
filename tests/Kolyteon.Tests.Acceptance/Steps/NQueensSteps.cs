using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.NQueens;
using Kolyteon.Solving;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class NQueensSteps
{
    private readonly IBinaryCsp<int, Square, NQueensProblem> _binaryCsp;
    private readonly ScenarioContext _scenarioContext;
    private readonly IBinaryCspSolver<int, Square> _solver;

    public NQueensSteps(IBinaryCsp<int, Square, NQueensProblem> binaryCsp,
        IBinaryCspSolver<int, Square> solver,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _solver = solver ?? throw new ArgumentNullException(nameof(solver));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created an N-Queens problem for N = (.*)")]
    public void GivenIHaveCreatedAnNQueensProblemForN(int n)
    {
        NQueensProblem problem = NQueensProblem.FromN(n);

        _scenarioContext.Add(Constants.Keys.Problem, problem);
    }

    [Given("I have serialized the N-Queens problem to JSON")]
    public void GivenIHaveSerializedTheNQueensProblemToJson()
    {
        NQueensProblem problem = _scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following squares as a solution to the N-Queens problem")]
    public void GivenIHaveProposedTheFollowingSquaresAsASolutionToTheNQueensProblem(Table table)
    {
        IReadOnlyList<Square> proposedSolution = table.CreateSet<SolutionItem>().Select(item => item.Square).ToArray();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Given("I have modelled the N-Queens problem as a binary CSP")]
    public void GivenIHaveModelledTheNQueensProblemAsABinaryCsp()
    {
        NQueensProblem problem = _scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I deserialize an N-Queens problem from the JSON")]
    public void WhenIDeserializeAnNQueensProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        NQueensProblem? deserializedProblem = JsonSerializer.Deserialize<NQueensProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the N-Queens problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheNQueensProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        NQueensProblem problem = _scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);
        IReadOnlyList<Square> proposedSolution = _scenarioContext.Get<IReadOnlyList<Square>>(Constants.Keys.ProposedSolution);

        CheckingResult result = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, result);
    }

    [When("I model the N-Queens problem as a binary CSP")]
    public void WhenIModelTheNQueensProblemAsABinaryCsp()
    {
        NQueensProblem problem = _scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When(@"I solve the N-Queens binary CSP using the '(.*)'\+'(.*)' search algorithm")]
    public void WhenISolveTheNQueensBinaryCspUsingTheSearchAlgorithm(CheckingStrategy checking, OrderingStrategy ordering)
    {
        _solver.CheckingStrategy = checking;
        _solver.OrderingStrategy = ordering;

        SolvingResult<int, Square> result = _solver.Solve(_binaryCsp);

        Square[] proposedSolution = result.Assignments.ToNQueensSolution();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Then("the deserialized and original N-Queens problems should be equal")]
    public void ThenTheDeserializedAndOriginalNQueensProblemsShouldBeEqual()
    {
        NQueensProblem problem = _scenarioContext.Get<NQueensProblem>(Constants.Keys.Problem);
        NQueensProblem? deserializedProblem = _scenarioContext.Get<NQueensProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    [Then("the N-Queens binary CSP should have (.*) variables")]
    public void ThenTheNQueensBinaryCspShouldHaveVariables(int expected) => _binaryCsp.Variables.Should().Be(expected);

    [Then("the N-Queens binary CSP should have (.*) constraints")]
    public void ThenTheNQueensBinaryCspShouldHaveConstraints(int expected) => _binaryCsp.Constraints.Should().Be(expected);

    [Then("the N-Queens binary CSP should have a constraint density of (.*)")]
    public void ThenTheNQueensBinaryCspShouldHaveAConstraintDensityOf(double expected) =>
        _binaryCsp.ConstraintDensity.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the N-Queens binary CSP should have a harmonic mean constraint tightness of (.*)")]
    public void ThenTheNQueensBinaryCspShouldHaveAHarmonicMeanConstraintTightnessOf(double expected) =>
        _binaryCsp.MeanTightness.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    private sealed record SolutionItem(Square Square);
}
