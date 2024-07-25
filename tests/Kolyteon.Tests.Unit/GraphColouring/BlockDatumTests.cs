using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.GraphColouring;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class NodeDatumTests
{
    [UnitTest]
    public sealed class Constructor
    {
        [Fact]
        public void Constructor_PermittedColoursArgIsNull_Throws()
        {
            // Arrange
            Node arbitraryNode = Node.FromName("A");

            // Act
            Action act = () => _ = new NodeDatum(arbitraryNode, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'permittedColours')");
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        private static readonly HashSet<Colour> ArbitraryColours = [Colour.Black, Colour.White];

        [Fact]
        public void CompareTo_InstanceNodePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            NodeDatum sut = new(Node.FromName("AA"), ArbitraryColours);
            NodeDatum other = new(Node.FromName("ZZ"), ArbitraryColours);

            // Act
            int result = sut.CompareTo(other);

            // Act
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualNodeValues_ReturnsZero()
        {
            // Arrange
            Node sharedNode = Node.FromName("A");

            NodeDatum sut = new(sharedNode, ArbitraryColours);
            NodeDatum other = new(sharedNode, ArbitraryColours);

            // Act
            int result = sut.CompareTo(other);

            // Act
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNodeFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            NodeDatum sut = new(Node.FromName("ZZ"), ArbitraryColours);
            NodeDatum other = new(Node.FromName("AA"), ArbitraryColours);

            // Act
            int result = sut.CompareTo(other);

            // Act
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceAndOtherHaveEqualNodeValueAndSamePermittedColoursValues_ReturnsTrue()
        {
            // Arrange
            Node sharedNode = Node.FromName("AA");

            NodeDatum sut = new(sharedNode, [Colour.Black, Colour.Red, Colour.White]);
            NodeDatum other = new(sharedNode, [Colour.White, Colour.Red, Colour.Black]);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNodeValues_ReturnsFalse()
        {
            // Arrange
            Colour[] sharedColours = [Colour.Black, Colour.White];

            NodeDatum sut = new(Node.FromName("AA"), sharedColours);
            NodeDatum other = new(Node.FromName("ZZ"), sharedColours);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveDifferentPermittedColoursValues_ReturnsFalse()
        {
            // Arrange
            Node sharedNode = Node.FromName("A");

            NodeDatum sut = new(sharedNode, [Colour.Black, Colour.Black, Colour.White]);
            NodeDatum other = new(sharedNode, [Colour.Black, Colour.Red, Colour.White]);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualNodeValueAndSamePermittedColoursValues_ReturnsTrue()
        {
            // Arrange
            Node sharedNode = Node.FromName("A");

            NodeDatum sut = new(sharedNode, [Colour.Black, Colour.Red, Colour.White]);
            NodeDatum other = new(sharedNode, [Colour.White, Colour.Red, Colour.Black]);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNodeValues_ReturnsFalse()
        {
            // Arrange
            Colour[] sharedColours = [Colour.Black, Colour.White];

            NodeDatum sut = new(Node.FromName("AA"), sharedColours);
            NodeDatum other = new(Node.FromName("ZZ"), sharedColours);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveDifferentPermittedColoursValues_ReturnsFalse()
        {
            // Arrange
            Node sharedNode = Node.FromName("A");

            NodeDatum sut = new(sharedNode, [Colour.Black, Colour.Black, Colour.White]);
            NodeDatum other = new(sharedNode, [Colour.Black, Colour.Red, Colour.White]);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualNodeValueAndSamePermittedColoursValues_ReturnsFalse()
        {
            // Arrange
            Node sharedNode = Node.FromName("A");

            NodeDatum sut = new(sharedNode, [Colour.Black, Colour.Red, Colour.White]);
            NodeDatum other = new(sharedNode, [Colour.White, Colour.Red, Colour.Black]);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNodeValues_ReturnsTrue()
        {
            // Arrange
            Colour[] sharedColours = [Colour.Black, Colour.White];

            NodeDatum sut = new(Node.FromName("AA"), sharedColours);
            NodeDatum other = new(Node.FromName("ZZ"), sharedColours);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveDifferentPermittedColoursValues_ReturnsTrue()
        {
            // Arrange
            Node sharedNode = Node.FromName("A");

            NodeDatum sut = new(sharedNode, [Colour.Black, Colour.Black, Colour.White]);
            NodeDatum other = new(sharedNode, [Colour.Black, Colour.Red, Colour.White]);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<Node, IReadOnlyCollection<Colour>> TestCases => new()
        {
            { Node.FromName("Lundy"), Array.Empty<Colour>() },
            { Node.FromName("Fastnet"), [Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow] },
            { Node.FromName("Irish Sea"), [Colour.Black] }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(Node block, IReadOnlyCollection<Colour> colours)
        {
            // Arrange
            NodeDatum originalDatum = new(block, colours);

            string json = JsonSerializer.Serialize(originalDatum, JsonSerializerOptions.Default);

            // Act
            NodeDatum? deserializedDatum = JsonSerializer.Deserialize<NodeDatum>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedDatum.Should().NotBeNull().And.Be(originalDatum);
        }
    }
}
