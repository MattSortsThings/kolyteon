using Mjt85.Kolyteon.MapColouring;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class MapColouringDomains : TheoryData<MapColouringPuzzle, IEnumerable<IReadOnlyList<Colour>>>
{
    public MapColouringDomains()
    {
        Region X1 = Region.FromId("X1");
        Region X2 = Region.FromId("X2");
        Region X3 = Region.FromId("X3");
        Region X4 = Region.FromId("X4");
        Region X5 = Region.FromId("X5");
        Region X6 = Region.FromId("X6");
        Region X7 = Region.FromId("X7");

        Add(MapColouringPuzzle.Create()
            .WithGlobalColours(Colour.White, Colour.Black)
            .AddRegion(X1)
            .Build(), [
            [Colour.Black, Colour.White]
        ]);

        Add(MapColouringPuzzle.Create()
            .WithGlobalColours(Colour.White, Colour.Black)
            .AddRegion(X1)
            .AddRegion(X2)
            .Build(), [
            [Colour.Black, Colour.White],
            [Colour.Black, Colour.White]
        ]);

        Add(MapColouringPuzzle.Create()
            .WithRegionSpecificColours()
            .AddRegionWithColours(X1)
            .AddRegionWithColours(X2, Colour.Red)
            .AddRegionWithColours(X3, Colour.Black, Colour.White, Colour.Red, Colour.White)
            .Build(), [
            [],
            [Colour.Red],
            [Colour.Black, Colour.Red, Colour.White]
        ]);

        Add(MapColouringPuzzle.Create()
            .WithRegionSpecificColours()
            .AddRegionWithColours(X3, Colour.Black, Colour.White)
            .AddRegionWithColours(X1)
            .AddRegionWithColours(X2, Colour.Red)
            .Build(), [
            [],
            [Colour.Red],
            [Colour.Black, Colour.White]
        ]);

        Add(MapColouringPuzzle.Create()
            .WithRegionSpecificColours()
            .AddRegionWithColours(X1, Colour.Red, Colour.Blue, Colour.Green)
            .AddRegionWithColours(X2, Colour.Blue, Colour.Green)
            .AddRegionWithColours(X3, Colour.Red, Colour.Blue)
            .AddRegionWithColours(X4, Colour.Red, Colour.Blue)
            .AddRegionWithColours(X5, Colour.Blue, Colour.Green)
            .AddRegionWithColours(X6, Colour.Red, Colour.Green, Colour.Yellow)
            .AddRegionWithColours(X7, Colour.Red, Colour.Blue)
            .SetAsNeighbours(X1, X2)
            .SetAsNeighbours(X1, X3)
            .SetAsNeighbours(X1, X4)
            .SetAsNeighbours(X1, X7)
            .SetAsNeighbours(X2, X6)
            .SetAsNeighbours(X3, X7)
            .SetAsNeighbours(X4, X5)
            .SetAsNeighbours(X4, X7)
            .SetAsNeighbours(X5, X6)
            .SetAsNeighbours(X5, X7)
            .Build(), [
            [Colour.Blue, Colour.Green, Colour.Red],
            [Colour.Blue, Colour.Green],
            [Colour.Blue, Colour.Red],
            [Colour.Blue, Colour.Red],
            [Colour.Blue, Colour.Green],
            [Colour.Green, Colour.Red, Colour.Yellow],
            [Colour.Blue, Colour.Red]
        ]);
    }
}
