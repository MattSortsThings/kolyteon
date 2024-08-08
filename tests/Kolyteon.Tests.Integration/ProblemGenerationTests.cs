using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Integration;

[IntegrationTest]
public abstract partial class ProblemGenerationTests
{
    private protected abstract int Seed { get; }

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
                .And.HaveCount(81 - emptySquares);

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
