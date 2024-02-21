using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensDomains : TheoryData<NQueensPuzzle, IEnumerable<IReadOnlyList<Queen>>>
{
    public NQueensDomains()
    {
        Add(NQueensPuzzle.FromN(1), [
            [new Queen(0, 0)]
        ]);

        Add(NQueensPuzzle.FromN(2), [
            [new Queen(0, 0), new Queen(0, 1)],
            [new Queen(1, 0), new Queen(1, 1)]
        ]);

        Add(NQueensPuzzle.FromN(5), [
            [new Queen(0, 0), new Queen(0, 1), new Queen(0, 2), new Queen(0, 3), new Queen(0, 4)],
            [new Queen(1, 0), new Queen(1, 1), new Queen(1, 2), new Queen(1, 3), new Queen(1, 4)],
            [new Queen(2, 0), new Queen(2, 1), new Queen(2, 2), new Queen(2, 3), new Queen(2, 4)],
            [new Queen(3, 0), new Queen(3, 1), new Queen(3, 2), new Queen(3, 3), new Queen(3, 4)],
            [new Queen(4, 0), new Queen(4, 1), new Queen(4, 2), new Queen(4, 3), new Queen(4, 4)]
        ]);
    }
}
