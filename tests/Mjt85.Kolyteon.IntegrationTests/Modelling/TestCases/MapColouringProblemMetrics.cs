using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class MapColouringProblemMetrics : TheoryData<MapColouringPuzzle, ProblemMetrics>
{
    public MapColouringProblemMetrics()
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
            .Build(), new ProblemMetrics
        {
            Variables = 1, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
        });

        Add(MapColouringPuzzle.Create()
            .WithPresetMapAndGlobalColours(PresetMaps.Australia(), Colour.Red, Colour.Green, Colour.Blue)
            .Build(), new ProblemMetrics
        {
            Variables = 7, Constraints = 9, ConstraintDensity = 0.428571, ConstraintTightness = 0.333333
        });

        Add(MapColouringPuzzle.Create()
            .WithPresetMapAndGlobalColours(PresetMaps.Australia(), Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
            .Build(), new ProblemMetrics
        {
            Variables = 7, Constraints = 9, ConstraintDensity = 0.428571, ConstraintTightness = 0.25
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
            .Build(), new ProblemMetrics
        {
            Variables = 7, Constraints = 10, ConstraintDensity = 0.476190, ConstraintTightness = 0.307692
        });
    }
}
