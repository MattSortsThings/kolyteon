using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuDegreeStatistics : TheoryData<ShikakuPuzzle, DegreeStatistics>
{
    public ShikakuDegreeStatistics()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new DegreeStatistics
        {
            MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), new DegreeStatistics
        {
            MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), new DegreeStatistics
        {
            MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new DegreeStatistics
        {
            MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), new DegreeStatistics
        {
            MinimumValue = 1, MeanValue = 1.5, MaximumValue = 2, DistinctValues = 2
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0010 },
            { 0005, null, null, null, null }
        }), new DegreeStatistics
        {
            MinimumValue = 1, MeanValue = 1.333333, MaximumValue = 2, DistinctValues = 2
        });
    }
}
