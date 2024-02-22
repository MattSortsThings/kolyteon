using Mjt85.Kolyteon.MapColouring;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class MapColouringVariables : TheoryData<MapColouringPuzzle, IEnumerable<Region>>
{
    public MapColouringVariables()
    {
        Region X1 = Region.FromId("X1");
        Region X2 = Region.FromId("X2");
        Region X3 = Region.FromId("X3");
        Region X4 = Region.FromId("X4");
        Region X5 = Region.FromId("X5");
        Region X6 = Region.FromId("X6");
        Region X7 = Region.FromId("X7");

        Add(MapColouringPuzzle.Create()
            .WithGlobalColours(Colour.Black)
            .AddRegion(X1)
            .Build(), [X1]);

        Add(MapColouringPuzzle.Create()
            .WithGlobalColours(Colour.Black)
            .AddRegion(X3)
            .AddRegion(X1)
            .AddRegion(X2)
            .Build(), [X1, X2, X3]);

        Add(MapColouringPuzzle.Create()
            .WithGlobalColours(Colour.Black)
            .AddRegion(X2)
            .AddRegion(X4)
            .AddRegion(X3)
            .AddRegion(X1)
            .SetAsNeighbours(X1, X2)
            .Build(), [X1, X2, X3, X4]);

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
            .Build(), [X1, X2, X3, X4, X5, X6, X7]);
    }
}
