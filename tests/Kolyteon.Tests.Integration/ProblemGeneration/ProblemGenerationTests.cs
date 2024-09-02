using System.Linq.Expressions;
using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.Shikaku;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Integration.ProblemGeneration;

[IntegrationTest]
public abstract partial class ProblemGenerationTests
{
    private protected abstract int Seed { get; }

    private static Expression<Func<FutoshikiProblem, bool>> ProblemHasAtLeastOneSign() =>
        problem => problem.LessThanSigns.Count > 0 || problem.GreaterThanSigns.Count > 0;

    [Theory]
    [InlineData(4, 1, 15)]
    [InlineData(4, 2, 14)]
    [InlineData(4, 12, 4)]
    [InlineData(4, 15, 1)]
    [InlineData(5, 1, 24)]
    [InlineData(5, 2, 23)]
    [InlineData(5, 20, 5)]
    [InlineData(5, 24, 1)]
    [InlineData(9, 1, 80)]
    [InlineData(9, 2, 79)]
    [InlineData(9, 70, 11)]
    [InlineData(9, 80, 1)]
    public void CanGenerateFutoshikiProblemFromGridSideLengthAndEmptySquares(int gridSideLength,
        int emptySquares,
        int expectedFilledSquares)
    {
        // Arrange
        FutoshikiGenerator generator = new(Seed);

        // Act
        FutoshikiProblem result = generator.Generate(gridSideLength, emptySquares);

        // Assert
        Block expectedGrid = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

        using (new AssertionScope())
        {
            result.Grid.Should().Be(expectedGrid);

            result.FilledSquares.Should().BeInAscendingOrder()
                .And.HaveCount(expectedFilledSquares)
                .And.AllSatisfy(filledSquare =>
                    result.Grid.Contains(filledSquare).Should().BeTrue())
                .And.AllSatisfy(filledSquare =>
                    filledSquare.Number.Should().BeGreaterOrEqualTo(1).And.BeLessThanOrEqualTo(gridSideLength));

            result.Should().Match(ProblemHasAtLeastOneSign());
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(20)]
    [InlineData(30)]
    [InlineData(39)]
    [InlineData(40)]
    [InlineData(49)]
    [InlineData(50)]
    public void CanGenerateGraphColouringProblemFromNodesAndPermittedColours(int nodes)
    {
        // Arrange
        GraphColouringGenerator generator = new(Seed);

        HashSet<Colour> colours = [Colour.Black, Colour.Aqua, Colour.Fuchsia, Colour.Yellow, Colour.White];

        // Act
        GraphColouringProblem result = generator.Generate(nodes, colours);

        // Assert
        using (new AssertionScope())
        {
            result.NodeData.Should().BeInAscendingOrder()
                .And.HaveCount(nodes)
                .And.AllSatisfy(datum =>
                    datum.PermittedColours.Should()
                        .BeEquivalentTo(colours, options => options.WithoutStrictOrdering()))
                .And.AllSatisfy(datum =>
                    datum.Node.Name.Should().MatchRegex(@"^N\d\d$"));
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(20)]
    [InlineData(30)]
    [InlineData(39)]
    [InlineData(40)]
    [InlineData(49)]
    [InlineData(50)]
    public void CanGenerateMapColouringProblemFromBlocksAndPermittedColours(int blocks)
    {
        // Arrange
        MapColouringGenerator generator = new(Seed);

        HashSet<Colour> colours = [Colour.Black, Colour.Aqua, Colour.Fuchsia, Colour.Yellow, Colour.White];

        // Act
        MapColouringProblem result = generator.Generate(blocks, colours);

        // Assert
        using (new AssertionScope())
        {
            result.BlockData.Should().BeInAscendingOrder()
                .And.HaveCount(blocks)
                .And.AllSatisfy(datum =>
                    result.Canvas.Contains(datum.Block).Should().BeTrue())
                .And.AllSatisfy(datum =>
                    datum.PermittedColours.Should()
                        .BeEquivalentTo(colours, options => options.WithoutStrictOrdering()));

            result.Canvas.Should().Be(Block.Parse("(0,0) [10x10]"));

            result.BlockData.Sum(datum => datum.Block.AreaInSquares).Should().Be(100);
        }
    }

    [Theory]
    [InlineData(5, 1)]
    [InlineData(5, 10)]
    [InlineData(10, 1)]
    [InlineData(10, 20)]
    [InlineData(15, 1)]
    [InlineData(15, 19)]
    [InlineData(15, 30)]
    [InlineData(20, 1)]
    [InlineData(20, 19)]
    [InlineData(20, 40)]
    public void CanGenerateShikakuProblemFromGridSideLengthAndHints(int gridSideLength, int hints)
    {
        // Arrange
        ShikakuGenerator generator = new(Seed);

        // Act
        ShikakuProblem result = generator.Generate(gridSideLength, hints);

        // Assert
        Block expectedGrid = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

        using (new AssertionScope())
        {
            result.Grid.Should().Be(expectedGrid);

            result.Hints.Should().BeInAscendingOrder()
                .And.HaveCount(hints)
                .And.AllSatisfy(hint => result.Grid.Contains(hint).Should().BeTrue())
                .And.AllSatisfy(hint => hint.Number.Should().BeGreaterOrEqualTo(2));

            result.Hints.Sum(hint => hint.Number).Should().Be(result.Grid.AreaInSquares);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(23)]
    [InlineData(30)]
    [InlineData(55)]
    [InlineData(63)]
    [InlineData(79)]
    [InlineData(80)]
    public void CanGenerateSudokuProblemFromEmptySquares(int emptySquares)
    {
        // Arrange
        SudokuGenerator generator = new(Seed);

        // Act
        SudokuProblem result = generator.Generate(emptySquares);

        // Assert
        using (new AssertionScope())
        {
            result.FilledSquares.Should().BeInAscendingOrder()
                .And.HaveCount(81 - emptySquares)
                .And.AllSatisfy(filledSquare =>
                    result.Grid.Contains(filledSquare).Should().BeTrue())
                .And.AllSatisfy(filledSquare =>
                    filledSquare.Number.Should().BeGreaterOrEqualTo(1).And.BeLessThanOrEqualTo(9));

            result.Grid.Should().Be(Block.Parse("(0,0) [9x9]"));

            result.Sectors.Should().Equal(Block.Parse("(0,0) [3x3]"),
                Block.Parse("(0,3) [3x3]"),
                Block.Parse("(0,6) [3x3]"),
                Block.Parse("(3,0) [3x3]"),
                Block.Parse("(3,3) [3x3]"),
                Block.Parse("(3,6) [3x3]"),
                Block.Parse("(6,0) [3x3]"),
                Block.Parse("(6,3) [3x3]"),
                Block.Parse("(6,6) [3x3]"));
        }
    }
}
