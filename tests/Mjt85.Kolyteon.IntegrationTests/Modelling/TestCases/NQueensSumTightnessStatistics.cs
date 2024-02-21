using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensSumTightnessStatistics : TheoryData<NQueensPuzzle, SumTightnessStatistics>
{
    public NQueensSumTightnessStatistics()
    {
        Add(NQueensPuzzle.FromN(1), new SumTightnessStatistics
        {
            MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
        });

        Add(NQueensPuzzle.FromN(2), new SumTightnessStatistics
        {
            MinimumValue = 1.0, MeanValue = 1.0, MaximumValue = 1.0, DistinctValues = 1
        });

        Add(NQueensPuzzle.FromN(5), new SumTightnessStatistics
        {
            MinimumValue = 1.6, MeanValue = 1.76, MaximumValue = 1.92, DistinctValues = 3
        });
    }
}
