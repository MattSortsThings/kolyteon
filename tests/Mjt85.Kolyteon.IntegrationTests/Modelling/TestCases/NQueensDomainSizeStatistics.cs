using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensDomainSizeStatistics : TheoryData<NQueensPuzzle, DomainSizeStatistics>
{
    public NQueensDomainSizeStatistics()
    {
        Add(NQueensPuzzle.FromN(1), new DomainSizeStatistics
        {
            MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
        });

        Add(NQueensPuzzle.FromN(2), new DomainSizeStatistics
        {
            MinimumValue = 2, MeanValue = 2.0, MaximumValue = 2, DistinctValues = 1
        });

        Add(NQueensPuzzle.FromN(5), new DomainSizeStatistics
        {
            MinimumValue = 5, MeanValue = 5.0, MaximumValue = 5, DistinctValues = 1
        });
    }
}
