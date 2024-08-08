using Kolyteon.Common;
using Kolyteon.MapColouring;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.MapColouring;

public static class MapColouringGeneratorTests
{
    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMinimalFakeRandom
    {
        public static TheoryData<int, HashSet<Colour>> TestCases => new()
        {
            { 1, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow] },
            { 2, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow] },
            { 10, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow, Colour.Black, Colour.White] },
            { 20, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow, Colour.Black, Colour.White] },
            { 49, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow, Colour.Black, Colour.White] },
            { 50, [Colour.Black, Colour.White, Colour.Olive, Colour.Maroon, Colour.Navy] }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(GenerateMethodUsingMinimalFakeRandom))]
        public void Generate_GivenBlocksAndPermittedColours_ReturnsInstanceWithBlockDataInAscendingOrder(int blocks,
            HashSet<Colour> permittedColours)
        {
            // Arrange
            MapColouringGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            MapColouringProblem result = sut.Generate(blocks, permittedColours);

            // Assert
            using (new AssertionScope())
            {
                result.BlockData.Should().BeInAscendingOrder()
                    .And.HaveCount(blocks)
                    .And.AllSatisfy(datum =>
                        result.Canvas.Contains(datum.Block).Should().BeTrue())
                    .And.AllSatisfy(datum =>
                        datum.PermittedColours.Should()
                            .BeEquivalentTo(permittedColours, options => options.WithoutStrictOrdering()));

                result.Canvas.Should().Be(Block.Parse("(0,0) [10x10]"));

                result.BlockData.Sum(datum => datum.Block.AreaInSquares).Should().Be(100);
            }
        }

        [Fact]
        public void Generate_BlocksArgIsLessThanOne_Throws()
        {
            // Arrange
            MapColouringGenerator sut = new(MinimalFakeRandom.Instance);

            HashSet<Colour> arbitraryColours = [Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow];

            // Act
            Action act = () => sut.Generate(0, arbitraryColours);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than or equal to 50. (Parameter 'blocks')\n" +
                             "Actual value was 0.");
        }

        [Fact]
        public void Generate_BlocksArgIsGreaterThanFifty_Throws()
        {
            // Arrange
            MapColouringGenerator sut = new(MinimalFakeRandom.Instance);

            HashSet<Colour> arbitraryColours = [Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow];

            // Act
            Action act = () => sut.Generate(51, arbitraryColours);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than or equal to 50. (Parameter 'blocks')\n" +
                             "Actual value was 51.");
        }

        [Fact]
        public void Generate_PermittedColoursArgIsNull_Throws()
        {
            // Arrange
            MapColouringGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryBlocks = 1;

            // Act
            Action act = () => sut.Generate(arbitraryBlocks, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'permittedColours')");
        }

        [Fact]
        public void Generate_PermittedColoursArgHasFewerThanFourValues_Throws()
        {
            // Arrange
            MapColouringGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryBlocks = 1;

            HashSet<Colour> permittedColours = [Colour.Black];

            // Act
            Action act = () => sut.Generate(arbitraryBlocks, permittedColours);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Must supply a set of at least 4 permitted colours. (Parameter 'permittedColours')");
        }
    }

    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMaximalFakeRandom
    {
        public static TheoryData<int, HashSet<Colour>> TestCases => new()
        {
            { 1, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow] },
            { 2, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow] },
            { 10, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow, Colour.Black, Colour.White] },
            { 20, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow, Colour.Black, Colour.White] },
            { 49, [Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow, Colour.Black, Colour.White] },
            { 50, [Colour.Black, Colour.White, Colour.Olive, Colour.Maroon, Colour.Navy] }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(GenerateMethodUsingMaximalFakeRandom))]
        public void Generate_GivenBlocksAndPermittedColours_ReturnsInstanceWithBlockDataInAscendingOrder(int blocks,
            HashSet<Colour> permittedColours)
        {
            // Arrange
            MapColouringGenerator sut = new(MaximalFakeRandom.Instance);

            // Act
            MapColouringProblem result = sut.Generate(blocks, permittedColours);

            // Assert
            using (new AssertionScope())
            {
                result.BlockData.Should().BeInAscendingOrder()
                    .And.HaveCount(blocks)
                    .And.AllSatisfy(datum =>
                        result.Canvas.Contains(datum.Block).Should().BeTrue())
                    .And.AllSatisfy(datum =>
                        datum.PermittedColours.Should()
                            .BeEquivalentTo(permittedColours, options => options.WithoutStrictOrdering()));

                result.Canvas.Should().Be(Block.Parse("(0,0) [10x10]"));
            }
        }
    }
}
