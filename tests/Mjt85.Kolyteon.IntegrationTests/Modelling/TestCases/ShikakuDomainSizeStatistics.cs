using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuDomainSizeStatistics : TheoryData<ShikakuPuzzle, DomainSizeStatistics>
{
    public ShikakuDomainSizeStatistics()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new DomainSizeStatistics
        {
            MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), new DomainSizeStatistics
        {
            MinimumValue = 2, MeanValue = 2.0, MaximumValue = 2, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), new DomainSizeStatistics
        {
            MinimumValue = 0, MeanValue = 1.5, MaximumValue = 3, DistinctValues = 2
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new DomainSizeStatistics
        {
            MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), new DomainSizeStatistics
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
        }), new DomainSizeStatistics
        {
            MinimumValue = 1, MeanValue = 1.333333, MaximumValue = 2, DistinctValues = 2
        });
    }
}
