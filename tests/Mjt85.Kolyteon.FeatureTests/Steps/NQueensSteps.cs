using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.FeatureTests.Helpers;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.Solving;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

[Binding]
public sealed class NQueensSteps
{
    private readonly IModellingBinaryCsp<NQueensPuzzle, int, Queen> _binaryCsp;
    private readonly IBinaryCspSolver<int, Queen> _binaryCspSolver;
    private readonly ScenarioContext _scenarioContext;

    public NQueensSteps(IModellingBinaryCsp<NQueensPuzzle, int, Queen> binaryCsp,
        IBinaryCspSolver<int, Queen> binaryCspSolver,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _binaryCspSolver = binaryCspSolver ?? throw new ArgumentNullException(nameof(binaryCspSolver));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created an N-Queens puzzle in which N = (.*)")]
    public void GivenIHaveCreatedAnN_QueensPuzzleInWhichN_(int n)
    {
        NQueensPuzzle puzzle = NQueensPuzzle.FromN(n);

        _scenarioContext.Add(Invariants.PUZZLE, puzzle);
    }

    [Given("I have serialized the N-Queens puzzle to JSON")]
    public void GivenIHaveSerializedTheN_QueensPuzzleToJson()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);

        var json = JsonSerializer.Serialize(puzzle, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.JSON, json);
    }

    [Given("I have obtained the following list of queens as a proposed solution to the N-Queens puzzle")]
    public void GivenIHaveObtainedTheFollowingListOfQueensAsAProposedSolutionToTheN_QueensPuzzle(Table table)
    {
        IReadOnlyList<Queen> proposedSolution = table.CreateSet<(int Column, int Row)>()
            .Select(item => new Queen(item.Column, item.Row))
            .ToArray();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [Given("I have modelled the N-Queens puzzle as a binary CSP")]
    public void GivenIHaveModelledTheN_QueensPuzzleAsABinaryCsp()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);
        _binaryCsp.Model(puzzle);
    }

    [Given("I have set the N-Queens binary CSP solver to use the '(.*)' search strategy")]
    public void GivenIHaveSetTheN_QueensBinaryCspSolverToUseTheSearchStrategy(Search strategy)
    {
        _binaryCspSolver.SearchStrategy = strategy;
    }

    [Given("I have set the N-Queens binary CSP solver to use the '(.*)' ordering strategy")]
    public void GivenIHaveSetTheN_QueensBinaryCspSolverToUseTheOrderingStrategy(Ordering strategy)
    {
        _binaryCspSolver.OrderingStrategy = strategy;
    }

    [When("I deserialize an N-Queens puzzle from the JSON")]
    public void WhenIDeserializeAnN_QueensPuzzleFromTheJson()
    {
        var json = _scenarioContext.Get<string>(Invariants.JSON);

        var deserializedPuzzle = JsonSerializer.Deserialize<NQueensPuzzle>(json, Invariants.GetJsonSerializerOptions());

        _scenarioContext.Add(Invariants.DESERIALIZED_PUZZLE, deserializedPuzzle);
    }

    [When("I ask the N-Queens puzzle to validate the proposed solution")]
    public void WhenIAskTheN_QueensPuzzleToValidateTheProposedSolution()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);
        IReadOnlyList<Queen>? proposedSolution = _scenarioContext.Get<IReadOnlyList<Queen>>(Invariants.PROPOSED_SOLUTION);

        ValidationResult? validationResult = puzzle.ValidSolution(proposedSolution);

        _scenarioContext.Add(Invariants.VALIDATION_RESULT, validationResult);
    }

    [When("I request the binary CSP metrics for the N-Queens puzzle")]
    public void WhenIRequestTheBinaryCspMetricsForTheN_QueensPuzzle()
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

    [When("I run the N-Queens binary CSP solver on the binary CSP")]
    public void WhenIRunTheN_QueensBinaryCspSolverOnTheBinaryCsp()
    {
        Result<int, Queen> result = _binaryCspSolver.Solve(_binaryCsp);
        IReadOnlyList<Queen> proposedSolution = result.Assignments.ToPuzzleSolution();

        _scenarioContext.Add(Invariants.PROPOSED_SOLUTION, proposedSolution);
    }

    [Then("the deserialized N-Queens puzzle should be the same as the original puzzle")]
    public void ThenTheDeserializedN_QueensPuzzleShouldBeTheSameAsTheOriginalPuzzle()
    {
        var puzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.PUZZLE);
        var deserializedPuzzle = _scenarioContext.Get<NQueensPuzzle>(Invariants.DESERIALIZED_PUZZLE);

        deserializedPuzzle.Should().NotBeNull().And.Be(puzzle);
    }
}
