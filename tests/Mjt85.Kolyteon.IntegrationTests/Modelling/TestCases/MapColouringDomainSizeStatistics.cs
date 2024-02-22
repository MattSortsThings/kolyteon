using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class MapColouringDomainSizeStatistics : TheoryData<MapColouringPuzzle, DomainSizeStatistics>
{
    public MapColouringDomainSizeStatistics()
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
            .Build(), new DomainSizeStatistics
        {
            MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
        });

        Add(MapColouringPuzzle.Create()
            .WithPresetMapAndGlobalColours(PresetMaps.Australia(), Colour.Red, Colour.Green, Colour.Blue)
            .Build(), new DomainSizeStatistics
        {
            MinimumValue = 3, MeanValue = 3.0, MaximumValue = 3, DistinctValues = 1
        });

        Add(MapColouringPuzzle.Create()
            .WithPresetMapAndGlobalColours(PresetMaps.Australia(), Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
            .Build(), new DomainSizeStatistics
        {
            MinimumValue = 4, MeanValue = 4.0, MaximumValue = 4, DistinctValues = 1
        });

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
            .Build(), new DomainSizeStatistics
        {
            MinimumValue = 2, MeanValue = 2.285714, MaximumValue = 3, DistinctValues = 2
        });
    }
}
