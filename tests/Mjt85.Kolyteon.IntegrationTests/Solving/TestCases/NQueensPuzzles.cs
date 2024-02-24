using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Solving.TestCases;

public sealed class NQueensPuzzles
{
    public sealed class Solvable : TheoryData<NQueensPuzzle>
    {
        public Solvable()
        {
            Add(NQueensPuzzle.FromN(1));
            Add(NQueensPuzzle.FromN(4));
            Add(NQueensPuzzle.FromN(5));
            Add(NQueensPuzzle.FromN(6));
            Add(NQueensPuzzle.FromN(7));
            Add(NQueensPuzzle.FromN(8));
            Add(NQueensPuzzle.FromN(9));
            Add(NQueensPuzzle.FromN(10));
            Add(NQueensPuzzle.FromN(11));
            Add(NQueensPuzzle.FromN(12));
            Add(NQueensPuzzle.FromN(13));
            Add(NQueensPuzzle.FromN(14));
            Add(NQueensPuzzle.FromN(15));
        }
    }

    public sealed class Unsolvable : TheoryData<NQueensPuzzle>
    {
        public Unsolvable()
        {
            Add(NQueensPuzzle.FromN(2));
            Add(NQueensPuzzle.FromN(3));
        }
    }
}
