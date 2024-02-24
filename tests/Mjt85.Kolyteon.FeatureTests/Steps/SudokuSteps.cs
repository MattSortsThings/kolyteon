using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.Sudoku;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class SudokuSteps
{
    private readonly IModellingBinaryCsp<SudokuPuzzle, EmptyCell, int> _binaryCsp;
    private readonly IBinaryCspSolver<EmptyCell, int> _binaryCspSolver;
    private readonly ScenarioContext _scenarioContext;

    public SudokuSteps(IModellingBinaryCsp<SudokuPuzzle, EmptyCell, int> binaryCsp,
        IBinaryCspSolver<EmptyCell, int> binaryCspSolver,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _binaryCspSolver = binaryCspSolver ?? throw new ArgumentNullException(nameof(binaryCspSolver));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Sudoku puzzle from the following grid")]
    public void GivenIHaveCreatedASudokuPuzzleFromTheFollowingGrid(SudokuPuzzle puzzle)
    {
        SudokuPuzzle x = puzzle;
        _scenarioContext.Add(Invariants.PUZZLE, puzzle);
    }

    [Given("I have serialized the Sudoku puzzle to JSON")]
    public void GivenIHaveSerializedTheSudokuPuzzleToJson()
    {
        var puzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.PUZZLE);

        var json = JsonSerializer.Serialize(puzzle, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [Given("I have obtained the following list of filled cells as a proposed solution to the Sudoku puzzle")]
    public void GivenIHaveObtainedTheFollowingListOfFilledCellsAsAProposedSolutionToTheSudokuPuzzle(Table table)
    {
        IReadOnlyList<FilledCell> proposedSolution = table.CreateSet<(int Column, int Row, int Number)>()
            .Select(item => new FilledCell(item.Column, item.Row, item.Number))
            .ToArray();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [Given("I have modelled the Sudoku puzzle as a binary CSP")]
    public void GivenIHaveModelledTheSudokuPuzzleAsABinaryCsp()
    {
        var puzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.PUZZLE);
        _binaryCsp.Model(puzzle);
    }

    [Given("I have set the Sudoku binary CSP solver to use the '(.*)' search strategy")]
    public void GivenIHaveSetTheSudokuBinaryCspSolverToUseTheSearchStrategy(Search strategy)
    {
        _binaryCspSolver.SearchStrategy = strategy;
    }

    [Given("I have set the Sudoku binary CSP solver to use the '(.*)' ordering strategy")]
    public void GivenIHaveSetTheSudokuBinaryCspSolverToUseTheOrderingStrategy(Ordering strategy)
    {
        _binaryCspSolver.OrderingStrategy = strategy;
    }

    [When("I deserialize a Sudoku puzzle from the JSON")]
    public void WhenIDeserializeASudokuPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<SudokuPuzzle>(json, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [When("I ask the Sudoku puzzle to validate the proposed solution")]
    public void WhenIAskTheSudokuPuzzleToValidateTheProposedSolution()
    {
        var puzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.PUZZLE);
        IReadOnlyList<FilledCell>? proposedSolution =
            _scenarioContext.Get<IReadOnlyList<FilledCell>>(Invariants.PROPOSED_SOLUTION);

        ValidationResult? validationResult = puzzle.ValidSolution(proposedSolution);

        _scenarioContext.Add(Invariants.VALIDATION_RESULT, validationResult);
    }

    [When("I request the binary CSP metrics for the Sudoku puzzle")]
    public void WhenIRequestTheBinaryCspMetricsForTheSudokuPuzzle()
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

    [When("I run the Sudoku binary CSP solver on the binary CSP")]
    public void WhenIRunTheSudokuBinaryCspSolverOnTheBinaryCsp()
    {
        Result<EmptyCell, int> result = _binaryCspSolver.Solve(_binaryCsp);
        IReadOnlyList<FilledCell> proposedSolution = result.Assignments.ToPuzzleSolution();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [Then("the deserialized Sudoku puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedSudokuPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<SudokuPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }

    [StepArgumentTransformation]
    public static SudokuPuzzle SudokuPuzzleTransform(string multiLineText)
    {
        var lines = multiLineText.Split('\n')
            .Select(line => line.Trim())
            .Select(line => line.Split(" ").ToArray())
            .ToArray();

        var length = lines.Length;

        var grid = new int?[length, length];

        for (var column = 0; column < length; column++)
        {
            for (var row = 0; row < length; row++)
            {
                if (int.TryParse(lines[row][column], out var number))
                {
                    grid[row, column] = number;
                }
            }
        }

        return SudokuPuzzle.FromGrid(grid);
    }
}
