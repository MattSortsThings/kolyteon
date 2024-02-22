using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuSumTightnessStatistics : TheoryData<ShikakuPuzzle, SumTightnessStatistics>
{
    public ShikakuSumTightnessStatistics()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new SumTightnessStatistics
        {
            MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), new SumTightnessStatistics
        {
            MinimumValue = 0.5, MeanValue = 0.5, MaximumValue = 0.5, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), new SumTightnessStatistics
        {
            MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new SumTightnessStatistics
        {
            MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), new SumTightnessStatistics
        {
            MinimumValue = 0.5, MeanValue = 1.0, MaximumValue = 1.5, DistinctValues = 2
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0010 },
            { 0005, null, null, null, null }
        }), new SumTightnessStatistics
        {
            MinimumValue = 0.5, MeanValue = 0.666667, MaximumValue = 1.0, DistinctValues = 2
        });
    }
}
