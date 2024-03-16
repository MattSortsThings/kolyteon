using Mjt85.Kolyteon.MapColouring;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

public static class GetBinaryCsp
{
    public static MapColouringBinaryCsp ModellingProblem(MapColouringPuzzle puzzle)
    {
        MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);
        binaryCsp.Model(puzzle);

        return binaryCsp;
    }

    public static MapColouringBinaryCsp EmptyWithCapacity(int capacity) => MapColouringBinaryCsp.WithInitialCapacity(capacity);
}
