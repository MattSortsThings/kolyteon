using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuAdjacentVariables : TheoryData<ShikakuPuzzle, IEnumerable<Pair<Hint>>>
{
    public ShikakuAdjacentVariables()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), Array.Empty<Pair<Hint>>());

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), [new Pair<Hint>(new Hint(0, 0, 5), new Hint(4, 4, 20))]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), Array.Empty<Pair<Hint>>());

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), Array.Empty<Pair<Hint>>());

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), [
            new Pair<Hint>(new Hint(1, 0, 4), new Hint(4, 3, 10)),
            new Pair<Hint>(new Hint(2, 0, 5), new Hint(4, 2, 6)),
            new Pair<Hint>(new Hint(2, 0, 5), new Hint(4, 3, 10))
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0010 },
            { 0005, null, null, null, null }
        }), [
            new Pair<Hint>(new Hint(0, 4, 5), new Hint(4, 0, 10)),
            new Pair<Hint>(new Hint(0, 4, 5), new Hint(4, 3, 10))
        ]);
    }
}
