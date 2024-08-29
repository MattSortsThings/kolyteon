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
    private readonly NQueensSolvingProgressReporter _progressReporter;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISilentBinaryCspSolver<int, Square> _solver;
    private readonly IVerboseBinaryCspSolver<int, Square> _verboseSolver;

    public NQueensSteps(IBinaryCsp<int, Square, NQueensProblem> binaryCsp,
        ScenarioContext scenarioContext,
        ISilentBinaryCspSolver<int, Square> solver,
        IVerboseBinaryCspSolver<int, Square> verboseSolver,
        NQueensSolvingProgressReporter progressReporter)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
        _solver = solver ?? throw new ArgumentNullException(nameof(solver));
        _verboseSolver = verboseSolver ?? throw new ArgumentNullException(nameof(verboseSolver));
        _progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
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
        SearchAlgorithm searchAlgorithm = new(checking, ordering);

        SolvingResult<int, Square> result =
            _solver.Solve(_binaryCsp, searchAlgorithm, CancellationToken.None);

        Square[] proposedSolution = result.Assignments.ToNQueensSolution();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When(@"I solve the N-Queens binary CSP using the verbose solver configured with the '(.*)'\+'(.*)' search algorithm")]
    public async Task WhenISolveTheNQueensBinaryCspUsingTheVerboseSolverConfiguredWithTheSearchAlgorithm(
        CheckingStrategy checking, OrderingStrategy ordering)
    {
        SearchAlgorithm searchAlgorithm = new(checking, ordering);

        SolvingResult<int, Square> result = await _verboseSolver.SolveAsync(_binaryCsp, _progressReporter, searchAlgorithm);

        Square[] proposedSolution = result.Assignments.ToNQueensSolution();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
        _scenarioContext.Add(Constants.Keys.SolvingProgressReports, _progressReporter.Reports.ToArray());
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

    [Then("the solving progress reports should be as follows")]
    public void ThenTheSolvingProgressReportsShouldBeAsFollows(Table table)
    {
        NQueensSolvingProgressReport[] expected = table.CreateSet<NQueensSolvingProgressReport>().ToArray();

        NQueensSolvingProgressReport[] progressReports =
            _scenarioContext.Get<NQueensSolvingProgressReport[]>(Constants.Keys.SolvingProgressReports);

        progressReports.Should().Equal(expected);
    }

    [Then("the solving progress reporter should have (.*) total step(?:s)?")]
    public void ThenTheSolvingProgressReporterShouldHaveTotalSteps(int expected) =>
        _progressReporter.TotalSteps.Should().Be(expected);

    [Then("the solving progress reporter should have (.*) simplifying step(?:s)?")]
    public void ThenTheSolvingProgressReporterShouldHaveSimplifyingSteps(int expected) =>
        _progressReporter.SimplifyingSteps.Should().Be(expected);

    [Then("the solving progress reporter should have (.*) assigning step(?:s)?")]
    public void ThenTheSolvingProgressReporterShouldHaveAssigningSteps(int expected) =>
        _progressReporter.AssigningSteps.Should().Be(expected);

    [Then("the solving progress reporter should have (.*) backtracking step(?:s)?")]
    public void ThenTheSolvingProgressReporterShouldHaveBacktrackingSteps(int expected) =>
        _progressReporter.BacktrackingSteps.Should().Be(expected);

    private sealed record SolutionItem(Square Square);
}
