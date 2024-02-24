using Mjt85.Kolyteon.MapColouring;

namespace Mjt85.Kolyteon.IntegrationTests.Solving.TestCases;

public sealed class MapColouringPuzzles
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");
    private static readonly Region R3 = Region.FromId("R3");
    private static readonly Region R4 = Region.FromId("R4");
    private static readonly Region R5 = Region.FromId("R5");
    private static readonly Region R6 = Region.FromId("R6");

    public sealed class Solvable : TheoryData<MapColouringPuzzle>
    {
        public Solvable()
        {
            Add(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .AddRegion(R1)
                .AddRegion(R2)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.DarkRed, Colour.DarkGreen, Colour.DarkGrey)
                .AddRegions([R0, R1, R2, R3, R4, R5])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R0, R3)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R1, R3)
                .SetAsNeighbours(R2, R3)
                .SetAsNeighbours(R4, R5)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion(R0)
                .AddRegion(R1)
                .SetAsNeighbours(R0, R1)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1, Colour.Black)
                .AddRegionWithColours(R2, Colour.DarkMagenta)
                .AddRegionWithColours(R3, Colour.Black, Colour.DarkMagenta)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R3)
                .SetAsNeighbours(R3, R0)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Red, Colour.Blue, Colour.Green)
                .AddRegionWithColours(R1, Colour.Blue, Colour.Green)
                .AddRegionWithColours(R2, Colour.Red, Colour.Blue)
                .AddRegionWithColours(R3, Colour.Red, Colour.Blue)
                .AddRegionWithColours(R4, Colour.Blue, Colour.Green)
                .AddRegionWithColours(R5, Colour.Red, Colour.Green, Colour.Yellow)
                .AddRegionWithColours(R6, Colour.Red, Colour.Blue)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R0, R3)
                .SetAsNeighbours(R0, R6)
                .SetAsNeighbours(R1, R5)
                .SetAsNeighbours(R2, R6)
                .SetAsNeighbours(R3, R4)
                .SetAsNeighbours(R3, R6)
                .SetAsNeighbours(R4, R5)
                .SetAsNeighbours(R4, R6)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Australia(),
                    Colour.Cyan, Colour.Magenta, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Australia(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Australia(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Canada(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Canada(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.CityOfLondon(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.CityOfLondon(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Japan(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Japan(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Paris(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Paris(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Rwanda(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Rwanda(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.SanMarino(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.SanMarino(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.UKShippingForecast(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.UKShippingForecast(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.UnitedStates(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.UnitedStates(),
                    Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, Colour.White)
                .Build());
        }
    }

    public sealed class Unsolvable : TheoryData<MapColouringPuzzle>
    {
        public Unsolvable()
        {
            Add(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion(R0)
                .AddRegion(R1)
                .AddRegion(R2)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R0)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Red, Colour.Blue, Colour.Green)
                .AddRegionWithColours(R1, Colour.Blue, Colour.Green)
                .AddRegionWithColours(R2)
                .AddRegionWithColours(R3, Colour.Red, Colour.Blue)
                .AddRegionWithColours(R4, Colour.Blue, Colour.Green)
                .AddRegionWithColours(R5, Colour.Red, Colour.Green, Colour.Yellow)
                .AddRegionWithColours(R6, Colour.Red, Colour.Blue)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R0, R3)
                .SetAsNeighbours(R0, R6)
                .SetAsNeighbours(R1, R5)
                .SetAsNeighbours(R2, R6)
                .SetAsNeighbours(R3, R4)
                .SetAsNeighbours(R3, R6)
                .SetAsNeighbours(R4, R5)
                .SetAsNeighbours(R4, R6)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Australia(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Canada(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.CityOfLondon(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Japan(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Paris(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.Rwanda(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.SanMarino(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.UKShippingForecast(),
                    Colour.Black, Colour.White)
                .Build());

            Add(MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(PresetMaps.UnitedStates(),
                    Colour.Black, Colour.White)
                .Build());
        }
    }
}
