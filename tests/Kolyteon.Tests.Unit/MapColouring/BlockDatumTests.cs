using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.MapColouring;

namespace Kolyteon.Tests.Unit.MapColouring;

public static class BlockDatumTests
{
    [UnitTest]
    public sealed class Constructor
    {
        [Fact]
        public void Constructor_PermittedColoursArgIsNull_Throws()
        {
            // Arrange
            Block arbitraryBlock = Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            Action act = () => _ = new BlockDatum(arbitraryBlock, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'permittedColours')");
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        private static readonly HashSet<Colour> ArbitraryColours = [Colour.Black, Colour.White];
        private static readonly Dimensions OneByOne = Dimensions.FromWidthAndHeight(1, 1);

        [Fact]
        public void CompareTo_InstanceBlockPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            BlockDatum sut = new(Square.FromColumnAndRow(0, 0).ToBlock(OneByOne), ArbitraryColours);
            BlockDatum other = new(Square.FromColumnAndRow(99, 99).ToBlock(OneByOne), ArbitraryColours);

            // Act
            int result = sut.CompareTo(other);

            // Act
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualBlockValues_ReturnsZero()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, ArbitraryColours);
            BlockDatum other = new(sharedBlock, ArbitraryColours);

            // Act
            int result = sut.CompareTo(other);

            // Act
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceBlockFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            BlockDatum sut = new(Square.FromColumnAndRow(99, 99).ToBlock(OneByOne), ArbitraryColours);
            BlockDatum other = new(Square.FromColumnAndRow(0, 0).ToBlock(OneByOne), ArbitraryColours);

            // Act
            int result = sut.CompareTo(other);

            // Act
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        private static readonly Dimensions OneByOne = Dimensions.FromWidthAndHeight(1, 1);

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualBlockValueAndSamePermittedColoursValues_ReturnsTrue()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, [Colour.Black, Colour.Red, Colour.White]);
            BlockDatum other = new(sharedBlock, [Colour.White, Colour.Red, Colour.Black]);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalBlockValues_ReturnsFalse()
        {
            // Arrange
            Colour[] sharedColours = [Colour.Black, Colour.White];

            BlockDatum sut = new(Square.FromColumnAndRow(0, 0).ToBlock(OneByOne), sharedColours);
            BlockDatum other = new(Square.FromColumnAndRow(99, 99).ToBlock(OneByOne), sharedColours);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveDifferentPermittedColoursValues_ReturnsFalse()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, [Colour.Black, Colour.Black, Colour.White]);
            BlockDatum other = new(sharedBlock, [Colour.Black, Colour.Red, Colour.White]);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        private static readonly Dimensions OneByOne = Dimensions.FromWidthAndHeight(1, 1);

        [Fact]
        public void Equality_InstanceAndOtherHaveEqualBlockValueAndSamePermittedColoursValues_ReturnsTrue()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, [Colour.Black, Colour.Red, Colour.White]);
            BlockDatum other = new(sharedBlock, [Colour.White, Colour.Red, Colour.Black]);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalBlockValues_ReturnsFalse()
        {
            // Arrange
            Colour[] sharedColours = [Colour.Black, Colour.White];

            BlockDatum sut = new(Square.FromColumnAndRow(0, 0).ToBlock(OneByOne), sharedColours);
            BlockDatum other = new(Square.FromColumnAndRow(99, 99).ToBlock(OneByOne), sharedColours);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveDifferentPermittedColoursValues_ReturnsFalse()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, [Colour.Black, Colour.Black, Colour.White]);
            BlockDatum other = new(sharedBlock, [Colour.Black, Colour.Red, Colour.White]);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        private static readonly Dimensions OneByOne = Dimensions.FromWidthAndHeight(1, 1);

        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualBlockValueAndSamePermittedColoursValues_ReturnsFalse()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, [Colour.Black, Colour.Red, Colour.White]);
            BlockDatum other = new(sharedBlock, [Colour.White, Colour.Red, Colour.Black]);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalBlockValues_ReturnsTrue()
        {
            // Arrange
            Colour[] sharedColours = [Colour.Black, Colour.White];

            BlockDatum sut = new(Square.FromColumnAndRow(0, 0).ToBlock(OneByOne), sharedColours);
            BlockDatum other = new(Square.FromColumnAndRow(99, 99).ToBlock(OneByOne), sharedColours);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveDifferentPermittedColoursValues_ReturnsTrue()
        {
            // Arrange
            Block sharedBlock = Square.FromColumnAndRow(0, 0).ToBlock(OneByOne);

            BlockDatum sut = new(sharedBlock, [Colour.Black, Colour.Black, Colour.White]);
            BlockDatum other = new(sharedBlock, [Colour.Black, Colour.Red, Colour.White]);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<Block, IReadOnlyCollection<Colour>> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Array.Empty<Colour>() },
            {
                Square.FromColumnAndRow(6, 3).ToBlock(Dimensions.FromWidthAndHeight(10, 20)),
                [Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow]
            },
            { Square.FromColumnAndRow(10, 13).ToBlock(Dimensions.FromWidthAndHeight(1, 5)), [Colour.Black] }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(Block block, IReadOnlyCollection<Colour> colours)
        {
            // Arrange
            BlockDatum originalDatum = new(block, colours);

            string json = JsonSerializer.Serialize(originalDatum, JsonSerializerOptions.Default);

            // Act
            BlockDatum? deserializedDatum = JsonSerializer.Deserialize<BlockDatum>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedDatum.Should().NotBeNull().And.Be(originalDatum);
        }
    }
}
