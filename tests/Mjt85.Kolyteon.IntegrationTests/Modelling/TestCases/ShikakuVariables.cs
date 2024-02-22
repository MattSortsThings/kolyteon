using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuVariables : TheoryData<ShikakuPuzzle, IEnumerable<Hint>>
{
    public ShikakuVariables()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), [
            new Hint(0, 0, 25)
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), [
            new Hint(0, 0, 5),
            new Hint(4, 4, 20)
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), [
            new Hint(0, 0, 4),
            new Hint(4, 4, 21)
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), [
            new Hint(0, 0, 5),
            new Hint(0, 1, 20)
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), [
            new Hint(1, 0, 4),
            new Hint(2, 0, 5),
            new Hint(4, 2, 6),
            new Hint(4, 3, 10)
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0010 },
            { 0005, null, null, null, null }
        }), [
            new Hint(0, 4, 5),
            new Hint(4, 0, 10),
            new Hint(4, 3, 10)
        ]);
    }
}
