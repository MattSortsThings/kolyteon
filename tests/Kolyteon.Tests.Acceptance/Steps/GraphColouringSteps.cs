using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class GraphColouringSteps(ScenarioContext scenarioContext)
{
    [Given(@"I have created a Graph Colouring problem with the following nodes and edges")]
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

        scenarioContext.Add(Constants.Keys.Problem, builder.Build());
    }

    [Given("I have serialized the Graph Colouring problem to JSON")]
    public void GivenIHaveSerializedTheGraphColouringProblemToJson()
    {
        GraphColouringProblem problem = scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following node and colour dictionary as a solution to the Graph Colouring problem")]
    public void GivenIHaveProposedTheFollowingNodeAndColourDictionaryAsASolutionToTheGraphColouringProblem(Table table)
    {
        IReadOnlyDictionary<Node, Colour> proposedSolution = table.CreateSet<SolutionItem>()
            .ToDictionary(item => item.Node, item => item.Colour);

        scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Graph Colouring problem from the JSON")]
    public void WhenIDeserializeAGraphColouringProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        GraphColouringProblem? deserializedProblem =
            JsonSerializer.Deserialize<GraphColouringProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Graph Colouring problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheGraphColouringProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        GraphColouringProblem problem = scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);
        IReadOnlyDictionary<Node, Colour> proposedSolution =
            scenarioContext.Get<IReadOnlyDictionary<Node, Colour>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [Then("the deserialized and original Graph Colouring problems should be equal")]
    public void ThenTheDeserializedAndOriginalGraphColouringProblemsShouldBeEqual()
    {
        GraphColouringProblem problem = scenarioContext.Get<GraphColouringProblem>(Constants.Keys.Problem);
        GraphColouringProblem? deserializedProblem =
            scenarioContext.Get<GraphColouringProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    private static IEnumerable<Edge> GetEdges(NodeItem[] nodeItems) => nodeItems.SelectMany(item =>
        item.AdjacentNodes.Select(adjacentNode => Edge.Between(item.Node, adjacentNode)));

    private sealed record NodeItem(Node Node, Colour[] PermittedColours, Node[] AdjacentNodes);

    private sealed record SolutionItem(Node Node, Colour Colour);
}