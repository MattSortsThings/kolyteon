using System.Text.Json;
using Kolyteon.GraphColouring;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class EdgeTests
{
    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceFirstNodePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Node sharedSecondNode = Node.FromName("MM");

            Edge sut = Edge.Between(Node.FromName("AA"), sharedSecondNode);
            Edge other = Edge.Between(Node.FromName("LL"), sharedSecondNode);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameFirstNodeAndInstanceSecondNodePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("MM");

            Edge sut = Edge.Between(sharedFirstNode, Node.FromName("NN"));
            Edge other = Edge.Between(sharedFirstNode, Node.FromName("ZZ"));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualFirstNodeAndSecondNodeValues_ReturnsZero()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("AA");
            Node sharedSecondNode = Node.FromName("ZZ");

            Edge sut = Edge.Between(sharedFirstNode, sharedSecondNode);
            Edge other = Edge.Between(sharedFirstNode, sharedSecondNode);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceFirstNodeFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Node sharedSecondNode = Node.FromName("MM");

            Edge sut = Edge.Between(Node.FromName("LL"), sharedSecondNode);
            Edge other = Edge.Between(Node.FromName("AA"), sharedSecondNode);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameFirstNodeAndInstanceSecondNodeFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("MM");

            Edge sut = Edge.Between(sharedFirstNode, Node.FromName("ZZ"));
            Edge other = Edge.Between(sharedFirstNode, Node.FromName("NN"));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceAndOtherHaveEqualFirstNodeAndSecondNodeValues_ReturnsTrue()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("AA");
            Node sharedSecondNode = Node.FromName("ZZ");

            Edge sut = Edge.Between(sharedFirstNode, sharedSecondNode);
            Edge other = Edge.Between(sharedFirstNode, sharedSecondNode);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalFirstNodeValues_ReturnFalse()
        {
            // Arrange
            Node sharedSecondNode = Node.FromName("MM");

            Edge sut = Edge.Between(Node.FromName("LL"), sharedSecondNode);
            Edge other = Edge.Between(Node.FromName("AA"), sharedSecondNode);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSecondNodeValues_ReturnsFalse()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("MM");

            Edge sut = Edge.Between(sharedFirstNode, Node.FromName("ZZ"));
            Edge other = Edge.Between(sharedFirstNode, Node.FromName("NN"));

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        [Theory]
        [InlineData("A", "B", "(A)-(B)")]
        [InlineData("1", "2", "(1)-(2)")]
        [InlineData("Irish Sea", "Southeast Iceland", "(Irish Sea)-(Southeast Iceland)")]
        public void ToString_ReturnsFormattedString(string firstNodeName, string secondNodeName, string expected)
        {
            // Arrange
            Node firstNode = Node.FromName(firstNodeName);
            Node secondNode = Node.FromName(secondNodeName);

            Edge sut = Edge.Between(firstNode, secondNode);

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualFirstNodeAndSecondNodeValues_ReturnsTrue()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("AA");
            Node sharedSecondNode = Node.FromName("ZZ");

            Edge sut = Edge.Between(sharedFirstNode, sharedSecondNode);
            Edge other = Edge.Between(sharedFirstNode, sharedSecondNode);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalFirstNodeValues_ReturnFalse()
        {
            // Arrange
            Node sharedSecondNode = Node.FromName("MM");

            Edge sut = Edge.Between(Node.FromName("LL"), sharedSecondNode);
            Edge other = Edge.Between(Node.FromName("AA"), sharedSecondNode);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSecondNodeValues_ReturnsFalse()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("MM");

            Edge sut = Edge.Between(sharedFirstNode, Node.FromName("ZZ"));
            Edge other = Edge.Between(sharedFirstNode, Node.FromName("NN"));

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
        public void Inequality_InstanceAndOtherHaveEqualFirstNodeAndSecondNodeValues_ReturnsFalse()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("AA");
            Node sharedSecondNode = Node.FromName("ZZ");

            Edge sut = Edge.Between(sharedFirstNode, sharedSecondNode);
            Edge other = Edge.Between(sharedFirstNode, sharedSecondNode);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalFirstNodeValues_ReturnsTrue()
        {
            // Arrange
            Node sharedSecondNode = Node.FromName("MM");

            Edge sut = Edge.Between(Node.FromName("LL"), sharedSecondNode);
            Edge other = Edge.Between(Node.FromName("AA"), sharedSecondNode);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSecondNodeValues_ReturnsTrue()
        {
            // Arrange
            Node sharedFirstNode = Node.FromName("MM");

            Edge sut = Edge.Between(sharedFirstNode, Node.FromName("ZZ"));
            Edge other = Edge.Between(sharedFirstNode, Node.FromName("NN"));

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class BetweenStaticFactoryMethod
    {
        [Fact]
        public void Between_NodeAArgPrecedesNodeBArg_ReturnsInstanceWithNodeAFirstAndNodeBSecond()
        {
            // Arrange
            Node node1 = Node.FromName("node1");
            Node node9 = Node.FromName("node9");

            // Act
            Edge result = Edge.Between(node1, node9);

            // Assert
            using (new AssertionScope())
            {
                result.FirstNode.Should().Be(node1);
                result.SecondNode.Should().Be(node9);
            }
        }

        [Fact]
        public void Between_NodeAArgFollowsNodeBArg_ReturnsInstanceWithNodeBFirstAndNodeASecond()
        {
            // Arrange
            Node node1 = Node.FromName("node1");
            Node node9 = Node.FromName("node9");

            // Act
            Edge result = Edge.Between(node9, node1);

            // Assert
            using (new AssertionScope())
            {
                result.FirstNode.Should().Be(node1);
                result.SecondNode.Should().Be(node9);
            }
        }

        [Fact]
        public void Between_NodeAAndNodeBArgsAreEqual_Throws()
        {
            // Arrange
            Node node1 = Node.FromName("node1");

            // Act
            Action act = () => Edge.Between(node1, node1);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Nodes must be different.");
        }
    }

    [UnitTest]
    public sealed class ParseStaticMethod
    {
        public static TheoryData<string, Edge> HappyPathTestCases => new()
        {
            { "(A)-(B)", Edge.Between(Node.FromName("A"), Node.FromName("B")) },
            { "(FairIsle)-(Viking)", Edge.Between(Node.FromName("FairIsle"), Node.FromName("Viking")) },
            { "(n0)-(n1)", Edge.Between(Node.FromName("n0"), Node.FromName("n1")) }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(ParseStaticMethod))]
        public void Parse_GivenStringInCorrectFormat_ReturnsParsedEdge(string value, Edge expected)
        {
            // Act
            Edge result = Edge.Parse(value);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Parse_ValueArgIsNull_Throws()
        {
            // Act
            Action act = () => Edge.Parse(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'value')");
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("A B")]
        [InlineData("A-B")]
        [InlineData("(A) (B)")]
        [InlineData("( )-(B)")]
        [InlineData("[A]-[B]")]
        [InlineData("(A)-(A)")]
        public void Parse_ValueArgIsInWrongFormat_Throws(string value)
        {
            // Act
            Action act = () => Edge.Parse(value);

            // Assert
            string expectedMessage = $"String '{value}' was not recognized as a valid Edge.";

            act.Should().Throw<FormatException>()
                .WithMessage(expectedMessage);
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData("A", "B")]
        [InlineData("1", "2")]
        [InlineData("Irish Sea", "Southeast Iceland")]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(string firstNodeName, string secondNodeName)
        {
            // Arrange
            Node firstNode = Node.FromName(firstNodeName);
            Node secondNode = Node.FromName(secondNodeName);

            Edge originalEdge = Edge.Between(firstNode, secondNode);

            string json = JsonSerializer.Serialize(originalEdge, JsonSerializerOptions.Default);

            // Act
            Edge? deserializedEdge = JsonSerializer.Deserialize<Edge>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedEdge.Should().NotBeNull().And.Be(originalEdge);
        }
    }
}
