using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensDegreeStatistics : TheoryData<NQueensPuzzle, DegreeStatistics>
{
    public NQueensDegreeStatistics()
    {
        Add(NQueensPuzzle.FromN(1), new DegreeStatistics
        {
            MinimumValue = 0, MeanValue = 0, MaximumValue = 0, DistinctValues = 1
        });

        Add(NQueensPuzzle.FromN(2), new DegreeStatistics
        {
            MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
        });

        Add(NQueensPuzzle.FromN(5), new DegreeStatistics
        {
            MinimumValue = 4, MeanValue = 4.0, MaximumValue = 4, DistinctValues = 1
        });
    }
}
