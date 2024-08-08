using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class GraphColouringGeneratorTests
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
        public void Generate_GivenNodesAndPermittedColours_ReturnsInstanceWithNodeDataInAscendingOrder(int nodes,
            HashSet<Colour> permittedColours)
        {
            // Arrange
            GraphColouringGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            GraphColouringProblem result = sut.Generate(nodes, permittedColours);

            // Assert
            using (new AssertionScope())
            {
                result.NodeData.Should().BeInAscendingOrder()
                    .And.HaveCount(nodes)
                    .And.AllSatisfy(datum =>
                        datum.PermittedColours.Should()
                            .BeEquivalentTo(permittedColours, options => options.WithoutStrictOrdering()))
                    .And.AllSatisfy(datum =>
                        datum.Node.Name.Should().MatchRegex(@"^N\d\d$"));
            }
        }

        [Fact]
        public void Generate_NodesArgIsLessThanOne_Throws()
        {
            // Arrange
            GraphColouringGenerator sut = new(MinimalFakeRandom.Instance);

            HashSet<Colour> arbitraryColours = [Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow];

            // Act
            Action act = () => sut.Generate(0, arbitraryColours);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than or equal to 50. (Parameter 'nodes')\n" +
                             "Actual value was 0.");
        }

        [Fact]
        public void Generate_NodesArgIsGreaterThanFifty_Throws()
        {
            // Arrange
            GraphColouringGenerator sut = new(MinimalFakeRandom.Instance);

            HashSet<Colour> arbitraryColours = [Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow];

            // Act
            Action act = () => sut.Generate(51, arbitraryColours);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than or equal to 50. (Parameter 'nodes')\n" +
                             "Actual value was 51.");
        }

        [Fact]
        public void Generate_PermittedColoursArgIsNull_Throws()
        {
            // Arrange
            GraphColouringGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryNodes = 1;

            // Act
            Action act = () => sut.Generate(arbitraryNodes, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'permittedColours')");
        }

        [Fact]
        public void Generate_PermittedColoursArgHasFewerThanFourValues_Throws()
        {
            // Arrange
            GraphColouringGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryNodes = 1;

            HashSet<Colour> permittedColours = [Colour.Black];

            // Act
            Action act = () => sut.Generate(arbitraryNodes, permittedColours);

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
        public void Generate_GivenNodesAndPermittedColours_ReturnsInstanceWithNodeDataInAscendingOrder(int nodes,
            HashSet<Colour> permittedColours)
        {
            // Arrange
            GraphColouringGenerator sut = new(MaximalFakeRandom.Instance);

            // Act
            GraphColouringProblem result = sut.Generate(nodes, permittedColours);

            // Assert
            using (new AssertionScope())
            {
                result.NodeData.Should().BeInAscendingOrder()
                    .And.HaveCount(nodes)
                    .And.AllSatisfy(datum =>
                        datum.PermittedColours.Should()
                            .BeEquivalentTo(permittedColours, options => options.WithoutStrictOrdering()))
                    .And.AllSatisfy(datum =>
                        datum.Node.Name.Should().MatchRegex(@"^N\d\d$"));
            }
        }
    }
}
