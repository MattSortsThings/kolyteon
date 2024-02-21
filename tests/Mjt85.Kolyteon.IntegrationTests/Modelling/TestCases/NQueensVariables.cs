using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensVariables : TheoryData<NQueensPuzzle, IEnumerable<int>>
{
    public NQueensVariables()
    {
        Add(NQueensPuzzle.FromN(1), [0]);
        Add(NQueensPuzzle.FromN(2), [0, 1]);
        Add(NQueensPuzzle.FromN(5), [0, 1, 2, 3, 4]);
    }
}
