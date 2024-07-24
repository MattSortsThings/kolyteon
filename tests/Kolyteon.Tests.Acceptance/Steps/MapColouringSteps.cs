using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.MapColouring;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.Steps;

[Binding]
internal sealed class MapColouringSteps(ScenarioContext scenarioContext)
{
    [Given("I have created a Map Colouring problem with a (.*) canvas and the following blocks")]
    public void GivenIHaveCreatedAMapColouringProblemWithACanvasAndTheFollowingBlocks(Dimensions size, Table table)
    {
        IMapColouringProblemBuilder.IBlockAndColoursAdder builder =
            MapColouringProblem.Create().WithCanvasSize(size).UseBlockSpecificColours();

        foreach ((Block block, Colour[] permittedColours) in table.CreateSet<BlockItem>())
        {
            builder = builder.AddBlockWithColours(block, permittedColours);
        }

        scenarioContext.Add(Constants.Keys.Problem, builder.Build());
    }

    [Given("I have serialized the Map Colouring problem to JSON")]
    public void GivenIHaveSerializedTheMapColouringProblemToJson()
    {
        MapColouringProblem problem = scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);

        string json = JsonSerializer.Serialize(problem, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.Json, json);
    }

    [Given("I have proposed the following block and colour dictionary as a solution to the Map Colouring problem")]
    public void GivenIHaveProposedTheFollowingBlockAndColourDictionaryAsASolutionToTheMapColouringProblem(Table table)
    {
        IReadOnlyDictionary<Block, Colour> proposedSolution = table.CreateSet<SolutionItem>()
            .ToDictionary(item => item.Block, item => item.Colour);

        scenarioContext.Add(Constants.Keys.ProposedSolution, proposedSolution);
    }

    [When("I deserialize a Map Colouring problem from the JSON")]
    public void WhenIDeserializeAMapColouringProblemFromTheJson()
    {
        string json = scenarioContext.Get<string>(Constants.Keys.Json);

        MapColouringProblem? deserializedProblem =
            JsonSerializer.Deserialize<MapColouringProblem>(json, JsonSerializerOptions.Default);

        scenarioContext.Add(Constants.Keys.DeserializedProblem, deserializedProblem);
    }

    [When("I ask the Map Colouring problem to verify the correctness of the proposed solution")]
    public void WhenIAskTheMapColouringProblemToVerifyTheCorrectnessOfTheProposedSolution()
    {
        MapColouringProblem problem = scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);
        IReadOnlyDictionary<Block, Colour> proposedSolution =
            scenarioContext.Get<IReadOnlyDictionary<Block, Colour>>(Constants.Keys.ProposedSolution);

        CheckingResult verificationResult = problem.VerifyCorrect(proposedSolution);

        scenarioContext.Add(Constants.Keys.VerificationResult, verificationResult);
    }

    [Then("the deserialized and original Map Colouring problems should be equal")]
    public void ThenTheDeserializedAndOriginalMapColouringProblemsShouldBeEqual()
    {
        MapColouringProblem problem = scenarioContext.Get<MapColouringProblem>(Constants.Keys.Problem);
        MapColouringProblem? deserializedProblem = scenarioContext.Get<MapColouringProblem?>(Constants.Keys.DeserializedProblem);

        deserializedProblem.Should().NotBeNull().And.Be(problem);
    }

    private sealed record BlockItem(Block Block, Colour[] PermittedColours);

    private sealed record SolutionItem(Block Block, Colour Colour);
}
