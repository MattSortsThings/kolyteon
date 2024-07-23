using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.MapColouring;

namespace Kolyteon.Tests.Unit.MapColouring;

public static class MapColouringProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        private static readonly Block BlockA = Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1));
        private static readonly Block BlockB = Square.FromColumnAndRow(5, 5).ToBlock(Dimensions.FromWidthAndHeight(3, 3));

        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .Build();

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualCanvasValuesAndEqualBlockData_ReturnsTrue()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalCanvasValues_ReturnsFalse()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(99, 99))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalBlockData_ReturnsFalse()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.Red, Colour.Blue, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .Build();

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        private static readonly Block BlockA = Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1));
        private static readonly Block BlockB = Square.FromColumnAndRow(5, 5).ToBlock(Dimensions.FromWidthAndHeight(3, 3));

        [Fact]
        public void Equality_InstanceAndOtherHaveEqualCanvasValuesAndEqualBlockData_ReturnsTrue()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalCanvasValues_ReturnsFalse()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(99, 99))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalBlockData_ReturnsFalse()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.Red, Colour.Blue, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        private static readonly Block BlockA = Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1));
        private static readonly Block BlockB = Square.FromColumnAndRow(5, 5).ToBlock(Dimensions.FromWidthAndHeight(3, 3));

        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualCanvasValuesAndEqualBlockData_ReturnsFalse()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalCanvasValues_ReturnsTrue()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(99, 99))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalBlockData_ReturnsTrue()
        {
            // Arrange
            MapColouringProblem sut = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            MapColouringProblem other = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseGlobalColours(Colour.Black, Colour.Red, Colour.Blue, Colour.White)
                .AddBlock(BlockA)
                .AddBlock(BlockB).Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FluentBuilder
    {
        private static readonly Block BlockA = Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1));
        private static readonly Block BlockB = Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3));
        private static readonly Block BlockC = Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(4, 1));

        [Fact]
        public void FluentBuilder_CanBuildWithGlobalPermittedColours_BlockDataAreInAscendingOrder()
        {
            // Arrange
            Dimensions canvasDimensions = Dimensions.FromWidthAndHeight(9, 9);

            // Act
            MapColouringProblem result = MapColouringProblem.Create()
                .WithCanvasSize(canvasDimensions)
                .UseGlobalColours(Colour.Black, Colour.White, Colour.Black)
                .AddBlock(BlockB)
                .AddBlock(BlockA)
                .AddBlock(BlockC).Build();

            // Assert
            Block expectedCanvas = canvasDimensions.ToBlock();

            using (new AssertionScope())
            {
                result.Canvas.Should().Be(expectedCanvas);
                result.BlockData.Should().SatisfyRespectively(
                    datum =>
                    {
                        datum.Block.Should().Be(BlockA);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.White]);
                    }, datum =>
                    {
                        datum.Block.Should().Be(BlockB);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.White]);
                    }, datum =>
                    {
                        datum.Block.Should().Be(BlockC);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.White]);
                    }
                ).And.BeInAscendingOrder();
            }
        }

        [Fact]
        public void FluentBuilder_CanBuildWithBlockSpecificPermittedColours_BlockDataAreInAscendingOrder()
        {
            // Arrange
            Dimensions canvasDimensions = Dimensions.FromWidthAndHeight(20, 20);

            // Act
            MapColouringProblem result = MapColouringProblem.Create()
                .WithCanvasSize(canvasDimensions)
                .UseBlockSpecificColours()
                .AddBlockWithColours(BlockB)
                .AddBlockWithColours(BlockA, [Colour.Black])
                .AddBlockWithColours(BlockC, [Colour.Red, Colour.Red, Colour.Black, Colour.White]).Build();

            // Assert
            Block expectedCanvas = canvasDimensions.ToBlock();

            using (new AssertionScope())
            {
                result.Canvas.Should().Be(expectedCanvas);
                result.BlockData.Should().SatisfyRespectively(
                    datum =>
                    {
                        datum.Block.Should().Be(BlockA);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black]);
                    }, datum =>
                    {
                        datum.Block.Should().Be(BlockB);
                        datum.PermittedColours.Should().BeEmpty();
                    }, datum =>
                    {
                        datum.Block.Should().Be(BlockC);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.Red, Colour.White]);
                    }
                ).And.BeInAscendingOrder();
            }
        }

        [Fact]
        public void FluentBuilder_ZeroBlocks_Throws()
        {
            // Act
            Action act = () => MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseBlockSpecificColours()
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Problem has zero blocks.");
        }

        [Fact]
        public void FluentBuilder_BlockIsNotInsideCanvas_Throws()
        {
            // Act
            Action act = () => MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(5, 5))
                .UseBlockSpecificColours()
                .AddBlockWithColours(Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)))
                .AddBlockWithColours(Square.FromColumnAndRow(4, 4).ToBlock(Dimensions.FromWidthAndHeight(99, 99)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Block (4,4) [99x99] is not inside canvas (0,0) [5x5].");
        }

        [Fact]
        public void FluentBuilder_OverlappingBlocks_Throws()
        {
            // Act
            Action act = () => MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseBlockSpecificColours()
                .AddBlockWithColours(Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1)))
                .AddBlockWithColours(Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 5)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Blocks (0,0) [5x5] and (1,1) [1x1] overlap.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue()
        {
            // Arrange
            MapColouringProblem originalProblem = MapColouringProblem.Create()
                .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                .UseBlockSpecificColours()
                .AddBlockWithColours(Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                    Colour.Black, Colour.White)
                .AddBlockWithColours(Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                    Colour.Black, Colour.Red, Colour.White, Colour.White)
                .AddBlockWithColours(Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)))
                .AddBlockWithColours(Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                    Colour.Red)
                .Build();

            string json = JsonSerializer.Serialize(originalProblem, JsonSerializerOptions.Default);

            // Act
            MapColouringProblem? result = JsonSerializer.Deserialize<MapColouringProblem>(json, JsonSerializerOptions.Default);

            // Assert
            result.Should().NotBeNull().And.Be(originalProblem);
        }
    }
}
