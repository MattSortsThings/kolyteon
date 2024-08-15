using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.Solving;
using Kolyteon.Sudoku;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class SudokuSteps
{
    private readonly IBinaryCsp<Square, int, SudokuProblem> _binaryCsp;
    private readonly ISudokuGenerator _generator;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISilentBinaryCspSolver<Square, int> _solver;

    public SudokuSteps(IBinaryCsp<Square, int, SudokuProblem> binaryCsp,
        ISilentBinaryCspSolver<Square, int> solver,
        ISudokuGenerator generator,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _solver = solver ?? throw new ArgumentNullException(nameof(solver));
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Sudoku problem from the following grid")]
    public void GivenIHaveCreatedASudokuProblemFromTheFollowingGrid(SudokuProblem problem) =>
        _scenarioContext.Add(Constants.Keys.Problem, problem);

    [Given("I have serialized the Sudoku problem to JSON")]
    public void GivenIHaveSerializedTheSudokuProblemToJson()
    {
        SudokuProblem problem = _scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following filled squares as a solution to the Sudoku problem")]
    public void GivenIHaveProposedTheFollowingFilledSquaresAsASolutionToTheSudokuProblem(Table table)
    {
        IReadOnlyList<NumberedSquare> proposedSolution = table.CreateSet<SolutionItem>()
            .Select(item => item.FilledSquare)
            .ToArray();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Given("I have set the Sudoku generator seed value to (.*)")]
    public void GivenIHaveSetTheSudokuGeneratorSeedValueTo(int seed) => _generator.UseSeed(seed);

    [Given("I have modelled the Sudoku problem as a binary CSP")]
    public void GivenIHaveModelledTheSudokuProblemAsABinaryCsp()
    {
        SudokuProblem problem = _scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I deserialize a Sudoku problem from the JSON")]
    public void WhenIDeserializeASudokuProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        SudokuProblem? deserializedProblem = JsonSerializer.Deserialize<SudokuProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Sudoku problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheSudokuProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        SudokuProblem problem = _scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);
        IReadOnlyList<NumberedSquare> proposedSolution =
            _scenarioContext.Get<IReadOnlyList<NumberedSquare>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Sudoku problem as a binary CSP")]
    public void WhenIModelTheSudokuProblemAsABinaryCsp()
    {
        SudokuProblem problem = _scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I ask the Sudoku generator for a problem with (.*) empty squares")]
    public void WhenIAskTheSudokuGeneratorForAProblemWithEmptySquares(int emptySquares)
    {
        SudokuProblem problem = _generator.Generate(emptySquares);

        _scenarioContext.Add(Constants.Keys.Problem, problem);
    }

    [When(@"I solve the Sudoku binary CSP using the '(.*)'\+'(.*)' search algorithm")]
    public void WhenISolveTheSudokuBinaryCspUsingTheSearchAlgorithm(CheckingStrategy checking, OrderingStrategy ordering)
    {
        _solver.CheckingStrategy = checking;
        _solver.OrderingStrategy = ordering;

        SolvingResult<Square, int> result = _solver.Solve(_binaryCsp);

        NumberedSquare[] proposedSolution = result.Assignments.ToSudokuSolution();

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Then("the deserialized and original Sudoku problems should be equal")]
    public void ThenTheDeserializedAndOriginalSudokuProblemsShouldBeEqual()
    {
        SudokuProblem problem = _scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);
        SudokuProblem? deserializedProblem = _scenarioContext.Get<SudokuProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    [Then("the Sudoku binary CSP should have (.*) variables")]
    public void ThenTheSudokuBinaryCspShouldHaveVariables(int expected) => _binaryCsp.Variables.Should().Be(expected);

    [Then("the Sudoku binary CSP should have (.*) constraints")]
    public void ThenTheSudokuBinaryCspShouldHaveConstraints(int expected) => _binaryCsp.Constraints.Should().Be(expected);

    [Then("the Sudoku binary CSP should have a constraint density of (.*)")]
    public void ThenTheSudokuBinaryCspShouldHaveAConstraintDensityOf(double expected) =>
        _binaryCsp.ConstraintDensity.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Sudoku binary CSP should have a harmonic mean constraint tightness of (.*)")]
    public void ThenTheSudokuBinaryCspShouldHaveAHarmonicMeanConstraintTightnessOf(double expected) =>
        _binaryCsp.MeanTightness.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Sudoku problem should have (.*) filled squares")]
    public void ThenTheSudokuProblemShouldHaveFilledSquares(int expected)
    {
        SudokuProblem problem = _scenarioContext.Get<SudokuProblem>(Constants.Keys.Problem);

        problem.FilledSquares.Should().HaveCount(expected);
    }

    private readonly record struct SolutionItem(NumberedSquare FilledSquare);
}
