using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Solving.TestCases;

public sealed class ShikakuPuzzles
{
    public sealed class Solvable : TheoryData<ShikakuPuzzle>
    {
        public Solvable()
        {
            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0025, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0020 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, 0010 },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 },
                { 0005, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, 0005, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, 0016, null, null, 0009, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, 0020, null, null, null, null, null, 0045, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { 0002, null, null, 0003, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0016, null, null, null, 0009, null, null, null, null, 0005 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { 0020, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { 0002, null, null, null, 0003, null, null, null, null, 0045 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, 0010, null, null, null, null, null, null },
                { 0015, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, 0025, null, null, null, 0010 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, 0012, null, null },
                { null, 0006, null, null, null, null, null, null, null, 0004 },
                { null, null, null, null, 0016, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, 0010, null, null, null, null, null, null },
                { 0006, null, null, null, null, 0010, null, null, null, 0004 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0010, null, null, null, null, null, null },
                { null, null, null, null, null, null, 0010, null, null, 0004 },
                { 0006, null, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, 0012, null, null },
                { null, 0006, null, null, null, null, null, null, null, 0004 },
                { null, null, null, null, 0016, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0025, null, null, 0015, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, 0002, 0012, null, null },
                { null, null, null, null, 0010, 0004, 0002, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, 0030, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, 0015, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, 0012, null, null },
                { null, null, null, 0020, 0005, null, 0002, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0010, 0004, 0002, null, null, null },
                { 0003, 0027, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, 0010, null, null, null },
                { null, null, null, 0010, null, null, null, null, null, null },
                { null, 0010, null, null, null, null, null, null, 0010, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0042, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { 0004, null, null, null, null, null, null, null, null, 0004 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, 0006, null, null, null, null, 0002 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0016, null, null, null, null, null, null },
                { null, 0010, null, null, null, null, null, 0004, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0020, null, null, null, null, null, null },
                { 0004, null, null, null, null, null, null, null, 0020, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null, null },
                { null, null, null, null, null, null, 0020, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0008, null, null, null, null, null, null, null, null, 0016 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, 0008, 0004, null, null, null },
                { null, null, null, null, null, null, 0004, null, null, null },
                { null, null, null, null, null, null, 0010, null, null, null },
                { 0004, null, null, null, null, 0008, 0012, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, 0006, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0020, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, 0010 },
                { 0002, null, null, null, null, null, null, null, null, 0008 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0064, null, null, null, null, null, null },
                { 0002, null, null, null, null, null, null, null, null, null },
                { 0005, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0004, null, 0002, 0003, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, 0021, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0009, null },
                { null, null, null, null, null, null, null, null, 0003, null },
                { null, null, null, null, null, null, null, null, null, null },
                { 0021, null, null, null, null, null, null, 0006, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, 0040, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0012, null, null, null, null, 0009, null },
                { null, null, null, 0012, null, null, null, null, 0003, null },
                { null, null, null, null, null, 0030, null, null, null, null },
                { null, null, null, null, null, null, null, 0006, null, null },
                { null, null, null, 0016, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, 0012, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, 0015, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0018, null },
                { null, null, 0009, null, null, null, null, null, null, null },
                { null, null, 0006, 0005, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0007, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, 0040, null, null, null }
            }));
        }
    }

    public sealed class Unsolvable : TheoryData<ShikakuPuzzle>
    {
        public Unsolvable()
        {
            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, 0010, null, null },
                { null, null, null, 0006, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0009 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, 0010, null },
                { null, null, null, null, null },
                { null, null, null, null, 0003 },
                { null, null, null, null, null },
                { null, 0010, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0004, 0004, 0004, null, 0005 },
                { null, null, null, null, null },
                { null, null, null, 0004, null },
                { null, null, null, 0004, null },
                { null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0004, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0021 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0004, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { 0005, 0006, null, null, null },
                { null, null, null, null, 0010 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, 0004, 0005, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0006 },
                { null, null, null, null, 0010 },
                { null, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 },
                { null, null, null, null, 0008 },
                { 0005, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 },
                { null, null, null, null, 0008 },
                { 0005, null, null, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0004, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0006 }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, 0010 },
                { 0002, null, null, null, null, null, null, null, null, 0008 },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, 0060, 0004, null, null, null, null, null },
                { 0002, null, null, null, null, null, null, null, null, null },
                { 0005, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0004, null, 0002, 0003, null, null }
            }));

            Add(ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, 0021, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0009, null },
                { null, null, null, null, null, null, null, null, 0003, null },
                { null, null, null, null, null, null, null, null, null, null },
                { 0021, null, null, null, null, null, null, 0006, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0012, 0028, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            }));
        }
    }
}
