using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Modelling;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class GraphColouringSteps
{
    private readonly IBinaryCsp<Node, Colour, GraphColouringProblem> _binaryCsp;
    private readonly IGraphColouringGenerator _generator;
    private readonly ScenarioContext _scenarioContext;

    public GraphColouringSteps(IBinaryCsp<Node, Colour, GraphColouringProblem> binaryCsp,
        IGraphColouringGenerator generator,
        ScenarioContext scenarioContext)
    {
        _binaryCsp = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }

    [Given("I have created a Graph Colouring problem with the following nodes and edges")]
    public void GivenIHaveCreatedAGraphColouringProblemWithTheFollowingNodesAndEdges(Table table)
    {
        NodeItem[] nodeItems = table.CreateSet<NodeItem>().ToArray();
        IGraphColouringProblemBuilder.INodeAndColoursAdder builder = GraphColouringProblem.Create().UseNodeSpecificColours();

        foreach ((Node node, Colour[] permittedColours, Node[] _) in nodeItems)
        {
            builder.AddNodeAndColours(node, permittedColours);
        }

        foreach (Edge edge in GetEdges(nodeItems))
        {
            builder.AddEdge(edge);
        }

        _scenarioContext.Add(Constants.Keys.Problem, builder.Build());
    }

    [Given("I have serialized the Graph Colouring problem to JSON")]
    public void GivenIHaveSerializedTheGraphColouringProblemToJson()
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following node and colour dictionary as a solution to the Graph Colouring problem")]
    public void GivenIHaveProposedTheFollowingNodeAndColourDictionaryAsASolutionToTheGraphColouringProblem(Table table)
    {
        IReadOnlyDictionary<Node, Colour> proposedSolution = table.CreateSet<SolutionItem>()
            .ToDictionary(item => item.Node, item => item.Colour);

        _scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [Given("I have set the Graph Colouring generator seed value to (.*)")]
    public void GivenIHaveSetTheGraphColouringGeneratorSeedValueTo(int seed) => _generator.UseSeed(seed);

    [When("I deserialize a Graph Colouring problem from the JSON")]
    public void WhenIDeserializeAGraphColouringProblemFromTheJson()
    {
        string json = _scenarioContext.Get<string>(Constants.Keys.Json);

        GraphColouringProblem? deserializedProblem =
            JsonSerializer.Deserialize<GraphColouringProblem>(json, JsonSerializerOptions.Default);

        _scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Graph Colouring problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheGraphColouringProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);
        IReadOnlyDictionary<Node, Colour> proposedSolution =
            _scenarioContext.Get<IReadOnlyDictionary<Node, Colour>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        _scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [When("I model the Graph Colouring problem as a binary CSP")]
    public void WhenIModelTheGraphColouringProblemAsABinaryCsp()
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);

        _binaryCsp.Model(problem);
    }

    [When("I ask the Graph Colouring generator for a problem with (.*) nodes and the colours (.*)")]
    public void WhenIAskTheGraphColouringGeneratorForAProblemWithNodesAndTheColours(int nodes, HashSet<Colour> permittedColours)
    {
        GraphColouringProblem problem = _generator.Generate(nodes, permittedColours);

        _scenarioContext.Add(Constants.Keys.Problem, problem);
    }

    [Then("the deserialized and original Graph Colouring problems should be equal")]
    public void ThenTheDeserializedAndOriginalGraphColouringProblemsShouldBeEqual()
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);
        GraphColouringProblem? deserializedProblem =
            _scenarioContext.Get<GraphColouringProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    [Then("the Graph Colouring binary CSP should have (.*) variables")]
    public void ThenTheGraphColouringBinaryCspShouldHaveVariables(int expected) => _binaryCsp.Variables.Should().Be(expected);

    [Then("the Graph Colouring binary CSP should have (.*) constraints")]
    public void ThenTheGraphColouringBinaryCspShouldHaveConstraints(int expected) =>
        _binaryCsp.Constraints.Should().Be(expected);

    [Then("the Graph Colouring binary CSP should have a constraint density of (.*)")]
    public void ThenTheGraphColouringBinaryCspShouldHaveAConstraintDensityOf(double expected) =>
        _binaryCsp.ConstraintDensity.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Graph Colouring binary CSP should have a harmonic mean tightness of (.*)")]
    public void ThenTheGraphColouringBinaryCspShouldHaveAHarmonicMeanTightnessOf(double expected) =>
        _binaryCsp.MeanTightness.Should().BeApproximately(expected, Constants.Precision.SixDecimalPlaces);

    [Then("the Graph Colouring problem should have (.*) nodes")]
    public void ThenTheGraphColouringProblemShouldHaveNodes(int expectedNodes)
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);

        problem.NodeData.Should().HaveCount(expectedNodes);
    }

    [Then("the Graph Colouring problem should have at least (.*) edge")]
    public void ThenTheGraphColouringProblemShouldHaveAtLeastEdge(int minEdges)
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);

        problem.Edges.Should().HaveCountGreaterOrEqualTo(minEdges);
    }

    [Then("every node in the Graph Colouring problem should have the colours (.*)")]
    public void ThenEveryNodeInTheGraphColouringProblemShouldHaveTheColours(HashSet<Colour> expectedColours)
    {
        GraphColouringProblem problem = _scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);

        problem.NodeData.Should().AllSatisfy(datum =>
            datum.PermittedColours.Should().BeEquivalentTo(expectedColours, options => options.WithoutStrictOrdering()));
    }

    private static IEnumerable<Edge> GetEdges(NodeItem[] nodeItems) => nodeItems.SelectMany(item =>
        item.AdjacentNodes.Select(adjacentNode => Edge.Between(item.Node, adjacentNode)));

    private sealed record NodeItem(Node Node, Colour[] PermittedColours, Node[] AdjacentNodes);

    private sealed record SolutionItem(Node Node, Colour Colour);
}
