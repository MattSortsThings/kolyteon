using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuDomains : TheoryData<ShikakuPuzzle, IEnumerable<IReadOnlyList<Rectangle>>>
{
    public ShikakuDomains()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), [
            [
                new Rectangle(0, 0, 5, 5)
            ]
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), [
            [
                new Rectangle(0, 0, 1, 5),
                new Rectangle(0, 0, 5, 1)
            ],
            [
                new Rectangle(0, 1, 5, 4),
                new Rectangle(1, 0, 4, 5)
            ]
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), [
            [
                new Rectangle(0, 0, 1, 4),
                new Rectangle(0, 0, 2, 2),
                new Rectangle(0, 0, 4, 1)
            ],
            Array.Empty<Rectangle>()
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), [
            [
                new Rectangle(0, 0, 5, 1)
            ],
            [
                new Rectangle(0, 1, 5, 4)
            ]
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), [
            [
                new Rectangle(0, 0, 2, 2),
                new Rectangle(1, 0, 1, 4)
            ],
            [
                new Rectangle(2, 0, 1, 5)
            ],
            [
                new Rectangle(2, 1, 3, 2),
                new Rectangle(3, 0, 2, 3)
            ],
            [
                new Rectangle(0, 3, 5, 2)
            ]
        ]);

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0010 },
            { 0005, null, null, null, null }
        }), [
            [
                new Rectangle(0, 0, 1, 5),
                new Rectangle(0, 4, 5, 1)
            ],
            [
                new Rectangle(0, 0, 5, 2)
            ],
            [
                new Rectangle(0, 2, 5, 2)
            ]
        ]);
    }
}
