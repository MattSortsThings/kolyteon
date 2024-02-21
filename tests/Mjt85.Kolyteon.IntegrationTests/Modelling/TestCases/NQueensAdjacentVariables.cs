using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensAdjacentVariables : TheoryData<NQueensPuzzle, IEnumerable<Pair<int>>>
{
    public NQueensAdjacentVariables()
    {
        Add(NQueensPuzzle.FromN(1), []);

        Add(NQueensPuzzle.FromN(2), [
            new Pair<int>(0, 1)
        ]);

        Add(NQueensPuzzle.FromN(3), [
            new Pair<int>(0, 1),
            new Pair<int>(0, 2),
            new Pair<int>(1, 2)
        ]);

        Add(NQueensPuzzle.FromN(5), [
            new Pair<int>(0, 1),
            new Pair<int>(0, 2),
            new Pair<int>(0, 3),
            new Pair<int>(0, 4),
            new Pair<int>(1, 2),
            new Pair<int>(1, 3),
            new Pair<int>(1, 4),
            new Pair<int>(2, 3),
            new Pair<int>(2, 4),
            new Pair<int>(3, 4)
        ]);
    }
}
